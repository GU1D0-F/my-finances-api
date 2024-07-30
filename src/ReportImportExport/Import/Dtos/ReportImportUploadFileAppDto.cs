using Microsoft.AspNetCore.Http;

namespace ReportImportExport.Import
{
    public class ReportImportUploadFileAppDto
    {
        public IFormFile File { get; set; }
        public int Type { get; set; }
        public string Documento { get; set; }
        public string UserId { get; set; }
    }
}
