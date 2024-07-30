using Microsoft.EntityFrameworkCore;
using MyFinances.Utils;
using ReportImportExport.Enums;
using ReportImportExport.Export;

namespace MyFinances.EntityFrameworkCore.Repositories.Reports.Export
{
    public class ReportExportRepository : IReportExportRepository
    {
        private readonly MyFinancesDbContext _dbContext;

        public ReportExportRepository(MyFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReportExport> GetAsync(int reportId) =>
            await _dbContext.ReportExportDbSet.FirstOrDefaultAsync(x => x.Id == reportId);


        public ReportExport GetByDocumentoAndType(string documento, string filters, int type)
        {
            return _dbContext.ReportExportDbSet.AsNoTracking()
                     .Include(i => i.ReportExportLogs)
                     .Where(w => w.Type == type
                         && w.Status != ReportStatusEnum.Finished
                         && w.Status != ReportStatusEnum.Error
                         && w.Status != ReportStatusEnum.Empty)
                     .WhereIf(!string.IsNullOrEmpty(documento), w => w.Documento.Equals(documento))
                     .WhereIf(!string.IsNullOrEmpty(filters), w => w.Filters.Equals(filters))
                     .FirstOrDefault();
        }

        public async Task<ReportExport> GetLastReportExportByType(int type) =>
            await _dbContext.ReportExportDbSet.OrderByDescending(x => x.StartTime).FirstOrDefaultAsync(x => x.Type == type);


        public async Task InsertAsync(ReportExport reportExport)
        {
            await _dbContext.AddAsync(reportExport);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReportExport reportExport)
        {
            _dbContext.Update(reportExport);
            await _dbContext.SaveChangesAsync();
        }
    }
}
