using ClosedXML.Excel;

namespace ReportImportExport.Export
{
    public interface ISimpleReportExportService
    {
        Task InsertLogAsync(int reportId, string log);

        Task<MemoryStream> ExecuteGenerationAsync<T, TR>(
            int reportId,
            string logDescription,
            IList<T> data,
            IXLWorkbook workbook = null,
            bool emptyData = false)
            where T : new()
            where TR : new();
    }
}
