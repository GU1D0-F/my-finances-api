namespace ImportExportXls.Exceptions
{
    internal class InvalidFormulaException : Exception
    {
        public InvalidFormulaException(string message) : base(message)
        {
        }

        public InvalidFormulaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidFormulaException()
        {
        }
    }
}
