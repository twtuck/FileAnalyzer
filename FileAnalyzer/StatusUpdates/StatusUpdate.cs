namespace FileAnalyzer
{
    /// <summary>
    /// Stores the percentage, column headers and current row values extracted from a file
    /// </summary>
    public class StatusUpdate : IStatusUpdate
    {
        /// <summary>
        /// The current line/row number
        /// </summary>
        public int RowId { get; set; }

        /// <summary>
        /// The percentage of file content processed so far
        /// </summary>
        public int ProcessPercentage { get; set; }
        
        /// <summary>
        /// The information of the detected/auto-generated column headers
        /// </summary>
        public ColumnHeader[] ColumnHeaders { get; set; }

        /// <summary>
        /// The values extracted from the current line/row
        /// </summary>
        public string[] Values { get; set; }
    }
}