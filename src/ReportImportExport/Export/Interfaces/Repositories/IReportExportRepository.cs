namespace ReportImportExport.Export
{
    public interface IReportExportRepository
    {
        Task<ReportExport> GetAsync(int reportId);
        Task InsertAsync(ReportExport reportExport);
        Task UpdateAsync(ReportExport reportExport);
        Task<ReportExport> GetLastReportExportByType(int type);
        ReportExport GetByDocumentoAndType(string documento, string filters, int type);
    }
}
