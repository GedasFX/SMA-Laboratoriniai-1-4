using System.Drawing;
using System.Windows.Forms;
namespace Pvz1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textOutputBox = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDivision = new System.Windows.Forms.Button();
            this.btnIterations = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStrings = new System.Windows.Forms.Button();
            this.radioF = new System.Windows.Forms.RadioButton();
            this.radioG = new System.Windows.Forms.RadioButton();
            this.btnScan = new System.Windows.Forms.Button();
            this.radioH = new System.Windows.Forms.RadioButton();
            this.btnGauss = new System.Windows.Forms.Button();
            this.btnOptimization = new System.Windows.Forms.Button();
            this.butBroiden = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // chartGraph
            // 
            chartArea1.Name = "ChartArea1";
            this.chartGraph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartGraph.Legends.Add(legend1);
            this.chartGraph.Location = new System.Drawing.Point(12, 12);
            this.chartGraph.Name = "chartGraph";
            this.chartGraph.Size = new System.Drawing.Size(669, 394);
            this.chartGraph.TabIndex = 0;
            this.chartGraph.Text = "chart1";
            // 
            // textOutputBox
            // 
            this.textOutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textOutputBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textOutputBox.Location = new System.Drawing.Point(12, 412);
            this.textOutputBox.Name = "textOutputBox";
            this.textOutputBox.Size = new System.Drawing.Size(669, 251);
            this.textOutputBox.TabIndex = 1;
            this.textOutputBox.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(773, 643);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Baigti";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDivision
            // 
            this.btnDivision.Location = new System.Drawing.Point(692, 12);
            this.btnDivision.Name = "btnDivision";
            this.btnDivision.Size = new System.Drawing.Size(156, 25);
            this.btnDivision.TabIndex = 4;
            this.btnDivision.Text = "Division Method";
            this.btnDivision.UseVisualStyleBackColor = true;
            this.btnDivision.Click += new System.EventHandler(this.BtnDivision_Click);
            // 
            // btnIterations
            // 
            this.btnIterations.Location = new System.Drawing.Point(692, 103);
            this.btnIterations.Name = "btnIterations";
            this.btnIterations.Size = new System.Drawing.Size(156, 23);
            this.btnIterations.TabIndex = 5;
            this.btnIterations.Text = "Simple Iterations Method";
            this.btnIterations.UseVisualStyleBackColor = true;
            this.btnIterations.Click += new System.EventHandler(this.BtnIterations_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(692, 643);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Valyti";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnStrings
            // 
            this.btnStrings.Location = new System.Drawing.Point(692, 74);
            this.btnStrings.Name = "btnStrings";
            this.btnStrings.Size = new System.Drawing.Size(156, 23);
            this.btnStrings.TabIndex = 7;
            this.btnStrings.Text = "String Method";
            this.btnStrings.UseVisualStyleBackColor = true;
            this.btnStrings.Click += new System.EventHandler(this.BtnStrings_Click);
            // 
            // radioF
            // 
            this.radioF.AutoSize = true;
            this.radioF.Checked = true;
            this.radioF.Location = new System.Drawing.Point(692, 413);
            this.radioF.Name = "radioF";
            this.radioF.Size = new System.Drawing.Size(75, 17);
            this.radioF.TabIndex = 9;
            this.radioF.TabStop = true;
            this.radioF.Text = "Function F";
            this.radioF.UseVisualStyleBackColor = true;
            // 
            // radioG
            // 
            this.radioG.AutoSize = true;
            this.radioG.Location = new System.Drawing.Point(692, 436);
            this.radioG.Name = "radioG";
            this.radioG.Size = new System.Drawing.Size(77, 17);
            this.radioG.TabIndex = 10;
            this.radioG.TabStop = true;
            this.radioG.Text = "Function G";
            this.radioG.UseVisualStyleBackColor = true;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(692, 43);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(156, 25);
            this.btnScan.TabIndex = 11;
            this.btnScan.Text = "Scan Method";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // radioH
            // 
            this.radioH.AutoSize = true;
            this.radioH.Location = new System.Drawing.Point(692, 459);
            this.radioH.Name = "radioH";
            this.radioH.Size = new System.Drawing.Size(77, 17);
            this.radioH.TabIndex = 12;
            this.radioH.TabStop = true;
            this.radioH.Text = "Function H";
            this.radioH.UseVisualStyleBackColor = true;
            // 
            // btnGauss
            // 
            this.btnGauss.Location = new System.Drawing.Point(692, 150);
            this.btnGauss.Name = "btnGauss";
            this.btnGauss.Size = new System.Drawing.Size(156, 23);
            this.btnGauss.TabIndex = 13;
            this.btnGauss.Text = "Gausian Elimination";
            this.btnGauss.UseVisualStyleBackColor = true;
            this.btnGauss.Click += new System.EventHandler(this.BtnGauss_Click);
            // 
            // btnOptimization
            // 
            this.btnOptimization.Location = new System.Drawing.Point(692, 208);
            this.btnOptimization.Name = "btnOptimization";
            this.btnOptimization.Size = new System.Drawing.Size(156, 23);
            this.btnOptimization.TabIndex = 14;
            this.btnOptimization.Text = "Optimization";
            this.btnOptimization.UseVisualStyleBackColor = true;
            this.btnOptimization.Click += new System.EventHandler(this.BtnOptimization_Click);
            // 
            // butBroiden
            // 
            this.butBroiden.Location = new System.Drawing.Point(692, 179);
            this.butBroiden.Name = "butBroiden";
            this.butBroiden.Size = new System.Drawing.Size(156, 23);
            this.butBroiden.TabIndex = 15;
            this.butBroiden.Text = "Broiden";
            this.butBroiden.UseVisualStyleBackColor = true;
            this.butBroiden.Click += new System.EventHandler(this.butBroiden_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 678);
            this.Controls.Add(this.butBroiden);
            this.Controls.Add(this.btnOptimization);
            this.Controls.Add(this.btnGauss);
            this.Controls.Add(this.radioH);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.radioG);
            this.Controls.Add(this.radioF);
            this.Controls.Add(this.btnStrings);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnIterations);
            this.Controls.Add(this.btnDivision);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textOutputBox);
            this.Controls.Add(this.chartGraph);
            this.Name = "Form1";
            this.Text = "Skaitiniai metodai ir Algoritmai";
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Paruošiamas langas vaizdavimui
        /// </summary>
        public void PreparareForm(float xmin, float xmax, float ymin, float ymax)
        {

            float x_grids = 10;
            //double xmin = 0; double xmax = 2 * Math.PI;
            chartGraph.ChartAreas[0].AxisX.MajorGrid.Interval = (xmax - xmin) / x_grids;
            chartGraph.ChartAreas[0].AxisX.LabelStyle.Interval = (xmax - xmin) / x_grids;
            chartGraph.ChartAreas[0].AxisX.MajorTickMark.Interval = (xmax - xmin) / x_grids;
            chartGraph.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Courier New", 8, FontStyle.Bold);

            float y_grids = 10;
            //double ymin = -1; double ymax = 1;
            chartGraph.ChartAreas[0].AxisY.MajorGrid.Interval = (ymax - ymin) / y_grids;
            chartGraph.ChartAreas[0].AxisY.LabelStyle.Interval = (ymax - ymin) / y_grids;
            chartGraph.ChartAreas[0].AxisY.MajorTickMark.Interval = (ymax - ymin) / y_grids;
            chartGraph.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Courier New", 8, FontStyle.Bold);

            chartGraph.ChartAreas[0].AxisX.Minimum = xmin;
            chartGraph.ChartAreas[0].AxisX.Maximum = xmax;
            chartGraph.ChartAreas[0].AxisY.Minimum = ymin;
            chartGraph.ChartAreas[0].AxisY.Maximum = ymax;

            chartGraph.Legends[0].Font = new Font("Times New Roman", 12, FontStyle.Bold);
            chartGraph.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartGraph.ChartAreas[0].CursorX.Interval = 0.01;
            chartGraph.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chartGraph.ChartAreas[0].CursorY.Interval = 0.01;
            
        }
        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
        private System.Windows.Forms.RichTextBox textOutputBox;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDivision;
        private Button btnIterations;
        private Button btnClear;
        private Button btnStrings;
        private RadioButton radioF;
        private RadioButton radioG;
        private Button btnScan;
        private RadioButton radioH;
        private Button btnGauss;
        private Button btnOptimization;
        private Button butBroiden;
    }



}

