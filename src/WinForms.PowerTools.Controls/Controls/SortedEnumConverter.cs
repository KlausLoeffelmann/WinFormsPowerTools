using System.Collections;
using System.ComponentModel;

namespace WinForms.PowerTools.Controls;

internal class SortedEnumConverter : EnumConverter
{
    public SortedEnumConverter(Type typeOfEnum) : base(typeOfEnum) { }

    private class EnumComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            // Cast the objects into Enums:
            if (x is Enum x_enum && y is Enum y_enum)
            {
                // return the compare results of the respective Enum names:
                return x_enum.ToString().CompareTo(y_enum.ToString());
            }

            return 0;
        }

        public static EnumComparer Default => new EnumComparer();
    }

    protected override IComparer Comparer => EnumComparer.Default;
}
