using System;
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
            var rowId = 0;
            var firstNonEmptyRowFound = false;

            // open the file stream to read in line by line
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var streamReader = new StreamReader(fileStream))
            {
                while (true)
                {
                    // allow the thread to sleep for an arbitrary amount of time in each loop, to avoid the
                    // hogging the CPU when processing a large file (to avoid an non-responsive UI)
                    if (cancellationToken.WaitHandle.WaitOne(2))
                    {
                        // cancel triggered by user
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

                        if (!firstNonEmptyRowFound && !string.IsNullOrWhiteSpace(line))
                        {
                            // found first non-empty line
                            firstNonEmptyRowFound = true;
                            if (firstRowIsHeaders)
                            {
                                // Process the current line as the column headers 
                                var headerLineLength = ProcessHeaders(line);
                                CompletionTracker.UpdateCompletion(headerLineLength);
                                notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = CompletionTracker.CurrentPercentage });
                                continue;
                            }
                        }

                        var (lineLength, values) = ProcessValues(line);
                        CompletionTracker.UpdateCompletion(lineLength);
                        notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = CompletionTracker.CurrentPercentage, Values = values, ColumnHeaders = ColumnHeaders });
                    }
                    catch (Exception ex)
                    {
                        if (!(ex is IndexOutOfRangeException) && !(ex is InconsistentRowException))
                        {
                            throw;
                        }

                        notification?.NotifyInconsistentRow(new InconsistentRow(rowId, line));
                    }
                }
            }
        }

        private int ProcessHeaders(string line)
        {
            InternalStatistics.NumberOfLines++;
            if (string.IsNullOrWhiteSpace(line))
                return 2; // assume the line ends with CR/LF

            var headers = line.Split(Separator);
            if (headers.Length > 0)
            {
                ColumnHeaders = headers.Select(h => new ColumnHeader { Name = h.Trim() }).ToArray();
            }

            return line.Length + 2; // assume the line ends with CR/LF
        }

        /// <summary>
        /// Split the given line with the configured separator
        /// </summary>
        /// <param name="line">The line to be processed</param>
        /// <returns>the length of the line and the split values</returns>
        private (int lineLength, string[] values) ProcessValues(string line)
        {
            InternalStatistics.NumberOfLines++;
            if (string.IsNullOrWhiteSpace(line))
                return (2, null);

            var values = line.Split(Separator);
            if (ColumnHeaders != null && values.Length != ColumnHeaders.Length)
            {
                throw new InconsistentRowException();
            }

            if (values.Length > 0)
            {
                if (ColumnHeaders == null)
                {
                    // Column names have not been populated from any line before, generate generic column names
                    ColumnHeaders = Enumerable.Range(1, values.Length)
                        .Select(n => new ColumnHeader { Name = $"Column_{n}" }).ToArray();
                }

                for (var i = 0; i < values.Length; i++)
                {
                    var value = values[i];
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

            // assume each line ends with CR/LF
            return (lineLength: line.Length + 2, values); 
        }

        public async Task<ProcessingResult> Start(INotification notification, bool firstRowIsHeaders)
        {
            return await Task.Run(() => ProcessFile(notification, CancellationTokenSource.Token, firstRowIsHeaders));
        }

        public void Cancel()
        {
            CancellationTokenSource.Cancel();
        }

    }

    public static class ValueTypes
    {
        public static ColumnType GetType(string value)
        {
            if (DateTime.TryParse(value, out _))
            {
                return ColumnType.DateTime;
            }
            
            if (bool.TryParse(value, out _))
            {
                return ColumnType.Boolean;
            }

            if (double.TryParse(value, out _))
            {
                return ColumnType.Numeric;
            }

            return ColumnType.String;
        }
    }
}