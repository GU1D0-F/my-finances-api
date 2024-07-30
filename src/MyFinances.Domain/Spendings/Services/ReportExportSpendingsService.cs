using MyFinances.Reports;
using MyFinances.Reports.Export;
using ReportImportExport.Enums;
using ReportImportExport.Export;

namespace MyFinances.Spendings
{
    public class ReportExportSpendingsService : IReportExportSpendingsService
    {
        private readonly ISpendingRepository _spendingRepository;
        private readonly ISimpleReportExportService _simpleReportExportService;

        public ReportExportSpendingsService(ISpendingRepository spendingRepository, ISimpleReportExportService simpleReportExportService)
        {
            _spendingRepository = spendingRepository;
            _simpleReportExportService = simpleReportExportService;
        }

        public async Task<ReportStreamDto> ExecuteAsync(ReportExport report)
        {
            var imporatdoresToExport = _spendingRepository.GetItems(report.UserId).Select(x => new SpendingDto
            {
                Id = x.Id,
                Data = x.Data,
                Descricao = x.Descricao,
                Observacao = x.Observacao,
                TipoTransacao = x.TipoTransacao,
                Valor = x.Valor
            }).ToList();

            var stream = await _simpleReportExportService
                .ExecuteGenerationAsync<SpendingDto, SpendingExportDto>(
                    report.Id,
                    ReportSpendingsConsts.ExportName,
                    imporatdoresToExport
                )
                .ConfigureAwait(false);

            return new ReportStreamDto()
            {
                Stream = stream,
                ExtensionType = ReportFileExtensionEnum.Xlsx
            };
        }
    }
}
