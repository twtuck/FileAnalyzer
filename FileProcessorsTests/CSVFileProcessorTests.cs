using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using FileAnalyzer;
using Moq;

namespace FileProcessorsTests
{
    [TestClass]
    public class CSVFileProcessorTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfFileNotExists()
        {
            const string file = "file.x";
            Assert.ThrowsException<Exception>(
                () => new CSVFileProcessor(file), 
                $"File '{file}' does not exist");
        }

        [TestMethod]
        public void ProcessCsvFileWithEmptyContent()
        {
            const string file = "test.csv";
            var fileStream = File.Create(file);
            fileStream.Close();

            var processor = new CSVFileProcessor(file);
            var result = processor.Start(null, true).Result;
            Assert.AreEqual(file, processor.FilePath);
            Assert.AreEqual(ProcessingResult.Complete, result);
            Assert.AreEqual(0, processor.Statistics.NumberOfLines);
            Assert.AreEqual(0, processor.Statistics.NumberOfWords);
            Assert.AreEqual(0, processor.Statistics.NumberOfCharsWithSpace);
            Assert.AreEqual(0, processor.Statistics.NumberOfCharsWithoutSpace);
            
            File.Delete(file);
        }

        [TestMethod]
        public void ProcessCsvFileWithHeadersOnly_FirstRowAsHeadersOn()
        {
            const string file = "test.csv";
            using (var fileStream = File.Create(file))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine("A,B,C,D");
            }

            IStatusUpdate statusUpdate = new StatusUpdate();
            var notification = new Mock<INotification>();
            notification.Setup(n => n.NotifyStatus(It.IsAny<IStatusUpdate>())).Callback<IStatusUpdate>(s => statusUpdate = s);

            var processor = FileProcessorFactory.GetFileProcessor(file);
            var result = processor.Start(notification.Object, true).Result;

            Assert.AreEqual(file, processor.FilePath);
            Assert.AreEqual(ProcessingResult.Complete, result);
            Assert.AreEqual(1, processor.Statistics.NumberOfLines);
            Assert.AreEqual(0, processor.Statistics.NumberOfWords);
            Assert.AreEqual(0, processor.Statistics.NumberOfCharsWithSpace);
            Assert.AreEqual(0, processor.Statistics.NumberOfCharsWithoutSpace);
            
            Assert.AreEqual(1, statusUpdate.RowId);
            Assert.AreEqual(100, statusUpdate.ProcessPercentage);
            Assert.AreEqual(4, statusUpdate.ColumnHeaders.Length);
            Assert.AreEqual("A", statusUpdate.ColumnHeaders[0].Name);
            Assert.AreEqual("B", statusUpdate.ColumnHeaders[1].Name);
            Assert.AreEqual("C", statusUpdate.ColumnHeaders[2].Name);
            Assert.AreEqual("D", statusUpdate.ColumnHeaders[3].Name);

