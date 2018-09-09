using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXApplication4
{
    public partial class Progressing : DevExpress.XtraEditors.XtraForm
    {
        int TotalTime = 0;
        public Action<int, int> setMinMax;
        public Action<int> setProcessingValue;
        public Action<String> setInfo1;
        public Action<String> setInfo2;
        public Action<IWin32Window> popup;
        public Progressing()
        {
            InitializeComponent();
            labelControl2.Text = "";
            labelControl3.Text = "";
            setMinMax += (min, max) =>
            {
                progressBarControl1.Properties.Minimum = min;
                progressBarControl1.Properties.Maximum = max;
            };
            setProcessingValue += (value) =>
            {
                progressBarControl1.EditValue = value;
            };
            setInfo1 += (value) =>
            {
                labelControl2.Text = value;
            };
            setInfo1 += (value) =>
            {
                labelControl3.Text = value;
            };
            textEdit1.Text = "00:00:00";
            textEdit1.GotFocus += (hander1, e1) =>
            {
                this.Focus();
            };
            popup += (form) =>
            {
                this.ShowDialog(form);

            };
            timer1.Start();
        }
        private new void ShowDialog(IWin32Window form) {
            base.ShowDialog(form);
            this.Owner = form as Form;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int hour = TotalTime / 3600;
            int minute = (TotalTime % 3600) / 60;
            int second = (TotalTime % 3600) % 60;
            textEdit1.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            TotalTime++;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Progressing_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}