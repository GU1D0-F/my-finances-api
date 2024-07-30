using ClosedXML.Excel;

namespace ImportExportXls.Models
{
    internal class CellRead
    {
        public XLDataType Type { get; set; }
        public Object Value { get; set; }
    }
}