            File.Delete(file);
        }

        [TestMethod]
        public void ProcessCsvFileWithHeadersOnly_FirstRowAsHeadersOff()
        {
            const string file = "test.csv";
            using (var fileStream = File.Create(file))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine("A,B,C,D");
            }

            var statusUpdates = new List<IStatusUpdate>();
            var notification = new Mock<INotification>();
            notification.Setup(n => n.NotifyStatus(It.IsAny<IStatusUpdate>())).Callback<IStatusUpdate>(s => statusUpdates.Add(s));

            var processor = FileProcessorFactory.GetFileProcessor(file);
            var result = processor.Start(notification.Object, false).Result;

            Assert.AreEqual(file, processor.FilePath);
            Assert.AreEqual(result, ProcessingResult.Complete);
            Assert.AreEqual(1, processor.Statistics.NumberOfLines);
            Assert.AreEqual(4, processor.Statistics.NumberOfWords);
            Assert.AreEqual(4, processor.Statistics.NumberOfCharsWithSpace);
            Assert.AreEqual(4, processor.Statistics.NumberOfCharsWithoutSpace);
            
            Assert.AreEqual(2, statusUpdates.Count);

            Assert.AreEqual(1, statusUpdates.ElementAt(0).RowId);
            Assert.AreEqual(100, statusUpdates.ElementAt(0).ProcessPercentage);
            Assert.AreEqual(statusUpdates.ElementAt(0).Values.Length, 4);
            Assert.AreEqual("Column_1", statusUpdates.ElementAt(0).ColumnHeaders[0].Name);
            Assert.AreEqual("Column_2", statusUpdates.ElementAt(0).ColumnHeaders[1].Name);
            Assert.AreEqual("Column_3", statusUpdates.ElementAt(0).ColumnHeaders[2].Name);
            Assert.AreEqual("Column_4", statusUpdates.ElementAt(0).ColumnHeaders[3].Name);
            Assert.AreEqual(ColumnType.String, statusUpdates.ElementAt(0).ColumnHeaders[0].Type);
            Assert.AreEqual(ColumnType.String, statusUpdates.ElementAt(0).ColumnHeaders[1].Type);
            Assert.AreEqual(ColumnType.String, statusUpdates.ElementAt(0).ColumnHeaders[2].Type);
            Assert.AreEqual(ColumnType.String, statusUpdates.ElementAt(0).ColumnHeaders[3].Type);
            Assert.AreEqual("A", statusUpdates.ElementAt(0).Values[0]);
            Assert.AreEqual("B", statusUpdates.ElementAt(0).Values[1]);
            Assert.AreEqual("C", statusUpdates.ElementAt(0).Values[2]);
            Assert.AreEqual("D", statusUpdates.ElementAt(0).Values[3]);

            File.Delete(file);
        }

        [TestMethod]
        public void ProcessCsvFileWithHeadersAndValues()
        {
            const string file = "test.csv";
            using (var fileStream = File.Create(file))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine("A,B,C,D");
                streamWriter.WriteLine("test,100.5,true,2021-12-31 00:30:00");
            }

            var statusUpdates = new List<IStatusUpdate>();
            var notification = new Mock<INotification>();
            notification.Setup(n => n.NotifyStatus(It.IsAny<IStatusUpdate>())).Callback<IStatusUpdate>(s => statusUpdates.Add(s));

            var processor = FileProcessorFactory.GetFileProcessor(file);
            var result = processor.Start(notification.Object, true).Result;

            Assert.AreEqual(file, processor.FilePath);
            Assert.AreEqual(ProcessingResult.Complete, result);
            Assert.AreEqual(2, processor.Statistics.NumberOfLines);
            Assert.AreEqual(5, processor.Statistics.NumberOfWords);
            Assert.AreEqual(32, processor.Statistics.NumberOfCharsWithSpace);
            Assert.AreEqual(31, processor.Statistics.NumberOfCharsWithoutSpace);
            
            Assert.AreEqual(3, statusUpdates.Count);

            var statusUpdate = statusUpdates.First(s => s.ProcessPercentage == 100 && s.ColumnHeaders.Any() && s.Values.Any());
            Assert.AreEqual(2, statusUpdate.RowId);
            Assert.AreEqual(100, statusUpdate.ProcessPercentage);
            Assert.AreEqual(4, statusUpdate.Values.Length);
            Assert.AreEqual("A", statusUpdate.ColumnHeaders[0].Name);
            Assert.AreEqual("B", statusUpdate.ColumnHeaders[1].Name);
            Assert.AreEqual("C", statusUpdate.ColumnHeaders[2].Name);
            Assert.AreEqual("D", statusUpdate.ColumnHeaders[3].Name);
            Assert.AreEqual(ColumnType.String, statusUpdate.ColumnHeaders[0].Type);
            Assert.AreEqual(ColumnType.Numeric, statusUpdate.ColumnHeaders[1].Type);
            Assert.AreEqual(ColumnType.Boolean, statusUpdate.ColumnHeaders[2].Type);
            Assert.AreEqual(ColumnType.DateTime,statusUpdate.ColumnHeaders[3].Type);
            Assert.AreEqual("test", statusUpdate.Values[0]);
            Assert.AreEqual("100.5", statusUpdate.Values[1]);
            Assert.AreEqual("true", statusUpdate.Values[2]);
            Assert.AreEqual("2021-12-31 00:30:00", statusUpdate.Values[3]);

            File.Delete(file);
        }
    }
}
