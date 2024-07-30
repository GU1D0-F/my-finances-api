namespace MyFinances.Reports.Export
{
    public interface IReportExportImplementationService
    {
        IReportExportDataService GetImplementation(ReportExportTypeEnum reportType);
    }
}
