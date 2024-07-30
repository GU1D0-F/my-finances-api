using Hangfire;
using Hangfire.States;
using ImportExportXls.Extensions;
using ReportImportExport.Enums;
using ReportImportExport.Utils;

namespace ReportImportExport.Export
{
    public class ReportExportService : IReportExportService
    {
        private readonly IReportExportRepository _reportExportRepository;
        private readonly IReportExportManagerService _reportExportManager;
        private readonly IFilterValidationService _filterValidationService;
        private readonly IConfigurationHelperService _configurationHelperService;
        private readonly IDirectoryFileManagerService _directoryFileManagerService;

        public ReportExportService(IReportExportRepository reportExportRepository, 
            IReportExportManagerService reportExportManager, 
            IFilterValidationService filterValidationService,
            IConfigurationHelperService configurationHelperService,
            IDirectoryFileManagerService directoryFileManagerService)
        {
            _reportExportManager = reportExportManager;
            _reportExportRepository = reportExportRepository;
            _filterValidationService = filterValidationService;
            _configurationHelperService = configurationHelperService;
            _directoryFileManagerService = directoryFileManagerService;
        }

        public virtual IList<ReportExportAppDto> EnqueueAsync(ReportExportCreateAppDto input)
        {
            string filters = null;

            if (input.Filters != null)
            {
                _filterValidationService.RunValidations(input.Filters.Value, input.Type);
                filters = input.Filters.Value.ToString();
            }

            input.Documentos ??= new();

            var reports = ScheduleAsync(
                    input.Documentos,
                    input.Type,
                    filters, 
                    input.UserId);

            return reports.Select(r => MapToGetOutputDto(r)).ToList();
        }

