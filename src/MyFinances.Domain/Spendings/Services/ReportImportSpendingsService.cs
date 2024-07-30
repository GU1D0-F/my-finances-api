using Hangfire;
using Hangfire.States;
using MyFinances.Reports;
using MyFinances.Reports.Import;
using ReportImportExport.Base;
using ReportImportExport.Import;
using ReportImportExport.Utils;

namespace MyFinances.Spendings
{
    public class ReportImportSpendingsService : IReportImportSpendingsService
    {
        private readonly ISimpleReportImportService _simpleReportImportService;
        private readonly IConfigurationHelperService _configurationHelperService;

        public ReportImportSpendingsService(ISimpleReportImportService simpleReportImportService, IConfigurationHelperService configurationHelperService)
        {
            _simpleReportImportService = simpleReportImportService;
            _configurationHelperService = configurationHelperService;
        }

        public void EnqueueReportImportJob(ReportImport report)
        {
            var args = new ReportImportJobArgsBaseDto()
            {
                ReportId = report.Id
            };

            EnqueueNewImportJob<SpendingImportJob, ReportImportJobArgsBaseDto>(args);
        }

        void EnqueueNewImportJob<T, TArgs>(TArgs args) where T : HangFireJobBase<TArgs>
        {
            new BackgroundJobClient().Create((T x) => x.Execute(null, args), new EnqueuedState(_configurationHelperService.GetImportQueueName()));
        }

        public Task<IList<SpendingImportDto>> GetDataFromReportAsync(ReportImport report)
        {
            return _simpleReportImportService
                .ExecuteImportationAsync<SpendingImportDto>(report, ReportSpendingsConsts.ImportName);
        }
    }
}
