using Microsoft.AspNetCore.Http;

namespace ReportImportExport.Import
{
    public class ReportImportUploadFileDto<T>
        where T : Enum
    {
        public IFormFile File { get; set; }
        public T Type { get; set; }

        public string Documento { get; set; }
        public string UserId { get; set; }
    }
}
