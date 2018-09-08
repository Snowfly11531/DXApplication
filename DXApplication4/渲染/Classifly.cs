using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace DXApplication4.渲染
{
    class Classifly
    {
        #region 栅格渲染
        public static void classifyRender(IRasterLayer rastlayer, string classMethod, int count, IColorRamp ramp)
        {
            try
            {
                IRasterBand band = GetBand(rastlayer);
                if (band.Histogram== null) {
                    band.ComputeStatsAndHist();
                }
                IRasterClassifyColorRampRenderer rasClassifyRender = new RasterClassifyColorRampRendererClass();
                IRasterRenderer rasRender = rasClassifyRender as IRasterRenderer;
                rasRender.Raster = rastlayer.Raster;
                rasRender.Update();

                int numClasses = count;
                IClassify classify = null;

                switch (classMethod)
                {
                    case "等间距分级":
                        classify = new EqualIntervalClass();
                        break;
                    case "自然断点分级":
                        classify = new NaturalBreaksClass();
                        break;
                }
                classify.Classify(ref numClasses);

                double[] Classes = classify.ClassBreaks as double[];
                UID pUid = classify.ClassID;
                IRasterClassifyUIProperties rasClassifyUI = rasClassifyRender as IRasterClassifyUIProperties;
                rasClassifyUI.ClassificationMethod = pUid;
                rasClassifyRender.ClassField = "Value";
                rasClassifyRender.ClassCount = count;
                rasRender.Update();
                IColor pColor;
                ISimpleFillSymbol pSym;

                for (int j = 0; j < count; j++)
                {
                    pColor = ramp.get_Color(j * (ramp.Size - 1) / (count - 1));
                    pSym = new SimpleFillSymbolClass();
                    pSym.Color = pColor;
                    rasClassifyRender.set_Symbol(j, (ISymbol)pSym);
                    rasClassifyRender.set_Break(j, rasClassifyRender.get_Break(j));                  
                }
                rasRender.Update();

                rastlayer.Renderer = rasClassifyRender as IRasterRenderer;
            }
            catch
            {
                XtraMessageBox.Show("唯一值数量已达到限制（65536）");
            }
        }

        public static void userRender(IRasterLayer rastlayer, IColorRamp ramp, int count, double[] values) {
            IRasterBand band = GetBand(rastlayer);
            if (band.Histogram == null)
            {
                band.ComputeStatsAndHist();
            }
            IRasterClassifyColorRampRenderer rasClassifyRender = new RasterClassifyColorRampRendererClass();
            IRasterRenderer rasRender = rasClassifyRender as IRasterRenderer;
            rasRender.Raster = rastlayer.Raster;
            rasRender.Update();
            //rasClassifyRender.ClassField = "Value";
            rasClassifyRender.ClassCount = count;
            rasRender.Update();
            IColor pColor;
            ISimpleFillSymbol pSym;
            int j;
            for (j = 0; j < count; j++)
            {
                pColor = ramp.get_Color(j * (ramp.Size - 1) / (count - 1));
                pSym = new SimpleFillSymbolClass();
                pSym.Color = pColor;
                rasClassifyRender.set_Symbol(j, (ISymbol)pSym);
                rasClassifyRender.set_Break(j, values[j - 1 < 0 ? 0 : j - 1]);
                if (j == 0)
                {
                    rasClassifyRender.set_Label(j, string.Format("{0:#######0.#####}-{1:#######0.#####}", 0, values[j]));
                }
                else
                {
                    rasClassifyRender.set_Label(j, string.Format("{0:#######0.#####}-{1:#######0.#####}", values[j-1], values[j]));
                }
                Console.WriteLine("自定义" + values[j]+","+rasClassifyRender.get_Break(j));
            }
            rasClassifyRender.set_Break(j, values[j-1]);
            rasRender.Update();
            rastlayer.Renderer = rasClassifyRender as IRasterRenderer;
        }

        public static void stretchRender(IRasterLayer rasterLayer,IColorRamp pColorRamp)
        {
            if (rasterLayer == null)
                return;

            IRaster raster = rasterLayer.Raster;
            IRasterStretchColorRampRenderer rasterStretchColorRampRenderer = new RasterStretchColorRampRendererClass();
            IRasterRenderer rasterRenderer = rasterStretchColorRampRenderer as IRasterRenderer;
            rasterRenderer.Raster = raster;
            rasterRenderer.Update();
            rasterStretchColorRampRenderer.ColorRamp = pColorRamp;
            rasterRenderer.Update();
            rasterLayer.Renderer = rasterStretchColorRampRenderer as IRasterRenderer;
        }

        public static void uniqueRender(IRasterLayer rasterLayer) {
            if (rasterLayer == null) return;
            IRaster raster = rasterLayer.Raster;
            IRasterUniqueValueRenderer render = new RasterUniqueValueRendererClass();
            IRasterRenderer rasterRender = render as IRasterRenderer;
            rasterRender.Raster = raster;
            rasterRender.Update();
            IUniqueValues uniqueValues=new UniqueValuesClass();
            IRasterCalcUniqueValues calcUniqueValues=new RasterCalcUniqueValuesClass();
            calcUniqueValues.AddFromRaster(raster,0,uniqueValues);
            IRasterRendererUniqueValues renderUniqueValues=render as IRasterRendererUniqueValues;
            renderUniqueValues.UniqueValues=uniqueValues;
            render.Field="Value";
            render.HeadingCount=1;
            render.set_Heading(0,"淹没水深");
            render.set_ClassCount(0,uniqueValues.Count);

            IRandomColorRamp ramp = new RandomColorRampClass();
            ramp.Size = uniqueValues.Count;
            bool b = true;
            ramp.CreateRamp(out b);

            for (int i = 0; i < uniqueValues.Count; i++) {
                render.AddValue(0,i, uniqueValues.get_UniqueValue(i));
                render.set_Label(0, i, uniqueValues.get_UniqueValue(i).ToString());
                ISimpleFillSymbol fillSymbol=new SimpleFillSymbolClass();
                fillSymbol.Color=ramp.get_Color(i);
                render.set_Symbol(0, i, fillSymbol as ISymbol);
            }
            rasterRender.Update();
            rasterLayer.Renderer = rasterRender;
        }
        #endregion

        #region 矢量渲染
        public static void uniqueRender(IFeatureLayer featLayer, string fieldName)
        {
            IGeoFeatureLayer pGeoFeatureLayer = featLayer as IGeoFeatureLayer;
            IFeatureClass pFeatureClass = featLayer.FeatureClass;      //获取图层上的featureClass            
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRendererClass();   //唯一值渲染器
            //设置渲染字段对象
            pUniqueValueRenderer.FieldCount = 1;
            pUniqueValueRenderer.set_Field(0, fieldName);

            ISimpleFillSymbol pSimFillSymbol = new SimpleFillSymbolClass();   //创建填充符号
            pUniqueValueRenderer.DefaultSymbol = (ISymbol)pSimFillSymbol;
            pUniqueValueRenderer.UseDefaultSymbol = false;
            int n = pFeatureClass.FeatureCount(null);
            for (int i = 0; i < n; i++)
            {
                IFeature pFeature = pFeatureCursor.NextFeature();
                string pFeatureValue = pFeature.get_Value(pFeature.Fields.FindField(fieldName)).ToString();
                pUniqueValueRenderer.AddValue(pFeatureValue, "", null);

            }
            IRandomColorRamp colorRamp = new RandomColorRampClass();
            colorRamp.Size = pUniqueValueRenderer.ValueCount;
            bool b = true;
            colorRamp.CreateRamp(out b);
            //为每个符号设置颜色

            for (int i = 0; i <= pUniqueValueRenderer.ValueCount - 1; i++)
            {
                string xv = pUniqueValueRenderer.get_Value(i);

                if (xv != "")
                {
                    pUniqueValueRenderer.set_Symbol(xv, getISymbolByGeomType(featLayer, 
                        colorRamp.get_Color(i * (colorRamp.Size - 1) / (pUniqueValueRenderer.ValueCount - 1))));
                }
            }
            pGeoFeatureLayer.Renderer = (IFeatureRenderer)pUniqueValueRenderer;
        }

        private static ISymbol getISymbolByGeomType(IFeatureLayer featLayer,IColor pColor)
        {
            ProgressWindow window = new ProgressWindow();
            ISymbol pSymbol=null;
            switch (featLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                    pNextSymbol.Color = pColor;
                    pSymbol = (ISymbol)pNextSymbol;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    ILineSymbol pNextSymbol1 = new CartographicLineSymbolClass();
                    pNextSymbol1.Color = pColor;
                    pSymbol = (ISymbol)pNextSymbol1;
                    break;
                case esriGeometryType.esriGeometryPoint:
                    ISimpleMarkerSymbol pNextSymbol2 = new SimpleMarkerSymbolClass();
                    pNextSymbol2.Size = 4;
                    pNextSymbol2.Outline = true;
                    pNextSymbol2.OutlineColor = getcolor(0, 0, 0);
                    pNextSymbol2.OutlineSize = 1;
                    pNextSymbol2.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pNextSymbol2.Color = pColor;
                    pSymbol = (ISymbol)pNextSymbol2;
                    break;
                default:
                    break;
            }
            return pSymbol;
        }
        public static void userRender(IFeatureLayer featLayer, string classMethod, string fieldName, IColorRamp colorRamp, int count,double[] values) {
            if (values.Length != count) {
                XtraMessageBox.Show("好像有一些问题，要不重试一下");
                return;
            }
            IClassBreaksRenderer pRender = new ClassBreaksRendererClass();
            pRender.BreakCount = count;
            pRender.Field = fieldName;
            IColor pColor;
            for (int i = 0; i < count; i++)
            {
                pColor = colorRamp.get_Color(i);
                pRender.set_Symbol(i, getISymbolByGeomType(featLayer,pColor));
                pRender.set_Break(i, values[i]);
            }
            IGeoFeatureLayer pGeoLyr = (IGeoFeatureLayer)featLayer;
            pGeoLyr.Renderer = (IFeatureRenderer)pRender;
        }

        public static void classifyRender(IFeatureLayer featLayer, string classMethod, string fieldName, IColorRamp colorRamp, int count)
        {
            try
            {
                //值分级
                IBasicHistogram pBasicHis = new BasicTableHistogramClass();
                ITableHistogram pTabHis = (ITableHistogram)pBasicHis;
                IClassifyGEN pClassify = null;
                switch (classMethod)
                {
                    case "等间距分级":
                        pClassify = new EqualIntervalClass();
                        break;
                    case "自然断点分级":
                        pClassify = new NaturalBreaksClass();
                        break;
                }
                pTabHis.Field = fieldName;
                ILayer Layer = (ILayer)featLayer;
                ITable pTab = (ITable)Layer;
                pTabHis.Table = pTab;
                object doubleArrVal, longArrFreq;
                pBasicHis.GetHistogram(out doubleArrVal, out longArrFreq);

                int nDes = count;
                pClassify.Classify(doubleArrVal, longArrFreq, ref nDes);
                double[] ClassNum;
                ClassNum = (double[])pClassify.ClassBreaks;
                int ClassCountResult = ClassNum.GetUpperBound(0);
                IClassBreaksRenderer pRender = new ClassBreaksRendererClass();
                pRender.BreakCount = ClassCountResult;
                pRender.Field = fieldName;
                ISimpleFillSymbol pSym;
                IColor pColor;
                for (int j = 0; j < ClassCountResult; j++)
                {
                    pColor = colorRamp.get_Color(j * (colorRamp.Size - 1) / (ClassCountResult - 1));
                    pRender.set_Symbol(j, getISymbolByGeomType(featLayer,pColor));
                    pRender.set_Break(j, ClassNum[j + 1]);
                    pRender.set_Label(j, ClassNum[j].ToString("0.00") + " - " + ClassNum[j + 1].ToString("0.00"));
                }

                IGeoFeatureLayer pGeoLyr = (IGeoFeatureLayer)Layer;
                pGeoLyr.Renderer = (IFeatureRenderer)pRender;
            }
            catch {
                XtraMessageBox.Show("嗯，哪个，就一个值，别分级了好不好^_^");
            }
        }

        public static void ProportionalRenderer(IFeatureLayer featLayer, string fieldName, IColor pColor, double count)
        {
            IProportionalSymbolRenderer psrender = new ProportionalSymbolRendererClass();
            psrender.Field = fieldName;
            psrender.ValueUnit = esriUnits.esriUnknownUnits;
            psrender.ValueRepresentation = esriValueRepresentations.esriValueRepUnknown;
            //选择渲染的样式，与颜色 minsymbol为比填内容，否则没有效果
            ISimpleMarkerSymbol markersym = new SimpleMarkerSymbol();
            markersym.Size = count;
            markersym.Style = esriSimpleMarkerStyle.esriSMSCircle;
            markersym.Color = pColor;
            psrender.MinSymbol = markersym as ISymbol;
            //IFeatureLayer featLayer = featLayer;
            IGeoFeatureLayer geofeat = featLayer as IGeoFeatureLayer;
            ICursor cursor = ((ITable)featLayer).Search(null, true);
            IDataStatistics datastat = new DataStatisticsClass();
            datastat.Cursor = cursor;
            datastat.Field = fieldName;//千万不能忽视
            IStatisticsResults statisticsResult;
            
            try
            {
                statisticsResult = datastat.Statistics;
                psrender.MinDataValue = statisticsResult.Minimum + 0.1;
                psrender.MaxDataValue = statisticsResult.Maximum;
                ////设置background的样式
                IFillSymbol fillsym = new SimpleFillSymbolClass();
                fillsym.Color = getcolor(201, 201, 251);
                ILineSymbol linesym = new SimpleLineSymbolClass();
                linesym.Width = 1;
                fillsym.Outline = linesym;
                psrender.BackgroundSymbol = fillsym;
                psrender.LegendSymbolCount = 6;//legend的数量
                psrender.CreateLegendSymbols();//创建TOC的legend
                geofeat.Renderer = (IFeatureRenderer)psrender;
            }
            catch
            {
                XtraMessageBox.Show("错误，选择的属性不是数值型！");
            }
        }
        #endregion

        public void ProportionalRenderer1(IFeatureLayer featLayer, string fieldName, IColorRamp colorRamp, int size)
        {
            IGeoFeatureLayer geoFeatureLayer;
            IFeatureLayer featureLayer;
            IProportionalSymbolRenderer proportionalSymbolRenderer;
            ITable table;
            ICursor cursor;
            IDataStatistics dataStatistics;
            IStatisticsResults statisticsResult;
            stdole.IFontDisp fontDisp;

            geoFeatureLayer = featLayer as IGeoFeatureLayer;
            featureLayer = geoFeatureLayer as IFeatureLayer;
            table = geoFeatureLayer as ITable;
            cursor = table.Search(null, true);
            dataStatistics = new DataStatisticsClass();
            dataStatistics.Cursor = cursor;
            dataStatistics.Field = fieldName;
            statisticsResult = dataStatistics.Statistics;
            if (statisticsResult != null)
            {
                IFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Color = colorRamp.get_Color(0);
                ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbolClass();

                fontDisp = new stdole.StdFontClass() as stdole.IFontDisp;
                fontDisp.Name = "arial";
                fontDisp.Size = 20;
                characterMarkerSymbol.Font = fontDisp;
                characterMarkerSymbol.CharacterIndex = 90;
                characterMarkerSymbol.Color = colorRamp.get_Color(4);
                characterMarkerSymbol.Size = size;
                proportionalSymbolRenderer = new ProportionalSymbolRendererClass();
                proportionalSymbolRenderer.ValueUnit = esriUnits.esriUnknownUnits;
                proportionalSymbolRenderer.Field = fieldName;
                proportionalSymbolRenderer.FlanneryCompensation = false;
                proportionalSymbolRenderer.MinDataValue = statisticsResult.Minimum;
                proportionalSymbolRenderer.MaxDataValue = statisticsResult.Maximum;
                proportionalSymbolRenderer.BackgroundSymbol = fillSymbol;
                proportionalSymbolRenderer.MinSymbol = characterMarkerSymbol as ISymbol;
                proportionalSymbolRenderer.LegendSymbolCount = 10;
                proportionalSymbolRenderer.CreateLegendSymbols();
                geoFeatureLayer.Renderer = proportionalSymbolRenderer as IFeatureRenderer;
            }
        }
        public static IRgbColor getcolor(int r, int g, int b)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = r;
            color.Green = g;
            color.Blue = b;
            return color;
        }
        public  static IRasterBand GetBand(IRasterLayer rasterlayer)
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
    }
}
