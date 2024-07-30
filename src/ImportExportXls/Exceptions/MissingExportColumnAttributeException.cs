namespace ImportExportXls.Exceptions
{
    internal class MissingExportColumnAttributeException : Exception
    {
        public MissingExportColumnAttributeException(string message) : base(message)
        {
        }

        public MissingExportColumnAttributeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingExportColumnAttributeException()
        {
        }
    }
}
