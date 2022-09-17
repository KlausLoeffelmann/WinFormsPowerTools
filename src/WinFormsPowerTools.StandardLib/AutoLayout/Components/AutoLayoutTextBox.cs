namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDataEntry<T>
        : AutoLayoutComponent<T> where T : IViewController
    {
        public AutoLayoutDataEntry(string name, string? text = null) 
            : base(name)
        {
            base.Text = text;
        }

        public bool IsReadOnly { get; set; }
    }

    public class AutoLayoutMultilineEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutMultilineEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutPasswordEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutPasswordEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutIntegerEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutIntegerEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDecimalEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutDecimalEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDateEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutDateEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutTimeEntry<T>
        : AutoLayoutDataEntry<T> where T : IViewController
    {
        public AutoLayoutTimeEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }
}
