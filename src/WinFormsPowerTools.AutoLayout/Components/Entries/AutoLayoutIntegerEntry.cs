﻿using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutIntegerEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutIntegerEntry(
            string? name = "integerEntry1",
            long? value = default,
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings)
            : base(name, bindings: bindings, isReadOnly: isReadOnly)
        {
            Value = value;
        }
        
        public long? Value { get; private set; }
    }
}
