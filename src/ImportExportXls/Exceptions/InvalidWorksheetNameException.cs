namespace ImportExportXls.Exceptions
{
    public class InvalidWorksheetNameException : Exception
    {
        public InvalidWorksheetNameException(string message) : base(message)
        {
        }

        public InvalidWorksheetNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidWorksheetNameException()
        {
        }
    }
}
