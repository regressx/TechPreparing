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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearManufacturerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.refreshTreeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.relationNameColumn = new Aga.Controls.Tree.TreeColumn();
            this.relationNoteColumn = new Aga.Controls.Tree.TreeColumn();
            this.nodeTextBoxDesignation = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAmount = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAmountWithUse = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxRelationName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxRelationNote = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OrganizationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearManufacturerButton});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(223, 26);
            // 
            // ClearManufacturerButton
            // 
            this.ClearManufacturerButton.Name = "ClearManufacturerButton";
            this.ClearManufacturerButton.Size = new System.Drawing.Size(222, 22);
            this.ClearManufacturerButton.Text = "Очистить изготовителя узла";
            this.ClearManufacturerButton.Click += new System.EventHandler(this.ClearManufacturerButton_Click);
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
            this.MainMaterialsButton.Enabled = false;
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
            this.standartsButton.Enabled = false;
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
            this.SetTechWithdrawalButton.Enabled = false;
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
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(1062, 294);
            this.splitContainer2.SplitterDistance = 753;
            this.splitContainer2.TabIndex = 7;
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ColumnHeaderHeight = 17;
            this.treeViewAdv.Columns.Add(this.designationTreeColumn);
            this.treeViewAdv.Columns.Add(this.nameTreeColumn);
            this.treeViewAdv.Columns.Add(this.amounTreeColumn);
            this.treeViewAdv.Columns.Add(this.amountWithUseTreeColumn);
            this.treeViewAdv.Columns.Add(this.relationNameColumn);
            this.treeViewAdv.Columns.Add(this.relationNoteColumn);
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxRelationName);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBoxRelationNote);
            this.treeViewAdv.NodeFilter = null;
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(753, 294);
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
            // relationNameColumn
            // 
            this.relationNameColumn.Header = "Связь";
            this.relationNameColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.relationNameColumn.TooltipText = null;
            this.relationNameColumn.Width = 100;
            // 
            // relationNoteColumn
            // 
            this.relationNoteColumn.Header = "Примечание";
            this.relationNoteColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.relationNoteColumn.TooltipText = null;
            this.relationNoteColumn.Width = 200;
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
            // nodeTextBoxRelationName
            // 
            this.nodeTextBoxRelationName.DataPropertyName = "RelationName";
            this.nodeTextBoxRelationName.IncrementalSearchEnabled = true;
            this.nodeTextBoxRelationName.LeftMargin = 3;
            this.nodeTextBoxRelationName.ParentColumn = this.relationNameColumn;
            // 
            // nodeTextBoxRelationNote
            // 
            this.nodeTextBoxRelationNote.DataPropertyName = "RelationNote";
            this.nodeTextBoxRelationNote.IncrementalSearchEnabled = true;
            this.nodeTextBoxRelationNote.LeftMargin = 3;
            this.nodeTextBoxRelationNote.ParentColumn = this.relationNoteColumn;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrganizationName,
            this.Status});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(305, 294);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // OrganizationName
            // 
            this.OrganizationName.HeaderText = "Наименование организации";
            this.OrganizationName.Name = "OrganizationName";
            this.OrganizationName.ReadOnly = true;
            this.OrganizationName.Width = 200;
            // 
            // Status
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Status.DefaultCellStyle = dataGridViewCellStyle1;
            this.Status.HeaderText = "Статус";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem ClearManufacturerButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ToolStripButton TechRoutesEditButton;
        private System.Windows.Forms.ToolStripButton MainMaterialsButton;
        private System.Windows.Forms.ToolStripButton standartsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton UpdateButton;
        private System.Windows.Forms.ToolStripButton SetTechWithdrawalButton;
        private System.Windows.Forms.ToolStripButton refreshTreeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton CheckReadyButton;
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrganizationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private Aga.Controls.Tree.TreeColumn relationNameColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxRelationName;
        private Aga.Controls.Tree.TreeColumn relationNoteColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxRelationNote;
    }
}