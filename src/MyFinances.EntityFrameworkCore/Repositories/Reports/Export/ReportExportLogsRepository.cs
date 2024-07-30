using ReportImportExport.Export;

namespace MyFinances.EntityFrameworkCore.Repositories.Reports.Export
{
    public class ReportExportLogsRepository : IReportExportLogsRepository
    {
        private readonly MyFinancesDbContext _dbContext;

        public ReportExportLogsRepository(MyFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(ReportExportLog log)
        {
            await _dbContext.ReportExportLogDbSet.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}
