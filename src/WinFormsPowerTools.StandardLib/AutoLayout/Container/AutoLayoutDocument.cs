namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDocument<T> : AutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutDocument(string title) : base(title)
        {
        }

        public string Title { get; set; }

        public T DataContext { get; set; }
    }
}
