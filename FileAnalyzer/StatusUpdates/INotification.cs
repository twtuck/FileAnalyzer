namespace FileAnalyzer
{
    public interface INotification
    {
        void NotifyStatus(IStatusUpdate statusUpdate);

        void NotifyInconsistentRow(InconsistentRow inconsistentRow);
    }
}