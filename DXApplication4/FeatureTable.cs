using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace DXApplication4
{
    class FeatureTable
    {
        DataSet dataSet = new DataSet();
        DataTable dataTable=new DataTable();
        public FeatureTable(IFeatureLayer pFeatureLayer){
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            ITable pFeatureTable = pFeatureClass as ITable;
            for (int i = 0; i < pFeatureTable.Fields.FieldCount; i++)
            {
                dataTable.Columns.Add(pFeatureTable.Fields.Field[i].Name);
            }
            int pFeatureCount = pFeatureClass.Fields.FieldCount;
            ICursor pFeatureCursor = pFeatureTable.Search(null, false);
            IRow pRow = pFeatureCursor.NextRow();
            while (pRow != null)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < pFeatureCount; i++)
                {
                    dataRow[pRow.Fields.Field[i].Name]=pRow.get_Value(i).ToString();
                }
                dataTable.Rows.Add(dataRow);
                pRow = pFeatureCursor.NextRow();
            }
            dataSet.Tables.Add(dataTable);
        }
        public DataSet get_dataSet(){
            return dataSet;
        }
        public DataTable get_dataTable()
        {
            return dataTable;
        }
       
    }
}
