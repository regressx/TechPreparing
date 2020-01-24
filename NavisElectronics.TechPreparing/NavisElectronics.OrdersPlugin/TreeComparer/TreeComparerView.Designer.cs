using NavisElectronics.Orders.TreeComparer;

namespace NavisElectronics.Orders.Views
{
    public partial class TreeComparerView
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
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.designationColumn = new Aga.Controls.Tree.TreeColumn();
            this.changeNumberColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountColumn = new Aga.Controls.Tree.TreeColumn();
            this.relationTypeColumn = new Aga.Controls.Tree.TreeColumn();
            this.leftTreeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.designationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.changeNumberTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.amounTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.relationTypeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.treeViewAdv2 = new Aga.Controls.Tree.TreeViewAdv();
            this.newNameColumn = new Aga.Controls.Tree.TreeColumn();
            this.newDesignationColumn = new Aga.Controls.Tree.TreeColumn();
            this.newChangeNumberColumn = new Aga.Controls.Tree.TreeColumn();
            this.newAmountColumn = new Aga.Controls.Tree.TreeColumn();
            this.newRelationColumn = new Aga.Controls.Tree.TreeColumn();
            this.rightTreecontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pushChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newNameTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.newDesignationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.newChangeNumberTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.newAmountTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.newRelationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.downloadTreeButton = new System.Windows.Forms.ToolStripButton();
            this.compareTreeButton = new System.Windows.Forms.ToolStripButton();
            this.leftTreeContextMenuStrip.SuspendLayout();
            this.rightTreecontextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 19;
            this.treeViewAdv1.Columns.Add(this.nameColumn);
            this.treeViewAdv1.Columns.Add(this.designationColumn);
            this.treeViewAdv1.Columns.Add(this.changeNumberColumn);
            this.treeViewAdv1.Columns.Add(this.amountColumn);
            this.treeViewAdv1.Columns.Add(this.relationTypeColumn);
            this.treeViewAdv1.ContextMenuStrip = this.leftTreeContextMenuStrip;
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this.nameTextBox);
            this.treeViewAdv1.NodeControls.Add(this.designationTextBox);
            this.treeViewAdv1.NodeControls.Add(this.changeNumberTextBox);
            this.treeViewAdv1.NodeControls.Add(this.amounTextBox);
            this.treeViewAdv1.NodeControls.Add(this.relationTypeTextBox);
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(726, 494);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            this.treeViewAdv1.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.treeViewAdv1_RowDraw);
            this.treeViewAdv1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewAdv_KeyDown);
            // 
            // nameColumn
            // 
            this.nameColumn.Header = "Наименование";
            this.nameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.nameColumn.TooltipText = null;
            this.nameColumn.Width = 200;
            // 
            // designationColumn
            // 
            this.designationColumn.Header = "Обозначение";
            this.designationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.designationColumn.TooltipText = null;
            this.designationColumn.Width = 200;
            // 
            // changeNumberColumn
            // 
            this.changeNumberColumn.Header = "Изм.";
            this.changeNumberColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.changeNumberColumn.TooltipText = null;
            // 
            // amountColumn
            // 
            this.amountColumn.Header = "Кол-во";
            this.amountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountColumn.TooltipText = null;
            // 
            // relationTypeColumn
            // 
            this.relationTypeColumn.Header = "Связь";
            this.relationTypeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.relationTypeColumn.TooltipText = null;
            // 
            // leftTreeContextMenuStrip
            // 
            this.leftTreeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteNodeMenuItem});
            this.leftTreeContextMenuStrip.Name = "leftTreeContextMenuStrip";
            this.leftTreeContextMenuStrip.Size = new System.Drawing.Size(145, 26);
            // 
            // deleteNodeMenuItem
            // 
            this.deleteNodeMenuItem.Name = "deleteNodeMenuItem";
            this.deleteNodeMenuItem.Size = new System.Drawing.Size(144, 22);
            this.deleteNodeMenuItem.Text = "Удалить узел";
            this.deleteNodeMenuItem.Click += new System.EventHandler(this.DeleteNodeMenuItem_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataPropertyName = "Name";
            this.nameTextBox.IncrementalSearchEnabled = true;
            this.nameTextBox.LeftMargin = 3;
            this.nameTextBox.ParentColumn = this.nameColumn;
            // 
            // designationTextBox
            // 
            this.designationTextBox.DataPropertyName = "Designation";
            this.designationTextBox.IncrementalSearchEnabled = true;
            this.designationTextBox.LeftMargin = 3;
            this.designationTextBox.ParentColumn = this.designationColumn;
            // 
            // changeNumberTextBox
            // 
            this.changeNumberTextBox.DataPropertyName = "ChangeNumber";
            this.changeNumberTextBox.IncrementalSearchEnabled = true;
            this.changeNumberTextBox.LeftMargin = 3;
            this.changeNumberTextBox.ParentColumn = this.changeNumberColumn;
            // 
            // amounTextBox
            // 
            this.amounTextBox.DataPropertyName = "Amount";
            this.amounTextBox.IncrementalSearchEnabled = true;
            this.amounTextBox.LeftMargin = 3;
            this.amounTextBox.ParentColumn = this.amountColumn;
            // 
            // relationTypeTextBox
            // 
            this.relationTypeTextBox.DataPropertyName = "RelationType";
            this.relationTypeTextBox.IncrementalSearchEnabled = true;
            this.relationTypeTextBox.LeftMargin = 3;
            this.relationTypeTextBox.ParentColumn = this.relationTypeColumn;
            // 
            // treeViewAdv2
            // 
            this.treeViewAdv2.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv2.ColumnHeaderHeight = 19;
            this.treeViewAdv2.Columns.Add(this.newNameColumn);
            this.treeViewAdv2.Columns.Add(this.newDesignationColumn);
            this.treeViewAdv2.Columns.Add(this.newChangeNumberColumn);
            this.treeViewAdv2.Columns.Add(this.newAmountColumn);
            this.treeViewAdv2.Columns.Add(this.newRelationColumn);
            this.treeViewAdv2.ContextMenuStrip = this.rightTreecontextMenuStrip;
            this.treeViewAdv2.DefaultToolTipProvider = null;
            this.treeViewAdv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv2.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv2.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv2.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv2.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv2.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv2.Model = null;
            this.treeViewAdv2.Name = "treeViewAdv2";
            this.treeViewAdv2.NodeControls.Add(this.newNameTextBox);
            this.treeViewAdv2.NodeControls.Add(this.newDesignationTextBox);
            this.treeViewAdv2.NodeControls.Add(this.newChangeNumberTextBox);
            this.treeViewAdv2.NodeControls.Add(this.newAmountTextBox);
            this.treeViewAdv2.NodeControls.Add(this.newRelationTextBox);
            this.treeViewAdv2.NodeFilter = null;
            this.treeViewAdv2.SelectedNode = null;
            this.treeViewAdv2.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv2.Size = new System.Drawing.Size(725, 494);
            this.treeViewAdv2.TabIndex = 1;
            this.treeViewAdv2.Text = "treeViewAdv2";
            this.treeViewAdv2.UseColumns = true;
            this.treeViewAdv2.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.treeViewAdv2_RowDraw);
            this.treeViewAdv2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewAdv_KeyDown);
            // 
            // newNameColumn
            // 
            this.newNameColumn.Header = "Наименование";
            this.newNameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.newNameColumn.TooltipText = null;
            this.newNameColumn.Width = 200;
            // 
            // newDesignationColumn
            // 
            this.newDesignationColumn.Header = "Обозначение";
            this.newDesignationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.newDesignationColumn.TooltipText = null;
            this.newDesignationColumn.Width = 200;
            // 
            // newChangeNumberColumn
            // 
            this.newChangeNumberColumn.Header = "Изм.";
            this.newChangeNumberColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.newChangeNumberColumn.TooltipText = null;
            // 
            // newAmountColumn
            // 
            this.newAmountColumn.Header = "Кол-во";
            this.newAmountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.newAmountColumn.TooltipText = null;
            // 
            // newRelationColumn
            // 
            this.newRelationColumn.Header = "Связь";
            this.newRelationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.newRelationColumn.TooltipText = null;
            // 
            // rightTreecontextMenuStrip
            // 
            this.rightTreecontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pushChangesMenuItem});
            this.rightTreecontextMenuStrip.Name = "contextMenuStrip";
            this.rightTreecontextMenuStrip.Size = new System.Drawing.Size(342, 26);
            // 
            // pushChangesMenuItem
            // 
            this.pushChangesMenuItem.Name = "pushChangesMenuItem";
            this.pushChangesMenuItem.Size = new System.Drawing.Size(341, 22);
            this.pushChangesMenuItem.Text = "Отправить выделенные изменения в старое дерево";
            this.pushChangesMenuItem.Click += new System.EventHandler(this.PushChangesMenuItem_Click);
            // 
            // newNameTextBox
            // 
            this.newNameTextBox.DataPropertyName = "Name";
            this.newNameTextBox.IncrementalSearchEnabled = true;
            this.newNameTextBox.LeftMargin = 3;
            this.newNameTextBox.ParentColumn = this.newNameColumn;
            // 
            // newDesignationTextBox
            // 
            this.newDesignationTextBox.DataPropertyName = "Designation";
            this.newDesignationTextBox.IncrementalSearchEnabled = true;
            this.newDesignationTextBox.LeftMargin = 3;
            this.newDesignationTextBox.ParentColumn = this.newDesignationColumn;
            // 
            // newChangeNumberTextBox
            // 
            this.newChangeNumberTextBox.DataPropertyName = "ChangeNumber";
            this.newChangeNumberTextBox.IncrementalSearchEnabled = true;
            this.newChangeNumberTextBox.LeftMargin = 3;
            this.newChangeNumberTextBox.ParentColumn = this.newChangeNumberColumn;
            // 
            // newAmountTextBox
            // 
            this.newAmountTextBox.DataPropertyName = "Amount";
            this.newAmountTextBox.IncrementalSearchEnabled = true;
            this.newAmountTextBox.LeftMargin = 3;
            this.newAmountTextBox.ParentColumn = this.newAmountColumn;
            // 
            // newRelationTextBox
            // 
            this.newRelationTextBox.DataPropertyName = "RelationType";
            this.newRelationTextBox.IncrementalSearchEnabled = true;
            this.newRelationTextBox.LeftMargin = 3;
            this.newRelationTextBox.ParentColumn = this.newRelationColumn;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip.Location = new System.Drawing.Point(0, 519);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1455, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(125, 17);
            this.toolStripStatusLabel1.Text = "Неизмененные позиции";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel2.Text = "Удаленные позиции";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel3.Text = "Добавленные позиции";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(113, 17);
            this.toolStripStatusLabel4.Text = "Измененные позиции";
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadTreeButton,
            this.compareTreeButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1455, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAdv1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeViewAdv2);
            this.splitContainer1.Size = new System.Drawing.Size(1455, 494);
            this.splitContainer1.SplitterDistance = 726;
            this.splitContainer1.TabIndex = 5;
            // 
            // downloadTreeButton
            // 
            this.downloadTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downloadTreeButton.Image = global::NavisElectronics.Orders.Properties.Resources.Download;
            this.downloadTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadTreeButton.Name = "downloadTreeButton";
            this.downloadTreeButton.Size = new System.Drawing.Size(23, 22);
            this.downloadTreeButton.Text = "Загрузить дерево из базы данных";
            this.downloadTreeButton.Click += new System.EventHandler(this.downloadTreeButton_Click);
            // 
            // compareTreeButton
            // 
            this.compareTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compareTreeButton.Image = global::NavisElectronics.Orders.Properties.Resources.icons8_forest_16;
            this.compareTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compareTreeButton.Name = "compareTreeButton";
            this.compareTreeButton.Size = new System.Drawing.Size(23, 22);
            this.compareTreeButton.Text = "Сравнить два дерева";
            this.compareTreeButton.Click += new System.EventHandler(this.compareTreeButton_Click);
            // 
            // TreeComparerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 541);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "TreeComparerView";
            this.Text = "TreeComparerView";
            this.leftTreeContextMenuStrip.ResumeLayout(false);
            this.rightTreecontextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton downloadTreeButton;
        private System.Windows.Forms.ToolStripButton compareTreeButton;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn designationColumn;
        private Aga.Controls.Tree.TreeColumn changeNumberColumn;
        private Aga.Controls.Tree.TreeColumn amountColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nameTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox designationTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox changeNumberTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox amounTextBox;
        private Aga.Controls.Tree.TreeColumn newNameColumn;
        private Aga.Controls.Tree.TreeColumn newDesignationColumn;
        private Aga.Controls.Tree.TreeColumn newChangeNumberColumn;
        private Aga.Controls.Tree.TreeColumn newAmountColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox newNameTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox newDesignationTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox newChangeNumberTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox newAmountTextBox;
        private System.Windows.Forms.ContextMenuStrip rightTreecontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem pushChangesMenuItem;
        private System.Windows.Forms.ContextMenuStrip leftTreeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeMenuItem;
        private Aga.Controls.Tree.TreeColumn relationTypeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox relationTypeTextBox;
        private Aga.Controls.Tree.TreeColumn newRelationColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox newRelationTextBox;
    }
}