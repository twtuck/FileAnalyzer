using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Represents the name and type of each column
    /// </summary>
    public class ColumnHeader
    {
        public string Name { get; set; }
        public ColumnType? Type { get; set; }
    }
}