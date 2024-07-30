using ReportImportExport.Export;

namespace MyFinances.Reports.Export
{
    internal class ReportExportManagerService : IReportExportManagerService
    {
        private readonly IReportExportImplementationService _reportExportImplementationService;

        public ReportExportManagerService(IReportExportImplementationService reportExportImplementationService)
        {
            _reportExportImplementationService = reportExportImplementationService;
        }

        public async Task<ReportStreamDto> CreateReportExportStreamAsync(ReportExport report)
        {
            return await Task.Run(() =>
            {
                var reportType = (ReportExportTypeEnum)report.Type;
                var reportService = _reportExportImplementationService.GetImplementation(reportType);

                return reportService.ExecuteAsync(report);
            }).ConfigureAwait(false);
        }

        public List<string> GetDocumentos(List<string> documentos, int type)
        {
            return documentos;
        }
    }
}
