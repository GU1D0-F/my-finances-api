using ReportImportExport.Enums;

namespace ReportImportExport.Export
{
    public interface IReportExportService
    {
        IList<ReportExportAppDto> EnqueueAsync(ReportExportCreateAppDto input);
        Task GenerateReportAsync(int reportId);
        Task<ReportExport> GetLastReportExportByType(int type);
        Task<(int Type, byte[] Stream, ReportFileExtensionEnum ExtensionType)> GetReportFileAsync(int reportId);
    }
}
