namespace MyFinances.Reports.Import
{
    public interface ISpendingImportFacade
    {
        Task ExecuteAsync(long reportId);
    }
}
