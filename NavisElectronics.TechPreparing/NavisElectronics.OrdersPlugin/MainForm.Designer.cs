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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.produceButton = new System.Windows.Forms.ToolStripMenuItem();
            this.produceInCurrentNodeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.produceInAllTreeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotProduceButton = new System.Windows.Forms.ToolStripMenuItem();
            this.notProdInCurrentNodeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.notProdInTreeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createReportButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.saveOrderButton = new System.Windows.Forms.ToolStripButton();
            this.saveInfoLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.loadAndUpdateButtonStrip = new System.Windows.Forms.ToolStripButton();
            this.DecryptDocumentsButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.designationColumn = new Aga.Controls.Tree.TreeColumn();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.firstParentColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountColumn = new Aga.Controls.Tree.TreeColumn();
            this.amountWithUseColumn = new Aga.Controls.Tree.TreeColumn();
            this.letterColumn = new Aga.Controls.Tree.TreeColumn();
            this.changeNumberColumn = new Aga.Controls.Tree.TreeColumn();
            this.changeDocumentColumn = new Aga.Controls.Tree.TreeColumn();
            this.noteColumn = new Aga.Controls.Tree.TreeColumn();
            this.statusColumn = new Aga.Controls.Tree.TreeColumn();
            this.baseVersionSign = new Aga.Controls.Tree.TreeColumn();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox3 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox4 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox5 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox6 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox7 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox8 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox9 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox10 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox11 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produceButton,
            this.doNotProduceButton,
            this.toolStripSeparator1,
            this.createReportButton});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(246, 76);
            // 
            // produceButton
            // 
            this.produceButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produceInCurrentNodeButton,
            this.produceInAllTreeButton});
            this.produceButton.Name = "produceButton";
            this.produceButton.Size = new System.Drawing.Size(245, 22);
            this.produceButton.Text = "Изготавливать";
            // 
            // produceInCurrentNodeButton
            // 
            this.produceInCurrentNodeButton.Name = "produceInCurrentNodeButton";
            this.produceInCurrentNodeButton.Size = new System.Drawing.Size(171, 22);
            this.produceInCurrentNodeButton.Text = "только в этом узле";
            this.produceInCurrentNodeButton.Click += new System.EventHandler(this.produceInCurrentNodeButton_Click);
            // 
            // produceInAllTreeButton
            // 
            this.produceInAllTreeButton.Name = "produceInAllTreeButton";
            this.produceInAllTreeButton.Size = new System.Drawing.Size(171, 22);
            this.produceInAllTreeButton.Text = "во всём дереве";
            this.produceInAllTreeButton.Click += new System.EventHandler(this.produceInAllTreeButton_Click);
            // 
            // doNotProduceButton
            // 
            this.doNotProduceButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notProdInCurrentNodeButton,
            this.notProdInTreeButton});
            this.doNotProduceButton.Image = global::NavisElectronics.Orders.Properties.Resources.DeleteNode;
            this.doNotProduceButton.Name = "doNotProduceButton";
            this.doNotProduceButton.Size = new System.Drawing.Size(245, 22);
            this.doNotProduceButton.Text = "Не изготавливать";
            // 
            // notProdInCurrentNodeButton
            // 
            this.notProdInCurrentNodeButton.Name = "notProdInCurrentNodeButton";
            this.notProdInCurrentNodeButton.Size = new System.Drawing.Size(171, 22);
            this.notProdInCurrentNodeButton.Text = "только в этом узле";
            this.notProdInCurrentNodeButton.Click += new System.EventHandler(this.notProdInCurrentNodeButton_Click);
            // 
            // notProdInTreeButton
            // 
            this.notProdInTreeButton.Name = "notProdInTreeButton";
            this.notProdInTreeButton.Size = new System.Drawing.Size(171, 22);
            this.notProdInTreeButton.Text = "во всём дереве";
            this.notProdInTreeButton.Click += new System.EventHandler(this.notProdInTreeButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // createReportButton
            // 
            this.createReportButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.createReportButton.Enabled = false;
            this.createReportButton.Name = "createReportButton";
            this.createReportButton.Size = new System.Drawing.Size(245, 22);
            this.createReportButton.Text = "Создать отчет по составу заказа";
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
            this.toolStripMenuItem6.Click += new System.EventHandler(this.CreateReportClickExcel_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveOrderButton,
            this.saveInfoLabel,
            this.toolStripProgressBar1,
            this.toolStripButton1,
            this.loadAndUpdateButtonStrip,
            this.DecryptDocumentsButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1335, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // saveOrderButton
            // 
            this.saveOrderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveOrderButton.Image = global::NavisElectronics.Orders.Properties.Resources.Save;
            this.saveOrderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveOrderButton.Name = "saveOrderButton";
            this.saveOrderButton.Size = new System.Drawing.Size(23, 22);
            this.saveOrderButton.Text = "Сохранить";
            this.saveOrderButton.Click += new System.EventHandler(this.SaveOrderButton_Click);
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
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::NavisElectronics.Orders.Properties.Resources.OK;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Начать проверку";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // loadAndUpdateButtonStrip
            // 
            this.loadAndUpdateButtonStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadAndUpdateButtonStrip.Image = global::NavisElectronics.Orders.Properties.Resources.Download;
            this.loadAndUpdateButtonStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadAndUpdateButtonStrip.Name = "loadAndUpdateButtonStrip";
            this.loadAndUpdateButtonStrip.Size = new System.Drawing.Size(23, 22);
            this.loadAndUpdateButtonStrip.Text = "Загрузка и обновление данных";
            this.loadAndUpdateButtonStrip.Click += new System.EventHandler(this.LoadAndUpdateButtonStrip_Click);
            // 
            // DecryptDocumentsButton
            // 
            this.DecryptDocumentsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecryptDocumentsButton.Image = ((System.Drawing.Image)(resources.GetObject("DecryptDocumentsButton.Image")));
            this.DecryptDocumentsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecryptDocumentsButton.Name = "DecryptDocumentsButton";
            this.DecryptDocumentsButton.Size = new System.Drawing.Size(23, 22);
            this.DecryptDocumentsButton.Text = "Расшифровать названия документов";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 410);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1335, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ColumnHeaderHeight = 22;
            this.treeViewAdv.Columns.Add(this.designationColumn);
            this.treeViewAdv.Columns.Add(this.nameColumn);
            this.treeViewAdv.Columns.Add(this.firstParentColumn);
            this.treeViewAdv.Columns.Add(this.amountColumn);
            this.treeViewAdv.Columns.Add(this.amountWithUseColumn);
            this.treeViewAdv.Columns.Add(this.letterColumn);
            this.treeViewAdv.Columns.Add(this.changeNumberColumn);
            this.treeViewAdv.Columns.Add(this.changeDocumentColumn);
            this.treeViewAdv.Columns.Add(this.noteColumn);
            this.treeViewAdv.Columns.Add(this.statusColumn);
            this.treeViewAdv.Columns.Add(this.baseVersionSign);
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
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox1);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox2);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox3);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox4);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox5);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox6);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox7);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox8);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox9);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox10);
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox11);
            this.treeViewAdv.NodeFilter = null;
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(1335, 385);
            this.treeViewAdv.TabIndex = 3;
            this.treeViewAdv.Text = "treeViewAdv";
            this.treeViewAdv.UseColumns = true;
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
            // firstParentColumn
            // 
            this.firstParentColumn.Header = "Перв. прим.";
            this.firstParentColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.firstParentColumn.TooltipText = null;
            this.firstParentColumn.Width = 150;
            // 
            // amountColumn
            // 
            this.amountColumn.Header = "Кол-во";
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
            // letterColumn
            // 
            this.letterColumn.Header = "Лит.";
            this.letterColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.letterColumn.TooltipText = null;
            // 
            // changeNumberColumn
            // 
            this.changeNumberColumn.Header = "Изм.";
            this.changeNumberColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.changeNumberColumn.TooltipText = null;
            // 
            // changeDocumentColumn
            // 
            this.changeDocumentColumn.Header = "Извещение";
            this.changeDocumentColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.changeDocumentColumn.TooltipText = null;
            this.changeDocumentColumn.Width = 100;
            // 
            // noteColumn
            // 
            this.noteColumn.Header = "Примечание";
            this.noteColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.noteColumn.TooltipText = null;
            this.noteColumn.Width = 250;
            // 
            // statusColumn
            // 
            this.statusColumn.Header = "Статус";
            this.statusColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.statusColumn.TooltipText = null;
            this.statusColumn.Width = 100;
            // 
            // baseVersionSign
            // 
            this.baseVersionSign.Header = "Базовая версия";
            this.baseVersionSign.SortOrder = System.Windows.Forms.SortOrder.None;
            this.baseVersionSign.TooltipText = null;
            this.baseVersionSign.Width = 75;
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "Designation";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = this.designationColumn;
            // 
            // nodeTextBox2
            // 
            this.nodeTextBox2.DataPropertyName = "Name";
            this.nodeTextBox2.IncrementalSearchEnabled = true;
            this.nodeTextBox2.LeftMargin = 3;
            this.nodeTextBox2.ParentColumn = this.nameColumn;
            // 
            // nodeTextBox3
            // 
            this.nodeTextBox3.DataPropertyName = "FirstUse";
            this.nodeTextBox3.IncrementalSearchEnabled = true;
            this.nodeTextBox3.LeftMargin = 3;
            this.nodeTextBox3.ParentColumn = this.firstParentColumn;
            // 
            // nodeTextBox4
            // 
            this.nodeTextBox4.DataPropertyName = "Status";
            this.nodeTextBox4.IncrementalSearchEnabled = true;
            this.nodeTextBox4.LeftMargin = 3;
            this.nodeTextBox4.ParentColumn = this.statusColumn;
            // 
            // nodeTextBox5
            // 
            this.nodeTextBox5.DataPropertyName = "BaseVersionSign";
            this.nodeTextBox5.IncrementalSearchEnabled = true;
            this.nodeTextBox5.LeftMargin = 3;
            this.nodeTextBox5.ParentColumn = this.baseVersionSign;
            // 
            // nodeTextBox6
            // 
            this.nodeTextBox6.DataPropertyName = "Amount";
            this.nodeTextBox6.IncrementalSearchEnabled = true;
            this.nodeTextBox6.LeftMargin = 3;
            this.nodeTextBox6.ParentColumn = this.amountColumn;
            // 
            // nodeTextBox7
            // 
            this.nodeTextBox7.DataPropertyName = "AmountWithUse";
            this.nodeTextBox7.IncrementalSearchEnabled = true;
            this.nodeTextBox7.LeftMargin = 3;
            this.nodeTextBox7.ParentColumn = this.amountWithUseColumn;
            // 
            // nodeTextBox8
            // 
            this.nodeTextBox8.DataPropertyName = "Letter";
            this.nodeTextBox8.IncrementalSearchEnabled = true;
            this.nodeTextBox8.LeftMargin = 3;
            this.nodeTextBox8.ParentColumn = this.letterColumn;
            // 
            // nodeTextBox9
            // 
            this.nodeTextBox9.DataPropertyName = "ChangeNumber";
            this.nodeTextBox9.IncrementalSearchEnabled = true;
            this.nodeTextBox9.LeftMargin = 3;
            this.nodeTextBox9.ParentColumn = this.changeNumberColumn;
            // 
            // nodeTextBox10
            // 
            this.nodeTextBox10.DataPropertyName = "ChangeDocument";
            this.nodeTextBox10.IncrementalSearchEnabled = true;
            this.nodeTextBox10.LeftMargin = 3;
            this.nodeTextBox10.ParentColumn = this.changeDocumentColumn;
            // 
            // nodeTextBox11
            // 
            this.nodeTextBox11.DataPropertyName = "Note";
            this.nodeTextBox11.IncrementalSearchEnabled = true;
            this.nodeTextBox11.LeftMargin = 3;
            this.nodeTextBox11.ParentColumn = this.noteColumn;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 432);
            this.Controls.Add(this.treeViewAdv);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainForm";
            this.Text = "Редактор состава заказа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem doNotProduceButton;
        private System.Windows.Forms.ToolStripMenuItem createReportButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton saveOrderButton;
        private System.Windows.Forms.ToolStripLabel saveInfoLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem produceButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton loadAndUpdateButtonStrip;
        private System.Windows.Forms.ToolStripMenuItem notProdInCurrentNodeButton;
        private System.Windows.Forms.ToolStripMenuItem notProdInTreeButton;
        private System.Windows.Forms.ToolStripMenuItem produceInCurrentNodeButton;
        private System.Windows.Forms.ToolStripMenuItem produceInAllTreeButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private Aga.Controls.Tree.TreeColumn changeNumberColumn;
        private Aga.Controls.Tree.TreeColumn changeDocumentColumn;
        private Aga.Controls.Tree.TreeColumn noteColumn;
        private Aga.Controls.Tree.TreeColumn letterColumn;
        private Aga.Controls.Tree.TreeColumn amountWithUseColumn;
        private Aga.Controls.Tree.TreeColumn amountColumn;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn designationColumn;
        private Aga.Controls.Tree.TreeColumn firstParentColumn;
        private Aga.Controls.Tree.TreeColumn statusColumn;
        private Aga.Controls.Tree.TreeColumn baseVersionSign;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox3;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox4;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox5;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox6;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox7;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox8;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox9;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox10;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox11;
        private System.Windows.Forms.ToolStripButton DecryptDocumentsButton;
    }
}