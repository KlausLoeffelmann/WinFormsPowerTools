using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDataEntry<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDataEntry(string name, string? text = null) 
            : base(name)
        {
            base.Text = text;
        }

        public bool IsReadOnly { get; set; }
    }

    public class AutoLayoutMultilineEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutMultilineEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutPasswordEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutPasswordEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutIntegerEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutIntegerEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDecimalEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDecimalEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDateEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDateEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutTimeEntry<T>
        : AutoLayoutDataEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTimeEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }
}
