namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTextBox<T>
        : AutoLayoutComponent<T> where T : IViewController
    {
        public AutoLayoutTextBox(string name, string text) : base(name)
        {
            base.Text = text;
        }
    }
}
