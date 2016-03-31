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
using System.Collections.Generic;

namespace MotionDetection
{
   public partial class FireKAM : MetroForm
   {
        private Capture _capture;
        private MotionHistory _motionHistory;
        private BackgroundSubtractor _forgroundDetector;
        private bool checkTimer;
        Heatmap pHeatmap;
        Panel[] Panels = new Panel[36];
        private int panelNum = 36;
        //"..\\..\\videoplayback480p.mp4"
        private string videoSource = "..\\..\\videoplayback720p.mp4";

        public FireKAM()
        {
            InitializeComponent();
            checkTimer = true;
            timer.Start();
            this.StyleManager = msmMain;          
            chart1.Series["Camera"].XValueType = ChartValueType.Auto;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            pHeatmap = new Heatmap();
            pictureBox3.Parent = motionImageBox;         
        }

        private void FireKAM_Load(object sender, EventArgs e)
        {
            GeneratePanel();

            ReportTypeCB.SelectedIndex = 0;
            SourceComboBox.SelectedIndex = 0;
            ReportSourceDropdown.SelectedIndex = 0;

            metroComboBox2.Items.Insert(0, "160");
            metroComboBox2.Items.Add("200");
            metroComboBox2.Items.Add("240");
            metroComboBox2.Items.Add("255");
            metroComboBox2.SelectedIndex = 0;

            metroComboBox1.Items.Insert(0, "Black");
            metroComboBox1.Items.Add("Red");
            metroComboBox1.Items.Add("Blue");
            metroComboBox1.SelectedIndex = 0;

            metroComboBox3.Items.Insert(0, "30");
            metroComboBox3.Items.Add("120");
            metroComboBox3.SelectedIndex = 0;

            metroComboBox2.Enabled = false;
            metroComboBox1.Enabled = false;
        }

        private void recordToggle_CheckedChanged(object sender, EventArgs e)
        {
            //try to create the capture
            if (recordToggle.Checked)
            {
                if (_capture == null)
                {
                    StartCapture();
                }
                else
                {
                    _capture.Start();
                }
            }
            else
            {
                _capture.Pause();
            }
        }

        private void StartCapture()
        {
            if (_capture == null)
            {
                try
                {
                    if (SourceComboBox.SelectedIndex == 0)
                    {                       
                        _capture = new Capture();
                    }
                    else if (SourceComboBox.SelectedIndex == 1)
                    {
                        _capture = new Capture(videoSource);
                    }
                }
                catch (NullReferenceException excpt)
                {   //show errors if there is any
                    MessageBox.Show(excpt.Message);
                }
            }

            if (_capture != null) //if camera capture has been successfully created
            {
                _motionHistory = new MotionHistory(
                    1.0, //in second, the duration of motion history you wants to keep 1.0
                    0.05, //in second, maxDelta for cvCalcMotionGradient 0.05
                    0.5); //in second, minDelta for cvCalcMotionGradient 0.5

                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
            }
        }

        private Mat _segMask = new Mat();
        private Mat _forgroundMask = new Mat();
        Heatmap myHeatMap = new Heatmap();

