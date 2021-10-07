namespace FileAnalyzer
{
    /// <summary>
    /// Used to keep track of the percentage of completion of a task
    /// </summary>
    public class CompletionTracker
    {
        // The final size to achieve
        private readonly long _targetSize;

        // The size completed so far
        private long _totalCompletedSize;

        public CompletionTracker(long targetSize)
        {
            _targetSize = targetSize;
        }

        /// <summary>
        /// Allows updating of the size completed so far
        /// </summary>
        /// <param name="completedSize"></param>
        public void UpdateCompletion(long completedSize)
        {
            _totalCompletedSize += completedSize;
        }

        /// <summary>
        /// Returns the calculated percentage of completion
        /// </summary>
        public int CurrentPercentage => _totalCompletedSize >= _targetSize? 100 : (int)(_totalCompletedSize * 100 / _targetSize);
    }
}