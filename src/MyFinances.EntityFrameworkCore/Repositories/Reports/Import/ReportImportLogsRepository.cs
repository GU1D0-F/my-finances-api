using ReportImportExport.Import;

namespace MyFinances.EntityFrameworkCore.Repositories.Reports.Import
{
    public class ReportImportLogsRepository : IReportImportLogRepository
    {
        private readonly MyFinancesDbContext _dbContext;

        public ReportImportLogsRepository(MyFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(ReportImportLog log)
        {
            await _dbContext.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}
