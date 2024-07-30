using ClosedXML.Excel;
using ImportExportXls.Exceptions;
using ImportExportXls.Models;

namespace ImportExportXls
{
    public class ReaderManager<T> where T : new()
    {
        internal IXLWorksheet ActiveWorksheet { get; set; }
        internal bool AcceptFormula { get; set; } = false;
        private int CurrentRowIndex { get; set; } = 1;

        internal bool NextRow()
        {
            if (IsLastRow()) return false;
            CurrentRowIndex++;
            return true;
        }

        private bool IsLastRow()
        {
            return ActiveWorksheet.LastRowUsed().RowNumber() - CurrentRowIndex == 0;
        }

        internal CellRead TryReadColumnInfo(int index)
        {
            var cell = ActiveWorksheet.Cell(CurrentRowIndex, index);

            CheckFormulaAcceptable(cell);

            return new CellRead()
            {
                Type = cell.DataType,
                Value = (cell.HasFormula ? cell.CachedValue : cell.Value)
            };
        }

        internal void CheckFormulaAcceptable(IXLCell cell)
        {
            if (cell.HasFormula && !AcceptFormula)
                throw new FormulaUnacceptableException("Any formula is unacceptable");

            try
            {
                var value = cell.CachedValue;
            }
            catch (Exception ex)
            {
                throw new InvalidFormulaException("The formula is invalid", ex);
            }
        }
    }
}
