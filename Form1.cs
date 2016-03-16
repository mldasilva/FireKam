//----------------------------------------------------------------------------
//  Copyright (C) 2004-2015 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------
// https://www.youtube.com/watch?v=mM4fLnRDVto
// Install-Package MetroFramework

using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;
using System.Diagnostics;
using Emgu.CV.GPU;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Windows.Forms.DataVisualization.Charting;
using MetroFramework.Forms;
using MongoDB.Bson;
using System.Timers;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MotionDetection
{
   public partial class FireKAM : MetroForm
   {
        private Capture _capture;
        private MotionHistory _motionHistory;
        private BackgroundSubtractor _forgroundDetector;
        private bool checkTimer;
        Heatmap pHeatmap;


        public FireKAM()
        {
            InitializeComponent();
            checkTimer = true;
            timer.Start();
            this.StyleManager = msmMain;
            ReportTypeCB.SelectedIndex = 0;
            chart1.Series["Camera"].XValueType = ChartValueType.Auto;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            pHeatmap = new Heatmap();
            pictureBox3.Parent = capturedImageBox;

            //try to create the capture
            if (_capture == null)
            {
                try
                {
                    //"..\\..\\videoplayback480p.mp4"
                    _capture = new Capture("..\\..\\videoplayback720p.mp4");
                }
                catch (NullReferenceException excpt)
                {   //show errors if there is any
                    MessageBox.Show(excpt.Message);
                }
                }

         if (_capture != null) //if camera capture has been successfully created
         {
            _motionHistory = new MotionHistory(
                1.0, //in second, the duration of motion history you wants to keep
                0.05, //in second, maxDelta for cvCalcMotionGradient
                0.5); //in second, minDelta for cvCalcMotionGradient

            _capture.ImageGrabbed += ProcessFrame;
            _capture.Start();
         }
      }

        private Mat _segMask = new Mat();
        private Mat _forgroundMask = new Mat();
        Heatmap myHeatMap = new Heatmap();

        private async void ProcessFrame(object sender, EventArgs e)
        {
            Mat image = new Mat();

            _capture.Retrieve(image);
            if (_forgroundDetector == null)
            {
                _forgroundDetector = new BackgroundSubtractorMOG2();
            }

            _forgroundDetector.Apply(image, _forgroundMask);

            //update the motion history
            _motionHistory.Update(_forgroundMask);         

            #region get a copy of the motion mask and enhance its color
            double[] minValues, maxValues;
            Point[] minLoc, maxLoc;
            _motionHistory.Mask.MinMax(out minValues, out maxValues, out minLoc, out maxLoc);
            Mat motionMask = new Mat();
            using (ScalarArray sa = new ScalarArray(255.0 / maxValues[0]))
            CvInvoke.Multiply(_motionHistory.Mask, sa, motionMask, 1, DepthType.Cv8U);
            //Image<Gray, Byte> motionMask = _motionHistory.Mask.Mul(255.0 / maxValues[0]);
            #endregion

            //create the motion image 
            Mat motionImage = new Mat(motionMask.Size.Height, motionMask.Size.Width, DepthType.Cv8U, 3);
            //display the motion pixels in blue (first channel)
            //motionImage[0] = motionMask;
            CvInvoke.InsertChannel(motionMask, motionImage, 0);

            //Threshold to define a motion area, reduce the value to detect smaller motion
            double minArea = 1000;

            //storage.Clear(); //clear the storage
            System.Drawing.Rectangle[] rects;
            using (VectorOfRect boundingRect = new VectorOfRect())
            {
                _motionHistory.GetMotionComponents(_segMask, boundingRect);
                rects = boundingRect.ToArray();
            }

            int motionCount = 0;
            //iterate through each of the motion component
            foreach (System.Drawing.Rectangle comp in rects)
            {              
                int area = comp.Width * comp.Height;
                //reject the components that have small area;
                if (area < minArea) continue;

                //create heatmap
                myHeatMap.heatPoints.Add(new HeatPoint(comp.X, comp.Y, 32));

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCount;
                _motionHistory.MotionInfo(_forgroundMask, comp, out angle, out motionPixelCount);

                //reject the area that contains too few motion
                if (motionPixelCount < area * 0.05) continue;

                //Draw each individual motion in red
                DrawMotion(motionImage, comp, angle, new Bgr(Color.Red));
                motionCount++;
            }

            //pictureBox3.BackgroundImage = myHeatMap.CreateHeatmap();
            Bitmap b = (Bitmap)myHeatMap.CreateHeatmap();
            //Image<Gray, Byte> normalizedMasterImage = new Image<Gray, Byte>(b);
            //motionImage = normalizedMasterImage.Mat;
            pictureBox3.BackgroundImage = b;
            

            // find and draw the overall motion angle
            double overallAngle, overallMotionPixelCount;
         
            _motionHistory.MotionInfo(_forgroundMask, new System.Drawing.Rectangle(Point.Empty, motionMask.Size), out overallAngle, out overallMotionPixelCount);
            DrawMotion(motionImage, new System.Drawing.Rectangle(Point.Empty, motionMask.Size), overallAngle, new Bgr(Color.Green));

            if (this.Disposing || this.IsDisposed)
            { 
                return;
            }

            //find pedestrian
            bool tryUseCuda = true;
            bool tryuseOpenCL = false;
            long processingTime;
            System.Drawing.Rectangle[] pedestrianRestult = FindPedestrian.Find(image, tryUseCuda, tryuseOpenCL, out processingTime);
            foreach (System.Drawing.Rectangle rect in pedestrianRestult)
            {
                CvInvoke.Rectangle(image, rect, new Bgr(Color.Gold).MCvScalar);
            }

            capturedImageBox.Image = image;
            //forgroundImageBox.Image = _forgroundMask;

            //Display the amount of motions found on the current image
            UpdateText(String.Format("Total Motions found: {0}; Motion Pixel count: {1}", motionCount, overallMotionPixelCount));

            //write into db
            if (checkTimer)
            {          
                var document = new BsonDocument
                {
                    {"Camera", "Camera1"},
                    {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    {"MotionCout", motionCount }
                };
                await DataAccess.Insert(document);
            }

            motionCount = 0;
            //Display the image of the motion
            motionImageBox.Image = motionImage;

      }

        private void UpdateText(String text)
      {
         if (!IsDisposed && !Disposing && InvokeRequired)
         {
            Invoke((Action<String>)UpdateText, text);
         }
         else
         {
            label3.Text = text;
         }
      }

      private static void DrawMotion(IInputOutputArray image, System.Drawing.Rectangle motionRegion, double angle, Bgr color)
      {
         CvInvoke.Rectangle(image, motionRegion, new MCvScalar(color.MCvScalar.V0, color.MCvScalar.V1, color.MCvScalar.V2));
         //float circleRadius = (motionRegion.Width + motionRegion.Height) >> 2;
         //Point center = new Point(motionRegion.X + (motionRegion.Width >> 1), motionRegion.Y + (motionRegion.Height >> 1));

         //CircleF circle = new CircleF(
         //   center,
         //   circleRadius);

         //int xDirection = (int)(Math.Cos(angle * (Math.PI / 180.0)) * circleRadius);
         //int yDirection = (int)(Math.Sin(angle * (Math.PI / 180.0)) * circleRadius);
         //Point pointOnCircle = new Point(
         //    center.X + xDirection,
         //    center.Y - yDirection);
         //LineSegment2D line = new LineSegment2D(center, pointOnCircle);
         //CvInvoke.Circle(image, Point.Round(circle.Center), (int)circle.Radius, color.MCvScalar);
         //CvInvoke.Line(image, line.P1, line.P2, color.MCvScalar);

      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {

         if (disposing && (components != null))
         {
            components.Dispose();
         }

         base.Dispose(disposing);
      }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _capture.Stop();
        }

        private void pedestianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedestrianPanel.Visible = true;
            MotionPanel.Visible = false;
        }

        private void motionDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedestrianPanel.Visible = false;
            MotionPanel.Visible = true;
            MotionPanel.Location = new Point(12, 52);
        }

        private void bothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MotionPanel.Visible = true;
            PedestrianPanel.Visible = true;
        }

       

        private void FireKAM_Load(object sender, EventArgs e)
        {

        }

        private void ToPDF_Click(object sender, EventArgs e)
        {
            msmMain.Theme = MetroFramework.MetroThemeStyle.Dark;
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf", FileMode.Create));
            doc.Open();


            var chartimage = new MemoryStream();
            chart1.SaveImage(chartimage, ChartImageFormat.Png);
            iTextSharp.text.Image Chart_image = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
            doc.Add(Chart_image);

            doc.Close();
        }

        //timer for record data
        private void timer_Tick(object sender, EventArgs e)
        {
            if (checkTimer)
            {
                checkTimer = false;
            }
            else
            {
                checkTimer = true;
            }
        }

        private async void GenerateChartBtn_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            //DateTime min = new DateTime(2016, 03, 08);
            //DateTime max = new DateTime(2016, 12, 31);
            DateTime min = dateTimePicker1.Value.Date;
            DateTime max = dateTimePicker2.Value.Date;
            //yearly report
            if (ReportTypeCB.SelectedIndex == 3)
            {             
                for (int i = 0; i < (max.Year - min.Year) + 1; i++)
                {
                    DateTime firstDayOfYear = new DateTime(min.Year, 1, 1);
                    string result = await DataAccess.FindByTime(GetDateTimeString(firstDayOfYear.AddYears(i)), GetDateTimeString(min.AddYears(i + 1).AddMilliseconds(-1)));
                    if (result != "no result")
                    {
                        chart1.Series[0].Points.AddXY(i + 1, int.Parse(result));
                        chart1.Series[1].Points.AddXY(i + 1, int.Parse(result));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i + 1, 0);
                        chart1.Series[1].Points.AddXY(i + 1, 0);
                    }
                }
            }
            //monthly report
            else if(ReportTypeCB.SelectedIndex == 2)
            {              
                for(int i = 0; i < 12; i++)
                {
                    //-1 millisecond to get the last second of the year
                    DateTime firstDayOfYear = new DateTime(min.Year, 1, 1);
                    string result = await DataAccess.FindByTime(GetDateTimeString(firstDayOfYear.AddMonths(i)), GetDateTimeString(min.AddMonths(i+1).AddMilliseconds(-1)));
                    if (result != "no result")
                    {
                        chart1.Series[0].Points.AddXY(i + 1, int.Parse(result));
                        chart1.Series[1].Points.AddXY(i + 1, int.Parse(result));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i + 1, 0);
                        chart1.Series[1].Points.AddXY(i + 1, 0);
                    }
                }               
            }
            //daily report
            else if (ReportTypeCB.SelectedIndex == 1)
            {
                for (int i = 0; i < (max-min).TotalDays; i++)
                {
                    string result = await DataAccess.FindByTime(GetDateTimeString(min.AddDays(i)), GetDateTimeString(min.AddDays(i + 1)));
                    if (result != "no result")
                    {
                        chart1.Series[0].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), int.Parse(result));
                        chart1.Series[1].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), int.Parse(result));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), 0);
                        chart1.Series[1].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), 0);
                    }
                } 
            }
            //hourly report
            else if (ReportTypeCB.SelectedIndex == 0)
            {
                //db.record.find({ "Time":{$gt: "2016-03-08 00:00:00", $lt: "2016-03-08 01:00:00"} })
                //db.record.insert({ "Camera":"Camera1","Time":"2016-03-08 09:12:12","MotionCout":1234})
                for (int i = 0; i < 24; i++)
                {
                    string result = await DataAccess.FindByTime(GetDateTimeString(min.AddHours(i)), GetDateTimeString(min.AddHours(i + 1)));
                    if (result != "no result")
                    {
                        chart1.Series[0].Points.AddXY(i, int.Parse(result));
                        chart1.Series[1].Points.AddXY(i, int.Parse(result));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i, 0);
                        chart1.Series[1].Points.AddXY(i, 0);
                    }
                }
            }
        }
              
        //convert datetime to 0000-00-00 format
        private string GetDateTimeString(DateTime date)
        {
            string result;
            result = date.Year + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00") + " " + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
            return result;
        }

        private void ReportTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ReportTypeCB.SelectedIndex == 0)
            {
                label8.Visible = false;
                dateTimePicker2.Visible = false;
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            }
            else if(ReportTypeCB.SelectedIndex == 1)
            {
                label8.Visible = true;
                dateTimePicker2.Visible = true;
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
                dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            }
            else if(ReportTypeCB.SelectedIndex == 2)
            {
                label8.Visible = false;
                dateTimePicker2.Visible = false;
                dateTimePicker1.CustomFormat = "yyyy";
            }
            else if(ReportTypeCB.SelectedIndex == 3)
            {
                label8.Visible = true;
                dateTimePicker2.Visible = true;
                dateTimePicker1.CustomFormat = "yyyy";
            }
        }

        public Bitmap[] splitBitmap()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            System.Drawing.Bitmap original;
            System.Drawing.Bitmap temp;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                original = new Bitmap(openDialog.FileName);
            }
            else
            {
                original = new Bitmap("FireKamlogo2.ico");
            }
            System.Drawing.Rectangle image1Rect = new System.Drawing.Rectangle(0,0, original.Width/3,original.Height/3);
            System.Drawing.Rectangle image2Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image3Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image4Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image5Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image6Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image7Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image8Rect = new System.Drawing.Rectangle();
            System.Drawing.Rectangle image9Rect = new System.Drawing.Rectangle();

            Bitmap[] imgs = new Bitmap[9];
            imgs[0] = original.Clone(image1Rect, original.PixelFormat);
            //assign imgs[0] to buttom background
            return imgs;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            splitBitmap();
        }
    }
}
