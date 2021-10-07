using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Thrown by the file processor when it encounters a line with its number of values being different from the number of columns detected.
    /// </summary>
    public class InconsistentRowException : Exception
    {
    }
}