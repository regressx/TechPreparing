namespace NavisElectronics.TechPreparation.Main
{
    partial class MdiMainForm
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
            this.toolStripPanelTop = new System.Windows.Forms.ToolStripPanel();
            this.formsToolStrip = new System.Windows.Forms.ToolStrip();
            this.workWithRoutesButton = new System.Windows.Forms.ToolStripButton();
            this.servicesToolStrip = new System.Windows.Forms.ToolStrip();
            this.compareTwoTreesButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStrip = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.checkOkButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripPanelTop.SuspendLayout();
            this.formsToolStrip.SuspendLayout();
            this.servicesToolStrip.SuspendLayout();
            this.saveToolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripPanelTop
            // 
            this.toolStripPanelTop.Controls.Add(this.formsToolStrip);
            this.toolStripPanelTop.Controls.Add(this.servicesToolStrip);
            this.toolStripPanelTop.Controls.Add(this.saveToolStrip);
            this.toolStripPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripPanelTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanelTop.Name = "toolStripPanelTop";
            this.toolStripPanelTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanelTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanelTop.Size = new System.Drawing.Size(882, 25);
            // 
            // formsToolStrip
            // 
            this.formsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.formsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workWithRoutesButton});
            this.formsToolStrip.Location = new System.Drawing.Point(3, 0);
            this.formsToolStrip.Name = "formsToolStrip";
            this.formsToolStrip.Size = new System.Drawing.Size(33, 25);
            this.formsToolStrip.TabIndex = 0;
            // 
            // workWithRoutesButton
            // 
            this.workWithRoutesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.workWithRoutesButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.RouteObject;
            this.workWithRoutesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.workWithRoutesButton.Name = "workWithRoutesButton";
            this.workWithRoutesButton.Size = new System.Drawing.Size(23, 22);
            this.workWithRoutesButton.Text = "Ведомость тех. маршрутов";
            // 
            // servicesToolStrip
            // 
            this.servicesToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.servicesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compareTwoTreesButton,
            this.checkOkButton});
            this.servicesToolStrip.Location = new System.Drawing.Point(36, 0);
            this.servicesToolStrip.Name = "servicesToolStrip";
            this.servicesToolStrip.Size = new System.Drawing.Size(56, 25);
            this.servicesToolStrip.TabIndex = 1;
            // 
            // compareTwoTreesButton
            // 
            this.compareTwoTreesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compareTwoTreesButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_forest_16;
            this.compareTwoTreesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compareTwoTreesButton.Name = "compareTwoTreesButton";
            this.compareTwoTreesButton.Size = new System.Drawing.Size(23, 22);
            this.compareTwoTreesButton.Text = "Сравнить два дерева";
            // 
            // saveToolStrip
            // 
            this.saveToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.saveToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton});
            this.saveToolStrip.Location = new System.Drawing.Point(92, 0);
            this.saveToolStrip.Name = "saveToolStrip";
            this.saveToolStrip.Size = new System.Drawing.Size(33, 25);
            this.saveToolStrip.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.if_stock_save_20659;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton3";
            // 
            // checkOkButton
            // 
            this.checkOkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.checkOkButton.Image = global::NavisElectronics.TechPreparation.Properties.Resources.icons8_ok_16;
            this.checkOkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkOkButton.Name = "checkOkButton";
            this.checkOkButton.Size = new System.Drawing.Size(23, 22);
            this.checkOkButton.Text = "toolStripButton4";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripProgressBar1});
            this.statusStrip.Location = new System.Drawing.Point(0, 439);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(882, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(76, 17);
            this.statusLabel.Text = "Не сохранено";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // MdiMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 461);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripPanelTop);
            this.IsMdiContainer = true;
            this.Name = "MdiMainForm";
            this.Text = "Редактор технологических ведомостей";
            this.toolStripPanelTop.ResumeLayout(false);
            this.toolStripPanelTop.PerformLayout();
            this.formsToolStrip.ResumeLayout(false);
            this.formsToolStrip.PerformLayout();
            this.servicesToolStrip.ResumeLayout(false);
            this.servicesToolStrip.PerformLayout();
            this.saveToolStrip.ResumeLayout(false);
            this.saveToolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel toolStripPanelTop;
        private System.Windows.Forms.ToolStrip formsToolStrip;
        private System.Windows.Forms.ToolStripButton workWithRoutesButton;
        private System.Windows.Forms.ToolStrip servicesToolStrip;
        private System.Windows.Forms.ToolStripButton compareTwoTreesButton;
        private System.Windows.Forms.ToolStrip saveToolStrip;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton checkOkButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}