namespace FileAnalyzer
{
    public class StatusUpdate : IStatusUpdate
    {
        public int RowId { get; set; }
        public int ProcessPercentage { get; set; }
        public ColumnHeader[] ColumnHeaders { get; set; }
        public string[] Values { get; set; }
    }
}