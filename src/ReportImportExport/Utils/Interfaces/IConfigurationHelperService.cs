using ReportImportExport.Enums;

namespace ReportImportExport.Utils
{
    public interface IConfigurationHelperService
    {
        FileDestinationTypeEnum GetFileDestinationType();

        string GetFileDestinationBasePathName();

        int GetExportCleanTime();

        int GetImportCleanTime();

        int GetErrorTime();

        string GetImportQueueName();

        string GetExportQueueName();

        string GetCleanQueueName();

        int GetMaxDegreeOfParallelism();
    }
}
