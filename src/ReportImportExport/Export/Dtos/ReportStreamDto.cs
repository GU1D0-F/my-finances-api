using ReportImportExport.Enums;

namespace ReportImportExport.Export
{
    public class ReportStreamDto
    {
        public MemoryStream Stream { get; set; }

        public ReportFileExtensionEnum ExtensionType { get; set; }
    }
}
