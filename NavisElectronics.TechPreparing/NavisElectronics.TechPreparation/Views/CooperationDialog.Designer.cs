namespace NavisElectronics.TechPreparation.Views
{
    partial class CooperationDialog
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
            this.components = new System.ComponentModel.Container();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern1 = new TenTec.Windows.iGridLib.iGColPattern();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern2 = new TenTec.Windows.iGridLib.iGColPattern();
            TenTec.Windows.iGridLib.iGColPattern iGColPattern3 = new TenTec.Windows.iGridLib.iGColPattern();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid1Col0CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col0ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1Col1CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col1ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1Col2CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1Col2ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // iGrid1
            // 
            iGColPattern1.CellStyle = this.iGrid1Col0CellStyle;
            iGColPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
            iGColPattern1.Text = "Обозначение";
            iGColPattern1.Width = 100;
            iGColPattern2.CellStyle = this.iGrid1Col1CellStyle;
            iGColPattern2.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
            iGColPattern2.Text = "Наименование";
            iGColPattern2.Width = 200;
            iGColPattern3.CellStyle = this.iGrid1Col2CellStyle;
            iGColPattern3.ColHdrStyle = this.iGrid1Col2ColHdrStyle;
            iGColPattern3.Text = "Кооперация";
            iGColPattern3.Width = 75;
            this.iGrid1.Cols.AddRange(new TenTec.Windows.iGridLib.iGColPattern[] {
            iGColPattern1,
            iGColPattern2,
            iGColPattern3});
            this.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid1.Location = new System.Drawing.Point(0, 0);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.ReadOnly = true;
            this.iGrid1.RowMode = true;
            this.iGrid1.SelectionMode = TenTec.Windows.iGridLib.iGSelectionMode.MultiExtended;
            this.iGrid1.Size = new System.Drawing.Size(381, 365);
            this.iGrid1.TabIndex = 0;
            // 
            // iGrid1Col2CellStyle
            // 
            this.iGrid1Col2CellStyle.ImageAlign = TenTec.Windows.iGridLib.iGContentAlignment.MiddleCenter;
            this.iGrid1Col2CellStyle.Type = TenTec.Windows.iGridLib.iGCellType.Check;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // CooperationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 365);
            this.Controls.Add(this.iGrid1);
            this.Name = "CooperationDialog";
            this.Text = "CooperationDialog";
            this.Load += new System.EventHandler(this.CooperationDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col0CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col0ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col1CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col1ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1Col2CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1Col2ColHdrStyle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}