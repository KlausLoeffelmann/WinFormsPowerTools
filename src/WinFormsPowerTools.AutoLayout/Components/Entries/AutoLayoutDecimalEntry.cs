using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDecimalEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDecimalEntry(
            string? name = "decimalEntryName1", 
            Decimal? value = default,
            string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
            Value = value;
        }
        public Decimal? Value { get; private set; }
    }
}
