namespace FileAnalyzer
{
    public interface IStatistics
    {
        int NumberOfLines { get; }
        int NumberOfWords { get; }
        int NumberOfCharsWithSpace { get; }
        int NumberOfCharsWithoutSpace { get; }
    }
}