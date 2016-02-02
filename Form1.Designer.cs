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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FireKAM));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dashboardBTN = new MetroFramework.Controls.MetroButton();
            this.devicesBTN = new MetroFramework.Controls.MetroButton();
            this.settingsBTN = new MetroFramework.Controls.MetroButton();
            this.supportBTN = new MetroFramework.Controls.MetroButton();
            this.msmMain = new MetroFramework.Components.MetroStyleManager(this.components);
            this.Page3 = new MetroFramework.Controls.MetroTabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ToPDF = new MetroFramework.Controls.MetroButton();
            this.Page2 = new MetroFramework.Controls.MetroTabPage();
            this.MotionPanel = new System.Windows.Forms.Panel();
            this.motionImageBox = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Page1 = new MetroFramework.Controls.MetroTabPage();
            this.PedestrianPanel = new System.Windows.Forms.Panel();
            this.capturedImageBox = new Emgu.CV.UI.ImageBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Home = new MetroFramework.Controls.MetroTabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
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
            this.metroTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.Page3.Controls.Add(this.chart2);
            this.Page3.Controls.Add(this.ToPDF);
            this.Page3.Controls.Add(this.chart1);
            this.Page3.HorizontalScrollbarBarColor = true;
            this.Page3.Location = new System.Drawing.Point(4, 35);
            this.Page3.Name = "Page3";
            this.Page3.Size = new System.Drawing.Size(856, 499);
            this.Page3.TabIndex = 2;
            this.Page3.Text = "Graphs";
            this.Page3.VerticalScrollbarBarColor = true;
            // 
            // chart1
            // 
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.chart1.Legends.Add(legend7);
            this.chart1.Location = new System.Drawing.Point(3, 17);
            this.chart1.Name = "chart1";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // ToPDF
            // 
            this.ToPDF.Location = new System.Drawing.Point(670, 17);
            this.ToPDF.Name = "ToPDF";
            this.ToPDF.Size = new System.Drawing.Size(149, 47);
            this.ToPDF.TabIndex = 16;
            this.ToPDF.Text = "Save as PDF";
            this.ToPDF.Click += new System.EventHandler(this.ToPDF_Click);
            // 
            // Page2
            // 
            this.Page2.Controls.Add(this.MotionPanel);
            this.Page2.HorizontalScrollbarBarColor = true;
            this.Page2.Location = new System.Drawing.Point(4, 35);
            this.Page2.Name = "Page2";
            this.Page2.Size = new System.Drawing.Size(856, 499);
            this.Page2.TabIndex = 1;
            this.Page2.Text = "Motion View";
            this.Page2.VerticalScrollbarBarColor = true;
            // 
            // MotionPanel
            // 
            this.MotionPanel.Controls.Add(this.label1);
            this.MotionPanel.Controls.Add(this.motionImageBox);
            this.MotionPanel.Location = new System.Drawing.Point(0, 35);
            this.MotionPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MotionPanel.Name = "MotionPanel";
            this.MotionPanel.Size = new System.Drawing.Size(451, 420);
            this.MotionPanel.TabIndex = 8;
            // 
            // motionImageBox
            // 
            this.motionImageBox.Location = new System.Drawing.Point(3, 32);
            this.motionImageBox.Name = "motionImageBox";
            this.motionImageBox.Size = new System.Drawing.Size(445, 385);
            this.motionImageBox.TabIndex = 2;
            this.motionImageBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Motion Detection";
            // 
            // Page1
            // 
            this.Page1.Controls.Add(this.label3);
            this.Page1.Controls.Add(this.PedestrianPanel);
            this.Page1.HorizontalScrollbarBarColor = true;
            this.Page1.Location = new System.Drawing.Point(4, 35);
            this.Page1.Name = "Page1";
            this.Page1.Size = new System.Drawing.Size(856, 499);
            this.Page1.TabIndex = 0;
            this.Page1.Text = "Pedestrian View";
            this.Page1.VerticalScrollbarBarColor = true;
            // 
            // PedestrianPanel
            // 
            this.PedestrianPanel.Controls.Add(this.label4);
            this.PedestrianPanel.Controls.Add(this.capturedImageBox);
            this.PedestrianPanel.Location = new System.Drawing.Point(0, 35);
            this.PedestrianPanel.Margin = new System.Windows.Forms.Padding(2);
            this.PedestrianPanel.Name = "PedestrianPanel";
            this.PedestrianPanel.Size = new System.Drawing.Size(451, 420);
            this.PedestrianPanel.TabIndex = 7;
            // 
            // capturedImageBox
            // 
            this.capturedImageBox.Location = new System.Drawing.Point(3, 32);
            this.capturedImageBox.Name = "capturedImageBox";
            this.capturedImageBox.Size = new System.Drawing.Size(445, 385);
            this.capturedImageBox.TabIndex = 0;
            this.capturedImageBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pedestrian Detection";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 457);
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
            this.Home.Size = new System.Drawing.Size(856, 499);
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
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.Home);
            this.metroTabControl1.Controls.Add(this.Page1);
            this.metroTabControl1.Controls.Add(this.Page2);
            this.metroTabControl1.Controls.Add(this.Page3);
            this.metroTabControl1.Location = new System.Drawing.Point(216, 30);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 2;
            this.metroTabControl1.Size = new System.Drawing.Size(864, 538);
            this.metroTabControl1.TabIndex = 11;
            // 
            // chart2
            // 
            chartArea8.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.chart2.Legends.Add(legend8);
            this.chart2.Location = new System.Drawing.Point(309, 17);
            this.chart2.Name = "chart2";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.chart2.Series.Add(series8);
            this.chart2.Size = new System.Drawing.Size(300, 300);
            this.chart2.TabIndex = 17;
            this.chart2.Text = "chart2";
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
            // FireKAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1102, 550);
            this.Controls.Add(this.settingsBTN);
            this.Controls.Add(this.devicesBTN);
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
            this.metroTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroButton dashboardBTN;
        private MetroFramework.Controls.MetroButton devicesBTN;
        private MetroFramework.Controls.MetroButton settingsBTN;
        private MetroFramework.Controls.MetroButton supportBTN;
        private MetroFramework.Components.MetroStyleManager msmMain;
        private MetroFramework.Controls.MetroTabPage Page3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
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
    }
}

