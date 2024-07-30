using ClosedXML.Excel;
using ImportExportXls.Extensions;

namespace ImportExportXls
{
    public class ExportManagerBuilder<T> : WritterManager<T> where T : new()
    {
        internal IXLWorkbook Workbook { get; set; }

        public ExportManagerBuilder<T> Init()
        {
            Workbook = new XLWorkbook();

            Properties = CommonManager.ExtractReferenceMapedProperties<T>();

            var worksheetName = CommonManager.ExtractWorksheetName<T>();

            Workbook.Worksheets.Add(worksheetName);
            Workbook.Worksheets.TryGetWorksheet(worksheetName, out var activeWorkshet);
            ActiveWorksheet = activeWorkshet;

            return this;
        }

        public ExportManagerBuilder<T> UseWorkBook(IXLWorkbook workbook)
        {
            Workbook = workbook;

            Properties = CommonManager.ExtractReferenceMapedProperties<T>();

            var worksheetName = CommonManager.ExtractWorksheetName<T>();

            Workbook.Worksheets.Add(worksheetName);
            Workbook.Worksheets.TryGetWorksheet(worksheetName, out var activeWorkshet);
            ActiveWorksheet = activeWorkshet;

            return this;
        }

        public ExportManagerBuilder<T> SetData(List<T> data)
        {
            Data = data;
            return this;
        }

        public MemoryStream StartExportProcess()
        {
            Properties.SortFields();
            ProcessData();
            FormatColumns();
            FormatTable();
            WriteFile();

            MemoryStream stream = new();
            Workbook.SaveAs(stream);
            return stream;
        }
    }
}
