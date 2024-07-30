using System.Text.Json;

namespace ReportImportExport.Export
{
    public class ReportExportCreateDto<T>
        where T : Enum
    {
        public List<string> Documentos { get; set; }

        public JsonElement? Filters { get; set; }

        public T Type { get; set; }

        public string UserId { get; set; }
    }
}
