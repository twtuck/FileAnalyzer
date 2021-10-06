using System.Threading.Tasks;

namespace FileAnalyzer
{
    public interface IFileProcessor
    {
        Task<ProcessingResult> Start(INotification notification, bool firstRowIsHeaders = false);
        void Cancel();
        string FileName { get; }
        IStatistics Statistics { get; }
    }
}