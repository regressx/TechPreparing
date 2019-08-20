namespace NavisElectronics.TechPreparation.Views
{
    partial class MainView
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
            this.iGridCol0CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGridCol0ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGridCol1CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGridCol1ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearCooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.refreshTreeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.LoadTechPreparationButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cooperationButton = new System.Windows.Forms.ToolStripButton();
            this.TechRoutesEditButton = new System.Windows.Forms.ToolStripButton();
            this.MainMaterialsButton = new System.Windows.Forms.ToolStripButton();
            this.standartsButton = new System.Windows.Forms.ToolStripButton();
            this.SetTechWithdrawalButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UpdateButton = new System.Windows.Forms.ToolStripButton();
            this.CheckReadyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.designationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.amounTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountWithUseTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nodeTextBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAmountWithUse = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.iGrid = new TenTec.Windows.iGridLib.iGrid();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // iGridCol1CellStyle
            // 
            this.iGridCol1CellStyle.Flags = TenTec.Windows.iGridLib.iGCellFlags.DisplayImage;
            this.iGridCol1CellStyle.ImageAlign = TenTec.Windows.iGridLib.iGContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearCooperationButton});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(190, 26);
            // 
            // ClearCooperationButton
            // 
            this.ClearCooperationButton.Name = "ClearCooperationButton";
            this.ClearCooperationButton.Size = new System.Drawing.Size(189, 22);
            this.ClearCooperationButton.Text = "Очистить кооперацию";
            this.ClearCooperationButton.Click += new System.EventHandler(this.ClearCooperationButton_Click);
            // 
            // textBoxNote
            // 
            this.textBoxNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNote.Location = new System.Drawing.Point(3, 53);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(1056, 122);
            this.textBoxNote.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Общие примечания к разделительной ведомости:";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshTreeButton,
            this.toolStripSeparator3,
            this.LoadTechPreparationButton,
            this.toolStripSeparator2,
            this.cooperationButton,
            this.TechRoutesEditButton,
            this.MainMaterialsButton,
            this.standartsButton,
            this.SetTechWithdrawalButton,
            this.toolStripSeparator1,
            this.UpdateButton,
            this.CheckReadyButton,
            this.toolStripSeparator4,
            this.saveButton,
            this.toolStripLabel1,
            this.toolStripProgressBar1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1062, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // refreshTreeButton
            // 
            this.refreshTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshTreeButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_repeat_16;
            this.refreshTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshTreeButton.Name = "refreshTreeButton";
            this.refreshTreeButton.Size = new System.Drawing.Size(23, 22);
            this.refreshTreeButton.Text = "Обновить дерево";
            this.refreshTreeButton.Click += new System.EventHandler(this.refreshTreeButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // LoadTechPreparationButton
            // 
            this.LoadTechPreparationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadTechPreparationButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_robot_16;
            this.LoadTechPreparationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadTechPreparationButton.Name = "LoadTechPreparationButton";
            this.LoadTechPreparationButton.Size = new System.Drawing.Size(23, 22);
            this.LoadTechPreparationButton.Text = "Загрузить тех. подготовку";
            this.LoadTechPreparationButton.Click += new System.EventHandler(this.LoadTechPreparationButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cooperationButton
            // 
            this.cooperationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cooperationButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_new_meeting_21476;
            this.cooperationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cooperationButton.Name = "cooperationButton";
            this.cooperationButton.Size = new System.Drawing.Size(23, 22);
            this.cooperationButton.Text = "Ведомость кооперации";
            // 
            // TechRoutesEditButton
            // 
            this.TechRoutesEditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TechRoutesEditButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.route_16;
            this.TechRoutesEditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TechRoutesEditButton.Name = "TechRoutesEditButton";
            this.TechRoutesEditButton.Size = new System.Drawing.Size(23, 22);
            this.TechRoutesEditButton.Text = "ВТМ";
            this.TechRoutesEditButton.Click += new System.EventHandler(this.TechRoutesEditButton_Click);
            // 
            // MainMaterialsButton
            // 
            this.MainMaterialsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainMaterialsButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.just_another_layers_16;
            this.MainMaterialsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainMaterialsButton.Name = "MainMaterialsButton";
            this.MainMaterialsButton.Size = new System.Drawing.Size(23, 22);
            this.MainMaterialsButton.Text = "Работа с основными материалами";
            this.MainMaterialsButton.Click += new System.EventHandler(this.MainMaterialsButton_Click);
            // 
            // standartsButton
            // 
            this.standartsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.standartsButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.iconfinder_Screw_bolts_3605318;
            this.standartsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.standartsButton.Name = "standartsButton";
            this.standartsButton.Size = new System.Drawing.Size(23, 22);
            this.standartsButton.Text = "Стандартные изделия";
            this.standartsButton.Click += new System.EventHandler(this.standartsButton_Click);
            // 
            // SetTechWithdrawalButton
            // 
            this.SetTechWithdrawalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetTechWithdrawalButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_full_trash_16;
            this.SetTechWithdrawalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetTechWithdrawalButton.Name = "SetTechWithdrawalButton";
            this.SetTechWithdrawalButton.Size = new System.Drawing.Size(23, 22);
            this.SetTechWithdrawalButton.Text = "Технологический отход";
            this.SetTechWithdrawalButton.Click += new System.EventHandler(this.SetTechWithdrawalButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // UpdateButton
            // 
            this.UpdateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UpdateButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_download_16;
            this.UpdateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(23, 22);
            this.UpdateButton.Text = "Загрузить новые данные из IPS";
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // CheckReadyButton
            // 
            this.CheckReadyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckReadyButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_ok_16;
            this.CheckReadyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckReadyButton.Name = "CheckReadyButton";
            this.CheckReadyButton.Size = new System.Drawing.Size(23, 22);
            this.CheckReadyButton.Text = "Проверить, что всё готово";
            this.CheckReadyButton.Click += new System.EventHandler(this.CheckReadyButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_save_20659;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Сохранить в базу";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 22);
            this.toolStripLabel1.Text = "Не сохранено";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            this.toolStripProgressBar1.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(1062, 476);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeViewAdv);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.iGrid);
            this.splitContainer2.Size = new System.Drawing.Size(1062, 294);
            this.splitContainer2.SplitterDistance = 817;
            this.splitContainer2.TabIndex = 7;
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ColumnHeaderHeight = 19;
            this.treeViewAdv.Columns.Add(this.designationTreeColumn);
            this.treeViewAdv.Columns.Add(this.nameTreeColumn);
            this.treeViewAdv.Columns.Add(this.amounTreeColumn);
            this.treeViewAdv.Columns.Add(this.amountWithUseTreeColumn);
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxDesignation);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxName);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxAmount);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxAmountWithUse);
            this.treeViewAdv.NodeFilter = null;
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(817, 294);
            this.treeViewAdv.TabIndex = 0;
            this.treeViewAdv.Text = "treeViewAdv";
            this.treeViewAdv.UseColumns = true;
            this.treeViewAdv.NodeMouseClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.treeViewAdv_NodeMouseClick);
            // 
            // designationTreeColumn
            // 
            this.designationTreeColumn.Header = "Обозначение";
            this.designationTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.designationTreeColumn.TooltipText = null;
            this.designationTreeColumn.Width = 150;
            // 
            // nameTreeColumn
            // 
            this.nameTreeColumn.Header = "Наименование";
            this.nameTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.nameTreeColumn.TooltipText = null;
            this.nameTreeColumn.Width = 200;
            // 
            // amounTreeColumn
            // 
            this.amounTreeColumn.Header = "Кол-во";
            this.amounTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amounTreeColumn.TooltipText = null;
            // 
            // amountWithUseTreeColumn
            // 
            this.amountWithUseTreeColumn.Header = "Кол-во с прим";
            this.amountWithUseTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountWithUseTreeColumn.TooltipText = null;
            this.amountWithUseTreeColumn.Width = 90;
            // 
            // nodeTextBoxDesignation
            // 
            this.nodeTextBoxDesignation.DataPropertyName = "Designation";
            this.nodeTextBoxDesignation.IncrementalSearchEnabled = true;
            this.nodeTextBoxDesignation.LeftMargin = 3;
            this.nodeTextBoxDesignation.ParentColumn = this.designationTreeColumn;
            // 
            // nodeTextBoxName
            // 
            this.nodeTextBoxName.DataPropertyName = "Name";
            this.nodeTextBoxName.IncrementalSearchEnabled = true;
            this.nodeTextBoxName.LeftMargin = 3;
            this.nodeTextBoxName.ParentColumn = this.nameTreeColumn;
            // 
            // nodeTextBoxAmount
            // 
            this.nodeTextBoxAmount.DataPropertyName = "Amount";
            this.nodeTextBoxAmount.IncrementalSearchEnabled = true;
            this.nodeTextBoxAmount.LeftMargin = 3;
            this.nodeTextBoxAmount.ParentColumn = this.amounTreeColumn;
            // 
            // nodeTextBoxAmountWithUse
            // 
            this.nodeTextBoxAmountWithUse.DataPropertyName = "AmountWithUse";
            this.nodeTextBoxAmountWithUse.IncrementalSearchEnabled = true;
            this.nodeTextBoxAmountWithUse.LeftMargin = 3;
            this.nodeTextBoxAmountWithUse.ParentColumn = this.amountWithUseTreeColumn;
            // 
            // iGrid
            // 
            iGColPattern1.CellStyle = this.iGridCol0CellStyle;
            iGColPattern1.ColHdrStyle = this.iGridCol0ColHdrStyle;
            iGColPattern1.Text = "Наименование";
            iGColPattern1.Width = 152;
            iGColPattern2.CellStyle = this.iGridCol1CellStyle;
            iGColPattern2.ColHdrStyle = this.iGridCol1ColHdrStyle;
            iGColPattern2.Text = "Статус";
            this.iGrid.Cols.AddRange(new TenTec.Windows.iGridLib.iGColPattern[] {
            iGColPattern1,
            iGColPattern2});
            this.iGrid.ContextMenuStrip = this.contextMenuStrip;
            this.iGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iGrid.Location = new System.Drawing.Point(0, 0);
            this.iGrid.Name = "iGrid";
            this.iGrid.ReadOnly = true;
            this.iGrid.Size = new System.Drawing.Size(241, 294);
            this.iGrid.TabIndex = 0;
            this.iGrid.CellDoubleClick += new TenTec.Windows.iGridLib.iGCellDoubleClickEventHandler(this.iGrid_CellDoubleClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxNote, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1062, 178);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 501);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainView";
            this.Text = "Редактор технологических ведомостей";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearCooperationButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ToolStripButton TechRoutesEditButton;
        private System.Windows.Forms.ToolStripButton MainMaterialsButton;
        private System.Windows.Forms.ToolStripButton standartsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton UpdateButton;
        private System.Windows.Forms.ToolStripButton LoadTechPreparationButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton SetTechWithdrawalButton;
        private System.Windows.Forms.ToolStripButton refreshTreeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton CheckReadyButton;
        private System.Windows.Forms.ToolStripButton cooperationButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private Aga.Controls.Tree.TreeColumn designationTreeColumn;
        private Aga.Controls.Tree.TreeColumn nameTreeColumn;
        private Aga.Controls.Tree.TreeColumn amounTreeColumn;
        private Aga.Controls.Tree.TreeColumn amountWithUseTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxDesignation;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxAmount;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxAmountWithUse;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private TenTec.Windows.iGridLib.iGrid iGrid;
        private TenTec.Windows.iGridLib.iGCellStyle iGridCol0CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGridCol0ColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGridCol1CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGridCol1ColHdrStyle;
    }
}