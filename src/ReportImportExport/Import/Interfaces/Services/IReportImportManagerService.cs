namespace ReportImportExport.Import
{
    public interface IReportImportManagerService
    {
        void EnqueueSpecificJob(ReportImport report);
    }
}
