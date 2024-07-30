using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportImportExport.Import;
using ReportImportExport.Utils;

namespace ReportImportExport.Controllers
{
    public class ImportReportAbstractController<T> : ControllerBase
          where T : Enum
    {
        private readonly IReportImportService _importService;

        public ImportReportAbstractController(IReportImportService importService)
        {
            _importService = importService;
        }


        [HttpPost]
        [Route("Upload")]
        [Consumes(contentType: "multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> UploadFileAsync([FromForm] ReportImportUploadFileDto<T> input)
        {
            var id = await _importService.UploadFileAsync(new ReportImportUploadFileAppDto()
            {
                File = input.File,
                Type = Convert.ToInt32(input.Type),
                Documento = string.IsNullOrEmpty(input.Documento) ? input.Type.GetDescription() : input.Documento,
                UserId = input.UserId
            });

            return Created("", id);
        }
    }
}
