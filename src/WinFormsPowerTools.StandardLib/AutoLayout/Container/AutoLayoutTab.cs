namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTab<T> : AutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutTab(string tabName) : base(tabName)
        {
        }
    }
}
