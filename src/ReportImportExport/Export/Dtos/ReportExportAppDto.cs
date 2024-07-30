using ReportImportExport.Enums;

namespace ReportImportExport.Export
{
    [Serializable]
    public class ReportExportAppDto
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public ReportStatusEnum Status { get; set; }
        public int Type { get; set; }       
        public ReportFileExtensionEnum? ExtensionType { get; set; }
        public string Filters { get; set; }
        public string FullPath { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public IList<ReportExportLogDto> Logs { get; set; }
    }
}
