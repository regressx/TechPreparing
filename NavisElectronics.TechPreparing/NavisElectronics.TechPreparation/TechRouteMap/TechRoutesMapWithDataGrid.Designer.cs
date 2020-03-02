namespace NavisElectronics.TechPreparation.Views
{
    partial class TechRoutesMapWithDataGrid
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ExpandAllButton = new System.Windows.Forms.ToolStripButton();
            this.CollapseAllButton = new System.Windows.Forms.ToolStripButton();
            this.refreshTreeButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateFromIPSButton = new System.Windows.Forms.ToolStripMenuItem();
            this.goToArchiveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.produceButton = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotProduceButton = new System.Windows.Forms.ToolStripMenuItem();
            this.SetInnerCooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveInnerCooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.createReport = new System.Windows.Forms.ToolStripMenuItem();
            this.createDevideList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.createSingleCompleteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCooperationListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.designationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.iconColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.routeTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.innerCooperationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.substituteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.noteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.relationNoteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.stockRateColumn = new Aga.Controls.Tree.TreeColumn();
            this.sampleSizeColumn = new Aga.Controls.Tree.TreeColumn();
            this.tpRefColumn = new Aga.Controls.Tree.TreeColumn();
            this.techTaskTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.agentTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.textBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRoute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.checkBoxInnerCoop = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.textBoxNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxSubstitute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRelationNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxSampleSize = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxStockRate = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxTpRef = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxTechTask = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.agentTextbox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.createMaterialsListButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllButton,
            this.CollapseAllButton,
            this.refreshTreeButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1270, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // ExpandAllButton
            // 
            this.ExpandAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExpandAllButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_flow_chart_16;
            this.ExpandAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExpandAllButton.Name = "ExpandAllButton";
            this.ExpandAllButton.Size = new System.Drawing.Size(23, 22);
            this.ExpandAllButton.Text = "Раскрыть все узлы";
            this.ExpandAllButton.Click += new System.EventHandler(this.ExpandAllButton_Click);
            // 
            // CollapseAllButton
            // 
            this.CollapseAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollapseAllButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_simple_tree_16;
            this.CollapseAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollapseAllButton.Name = "CollapseAllButton";
            this.CollapseAllButton.Size = new System.Drawing.Size(23, 22);
            this.CollapseAllButton.Text = "Свернуть все узлы";
            this.CollapseAllButton.Click += new System.EventHandler(this.CollapseAllButton_Click);
            // 
            // refreshTreeButton
            // 
            this.refreshTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshTreeButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_repeat_16;
            this.refreshTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshTreeButton.Name = "refreshTreeButton";
            this.refreshTreeButton.Size = new System.Drawing.Size(23, 22);
            this.refreshTreeButton.Text = "Обновить дерево";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateFromIPSButton,
            this.goToArchiveButton,
            this.toolStripSeparator1,
            this.produceButton,
            this.doNotProduceButton,
            this.SetInnerCooperationButton,
            this.RemoveInnerCooperationButton,
            this.toolStripSeparator3,
            this.EditNote,
            this.toolStripSeparator4,
            this.createReport,
            this.createDevideList,
            this.toolStripMenuItem2,
            this.createCooperationListMenuItem,
            this.createMaterialsListButton});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(293, 308);
            // 
            // updateFromIPSButton
            // 
            this.updateFromIPSButton.Name = "updateFromIPSButton";
            this.updateFromIPSButton.Size = new System.Drawing.Size(292, 22);
            this.updateFromIPSButton.Text = "Обновить узел из IPS";
            this.updateFromIPSButton.Click += new System.EventHandler(this.updateFromIPSButton_Click);
            // 
            // goToArchiveButton
            // 
            this.goToArchiveButton.Name = "goToArchiveButton";
            this.goToArchiveButton.Size = new System.Drawing.Size(292, 22);
            this.goToArchiveButton.Text = "Перейти к архиву предприятия";
            this.goToArchiveButton.Click += new System.EventHandler(this.goToArchiveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(289, 6);
            // 
            // produceButton
            // 
            this.produceButton.Name = "produceButton";
            this.produceButton.Size = new System.Drawing.Size(292, 22);
            this.produceButton.Text = "Изготавливать";
            this.produceButton.Click += new System.EventHandler(this.produceButton_Click);
            // 
            // doNotProduceButton
            // 
            this.doNotProduceButton.Name = "doNotProduceButton";
            this.doNotProduceButton.Size = new System.Drawing.Size(292, 22);
            this.doNotProduceButton.Text = "Не изготавливать";
            this.doNotProduceButton.Click += new System.EventHandler(this.doNotProduceButton_Click);
            // 
            // SetInnerCooperationButton
            // 
            this.SetInnerCooperationButton.Name = "SetInnerCooperationButton";
            this.SetInnerCooperationButton.Size = new System.Drawing.Size(292, 22);
            this.SetInnerCooperationButton.Text = "Отметить внутрипроизв. кооп.";
            this.SetInnerCooperationButton.Click += new System.EventHandler(this.SetInnerCooperationButton_Click);
            // 
            // RemoveInnerCooperationButton
            // 
            this.RemoveInnerCooperationButton.Name = "RemoveInnerCooperationButton";
            this.RemoveInnerCooperationButton.Size = new System.Drawing.Size(292, 22);
            this.RemoveInnerCooperationButton.Text = "Убрать внутрипроизв. кооп.";
            this.RemoveInnerCooperationButton.Click += new System.EventHandler(this.RemoveInnerCooperationButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(289, 6);
            // 
            // EditNote
            // 
            this.EditNote.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_insert_note_21825;
            this.EditNote.Name = "EditNote";
            this.EditNote.Size = new System.Drawing.Size(292, 22);
            this.EditNote.Text = "Добавить примечание";
            this.EditNote.Click += new System.EventHandler(this.EditNote_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(289, 6);
            // 
            // createReport
            // 
            this.createReport.Name = "createReport";
            this.createReport.Size = new System.Drawing.Size(292, 22);
            this.createReport.Text = "Сформировать ВТМ в IPS";
            this.createReport.Click += new System.EventHandler(this.createReport_Click);
            // 
            // createDevideList
            // 
            this.createDevideList.Name = "createDevideList";
            this.createDevideList.Size = new System.Drawing.Size(292, 22);
            this.createDevideList.Text = "Создать разделительную ведомость в IPS";
            this.createDevideList.Click += new System.EventHandler(this.createDevideList_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSingleCompleteListMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(292, 22);
            this.toolStripMenuItem2.Text = "Создание компл. карты";
            // 
            // createSingleCompleteListMenuItem
            // 
            this.createSingleCompleteListMenuItem.Name = "createSingleCompleteListMenuItem";
            this.createSingleCompleteListMenuItem.Size = new System.Drawing.Size(307, 22);
            this.createSingleCompleteListMenuItem.Text = "Создать одиночную комплектовочную карту";
            this.createSingleCompleteListMenuItem.Click += new System.EventHandler(this.createSingleCompleteListMenuItem_Click);
            // 
            // createCooperationListMenuItem
            // 
            this.createCooperationListMenuItem.Name = "createCooperationListMenuItem";
            this.createCooperationListMenuItem.Size = new System.Drawing.Size(292, 22);
            this.createCooperationListMenuItem.Text = "Создать ведомость кооперации";
            this.createCooperationListMenuItem.Click += new System.EventHandler(this.createCooperationListMenuItem_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Enabled = false;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(240, 454);
            this.propertyGrid1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAdv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(1270, 454);
            this.splitContainer1.SplitterDistance = 1026;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ColumnHeaderHeight = 19;
            this.treeViewAdv.Columns.Add(this.designationTreeColumn);
            this.treeViewAdv.Columns.Add(this.nameTreeColumn);
            this.treeViewAdv.Columns.Add(this.iconColumn);
            this.treeViewAdv.Columns.Add(this.amountTreeColumn);
            this.treeViewAdv.Columns.Add(this.routeTreeColumn);
            this.treeViewAdv.Columns.Add(this.innerCooperationTreeColumn);
            this.treeViewAdv.Columns.Add(this.substituteTreeColumn);
            this.treeViewAdv.Columns.Add(this.noteTreeColumn);
            this.treeViewAdv.Columns.Add(this.relationNoteTreeColumn);
            this.treeViewAdv.Columns.Add(this.stockRateColumn);
            this.treeViewAdv.Columns.Add(this.sampleSizeColumn);
            this.treeViewAdv.Columns.Add(this.tpRefColumn);
            this.treeViewAdv.Columns.Add(this.techTaskTreeColumn);
            this.treeViewAdv.Columns.Add(this.agentTreeColumn);
            this.treeViewAdv.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.NodeControls.Add(this.nodeIcon);
            this.treeViewAdv.NodeControls.Add(this.textBoxDesignation);
            this.treeViewAdv.NodeControls.Add(this.textBoxName);
            this.treeViewAdv.NodeControls.Add(this.textBoxAmount);
            this.treeViewAdv.NodeControls.Add(this.textBoxRoute);
            this.treeViewAdv.NodeControls.Add(this.checkBoxInnerCoop);
            this.treeViewAdv.NodeControls.Add(this.textBoxNote);
            this.treeViewAdv.NodeControls.Add(this.textBoxSubstitute);
            this.treeViewAdv.NodeControls.Add(this.textBoxRelationNote);
            this.treeViewAdv.NodeControls.Add(this.textBoxSampleSize);
            this.treeViewAdv.NodeControls.Add(this.textBoxStockRate);
            this.treeViewAdv.NodeControls.Add(this.textBoxTpRef);
            this.treeViewAdv.NodeControls.Add(this.textBoxTechTask);
            this.treeViewAdv.NodeControls.Add(this.agentTextbox);
            this.treeViewAdv.NodeFilter = null;
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv.Size = new System.Drawing.Size(1026, 454);
            this.treeViewAdv.TabIndex = 2;
            this.treeViewAdv.Text = "treeViewAdv1";
            this.treeViewAdv.UseColumns = true;
            this.treeViewAdv.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.treeViewAdv1_RowDraw);
            this.treeViewAdv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewAdv_MouseClick);
            // 
            // designationTreeColumn
            // 
            this.designationTreeColumn.Header = "Обозначение";
            this.designationTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.designationTreeColumn.TooltipText = null;
            this.designationTreeColumn.Width = 250;
            // 
            // nameTreeColumn
            // 
            this.nameTreeColumn.Header = "Наименование";
            this.nameTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.nameTreeColumn.TooltipText = null;
            this.nameTreeColumn.Width = 300;
            // 
            // iconColumn
            // 
            this.iconColumn.Header = "";
            this.iconColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.iconColumn.TooltipText = null;
            // 
            // amountTreeColumn
            // 
            this.amountTreeColumn.Header = "Кол-во";
            this.amountTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountTreeColumn.TooltipText = null;
            this.amountTreeColumn.Width = 70;
            // 
            // routeTreeColumn
            // 
            this.routeTreeColumn.Header = "Маршут";
            this.routeTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.routeTreeColumn.TooltipText = null;
            this.routeTreeColumn.Width = 200;
            // 
            // innerCooperationTreeColumn
            // 
            this.innerCooperationTreeColumn.Header = "Внутр. произв. кооп.";
            this.innerCooperationTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.innerCooperationTreeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.innerCooperationTreeColumn.TooltipText = null;
            // 
            // substituteTreeColumn
            // 
            this.substituteTreeColumn.Header = "Инф. о заменах";
            this.substituteTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.substituteTreeColumn.TooltipText = null;
            this.substituteTreeColumn.Width = 200;
            // 
            // noteTreeColumn
            // 
            this.noteTreeColumn.Header = "Примечание техн.";
            this.noteTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.noteTreeColumn.TooltipText = null;
            this.noteTreeColumn.Width = 200;
            // 
            // relationNoteTreeColumn
            // 
            this.relationNoteTreeColumn.Header = "Примечание";
            this.relationNoteTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.relationNoteTreeColumn.TooltipText = null;
            // 
            // stockRateColumn
            // 
            this.stockRateColumn.Header = "Коэф. зап.";
            this.stockRateColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.stockRateColumn.TooltipText = null;
            // 
            // sampleSizeColumn
            // 
            this.sampleSizeColumn.Header = "Выборка";
            this.sampleSizeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.sampleSizeColumn.TooltipText = null;
            // 
            // tpRefColumn
            // 
            this.tpRefColumn.Header = "ТП вх. контр.";
            this.tpRefColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tpRefColumn.TooltipText = null;
            // 
            // techTaskTreeColumn
            // 
            this.techTaskTreeColumn.Header = "Тех. задание";
            this.techTaskTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.techTaskTreeColumn.TooltipText = null;
            // 
            // agentTreeColumn
            // 
            this.agentTreeColumn.Header = "Изготовитель";
            this.agentTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.agentTreeColumn.TooltipText = null;
            this.agentTreeColumn.Width = 200;
            // 
            // nodeIcon
            // 
            this.nodeIcon.DataPropertyName = "Image";
            this.nodeIcon.LeftMargin = 1;
            this.nodeIcon.ParentColumn = this.iconColumn;
            this.nodeIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // textBoxDesignation
            // 
            this.textBoxDesignation.DataPropertyName = "Designation";
            this.textBoxDesignation.IncrementalSearchEnabled = true;
            this.textBoxDesignation.LeftMargin = 3;
            this.textBoxDesignation.ParentColumn = this.designationTreeColumn;
            // 
            // textBoxName
            // 
            this.textBoxName.DataPropertyName = "Name";
            this.textBoxName.IncrementalSearchEnabled = true;
            this.textBoxName.LeftMargin = 3;
            this.textBoxName.ParentColumn = this.nameTreeColumn;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.DataPropertyName = "Amount";
            this.textBoxAmount.IncrementalSearchEnabled = true;
            this.textBoxAmount.LeftMargin = 3;
            this.textBoxAmount.ParentColumn = this.amountTreeColumn;
            // 
            // textBoxRoute
            // 
            this.textBoxRoute.DataPropertyName = "Route";
            this.textBoxRoute.IncrementalSearchEnabled = true;
            this.textBoxRoute.LeftMargin = 3;
            this.textBoxRoute.ParentColumn = this.routeTreeColumn;
            // 
            // checkBoxInnerCoop
            // 
            this.checkBoxInnerCoop.DataPropertyName = "InnerCooperation";
            this.checkBoxInnerCoop.LeftMargin = 0;
            this.checkBoxInnerCoop.ParentColumn = this.innerCooperationTreeColumn;
            // 
            // textBoxNote
            // 
            this.textBoxNote.DataPropertyName = "Note";
            this.textBoxNote.IncrementalSearchEnabled = true;
            this.textBoxNote.LeftMargin = 3;
            this.textBoxNote.ParentColumn = this.noteTreeColumn;
            // 
            // textBoxSubstitute
            // 
            this.textBoxSubstitute.DataPropertyName = "SubInfo";
            this.textBoxSubstitute.IncrementalSearchEnabled = true;
            this.textBoxSubstitute.LeftMargin = 3;
            this.textBoxSubstitute.ParentColumn = this.substituteTreeColumn;
            // 
            // textBoxRelationNote
            // 
            this.textBoxRelationNote.DataPropertyName = "RelationNote";
            this.textBoxRelationNote.IncrementalSearchEnabled = true;
            this.textBoxRelationNote.LeftMargin = 3;
            this.textBoxRelationNote.ParentColumn = this.relationNoteTreeColumn;
            // 
            // textBoxSampleSize
            // 
            this.textBoxSampleSize.DataPropertyName = "SampleSize";
            this.textBoxSampleSize.IncrementalSearchEnabled = true;
            this.textBoxSampleSize.LeftMargin = 3;
            this.textBoxSampleSize.ParentColumn = this.sampleSizeColumn;
            // 
            // textBoxStockRate
            // 
            this.textBoxStockRate.DataPropertyName = "StockRate";
            this.textBoxStockRate.IncrementalSearchEnabled = true;
            this.textBoxStockRate.LeftMargin = 3;
            this.textBoxStockRate.ParentColumn = this.stockRateColumn;
            // 
            // textBoxTpRef
            // 
            this.textBoxTpRef.DataPropertyName = "TechProcessReference";
            this.textBoxTpRef.IncrementalSearchEnabled = true;
            this.textBoxTpRef.LeftMargin = 3;
            this.textBoxTpRef.ParentColumn = this.tpRefColumn;
            // 
            // textBoxTechTask
            // 
            this.textBoxTechTask.DataPropertyName = "TechTask";
            this.textBoxTechTask.IncrementalSearchEnabled = true;
            this.textBoxTechTask.LeftMargin = 3;
            this.textBoxTechTask.ParentColumn = this.techTaskTreeColumn;
            // 
            // agentTextbox
            // 
            this.agentTextbox.DataPropertyName = "Agent";
            this.agentTextbox.IncrementalSearchEnabled = true;
            this.agentTextbox.LeftMargin = 3;
            this.agentTextbox.ParentColumn = this.agentTreeColumn;
            // 
            // createMaterialsListButton
            // 
            this.createMaterialsListButton.Name = "createMaterialsListButton";
            this.createMaterialsListButton.Size = new System.Drawing.Size(292, 22);
            this.createMaterialsListButton.Text = "Создать ВСН";
            // 
            // TechRoutesMapWithDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 479);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Name = "TechRoutesMapWithDataGrid";
            this.Text = "Ведомость технологических маршрутов";
            this.Load += new System.EventHandler(this.TechRoutesMap_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private Aga.Controls.Tree.TreeColumn designationTreeColumn;
        private Aga.Controls.Tree.TreeColumn nameTreeColumn;
        private Aga.Controls.Tree.TreeColumn amountTreeColumn;
        private Aga.Controls.Tree.TreeColumn routeTreeColumn;
        private Aga.Controls.Tree.TreeColumn noteTreeColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxDesignation;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxAmount;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxRoute;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxNote;
        private System.Windows.Forms.ToolStripMenuItem EditNote;
        private Aga.Controls.Tree.TreeColumn substituteTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxSubstitute;
        private Aga.Controls.Tree.TreeColumn agentTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox agentTextbox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem goToArchiveButton;
        private System.Windows.Forms.ToolStripMenuItem createReport;
        private System.Windows.Forms.ToolStripMenuItem createDevideList;
        private System.Windows.Forms.ToolStripButton ExpandAllButton;
        private System.Windows.Forms.ToolStripButton CollapseAllButton;
        private Aga.Controls.Tree.TreeColumn innerCooperationTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox checkBoxInnerCoop;
        private System.Windows.Forms.ToolStripMenuItem SetInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem RemoveInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createSingleCompleteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCooperationListMenuItem;
        private System.Windows.Forms.ToolStripButton refreshTreeButton;
        private Aga.Controls.Tree.NodeControls.NodeIcon nodeIcon;
        private Aga.Controls.Tree.TreeColumn iconColumn;
        private System.Windows.Forms.ToolStripMenuItem updateFromIPSButton;
        private Aga.Controls.Tree.TreeColumn relationNoteTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxRelationNote;
        private Aga.Controls.Tree.TreeColumn stockRateColumn;
        private Aga.Controls.Tree.TreeColumn sampleSizeColumn;
        private Aga.Controls.Tree.TreeColumn tpRefColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxSampleSize;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxStockRate;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxTpRef;
        private Aga.Controls.Tree.TreeColumn techTaskTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxTechTask;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem produceButton;
        private System.Windows.Forms.ToolStripMenuItem doNotProduceButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem createMaterialsListButton;
    }
}