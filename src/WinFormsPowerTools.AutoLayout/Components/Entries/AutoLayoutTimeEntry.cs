using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTimeEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTimeEntry(
            string? name = "timeEntry1",
            TimeOnly? value = default,
            string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
            Value = value;
        }
        public TimeOnly? Value { get; private set; }
    }
}
