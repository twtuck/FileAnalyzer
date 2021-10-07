using System;
using System.Linq;

namespace FileAnalyzer
{
    /// <summary>
    /// A concrete implementation of FileProcessorBase, used to process a comma-separated file
    /// </summary>
    public class CSVFileProcessor : FileProcessorBase
    {
        public CSVFileProcessor(string inputFile) : base(inputFile)
        {
        }

        protected override char Separator => ',';
    }
}