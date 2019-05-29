namespace NavisElectronics.ListOfCooperation
{
    partial class MaterialsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialsView));
            TenTec.Windows.iGridLib.iGColPattern iGColPattern1 = new TenTec.Windows.iGridLib.iGColPattern();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern2 = new TenTec.Windows.iGridLib.iGColPattern();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern3 = new TenTec.Windows.iGridLib.iGColPattern();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern4 = new TenTec.Windows.iGridLib.iGColPattern();
            this.iGrid1Col0CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col0ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1Col1CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col1ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1Col2CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col2ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1Col3CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col3ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid1DefaultCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1DefaultColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1RowTextColCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // iGrid1Col3CellStyle
            // 
            this.iGrid1Col3CellStyle.FormatString = "{0:F3}";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveButton,
            this.toolStripSeparator1,
            this.checkButton,
            this.toolStripComboBox1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(648, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = global::NavisElectronics.ListOfCooperation.Properties.Resources.if_stock_save_20659;
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 22);
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // checkButton
            // 
            this.checkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.checkButton.Enabled = false;
            this.checkButton.Image = global::NavisElectronics.ListOfCooperation.Properties.Resources.if_stock_check_filled_21448;
            this.checkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(23, 22);
            this.checkButton.Text = "Проверить, что всё готово";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Найти";
            // 
            // iGrid1
            // 
            iGColPattern1.CellStyle = this.iGrid1Col0CellStyle;
            iGColPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
            iGColPattern1.Text = "Id";
            iGColPattern2.CellStyle = this.iGrid1Col1CellStyle;
            iGColPattern2.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
            iGColPattern2.Text = "Наименование";
            iGColPattern2.Width = 374;
            iGColPattern3.CellStyle = this.iGrid1Col2CellStyle;
            iGColPattern3.ColHdrStyle = this.iGrid1Col2ColHdrStyle;
            iGColPattern3.Text = "Ед. изм.";
            iGColPattern3.Width = 91;
            iGColPattern4.CellStyle = this.iGrid1Col3CellStyle;
            iGColPattern4.ColHdrStyle = this.iGrid1Col3ColHdrStyle;
            iGColPattern4.Text = "Всего";
            iGColPattern4.Width = 97;
            this.iGrid1.Cols.AddRange(new TenTec.Windows.iGridLib.iGColPattern[] {
            iGColPattern1,
            iGColPattern2,
            iGColPattern3,
            iGColPattern4});
            this.iGrid1.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
            this.iGrid1.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
            this.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid1.Location = new System.Drawing.Point(0, 25);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.ReadOnly = true;
            this.iGrid1.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
            this.iGrid1.Size = new System.Drawing.Size(648, 431);
            this.iGrid1.TabIndex = 2;
            this.iGrid1.CellDoubleClick += new TenTec.Windows.iGridLib.iGCellDoubleClickEventHandler(this.iGrid1_CellDoubleClick);
            // 
            // MaterialsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 456);
            this.Controls.Add(this.iGrid1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MaterialsView";
            this.Text = "MaterialsView";
            this.Load += new System.EventHandler(this.MaterialsView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton checkButton;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1DefaultCellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1DefaultColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1RowTextColCellStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col0CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col0ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col1CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col1ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col2CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col2ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col3CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col3ColHdrStyle;
    }
}