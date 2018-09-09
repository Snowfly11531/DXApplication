using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading.Tasks;

namespace DXApplication4
{
    public partial class IDW : PopupButtonEditForm
    {
        Progressing progressing;
        public IDW()
        {
            InitializeComponent();
            btnRun.Click += btnRun_Click;
        }
        public void btnRun_Click(object hander, EventArgs e) {
            progressing = new Progressing();
            new Task(() =>
            {
                this.BeginInvoke(progressing.popup,new object[]{this});
            }).Start();
        }
    }
}