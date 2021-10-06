namespace FileAnalyzer
{
    public interface IStatusUpdate
    {
        int RowId { get; }
        int ProcessPercentage { get; }
        ColumnHeader[] ColumnHeaders { get; }
        string[] Values { get; }
    }
}