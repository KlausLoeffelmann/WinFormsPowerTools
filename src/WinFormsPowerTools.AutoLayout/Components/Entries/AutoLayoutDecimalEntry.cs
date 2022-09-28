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
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings)
            : base(name, bindings: bindings, isReadOnly: isReadOnly)
        {
            Value = value;
        }
        
        public Decimal? Value { get; private set; }
    }
}
