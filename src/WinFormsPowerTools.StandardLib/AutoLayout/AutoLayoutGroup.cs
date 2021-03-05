namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T> : AutoLayoutContainer<T> where T : IFormsController
    {
        public AutoLayoutGroup(string frameName) : base(frameName)
        {
        }
    }
}
