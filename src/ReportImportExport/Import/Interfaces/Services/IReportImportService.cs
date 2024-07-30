namespace ReportImportExport.Import
{
    public interface IReportImportService
    {
        Task<ReportImport> GetAsync(long reportId);
        Task<long> UploadFileAsync(ReportImportUploadFileAppDto input);
        Task ProcessWithLogAndReportUpdateAsync(ReportImport report, Action<ReportImport> action);
    }
}
