namespace ReportImportExport.Import
{
    public interface IReportImportRepository
    {
        IQueryable<ReportImport> GetReports();
        Task AddAsync(ReportImport import);
        Task UpdateAsync(ReportImport import);
    }
}
