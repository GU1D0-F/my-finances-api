using System.Drawing;

namespace ImportExportXls.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportColumnHeaderBackgoundColorAttribute : Attribute
    {
        public readonly Color HeaderBackgroundColor;

        public ExportColumnHeaderBackgoundColorAttribute(int red = 255, int green = 255, int blue = 255)
        {
            HeaderBackgroundColor = Color.FromArgb(red, green, blue);
        }
    }
}
