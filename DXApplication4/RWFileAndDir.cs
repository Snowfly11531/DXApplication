using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication4
{
    public class OSFileAndDir
    {
        public static String OpenFile(String Title,String type)
        {
            String path="";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Title;
            openFileDialog.Filter = type;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }
        public static String SaveFile(String Title, String type)
        {
            String path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = Title;
            saveFileDialog.Filter = type;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }
            return path;
        }
    }
}
