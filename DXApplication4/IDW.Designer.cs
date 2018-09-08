namespace DXApplication4
{
    partial class IDW
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
        private void InitializeComponent()
        {
            this.tBSavePath = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tBRefPath = new DXApplication4.PopupButtonEditForm.RasterButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tBPointPath = new DXApplication4.PopupButtonEditForm.FeatureButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBSavePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBRefPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBPointPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tBSavePath
            // 
            this.tBSavePath.Location = new System.Drawing.Point(185, 117);
            this.tBSavePath.Name = "tBSavePath";
            this.tBSavePath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBSavePath.Properties.Appearance.Options.UseFont = true;
            this.tBSavePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tBSavePath.Size = new System.Drawing.Size(368, 24);
            this.tBSavePath.TabIndex = 26;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 120);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(105, 18);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "插值结果输出：";
            // 
            // tBRefPath
            // 
            this.tBRefPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.tBRefPath.Location = new System.Drawing.Point(185, 53);
            this.tBRefPath.Name = "tBRefPath";
            this.tBRefPath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBRefPath.Properties.Appearance.Options.UseFont = true;
            this.tBRefPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tBRefPath.Size = new System.Drawing.Size(368, 24);
            this.tBRefPath.TabIndex = 24;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(105, 18);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "产靠栅格输入：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 18);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "插值点输入：";
            // 
            // tBPointPath
            // 
            this.tBPointPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.tBPointPath.Location = new System.Drawing.Point(186, 9);
            this.tBPointPath.Name = "tBPointPath";
            this.tBPointPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tBPointPath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBPointPath.Properties.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.tBPointPath.Properties.Appearance.Options.UseBackColor = true;
            this.tBPointPath.Properties.Appearance.Options.UseFont = true;
            this.tBPointPath.Properties.Appearance.Options.UseForeColor = true;
            this.tBPointPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tBPointPath.Size = new System.Drawing.Size(368, 24);
            this.tBPointPath.TabIndex = 27;
            // 
            // IDW
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 285);
            this.Controls.Add(this.tBPointPath);
            this.Controls.Add(this.tBSavePath);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.tBRefPath);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.LookAndFeel.SkinName = "Caramel";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "IDW";
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.tBRefPath, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.tBSavePath, 0);
            this.Controls.SetChildIndex(this.listBoxControl1, 0);
            this.Controls.SetChildIndex(this.tBPointPath, 0);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBSavePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBRefPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBPointPath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.ButtonEdit tBSavePath;
        public RasterButtonEdit tBRefPath;
        public FeatureButtonEdit tBPointPath;
    }
}