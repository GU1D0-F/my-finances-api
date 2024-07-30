using ReportImportExport.Base;

namespace ReportImportExport.Export
{
    public class ReportExportLog : ReportLogBase
    {
        public int ReportExportId { get; set; }

        public ReportExport ReportExport { get; set; }
    }
}
