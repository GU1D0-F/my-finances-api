using ReportImportExport.Enums;

namespace ReportImportExport.Consts
{
    public static class ExportImportReportConsts
    {
        public const string FileDestinationTypeConfigurationName = "Report:FileDestinationType";
        public const string FileDestinationBasePathNameConfigurationName = "Report:FileDestinationBasePathName";
        public const string CleanExportTableCronConfigurationName = "Report:ClearExportTableCron";
        public const string AutoErrorInMinutesCronConfigurationName = "Report:AutoErrorInMinutes";
        public const string ExportCleanInHoursCronConfigurationName = "Report:ExportCleanInHours";
        public const string ImportCleanInHoursCronConfigurationName = "Report:ImportCleanInHours";
        public const string FileDestinationBasePathNameDefaultValue = "C:\\report";
        public const FileDestinationTypeEnum FileDestinationTypeDefaultValue = FileDestinationTypeEnum.Database;
        public const int AutoErrorInMinutesDefaultValue = 120;
        public const int ExportCleanInHoursDefaultValue = 48;
        public const int ImportCleanInHoursDefaultValue = 48;
        public const string MaxDegreeOfParallelism = "Performance:MaxDegreeOfParallelism";
        public const int MinDegreeOfParallelism = 2;
        public const string ReportExportQueueNameDefaultValue = "default";
        public const string ReportExportQueueName = "Report:ReportExportQueueName";

        public const string ReportImportQueueNameDefaultValue = "default";
        public const string ReportImportQueueName = "Report:ReportImportQueueName";

        public const string ReportCleanExportImportQueueNameDefaultValue = "default";
        public const string ReportCleanExportImportQueueName = "Report:ReportCleanExportImportQueueName";
        public const string SubPathReportImport = "import";
        public const string SubPathReportExport = "export";
        public const string GenericErrorMessage =
            "Processamento finalizado com erro: exportação não foi iniciada com sucesso.";
    }
}
