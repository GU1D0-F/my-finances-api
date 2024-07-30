using ReportImportExport.Base;
using ReportImportExport.Import;

namespace MyFinances.Reports.Import
{
    public class SpendingImportJob : ExportImportReportQueueJob<ReportImportJobArgsBaseDto>
    {
        private readonly ISpendingImportFacade _spendingImportFacade;

        public SpendingImportJob(ISpendingImportFacade spendingImportFacade)
        {
            _spendingImportFacade = spendingImportFacade;
        }

        public override void Execute(ReportImportJobArgsBaseDto args)
        {
            ExecuteWithExceptionless(() => ExecuteInternalAsync(args).Wait());
        }

        private async Task ExecuteInternalAsync(ReportImportJobArgsBaseDto args) =>
            await _spendingImportFacade.ExecuteAsync(args.ReportId).ConfigureAwait(false);
    }
}
