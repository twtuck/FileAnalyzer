using System;
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
        // Used to process the selected file
        private IFileProcessor _fileProcessor;

        // Used to pass status updates to the UI thread
        private IProgress<IStatusUpdate> _progress;

        // Store a list of row values that consists of different number of columns vs the detected column headers
        private readonly List<InconsistentRow> _inconsistentRows = new List<InconsistentRow>();

        // Store the column headers detected from the import file
        private ColumnHeader[] _columnHeaders;

        // Store a combined description of the data type for each column
        private string _columnDataTypes;

        // Used by the result list view to sort items by selected column
        private readonly ListViewItemComparer _listViewItemComparer = new ListViewItemComparer();

        // Indicates whether a file processing is in progress
        private bool _processing;

        public MainForm()
        {
            InitializeComponent();
            listViewResults.DoubleBuffered(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // resize the controls based on the initial size when the main form is loaded
            ResizeControls();

            var progress = new Progress<IStatusUpdate>();
            progress.ProgressChanged += (_, statusUpdate) =>
            {
                // Update status text and progress bar based on status updates from the file processor
                labelStatus.Text = string.Format(Properties.Resources.MainForm_Status_Label_Processing, statusUpdate.RowId);
                progressBar.Value = statusUpdate.ProcessPercentage;

                // Populate the column headers and values into the result list view
                PopulateListView(statusUpdate.ColumnHeaders, statusUpdate.Values);
            };
            _progress = progress;
        }

        // Update UI components and initialize local variables before processing starts
        private void UpdateBeforeRun()
        {
            // update UI states
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

            // clears inconsistent rows and column headers of previous file
            _inconsistentRows.Clear();
            _columnHeaders = null;

            // remove the sorting comparer from the result list view, to disable sorting
            // while the file is processed and the list view is populated
            listViewResults.ListViewItemSorter = null;

            // reset the sorting options
            _listViewItemComparer.Reset();

            // mark the start of processing
            _processing = true;
        }

        // Update UI components after processing is complete or cancelled
        private void UpdateAfterRun(ProcessingResult processingResult, string errorMessage = null)
        {
            // clear progress bar after processing stops
            progressBar.Value = 0;

            // update UI states
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
            linkLabelViewStatistics.Visible = processingResult == ProcessingResult.Complete;
            linkLabelViewInconsistentRows.Visible = _inconsistentRows.Count > 0;

            // marks end of file processing
            _processing = false;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            // resize the ui controls when the main form is resized
            ResizeControls();
        }

        // Resize UI controls to fit the client area
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
            // cancel any ongoing file processing when the form is closed
            _fileProcessor?.Cancel();
        }

        public void NotifyStatus(IStatusUpdate statusUpdate)
        {
            // pass the status update notification back to the UI thread
            _progress.Report(statusUpdate);
        }

        public void NotifyInconsistentRow(InconsistentRow inconsistentRow)
        {
            // pass the inconsistent row info back to the UI thread
            _inconsistentRows.Add(inconsistentRow);
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            // Launch a file select dialog for the user to select an input file
            var dialogResult = browseInputFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                // update the input file text box when user confirms a file selection 
                textBoxInputFile.Text = browseInputFileDialog.FileName;
            }
        }

        private async void ButtonImport_Click(object sender, EventArgs e)
        {
            if (buttonImport.Text == Properties.Resources.MainForm_Process_Label_Import)
            {
                // start importing asynchronously
                await DoImport();
            }
            else
            {
                // the button is currently in Cancel mode (i.e. a file processing) is running
                // clicking on this button now indicates the intention to cancel the current processing
                _fileProcessor?.Cancel();
            }
        }

        private async Task DoImport()
        {
            // check if an input file has been specified
            if (string.IsNullOrEmpty(textBoxInputFile.Text))
            {
                MessageBox.Show(@"Please specify a CSV file to import", @"File not specified", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                // check if the specified file path constitutes of a file name
                if (string.IsNullOrEmpty(Path.GetFileName(textBoxInputFile.Text)))
                {
                    MessageBox.Show(@"Please specify a valid file path", @"Invalid file name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                UpdateBeforeRun();

                // Cancel any ongoing file processing
                _fileProcessor?.Cancel();

                // Retrieve a file processor and start processing the specified input file
                _fileProcessor = FileProcessorFactory.GetFileProcessor(textBoxInputFile.Text);
                var processingResult = await _fileProcessor.Start(this, checkBoxFirstRowHeaders.Checked);

                UpdateAfterRun(processingResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Input file error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                UpdateAfterRun(ProcessingResult.Failed, ex.Message);
            }
        }

        private void LinkLabelViewStatistics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // nothing to show if file processor or statistics is not available
            var statistics = _fileProcessor?.Statistics;
            if (statistics == null)
                return;

            // gets the full path of the file processed by the file processor
            var filename = _fileProcessor?.FilePath;
            if (!string.IsNullOrEmpty(filename))
            {
                // extract only the filename from the full path
                filename = Path.GetFileName(filename);
            }

            // initialize and launch the statistics dialog box
            var statisticsBox = new StatisticsBox(statistics, filename);
            statisticsBox.ShowDialog();
        }

        // populates the result list view with column headers and values
        private void PopulateListView(ColumnHeader[] columnHeaders, string[] values)
        {
            if (listViewResults.Columns.Count == 0 && columnHeaders != null)
            {
                // new column headers info are supplied and 
                // the column headers in the result list view is not yet populated
                _columnHeaders = columnHeaders;
                var stringBuilder = new StringBuilder();

                // insert the column headers into the result list view and generate a summary description of the column types
                foreach (var columnHeader in columnHeaders)
                {
                    listViewResults.Columns.Add(columnHeader.Name, 150);
                    stringBuilder.AppendFormat("{0}: {1}", columnHeader.Name, columnHeader.Type);
                    stringBuilder.AppendLine();
                }

                _columnDataTypes = stringBuilder.ToString();
            }

            if (values?.Length > 0)
            {
                // row values are supplied, so insert the values as a new row in the result list view
                var item = listViewResults.Items.Add(values[0]);
                item.ToolTipText = _columnDataTypes;
                if (values.Length > 1)
                {
                    item.SubItems.AddRange(values.Skip(1).ToArray());
                }
            }
        }

        private void LinkLabelViewInconsistentRows_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // gets the full path of the file processed by the file processor
            var filename = _fileProcessor?.FilePath;
            if (!string.IsNullOrEmpty(filename))
            {
                // extract only the filename from the full path
                filename = Path.GetFileName(filename);
            }

            // initialize and launch the inconsistent rows dialog box
            var inconsistentRowsBox = new InconsistentRowsBox(_inconsistentRows, filename);
            inconsistentRowsBox.ShowDialog();
        }

        private void ListViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_processing)
            {
                // file processing in ongoing, do not allow column sorting
                return;
            }

            // check if clicked column is already sorted
            if (e.Column == _listViewItemComparer.SortColumn)
            {
                // reverse the sort order of the clicked column
                _listViewItemComparer.Order = 
                    _listViewItemComparer.Order == SortOrder.Ascending ? 
                        SortOrder.Descending : 
                        SortOrder.Ascending;
            }
            else
            {
                // a different column is clicked, so set the current column index as
                // the column number to be sorted and default the sort order to ascending
                _listViewItemComparer.SortColumn = e.Column;
                _listViewItemComparer.Order = SortOrder.Ascending;
            }

            // specify the type of the column to be sorted, assume the column is string if column type info is not available
            _listViewItemComparer.ColumnType = _columnHeaders?[e.Column].Type?? ColumnType.String;

            // assign the comparer to the result list view
            listViewResults.ListViewItemSorter = _listViewItemComparer;

            // perform the sort with the updated sort options
            listViewResults.Sort();
        }
    }
}
