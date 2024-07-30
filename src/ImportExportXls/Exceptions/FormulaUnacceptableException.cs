namespace ImportExportXls.Exceptions
{
    public class FormulaUnacceptableException : Exception
    {
        public FormulaUnacceptableException(string message) : base(message)
        {
        }

        public FormulaUnacceptableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FormulaUnacceptableException()
        {
        }
    }
}
