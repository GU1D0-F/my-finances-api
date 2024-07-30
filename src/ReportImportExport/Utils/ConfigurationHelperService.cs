using Microsoft.Extensions.Configuration;
using ReportImportExport.Consts;
using ReportImportExport.Enums;

namespace ReportImportExport.Utils
{
    public class ConfigurationHelperService : IConfigurationHelperService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FileDestinationTypeEnum GetFileDestinationType()
        {
            var defaultType = ExportImportReportConsts.FileDestinationTypeDefaultValue;
            var configurationType = _configuration[ExportImportReportConsts.FileDestinationTypeConfigurationName];

            if (string.IsNullOrEmpty(configurationType))
                return defaultType;

            var type = Enum.GetValues(typeof(FileDestinationTypeEnum))
                .Cast<FileDestinationTypeEnum?>()
                .FirstOrDefault(value => value.GetDescription() == configurationType);

            if (type.HasValue)
                return type.Value;

            return defaultType;
        }
        public int GetMaxDegreeOfParallelism()
        {
            _ = int.TryParse(_configuration[ExportImportReportConsts.MaxDegreeOfParallelism], out var maxValues);
            return maxValues > 0
                        ? maxValues
                        : ExportImportReportConsts.MinDegreeOfParallelism;
        }
        public string GetFileDestinationBasePathName()
        {
            return _configuration[ExportImportReportConsts.FileDestinationBasePathNameConfigurationName]
                ?? ExportImportReportConsts.FileDestinationBasePathNameDefaultValue;
        }

        public int GetExportCleanTime()
        {
            var timeString = _configuration[ExportImportReportConsts.ExportCleanInHoursCronConfigurationName];

            return int.TryParse(timeString, out var intValue)
                ? intValue
                : ExportImportReportConsts.ExportCleanInHoursDefaultValue;
        }

        public int GetImportCleanTime()
        {
            var timeString = _configuration[ExportImportReportConsts.ImportCleanInHoursCronConfigurationName];

            return int.TryParse(timeString, out var intValue)
                ? intValue
                : ExportImportReportConsts.ImportCleanInHoursDefaultValue;
        }

        public int GetErrorTime()
        {
            var timeString = _configuration[ExportImportReportConsts.AutoErrorInMinutesCronConfigurationName];

            return int.TryParse(timeString, out var intValue)
                ? intValue
                : ExportImportReportConsts.AutoErrorInMinutesDefaultValue;
        }

        public string GetImportQueueName()
        {
            return _configuration[ExportImportReportConsts.ReportImportQueueName]
                ?? ExportImportReportConsts.ReportImportQueueNameDefaultValue;
        }

        public string GetExportQueueName()
        {
            return _configuration[ExportImportReportConsts.ReportExportQueueName]
                ?? ExportImportReportConsts.ReportExportQueueNameDefaultValue;
        }

        public string GetCleanQueueName()
        {
            return _configuration[ExportImportReportConsts.ReportCleanExportImportQueueName]
                ?? ExportImportReportConsts.ReportCleanExportImportQueueNameDefaultValue;
        }
    }
}
