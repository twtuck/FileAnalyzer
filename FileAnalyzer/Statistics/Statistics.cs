namespace FileAnalyzer
{
    public class Statistics : IStatistics
    {
        public int NumberOfLines { get; set; }
        public int NumberOfWords { get; set; }
        public int NumberOfCharsWithSpace { get; set; }
        public int NumberOfCharsWithoutSpace { get; set; }
    }
}