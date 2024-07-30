namespace ReportImportExport.Import
{
    public interface IReportImportJobService
    {
        void EnqueueReportImportJob(ReportImport report);
    }
}
