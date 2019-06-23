namespace NavisElectronics.TechPreparation.Views
{
    partial class ParametersView
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
            this.applyButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sampleSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stockRateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockRateNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.applyButton.Location = new System.Drawing.Point(68, 106);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Принять";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(149, 106);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Коэф. зап";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Выборка, %";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.sampleSizeNumericUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.applyButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.stockRateNumericUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.closeButton, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 135);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // sampleSizeNumericUpDown
            // 
            this.sampleSizeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.sampleSizeNumericUpDown.Location = new System.Drawing.Point(149, 66);
            this.sampleSizeNumericUpDown.Name = "sampleSizeNumericUpDown";
            this.sampleSizeNumericUpDown.Size = new System.Drawing.Size(141, 20);
            this.sampleSizeNumericUpDown.TabIndex = 1;
            this.sampleSizeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // stockRateNumericUpDown
            // 
            this.stockRateNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.stockRateNumericUpDown.DecimalPlaces = 2;
            this.stockRateNumericUpDown.Location = new System.Drawing.Point(149, 15);
            this.stockRateNumericUpDown.Name = "stockRateNumericUpDown";
            this.stockRateNumericUpDown.Size = new System.Drawing.Size(141, 20);
            this.stockRateNumericUpDown.TabIndex = 0;
            this.stockRateNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ParametersView
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(293, 135);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ParametersView";
            this.Text = "Задайте значения";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockRateNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown sampleSizeNumericUpDown;
        private System.Windows.Forms.NumericUpDown stockRateNumericUpDown;
    }
}