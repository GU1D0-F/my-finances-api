using ImportExportXls.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportImportExport.Enums;
using ReportImportExport.Export;
using ReportImportExport.Utils;
using System.Globalization;
using System.Net.Http.Headers;

namespace ReportImportExport.Controllers
{
    public abstract class ExportReportAbstractController<T> : ControllerBase
        where T : Enum
    {
        private readonly IReportExportService _reportExportService;

        public ExportReportAbstractController(IReportExportService reportExportService)
        {
            _reportExportService = reportExportService;
        }


        [HttpPost]
        [Route("exportacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IList<ReportExportDto<T>> CreateReportExportacao([FromBody] ReportExportCreateDto<T> input)
        {
            var report = _reportExportService.EnqueueAsync(new ReportExportCreateAppDto(input.Documentos, input.Filters, Convert.ToInt32(input.Type), input.UserId));

            return report.Select(x => ToSpecificDto(x)).ToList();
        }

        private static ReportExportDto<T> ToSpecificDto(ReportExportAppDto args)
        {
            return new ReportExportDto<T>()
            {
                Documento = args.Documento,
                Logs = args.Logs,
                Id = args.Id,
                Status = args.Status,
                EndTime = args.EndTime,
                StartTime = args.StartTime,
                Filters = args.Filters,
                ExtensionType = args.ExtensionType,
                FullPath = args.FullPath,
                Type = (T)Enum.Parse(typeof(T), args.Type.ToString())
            };
        }

        [HttpGet]
        [Route("file/exportacao/{reportId:long}")]
        public virtual async Task<FileContentResult> GetReportFile([FromRoute] int reportId)
        {
            var result = await _reportExportService.GetReportFileAsync(reportId);

            if (result.Stream.IsNullOrEmpty())
                throw new Exception($"id = {reportId}. Not Found");

            var extension = result.ExtensionType.GetDescription();

            var cd = new ContentDispositionHeaderValue("attachment")
            {

                FileName = $"Report-{reportId}-{DateTime.Now.ToString("ddMMyyyy-HHmmss", CultureInfo.InvariantCulture)}.{extension}"
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(
                result.Stream,
                (result.ExtensionType == ReportFileExtensionEnum.Xlsx
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/zip"),
                GenerateExportFileNameDownload(reportId, result.Type));
        }

        protected virtual string GenerateExportFileNameDownload(long reportId, int type, string document = "")
        {
            var filename = ((T)Enum.Parse(typeof(T), type.ToString())).GetDescription(reportId.ToString());

            if (!document.IsNullOrEmpty())
                document += " - ";

            return $"{filename} - {document}{DateTime.Now.ToString("ddMMyyyy-HHmmss", CultureInfo.InvariantCulture)}";
        }

        [HttpGet("exportacao")]
        public virtual async Task<ReportExport> GetLastExportByType(int type) =>
            await _reportExportService.GetLastReportExportByType(type);
    }
}
