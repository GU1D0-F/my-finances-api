namespace ReportImportExport.Import
{
    public interface IReportImportLogRepository
    {
        Task InsertAsync(ReportImportLog log);
    }
}
