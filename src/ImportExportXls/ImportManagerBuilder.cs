using ClosedXML.Excel;
using ImportExportXls.Exceptions;
using ImportExportXls.Extensions;

namespace ImportExportXls
{
    public class ImportManagerBuilder<T> : ImportProcessor<T> where T : new()
    {
        internal IXLWorkbook Workbook { get; set; }

        public ImportManagerBuilder<T> Init()
        {
            Properties = CommonManager.ExtractReferenceMapedProperties<T>();
            ImportedData = new List<T>();
            return this;
        }

        public ImportManagerBuilder<T> OpenFile(MemoryStream stream, string worksheetName)
        {
            Workbook = new XLWorkbook(stream);

            Workbook.TryGetWorksheet(worksheetName, out var worksheet);

            ActiveWorksheet = worksheet ?? throw new InvalidWorksheetNameException();

            return this;
        }

        public ImportManagerBuilder<T> ToggleAcceptFormula(bool accept)
        {
            AcceptFormula = accept;

            return this;
        }

        public ImportManagerBuilder<T> SetStringDateFormat(string stringDateFormat)
        {
            StringDateFormat = stringDateFormat;
            return this;
        }


        public IList<T> StartImportProcess()
        {
            Properties.SortFields();
            ProcessColumns();
            InitReadData();
            return ImportedData;
        }
    }
}
