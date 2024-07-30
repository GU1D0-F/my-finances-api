using ClosedXML.Excel;

namespace ImportExportXls.Extensions
{
    internal static class XLDataTypeExtensions
    {
        internal static string GetName(this XLDataType type)
        {
            return type switch
            {
                XLDataType.Number => "Number",
                XLDataType.Text => "Text",
                XLDataType.Boolean => "Boolean",
                XLDataType.DateTime => "DateTime",
                XLDataType.TimeSpan => "TimeSpan",
                _ => "Unknown",
            };
        }
    }
}
