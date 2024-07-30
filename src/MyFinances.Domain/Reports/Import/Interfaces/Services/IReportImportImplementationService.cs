using ReportImportExport.Import;

namespace MyFinances.Reports.Import
{
    public interface IReportImportImplementationService
    {
        IReportImportJobService GetImplementation(ReportImportTypeEnum reportType);
    }
}
