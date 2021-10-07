using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileAnalyzer
{
    public abstract class FileProcessorBase : IFileProcessor
    {
        // To cancel the processing
        protected CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        // Used to keep track of the percentage of contents that have been processed
        protected CompletionTracker CompletionTracker;

        // To keep the lines/words/characters count in the file being processed
        protected Statistics InternalStatistics = new Statistics();

        // Keeps the information of the column headers
        protected ColumnHeader[] ColumnHeaders;

        // The full path of the file being processed
        public string FilePath { get; }

        // Exposes the interface to access the statistics
        public IStatistics Statistics => InternalStatistics;

        // The separator character that separates a line into multiple values
        protected abstract char Separator { get; }

        protected FileProcessorBase(string filePath)
        {
            var inputFileInfo = new FileInfo(filePath);
            if (!inputFileInfo.Exists)
                throw new Exception($"File '{filePath}' does not exist");
            FilePath = filePath;

            // initialize the completion tracker with the target file size
            CompletionTracker = new CompletionTracker(inputFileInfo.Length);
        }

        private ProcessingResult ProcessFile(INotification notification, CancellationToken cancellationToken, bool firstRowIsHeaders)
        {
            // keep tracks of row number
            var rowId = 0;

            // indicate whether the first non-empty row in the file has been found
            var firstNonEmptyRowFound = false;

            // start measuring the time spent on processing the file
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // open the file stream to read in line by line
                using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (var streamReader = new StreamReader(fileStream))
                {
                    while (true)
                    {
                        // allow the thread to sleep for an arbitrary amount of time in each loop, to avoid 
                        // hogging the CPU when processing a large file (to avoid an non-responsive UI)
                        if (cancellationToken.WaitHandle.WaitOne(2))
                        {
                            // notify cancellation triggered by user
                            notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = 0 });
                            return ProcessingResult.Cancelled;
                        }

                        var line = streamReader.ReadLine();
                        if (line == null)
                        {
                            // no more line to read
                            notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = 100, ColumnHeaders = ColumnHeaders });
                            return ProcessingResult.Complete;
                        }

                        try
                        {
                            rowId++;

                            // check whether the current line is the first non-empty line
                            if (!firstNonEmptyRowFound && !string.IsNullOrWhiteSpace(line))
                            {
                                // found first non-empty line
                                firstNonEmptyRowFound = true;

                                // check whether the option to treat first row as headers is enabled
                                if (firstRowIsHeaders)
                                {
                                    // Process the current line as the column headers 
                                    var headerLineLength = ProcessHeaders(line);

                                    // add the number of bytes from the current line to the completion tracker
                                    CompletionTracker.UpdateCompletion(headerLineLength);

                                    // notify the current completion percentage
                                    notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = CompletionTracker.CurrentPercentage });
                                    continue;
                                }
                            }

                            // parse the the current line
                            var (lineLength, values) = ProcessValues(line);

                            // add the number of bytes from the current line to the completion tracker
                            CompletionTracker.UpdateCompletion(lineLength);

                            // notify the current completion percentage, together with the column headers and values from current line
                            notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = CompletionTracker.CurrentPercentage, Values = values, ColumnHeaders = ColumnHeaders });
                        }
                        catch (Exception ex)
                        {
                            if (!(ex is IndexOutOfRangeException) && !(ex is InconsistentRowException))
                            {
                                throw;
                            }

                            // notify encountering a line with different number values than the expected columns
                            notification?.NotifyInconsistentRow(new InconsistentRow(rowId, line));
                        }
                    }
                }
            }
            finally
            {
                stopwatch.Stop();

                // update time taken to process the file into the statistics
                InternalStatistics.ProcessingTime = stopwatch.Elapsed;
            }
        }

        private int ProcessHeaders(string line)
        {
            // update number of lines processed in the statistics
            InternalStatistics.NumberOfLines++;

            // no processing for empty line, return 2 to represent the length of CR/LF
            if (string.IsNullOrWhiteSpace(line))
                return 2; 

            // split the line with the configured separator and store the values as column header names
            var headers = line.Split(Separator);
            if (headers.Length > 0)
            {
                ColumnHeaders = headers.Select(h => new ColumnHeader { Name = h.Trim() }).ToArray();
            }

            // return the length of the line, assuming the line ends with CR/LF
            return line.Length + 2; 
        }

        private (int lineLength, string[] values) ProcessValues(string line)
        {
            // update number of lines processed in the statistics
            InternalStatistics.NumberOfLines++;

            // no processing for empty line, return 2 to represent the length of CR/LF
            if (string.IsNullOrWhiteSpace(line))
                return (2, null);

            // split the given line with the configured separator
            var values = line.Split(Separator);

            // if column headers have already been extracted/generated, check whether the current line has
            // the same number of values as the number of columns
            if (ColumnHeaders != null && values.Length != ColumnHeaders.Length)
            {
                throw new InconsistentRowException();
            }

            // check if the current line contains separated values
            if (values.Length > 0)
            {
                if (ColumnHeaders == null)
                {
                    // Column names have not been populated from any line before, generate generic column names
                    ColumnHeaders = Enumerable.Range(1, values.Length)
                        .Select(n => new ColumnHeader { Name = $"Column_{n}" }).ToArray();
                }

                // walk through each separated values
                for (var i = 0; i < values.Length; i++)
                {
                    var value = values[i];

                    // update statistics based on the current value
                    InternalStatistics.NumberOfCharsWithSpace += value.Length;
                    InternalStatistics.NumberOfCharsWithoutSpace += value.Replace(" ", "").Length;
                    InternalStatistics.NumberOfWords += value.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries).Length;

                    if (ColumnHeaders[i].Type == null)
                    {
                        // Determine the column type based on the current value
                        ColumnHeaders[i].Type = ValueTypes.GetType(value);
                    }
                }
            }

            // return the length of the line and the split values, assuming the line ends with CR/LF
            return (lineLength: line.Length + 2, values); 
        }

        public async Task<ProcessingResult> Start(INotification notification, bool firstRowIsHeaders)
        {
            // starts processing the file in a new task
            return await Task.Run(() => ProcessFile(notification, CancellationTokenSource.Token, firstRowIsHeaders));
        }

        public void Cancel()
        {
            // Signal the processing loop to stop 
            CancellationTokenSource.Cancel();
        }
    }
}