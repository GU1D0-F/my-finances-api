using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFinances.Spendings;
using MyFinances.Users;
using ReportImportExport;
using ReportImportExport.Export;
using ReportImportExport.Import;

namespace MyFinances.EntityFrameworkCore
{
    public class MyFinancesDbContext : IdentityDbContext<User>
    {
        public DbSet<User> UserDbSet { get; set; }
        public DbSet<Spending> SpendingDbSet { get; set; }
        public DbSet<ReportImport> ReportImportDbSet { get; set; }
        public DbSet<ReportImportLog> ReportImportLogDbSet { get; set; }
        public DbSet<ReportExport> ReportExportDbSet { get; set; }
        public DbSet<ReportExportLog> ReportExportLogDbSet { get; set; }

        public MyFinancesDbContext(DbContextOptions<MyFinancesDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterExportImportReportEfCoreService();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyFinancesDbContext).Assembly);
        }
    }
}
