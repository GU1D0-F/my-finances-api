using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.Domain.Spendings.Services;
using MyFinances.Domain.Users.Services;
using MyFinances.Reports.Export;
using MyFinances.Reports.Import;
using MyFinances.Spendings;
using MyFinances.Users;
using ReportImportExport.Export;
using ReportImportExport.Import;
using ReportImportExport;


namespace MyFinances.Domain
{
    public static class DependenciesInjectorDomain
    {
        public static IServiceCollection AddDependenciesDomain(this IServiceCollection services)
        {
            services.AddDependenciesReportImportExportServices();

            var mappingConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<SpendingDto, SpendingExportDto>();
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IReportImportImplementationService, ReportImportImplementationService>();
            services.AddScoped<IReportExportImplementationService, ReportExportImplementationService>();
            services.AddScoped<IReportImportSpendingsService, ReportImportSpendingsService>();
            services.AddScoped<IReportExportSpendingsService, ReportExportSpendingsService>();
            services.AddScoped<IReportImportManagerService, ReportImportManagerService>();
            services.AddScoped<IReportExportManagerService, ReportExportManagerService>();
            services.AddScoped<ISpendingImportFacade, SpendingImportFacade>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<ISpendingService, SpendingService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IDashboardService, DashboardService>();
            //services.AddScoped<IMetricsService, MetricsService>();

            return services;
        }
    }
}
