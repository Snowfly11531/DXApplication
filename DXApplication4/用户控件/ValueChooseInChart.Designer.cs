namespace DXApplication4.用户控件
{
    partial class ValueChooseInChart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.RuntimeHitTesting = true;
            series1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            series1.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            series1.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "Series 1";
            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.View = splineSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(381, 159);
            this.chartControl1.TabIndex = 25;
            this.chartControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseDown);
            this.chartControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseMove);
            this.chartControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseUp);
            // 
            // panelControl9
            // 
            this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl9.Location = new System.Drawing.Point(381, 0);
            this.panelControl9.Name = "panelControl9";
            this.panelControl9.Size = new System.Drawing.Size(74, 159);
            this.panelControl9.TabIndex = 26;
            // 
            // ValueChooseInChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl9);
            this.Controls.Add(this.chartControl1);
            this.LookAndFeel.SkinName = "Caramel";
            this.Name = "ValueChooseInChart";
            this.Size = new System.Drawing.Size(455, 159);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.PanelControl panelControl9;
    }
}
