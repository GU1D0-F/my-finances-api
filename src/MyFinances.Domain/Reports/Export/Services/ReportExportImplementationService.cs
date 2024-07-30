using Microsoft.Extensions.DependencyInjection;
using MyFinances.Spendings;

namespace MyFinances.Reports.Export
{
    public class ReportExportImplementationService : IReportExportImplementationService
    {
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _serviceProvider;

        public ReportExportImplementationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
        }

        public IReportExportDataService GetImplementation(ReportExportTypeEnum reportType)
        {
            return reportType switch
            {
                ReportExportTypeEnum.EntradasSaidasDoMes => _scope.ServiceProvider.GetRequiredService<IReportExportSpendingsService>(),
                _ => throw new Exception("Erro ao buscar implementacao")
            };
        }
    }
}
