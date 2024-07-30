using ReportImportExport.Import;

namespace MyFinances.EntityFrameworkCore.Repositories.Reports.Import
{
    public class ReportImportRepository : IReportImportRepository
    {
        private readonly MyFinancesDbContext _dbContext;

        public ReportImportRepository(MyFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ReportImport import)
        {
            await _dbContext.AddAsync(import);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<ReportImport> GetReports() =>
            _dbContext.ReportImportDbSet;

        public async Task UpdateAsync(ReportImport import)
        {
            _dbContext.Update(import);
            await _dbContext.SaveChangesAsync();
        }
    }
}
