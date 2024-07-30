using DocumentFormat.OpenXml.Spreadsheet;
using ReportImportExport.Base;
using ReportImportExport.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReportImportExport.Export
{
    public class ReportExport : ReportBase
    {
        public int Type { get; set; }

        [MaxLength(8000)]
        public string Filters { get; set; }

        public string UserId { get; set; }

        public ReportFileExtensionEnum? ExtensionType { get; set; } = ReportFileExtensionEnum.Xlsx;

        public IList<ReportExportLog> ReportExportLogs { get; set; } = new List<ReportExportLog>();
    }
}
