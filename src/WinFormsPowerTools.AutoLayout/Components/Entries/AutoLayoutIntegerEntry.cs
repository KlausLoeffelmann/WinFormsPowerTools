using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutIntegerEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutIntegerEntry(
            string? name = "integerEntry1",
            long? value = default,
            string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
            Value = value;
        }
        public long? Value { get; private set; }
    }
}
