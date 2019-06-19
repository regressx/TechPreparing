namespace NavisElectronics.TechPreparation.Views
{
    partial class CooperationView
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
            this.myToolStrip = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchInTreeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SetAllCooperationButton = new System.Windows.Forms.ToolStripButton();
            this.CheckButtonClick = new System.Windows.Forms.ToolStripButton();
            this.CheckingModeOn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ExpandAllButton = new System.Windows.Forms.ToolStripButton();
            this.collapseAllButton = new System.Windows.Forms.ToolStripButton();
            this.myContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showIntermechCardButton = new System.Windows.Forms.ToolStripMenuItem();
            this.GoToTheArchiveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.findInTreeNodeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CooperationButton = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteCoopButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SetTechProcessButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetTechProcessButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SetNoteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteNoteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SetParametersButton = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTechTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetPcbMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DesignationColumn = new Aga.Controls.Tree.TreeColumn();
            this.NameColumn = new Aga.Controls.Tree.TreeColumn();
            this.CooperationColumn = new Aga.Controls.Tree.TreeColumn();
            this.AmountColumn = new Aga.Controls.Tree.TreeColumn();
            this.AmountWithUseColumn = new Aga.Controls.Tree.TreeColumn();
            this.TotalAmountColumn = new Aga.Controls.Tree.TreeColumn();
            this.StockRateColumn = new Aga.Controls.Tree.TreeColumn();
            this.SampleSizeColumn = new Aga.Controls.Tree.TreeColumn();
            this.TechProcessReferenceColumn = new Aga.Controls.Tree.TreeColumn();
            this.NoteColumn = new Aga.Controls.Tree.TreeColumn();
            this.SubstituteInfoColumn = new Aga.Controls.Tree.TreeColumn();
            this.pcbColumn = new Aga.Controls.Tree.TreeColumn();
            this.pcbVersionColumn = new Aga.Controls.Tree.TreeColumn();
            this.techTaskColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.designationTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.coopCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.amountTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.amountWithUseTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.totalAmountTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.stockRateTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.sampleSizeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.techProcTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.noteTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.subInfoTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.pcbCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.pcbVersionTextbox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.TechTaskTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.myToolStrip.SuspendLayout();
            this.myContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // myToolStrip
            // 
            this.myToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveButton,
            this.toolStripSeparator3,
            this.SearchInTreeButton,
            this.toolStripSeparator4,
            this.SetAllCooperationButton,
            this.CheckButtonClick,
            this.CheckingModeOn,
            this.toolStripSeparator7,
            this.ExpandAllButton,
            this.collapseAllButton});
            this.myToolStrip.Location = new System.Drawing.Point(0, 0);
            this.myToolStrip.Name = "myToolStrip";
            this.myToolStrip.Size = new System.Drawing.Size(1412, 25);
            this.myToolStrip.TabIndex = 1;
            this.myToolStrip.Text = "toolStrip1";
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SearchInTreeButton
            // 
            this.SearchInTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchInTreeButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_filefind_20455;
            this.SearchInTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchInTreeButton.Name = "SearchInTreeButton";
            this.SearchInTreeButton.Size = new System.Drawing.Size(23, 22);
            this.SearchInTreeButton.Text = "Найти";
            this.SearchInTreeButton.Click += new System.EventHandler(this.SearchInTreeButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // SetAllCooperationButton
            // 
            this.SetAllCooperationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetAllCooperationButton.Enabled = false;
            this.SetAllCooperationButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_list_enum_21970;
            this.SetAllCooperationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetAllCooperationButton.Name = "SetAllCooperationButton";
            this.SetAllCooperationButton.Size = new System.Drawing.Size(23, 22);
            this.SetAllCooperationButton.Text = "Проставить кооперацию";
            this.SetAllCooperationButton.Click += new System.EventHandler(this.SetAllCooperationButton_Click);
            // 
            // CheckButtonClick
            // 
            this.CheckButtonClick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckButtonClick.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_check_filled_21448;
            this.CheckButtonClick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckButtonClick.Name = "CheckButtonClick";
            this.CheckButtonClick.Size = new System.Drawing.Size(23, 22);
            this.CheckButtonClick.Text = "Проверить, что всё готово";
            this.CheckButtonClick.Click += new System.EventHandler(this.CheckButtonClick_Click);
            // 
            // CheckingModeOn
            // 
            this.CheckingModeOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckingModeOn.Enabled = false;
            this.CheckingModeOn.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_edit_16;
            this.CheckingModeOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckingModeOn.Name = "CheckingModeOn";
            this.CheckingModeOn.Size = new System.Drawing.Size(23, 22);
            this.CheckingModeOn.Text = "Режим проверки";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
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
            // collapseAllButton
            // 
            this.collapseAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collapseAllButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_simple_tree_16;
            this.collapseAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.collapseAllButton.Name = "collapseAllButton";
            this.collapseAllButton.Size = new System.Drawing.Size(23, 22);
            this.collapseAllButton.Text = "Собрать все узлы";
            this.collapseAllButton.Click += new System.EventHandler(this.collapseAllButton_Click);
            // 
            // myContextMenuStrip
            // 
            this.myContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showIntermechCardButton,
            this.GoToTheArchiveButton,
            this.findInTreeNodeButton,
            this.toolStripSeparator6,
            this.CooperationButton,
            this.DeleteCoopButton,
            this.toolStripSeparator1,
            this.SetTechProcessButton,
            this.ResetTechProcessButton,
            this.toolStripSeparator2,
            this.SetNoteButton,
            this.DeleteNoteButton,
            this.toolStripSeparator5,
            this.SetParametersButton,
            this.SetTechTaskMenuItem,
            this.SetPcbMenuItem});
            this.myContextMenuStrip.Name = "MyContextMenuStrip";
            this.myContextMenuStrip.Size = new System.Drawing.Size(245, 292);
            // 
            // showIntermechCardButton
            // 
            this.showIntermechCardButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.Search_16x;
            this.showIntermechCardButton.Name = "showIntermechCardButton";
            this.showIntermechCardButton.Size = new System.Drawing.Size(244, 22);
            this.showIntermechCardButton.Text = "Просмотр";
            this.showIntermechCardButton.Click += new System.EventHandler(this.SearchInArchiveButton_Click);
            // 
            // GoToTheArchiveButton
            // 
            this.GoToTheArchiveButton.Name = "GoToTheArchiveButton";
            this.GoToTheArchiveButton.Size = new System.Drawing.Size(244, 22);
            this.GoToTheArchiveButton.Text = "Перейти к архиву предприятия";
            this.GoToTheArchiveButton.Click += new System.EventHandler(this.GoToTheArchiveButton_Click);
            // 
            // findInTreeNodeButton
            // 
            this.findInTreeNodeButton.Enabled = false;
            this.findInTreeNodeButton.Name = "findInTreeNodeButton";
            this.findInTreeNodeButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findInTreeNodeButton.Size = new System.Drawing.Size(244, 22);
            this.findInTreeNodeButton.Text = "Найти в узле";
            this.findInTreeNodeButton.Click += new System.EventHandler(this.findInTreeButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(241, 6);
            // 
            // CooperationButton
            // 
            this.CooperationButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_new_meeting_21476;
            this.CooperationButton.Name = "CooperationButton";
            this.CooperationButton.Size = new System.Drawing.Size(244, 22);
            this.CooperationButton.Text = "По кооперации";
            this.CooperationButton.Click += new System.EventHandler(this.CooperationButton_Click);
            // 
            // DeleteCoopButton
            // 
            this.DeleteCoopButton.Name = "DeleteCoopButton";
            this.DeleteCoopButton.Size = new System.Drawing.Size(244, 22);
            this.DeleteCoopButton.Text = "Убрать кооперацию";
            this.DeleteCoopButton.Click += new System.EventHandler(this.DeleteCoopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            // 
            // SetTechProcessButton
            // 
            this.SetTechProcessButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_x_office_document_21104;
            this.SetTechProcessButton.Name = "SetTechProcessButton";
            this.SetTechProcessButton.Size = new System.Drawing.Size(244, 22);
            this.SetTechProcessButton.Text = "Указать ТТП";
            this.SetTechProcessButton.Click += new System.EventHandler(this.SetTechProcessButton_Click);
            // 
            // ResetTechProcessButton
            // 
            this.ResetTechProcessButton.Name = "ResetTechProcessButton";
            this.ResetTechProcessButton.Size = new System.Drawing.Size(244, 22);
            this.ResetTechProcessButton.Text = "Убрать ТТП";
            this.ResetTechProcessButton.Click += new System.EventHandler(this.ResetTechProcessButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
            // 
            // SetNoteButton
            // 
            this.SetNoteButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_insert_note_21825;
            this.SetNoteButton.Name = "SetNoteButton";
            this.SetNoteButton.Size = new System.Drawing.Size(244, 22);
            this.SetNoteButton.Text = "Добавить примечание";
            this.SetNoteButton.Click += new System.EventHandler(this.SetNoteButton_Click);
            // 
            // DeleteNoteButton
            // 
            this.DeleteNoteButton.Name = "DeleteNoteButton";
            this.DeleteNoteButton.Size = new System.Drawing.Size(244, 22);
            this.DeleteNoteButton.Text = "Очистить примечание";
            this.DeleteNoteButton.Click += new System.EventHandler(this.DeleteNoteButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(241, 6);
            // 
            // SetParametersButton
            // 
            this.SetParametersButton.Name = "SetParametersButton";
            this.SetParametersButton.Size = new System.Drawing.Size(244, 22);
            this.SetParametersButton.Text = "Задать параметры";
            this.SetParametersButton.Click += new System.EventHandler(this.SetParametersButton_Click);
            // 
            // SetTechTaskMenuItem
            // 
            this.SetTechTaskMenuItem.Name = "SetTechTaskMenuItem";
            this.SetTechTaskMenuItem.Size = new System.Drawing.Size(244, 22);
            this.SetTechTaskMenuItem.Text = "Указать данные ТЗ";
            this.SetTechTaskMenuItem.Click += new System.EventHandler(this.SetTechTaskMenuItem_Click);
            // 
            // SetPcbMenuItem
            // 
            this.SetPcbMenuItem.Name = "SetPcbMenuItem";
            this.SetPcbMenuItem.Size = new System.Drawing.Size(244, 22);
            this.SetPcbMenuItem.Text = "Указать, что это ПП";
            this.SetPcbMenuItem.Click += new System.EventHandler(this.SetPcbMenuItem_Click);
            // 
            // DesignationColumn
            // 
            this.DesignationColumn.Header = "Обозначение";
            this.DesignationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.DesignationColumn.TooltipText = null;
            this.DesignationColumn.Width = 250;
            // 
            // NameColumn
            // 
            this.NameColumn.Header = "Наименование";
            this.NameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.NameColumn.TooltipText = null;
            this.NameColumn.Width = 250;
            // 
            // CooperationColumn
            // 
            this.CooperationColumn.Header = "Кооперация";
            this.CooperationColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.CooperationColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CooperationColumn.TooltipText = null;
            this.CooperationColumn.Width = 100;
            // 
            // AmountColumn
            // 
            this.AmountColumn.Header = "Кол-во";
            this.AmountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.AmountColumn.TooltipText = null;
            // 
            // AmountWithUseColumn
            // 
            this.AmountWithUseColumn.Header = "Кол-во с прим.";
            this.AmountWithUseColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.AmountWithUseColumn.TooltipText = null;
            this.AmountWithUseColumn.Width = 75;
            // 
            // TotalAmountColumn
            // 
            this.TotalAmountColumn.Header = "Всего";
            this.TotalAmountColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.TotalAmountColumn.TooltipText = null;
            // 
            // StockRateColumn
            // 
            this.StockRateColumn.Header = "Коэф. запаса";
            this.StockRateColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.StockRateColumn.TooltipText = null;
            this.StockRateColumn.Width = 75;
            // 
            // SampleSizeColumn
            // 
            this.SampleSizeColumn.Header = "Объем выборки";
            this.SampleSizeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.SampleSizeColumn.TooltipText = null;
            this.SampleSizeColumn.Width = 75;
            // 
            // TechProcessReferenceColumn
            // 
            this.TechProcessReferenceColumn.Header = "Тех. процесс";
            this.TechProcessReferenceColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.TechProcessReferenceColumn.TooltipText = null;
            this.TechProcessReferenceColumn.Width = 150;
            // 
            // NoteColumn
            // 
            this.NoteColumn.Header = "Примечание";
            this.NoteColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.NoteColumn.TooltipText = null;
            this.NoteColumn.Width = 200;
            // 
            // SubstituteInfoColumn
            // 
            this.SubstituteInfoColumn.Header = "Замена";
            this.SubstituteInfoColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.SubstituteInfoColumn.TooltipText = null;
            this.SubstituteInfoColumn.Width = 150;
            // 
            // pcbColumn
            // 
            this.pcbColumn.Header = "Pcb";
            this.pcbColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.pcbColumn.TooltipText = null;
            // 
            // pcbVersionColumn
            // 
            this.pcbVersionColumn.Header = "Версия Pcb";
            this.pcbVersionColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.pcbVersionColumn.TooltipText = null;
            // 
            // techTaskColumn
            // 
            this.techTaskColumn.Header = "ТЗ";
            this.techTaskColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.techTaskColumn.TooltipText = null;
            this.techTaskColumn.Width = 250;
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataPropertyName = "Name";
            this.nameTextBox.IncrementalSearchEnabled = true;
            this.nameTextBox.LeftMargin = 3;
            this.nameTextBox.ParentColumn = this.NameColumn;
            // 
            // designationTextBox
            // 
            this.designationTextBox.DataPropertyName = "Designation";
            this.designationTextBox.IncrementalSearchEnabled = true;
            this.designationTextBox.LeftMargin = 3;
            this.designationTextBox.ParentColumn = this.DesignationColumn;
            // 
            // coopCheckBox
            // 
            this.coopCheckBox.DataPropertyName = "CooperationFlag";
            this.coopCheckBox.LeftMargin = 0;
            this.coopCheckBox.ParentColumn = this.CooperationColumn;
            // 
            // amountTextBox
            // 
            this.amountTextBox.DataPropertyName = "Amount";
            this.amountTextBox.IncrementalSearchEnabled = true;
            this.amountTextBox.LeftMargin = 3;
            this.amountTextBox.ParentColumn = this.AmountColumn;
            // 
            // amountWithUseTextBox
            // 
            this.amountWithUseTextBox.DataPropertyName = "AmountWithUse";
            this.amountWithUseTextBox.IncrementalSearchEnabled = true;
            this.amountWithUseTextBox.LeftMargin = 3;
            this.amountWithUseTextBox.ParentColumn = this.AmountWithUseColumn;
            // 
            // totalAmountTextBox
            // 
            this.totalAmountTextBox.DataPropertyName = "TotalAmount";
            this.totalAmountTextBox.IncrementalSearchEnabled = true;
            this.totalAmountTextBox.LeftMargin = 3;
            this.totalAmountTextBox.ParentColumn = this.TotalAmountColumn;
            // 
            // stockRateTextBox
            // 
            this.stockRateTextBox.DataPropertyName = "StockRate";
            this.stockRateTextBox.IncrementalSearchEnabled = true;
            this.stockRateTextBox.LeftMargin = 3;
            this.stockRateTextBox.ParentColumn = this.StockRateColumn;
            // 
            // sampleSizeTextBox
            // 
            this.sampleSizeTextBox.DataPropertyName = "SampleSize";
            this.sampleSizeTextBox.IncrementalSearchEnabled = true;
            this.sampleSizeTextBox.LeftMargin = 3;
            this.sampleSizeTextBox.ParentColumn = this.SampleSizeColumn;
            // 
            // techProcTextBox
            // 
            this.techProcTextBox.DataPropertyName = "TechProcessReference";
            this.techProcTextBox.IncrementalSearchEnabled = true;
            this.techProcTextBox.LeftMargin = 3;
            this.techProcTextBox.ParentColumn = this.TechProcessReferenceColumn;
            // 
            // noteTextBox
            // 
            this.noteTextBox.DataPropertyName = "Note";
            this.noteTextBox.IncrementalSearchEnabled = true;
            this.noteTextBox.LeftMargin = 3;
            this.noteTextBox.ParentColumn = this.NoteColumn;
            // 
            // subInfoTextBox
            // 
            this.subInfoTextBox.DataPropertyName = "SubstituteInfo";
            this.subInfoTextBox.IncrementalSearchEnabled = true;
            this.subInfoTextBox.LeftMargin = 3;
            this.subInfoTextBox.ParentColumn = this.SubstituteInfoColumn;
            // 
            // pcbCheckBox
            // 
            this.pcbCheckBox.DataPropertyName = "IsPcb";
            this.pcbCheckBox.LeftMargin = 0;
            this.pcbCheckBox.ParentColumn = this.pcbColumn;
            // 
            // pcbVersionTextbox
            // 
            this.pcbVersionTextbox.DataPropertyName = "PcbVersion";
            this.pcbVersionTextbox.IncrementalSearchEnabled = true;
            this.pcbVersionTextbox.LeftMargin = 3;
            this.pcbVersionTextbox.ParentColumn = this.pcbVersionColumn;
            // 
            // TechTaskTextBox
            // 
            this.TechTaskTextBox.DataPropertyName = "TechTask";
            this.TechTaskTextBox.IncrementalSearchEnabled = true;
            this.TechTaskTextBox.LeftMargin = 3;
            this.TechTaskTextBox.ParentColumn = this.techTaskColumn;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_report3_16x16_9951;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "ВК";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_report1_16x16_9945;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "ВТМ";
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ColumnHeaderHeight = 17;
            this.treeViewAdv1.Columns.Add(this.DesignationColumn);
            this.treeViewAdv1.Columns.Add(this.NameColumn);
            this.treeViewAdv1.Columns.Add(this.CooperationColumn);
            this.treeViewAdv1.Columns.Add(this.AmountColumn);
            this.treeViewAdv1.Columns.Add(this.AmountWithUseColumn);
            this.treeViewAdv1.Columns.Add(this.TotalAmountColumn);
            this.treeViewAdv1.Columns.Add(this.StockRateColumn);
            this.treeViewAdv1.Columns.Add(this.SampleSizeColumn);
            this.treeViewAdv1.Columns.Add(this.TechProcessReferenceColumn);
            this.treeViewAdv1.Columns.Add(this.NoteColumn);
            this.treeViewAdv1.Columns.Add(this.SubstituteInfoColumn);
            this.treeViewAdv1.Columns.Add(this.pcbColumn);
            this.treeViewAdv1.Columns.Add(this.pcbVersionColumn);
            this.treeViewAdv1.Columns.Add(this.techTaskColumn);
            this.treeViewAdv1.ContextMenuStrip = this.myContextMenuStrip;
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
            this.treeViewAdv1.NodeControls.Add(this.nameTextBox);
            this.treeViewAdv1.NodeControls.Add(this.designationTextBox);
            this.treeViewAdv1.NodeControls.Add(this.coopCheckBox);
            this.treeViewAdv1.NodeControls.Add(this.amountTextBox);
            this.treeViewAdv1.NodeControls.Add(this.amountWithUseTextBox);
            this.treeViewAdv1.NodeControls.Add(this.totalAmountTextBox);
            this.treeViewAdv1.NodeControls.Add(this.stockRateTextBox);
            this.treeViewAdv1.NodeControls.Add(this.sampleSizeTextBox);
            this.treeViewAdv1.NodeControls.Add(this.techProcTextBox);
            this.treeViewAdv1.NodeControls.Add(this.noteTextBox);
            this.treeViewAdv1.NodeControls.Add(this.subInfoTextBox);
            this.treeViewAdv1.NodeControls.Add(this.pcbCheckBox);
            this.treeViewAdv1.NodeControls.Add(this.pcbVersionTextbox);
            this.treeViewAdv1.NodeControls.Add(this.TechTaskTextBox);
            this.treeViewAdv1.NodeFilter = null;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv1.Size = new System.Drawing.Size(1412, 476);
            this.treeViewAdv1.TabIndex = 2;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            this.treeViewAdv1.RowDraw += new System.EventHandler<Aga.Controls.Tree.TreeViewRowDrawEventArgs>(this.TreeViewAdv1_RowDraw);
            // 
            // CooperationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 501);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.myToolStrip);
            this.Name = "CooperationView";
            this.Text = "CooperationForm";
            this.myToolStrip.ResumeLayout(false);
            this.myToolStrip.PerformLayout();
            this.myContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip myToolStrip;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ContextMenuStrip myContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CooperationButton;
        private System.Windows.Forms.ToolStripMenuItem DeleteCoopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SetTechProcessButton;
        private System.Windows.Forms.ToolStripMenuItem ResetTechProcessButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem SetNoteButton;
        private System.Windows.Forms.ToolStripMenuItem DeleteNoteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton CheckButtonClick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem SetParametersButton;
        private System.Windows.Forms.ToolStripButton SetAllCooperationButton;
        private System.Windows.Forms.ToolStripMenuItem showIntermechCardButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private Aga.Controls.Tree.TreeColumn NameColumn;
        private Aga.Controls.Tree.TreeColumn DesignationColumn;
        private Aga.Controls.Tree.TreeColumn CooperationColumn;
        private Aga.Controls.Tree.TreeColumn AmountColumn;
        private Aga.Controls.Tree.TreeColumn AmountWithUseColumn;
        private Aga.Controls.Tree.TreeColumn TotalAmountColumn;
        private Aga.Controls.Tree.TreeColumn StockRateColumn;
        private Aga.Controls.Tree.TreeColumn SampleSizeColumn;
        private Aga.Controls.Tree.TreeColumn TechProcessReferenceColumn;
        private Aga.Controls.Tree.TreeColumn NoteColumn;
        private Aga.Controls.Tree.TreeColumn SubstituteInfoColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nameTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox designationTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox amountTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox amountWithUseTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox totalAmountTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox stockRateTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox sampleSizeTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox techProcTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox noteTextBox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox subInfoTextBox;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox coopCheckBox;
        private Aga.Controls.Tree.TreeColumn pcbColumn;
        private System.Windows.Forms.ToolStripMenuItem GoToTheArchiveButton;
        private System.Windows.Forms.ToolStripMenuItem findInTreeNodeButton;
        private System.Windows.Forms.ToolStripButton CheckingModeOn;
        private System.Windows.Forms.ToolStripButton SearchInTreeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton ExpandAllButton;
        private System.Windows.Forms.ToolStripButton collapseAllButton;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox pcbCheckBox;
        private Aga.Controls.Tree.TreeColumn pcbVersionColumn;
        private Aga.Controls.Tree.TreeColumn techTaskColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox pcbVersionTextbox;
        private Aga.Controls.Tree.NodeControls.NodeTextBox TechTaskTextBox;
        private System.Windows.Forms.ToolStripMenuItem SetTechTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetPcbMenuItem;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
    }
}