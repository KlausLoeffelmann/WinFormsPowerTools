using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDateEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDateEntry(
            string? name = "dateEntry1",
            DateTime? value = default,
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings)
            : base(name, bindings: bindings, isReadOnly: isReadOnly)
        {
            Value = value;
        }

        public DateTime? Value { get; private set; }
    }
}