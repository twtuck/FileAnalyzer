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

        public string FileName { get; }

        // Exposes the interface to access the statistics
        public IStatistics Statistics => InternalStatistics;

        protected abstract char Separator { get; }

        protected FileProcessorBase(string inputFile)
        {
            var inputFileInfo = new FileInfo(inputFile);
            if (!inputFileInfo.Exists)
                throw new Exception($"File '{inputFile}' does not exist");
            FileName = inputFile;
            CompletionTracker = new CompletionTracker(inputFileInfo.Length);
        }

        private ProcessingResult ProcessFile(INotification notification, CancellationToken cancellationToken, bool firstRowIsHeaders)
        {
            var rowId = 0;
            var firstNonEmptyRowFound = false;

            using (var fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            using (var streamReader = new StreamReader(fileStream))
            {
                while (true)
                {
                    if (cancellationToken.WaitHandle.WaitOne(50))
                    {
                        // cancel triggered by user
                        notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = 0 });
                        return ProcessingResult.Cancelled;
                    }

                    var line = streamReader.ReadLine();
                    if (line == null)
                    {
                        // no more line to read
                        notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = 100 });
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

                        var (lineLength, values, columnHeaders) = ProcessValues(line);
                        CompletionTracker.UpdateCompletion(lineLength);
                        notification?.NotifyStatus(new StatusUpdate { RowId = rowId, ProcessPercentage = CompletionTracker.CurrentPercentage, Values = values, ColumnHeaders = columnHeaders });
                    }
                    catch (IndexOutOfRangeException)
                    {
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


        private (int lineLength, string[] values, ColumnHeader[] columnHeaders) ProcessValues(string line)
        {
            InternalStatistics.NumberOfLines++;
            if (string.IsNullOrWhiteSpace(line))
                return (2, null, null);

            var values = line.Split(Separator);

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
            return (lineLength: line.Length + 2, values, ColumnHeaders); 
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

    public enum ColumnType
    {
        String,
        Boolean,
        Numeric,
        DateTime
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