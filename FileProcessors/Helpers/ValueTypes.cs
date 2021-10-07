using System;

namespace FileAnalyzer
{
    public static class ValueTypes
    {
        // Determine and return the type of the given value
        // If the value cannot be determined as DateTime, boolean or numeric, it will default as a string type
        public static ColumnType GetType(string value)
        {
            if (double.TryParse(value, out _))
            {
                return ColumnType.Numeric;
            }

            if (DateTime.TryParse(value, out _))
            {
                return ColumnType.DateTime;
            }
            
            if (bool.TryParse(value, out _))
            {
                return ColumnType.Boolean;
            }

            return ColumnType.String;
        }
    }
}