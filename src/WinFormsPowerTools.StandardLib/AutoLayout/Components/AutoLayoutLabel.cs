namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutLabel<T> 
        : AutoLayoutComponent<T> where T : IViewController
    {
        public AutoLayoutLabel(string name, string text) : base(name)
        {
            base.Text = text;
        }
    }
}
