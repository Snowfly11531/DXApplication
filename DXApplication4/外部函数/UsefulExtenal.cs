using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace DXApplication4.外部函数
{
    public static class UsefulExtenal
    {
        public static void RemoveAll(this ListBoxItemCollection items) {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                items.RemoveAt(i);
            }
        }

    }
}
