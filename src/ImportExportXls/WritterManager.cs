using ClosedXML.Excel;
using ImportExportXls.Consts;
using ImportExportXls.Enums;
using ImportExportXls.Extensions;

namespace ImportExportXls
{
    public class WritterManager<T> : ExportProcessor<T> where T : new()
    {
        internal IXLWorksheet ActiveWorksheet { get; set; }
        private int CurrentRowIndex { get; set; } = 1;

        internal void FormatTable()
        {
            var ntFirstCell = ActiveWorksheet.Cell(1, 1);
            var ntLastCell = ActiveWorksheet.Cell(DataToExport.Count + 1, Columns.Count);
            ActiveWorksheet.ShowZeros = true;
            var namesTable = ActiveWorksheet.Range(ntFirstCell, ntLastCell).CreateTable();
            namesTable.Theme = XLTableTheme.TableStyleMedium9;
        }

        internal void WriteFile()
        {
            WriteHeaders();
            WriteContentLines();

            foreach (var column in Columns)
            {
                ActiveWorksheet.Column(column.Index).AdjustToContents();
            }

        }

        internal void FormatColumns()
        {
            foreach (var column in Columns)
            {
                var mask = FormatarMascara(column.Mask, column.Formatacao);

                if (mask.IsNullOrEmpty())
                    continue;

                var range = ActiveWorksheet.Range(2, column.Index, Data.Count + 1, column.Index);
                range.Style.NumberFormat.SetFormat(mask);
            }
        }

        private string FormatarMascara(string mask, FormatacaoEnum formatacao)
        {
            if (mask.Trim() != "")
                return mask;

            switch (formatacao)
            {
                case FormatacaoEnum.Valor:
                    return ExportImportXlsConsts.ValorMask;
                case FormatacaoEnum.Quantidade:
                case FormatacaoEnum.Percentual:
                    return ExportImportXlsConsts.QuantidadeMask;
                case FormatacaoEnum.Aliquota:
                    return ExportImportXlsConsts.AliquotaMask;
                case FormatacaoEnum.Cnpj:
                    return ExportImportXlsConsts.CnpjMask;
                case FormatacaoEnum.CnpjOuCnpjRaiz:
                    return ExportImportXlsConsts.CnpjOuCnpjRaiz;
                case FormatacaoEnum.Data:
                    return ExportImportXlsConsts.DateMask;
                case FormatacaoEnum.DataEHora:
                    return ExportImportXlsConsts.DateTimeMask;
                case FormatacaoEnum.Numero:
                    return ExportImportXlsConsts.NumeroMask;
                case FormatacaoEnum.ForceText:
                    return ExportImportXlsConsts.ForceTextMask;
                default:
                    return "";
            }
        }

        internal void WriteHeaders()
        {
            foreach (var column in Columns)
                SetCellValue(column.Index, column.Label, TypesEnum.String, column.Mask, true);

            CurrentRowIndex++;
        }

        internal void WriteContentLines()
        {
            foreach (var rowData in DataToExport)
            {
                foreach (var column in Columns)
                {
                    rowData.TryGetValue(column.Label, out var value);
                    SetCellValue(column.Index, value.Value, value.Type, column.Mask);
                }
                CurrentRowIndex++;
            }
        }
        private void SetCellValue(int columnIndex, Object value, TypesEnum type, string mask, bool wrapText = false)
        {
            var cell = ActiveWorksheet.Cell(CurrentRowIndex, columnIndex);

            if (type == TypesEnum.String)
            {
                var data = (string)value;

                cell.Value = data;
                cell.Style.Alignment.SetWrapText(wrapText);
            }


            if (type == TypesEnum.Int || type == TypesEnum.NullableInt)
            {
                if (value != null)
                    cell.Value = (int)value;
            }

            if (type == TypesEnum.Long || type == TypesEnum.NullableLong)
            {
                if (value != null)
                    cell.Value = (long)value;
            }

            if (type == TypesEnum.Decimal || type == TypesEnum.NullableDecimal)
            {
                if (value != null)
                    cell.Value = (decimal)value;
            }


            if (type == TypesEnum.DateTime || type == TypesEnum.NullableDateTime)
            {
                if (value != null && (((DateTime)value) != default))
                    cell.Value = ((DateTime)value);
            }

            if (type == TypesEnum.Bool || type == TypesEnum.NullableBool)
            {
                if (value != null)
                    cell.Value = ((bool)value);
            }
        }
    }
}
