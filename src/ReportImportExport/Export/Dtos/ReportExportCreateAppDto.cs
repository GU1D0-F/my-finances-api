using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ReportImportExport.Export
{
    [Serializable]
    public class ReportExportCreateAppDto
    {
        public ReportExportCreateAppDto(List<string> documentos, JsonElement? filters, int Type, string userId)
        {
            Documentos = documentos;
            Filters = filters;
            UserId = userId;
        }


        public List<string> Documentos { get; set; }

        public JsonElement? Filters { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Type { get; set; }

        public void LoadFilters()
        {
            Documentos = new List<string>()
            {
                Filters.ToString()
            };
        }
    }
}
