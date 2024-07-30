using ImportExportXls;
using ImportExportXls.Annotations;
using ImportExportXls.Extensions;
using ReportImportExport.Enums;
using ReportImportExport.Utils;

namespace ReportImportExport.Import
{
    public class SimpleReportImportService : ISimpleReportImportService
    {
        private readonly string _datePattern = "dd/MM/yyyy";
        private readonly IReportImportRepository _reportImportRepository;
        private readonly IReportImportLogRepository _reportImportLogRepository;
        private readonly IConfigurationHelperService _configurationHelperService;
        private readonly IDirectoryFileManagerService _directoryFileManagerService;

        public SimpleReportImportService(IReportImportRepository reportImportRepository, 
            IReportImportLogRepository reportImportLogRepository, 
            IConfigurationHelperService configurationHelperService,
            IDirectoryFileManagerService directoryFileManagerService)
        {
            _reportImportRepository = reportImportRepository;
            _reportImportLogRepository = reportImportLogRepository;
            _configurationHelperService = configurationHelperService;
            _directoryFileManagerService = directoryFileManagerService;
        }

        public async Task<IList<T>> ExecuteImportationAsync<T>(
           ReportImport report,
           string logDescription) where T : new()
        {
            try
            {
                var worksheetName = GetWorksheetName<T>();

                logDescription = string.IsNullOrEmpty(logDescription) ? worksheetName : logDescription;

                MemoryStream stream = GetFileMemoryStream(report);

                await InsertLog(report.Id, $"Iniciando Processo de Importação - {logDescription}").ConfigureAwait(false);

                var result = new ImportManagerBuilder<T>()
                    .Init()
                    .SetStringDateFormat(_datePattern)
                    .OpenFile(stream, worksheetName)
                    .StartImportProcess();

                return result;
            }
            catch (Exception e)
            {
                report.EndTime = DateTime.Now;
                await UpdateProgressAndLogAsync(report, ReportStatusEnum.Error, $"Finalizado Processo com erro - {e.Message}").ConfigureAwait(false);
                throw new Exception(e.Message);
            }
        }

        private MemoryStream GetFileMemoryStream(ReportImport report)
        {
            var file = report.ExcelFile;
            var destinationType = _configurationHelperService.GetFileDestinationType();

            if (destinationType == FileDestinationTypeEnum.Directory)
                file = _directoryFileManagerService.GetFile(report);

            return new MemoryStream(file);
        }

        private string GetWorksheetName<T>() where T : new()
        {
            var info = (new T())
                .GetType()
                .CustomAttributes
                .FirstOrDefault(x => x.AttributeType == typeof(ExportWorkSheetAttribute));

            return (string)info.ConstructorArguments[0].Value;
        }

        private async Task InsertLog(int reportId, string log)
        {
            var reportLog = new ReportImportLog()
            {
                Log = log,
                ReportImportId = reportId
            };

            await _reportImportLogRepository.InsertAsync(reportLog)
                                      .ConfigureAwait(false);
        }

        private async Task UpdateProgressAndLogAsync(ReportImport report, ReportStatusEnum status, string log)
        {
            report.Status = status;
            await _reportImportRepository.UpdateAsync(report).ConfigureAwait(false);

            if (!log.IsNullOrEmpty())
                await InsertLog(report.Id, log);
        }
    }
}
