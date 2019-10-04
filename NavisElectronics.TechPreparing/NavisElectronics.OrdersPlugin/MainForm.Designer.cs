namespace NavisElectronics.Orders
{
    partial class MainForm
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
            this.designationColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountWithUseColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.designationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nameTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.amountTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.amountWithUseTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.saveORderButton = new System.Windows.Forms.ToolStripButton();
            this.saveInfoLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.noteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.noteTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 25;
            this.treeViewAdv1.Columns.Add(this.designationColumn);
            this.treeViewAdv1.Columns.Add(this.nameColumn);
            this.treeViewAdv1.Columns.Add(this.amountColumn);
            this.treeViewAdv1.Columns.Add(this.amountWithUseColumn);
            this.treeViewAdv1.Columns.Add(this.noteTreeColumn);
            this.treeViewAdv1.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(12, 38);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this.designationTextBox);
            this.treeViewAdv1.NodeControls.Add(this.nameTextBox);
            this.treeViewAdv1.NodeControls.Add(this.amountTextBox);
            this.treeViewAdv1.NodeControls.Add(this.amountWithUseTextBox);
            this.treeViewAdv1.NodeControls.Add(this.noteTextBox);
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(931, 327);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv";
            this.treeViewAdv1.UseColumns = true;
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
            // amountColumn
            // 
            this.amountColumn.Header = "Кол-во по СП";
            this.amountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountColumn.TooltipText = null;
            this.amountColumn.Width = 75;
            // 
            // amountWithUseColumn
            // 
            this.amountWithUseColumn.Header = "Кол-во с прим.";
            this.amountWithUseColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountWithUseColumn.TooltipText = null;
            this.amountWithUseColumn.Width = 75;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(246, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuItem1.Text = "Добавить новый узел";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuItem2.Text = "Заменить";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuItem3.Text = "Не изготавливать";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuItem4.Text = "Создать отчет по составу заказа";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem5.Text = "в IPS";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem6.Text = "в Excel";
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
            // amountTextBox
            // 
            this.amountTextBox.DataPropertyName = "Amount";
            this.amountTextBox.IncrementalSearchEnabled = true;
            this.amountTextBox.LeftMargin = 3;
            this.amountTextBox.ParentColumn = this.amountColumn;
            // 
            // amountWithUseTextBox
            // 
            this.amountWithUseTextBox.DataPropertyName = "AmountWithUse";
            this.amountWithUseTextBox.IncrementalSearchEnabled = true;
            this.amountWithUseTextBox.LeftMargin = 3;
            this.amountWithUseTextBox.ParentColumn = this.amountWithUseColumn;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveORderButton,
            this.saveInfoLabel,
            this.toolStripProgressBar1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(955, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // saveORderButton
            // 
            this.saveORderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveORderButton.Image = global::NavisElectronics.Orders.Properties.Resources.if_stock_save_20659;
            this.saveORderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveORderButton.Name = "saveORderButton";
            this.saveORderButton.Size = new System.Drawing.Size(23, 22);
            this.saveORderButton.Text = "Сохранить";
            // 
            // saveInfoLabel
            // 
            this.saveInfoLabel.Name = "saveInfoLabel";
            this.saveInfoLabel.Size = new System.Drawing.Size(76, 22);
            this.saveInfoLabel.Text = "Не сохранено";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            // 
            // noteTreeColumn
            // 
            this.noteTreeColumn.Header = "Примечание";
            this.noteTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.noteTreeColumn.TooltipText = null;
            this.noteTreeColumn.Width = 200;
            // 
            // noteTextBox
            // 
            this.noteTextBox.DataPropertyName = "Note";
            this.noteTextBox.IncrementalSearchEnabled = true;
            this.noteTextBox.LeftMargin = 3;
            this.noteTextBox.ParentColumn = this.noteTreeColumn;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 377);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.treeViewAdv1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private Aga.Controls.Tree.TreeColumn designationColumn;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn amountColumn;
        private Aga.Controls.Tree.TreeColumn amountWithUseColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton saveORderButton;
        private System.Windows.Forms.ToolStripLabel saveInfoLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox designationTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nameTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox amountTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox amountWithUseTextBox;
        private Aga.Controls.Tree.TreeColumn noteTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox noteTextBox;
    }
}