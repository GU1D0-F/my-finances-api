using ImportExportXls.Annotations;
using ImportExportXls.Enums;
using MyFinances.Reports;
using MyFinances.Spendings;

namespace MyFinances.Reports.Export
{
    [ExportWorkSheet(ReportSpendingsConsts.ExportWorkSheet)]
    public class SpendingExportDto
    {
        [ExportColumn("Data", 1, formatacao: FormatacaoEnum.Data)]
        public DateTime Data { get; set; }
        [ExportColumn("Descricao", 2, formatacao: FormatacaoEnum.ForceText)]
        public string Descricao { get; set; }
        [ExportColumn("Valor", 3, formatacao: FormatacaoEnum.Valor)]
        public decimal Valor { get; set; }
        [ExportColumn("TipoTransacao", 4, formatacao: FormatacaoEnum.ForceText)]
        public TipoTransacaoEnum TipoTransacao { get; set; }
        [ExportColumn("Observacao", 5, formatacao: FormatacaoEnum.ForceText)]
        public string Observacao { get; set; }
    }
}
