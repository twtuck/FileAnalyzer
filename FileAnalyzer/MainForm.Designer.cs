
namespace FileAnalyzer
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
            this.labelInputFile = new System.Windows.Forms.Label();
            this.textBoxInputFile = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.browseInputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxFirstRowHeaders = new System.Windows.Forms.CheckBox();
            this.linkLabelViewStatistics = new System.Windows.Forms.LinkLabel();
            this.linkLabelViewInconsistentRows = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelInputFile
            // 
            this.labelInputFile.AutoSize = true;
            this.labelInputFile.Location = new System.Drawing.Point(18, 12);
            this.labelInputFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInputFile.Name = "labelInputFile";
            this.labelInputFile.Size = new System.Drawing.Size(75, 20);
            this.labelInputFile.TabIndex = 0;
            this.labelInputFile.Text = "Input File";
            // 
            // textBoxInputFile
            // 
            this.textBoxInputFile.Location = new System.Drawing.Point(101, 8);
            this.textBoxInputFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxInputFile.Name = "textBoxInputFile";
            this.textBoxInputFile.Size = new System.Drawing.Size(551, 26);
            this.textBoxInputFile.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(663, 5);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(88, 35);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(757, 5);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(88, 35);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.HideSelection = false;
            this.listViewResults.Location = new System.Drawing.Point(-2, 51);
            this.listViewResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewResults.MultiSelect = false;
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.ShowItemToolTips = true;
            this.listViewResults.Size = new System.Drawing.Size(1178, 650);
            this.listViewResults.TabIndex = 4;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewResults_ColumnClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(430, 708);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(695, 31);
            this.progressBar.Step = 100;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 5;
            // 
            // browseInputFileDialog
            // 
            this.browseInputFileDialog.Filter = "CSV files|*.csv|TSV files|*.tsv|All files|*.*";
            this.browseInputFileDialog.RestoreDirectory = true;
            this.browseInputFileDialog.Title = "Select CSV file to import";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(18, 718);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(55, 20);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Ready";
            // 
            // checkBoxFirstRowHeaders
            // 
            this.checkBoxFirstRowHeaders.AutoSize = true;
            this.checkBoxFirstRowHeaders.Checked = true;
            this.checkBoxFirstRowHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFirstRowHeaders.Location = new System.Drawing.Point(863, 10);
            this.checkBoxFirstRowHeaders.Name = "checkBoxFirstRowHeaders";
            this.checkBoxFirstRowHeaders.Size = new System.Drawing.Size(178, 24);
            this.checkBoxFirstRowHeaders.TabIndex = 7;
            this.checkBoxFirstRowHeaders.Text = "First row as headers";
            this.checkBoxFirstRowHeaders.UseVisualStyleBackColor = true;
            // 
            // linkLabelViewStatistics
            // 
            this.linkLabelViewStatistics.AutoSize = true;
            this.linkLabelViewStatistics.Location = new System.Drawing.Point(112, 718);
            this.linkLabelViewStatistics.Name = "linkLabelViewStatistics";
            this.linkLabelViewStatistics.Size = new System.Drawing.Size(112, 20);
            this.linkLabelViewStatistics.TabIndex = 8;
            this.linkLabelViewStatistics.TabStop = true;
            this.linkLabelViewStatistics.Text = "View Statistics";
            this.linkLabelViewStatistics.Visible = false;
            this.linkLabelViewStatistics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelViewStatistics_LinkClicked);
            // 
            // linkLabelViewInconsistentRows
            // 
            this.linkLabelViewInconsistentRows.AutoSize = true;
            this.linkLabelViewInconsistentRows.Location = new System.Drawing.Point(241, 718);
            this.linkLabelViewInconsistentRows.Name = "linkLabelViewInconsistentRows";
            this.linkLabelViewInconsistentRows.Size = new System.Drawing.Size(178, 20);
            this.linkLabelViewInconsistentRows.TabIndex = 9;
            this.linkLabelViewInconsistentRows.TabStop = true;
            this.linkLabelViewInconsistentRows.Text = "View Inconsistent Rows";
            this.linkLabelViewInconsistentRows.Visible = false;
            this.linkLabelViewInconsistentRows.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelViewInconsistentRows_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 743);
            this.Controls.Add(this.linkLabelViewInconsistentRows);
            this.Controls.Add(this.linkLabelViewStatistics);
            this.Controls.Add(this.checkBoxFirstRowHeaders);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxInputFile);
            this.Controls.Add(this.labelInputFile);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1192, 739);
            this.Name = "MainForm";
            this.Text = "File Analyzer - CSV/TSV";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInputFile;
        private System.Windows.Forms.TextBox textBoxInputFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.OpenFileDialog browseInputFileDialog;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxFirstRowHeaders;
        private System.Windows.Forms.LinkLabel linkLabelViewStatistics;
        private System.Windows.Forms.LinkLabel linkLabelViewInconsistentRows;
    }
}

