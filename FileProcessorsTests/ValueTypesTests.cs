using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileAnalyzer;

namespace FileProcessorsTests
{
    [TestClass]
    public class ValueTypesTests
    {
        [TestMethod]
        public void ShouldRecognizeDateTimeType_Format1()
        {
            var type = ValueTypes.GetType("2021-01-31 15:30:00");
            Assert.AreEqual(ColumnType.DateTime, type);
        }

        [TestMethod]
        public void ShouldRecognizeDateTimeType_Format2()
        {
            var type = ValueTypes.GetType("18-12-2021 15:30:00");
            Assert.AreEqual(ColumnType.DateTime, type);
        }

        [TestMethod]
        public void ShouldRecognizeDateTimeType_Format3()
        {
            var type = ValueTypes.GetType("18/12/2021 15:30:00");
            Assert.AreEqual(ColumnType.DateTime, type);
        }

        [TestMethod]
        public void ShouldRecognizeBooleanType_SmallCases()
        {
            var type = ValueTypes.GetType("true");
            Assert.AreEqual(ColumnType.Boolean, type);
        }

        [TestMethod]
        public void ShouldRecognizeBooleanType_UpperCases()
        {
            var type = ValueTypes.GetType("FALSE");
            Assert.AreEqual(ColumnType.Boolean, type);
        }

        [TestMethod]
        public void ShouldRecognizeBooleanType_MixedCases()
        {
            var type = ValueTypes.GetType("tRuE");
            Assert.AreEqual(ColumnType.Boolean, type);
        }

        [TestMethod]
        public void ShouldRecognizeNumericType_Integer()
        {
            var type = ValueTypes.GetType("1");
            Assert.AreEqual(ColumnType.Numeric, type);
        }

        [TestMethod]
        public void ShouldRecognizeNumericType_Double()
        {
            var type = ValueTypes.GetType("12005.20");
            Assert.AreEqual(ColumnType.Numeric, type);
        }

        [TestMethod]
        public void ShouldRecognizeStringType1()
        {
            var type = ValueTypes.GetType("18/12/2021 15.30:00");
            Assert.AreEqual(ColumnType.String, type);
        }

        [TestMethod]
        public void ShouldRecognizeStringType2()
        {
            var type = ValueTypes.GetType("yes");
            Assert.AreEqual(ColumnType.String, type);
        }

        [TestMethod]
        public void ShouldRecognizeStringType_Empty()
        {
            var type = ValueTypes.GetType("");
            Assert.AreEqual(ColumnType.String, type);
        }
    }
}
