//----------------------------------------------------------------------------
//  Copyright (C) 2004-2015 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------
// https://www.youtube.com/watch?v=mM4fLnRDVto
// Install-Package MetroFramework

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;
using System.Diagnostics;
using ZedGraph;
using Emgu.CV.GPU;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Windows.Forms.DataVisualization.Charting;
using MetroFramework.Forms;
using MongoDB.Bson;
using System.Timers;

namespace MotionDetection
{

   public partial class FireKAM : MetroForm
   {
        private Capture _capture;
        private MotionHistory _motionHistory;
        private BackgroundSubtractor _forgroundDetector;
        private bool checkTimer;
        Panel[] Panels = new Panel[256];
        private int k = 0;
        private int panelNum = 30;

      public FireKAM()
      {
            InitializeComponent();
            checkTimer = true;
            timer.Start();
            this.StyleManager = msmMain;
            //this.chart1.Series["Camera"].Points.AddXY("Max", 200);
            //this.chart1.Series["Camera"].Points.AddXY("Max", 33);
            //this.chart1.Series["Camera"].Points.AddXY("Max", 33);
            chart1.Series["Camera"].XValueType = ChartValueType.DateTime;
            System.DateTime x = DateTime.Now;
            chart1.Series[0].Points.AddXY(x.ToOADate(),34);

            
            
            

            //try to create the capture
            if (_capture == null)
         {
            try
            {
                //"..\\..\\videoplayback480p.mp4"
                _capture = new Capture("..\\..\\videoplayback480p.mp4");
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

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCount;
                _motionHistory.MotionInfo(_forgroundMask, comp, out angle, out motionPixelCount);

                //reject the area that contains too few motion
                if (motionPixelCount < area * 0.05) continue;

                //Draw each individual motion in red
                DrawMotion(motionImage, comp, angle, new Bgr(Color.Red));
                motionCount++;
            }

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

            //test string
            //string a = await DataAccess.FindByTime("2016-02-22 03:00:00", "2016-02-28 02:00:00");

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

        private void FireKAM_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 120; i++)
            {
                if (i == 6 || i == 12 || i == 18 || i == 24)
                {
                    k++;
                }
                
                Panels[i] = new Panel();

                Panels[i].BackColor = Color.Transparent;
                Panels[i].Location = new Point(0 + (115 * (i - (k * 6))), 3 + (100 * k));

                Panels[i].Click += new EventHandler(paneli_Click);

                Panels[i].Size = new Size(115, 100);
                //Panels[i].Size = new Size(capturedImageBox.Width / 6, capturedImageBox.Height/5);
                Panels[i].Visible = false;

                Panels[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                capturedImageBox.Controls.Add(Panels[i]);

            }
            
            metroComboBox2.Items.Insert(0,"160");
            metroComboBox2.Items.Add("200");
            metroComboBox2.Items.Add("240");
            metroComboBox2.Items.Add("255");
            metroComboBox2.SelectedIndex = 0;
            
            metroComboBox1.Items.Insert(0, "Black");
            metroComboBox1.Items.Add("Red");
            metroComboBox1.Items.Add("Blue");
            metroComboBox1.SelectedIndex = 0;

            metroComboBox3.Items.Insert(0,"30");
            metroComboBox3.Items.Add("120");
            metroComboBox3.SelectedIndex = 0;

            metroComboBox2.Enabled = false;
            metroComboBox1.Enabled = false;
        }  

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                Panels[i].Visible ^= true;
            }
            metroComboBox2.Enabled ^= true;
            metroComboBox1.Enabled ^= true;
        }

        private void paneli_Click(object sender, EventArgs e)
        {
            Color customColor = Color.Black;

            if (metroComboBox1.Text == "Black")
            {
                customColor = Color.Black;
            }
            else if (metroComboBox1.Text == "Red")
            {
                customColor = Color.DarkRed;
            }
            else if (metroComboBox1.Text == "Blue")
            {
                customColor = Color.DarkBlue;
            }

            for (int i = 0; i < panelNum; i++)
            if (sender == Panels[i])
            {
                if (Panels[i].BackColor == Color.FromArgb(Convert.ToInt32(metroComboBox2.Text), customColor))
                {
                    Panels[i].BackColor = Color.Transparent;
                }
                else
                {
                    Panels[i].BackColor = Color.FromArgb(Convert.ToInt32(metroComboBox2.Text), customColor);
                }
            }
            
        }

       

        
    }
}
