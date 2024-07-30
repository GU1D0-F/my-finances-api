using AutoMapper;
using ClosedXML.Excel;
using ImportExportXls;
using Microsoft.AspNetCore.Identity;

namespace ReportImportExport.Export
{
    public class SimpleReportExportService : ISimpleReportExportService
    {
        private readonly IMapper _mapper;
        private readonly IReportExportLogsRepository _reportExportLogRepository;

        public SimpleReportExportService(IMapper mapper, IReportExportLogsRepository reportExportLogRepository)
        {
            _mapper = mapper;
            _reportExportLogRepository = reportExportLogRepository;

        }

        public async Task InsertLogAsync(int reportId, string log)
        {
            var reportLog = new ReportExportLog()
            {
                Log = log,
                ReportExportId = reportId
            };

            await _reportExportLogRepository.InsertAsync(reportLog);
        }

        private void InitializeList<T>(List<T> list)
        {
            if (list.Any() && list.First() == null)
                list = new List<T>();
        }

       
        public async Task<MemoryStream> ExecuteGenerationAsync<T, TR>(
            int reportId,
            string logDescription,
            IList<T> data,
            IXLWorkbook workbook = null,
            bool emptyData = false)
            where T : new()
            where TR : new()
        {
            workbook ??= new XLWorkbook();

            var stream = new MemoryStream();

            if (data != null && (data.Any() || emptyData))
            {
                await InsertLogAsync(reportId, $"Inciando busca de dados - {logDescription}").ConfigureAwait(false);

                var users = GetUsers();

                InitializeList(data.ToList());

                await InsertLogAsync(reportId, $"Ajustando usuários de Inclusão e Alteração - {logDescription}").ConfigureAwait(false);
                var dataDto = GenerateDtos<T, TR>(data, users);

                await InsertLogAsync(reportId, $"Gerando arquivo excel - {logDescription}").ConfigureAwait(false);

                stream = new ExportManagerBuilder<TR>()
                    .Init()
                    .UseWorkBook(workbook)
                    .SetData(dataDto)
                    .StartExportProcess();
            }

            return stream;

        }

        private List<IdentityUser> GetUsers()
        {
            return null;
            //    return _userRepository.GetListAsync().GetAwaiter().GetResult();
        }

        private List<TR> GenerateDtos<T, TR>(IList<T> dataList, List<IdentityUser> users)
            where T : new()
            where TR : new()
        {
            var returnData = new List<TR>();

            //if (new T() is IFullAuditedObject && new TR() is IAuditedReportDto)
            //{
            //    foreach (var data in dataList.ToList().OrderBy(o => ((IFullAuditedObject)o).CreatorId))
            //    {
            //        var dataDto = ApplyAuditory<T, TR>(data, users);
            //        returnData.Add(dataDto);
            //    }
            //}
            //else
            //{
            returnData.AddRange(_mapper.Map<IList<T>, IList<TR>>(dataList));
            //}

            return returnData;
        }

        //private TR ApplyAuditory<T, TR>(T data, List<IdentityUser> users)
        //    where T : new()
        //    where TR : new()
        //{
        //    var dataDto = _objectMapper.Map<T, TR>(data);


        //    ((IAuditedReportDto)dataDto).CreatorUser = users.FirstOrDefault(f =>
        //        f.Id == ((IFullAuditedObject)data).CreatorId)?.UserName;

        //    if (((IFullAuditedObject)data).LastModifierId.HasValue)
        //        ((IAuditedReportDto)dataDto).LastModifierUser = users.FirstOrDefault(f =>
        //            f.Id == ((IFullAuditedObject)data).LastModifierId)?.UserName;

        //    return dataDto;
        //}
    }
}
