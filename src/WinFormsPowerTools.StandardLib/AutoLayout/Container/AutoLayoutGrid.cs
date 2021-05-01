namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGrid<T>
        : IAutoLayoutElement where T : IViewController
    {
        public AutoLayoutGrid(string name)
        {
        }

        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public object Tag { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }

    public class AutoLayoutRow<T>
    : IAutoLayoutElement where T : IViewController
    {
        public AutoLayoutRow(string name)
        {
        }

        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public object Tag { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }

    public class AutoLayoutColumn<T>
        : AutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutColumn(string name) : base(name)
        {
        }
    }
}
