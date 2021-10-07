namespace FileAnalyzer
{
    /// <summary>
    /// Provides an interface for the file processor to send notifications of status updates and inconsistent rows
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// To notify an update in the processing status
        /// </summary>
        /// <param name="statusUpdate">New update in the processing status</param>
        void NotifyStatus(IStatusUpdate statusUpdate);

        /// <summary>
        /// To notify an inconsistent row/line detected in a file
        /// </summary>
        /// <param name="inconsistentRow">The information of the inconsistent row/line</param>
        void NotifyInconsistentRow(InconsistentRow inconsistentRow);
    }
}