namespace NavisElectronics.ListOfCooperation.Views
{
    partial class TreeNodeDialogView
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
            this.idColumn = new Aga.Controls.Tree.TreeColumn();
            this.designationColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.idTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.designationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nameTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.acceptButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 17;
            this.treeViewAdv1.Columns.Add(this.idColumn);
            this.treeViewAdv1.Columns.Add(this.designationColumn);
            this.treeViewAdv1.Columns.Add(this.nameColumn);
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(3, 3);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this.idTextBox);
            this.treeViewAdv1.NodeControls.Add(this.designationTextBox);
            this.treeViewAdv1.NodeControls.Add(this.nameTextBox);
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(520, 377);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            // 
            // idColumn
            // 
            this.idColumn.Header = "Id";
            this.idColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.idColumn.TooltipText = null;
            this.idColumn.Width = 75;
            // 
            // designationColumn
            // 
            this.designationColumn.Header = "Обозначение";
            this.designationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.designationColumn.TooltipText = null;
            this.designationColumn.Width = 150;
            // 
            // nameColumn
            // 
            this.nameColumn.Header = "Наименование";
            this.nameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.nameColumn.TooltipText = null;
            this.nameColumn.Width = 250;
            // 
            // idTextBox
            // 
            this.idTextBox.DataPropertyName = "Id";
            this.idTextBox.IncrementalSearchEnabled = true;
            this.idTextBox.LeftMargin = 3;
            this.idTextBox.ParentColumn = this.idColumn;
            // 
            // designationTextBox
            // 
            this.designationTextBox.DataPropertyName = "Designation";
            this.designationTextBox.IncrementalSearchEnabled = true;
            this.designationTextBox.LeftMargin = 3;
            this.designationTextBox.ParentColumn = this.designationColumn;
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataPropertyName = "Name";
            this.nameTextBox.IncrementalSearchEnabled = true;
            this.nameTextBox.LeftMargin = 3;
            this.nameTextBox.ParentColumn = this.nameColumn;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.acceptButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeViewAdv1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 418);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(225, 386);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 29);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "ОК";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // TreeNodeDialogView
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 418);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TreeNodeDialogView";
            this.Text = "Выберите узел для копирования данных";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private Aga.Controls.Tree.TreeColumn idColumn;
        private Aga.Controls.Tree.TreeColumn designationColumn;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox idTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox designationTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button acceptButton;
    }
}