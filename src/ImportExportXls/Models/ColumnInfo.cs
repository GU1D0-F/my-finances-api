using Fasterflect;
using ImportExportXls.Enums;
using System.Drawing;

namespace ImportExportXls.Models
{
    internal class ColumnInfo<T>
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public TypesEnum Type { get; set; }
        public MemberGetter GetValueFunc { get; set; }
        public MemberSetter SetValueFunc { get; set; }
        public Color HeaderBackgroundColor { get; set; }
        public string Mask { get; set; }
        public FormatacaoEnum Formatacao { get; set; } = FormatacaoEnum.Default;
    }
}