        private async void ProcessFrame(object sender, EventArgs e)
        {
            List<BsonDocument> tempList = new List<BsonDocument>();
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
            double minArea = 5000;

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

                if (!CheckArea(comp)) continue;

                //find center point
                Point center = new Point(comp.X + (comp.Width >> 1), comp.Y + (comp.Height >> 1));

                //insert to temp motion list
                var document = new BsonDocument
                {
                    {"Source", Path.GetFileName(videoSource)},
                    {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    {"Area", GetAreaCode(center).ToString()},
                    {"AreaX", center.X.ToString()},
                    {"AreaY", center.Y.ToString()},
                    {"MotionCout", 1 }
                };
                tempList.Add(document);

                //create heatmap
                myHeatMap.heatPoints.Add(new HeatPoint(center.X, center.Y, 16));

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCount;
                _motionHistory.MotionInfo(_forgroundMask, comp, out angle, out motionPixelCount);

                //reject the area that contains too few motion 0.05
                if (motionPixelCount < area * 0.1) continue;

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
            //bool tryUseCuda = true;
            //bool tryuseOpenCL = false;
            //long processingTime;
            //System.Drawing.Rectangle[] pedestrianRestult = FindPedestrian.Find(image, tryUseCuda, tryuseOpenCL, out processingTime);
            //foreach (System.Drawing.Rectangle rect in pedestrianRestult)
            //{
            //    CvInvoke.Rectangle(image, rect, new Bgr(Color.Gold).MCvScalar);
            //}

            capturedImageBox.Image = image;
            //forgroundImageBox.Image = _forgroundMask;

            //Display the amount of motions found on the current image
            UpdateText(String.Format("Total Motions found: {0}; Motion Pixel count: {1}", motionCount, overallMotionPixelCount));

            //write into db
            if (checkTimer)
            {
                foreach(var doc in tempList)
                {
                    await DataAccess.Insert(doc);
                }             
            }

            motionCount = 0;
            //Display the image of the motion
            motionImageBox.Image = motionImage;         
      }

        private int GetAreaCode(Point rect)
        {
            //area code: 1 2 3 4 5 6 || 7 8 9 10 11 12 ||...||...||..36
            //y*6+x = area code
            int x = rect.X / (capturedImageBox.Width / 6);
            int y = rect.Y / (capturedImageBox.Height / 6);
            int area = y * 6 + x;

            return area;
        }

        private bool CheckArea(System.Drawing.Rectangle rect)
        {
            //area code: 1 2 3 4 5 6 || 7 8 9 10 11 12 ||...||...||..36
            //y*6+x = area code
            int x = rect.X / (capturedImageBox.Width / 6);
            int y = rect.Y / (capturedImageBox.Height / 6);
            int area = y * 6 + x;

            for (int i = 0; i < panelNum; i++)
            {
                if (Panels[i].BackColor == Color.Transparent && area == i)
                {
                    return true;
                }
            }
            return false;
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
            if (_capture != null)
            {
                _capture.Stop();
            }
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
            {
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

        private void GeneratePanel()
        {
            Array.Clear(Panels, 0, Panels.Length);
            int k = 0;
            int j = 0;

            for (int i = 0; i < 36; i++)
            {
                if (i == 6 || i == 12 || i == 18 || i == 24 || i == 30 || i == 36)
                {
                    k++;
                    j = 0;
                }

                Panels[i] = new Panel();

                Panels[i].BackColor = Color.Transparent;
                //Panels[i].Location = new Point(0 + (115 * (i - (k * 6))), 3 + (100 * k));
                Panels[i].Location = new Point(capturedImageBox.Location.X + (capturedImageBox.Width / 6 * j),
                                               capturedImageBox.Location.Y + (capturedImageBox.Height / 6 * k));

                Panels[i].Click += new EventHandler(paneli_Click);

                Panels[i].Size = new Size(capturedImageBox.Width / 6, capturedImageBox.Height / 6);
                //Panels[i].Size = new Size(capturedImageBox.Width / 6, capturedImageBox.Height/5);
                Panels[i].Visible = false;

                Panels[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                capturedImageBox.Controls.Add(Panels[i]);
                j++;
            }
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

            if (ReportTypeCB.SelectedIndex == 4)
            {
                ChartTypeSwitch("pie");
                for (int i = 0; i < 36; i++)
                {
                    List<BsonDocument> result = await DataAccess.FindByTimeAndArea(GetDateTimeString(min), GetDateTimeString(max.AddDays(1).AddMilliseconds(-1)), i+1);
                    if (result != null)
                    {
                        chart1.Series[0].Points.AddXY(i, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i, 0);
                    }
                }
            }
            //yearly report
            else if (ReportTypeCB.SelectedIndex == 3)
            {
                ChartTypeSwitch("column");
                for (int i = 0; i < (max.Year - min.Year) + 1; i++)
                {
                    DateTime firstDayOfYear = new DateTime(min.Year, 1, 1);
                    List<BsonDocument> result = await DataAccess.FindByTime(GetDateTimeString(firstDayOfYear.AddYears(i)), GetDateTimeString(min.AddYears(i + 1).AddMilliseconds(-1)), ReportSourceDropdown.Text);
                    if (result != null)
                    {
                        chart1.Series[0].Points.AddXY(i + 1, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                        chart1.Series[1].Points.AddXY(i + 1, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i + 1, 0);
                        chart1.Series[1].Points.AddXY(i + 1, 0);

                        chart1.Series[0].Points[i].Label = "#VAL";
                    }
                }
            }
            //monthly report
            else if(ReportTypeCB.SelectedIndex == 2)
            {
                ChartTypeSwitch("column");
                for (int i = 0; i < 12; i++)
                {
                    //-1 millisecond to get the last second of the year
                    DateTime firstDayOfYear = new DateTime(min.Year, 1, 1);
                    List<BsonDocument> result = await DataAccess.FindByTime(GetDateTimeString(firstDayOfYear.AddMonths(i)), GetDateTimeString(min.AddMonths(i+1).AddMilliseconds(-1)), ReportSourceDropdown.Text);
                    if (result != null)
                    {
                        chart1.Series[0].Points.AddXY(i + 1, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                        chart1.Series[1].Points.AddXY(i + 1, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i + 1, 0);
                        chart1.Series[1].Points.AddXY(i + 1, 0);

                        chart1.Series[0].Points[i].Label = "#VAL";
                    }
                }               
            }
            //daily report
            else if (ReportTypeCB.SelectedIndex == 1)
            {
                ChartTypeSwitch("column");
                for (int i = 0; i < (max-min).TotalDays; i++)
                {
                    List<BsonDocument> result = await DataAccess.FindByTime(GetDateTimeString(min.AddDays(i)), GetDateTimeString(min.AddDays(i + 1)), ReportSourceDropdown.Text);
                    if (result != null)
                    {
                        chart1.Series[0].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                        chart1.Series[1].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), 0);
                        chart1.Series[1].Points.AddXY(min.AddDays(i).ToString("dd/mm/yyyy"), 0);

                        chart1.Series[0].Points[i].Label = "#VAL";
                    }
                } 
            }
            //hourly report
            else if (ReportTypeCB.SelectedIndex == 0)
            {
                ChartTypeSwitch("column");
                for (int i = 0; i < 24; i++)
                {
                    List<BsonDocument> result = await DataAccess.FindByTime(GetDateTimeString(min.AddHours(i)), GetDateTimeString(min.AddHours(i + 1)), ReportSourceDropdown.Text);
                    if (result != null)
                    {
                        chart1.Series[0].Points.AddXY(i, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                        chart1.Series[1].Points.AddXY(i, int.Parse(result[0].GetElement("MotionCount").Value.ToString()));
                    }
                    else
                    {
                        chart1.Series[0].Points.AddXY(i, 0);
                        chart1.Series[1].Points.AddXY(i, 0);

                        chart1.Series[0].Points[i].Label = "#VAL";
                    }
                }
            }
        }
        
        private void ChartTypeSwitch(string chartType)
        {
            if(chartType == "pie")
            {
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Series[1].ChartType = SeriesChartType.Pie;
                chart1.Series[0]["PieLabelStyle"] = "Outside";
                chart1.Series[1]["PieLabelStyle"] = "Outside";
                // Set these other two properties so that you can see the connecting lines
                chart1.Series[0].BorderWidth = 1;
                chart1.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set the pie label as well as legend text to be displayed as percentage
                // The P2 indicates a precision of 2 decimals
                chart1.Series[0].Label = "#VALX (#PERCENT)";

                chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart1.ChartAreas[0].Area3DStyle.Inclination = 50;
            }
            else if(chartType == "column")
            {
                chart1.Series[0].ChartType = SeriesChartType.Column;
                chart1.Series[1].ChartType = SeriesChartType.Line;
                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                chart1.Series[0]["PieLabelStyle"] = "Inside";
                chart1.Series[1]["PieLabelStyle"] = "Inside";
                // Set the pie label as well as legend text to be displayed as percentage
                // The P2 indicates a precision of 2 decimals
                chart1.Series[0].Label = "#VAL (#PERCENT)";
                chart1.Series[0].SmartLabelStyle.IsMarkerOverlappingAllowed = false;
                chart1.Series[0].SmartLabelStyle.MovingDirection  = LabelAlignmentStyles.Right;
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
                dateTimePicker2.CustomFormat = "yyyy";
            }
            else if (ReportTypeCB.SelectedIndex == 4)
            {
                label8.Visible = true;
                dateTimePicker2.Visible = true;
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
                dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            }
        }

        private void metroToggle1_CheckedChanged_1(object sender, EventArgs e)
        {     
            if (metroToggle1.Checked == true)
            {
                for (int i = 0; i < 36; i++)
                {
                    Panels[i].Visible = true;
                }
                metroComboBox2.Enabled = true;
                metroComboBox1.Enabled = true;
            }
            else if(metroToggle1.Checked == false)
            {
                for (int i = 0; i < 36; i++)
                {
                    Panels[i].Visible = false;
                }
                metroComboBox2.Enabled = false;
                metroComboBox1.Enabled = false;
            }
        }

        private void SourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SourceComboBox.SelectedIndex == 0)
            {
                videoSource = "Camera";
                if (recordToggle.Checked == true)
                {
                    _capture.Dispose();
                    _capture = null;
                    StartCapture();
                }
                else
                {
                    if (_capture != null)
                    {
                        _capture.Dispose();
                        _capture = null;
                    }
                }
            }
            else if (SourceComboBox.SelectedIndex == 1)
            {
                OpenFileDialog choofdlog = new OpenFileDialog();
                choofdlog.Filter = "All Videos Files |*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                  " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = false;

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    videoSource = choofdlog.FileName;
                    if (recordToggle.Checked == true)
                    {                       
                        _capture.Dispose();
                        _capture = null;
                        StartCapture();
                    }
                    else
                    {
                        if (_capture != null)
                        {
                            _capture.Dispose();
                            _capture = null;
                        }
                    }
                }
                else
                {
                    SourceComboBox.SelectedIndex = 0;
                }
            }
        }

        private async void SourceDropdown_DropDown(object sender, EventArgs e)
        {
            ReportSourceDropdown.Items.Clear();
            ReportSourceDropdown.Items.Add("All");
            ReportSourceDropdown.SelectedIndex = 0;
            var distincts = await DataAccess.FindSourceDistinct();
            foreach(string distinct in distincts)
            {
                ReportSourceDropdown.Items.Add(distinct);
            }
        }
    }
}
