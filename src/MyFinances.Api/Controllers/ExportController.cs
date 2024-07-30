using Microsoft.AspNetCore.Mvc;
using MyFinances.Reports.Import;
using ReportImportExport.Controllers;
using ReportImportExport.Export;

namespace MyFinances.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ExportReportAbstractController<ReportImportTypeEnum>
    {
        public ExportController(IReportExportService reportExportService) : base(reportExportService)
        {
        }
    }
}
