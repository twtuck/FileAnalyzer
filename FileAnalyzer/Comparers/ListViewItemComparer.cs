using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileAnalyzer
{
    public class ListViewItemComparer : IComparer
    {
        // Specifies the comparer object
        private readonly IComparer _defaultComparer;

        // Specifies the column to be sorted
        public int SortColumn { set; get; }

        // Specifies the order in which to sort)
        public SortOrder Order { set; get; }

        // Specifies the type of column to be sorted
        public ColumnType ColumnType { get; set; }

        public ListViewItemComparer()
        {
            SortColumn = 0;
            Order = SortOrder.None;

            // Use CaseInsensitiveComparer as the default comparer
            _defaultComparer = new CaseInsensitiveComparer();
        }

        public void Reset()
        {
            SortColumn = 0;
            Order = SortOrder.None;
            ColumnType = ColumnType.String;
        }

        // The result of the comparison.
        // 0 if x = y, positive if x > y,  negative if x < y
        public int Compare(object x, object y)
        {
            var xValue = (x as ListViewItem)?.SubItems[SortColumn].Text;
            var yValue = (y as ListViewItem)?.SubItems[SortColumn].Text;

            int compareResult;
            switch (ColumnType)
            {
                case ColumnType.DateTime:
                    compareResult = CompareAsDateTime(xValue, yValue);
                    break;
                case ColumnType.Numeric:
                    compareResult = CompareAsDouble(xValue, yValue);
                    break;
                default:
                    compareResult = CompareAsString(xValue, yValue);
                    break;
            }
            return Order == SortOrder.Descending? -compareResult : compareResult;
        }

        private int CompareAsString(string x, string y)
        {
            return _defaultComparer.Compare(x, y);
        }

        private int CompareAsDouble(string x, string y)
        {
            try
            {
                var firstValue = double.Parse(x as string);
                var secondValue = double.Parse(y as string);
                return (int)(firstValue - secondValue);
            }
            catch
            {
                // Failed to parse input values as double, compare them as strings
                return CompareAsString(x, y);
            }
        }

        private int CompareAsDateTime(string x, string y)
        {
            try
            {
                var firstDate = DateTime.Parse(x as string);
                var secondDate = DateTime.Parse(y as string);
                return DateTime.Compare(firstDate, secondDate);
            }
            catch
            {
                // Failed to parse input values as DateTime, compare them as strings
                return CompareAsString(x, y);
            }
        }
    }
}