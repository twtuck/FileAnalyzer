using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAnalyzer
{
    public partial class MainForm : Form, INotification
    {
        private IFileProcessor _fileProcessor;
        private IProgress<IStatusUpdate> _progress;
        private readonly List<InconsistentRow> _inconsistentRows = new List<InconsistentRow>();
        private ColumnHeader[] _columnHeaders;
        private string _toolTipText;
        private readonly ListViewItemComparer _listViewItemComparer;

        public MainForm()
        {
            InitializeComponent();
            listViewResults.DoubleBuffered(true);
            _listViewItemComparer = new ListViewItemComparer();
            listViewResults.ListViewItemSorter = _listViewItemComparer;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ResizeControls();
            var progress = new Progress<IStatusUpdate>();
            progress.ProgressChanged += (_, statusUpdate) =>
            {
                labelStatus.Text = string.Format(Properties.Resources.MainForm_Status_Label_Processing, statusUpdate.RowId);
                progressBar.Value = statusUpdate.ProcessPercentage;
                PopulateListView(statusUpdate.ColumnHeaders, statusUpdate.Values);
            };
            _progress = progress;
        }

        /// <summary>
        ///  Update UI components before processing starts
        /// </summary>
        private void UpdateUIBeforeRun()
        {
            progressBar.Value = 0;
            buttonImport.Text = Properties.Resources.MainForm_Process_Label_Cancel;
            labelStatus.Text = Properties.Resources.MainForm_Status_Label_Running;
            buttonBrowse.Enabled = false;
            textBoxInputFile.Enabled = false;
            checkBoxFirstRowHeaders.Enabled = false;
            linkLabelViewStatistics.Visible = false;
            linkLabelViewInconsistentRows.Visible = false;
            listViewResults.Items.Clear();
            listViewResults.Columns.Clear();
            listViewResults.Enabled = false;
            _listViewItemComparer.SortColumn = 0;
            _listViewItemComparer.Order = SortOrder.Ascending;
            _listViewItemComparer.ColumnType = ColumnType.String;

        }

        /// <summary>
        /// Update UI components after processing is complete or cancelled
        /// </summary>
        private void UpdateUIAfterRun(ProcessingResult processingResult, string errorMessage = null)
        {
            progressBar.Value = 0;
            buttonImport.Text = Properties.Resources.MainForm_Process_Label_Import;
            switch (processingResult)
            {
                case ProcessingResult.Complete:
                    labelStatus.Text = Properties.Resources.MainForm_Status_Label_Complete;
                    break;
                case ProcessingResult.Cancelled:
                    labelStatus.Text = Properties.Resources.MainForm_Status_Label_Cancelled;
                    break;
                case ProcessingResult.Failed:
                    labelStatus.Text = string.Format(Properties.Resources.MainForm_Status_Label_Failed, errorMessage);
                    break;
            }
            buttonBrowse.Enabled = true;
            textBoxInputFile.Enabled = true;
            checkBoxFirstRowHeaders.Enabled = true;
            listViewResults.Enabled = true;
            linkLabelViewStatistics.Visible = processingResult == ProcessingResult.Complete;
            linkLabelViewInconsistentRows.Visible = _inconsistentRows.Count > 0;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            ResizeControls();
        }

        /// <summary>
        /// Resize controls to fit the client area
        /// </summary>
        private void ResizeControls()
        {
            listViewResults.Top = 30;
            listViewResults.Left = 0;
            listViewResults.Size = new Size(ClientSize.Width, ClientSize.Height - 50);
            labelStatus.Top = linkLabelViewStatistics.Top = linkLabelViewInconsistentRows.Top = ClientSize.Height - 18;
            progressBar.Top = ClientSize.Height - 20;
            progressBar.Size = new Size(ClientSize.Width - 180, 20);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // cancel any ongoing processing
            _fileProcessor?.Cancel();
        }

        public void NotifyStatus(IStatusUpdate statusUpdate)
        {
            _progress.Report(statusUpdate);
        }

        public void NotifyInconsistentRow(InconsistentRow inconsistentRow)
        {
            _inconsistentRows.Add(inconsistentRow);
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            // Launch a file select dialog for the user to select an input file
            var dialogResult = browseInputFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textBoxInputFile.Text = browseInputFileDialog.FileName;
            }
        }

        private async void ButtonImport_Click(object sender, EventArgs e)
        {
            if (buttonImport.Text == Properties.Resources.MainForm_Process_Label_Import)
            {
                await DoImport();
            }
            else
            {
                _fileProcessor?.Cancel();
            }
        }

        private async Task DoImport()
        {
            // check if a file name has been specified
            if (string.IsNullOrEmpty(textBoxInputFile.Text))
            {
                MessageBox.Show(@"Please specify a CSV file to import", @"File not specified", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                // check if the specified file constitutes of a file name
                if (string.IsNullOrEmpty(Path.GetFileName(textBoxInputFile.Text)))
                {
                    MessageBox.Show(@"Please specify a valid file path", @"Invalid file name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                UpdateUIBeforeRun();
                _inconsistentRows.Clear();
                _columnHeaders = null;

                // Cancel any processing currently running
                _fileProcessor?.Cancel();

                // Initial a new processor and start processing the specified input file
                _fileProcessor = FileProcessorFactory.GetFileProcessor(textBoxInputFile.Text);
                var processingResult = await _fileProcessor.Start(this, checkBoxFirstRowHeaders.Checked);

                UpdateUIAfterRun(processingResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Input file error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                UpdateUIAfterRun(ProcessingResult.Failed, ex.Message);
                return;
            }
        }

        private void LinkLabelViewStatistics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var statistics = _fileProcessor?.Statistics;
            if (statistics == null)
                return;

            var filename = _fileProcessor?.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                // extract only the filename from the full path
                filename = Path.GetFileName(filename);
            }

            var statisticsBox = new StatisticsBox(statistics, filename);
            statisticsBox.ShowDialog();
        }

        private void PopulateListView(ColumnHeader[] columnHeaders, string[] values)
        {
            if (listViewResults.Columns.Count == 0 && columnHeaders != null)
            {
                _columnHeaders = columnHeaders;
                var stringBuilder = new StringBuilder();

                foreach (var columnHeader in columnHeaders)
                {
                    listViewResults.Columns.Add(columnHeader.Name, 150);
                    stringBuilder.AppendFormat("{0}: {1}", columnHeader.Name, columnHeader.Type);
                    stringBuilder.AppendLine();
                }

                _toolTipText = stringBuilder.ToString();
            }

            if (values?.Length > 0)
            {
                var item = listViewResults.Items.Add(values[0]);
                item.ToolTipText = _toolTipText;
                if (values.Length > 1)
                {
                    item.SubItems.AddRange(values.Skip(1).ToArray());
                }
            }
        }

        private void LinkLabelViewInconsistentRows_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var filename = _fileProcessor?.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                // extract only the filename from the full path
                filename = Path.GetFileName(filename);
            }

            var inconsistentRowsBox = new InconsistentRowsBox(_inconsistentRows, filename);
            inconsistentRowsBox.ShowDialog();
        }

        private void ListViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _listViewItemComparer.SortColumn)
            {
                // Reverse the current sort direction for this column.
                _listViewItemComparer.Order = 
                    _listViewItemComparer.Order == SortOrder.Ascending ? 
                        SortOrder.Descending : 
                        SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _listViewItemComparer.SortColumn = e.Column;
                _listViewItemComparer.Order = SortOrder.Ascending;
            }

            _listViewItemComparer.ColumnType = _columnHeaders?[e.Column].Type?? ColumnType.String;

            // Perform the sort with these new sort options.
            listViewResults.Sort();
        }
    }
}
