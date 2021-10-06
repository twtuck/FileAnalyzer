using System;
using System.Linq;

namespace FileAnalyzer
{
    public class CSVFileProcessor : FileProcessorBase
    {
        public CSVFileProcessor(string inputFile) : base(inputFile)
        {
        }

        protected override char Separator => ',';
    }
}