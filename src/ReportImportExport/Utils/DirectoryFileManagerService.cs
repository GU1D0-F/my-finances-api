using ReportImportExport.Base;
using ReportImportExport.Consts;
using ReportImportExport.Enums;
using ReportImportExport.Export;
using ReportImportExport.Import;

namespace ReportImportExport.Utils
{
    public class DirectoryFileManagerService : IDirectoryFileManagerService
    {
        private readonly IConfigurationHelperService _configurationHelperService;

        public DirectoryFileManagerService(IConfigurationHelperService configurationHelperService)
        {
            _configurationHelperService = configurationHelperService;
        }

        public string SaveExportFileOnDirectoryPath(ReportExport report, ReportStreamDto stream)
        {
            return SaveFileOnDirectoryPath(
                report,
                stream.Stream.ToArray(),
                stream.ExtensionType.GetDescription(),
                ExportImportReportConsts.SubPathReportExport);
        }

        public string SaveImportFileOnDirectoryPath(ReportImport report, byte[] file)
        {
            return SaveFileOnDirectoryPath(
                report,
                file,
                ReportFileExtensionEnum.Xlsx.GetDescription(),
                ExportImportReportConsts.SubPathReportImport);
        }

        private string SaveFileOnDirectoryPath(ReportBase report, byte[] file, string type, string subPath)
        {
            var path = GetDestinationPath(subPath);
            var fileName = GetFileName(report, type);
            var fullPath = Path.Combine(path, fileName);

            DeleteFile(fullPath);

            CreateFile(fullPath, file);

            return fullPath;
        }

        public void DeleteReportFileOnDirectoryPath(IEnumerable<string> fullPath)
        {
            _ = Parallel.ForEach(fullPath,
                new ParallelOptions { MaxDegreeOfParallelism = _configurationHelperService.GetMaxDegreeOfParallelism() },
                fileName => { DeleteFile(fileName); });
        }

        private string GetDestinationPath(string subPath)
        {
            var basePath = _configurationHelperService.GetFileDestinationBasePathName();
            var path = Path.Combine(basePath, subPath);

            Directory.CreateDirectory(path);

            return path;
        }

        private string GetFileName(ReportBase report, string type)
        {
            return $"{report.Id} - {report.Documento}.{type}";
        }

        private void DeleteFile(string fullPath)
        {
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private void CreateFile(string fullPath, byte[] file)
        {
            File.WriteAllBytes(fullPath, file);
        }

        public byte[] GetFile(ReportBase report)
        {
            return File.ReadAllBytes(report.FullPath);
        }
    }
}
