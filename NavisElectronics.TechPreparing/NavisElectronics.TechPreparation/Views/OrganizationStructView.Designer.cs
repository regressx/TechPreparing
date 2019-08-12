namespace NavisElectronics.TechPreparation.Views
{
    partial class OrganizationStructView
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
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.applyButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeColumnName = new Aga.Controls.Tree.TreeColumn();
            this.treeColumnWorkshop = new Aga.Controls.Tree.TreeColumn();
            this.treeColumnPart = new Aga.Controls.Tree.TreeColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 19;
            this.treeViewAdv1.Columns.Add(this.treeColumnName);
            this.treeViewAdv1.Columns.Add(this.treeColumnWorkshop);
            this.treeViewAdv1.Columns.Add(this.treeColumnPart);
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(3, 3);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(463, 340);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.applyButton.Location = new System.Drawing.Point(163, 349);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(142, 25);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Принять";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeViewAdv1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.applyButton, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 377);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // treeColumnName
            // 
            this.treeColumnName.Header = "Наименование";
            this.treeColumnName.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumnName.TooltipText = null;
            this.treeColumnName.Width = 200;
            // 
            // treeColumnWorkshop
            // 
            this.treeColumnWorkshop.Header = "Цех";
            this.treeColumnWorkshop.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumnWorkshop.TooltipText = null;
            this.treeColumnWorkshop.Width = 75;
            // 
            // treeColumnPart
            // 
            this.treeColumnPart.Header = "Участок";
            this.treeColumnPart.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumnPart.TooltipText = null;
            this.treeColumnPart.Width = 75;
            // 
            // OrganizationStructView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 377);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrganizationStructView";
            this.Text = "OrganizationStructView";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Aga.Controls.Tree.TreeColumn treeColumnName;
        private Aga.Controls.Tree.TreeColumn treeColumnWorkshop;
        private Aga.Controls.Tree.TreeColumn treeColumnPart;
    }
}