        public async Task GenerateReportAsync(int reportId)
        {
            var report = await GetReportAsync(reportId).ConfigureAwait(false);

            report.StartTime = DateTime.Now;
            await UpdateProgressAndLog(report, ReportStatusEnum.Processing, "Iniciando a geração do relatório").ConfigureAwait(false);

            try
            {
                var stream = await _reportExportManager.CreateReportExportStreamAsync(report).ConfigureAwait(false);

                report.EndTime = DateTime.Now;

                if (stream.Stream?.Capacity > 0)
                {
                    await UpdateProgressAndLog(report, ReportStatusEnum.WritingFile, "Gravando o arquivo Excel no banco de dados").ConfigureAwait(false);
                    await UpdateFileAsync(report, stream).ConfigureAwait(false);
                    await UpdateProgressAndLog(report, ReportStatusEnum.Finished, "Processamento finalizado com sucesso").ConfigureAwait(false);
                }
                else
                {
                    report.ExtensionType = null;
                    report.ExcelFile = null;
                    await UpdateProgressAndLog(report, ReportStatusEnum.Empty, "Sem dados para exportar").ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                report.EndTime = DateTime.Now;
                await UpdateProgressAndLog(report, ReportStatusEnum.Error, $"Processamento finalizado com erro: {ex.Message}").ConfigureAwait(false);

                throw;
            }
        }

        protected static ReportExportAppDto MapToGetOutputDto(ReportExport entity)
        {
            return new ReportExportAppDto()
            {
                Documento = entity.Documento,
                Status = entity.Status,
                Type = entity.Type,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Filters = entity.Filters,
                ExtensionType = entity.ExtensionType,
                FullPath = entity.FullPath,
                Logs = entity.ReportExportLogs.Select(s => new ReportExportLogDto() { Id = s.Id, Log = s.Log }).ToList(),
                Id = entity.Id
            };
        }

        private IList<ReportExport> ScheduleAsync(
            List<string> documentos,
            int type,
            string filterJson, 
            string userId)
        {
            var reports = new List<ReportExport>();

            documentos = GetDocumentos(documentos, type);

            if (documentos.Any())
            {
                foreach (var documento in documentos)
                {
                    var report = GetEnqueueJob(documento, filterJson, type, userId);
                    reports.Add(report);
                }
            }
            else
            {
                var report = GetEnqueueJob(string.Empty, filterJson, type, userId);
                reports.Add(report);
            }

            return reports;
        }

        private List<string> GetDocumentos(List<string> documentos, int type)
        {
            if (documentos.Any())
                return documentos;

            documentos = _reportExportManager.GetDocumentos(documentos, type);

            return documentos;
        }

        private ReportExport GetEnqueueJob(string documento, string filters, int type, string userId)
        {
            var report = _reportExportRepository.GetByDocumentoAndType(documento, filters, type);

            report ??= EnqueueNewExportJob(documento, filters, type, userId);

            return report;
        }

        private ReportExport EnqueueNewExportJob(string documento, string filters, int type, string userId)
        {
            var report = CreateAsync(documento, filters, Convert.ToInt32(type), userId).GetAwaiter().GetResult();

            var jobArgs = new ReportExportTenantReportJobArgs()
            {
                ReportId = report.Id
            };

            EnqueueNewExportJob(jobArgs);

            return report;
        }

        private async Task<ReportExport> CreateAsync(string documento, string filters, int type, string userId)
        {
            var report = new ReportExport()
            {
                Documento = documento,
                Filters = filters,
                Type = type,
                Status = ReportStatusEnum.New,
                StartTime = DateTime.Now,
                UserId = userId
            };

            await _reportExportRepository.InsertAsync(report).ConfigureAwait(false);

            return report;
        }

        private void EnqueueNewExportJob(ReportExportTenantReportJobArgs jobArgs)
        {
            var client = new BackgroundJobClient();
            client.Create<ReportExportTenantReportJob>(x => x.Execute(null, jobArgs),
                new EnqueuedState(_configurationHelperService.GetExportQueueName()));
        }

        private async Task<ReportExport> GetReportAsync(int reportId) =>
           await _reportExportRepository.GetAsync(reportId).ConfigureAwait(false);

        private async Task UpdateProgressAndLog(ReportExport report, ReportStatusEnum status, string log)
        {
            report.Status = status;

            if (!log.IsNullOrEmpty())
                report.ReportExportLogs.Add(ReportLog(report, log));

            await _reportExportRepository.UpdateAsync(report).ConfigureAwait(false);
        }

        private async Task UpdateFileAsync(ReportExport report, ReportStreamDto stream)
        {
            var destinationType = _configurationHelperService.GetFileDestinationType();

            await UpdateProgressAndLog(report, ReportStatusEnum.WritingFile, destinationType.GetDescription()).ConfigureAwait(false);

            report.ExtensionType = stream.ExtensionType;

            if (destinationType == FileDestinationTypeEnum.Database)
                report.ExcelFile = stream.Stream.ToArray();
            else if (destinationType == FileDestinationTypeEnum.Directory)
                report.FullPath = _directoryFileManagerService.SaveExportFileOnDirectoryPath(report, stream);

            await _reportExportRepository.UpdateAsync(report).ConfigureAwait(false);
        }

        public async Task<(int Type, byte[] Stream, ReportFileExtensionEnum ExtensionType)> GetReportFileAsync(int reportId)
        {
            var report = await _reportExportRepository
                                   .GetAsync(reportId)
                                   .ConfigureAwait(false);

            var file = GetFile(report);

            return (report.Type, file, (report?.ExtensionType ?? ReportFileExtensionEnum.Xlsx));
        }

        private byte[] GetFile(ReportExport report)
        {
            var file = report?.ExcelFile;

            var destinationType = _configurationHelperService.GetFileDestinationType();
            if (report != null && destinationType == FileDestinationTypeEnum.Directory)
                file = _directoryFileManagerService.GetFile(report);

            return file;
        }

        private ReportExportLog ReportLog(ReportExport report, string log)
        {
            return new ReportExportLog()
            {
                Log = log,
                ReportExportId = report.Id
            };
        }

        public Task<ReportExport> GetLastReportExportByType(int type) =>
            _reportExportRepository.GetLastReportExportByType(type);
    }
}
