namespace NavisElectronics.TechPreparation.Views
{
    partial class TechRoutesMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechRoutesMap));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SetNodesForComplectButton = new System.Windows.Forms.ToolStripButton();
            this.createCompleteCardsForWholeOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExpandAllButton = new System.Windows.Forms.ToolStripButton();
            this.CollapseAllButton = new System.Windows.Forms.ToolStripButton();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.designationColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountColumn = new Aga.Controls.Tree.TreeColumn();
            this.routeColumn = new Aga.Controls.Tree.TreeColumn();
            this.InnerCooperation = new Aga.Controls.Tree.TreeColumn();
            this.ContainsInnerCooperationColumn = new Aga.Controls.Tree.TreeColumn();
            this.noteColumn = new Aga.Controls.Tree.TreeColumn();
            this.substituteColumn = new Aga.Controls.Tree.TreeColumn();
            this.AgentColumn = new Aga.Controls.Tree.TreeColumn();
            this.IsToComplecColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowButton = new System.Windows.Forms.ToolStripMenuItem();
            this.goToArchiveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SetTechRouteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.addIntoExistingRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.SetInnerCooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveInnerCooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.EditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createReport = new System.Windows.Forms.ToolStripMenuItem();
            this.createDevideList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.createSingleCompleteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFullCompleteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCooperationListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRoute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.innerCoopCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.ContainsInnerCoopCheckBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxSubstitute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.agentTextbox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.isToComplectCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveButton,
            this.toolStripSeparator2,
            this.SetNodesForComplectButton,
            this.createCompleteCardsForWholeOrder,
            this.toolStripSeparator3,
            this.ExpandAllButton,
            this.CollapseAllButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1131, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_save_20659;
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 22);
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SetNodesForComplectButton
            // 
            this.SetNodesForComplectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetNodesForComplectButton.Image = ((System.Drawing.Image)(resources.GetObject("SetNodesForComplectButton.Image")));
            this.SetNodesForComplectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetNodesForComplectButton.Name = "SetNodesForComplectButton";
            this.SetNodesForComplectButton.Size = new System.Drawing.Size(23, 22);
            this.SetNodesForComplectButton.Text = "Проставить комлпектовочные узлы";
            this.SetNodesForComplectButton.Click += new System.EventHandler(this.SetNodesForComplectButton_Click);
            // 
            // createCompleteCardsForWholeOrder
            // 
            this.createCompleteCardsForWholeOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createCompleteCardsForWholeOrder.Image = ((System.Drawing.Image)(resources.GetObject("createCompleteCardsForWholeOrder.Image")));
            this.createCompleteCardsForWholeOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createCompleteCardsForWholeOrder.Name = "createCompleteCardsForWholeOrder";
            this.createCompleteCardsForWholeOrder.Size = new System.Drawing.Size(23, 22);
            this.createCompleteCardsForWholeOrder.Text = "Создать комплектовочные карты на заказ";
            this.createCompleteCardsForWholeOrder.Click += new System.EventHandler(this.createCompleteCardsForWholeOrder_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 17;
            this.treeViewAdv1.Columns.Add(this.designationColumn);
            this.treeViewAdv1.Columns.Add(this.nameColumn);
            this.treeViewAdv1.Columns.Add(this.amountColumn);
            this.treeViewAdv1.Columns.Add(this.routeColumn);
            this.treeViewAdv1.Columns.Add(this.InnerCooperation);
            this.treeViewAdv1.Columns.Add(this.ContainsInnerCooperationColumn);
            this.treeViewAdv1.Columns.Add(this.noteColumn);
            this.treeViewAdv1.Columns.Add(this.substituteColumn);
            this.treeViewAdv1.Columns.Add(this.AgentColumn);
            this.treeViewAdv1.Columns.Add(this.IsToComplecColumn);
            this.treeViewAdv1.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv1.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 25);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this.textBoxDesignation);
            this.treeViewAdv1.NodeControls.Add(this.textBoxName);
            this.treeViewAdv1.NodeControls.Add(this.textBoxAmount);
            this.treeViewAdv1.NodeControls.Add(this.textBoxRoute);
            this.treeViewAdv1.NodeControls.Add(this.innerCoopCheckBox);
            this.treeViewAdv1.NodeControls.Add(this.ContainsInnerCoopCheckBox);
            this.treeViewAdv1.NodeControls.Add(this.textBoxNote);
            this.treeViewAdv1.NodeControls.Add(this.textBoxSubstitute);
            this.treeViewAdv1.NodeControls.Add(this.agentTextbox);
            this.treeViewAdv1.NodeControls.Add(this.isToComplectCheckBox);
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv1.Size = new System.Drawing.Size(1131, 366);
            this.treeViewAdv1.TabIndex = 2;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            this.treeViewAdv1.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.treeViewAdv1_RowDraw);
            // 
            // designationColumn
            // 
            this.designationColumn.Header = "Обозначение";
            this.designationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.designationColumn.TooltipText = null;
            this.designationColumn.Width = 250;
            // 
            // nameColumn
            // 
            this.nameColumn.Header = "Наименование";
            this.nameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.nameColumn.TooltipText = null;
            this.nameColumn.Width = 300;
            // 
            // amountColumn
            // 
            this.amountColumn.Header = "Кол-во";
            this.amountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountColumn.TooltipText = null;
            this.amountColumn.Width = 70;
            // 
            // routeColumn
            // 
            this.routeColumn.Header = "Маршут";
            this.routeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.routeColumn.TooltipText = null;
            this.routeColumn.Width = 200;
            // 
            // InnerCooperation
            // 
            this.InnerCooperation.Header = "Внутр. произв. кооп.";
            this.InnerCooperation.SortOrder = System.Windows.Forms.SortOrder.None;
            this.InnerCooperation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.InnerCooperation.TooltipText = null;
            // 
            // ContainsInnerCooperationColumn
            // 
            this.ContainsInnerCooperationColumn.Header = "Содержит вн. кооп";
            this.ContainsInnerCooperationColumn.IsVisible = false;
            this.ContainsInnerCooperationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.ContainsInnerCooperationColumn.TooltipText = null;
            // 
            // noteColumn
            // 
            this.noteColumn.Header = "Примечание";
            this.noteColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.noteColumn.TooltipText = null;
            this.noteColumn.Width = 200;
            // 
            // substituteColumn
            // 
            this.substituteColumn.Header = "Инф. о заменах";
            this.substituteColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.substituteColumn.TooltipText = null;
            this.substituteColumn.Width = 200;
            // 
            // AgentColumn
            // 
            this.AgentColumn.Header = "Изготовитель";
            this.AgentColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.AgentColumn.TooltipText = null;
            this.AgentColumn.Width = 200;
            // 
            // IsToComplecColumn
            // 
            this.IsToComplecColumn.Header = "Узел для компл.";
            this.IsToComplecColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.IsToComplecColumn.TooltipText = null;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowButton,
            this.goToArchiveButton,
            this.toolStripSeparator1,
            this.SetTechRouteMenuItem,
            this.SetInnerCooperationButton,
            this.RemoveInnerCooperationButton,
            this.EditNote,
            this.CopyRouteButton,
            this.PasteRouteButton,
            this.toolStripMenuItem1,
            this.createReport,
            this.createDevideList,
            this.toolStripMenuItem2,
            this.createCooperationListMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(293, 318);
            // 
            // ShowButton
            // 
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(292, 22);
            this.ShowButton.Text = "Посмотреть";
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
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
            // SetTechRouteMenuItem
            // 
            this.SetTechRouteMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewRouteButton,
            this.addIntoExistingRouteButton});
            this.SetTechRouteMenuItem.Image = global::NavisElectronics.TechPreparation.Properties.Resources.RouteObject;
            this.SetTechRouteMenuItem.Name = "SetTechRouteMenuItem";
            this.SetTechRouteMenuItem.Size = new System.Drawing.Size(292, 22);
            this.SetTechRouteMenuItem.Text = "Маршрут изготовления";
            // 
            // createNewRouteButton
            // 
            this.createNewRouteButton.Name = "createNewRouteButton";
            this.createNewRouteButton.Size = new System.Drawing.Size(221, 22);
            this.createNewRouteButton.Text = "Добавить новый";
            this.createNewRouteButton.Click += new System.EventHandler(this.createNewRouteButton_Click);
            // 
            // addIntoExistingRouteButton
            // 
            this.addIntoExistingRouteButton.Name = "addIntoExistingRouteButton";
            this.addIntoExistingRouteButton.Size = new System.Drawing.Size(221, 22);
            this.addIntoExistingRouteButton.Text = "Добавить к существующему";
            this.addIntoExistingRouteButton.Click += new System.EventHandler(this.addIntoExistingRouteButton_Click);
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
            // EditNote
            // 
            this.EditNote.Name = "EditNote";
            this.EditNote.Size = new System.Drawing.Size(292, 22);
            this.EditNote.Text = "Добавить примечание";
            this.EditNote.Click += new System.EventHandler(this.EditNote_Click);
            // 
            // CopyRouteButton
            // 
            this.CopyRouteButton.Name = "CopyRouteButton";
            this.CopyRouteButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyRouteButton.Size = new System.Drawing.Size(292, 22);
            this.CopyRouteButton.Text = "Копировать маршрут";
            this.CopyRouteButton.Click += new System.EventHandler(this.CopyRouteButton_Click);
            // 
            // PasteRouteButton
            // 
            this.PasteRouteButton.Name = "PasteRouteButton";
            this.PasteRouteButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteRouteButton.Size = new System.Drawing.Size(292, 22);
            this.PasteRouteButton.Text = "Вставить маршрут";
            this.PasteRouteButton.Click += new System.EventHandler(this.PasteRouteButton_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(292, 22);
            this.toolStripMenuItem1.Text = "Создать ТП по указанному маршруту";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
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
            this.createSingleCompleteListMenuItem,
            this.createFullCompleteListMenuItem});
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
            // createFullCompleteListMenuItem
            // 
            this.createFullCompleteListMenuItem.Name = "createFullCompleteListMenuItem";
            this.createFullCompleteListMenuItem.Size = new System.Drawing.Size(307, 22);
            this.createFullCompleteListMenuItem.Text = "Создать полную комплектовочную карту";
            this.createFullCompleteListMenuItem.Click += new System.EventHandler(this.createFullCompleteListMenuItem_Click);
            // 
            // createCooperationListMenuItem
            // 
            this.createCooperationListMenuItem.Name = "createCooperationListMenuItem";
            this.createCooperationListMenuItem.Size = new System.Drawing.Size(292, 22);
            this.createCooperationListMenuItem.Text = "Создать ведомость кооперации";
            this.createCooperationListMenuItem.Click += new System.EventHandler(this.createCooperationListMenuItem_Click);
            // 
            // textBoxDesignation
            // 
            this.textBoxDesignation.DataPropertyName = "Designation";
            this.textBoxDesignation.IncrementalSearchEnabled = true;
            this.textBoxDesignation.LeftMargin = 3;
            this.textBoxDesignation.ParentColumn = this.designationColumn;
            // 
            // textBoxName
            // 
            this.textBoxName.DataPropertyName = "Name";
            this.textBoxName.IncrementalSearchEnabled = true;
            this.textBoxName.LeftMargin = 3;
            this.textBoxName.ParentColumn = this.nameColumn;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.DataPropertyName = "Amount";
            this.textBoxAmount.IncrementalSearchEnabled = true;
            this.textBoxAmount.LeftMargin = 3;
            this.textBoxAmount.ParentColumn = this.amountColumn;
            // 
            // textBoxRoute
            // 
            this.textBoxRoute.DataPropertyName = "Route";
            this.textBoxRoute.IncrementalSearchEnabled = true;
            this.textBoxRoute.LeftMargin = 3;
            this.textBoxRoute.ParentColumn = this.routeColumn;
            // 
            // innerCoopCheckBox
            // 
            this.innerCoopCheckBox.DataPropertyName = "InnerCooperation";
            this.innerCoopCheckBox.LeftMargin = 0;
            this.innerCoopCheckBox.ParentColumn = this.InnerCooperation;
            // 
            // ContainsInnerCoopCheckBox
            // 
            this.ContainsInnerCoopCheckBox.DataPropertyName = "ContainsInnerCooperation";
            this.ContainsInnerCoopCheckBox.IncrementalSearchEnabled = true;
            this.ContainsInnerCoopCheckBox.LeftMargin = 3;
            this.ContainsInnerCoopCheckBox.ParentColumn = this.ContainsInnerCooperationColumn;
            // 
            // textBoxNote
            // 
            this.textBoxNote.DataPropertyName = "Note";
            this.textBoxNote.IncrementalSearchEnabled = true;
            this.textBoxNote.LeftMargin = 3;
            this.textBoxNote.ParentColumn = this.noteColumn;
            // 
            // textBoxSubstitute
            // 
            this.textBoxSubstitute.DataPropertyName = "SubInfo";
            this.textBoxSubstitute.IncrementalSearchEnabled = true;
            this.textBoxSubstitute.LeftMargin = 3;
            this.textBoxSubstitute.ParentColumn = this.substituteColumn;
            // 
            // agentTextbox
            // 
            this.agentTextbox.DataPropertyName = "Agent";
            this.agentTextbox.IncrementalSearchEnabled = true;
            this.agentTextbox.LeftMargin = 3;
            this.agentTextbox.ParentColumn = this.AgentColumn;
            // 
            // isToComplectCheckBox
            // 
            this.isToComplectCheckBox.DataPropertyName = "IsToComplect";
            this.isToComplectCheckBox.LeftMargin = 0;
            this.isToComplectCheckBox.ParentColumn = this.IsToComplecColumn;
            // 
            // TechRoutesMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 391);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TechRoutesMap";
            this.Text = "Ведомость технологических маршрутов";
            this.Load += new System.EventHandler(this.TechRoutesMap_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private Aga.Controls.Tree.TreeColumn designationColumn;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn amountColumn;
        private Aga.Controls.Tree.TreeColumn routeColumn;
        private Aga.Controls.Tree.TreeColumn noteColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SetTechRouteMenuItem;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxDesignation;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxAmount;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxRoute;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxNote;
        private System.Windows.Forms.ToolStripMenuItem EditNote;
        private Aga.Controls.Tree.TreeColumn substituteColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxSubstitute;
        private System.Windows.Forms.ToolStripMenuItem CopyRouteButton;
        private System.Windows.Forms.ToolStripMenuItem PasteRouteButton;
        private Aga.Controls.Tree.TreeColumn AgentColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox agentTextbox;
        private System.Windows.Forms.ToolStripMenuItem ShowButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem goToArchiveButton;
        private System.Windows.Forms.ToolStripMenuItem createReport;
        private System.Windows.Forms.ToolStripMenuItem createDevideList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ExpandAllButton;
        private System.Windows.Forms.ToolStripButton CollapseAllButton;
        private Aga.Controls.Tree.TreeColumn InnerCooperation;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox innerCoopCheckBox;
        private System.Windows.Forms.ToolStripMenuItem SetInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem RemoveInnerCooperationButton;
        private Aga.Controls.Tree.TreeColumn ContainsInnerCooperationColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ContainsInnerCoopCheckBox;
        private System.Windows.Forms.ToolStripMenuItem createNewRouteButton;
        private System.Windows.Forms.ToolStripMenuItem addIntoExistingRouteButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createSingleCompleteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFullCompleteListMenuItem;
        private System.Windows.Forms.ToolStripButton SetNodesForComplectButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Aga.Controls.Tree.TreeColumn IsToComplecColumn;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox isToComplectCheckBox;
        private System.Windows.Forms.ToolStripMenuItem createCooperationListMenuItem;
        private System.Windows.Forms.ToolStripButton createCompleteCardsForWholeOrder;
    }
}