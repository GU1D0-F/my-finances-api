using System.ComponentModel.DataAnnotations;

namespace ReportImportExport.Base
{
    public class ReportLogBase
    {
        public int Id { get; set; }
        [MaxLength(4000)]
        public string Log { get; set; }
    }
}
