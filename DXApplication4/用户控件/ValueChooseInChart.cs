using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;

namespace DXApplication4.用户控件
{
    public partial class ValueChooseInChart : DevExpress.XtraEditors.XtraUserControl
    {
        struct TextAndLine
        {
            public TextEdit textEdit;
            public ConstantLine constantLine;
        }
        #region 全局变量
        TextAndLine[] textAndLine;
        private ConstantLine curruentConstantLine=null;
        Boolean mouseDown = false;
        int valueCount;
        double maxValue;
        double minValue;
        #endregion

        public ValueChooseInChart()
        {
            InitializeComponent();
        }

        public void drawLine(double[] ArrVal, System.Int32[] ArrFreq, int valueCount)
        {
            maxValue = ArrVal.Max();
            minValue = ArrVal.Min();
            this.valueCount = valueCount;
            chartControl1.Series[0].Points.RemoveRange(0, chartControl1.Series[0].Points.Count);
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            WholeRange range = diagram.AxisX.WholeRange;
            range.SetMinMaxValues(0, maxValue);
            chartControl1.Refresh();
            for(int i=panelControl9.Controls.Count;i>0;i--){
                panelControl9.Controls.RemoveAt(i - 1);
            }
            if (ArrFreq.Length == 0)
            {
                XtraMessageBox.Show("当前属性无法自定义分级，请换一个");
                return;
            }
            #region 添加点值
            for (int i = 0; i < ArrVal.Length; i++)
            {
                SeriesPoint point = new SeriesPoint();
                point.Argument = ArrVal[i].ToString();
                double[] value = new double[1];
                value[0] = Convert.ToDouble(ArrFreq[i]);
                point.Values = value;
                chartControl1.Series[0].Points.Add(point);
            }
            drawConstantline();
            #endregion
        }
        public void drawline(double minValue,double maxValue,int valueCount){
            for (int i = panelControl9.Controls.Count; i > 0; i--)
            {
                panelControl9.Controls.RemoveAt(i - 1);
            }
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.valueCount = valueCount;
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            WholeRange range = diagram.AxisX.WholeRange;
            range.SetMinMaxValues(0, maxValue);
            chartControl1.Refresh();
            drawConstantline();
            
        }

        private void drawConstantline()
        {
            #region 在图标中画分割线，并将分割线的值显示在textedit中
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisX.ConstantLines.Clear();
            textAndLine = new TextAndLine[valueCount];
            for (int i = 0; i < valueCount; i++)
            {
                TextEdit textedit = new TextEdit();
                textedit.Dock = DockStyle.Top;
                textedit.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
                double value = (maxValue - minValue) * (i + 1) / valueCount + minValue;
                ConstantLine line = new ConstantLine(string.Format("{0:############0.#######}", value), value);
                textedit.Text = line.Name;
                line.Color = Color.Pink;
                textAndLine[i] = new TextAndLine() { textEdit = textedit, constantLine = line };
                diagram.AxisX.ConstantLines.Add(line);
            }
            #endregion
            #region 设置textedit中值改变时constantline自动改变
            for (int i = 0; i < valueCount; i++)
            {
                panelControl9.Controls.Add(textAndLine[valueCount - i - 1].textEdit);
                textAndLine[valueCount - i - 1].textEdit.TextChanged += (sender1, e1) =>
                {
                    try
                    {
                        if (Convert.ToDouble((sender1 as TextEdit).Text) >= minValue)
                        {
                            textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.AxisValue =
                                Convert.ToDouble((sender1 as TextEdit).Text);
                            textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.Name =
                                (sender1 as TextEdit).Text;
                        }

                    }
                    catch
                    {
                    }
                };
                textAndLine[valueCount - i - 1].textEdit.LostFocus += (sender1, e1) =>
                {
                    try
                    {
                        if (Convert.ToDouble((sender1 as TextEdit).Text) < minValue)
                        {
                            XtraMessageBox.Show("输入值不再范围内");
                            (sender1 as TextEdit).Text =
                                textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.Name;
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("请输入数字谢谢，本来能限制的，不想改了");
                        (sender1 as TextEdit).Text =
                            textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.Name;
                    }
                };
                textAndLine[valueCount - i - 1].textEdit.KeyDown += (sender1, e1) =>
                {
                    if (e1.KeyCode == Keys.Enter)
                    {
                        try
                        {
                            if (Convert.ToDouble((sender1 as TextEdit).Text) < minValue)
                            {
                                XtraMessageBox.Show("输入值不再范围内");
                                (sender1 as TextEdit).Text =
                                    textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.Name;
                            }
                        }
                        catch
                        {
                            XtraMessageBox.Show("请输入数字谢谢，本来能限制的，不想改了");
                            (sender1 as TextEdit).Text =
                                textAndLine[valueCount - 1 - panelControl9.Controls.IndexOf(sender1 as Control)].constantLine.Name;
                        }
                    }
                };
            }
            #endregion
        }
        private void chartControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            ChartHitInfo hitinfo = chartControl1.CalcHitInfo(e.X, e.Y);
            curruentConstantLine = hitinfo.ConstantLine;
        }

        private void chartControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (curruentConstantLine != null)
                {
                    //this.Cursor = System.Windows.Forms.Cursors.VSplit;
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    DiagramCoordinates coordinates = diagram.PointToDiagram(e.Location);
                    if ( coordinates.NumericalArgument >= minValue)
                    {
                        curruentConstantLine.AxisValue = coordinates.NumericalArgument;
                        curruentConstantLine.Name = string.Format("{0:############0.#######}", coordinates.NumericalArgument);
                        foreach (var tL in textAndLine)
                        {
                            tL.textEdit.Text = tL.constantLine.Name;
                        }
                    }
                }
            }
            else
            {
                ChartHitInfo hitinfo = chartControl1.CalcHitInfo(e.X, e.Y);
                if (hitinfo.ConstantLine != null)
                {
                    this.Cursor = System.Windows.Forms.Cursors.VSplit;
                }
            }
        }

        private void chartControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        public double[] getValues() {
            double[] values = new double[valueCount];
            for (int i = 0; i < valueCount; i++) {
                values[i] = Convert.ToDouble((panelControl9.Controls[i] as TextEdit).Text);
            }
            Array.Sort(values);
            print(values);
            return values;
        }
        public void print<T>(IEnumerable<T> values) {
            foreach (var value in values) {
                Console.WriteLine(value);
            }
        }
    }
}
