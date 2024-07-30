using ImportExportXls.Utils;
using System.Reflection;

namespace ImportExportXls.Extensions
{
    internal static class PropertiesExtensions
    {
        internal static List<PropertyInfo> SortFields(this List<PropertyInfo> properties)
        {
            properties.Sort((a, b) => AttributeUtils.ComparePropertyInfo(a, b));
            return properties;
        }
    }
}
