using System;
using System.ComponentModel;

namespace Warp.AutoLayout
{
    public class AutoLayoutTimeEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTimeEntry(
            string? name = "timeEntry1",
            TimeOnly? value = default,
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings)
            : base(name, bindings: bindings, isReadOnly: isReadOnly)
        {
            Value = value;
        }
        
        public TimeOnly? Value { get; private set; }
    }
}
