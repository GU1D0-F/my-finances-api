using ReportImportExport.Enums;

namespace ReportImportExport.Base
{
    public class ReportBase
    {
        public int Id { get; set; }
        public byte[] ExcelFile { get; set; }

        public string Documento { get; set; }

        public string FullPath { get; set; }

        public ReportStatusEnum Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
