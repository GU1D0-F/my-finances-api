using ReportImportExport.Import;

namespace MyFinances.Reports.Import
{
    public class ReportImportManagerService : IReportImportManagerService
    {
        private readonly IReportImportImplementationService _reportImportImplementationService;

        public ReportImportManagerService(IReportImportImplementationService reportImportImplementationService)
        {
            _reportImportImplementationService = reportImportImplementationService;
        }

        public void EnqueueSpecificJob(ReportImport report)
        {
            try
            {
                var reportType = (ReportImportTypeEnum)report.Type;

                var reportService = _reportImportImplementationService.GetImplementation(reportType);

                reportService.EnqueueReportImportJob(report);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
