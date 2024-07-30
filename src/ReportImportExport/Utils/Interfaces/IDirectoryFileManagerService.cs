using ReportImportExport.Base;
using ReportImportExport.Export;
using ReportImportExport.Import;

namespace ReportImportExport.Utils
{
    public interface IDirectoryFileManagerService
    {
        string SaveExportFileOnDirectoryPath(ReportExport report, ReportStreamDto stream);
        string SaveImportFileOnDirectoryPath(ReportImport report, byte[] file);
        void DeleteReportFileOnDirectoryPath(IEnumerable<string> fullPath);
        byte[] GetFile(ReportBase report);
    }
}
