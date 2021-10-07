using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Stores the line, word and character counts of a processed file
    /// </summary>
    public class Statistics : IStatistics
    {
        public int NumberOfLines { get; set; }
        public int NumberOfWords { get; set; }
        public int NumberOfCharsWithSpace { get; set; }
        public int NumberOfCharsWithoutSpace { get; set; }
        public TimeSpan ProcessingTime { get; set;  }
    }
}