using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReportImportExport.Export;
using ReportImportExport.Import;
using ReportImportExport.Utils;

namespace ReportImportExport
{
    public static class DependenciesInjectorReportImportExport
    {
        public static IServiceCollection AddDependenciesReportImportExportServices(this IServiceCollection services)
        {
            services.AddScoped<IReportImportService, ReportImportService>();
            services.AddScoped<ISimpleReportImportService, SimpleReportImportService>();
            services.AddScoped<IConfigurationHelperService, ConfigurationHelperService>();
            services.AddScoped<IReportExportService, ReportExportService>();
            services.AddScoped<ISimpleReportExportService, SimpleReportExportService>();
            services.AddScoped<IFilterValidationService, FilterValidationService>();
            services.AddScoped<IDirectoryFileManagerService, DirectoryFileManagerService>();

            return services;
        }


        public static void RegisterExportImportReportEfCoreService(this ModelBuilder builder)
        {
            #region ReportExport
            builder.Entity<ReportExport>().ToTable("ReportExport");

            builder.Entity<ReportExport>()
                .HasMany(x => x.ReportExportLogs)
                .WithOne(x => x.ReportExport)
                .HasForeignKey(x => x.ReportExportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ReportExportLog>().ToTable($"ReportExportLog");
            #endregion

            #region ReportImport
            builder.Entity<ReportImport>().ToTable("ReportImport");

            builder.Entity<ReportImport>()
                .HasMany(h => h.ReportImportLogs)
                .WithOne(x => x.ReportImport)
                .HasForeignKey(x => x.ReportImportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ReportImportLog>().ToTable($"ReportImportLog");
            #endregion
        }
    }
}
