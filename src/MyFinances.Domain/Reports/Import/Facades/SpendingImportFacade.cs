using MyFinances.Spendings;
using ReportImportExport.Import;
using System.Data.Common;
using System.Globalization;

namespace MyFinances.Reports.Import
{
    public class SpendingImportFacade : ISpendingImportFacade
    {
        private readonly ISpendingRepository _repository;
        private readonly IReportImportService _reportImportService;
        private readonly IReportImportSpendingsService _reportImportSpendingService;

        public SpendingImportFacade(
            ISpendingRepository repository,
            IReportImportService reportImportService,
            IReportImportSpendingsService reportImportSpendingsService)
        {
            _repository = repository;
            _reportImportService = reportImportService;
            _reportImportSpendingService = reportImportSpendingsService;
        }

        public async Task ExecuteAsync(long reportId)
        {
            try
            {
                var report = await _reportImportService.GetAsync(reportId).ConfigureAwait(false);

                await _reportImportService.ProcessWithLogAndReportUpdateAsync(report, (report) =>
                {
                    CadastroGastosAsync(report).Wait();
                }).ConfigureAwait(false);
            }
            catch (DbException)
            {
                throw new Exception("Erro ao persistir informações no banco favor revisar a planinlha enviada");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task CadastroGastosAsync(ReportImport report)
        {
            var dataFromReport = await _reportImportSpendingService.GetDataFromReportAsync(report).ConfigureAwait(false);
            foreach (var input in dataFromReport)
            {
                string[] columns = input.DateCategoryTitleAmount.Split(',');

                string date = columns[0];
                string category = columns[1];
                string title = columns[2];
                decimal amount = decimal.Parse(columns[3], CultureInfo.InvariantCulture);

                if (amount < 0)
                    continue;

                Spending spending = new()
                {
                    Data = DateTime.Parse(date),
                    Categoria = category,
                    Descricao = title,
                    Valor = -1 * amount,
                    TipoTransacao = TipoTransacaoEnum.Saida,
                    UserId = report.UserId
                };

                await _repository.AddItem(spending);
            }
        }
    }
}
