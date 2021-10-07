namespace FileAnalyzer
{
    /// <summary>
    /// Represents a row/line in the processed file where the number of values is not equal to the number of columns detected
    /// </summary>
    public class InconsistentRow
    {
        /// <summary>
        /// The line number at which the inconsistent row/line is found
        /// </summary>
        public int RowId { get; }

        /// <summary>
        /// The original value of the inconsistent row/line
        /// </summary>
        public string Value { get; }

        public InconsistentRow(int rowId, string value)
        {
            RowId = rowId;
            Value = value;
        }
    }
}