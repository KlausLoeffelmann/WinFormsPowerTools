namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTab<T> : AutoLayoutContainer<T> where T : IFormsController
    {
        public AutoLayoutTab(string tabName) : base(tabName)
        {
        }
    }
}
