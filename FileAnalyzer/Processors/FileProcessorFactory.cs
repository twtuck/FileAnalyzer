using System;
using System.IO;

namespace FileAnalyzer
{
    public enum FileType
    {
        CSV,
        TSV
    }

    public static class FileProcessorFactory
    {
        public static IFileProcessor GetFileProcessor(string inputFile, FileType? fileType = null)
        {
            if (fileType == null)
            {
                // if fileType not specified, check file extension
                // default to csv if extension is not tsv
                var extension = Path.GetExtension(inputFile);
                if (string.Equals(extension, ".tsv", StringComparison.CurrentCultureIgnoreCase))
                    fileType = FileType.TSV;
                else
                    fileType = FileType.CSV;
            }

            switch (fileType)
            {
                case FileType.CSV:
                    return new CSVFileProcessor(inputFile);
                case FileType.TSV:
                    return new TSVFileProcessor(inputFile);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}