namespace ImportExportXls.Exceptions
{
    internal class MissingExportWorkSheetAttributeException : Exception
    {
        public MissingExportWorkSheetAttributeException(string message) : base(message)
        {
        }

        public MissingExportWorkSheetAttributeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingExportWorkSheetAttributeException()
        {
        }
    }
}
