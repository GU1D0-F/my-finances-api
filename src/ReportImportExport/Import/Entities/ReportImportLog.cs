using ReportImportExport.Base;

namespace ReportImportExport.Import
{
    public class ReportImportLog : ReportLogBase
    {
        public int ReportImportId { get; set; }

        public ReportImport ReportImport { get; set; }
    }
}
