using ImportExportXls.Annotations;

namespace MyFinances.Reports.Import
{
    [ExportWorkSheet("nubank")]
    public class SpendingImportDto
    {
        [ExportColumn("date,category,title,amount", 1)]
        public string DateCategoryTitleAmount { get; set; }
    }
}
