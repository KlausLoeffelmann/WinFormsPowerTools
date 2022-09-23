using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDateEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDateEntry(
            string? name = "dateEntryName1",
            DateTime? value = default,
            string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
            Value = value;
        }
        
        public DateTime? Value { get; private set; }
    }
}
