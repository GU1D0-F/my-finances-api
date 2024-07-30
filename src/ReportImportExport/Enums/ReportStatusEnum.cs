using System.ComponentModel;

namespace ReportImportExport.Enums
{
    public enum ReportStatusEnum
    {
        [Description("Novo")]
        New = 0,
        [Description("Processando")]
        Processing = 10,
        [Description("Salvando arquivo")]
        WritingFile = 20,
        [Description("Finalizado com sucesso")]
        Finished = 30,
        [Description("Finalizado com falha")]
        Error = 100,
        [Description("Sem dados para exportar")]
        Empty = 200
    }
}
