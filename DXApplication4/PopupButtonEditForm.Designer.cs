namespace DXApplication4
{
    partial class PopupButtonEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRun.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Appearance.Options.UseFont = true;
            this.btnRun.Location = new System.Drawing.Point(12, 239);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(103, 29);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "开始计算";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(450, 239);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(103, 29);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取消";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBoxControl1.Location = new System.Drawing.Point(185, 147);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(368, 95);
            this.listBoxControl1.TabIndex = 8;
            this.listBoxControl1.SelectedValueChanged += new System.EventHandler(this.listBoxControl1_SelectedValueChanged);
            // 
            // PopupButtonEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 285);
            this.Controls.Add(this.listBoxControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnRun);
            this.LookAndFeel.SkinName = "Caramel";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "PopupButtonEditForm";
            this.Text = "IDW";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        public DevExpress.XtraEditors.ListBoxControl listBoxControl1;
    }
}