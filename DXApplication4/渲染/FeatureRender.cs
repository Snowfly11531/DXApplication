using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace DXApplication4.渲染
{
    public partial class FeatureRender : DevExpress.XtraEditors.XtraForm
    {
        AxMapControl axMapControl1;
        IFeatureLayer featureLayer;
        AxTOCControl axTOCControl1;
        public FeatureRender(AxMapControl axMapControl1, AxTOCControl axTOCControl1, IFeatureLayer featureLayer)
        {
            this.axMapControl1 = axMapControl1;
            this.axTOCControl1 = axTOCControl1;
            this.featureLayer = featureLayer;
            InitializeComponent();
            IFeatureClass featureClass = featureLayer.FeatureClass;
            fieldChoose(field =>
            {
                return true;
            });
            this.Height = 106;
        }
        private IColorRamp getRamp()
        {
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

        private void simpleButton1_Click(object sender, EventArgs e)            //渲染图层
        {
            if (comboBoxEdit2.Text == "分级渲染")
            {
                if (comboBoxEdit3.Text != "自定义分级")
                {
                    Classifly.classifyRender(featureLayer, comboBoxEdit3.Text, comboBoxEdit1.Text,
                         getRamp(), Convert.ToInt16(spinEdit1.Text));
                }else{
                    Classifly.userRender(featureLayer, comboBoxEdit3.Text, comboBoxEdit1.Text,
                         getRamp(), Convert.ToInt16(spinEdit1.Text),valueChooseInChart1.getValues());
                }
            }
            else if (comboBoxEdit2.Text == "圆大小渲染")
            {

                Classifly.ProportionalRenderer(featureLayer, comboBoxEdit1.Text,
                    ColorToIColor(colorPickEdit3.Color), Convert.ToDouble(spinEdit2.Text));
            }
            else
            {
                Classifly.uniqueRender(featureLayer, comboBoxEdit1.Text);
            }
            axMapControl1.Refresh();
            axTOCControl1.Update();
        }        

        private void chooseNumericField()                                       //选择数值型字段
        {
            fieldChoose(field =>
            {
                if (field.Type == esriFieldType.esriFieldTypeDouble || field.Type == esriFieldType.esriFieldTypeSingle || field.Type == esriFieldType.esriFieldTypeInteger
                    || field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    IBasicHistogram pBasicHis = new BasicTableHistogramClass();
                    ITableHistogram pTabHis = (ITableHistogram)pBasicHis;
                    pTabHis.Field = field.Name;
                    ILayer Layer = (ILayer)featureLayer;
                    ITable pTab = (ITable)Layer;
                    pTabHis.Table = pTab;
                    object doubleArrVal, longArrFreq;
                    pBasicHis.GetHistogram(out doubleArrVal, out longArrFreq);
                    double[] ArrVal = (double[])doubleArrVal;
                    if (ArrVal.Length == 1) return false;
                    else return true;
                }
                else
                {
                    return false;
                }
            });
        }                                   

        private void fieldChoose(Func<IField, bool> chooseWay)                  //选择符合chooseWay条件的字段
        {
            for (int i = comboBoxEdit1.Properties.Items.Count - 1; i >= 0; i--)
            {
                comboBoxEdit1.Properties.Items.RemoveAt(i);
            }
            for (int i = 0; i < featureLayer.FeatureClass.Fields.FieldCount; i++)
            {
                IField pField = featureLayer.FeatureClass.Fields.Field[i];
                if (chooseWay(pField))
                {
                    comboBoxEdit1.Properties.Items.Add(pField.Name);
                }
            }
            comboBoxEdit1.Text = comboBoxEdit1.Properties.Items[0].ToString();
        }               

        private void comboBoxEdit2_TextChanged(object sender, EventArgs e)      //当渲染方式改变时，选择需要显示的控件,并限制显示的字段，如分级渲染时只加载数值型字段
        {
            if (comboBoxEdit2.Text == "分级渲染")
            {
                chooseNumericField();
                panelControl4.Visible = true;
                panelControl3.Visible = true;
                panelControl0.Visible = true;
                panelControl5.Visible = true;
                panelControl6.Visible = false;
                panelControl7.Visible = false;
                valueChooseInChart1.Visible = false;
            }
            else if (comboBoxEdit2.Text == "圆大小渲染")
            {
                chooseNumericField();
                panelControl4.Visible = false;
                panelControl3.Visible = false;
                panelControl0.Visible = false;
                panelControl5.Visible = false;
                panelControl6.Visible = true;
                panelControl7.Visible = true;
                valueChooseInChart1.Visible = false;
                
            }
            else
            {
                fieldChoose(field =>
                {
                    return true;
                });
                panelControl3.Visible = false;
                panelControl4.Visible = false;
                panelControl0.Visible = false;
                panelControl5.Visible = false;
                panelControl6.Visible = false;
                panelControl7.Visible = false;
                valueChooseInChart1.Visible= false;
            }
            autoHeight();
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

        private void drawLine()                                                 //在表中画出值的频率分布线
        { 
            IBasicHistogram pBasicHis = new BasicTableHistogramClass();
            ITableHistogram pTabHis = (ITableHistogram)pBasicHis;
            pTabHis.Field = comboBoxEdit1.Text;
            ILayer Layer = (ILayer)featureLayer;
            ITable pTab = (ITable)Layer;
            pTabHis.Table = pTab;
            object doubleArrVal, longArrFreq;
            pBasicHis.GetHistogram(out doubleArrVal, out longArrFreq);

            double[] ArrVal = (double[])doubleArrVal;
            System.Int32[] ArrFreq = (System.Int32[])longArrFreq;
            if (ArrVal.Length < 50)
            {
                valueChooseInChart1.drawLine(ArrVal, ArrFreq, Convert.ToInt16(spinEdit1.Text));
            }
            else
            {
                valueChooseInChart1.drawline(ArrVal.Min(), ArrVal.Max(), Convert.ToInt16(spinEdit1.Text));
            }
        }   

        private void comboBoxEdit3_TextChanged(object sender, EventArgs e)      //当分级方式改变时，判断是否为自定义分级，若是调出图标并重画
        {

            if (comboBoxEdit3.Text == "自定义分级")
            {
                drawLine();
                valueChooseInChart1.Visible = true;
                autoHeight();
            }
            else {
                valueChooseInChart1.Visible = false;
                autoHeight();
            }
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)      //当分类字段改变时，重画图表
        {
            if (valueChooseInChart1.Visible)
                drawLine();
        }

        private void spinEdit1_TextChanged(object sender, EventArgs e)          //当分类级数改变时，重画图表
        {
            if (valueChooseInChart1.Visible)
                drawLine();
        }

    }
}