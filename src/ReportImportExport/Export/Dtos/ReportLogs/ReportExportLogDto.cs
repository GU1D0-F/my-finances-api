using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ReportImportExport.Export
{
    [Serializable]
    public class ReportExportLogDto
    {
        public int Id { get; set; }

        public long ReportId { get; set; }

        [MaxLength(4000)]
        public string Log { get; set; }

        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public virtual ReportExportAppDto Report { get; set; }
    }
}
