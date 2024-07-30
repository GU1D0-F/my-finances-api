using ReportImportExport.Export;

namespace MyFinances.Reports.Export
{
    public interface IReportExportDataService
    {
        Task<ReportStreamDto> ExecuteAsync(ReportExport report);
    }
}
