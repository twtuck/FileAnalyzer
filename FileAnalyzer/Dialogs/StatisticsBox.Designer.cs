
namespace FileAnalyzer
{
    partial class StatisticsBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxCharsWithSpaceCount = new System.Windows.Forms.TextBox();
            this.textBoxCharsNoSpaceCount = new System.Windows.Forms.TextBox();
            this.textBoxWordsCount = new System.Windows.Forms.TextBox();
            this.labelLines = new System.Windows.Forms.Label();
            this.labelWords = new System.Windows.Forms.Label();
            this.labelCharsNoSpace = new System.Windows.Forms.Label();
            this.labelCharsWithSpace = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.textBoxLinesCount = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel.Controls.Add(this.textBoxCharsWithSpaceCount, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxCharsNoSpaceCount, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.textBoxWordsCount, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelLines, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelWords, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCharsNoSpace, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCharsWithSpace, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.textBoxLinesCount, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(14, 14);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(324, 211);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // textBoxCharsWithSpaceCount
            // 
            this.textBoxCharsWithSpaceCount.Enabled = false;
            this.textBoxCharsWithSpaceCount.Location = new System.Drawing.Point(219, 123);
            this.textBoxCharsWithSpaceCount.Name = "textBoxCharsWithSpaceCount";
            this.textBoxCharsWithSpaceCount.Size = new System.Drawing.Size(102, 26);
            this.textBoxCharsWithSpaceCount.TabIndex = 28;
            this.textBoxCharsWithSpaceCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCharsNoSpaceCount
            // 
            this.textBoxCharsNoSpaceCount.Enabled = false;
            this.textBoxCharsNoSpaceCount.Location = new System.Drawing.Point(219, 83);
            this.textBoxCharsNoSpaceCount.Name = "textBoxCharsNoSpaceCount";
            this.textBoxCharsNoSpaceCount.Size = new System.Drawing.Size(102, 26);
            this.textBoxCharsNoSpaceCount.TabIndex = 27;
            this.textBoxCharsNoSpaceCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxWordsCount
            // 
            this.textBoxWordsCount.Enabled = false;
            this.textBoxWordsCount.Location = new System.Drawing.Point(219, 43);
            this.textBoxWordsCount.Name = "textBoxWordsCount";
            this.textBoxWordsCount.Size = new System.Drawing.Size(102, 26);
            this.textBoxWordsCount.TabIndex = 26;
            this.textBoxWordsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLines
            // 
            this.labelLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLines.Location = new System.Drawing.Point(9, 0);
            this.labelLines.Margin = new System.Windows.Forms.Padding(9, 0, 4, 0);
            this.labelLines.MaximumSize = new System.Drawing.Size(0, 26);
            this.labelLines.Name = "labelLines";
            this.labelLines.Size = new System.Drawing.Size(203, 26);
            this.labelLines.TabIndex = 19;
            this.labelLines.Text = "Lines Processed";
            this.labelLines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWords
            // 
            this.labelWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWords.Location = new System.Drawing.Point(9, 40);
            this.labelWords.Margin = new System.Windows.Forms.Padding(9, 0, 4, 0);
            this.labelWords.MaximumSize = new System.Drawing.Size(0, 26);
            this.labelWords.Name = "labelWords";
            this.labelWords.Size = new System.Drawing.Size(203, 26);
            this.labelWords.TabIndex = 0;
            this.labelWords.Text = "Words";
            this.labelWords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCharsNoSpace
            // 
            this.labelCharsNoSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCharsNoSpace.Location = new System.Drawing.Point(9, 80);
            this.labelCharsNoSpace.Margin = new System.Windows.Forms.Padding(9, 0, 4, 0);
            this.labelCharsNoSpace.MaximumSize = new System.Drawing.Size(0, 26);
            this.labelCharsNoSpace.Name = "labelCharsNoSpace";
            this.labelCharsNoSpace.Size = new System.Drawing.Size(203, 26);
            this.labelCharsNoSpace.TabIndex = 21;
            this.labelCharsNoSpace.Text = "Characters (without space)";
            this.labelCharsNoSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCharsWithSpace
            // 
            this.labelCharsWithSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCharsWithSpace.Location = new System.Drawing.Point(9, 120);
            this.labelCharsWithSpace.Margin = new System.Windows.Forms.Padding(9, 0, 4, 0);
            this.labelCharsWithSpace.MaximumSize = new System.Drawing.Size(0, 26);
            this.labelCharsWithSpace.Name = "labelCharsWithSpace";
            this.labelCharsWithSpace.Size = new System.Drawing.Size(203, 26);
            this.labelCharsWithSpace.TabIndex = 22;
            this.labelCharsWithSpace.Text = "Characters (with space)";
            this.labelCharsWithSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(100, 172);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 34);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // textBoxLinesCount
            // 
            this.textBoxLinesCount.Enabled = false;
            this.textBoxLinesCount.Location = new System.Drawing.Point(219, 3);
            this.textBoxLinesCount.Name = "textBoxLinesCount";
            this.textBoxLinesCount.Size = new System.Drawing.Size(102, 26);
            this.textBoxLinesCount.TabIndex = 25;
            this.textBoxLinesCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StatisticsBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(352, 239);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatisticsBox";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Statistics";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelLines;
        private System.Windows.Forms.Label labelWords;
        private System.Windows.Forms.Label labelCharsNoSpace;
        private System.Windows.Forms.Label labelCharsWithSpace;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textBoxCharsWithSpaceCount;
        private System.Windows.Forms.TextBox textBoxCharsNoSpaceCount;
        private System.Windows.Forms.TextBox textBoxWordsCount;
        private System.Windows.Forms.TextBox textBoxLinesCount;
    }
}
