using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Exposes the interface to access the line, word and character counts of a processed file
    /// </summary>
    public interface IStatistics
    {
        int NumberOfLines { get; }
        int NumberOfWords { get; }
        int NumberOfCharsWithSpace { get; }
        int NumberOfCharsWithoutSpace { get; }
        TimeSpan ProcessingTime { get; } 
    }
}