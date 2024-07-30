using ReportImportExport.Base;

namespace ReportImportExport.Import
{
    public class ReportImport : ReportBase
    {
        public int Type { get; set; }
        public IList<ReportImportLog> ReportImportLogs { get; set; } = new List<ReportImportLog>();
        public string UserId { get; set; }
    }
}
