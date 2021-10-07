using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FileAnalyzer;

namespace FileProcessorsTests
{
    [TestClass]
    public class FileProcessorFactoryTests
    {
        [TestMethod]
        public void ShouldReturnTsvProcessorForFileWithTsvExtension()
        {
            const string file = "test.tsv";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file);
                Assert.IsInstanceOfType(processor, typeof(TSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldReturnCsvProcessorForFileWithCsvExtension()
        {
            const string file = "test.csv";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file);
                Assert.IsInstanceOfType(processor, typeof(CSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldReturnCsvProcessorForFileWithOtherExtensions()
        {
            const string file = "test.txt";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file);
                Assert.IsInstanceOfType(processor, typeof(CSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldReturnCsvProcessorForFileWithWithoutExtension()
        {
            const string file = "test";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file);
                Assert.IsInstanceOfType(processor, typeof(CSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldReturnTsvProcessorIfFileTypeParameterIsTsv()
        {
            const string file = "test.csv";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file, FileType.TSV);
                Assert.IsInstanceOfType(processor, typeof(TSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldReturnCsvProcessorIfFileTypeParameterIsCsv()
        {
            const string file = "test.tsv";
            using (File.Create(file))
            {
                var processor = FileProcessorFactory.GetFileProcessor(file, FileType.CSV);
                Assert.IsInstanceOfType(processor, typeof(CSVFileProcessor));
            }
            File.Delete(file);
        }

        [TestMethod]
        public void ShouldThrowExceptionIfFileNotExists()
        {
            const string file = "file.x";
            Assert.ThrowsException<Exception>(
                () => FileProcessorFactory.GetFileProcessor(file), 
                $"File '{file}' does not exist");
        }

        [TestMethod]
        public void ShouldThrowExceptionIfFileTypeParameterIsInvalid()
        {
            const string file = "test.tsv";
            using (File.Create(file))
            {
                Assert.ThrowsException<NotSupportedException>(
                    () => FileProcessorFactory.GetFileProcessor(file, (FileType)10));
            }
            File.Delete(file);
        }
    }
}
