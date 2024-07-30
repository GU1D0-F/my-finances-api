namespace ReportImportExport.Import
{
    public interface ISimpleReportImportService
    {
        Task<IList<T>> ExecuteImportationAsync<T>(
            ReportImport report,
            string logDescription) where T : new();
    }
}
