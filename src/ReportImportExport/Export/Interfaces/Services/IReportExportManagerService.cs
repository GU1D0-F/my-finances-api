namespace ReportImportExport.Export
{
    public interface IReportExportManagerService
    {
        Task<ReportStreamDto> CreateReportExportStreamAsync(ReportExport report);

        List<string> GetDocumentos(List<string> documentos, int type);
    }
}
