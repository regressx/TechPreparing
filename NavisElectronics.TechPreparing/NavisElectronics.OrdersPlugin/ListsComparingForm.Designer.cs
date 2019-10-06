namespace NavisElectronics.Orders
{
    partial class ListsComparingForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid1DefaultCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1DefaultColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1RowTextColCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid2 = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid3 = new TenTec.Windows.iGridLib.iGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid3)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.iGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.iGrid2);
            this.splitContainer1.Size = new System.Drawing.Size(653, 196);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.iGrid3);
            this.splitContainer2.Size = new System.Drawing.Size(653, 327);
            this.splitContainer2.SplitterDistance = 196;
            this.splitContainer2.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 426);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // iGrid1
            // 
            this.iGrid1.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
            this.iGrid1.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
            this.iGrid1.Header.Height = 19;
            this.iGrid1.Location = new System.Drawing.Point(18, 3);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
            this.iGrid1.Size = new System.Drawing.Size(200, 160);
            this.iGrid1.TabIndex = 0;
            // 
            // iGrid2
            // 
            this.iGrid2.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
            this.iGrid2.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
            this.iGrid2.Header.Height = 19;
            this.iGrid2.Location = new System.Drawing.Point(52, 3);
            this.iGrid2.Name = "iGrid2";
            this.iGrid2.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
            this.iGrid2.Size = new System.Drawing.Size(200, 160);
            this.iGrid2.TabIndex = 1;
            // 
            // iGrid3
            // 
            this.iGrid3.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
            this.iGrid3.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
            this.iGrid3.Header.Height = 19;
            this.iGrid3.Location = new System.Drawing.Point(3, 16);
            this.iGrid3.Name = "iGrid3";
            this.iGrid3.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
            this.iGrid3.Size = new System.Drawing.Size(647, 94);
            this.iGrid3.TabIndex = 2;
            // 
            // ListsComparingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ListsComparingForm";
            this.Text = "ListsComparingForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1DefaultCellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1DefaultColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1RowTextColCellStyle;
        private TenTec.Windows.iGridLib.iGrid iGrid2;
        private TenTec.Windows.iGridLib.iGrid iGrid3;
    }
}