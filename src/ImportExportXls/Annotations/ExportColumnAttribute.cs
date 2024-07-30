using ImportExportXls.Enums;

namespace ImportExportXls.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportColumnAttribute : Attribute
    {
        private readonly string AutoOpenXml_Header;
        private readonly int AutoOpenXml_Index;
        private readonly string AutoOpenXml_Mask;
        private readonly FormatacaoEnum AutoOpenXml_Formatacao;

        public ExportColumnAttribute(string header, int index = -1, string mask = "", FormatacaoEnum formatacao = FormatacaoEnum.Default)
        {
            AutoOpenXml_Header = header;
            AutoOpenXml_Index = index;
            AutoOpenXml_Mask = mask;
            AutoOpenXml_Formatacao = formatacao;
        }
    }
}
