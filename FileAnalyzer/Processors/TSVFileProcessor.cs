using System;
using System.Linq;

namespace FileAnalyzer
{
    public class TSVFileProcessor : FileProcessorBase
    {
        public TSVFileProcessor(string inputFile) : base(inputFile)
        {
        }

        protected override char Separator => '\t';
    }
}