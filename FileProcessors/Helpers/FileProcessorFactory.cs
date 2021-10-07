using System;
using System.IO;

namespace FileAnalyzer
{
    /// <summary>
    /// Used to supply a file processor based on the extension of the specified file
    /// </summary>
    public static class FileProcessorFactory
    {
        /// <summary>
        /// Supply a file processor based on the extension of the specified file.
        /// 
        /// </summary>
        /// <param name="inputFile">The full path of the file to be processed</param>
        /// <param name="fileType">If specified, a file processor of the specified type will be return. Otherwise, a file processor based on the file extension will be returned. (TSV processor for .tsv, CSV processor for any other extensions)</param>
        /// <returns>Returns an interface to a concrete file processor</returns>
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