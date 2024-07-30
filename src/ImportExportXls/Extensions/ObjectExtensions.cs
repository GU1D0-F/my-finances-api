namespace ImportExportXls.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmpty(this object value)
        {
            return value == null || value.ToString().Trim().Equals(String.Empty);
        }
    }
}
