using Microsoft.Extensions.DependencyInjection;
using MyFinances.Spendings;
using ReportImportExport.Import;

namespace MyFinances.Reports.Import
{
    public class ReportImportImplementationService : IReportImportImplementationService
    {
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _serviceProvider;

        public ReportImportImplementationService
            (
                IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
        }

        public IReportImportJobService GetImplementation(ReportImportTypeEnum reportType)
        {
            return reportType switch
            {
                ReportImportTypeEnum.CadastroGastosMensaisNubank => _scope.ServiceProvider.GetRequiredService<IReportImportSpendingsService>(),
                _ => throw new Exception("Erro ao buscar implementacao")
            };
        }
    }
}
