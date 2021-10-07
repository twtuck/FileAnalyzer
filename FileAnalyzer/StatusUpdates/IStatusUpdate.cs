namespace FileAnalyzer
{
    /// <summary>
    /// An interface to access the status update from a file processor
    /// </summary>
    public interface IStatusUpdate
    {
        /// <summary>
        /// The current line/row number
        /// </summary>
        int RowId { get; }

        /// <summary>
        /// The percentage of file content processed so far
        /// </summary>
        int ProcessPercentage { get; }

        /// <summary>
        /// The information of the detected/auto-generated column headers
        /// </summary>
        ColumnHeader[] ColumnHeaders { get; }

        /// <summary>
        /// The values extracted from the current line/row
        /// </summary>
        string[] Values { get; }
    }
}