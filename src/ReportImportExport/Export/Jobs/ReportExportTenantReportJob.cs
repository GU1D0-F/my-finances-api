using ReportImportExport.Base;

namespace ReportImportExport.Export
{
    internal class ReportExportTenantReportJob : ExportImportReportQueueJob<ReportExportTenantReportJobArgs>
    {
        private readonly IReportExportService _reportExportService;

        public ReportExportTenantReportJob(IReportExportService reportExportService)
        {
            _reportExportService = reportExportService;
        }

        public override void Execute(ReportExportTenantReportJobArgs args)
        {
            _reportExportService.GenerateReportAsync(args.ReportId).Wait();
        }
    }
}
