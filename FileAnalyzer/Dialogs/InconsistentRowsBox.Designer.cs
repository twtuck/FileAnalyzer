
namespace FileAnalyzer
{
    partial class InconsistentRowsBox
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
            System.Windows.Forms.ColumnHeader columnHeaderRowId;
            System.Windows.Forms.ColumnHeader columnHeaderValue;
            this.okButton = new System.Windows.Forms.Button();
            this.listViewInconsistentRows = new System.Windows.Forms.ListView();
            columnHeaderRowId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // columnHeaderRowId
            // 
            columnHeaderRowId.Text = "Row";
            // 
            // columnHeaderValue
            // 
            columnHeaderValue.Text = "Value";
            columnHeaderValue.Width = 819;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(410, 388);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 34);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "&OK";
            // 
            // listViewInconsistentRows
            // 
            this.listViewInconsistentRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderRowId,
            columnHeaderValue});
            this.listViewInconsistentRows.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewInconsistentRows.HideSelection = false;
            this.listViewInconsistentRows.Location = new System.Drawing.Point(14, 14);
            this.listViewInconsistentRows.Name = "listViewInconsistentRows";
            this.listViewInconsistentRows.Size = new System.Drawing.Size(906, 360);
            this.listViewInconsistentRows.TabIndex = 26;
            this.listViewInconsistentRows.UseCompatibleStateImageBehavior = false;
            this.listViewInconsistentRows.View = System.Windows.Forms.View.Details;
            // 
            // InconsistentRowsBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(934, 435);
            this.Controls.Add(this.listViewInconsistentRows);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InconsistentRowsBox";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inconsistent Rows";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListView listViewInconsistentRows;
    }
}
