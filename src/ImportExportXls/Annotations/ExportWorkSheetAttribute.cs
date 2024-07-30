namespace ImportExportXls.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportWorkSheetAttribute : Attribute
    {
        private readonly string AutoOpenXml_Name;

        public ExportWorkSheetAttribute(string name)
        {
            AutoOpenXml_Name = name;
        }
    }
}
