namespace ReportImportExport.Export
{
    public interface IReportExportLogsRepository
    {
        Task InsertAsync(ReportExportLog log);
    }
}
