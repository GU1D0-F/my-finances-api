using Microsoft.AspNetCore.Mvc;
using MyFinances.Reports.Import;
using ReportImportExport.Controllers;
using ReportImportExport.Import;

namespace MyFinances.Api.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ReportImportController : ImportReportAbstractController<ReportImportTypeEnum>
    {
        public ReportImportController(IReportImportService importService) : base(importService)
        {
        }
    }
}
