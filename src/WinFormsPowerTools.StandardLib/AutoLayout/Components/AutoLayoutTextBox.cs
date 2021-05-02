namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTextEntry<T>
        : AutoLayoutComponent<T> where T : IViewController
    {
        public AutoLayoutTextEntry(string name, string text = null) 
            : base(name)
        {
            base.Text = text;
        }

        public bool IsReadOnly { get; set; }
    }

    public class AutoLayoutMultilineEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutMultilineEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutPasswordEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutPasswordEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutIntegerEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutIntegerEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDecimalEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutDecimalEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutDateEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutDateEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }

    public class AutoLayoutTimeEntry<T>
        : AutoLayoutTextEntry<T> where T : IViewController
    {
        public AutoLayoutTimeEntry(string name, string text) 
            : base(name)
        {
            base.Text = text;
        }
    }
}
