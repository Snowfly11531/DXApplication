using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DXApplication4.外部函数;
using DXApplication4;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System.IO;
using DevExpress.Utils.MVVM.Services;

namespace DXApplication4
{
    public partial class PopupButtonEditForm : DevExpress.XtraEditors.XtraForm
    {
        public ButtonEdit bindButtonEdit;
        public List<int> layerIndexList = new List<int>();
        public class FeatureButtonEdit : ButtonEdit
        {
            public IFeatureLayer featureLayer
            {
                get
                {
                    return featureLayer;
                }
                set
                {
                    if (value != null)
                    {
                        this.BackColor = Color.GreenYellow;
                    }
                    else {
                        XtraMessageBox.Show("矢量文件读取失败");
                        this.BackColor = Color.PaleVioletRed;
                    }
                }
            }
        };
        public class RasterButtonEdit : ButtonEdit
        {
            public IRasterLayer rasterLayer
            {
                get
                {
                    return rasterLayer;
                }
                set
                {
                    if (value != null)
                    {
                        this.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        XtraMessageBox.Show("栅格文件读取失败");
                        this.BackColor = Color.PaleVioletRed;
                    }
                }
            }
        };
        public class SaveButtonEdit : ButtonEdit
        {
            public SaveButtonEdit():base()
            {
                
            }
            public new String Text
            {
                get
                {
                    return Text;
                }
                set
                {
                    if (File.Exists(value))
                    {
                        XtraMessageBox.Show("该文件已存在，请更换");
                        this.BackColor = Color.PaleVioletRed;
                    }
                    else
                    {
                        this.BackColor = Color.GreenYellow;
                    }
                }
            }
        };
        List<NameAndLayer> lists;
        List<NameAndLayer> FeatureLists;
        List<NameAndLayer> RasterLists;
        private struct NameAndLayer{
            public string name;
            public ILayer layer;
        }
        private List<NameAndLayer> getNameAndLayer(AxMapControl axMapControl){
            List<NameAndLayer> lists = new List<NameAndLayer>();
            for(int i=0;i<axMapControl.Map.LayerCount;i++){
                lists.Add(new NameAndLayer(){name=axMapControl.Map.get_Layer(i).Name,layer=axMapControl.Map.get_Layer(i)});
            }
            return lists;
        }
        public PopupButtonEditForm()
        {
            InitializeComponent();
            listBoxControl1.Hide();
        }

        private void showPopup(object hander1, EventArgs e1){
            this.bindButtonEdit = hander1 as ButtonEdit;
            listBoxControl1.Top = bindButtonEdit.Top + bindButtonEdit.Height;
            listBoxControl1.Left = bindButtonEdit.Left;
            listBoxControl1.Width = bindButtonEdit.Width;
            listBoxControl1.Height = (listBoxControl1.Font.Height+3) * listBoxControl1.ItemCount < 100 ? 
                (listBoxControl1.Font.Height+3) * listBoxControl1.ItemCount : 100;
            listBoxControl1.Items.RemoveAll();
            if (bindButtonEdit is FeatureButtonEdit)
            {
                listBoxControl1.Items.AddRange(FeatureLists.Select(i => i.name).ToArray<String>());
            }
            else if (bindButtonEdit is RasterButtonEdit)
            {
                listBoxControl1.Items.AddRange(RasterLists.Select(i => i.name).ToArray<String>());
            }
            listBoxControl1.Show();             

        }

        private void hidePopup(object hander1, EventArgs e1)
        {
            listBoxControl1.Hide();
        }

        public new void Show(IWin32Window form)
        {
            base.Show(form);
            Form1 form1 = this.Owner as Form1;
            lists = getNameAndLayer(form1.axMapControl1);
            FeatureLists = lists.Where(i => i.layer is IFeatureLayer).ToList();
            RasterLists = lists.Where(i => i.layer is IRasterLayer).ToList();
            foreach (var control in this.Controls)
            {
                if (control is ButtonEdit)
                {
                    Console.WriteLine((control as ButtonEdit).Name);
                    var buttonEdit = control as ButtonEdit;
                    if (buttonEdit is FeatureButtonEdit)
                    {
                        (buttonEdit as FeatureButtonEdit).ButtonClick += (hander1, e1) =>
                        {
                            String path=OSFileAndDir.OpenFile("打开矢量图层", "Shapefile(*.shp)|*.shp");
                            IFeatureLayer pFeatureLayer=null;
                            try
                            {
                                GISdataManager.readSHP(path, ref pFeatureLayer);
                            }
                            catch { }
                            (buttonEdit as FeatureButtonEdit).Text = path;
                            (buttonEdit as FeatureButtonEdit).featureLayer = pFeatureLayer;
                        };
                    }
                    if (buttonEdit is RasterButtonEdit)
                    {
                        (buttonEdit as RasterButtonEdit).ButtonClick += (hander1, e1) =>
                        {
                            String path = OSFileAndDir.OpenFile("打开栅格图层", "TIFF(*.tif)|*.tif|IMAGE(*.img)|img");
                            IRasterLayer pRasterLayer = null;
                            try
                            {
                                GISdataManager.readRaster(path, ref pRasterLayer);
                            }
                            catch { }
                            (buttonEdit as RasterButtonEdit).Text = path;
                            (buttonEdit as RasterButtonEdit).rasterLayer = pRasterLayer;
                        };
                    }
                    if (buttonEdit is SaveButtonEdit)
                    {
                        (buttonEdit as SaveButtonEdit).ButtonClick += (hander1, e1) =>
                        {
                            String path = OSFileAndDir.SaveFile("保存图层", "ShapeFile(*.shp)|*.shp|TIFF(*.tif)|*.tif|IMAGE(*.img)|img");
                            (buttonEdit as SaveButtonEdit).Text = path;
                        };
                    }
                    buttonEdit.Click += showPopup;
                    buttonEdit.Click += showPopup;
                }
            }
            this.Click += hidePopup;
        }

        private void listBoxControl1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxControl1.Visible)
            {
                try
                {
                    bindButtonEdit.Focus();
                    Console.WriteLine(bindButtonEdit.Name);
                    this.bindButtonEdit.Text = listBoxControl1.SelectedValue.ToString();
                    if (bindButtonEdit is FeatureButtonEdit)
                    {
                        (bindButtonEdit as FeatureButtonEdit).featureLayer = FeatureLists[listBoxControl1.SelectedIndex].layer as IFeatureLayer;
                    }
                    else if (bindButtonEdit is RasterButtonEdit)
                    {
                        (bindButtonEdit as RasterButtonEdit).rasterLayer = RasterLists[listBoxControl1.SelectedIndex].layer as IRasterLayer;
                    }
                    if (this.bindButtonEdit.Text.Length!=0)
                    {
                        listBoxControl1.Hide();
                    }
                }
                catch
                {
                    Console.WriteLine("一点小bug，问题不大");
                }
            }
        }

        public void btnRun_Click(object sender, EventArgs e)
        {

        }


        
    }
}