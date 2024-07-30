using Microsoft.EntityFrameworkCore;
using ReportImportExport.Enums;
using ReportImportExport.Utils;

namespace ReportImportExport.Import
{
    public class ReportImportService : IReportImportService
    {
        private readonly IReportImportRepository _reportImportRepository;
        private readonly IReportImportLogRepository _reportImportLogRepository;
        private readonly IConfigurationHelperService _configurationHelperService;
        private readonly IReportImportManagerService _reportImportManagerService;

        public ReportImportService(
            IReportImportRepository reportImportRepository,
            IReportImportLogRepository reportImportLogRepository,
            IConfigurationHelperService configurationHelperService, 
            IReportImportManagerService reportImportManagerService)
        {
            _reportImportRepository = reportImportRepository;
            _reportImportLogRepository = reportImportLogRepository;
            _configurationHelperService = configurationHelperService;
            _reportImportManagerService = reportImportManagerService;

        }

        public async Task<long> UploadFileAsync(ReportImportUploadFileAppDto input)
        {
            if (input.File is null)
                throw new Exception("File is null");

            using MemoryStream ms = new();
            await input.File.CopyToAsync(ms);
            return await CreateReportAsync(ms.ToArray(),
                input.Type,
                input.Documento,
                input.UserId).ConfigureAwait(false);
        }

        public async Task<long> CreateReportAsync(byte[] fileStream, int type, string documento, string userId)
        {
            var destinationType = _configurationHelperService.GetFileDestinationType();

            var report = new ReportImport()
            {
                Documento = documento,
                Type = type,
                Status = ReportStatusEnum.New,
                UserId = userId
            };


            if (destinationType == FileDestinationTypeEnum.Database)
                report.ExcelFile = fileStream;

            await _reportImportRepository.AddAsync(report);
            _reportImportManagerService.EnqueueSpecificJob(report);

            return report.Id;
        }

        public async Task<ReportImport> GetAsync(long reportId) =>
            await _reportImportRepository.GetReports().FirstOrDefaultAsync(x => x.Id == reportId);     

        public async Task ProcessWithLogAndReportUpdateAsync(ReportImport report, Action<ReportImport> action)
        {
            try
            {
                await StartProcessingAsync(report).ConfigureAwait(continueOnCapturedContext: false);

                action(report);

                await FinishProcessingAsync(report, ReportStatusEnum.Finished).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception e)
            {
                await InserirLogAsync(report.Id, "Processamento finalizado com erro: " + e.ToFormattedString()).ConfigureAwait(continueOnCapturedContext: false);
                await FinishProcessingAsync(report, ReportStatusEnum.Error).ConfigureAwait(continueOnCapturedContext: false);
                throw new Exception("Ocorreu um erro na importação. " + e.Message);
            }
        }

        public async Task StartProcessingAsync(ReportImport report)
        {
            report.StartTime = DateTime.Now;
            report.Status = ReportStatusEnum.Processing;

            await _reportImportRepository.UpdateAsync(report);

            await InserirLogAsync(report.Id, "Iniciando processamento de importação.");
        }

        public async Task FinishProcessingAsync(ReportImport report, ReportStatusEnum status)
        {
            report.EndTime = DateTime.Now;
            report.Status = status;

            await _reportImportRepository.UpdateAsync(report);

            string description = status.GetDescription();
            await InserirLogAsync(report.Id, "Finalizando processamento de importação com status [" + description + "].");
        }

        public async Task InserirLogAsync(int reportId, string log)
        {
            ReportImportLog entity = new()
            {
                Log = log,
                ReportImportId = reportId
            };

            await _reportImportLogRepository.InsertAsync(entity);
        }
    }
}
