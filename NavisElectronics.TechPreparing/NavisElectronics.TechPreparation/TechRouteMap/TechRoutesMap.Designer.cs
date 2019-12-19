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
            this.downloadInfoFromIPSButton = new System.Windows.Forms.ToolStripButton();
            this.ExpandAllButton = new System.Windows.Forms.ToolStripButton();
            this.CollapseAllButton = new System.Windows.Forms.ToolStripButton();
            this.refreshTreeButton = new System.Windows.Forms.ToolStripButton();
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
            this.agentTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateFromIPSButton = new System.Windows.Forms.ToolStripMenuItem();
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
            this.nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.textBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRoute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.checkBoxInnerCoop = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.textBoxNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxSubstitute = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.agentTextbox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxRelationNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.stockRateColumn = new Aga.Controls.Tree.TreeColumn();
            this.sampleSizeColumn = new Aga.Controls.Tree.TreeColumn();
            this.tpRefColumn = new Aga.Controls.Tree.TreeColumn();
            this.textBoxSampleSize = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxStockRate = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.textBoxTpRef = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadInfoFromIPSButton,
            this.ExpandAllButton,
            this.CollapseAllButton,
            this.refreshTreeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1473, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // downloadInfoFromIPSButton
            // 
            this.downloadInfoFromIPSButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downloadInfoFromIPSButton.Enabled = false;
            this.downloadInfoFromIPSButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.road;
            this.downloadInfoFromIPSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadInfoFromIPSButton.Name = "downloadInfoFromIPSButton";
            this.downloadInfoFromIPSButton.Size = new System.Drawing.Size(23, 22);
            this.downloadInfoFromIPSButton.Text = "Заполнить из IPS";
            this.downloadInfoFromIPSButton.Click += new System.EventHandler(this.SetTechRoutesButtonButton_Click);
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
            this.treeViewAdv.Columns.Add(this.agentTreeColumn);
            this.treeViewAdv.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.treeViewAdv.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(0, 25);
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
            this.updateFromIPSButton,
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
            this.contextMenuStrip.Size = new System.Drawing.Size(293, 362);
            // 
            // updateFromIPSButton
            // 
            this.updateFromIPSButton.Name = "updateFromIPSButton";
            this.updateFromIPSButton.Size = new System.Drawing.Size(292, 22);
            this.updateFromIPSButton.Text = "Обновить узел из IPS";
            this.updateFromIPSButton.Click += new System.EventHandler(this.updateFromIPSButton_Click);
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
            this.editTechRoutesButton.Enabled = false;
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
            this.SetTechRouteMenuItem.Enabled = false;
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
            this.deleteRouteMenuItem.Enabled = false;
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
            this.CopyRouteButton.Enabled = false;
            this.CopyRouteButton.Name = "CopyRouteButton";
            this.CopyRouteButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyRouteButton.Size = new System.Drawing.Size(292, 22);
            this.CopyRouteButton.Text = "Копировать маршрут";
            this.CopyRouteButton.Click += new System.EventHandler(this.CopyRouteButton_Click);
            // 
            // PasteRouteButton
            // 
            this.PasteRouteButton.Enabled = false;
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
            // agentTextbox
            // 
            this.agentTextbox.DataPropertyName = "Agent";
            this.agentTextbox.IncrementalSearchEnabled = true;
            this.agentTextbox.LeftMargin = 3;
            this.agentTextbox.ParentColumn = this.agentTreeColumn;
            // 
            // textBoxRelationNote
            // 
            this.textBoxRelationNote.DataPropertyName = "RelationNote";
            this.textBoxRelationNote.IncrementalSearchEnabled = true;
            this.textBoxRelationNote.LeftMargin = 3;
            this.textBoxRelationNote.ParentColumn = this.relationNoteTreeColumn;
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
        private Aga.Controls.Tree.NodeControls.NodeCheckBox checkBoxInnerCoop;
        private System.Windows.Forms.ToolStripMenuItem SetInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem RemoveInnerCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem createNewRouteButton;
        private System.Windows.Forms.ToolStripMenuItem addIntoExistingRouteButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createSingleCompleteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCooperationListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRouteMenuItem;
        private System.Windows.Forms.ToolStripButton downloadInfoFromIPSButton;
        private System.Windows.Forms.ToolStripMenuItem editTechRoutesButton;
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
    }
}