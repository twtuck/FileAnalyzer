namespace FileAnalyzer
{
    public class CompletionTracker
    {
        private readonly long _targetSize;
        private long _totalCompletedSize;

        public CompletionTracker(long targetSize)
        {
            _targetSize = targetSize;
        }

        public void UpdateCompletion(long completedSize)
        {
            _totalCompletedSize += completedSize;
        }

        public int CurrentPercentage => (int)(_totalCompletedSize * 100 / _targetSize);
    }
}