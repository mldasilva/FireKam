namespace MotionDetection
{
    partial class FireKAM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FireKAM));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dashboardBTN = new MetroFramework.Controls.MetroButton();
            this.devicesBTN = new MetroFramework.Controls.MetroButton();
            this.settingsBTN = new MetroFramework.Controls.MetroButton();
            this.supportBTN = new MetroFramework.Controls.MetroButton();
            this.msmMain = new MetroFramework.Components.MetroStyleManager(this.components);
            this.Page3 = new MetroFramework.Controls.MetroTabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.MotionCountMaxTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TimeMaxTb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TimeMinTb = new System.Windows.Forms.TextBox();
            this.MotionCountMinTb = new System.Windows.Forms.TextBox();
            this.CareraTb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GenerateChartBtn = new MetroFramework.Controls.MetroButton();
            this.ToPDF = new MetroFramework.Controls.MetroButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Page2 = new MetroFramework.Controls.MetroTabPage();
            this.MotionPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.motionImageBox = new Emgu.CV.UI.ImageBox();
            this.Page1 = new MetroFramework.Controls.MetroTabPage();
            this.metroToggle1 = new MetroFramework.Controls.MetroToggle();
            this.PedestrianPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.capturedImageBox = new Emgu.CV.UI.ImageBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Home = new MetroFramework.Controls.MetroTabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.metroComboBox2 = new MetroFramework.Controls.MetroComboBox();
            this.gridLabel = new MetroFramework.Controls.MetroLabel();
            this.colorLabel = new MetroFramework.Controls.MetroLabel();
            this.opacityLabel = new MetroFramework.Controls.MetroLabel();
            this.metroComboBox3 = new MetroFramework.Controls.MetroComboBox();
            this.panelLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.msmMain)).BeginInit();
            this.Page3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.Page2.SuspendLayout();
            this.MotionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionImageBox)).BeginInit();
            this.Page1.SuspendLayout();
            this.PedestrianPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).BeginInit();
            this.Home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashboardBTN
            // 
            this.dashboardBTN.Location = new System.Drawing.Point(23, 63);
            this.dashboardBTN.Name = "dashboardBTN";
            this.dashboardBTN.Size = new System.Drawing.Size(175, 66);
            this.dashboardBTN.TabIndex = 12;
            this.dashboardBTN.Text = "Dashboard";
            // 
            // devicesBTN
            // 
            this.devicesBTN.Location = new System.Drawing.Point(23, 135);
            this.devicesBTN.Name = "devicesBTN";
            this.devicesBTN.Size = new System.Drawing.Size(175, 66);
            this.devicesBTN.TabIndex = 13;
            this.devicesBTN.Text = "Devices";
            // 
            // settingsBTN
            // 
            this.settingsBTN.Location = new System.Drawing.Point(23, 207);
            this.settingsBTN.Name = "settingsBTN";
            this.settingsBTN.Size = new System.Drawing.Size(175, 66);
            this.settingsBTN.TabIndex = 14;
            this.settingsBTN.Text = "Settings";
            // 
            // supportBTN
            // 
            this.supportBTN.Location = new System.Drawing.Point(23, 375);
            this.supportBTN.Name = "supportBTN";
            this.supportBTN.Size = new System.Drawing.Size(175, 66);
            this.supportBTN.TabIndex = 15;
            this.supportBTN.Text = "Support";
            // 
            // msmMain
            // 
            this.msmMain.Owner = null;
            this.msmMain.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Page3
            // 
            this.Page3.Controls.Add(this.label10);
            this.Page3.Controls.Add(this.MotionCountMaxTb);
            this.Page3.Controls.Add(this.label9);
            this.Page3.Controls.Add(this.label8);
            this.Page3.Controls.Add(this.TimeMaxTb);
            this.Page3.Controls.Add(this.label7);
            this.Page3.Controls.Add(this.TimeMinTb);
            this.Page3.Controls.Add(this.MotionCountMinTb);
            this.Page3.Controls.Add(this.CareraTb);
            this.Page3.Controls.Add(this.label6);
            this.Page3.Controls.Add(this.label5);
            this.Page3.Controls.Add(this.label2);
            this.Page3.Controls.Add(this.GenerateChartBtn);
            this.Page3.Controls.Add(this.ToPDF);
            this.Page3.Controls.Add(this.chart1);
            this.Page3.HorizontalScrollbarBarColor = true;
            this.Page3.Location = new System.Drawing.Point(4, 35);
            this.Page3.Name = "Page3";
            this.Page3.Size = new System.Drawing.Size(915, 570);
            this.Page3.TabIndex = 2;
            this.Page3.Text = "Graphs";
            this.Page3.VerticalScrollbarBarColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(194, 124);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Max";
            // 
            // MotionCountMaxTb
            // 
            this.MotionCountMaxTb.Location = new System.Drawing.Point(182, 138);
            this.MotionCountMaxTb.Margin = new System.Windows.Forms.Padding(2);
            this.MotionCountMaxTb.Name = "MotionCountMaxTb";
            this.MotionCountMaxTb.Size = new System.Drawing.Size(52, 20);
            this.MotionCountMaxTb.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 125);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Max";
            // 
            // TimeMaxTb
            // 
            this.TimeMaxTb.Location = new System.Drawing.Point(182, 91);
            this.TimeMaxTb.Margin = new System.Windows.Forms.Padding(2);
            this.TimeMaxTb.Name = "TimeMaxTb";
            this.TimeMaxTb.Size = new System.Drawing.Size(52, 20);
            this.TimeMaxTb.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(123, 76);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Min";
            // 
            // TimeMinTb
            // 
            this.TimeMinTb.Location = new System.Drawing.Point(110, 91);
            this.TimeMinTb.Margin = new System.Windows.Forms.Padding(2);
            this.TimeMinTb.Name = "TimeMinTb";
            this.TimeMinTb.Size = new System.Drawing.Size(52, 20);
            this.TimeMinTb.TabIndex = 24;
            // 
            // MotionCountMinTb
            // 
            this.MotionCountMinTb.Location = new System.Drawing.Point(110, 140);
            this.MotionCountMinTb.Margin = new System.Windows.Forms.Padding(2);
            this.MotionCountMinTb.Name = "MotionCountMinTb";
            this.MotionCountMinTb.Size = new System.Drawing.Size(52, 20);
            this.MotionCountMinTb.TabIndex = 23;
            // 
            // CareraTb
            // 
            this.CareraTb.Location = new System.Drawing.Point(110, 45);
            this.CareraTb.Margin = new System.Windows.Forms.Padding(2);
            this.CareraTb.Name = "CareraTb";
            this.CareraTb.Size = new System.Drawing.Size(52, 20);
            this.CareraTb.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 141);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Motion Count";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 94);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Camera";
            // 
            // GenerateChartBtn
            // 
            this.GenerateChartBtn.Location = new System.Drawing.Point(75, 217);
            this.GenerateChartBtn.Name = "GenerateChartBtn";
            this.GenerateChartBtn.Size = new System.Drawing.Size(85, 33);
            this.GenerateChartBtn.TabIndex = 18;
            this.GenerateChartBtn.Text = "Generate";
            // 
            // ToPDF
            // 
            this.ToPDF.Location = new System.Drawing.Point(805, 411);
            this.ToPDF.Name = "ToPDF";
            this.ToPDF.Size = new System.Drawing.Size(85, 33);
            this.ToPDF.TabIndex = 16;
            this.ToPDF.Text = "Save as PDF";
            this.ToPDF.Click += new System.EventHandler(this.ToPDF_Click);
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(298, 11);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Camera";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(601, 300);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // Page2
            // 
            this.Page2.Controls.Add(this.MotionPanel);
            this.Page2.HorizontalScrollbarBarColor = true;
            this.Page2.Location = new System.Drawing.Point(4, 35);
            this.Page2.Name = "Page2";
            this.Page2.Size = new System.Drawing.Size(915, 570);
            this.Page2.TabIndex = 1;
            this.Page2.Text = "Motion View";
            this.Page2.VerticalScrollbarBarColor = true;
            // 
            // MotionPanel
            // 
            this.MotionPanel.Controls.Add(this.label1);
            this.MotionPanel.Controls.Add(this.motionImageBox);
            this.MotionPanel.Location = new System.Drawing.Point(35, 26);
            this.MotionPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MotionPanel.Name = "MotionPanel";
            this.MotionPanel.Size = new System.Drawing.Size(712, 527);
            this.MotionPanel.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Motion Detection";
            // 
            // motionImageBox
            // 
            this.motionImageBox.Location = new System.Drawing.Point(12, 26);
            this.motionImageBox.Name = "motionImageBox";
            this.motionImageBox.Size = new System.Drawing.Size(690, 487);
            this.motionImageBox.TabIndex = 2;
            this.motionImageBox.TabStop = false;
            // 
            // Page1
            // 
            this.Page1.Controls.Add(this.panelLabel);
            this.Page1.Controls.Add(this.metroComboBox3);
            this.Page1.Controls.Add(this.opacityLabel);
            this.Page1.Controls.Add(this.colorLabel);
            this.Page1.Controls.Add(this.gridLabel);
            this.Page1.Controls.Add(this.metroComboBox2);
            this.Page1.Controls.Add(this.metroComboBox1);
            this.Page1.Controls.Add(this.metroToggle1);
            this.Page1.Controls.Add(this.PedestrianPanel);
            this.Page1.HorizontalScrollbarBarColor = true;
            this.Page1.Location = new System.Drawing.Point(4, 35);
            this.Page1.Name = "Page1";
            this.Page1.Size = new System.Drawing.Size(915, 570);
            this.Page1.TabIndex = 0;
            this.Page1.Text = "Pedestrian View";
            this.Page1.VerticalScrollbarBarColor = true;
            // 
            // metroToggle1
            // 
            this.metroToggle1.AutoSize = true;
            this.metroToggle1.Location = new System.Drawing.Point(832, 52);
            this.metroToggle1.Name = "metroToggle1";
            this.metroToggle1.Size = new System.Drawing.Size(80, 17);
            this.metroToggle1.TabIndex = 8;
            this.metroToggle1.Text = "Off";
            this.metroToggle1.UseVisualStyleBackColor = true;
            this.metroToggle1.CheckedChanged += new System.EventHandler(this.metroToggle1_CheckedChanged);
            // 
            // PedestrianPanel
            // 
            this.PedestrianPanel.Controls.Add(this.label4);
            this.PedestrianPanel.Controls.Add(this.capturedImageBox);
            this.PedestrianPanel.Location = new System.Drawing.Point(35, 26);
            this.PedestrianPanel.Margin = new System.Windows.Forms.Padding(2);
            this.PedestrianPanel.Name = "PedestrianPanel";
            this.PedestrianPanel.Size = new System.Drawing.Size(712, 527);
            this.PedestrianPanel.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pedestrian Detection";
            // 
            // capturedImageBox
            // 
            this.capturedImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.capturedImageBox.Location = new System.Drawing.Point(12, 26);
            this.capturedImageBox.Name = "capturedImageBox";
            this.capturedImageBox.Size = new System.Drawing.Size(690, 487);
            this.capturedImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.capturedImageBox.TabIndex = 0;
            this.capturedImageBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 660);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // Home
            // 
            this.Home.Controls.Add(this.pictureBox1);
            this.Home.Controls.Add(this.pictureBox2);
            this.Home.HorizontalScrollbarBarColor = true;
            this.Home.Location = new System.Drawing.Point(4, 35);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(915, 570);
            this.Home.TabIndex = 3;
            this.Home.Text = "Home";
            this.Home.VerticalScrollbarBarColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(676, 345);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 117);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(816, 343);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.Home);
            this.metroTabControl1.Controls.Add(this.Page1);
            this.metroTabControl1.Controls.Add(this.Page2);
            this.metroTabControl1.Controls.Add(this.Page3);
            this.metroTabControl1.Location = new System.Drawing.Point(216, 30);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 2;
            this.metroTabControl1.Size = new System.Drawing.Size(923, 609);
            this.metroTabControl1.TabIndex = 11;
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(832, 107);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(80, 29);
            this.metroComboBox1.TabIndex = 9;
            // 
            // metroComboBox2
            // 
            this.metroComboBox2.FormattingEnabled = true;
            this.metroComboBox2.ItemHeight = 23;
            this.metroComboBox2.Location = new System.Drawing.Point(832, 179);
            this.metroComboBox2.Name = "metroComboBox2";
            this.metroComboBox2.Size = new System.Drawing.Size(80, 29);
            this.metroComboBox2.TabIndex = 10;
            // 
            // gridLabel
            // 
            this.gridLabel.AutoSize = true;
            this.gridLabel.Location = new System.Drawing.Point(760, 52);
            this.gridLabel.Name = "gridLabel";
            this.gridLabel.Size = new System.Drawing.Size(62, 19);
            this.gridLabel.TabIndex = 11;
            this.gridLabel.Text = "Grid Tool";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(760, 117);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(49, 19);
            this.colorLabel.TabIndex = 12;
            this.colorLabel.Text = "Colour";
            // 
            // opacityLabel
            // 
            this.opacityLabel.AutoSize = true;
            this.opacityLabel.Location = new System.Drawing.Point(760, 189);
            this.opacityLabel.Name = "opacityLabel";
            this.opacityLabel.Size = new System.Drawing.Size(54, 19);
            this.opacityLabel.TabIndex = 13;
            this.opacityLabel.Text = "Opacity";
            // 
            // metroComboBox3
            // 
            this.metroComboBox3.FormattingEnabled = true;
            this.metroComboBox3.ItemHeight = 23;
            this.metroComboBox3.Location = new System.Drawing.Point(832, 255);
            this.metroComboBox3.Name = "metroComboBox3";
            this.metroComboBox3.Size = new System.Drawing.Size(80, 29);
            this.metroComboBox3.TabIndex = 16;
            // 
            // panelLabel
            // 
            this.panelLabel.AutoSize = true;
            this.panelLabel.Location = new System.Drawing.Point(760, 265);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(48, 19);
            this.panelLabel.TabIndex = 16;
            this.panelLabel.Text = "Panel#";
            // 
            // FireKAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1183, 700);
            this.Controls.Add(this.settingsBTN);
            this.Controls.Add(this.devicesBTN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dashboardBTN);
            this.Controls.Add(this.supportBTN);
            this.Controls.Add(this.metroTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FireKAM";
            this.Text = "FireKAM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FireKAM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.msmMain)).EndInit();
            this.Page3.ResumeLayout(false);
            this.Page3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.Page2.ResumeLayout(false);
            this.MotionPanel.ResumeLayout(false);
            this.MotionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionImageBox)).EndInit();
            this.Page1.ResumeLayout(false);
            this.Page1.PerformLayout();
            this.PedestrianPanel.ResumeLayout(false);
            this.PedestrianPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).EndInit();
            this.Home.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroButton dashboardBTN;
        private MetroFramework.Controls.MetroButton devicesBTN;
        private MetroFramework.Controls.MetroButton settingsBTN;
        private MetroFramework.Controls.MetroButton supportBTN;
        private MetroFramework.Components.MetroStyleManager msmMain;
        private MetroFramework.Controls.MetroTabPage Page3;
        private MetroFramework.Controls.MetroButton ToPDF;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private MetroFramework.Controls.MetroTabPage Page2;
        private System.Windows.Forms.Panel MotionPanel;
        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox motionImageBox;
        private MetroFramework.Controls.MetroTabPage Page1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PedestrianPanel;
        private System.Windows.Forms.Label label4;
        private Emgu.CV.UI.ImageBox capturedImageBox;
        private MetroFramework.Controls.MetroTabPage Home;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroButton GenerateChartBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TimeMaxTb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TimeMinTb;
        private System.Windows.Forms.TextBox MotionCountMinTb;
        private System.Windows.Forms.TextBox CareraTb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox MotionCountMaxTb;
        private System.Windows.Forms.Timer timer;
        private MetroFramework.Controls.MetroToggle metroToggle1;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroComboBox metroComboBox2;
        private MetroFramework.Controls.MetroLabel opacityLabel;
        private MetroFramework.Controls.MetroLabel colorLabel;
        private MetroFramework.Controls.MetroLabel gridLabel;
        private MetroFramework.Controls.MetroLabel panelLabel;
        private MetroFramework.Controls.MetroComboBox metroComboBox3;
    }
}

