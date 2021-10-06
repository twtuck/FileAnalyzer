namespace FileAnalyzer
{
    public class InconsistentRow
    {
        public int RowId { get; }
        public string Value { get; }

        public InconsistentRow(int rowId, string value)
        {
            RowId = rowId;
            Value = value;
        }
    }
}