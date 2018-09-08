using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace DXApplication4.渲染
{
    public partial class RasterRender : DevExpress.XtraEditors.XtraForm
    {
        AxMapControl axMapControl1;
        IRasterLayer rasterLayer;
        AxTOCControl axTOCControl1;
        public RasterRender(AxMapControl axMapControl1,AxTOCControl axTOCControl1,IRasterLayer rasterLayer)
        {
            this.axMapControl1 = axMapControl1;
            this.axTOCControl1 =axTOCControl1;
            this.rasterLayer = rasterLayer;
            InitializeComponent();
            this.Height = 90;
        }
        private IColorRamp getRamp() {
            IAlgorithmicColorRamp ramp = new AlgorithmicColorRampClass();
            ramp.FromColor = ColorToIColor(colorPickEdit1.Color);
            ramp.ToColor = ColorToIColor(colorPickEdit2.Color);
            ramp.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;
            ramp.Size = Convert.ToInt16(spinEdit1.Text);
            bool b = true;
            ramp.CreateRamp(out b);
            return ramp;
        }

        public static IColor ColorToIColor(Color color)
        {
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pColor;
        }

        private void autoHeight()                                               //当控件个数改变时，自动调整窗体大小
        {
            int height = 0;
            foreach (Control control in this.Controls)
            {
                if (control.Visible)
                {
                    height += control.Height;
                }
            }
            this.Height = height + 30;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text == "分级渲染")
            {
                if (comboBoxEdit1.Text != "自定义分级")
                {
                    Classifly.classifyRender(rasterLayer, comboBoxEdit1.Text,
                        Convert.ToInt16(spinEdit1.Text), getRamp());
                }
                else {
                    Console.WriteLine("自定义");
                    Classifly.userRender(rasterLayer, getRamp(), Convert.ToInt32(spinEdit1.Text), valueChooseInChart1.getValues());
                }
            }
            else if (comboBoxEdit2.Text == "连续渲染")
            {
                Classifly.stretchRender(rasterLayer, getRamp());
            }
            else {
                Classifly.uniqueRender(rasterLayer);
            }
            axMapControl1.Refresh();
            axTOCControl1.Update();
        }
        public void drawLine() {
            IUniqueValues uniqueValues = new UniqueValuesClass();
            IRasterCalcStatsHistogram calcstatsHistogram = new RasterCalcStatsHistogramClass();
            IStatsHistogram statsHistogram = new StatsHistogramClass();
            calcstatsHistogram.ComputeFromRaster(rasterLayer.Raster, 0, statsHistogram);
            IRasterCalcUniqueValues calcUniqueValues = new RasterCalcUniqueValuesClass();
            try
            {
                calcUniqueValues.AddFromRaster(rasterLayer.Raster, 0, uniqueValues);
                double[] ArrVal = new double[uniqueValues.Count];
                System.Int32[] ArrFreq = new System.Int32[uniqueValues.Count];
                for (var i = 0; i < uniqueValues.Count; i++)
                {
                    ArrVal[i] = Convert.ToDouble(uniqueValues.get_UniqueValue(i));
                    ArrFreq[i] = uniqueValues.get_UniqueCount(i);
                }
                valueChooseInChart1.drawLine(ArrVal, ArrFreq, Convert.ToInt32(spinEdit1.Text));
            }
            catch
            {
                valueChooseInChart1.drawline(statsHistogram.Min, statsHistogram.Max, Convert.ToInt16(spinEdit1.Text));
            }
        }
        public IRasterBand GetBand(IRasterLayer rasterlayer)
        {
            string fullpath = rasterlayer.FilePath;
            string filePath = System.IO.Path.GetDirectoryName(fullpath);
            string fileName = System.IO.Path.GetFileName(fullpath);
            IWorkspaceFactory wsf = new RasterWorkspaceFactoryClass();
            IWorkspace ws = wsf.OpenFromFile(filePath, 0);
            IRasterWorkspace rasterws = ws as IRasterWorkspace;
            IRasterDataset rastdataset = rasterws.OpenRasterDataset(fileName);
            IRasterBandCollection bandcoll = rastdataset as IRasterBandCollection;
            return bandcoll.Item(0);
        }

        private void comboBoxEdit2_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text == "分级渲染")
            {
                panelControl2.Visible = true;
                panelControl3.Visible = true;
                panelControl4.Visible = true;
                panelControl0.Visible = true;
                valueChooseInChart1.Visible = false;
            }
            else if (comboBoxEdit2.Text == "连续渲染")
            {
                panelControl2.Visible = false;
                panelControl3.Visible = true;
                panelControl4.Visible = true;
                panelControl0.Visible = false;
                valueChooseInChart1.Visible = false;
            }
            else {
                panelControl2.Visible = false;
                panelControl3.Visible = false;
                panelControl4.Visible = false;
                panelControl0.Visible = false;
                valueChooseInChart1.Visible = false;
            }
            autoHeight();
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "自定义分级") {
                drawLine();
                valueChooseInChart1.Visible = true;
                autoHeight();
            }
        }

        private void spinEdit1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "自定义分级")
            {
                drawLine();
            }
        }

    }

}