using System.Threading.Tasks;

namespace FileAnalyzer
{
    /// <summary>
    /// Allows controlling of the start and cancellation of a file processor
    /// </summary>
    public interface IFileProcessor
    {
        /// <summary>
        /// Triggers the start of file processing 
        /// </summary>
        /// <param name="notification">If supplied, will be used by the file processor to notify status updates of the processing</param>
        /// <param name="firstRowIsHeaders">Specifies whether the processor should extract the column headers from the first non-empty line in the file. Default: false</param>
        /// <returns></returns>
        Task<ProcessingResult> Start(INotification notification, bool firstRowIsHeaders = false);

        /// <summary>
        /// Cancel any in-progress file processing
        /// </summary>
        void Cancel();

        /// <summary>
        /// Returns the full path of the file being processed by the file processor
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Returns the statistics of the file processed
        /// </summary>
        IStatistics Statistics { get; }
    }
}