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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.DefaultCoopRouteButton = new System.Windows.Forms.ToolStripButton();
            this.ExpandAllButton = new System.Windows.Forms.ToolStripButton();
            this.CollapseAllButton = new System.Windows.Forms.ToolStripButton();
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.designationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.routeTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountWithUseTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.totalTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.innerCooperationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.ContainsInnerCooperationTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.isPcbTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.pcbVersionTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.substituteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.noteTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.agentTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowButton = new System.Windows.Forms.ToolStripMenuItem();
            this.goToArchiveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTechRoutesButton = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTechRouteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.addIntoExistingRouteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRouteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.createCooperationListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRoute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.innerCoopCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.ContainsInnerCoopCheckBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.pcbCheckBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.pcbVersionTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxSubstitute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.agentTextbox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.refreshTreeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DefaultCoopRouteButton,
            this.ExpandAllButton,
            this.CollapseAllButton,
            this.refreshTreeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1473, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // DefaultCoopRouteButton
            // 
            this.DefaultCoopRouteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DefaultCoopRouteButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.road;
            this.DefaultCoopRouteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DefaultCoopRouteButton.Name = "DefaultCoopRouteButton";
            this.DefaultCoopRouteButton.Size = new System.Drawing.Size(23, 22);
            this.DefaultCoopRouteButton.Text = "Проставить кооперации путь по умолчанию";
            this.DefaultCoopRouteButton.Click += new System.EventHandler(this.DefaultCoopRouteButton_Click);
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
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ColumnHeaderHeight = 17;
            this.treeViewAdv.Columns.Add(this.designationTreeColumn);
            this.treeViewAdv.Columns.Add(this.nameTreeColumn);
            this.treeViewAdv.Columns.Add(this.amountTreeColumn);
            this.treeViewAdv.Columns.Add(this.routeTreeColumn);
            this.treeViewAdv.Columns.Add(this.amountWithUseTreeColumn);
            this.treeViewAdv.Columns.Add(this.totalTreeColumn);
            this.treeViewAdv.Columns.Add(this.innerCooperationTreeColumn);
            this.treeViewAdv.Columns.Add(this.ContainsInnerCooperationTreeColumn);
            this.treeViewAdv.Columns.Add(this.isPcbTreeColumn);
            this.treeViewAdv.Columns.Add(this.pcbVersionTreeColumn);
            this.treeViewAdv.Columns.Add(this.substituteTreeColumn);
            this.treeViewAdv.Columns.Add(this.noteTreeColumn);
            this.treeViewAdv.Columns.Add(this.agentTreeColumn);
            this.treeViewAdv.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(0, 25);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.NodeControls.Add(this.textBoxDesignation);
            this.treeViewAdv.NodeControls.Add(this.textBoxName);
            this.treeViewAdv.NodeControls.Add(this.textBoxAmount);
            this.treeViewAdv.NodeControls.Add(this.textBoxRoute);
            this.treeViewAdv.NodeControls.Add(this.innerCoopCheckBox);
            this.treeViewAdv.NodeControls.Add(this.ContainsInnerCoopCheckBox);
            this.treeViewAdv.NodeControls.Add(this.textBoxNote);
            this.treeViewAdv.NodeControls.Add(this.pcbCheckBox);
            this.treeViewAdv.NodeControls.Add(this.pcbVersionTextBox);
            this.treeViewAdv.NodeControls.Add(this.textBoxSubstitute);
            this.treeViewAdv.NodeControls.Add(this.agentTextbox);
            this.treeViewAdv.NodeFilter = null;
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv.Size = new System.Drawing.Size(1473, 454);
            this.treeViewAdv.TabIndex = 2;
            this.treeViewAdv.Text = "treeViewAdv1";
            this.treeViewAdv.UseColumns = true;
            this.treeViewAdv.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.treeViewAdv1_RowDraw);
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
            // amountWithUseTreeColumn
            // 
            this.amountWithUseTreeColumn.Header = "Кол. с прим.";
            this.amountWithUseTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.amountWithUseTreeColumn.TooltipText = null;
            // 
            // totalTreeColumn
            // 
            this.totalTreeColumn.Header = "Всего";
            this.totalTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.totalTreeColumn.TooltipText = null;
            // 
            // innerCooperationTreeColumn
            // 
            this.innerCooperationTreeColumn.Header = "Внутр. произв. кооп.";
            this.innerCooperationTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.innerCooperationTreeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.innerCooperationTreeColumn.TooltipText = null;
            // 
            // ContainsInnerCooperationTreeColumn
            // 
            this.ContainsInnerCooperationTreeColumn.Header = "Содержит вн. кооп";
            this.ContainsInnerCooperationTreeColumn.IsVisible = false;
            this.ContainsInnerCooperationTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.ContainsInnerCooperationTreeColumn.TooltipText = null;
            // 
            // isPcbTreeColumn
            // 
            this.isPcbTreeColumn.Header = "PCB";
            this.isPcbTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.isPcbTreeColumn.TooltipText = null;
            // 
            // pcbVersionTreeColumn
            // 
            this.pcbVersionTreeColumn.Header = "Версия PCB";
            this.pcbVersionTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.pcbVersionTreeColumn.TooltipText = null;
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
            this.noteTreeColumn.Header = "Примечание";
            this.noteTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.noteTreeColumn.TooltipText = null;
            this.noteTreeColumn.Width = 200;
            // 
            // agentTreeColumn
            // 
            this.agentTreeColumn.Header = "Изготовитель";
            this.agentTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.agentTreeColumn.TooltipText = null;
            this.agentTreeColumn.Width = 200;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowButton,
            this.goToArchiveButton,
            this.toolStripSeparator1,
            this.editTechRoutesButton,
            this.SetTechRouteMenuItem,
            this.deleteRouteMenuItem,
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
            this.contextMenuStrip.Size = new System.Drawing.Size(293, 340);
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
            // editTechRoutesButton
            // 
            this.editTechRoutesButton.Name = "editTechRoutesButton";
            this.editTechRoutesButton.Size = new System.Drawing.Size(292, 22);
            this.editTechRoutesButton.Text = "Редактировать маршруты";
            this.editTechRoutesButton.Click += new System.EventHandler(this.editTechRoutesButton_Click);
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
            this.createNewRouteButton.Text = "Создать новый";
            this.createNewRouteButton.Click += new System.EventHandler(this.createNewRouteButton_Click);
            // 
            // addIntoExistingRouteButton
            // 
            this.addIntoExistingRouteButton.Name = "addIntoExistingRouteButton";
            this.addIntoExistingRouteButton.Size = new System.Drawing.Size(221, 22);
            this.addIntoExistingRouteButton.Text = "Добавить к существующему";
            this.addIntoExistingRouteButton.Click += new System.EventHandler(this.addIntoExistingRouteButton_Click);
            // 
            // deleteRouteMenuItem
            // 
            this.deleteRouteMenuItem.Image = global::NavisElectronics.TechPreparation.Properties.Resources.action_Cancel_16xLG;
            this.deleteRouteMenuItem.Name = "deleteRouteMenuItem";
            this.deleteRouteMenuItem.Size = new System.Drawing.Size(292, 22);
            this.deleteRouteMenuItem.Text = "Удалить маршрут";
            this.deleteRouteMenuItem.Click += new System.EventHandler(this.deleteRouteMenuItem_Click);
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
            // innerCoopCheckBox
            // 
            this.innerCoopCheckBox.DataPropertyName = "InnerCooperation";
            this.innerCoopCheckBox.LeftMargin = 0;
            this.innerCoopCheckBox.ParentColumn = this.innerCooperationTreeColumn;
            // 
            // ContainsInnerCoopCheckBox
            // 
            this.ContainsInnerCoopCheckBox.DataPropertyName = "ContainsInnerCooperation";
            this.ContainsInnerCoopCheckBox.IncrementalSearchEnabled = true;
            this.ContainsInnerCoopCheckBox.LeftMargin = 3;
            this.ContainsInnerCoopCheckBox.ParentColumn = this.ContainsInnerCooperationTreeColumn;
            // 
            // textBoxNote
            // 
            this.textBoxNote.DataPropertyName = "Note";
            this.textBoxNote.IncrementalSearchEnabled = true;
            this.textBoxNote.LeftMargin = 3;
            this.textBoxNote.ParentColumn = this.noteTreeColumn;
            // 
            // pcbCheckBox
            // 
            this.pcbCheckBox.DataPropertyName = "IsPcb";
            this.pcbCheckBox.IncrementalSearchEnabled = true;
            this.pcbCheckBox.LeftMargin = 3;
            this.pcbCheckBox.ParentColumn = this.isPcbTreeColumn;
            // 
            // pcbVersionTextBox
            // 
            this.pcbVersionTextBox.DataPropertyName = "PcbVersion";
            this.pcbVersionTextBox.IncrementalSearchEnabled = true;
            this.pcbVersionTextBox.LeftMargin = 3;
            this.pcbVersionTextBox.ParentColumn = this.pcbVersionTreeColumn;
            // 
            // textBoxSubstitute
            // 
            this.textBoxSubstitute.DataPropertyName = "SubInfo";
            this.textBoxSubstitute.IncrementalSearchEnabled = true;
            this.textBoxSubstitute.LeftMargin = 3;
            this.textBoxSubstitute.ParentColumn = this.substituteTreeColumn;
            // 
            // agentTextbox
            // 
            this.agentTextbox.DataPropertyName = "Agent";
            this.agentTextbox.IncrementalSearchEnabled = true;
            this.agentTextbox.LeftMargin = 3;
            this.agentTextbox.ParentColumn = this.agentTreeColumn;
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
            // TechRoutesMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 479);
            this.Controls.Add(this.treeViewAdv);
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
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private Aga.Controls.Tree.TreeColumn designationTreeColumn;
        private Aga.Controls.Tree.TreeColumn nameTreeColumn;
        private Aga.Controls.Tree.TreeColumn amountTreeColumn;
        private Aga.Controls.Tree.TreeColumn routeTreeColumn;
        private Aga.Controls.Tree.TreeColumn noteTreeColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SetTechRouteMenuItem;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxDesignation;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxAmount;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxRoute;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxNote;
        private System.Windows.Forms.ToolStripMenuItem EditNote;
        private Aga.Controls.Tree.TreeColumn substituteTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox textBoxSubstitute;
        private System.Windows.Forms.ToolStripMenuItem CopyRouteButton;
        private System.Windows.Forms.ToolStripMenuItem PasteRouteButton;
        private Aga.Controls.Tree.TreeColumn agentTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox agentTextbox;
        private System.Windows.Forms.ToolStripMenuItem ShowButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem goToArchiveButton;
        private System.Windows.Forms.ToolStripMenuItem createReport;
        private System.Windows.Forms.ToolStripMenuItem createDevideList;
        private System.Windows.Forms.ToolStripButton ExpandAllButton;
        private System.Windows.Forms.ToolStripButton CollapseAllButton;
        private Aga.Controls.Tree.TreeColumn innerCooperationTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox innerCoopCheckBox;
        private System.Windows.Forms.ToolStripMenuItem SetInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem RemoveInnerCooperationButton;
        private Aga.Controls.Tree.TreeColumn ContainsInnerCooperationTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ContainsInnerCoopCheckBox;
        private System.Windows.Forms.ToolStripMenuItem createNewRouteButton;
        private System.Windows.Forms.ToolStripMenuItem addIntoExistingRouteButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createSingleCompleteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCooperationListMenuItem;
        private Aga.Controls.Tree.TreeColumn amountWithUseTreeColumn;
        private Aga.Controls.Tree.TreeColumn totalTreeColumn;
        private Aga.Controls.Tree.TreeColumn isPcbTreeColumn;
        private Aga.Controls.Tree.TreeColumn pcbVersionTreeColumn;
        private System.Windows.Forms.ToolStripMenuItem deleteRouteMenuItem;
        private Aga.Controls.Tree.NodeControls.NodeTextBox pcbCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox pcbVersionTextBox;
        private System.Windows.Forms.ToolStripButton DefaultCoopRouteButton;
        private System.Windows.Forms.ToolStripMenuItem editTechRoutesButton;
        private System.Windows.Forms.ToolStripButton refreshTreeButton;
    }
}