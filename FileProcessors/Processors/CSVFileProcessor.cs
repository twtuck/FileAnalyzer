using System;
using System.Linq;

namespace FileAnalyzer
{
    /// <summary>
    /// A concrete implementation of FileProcessorBase, used to process a comma-separated file
    /// </summary>
    public class CSVFileProcessor : FileProcessorBase
    {
        public CSVFileProcessor(string filePath) : base(filePath)
        {
        }

        protected override char Separator => ',';
    }
}