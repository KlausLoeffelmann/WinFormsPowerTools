namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T>
        : AutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutGroup(string name) : base(name)
        {
        }
    }
}
