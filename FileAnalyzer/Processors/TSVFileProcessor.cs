using System;
using System.Linq;

namespace FileAnalyzer
{
    /// <summary>
    /// A concrete implementation of FileProcessorBase, used to process a tab-separated file
    /// </summary>
    public class TSVFileProcessor : FileProcessorBase
    {
        public TSVFileProcessor(string inputFile) : base(inputFile)
        {
        }

        protected override char Separator => '\t';
    }
}