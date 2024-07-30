using MyFinances.Reports.Import;
using ReportImportExport.Import;

namespace MyFinances.Spendings
{
    public interface IReportImportSpendingsService : IReportImportJobService
    {
        Task<IList<SpendingImportDto>> GetDataFromReportAsync(ReportImport report);
    }
}
