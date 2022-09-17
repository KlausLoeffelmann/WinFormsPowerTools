namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutComponent<T> 
        : IAutoLayoutElement<T> where T : IViewController
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        public virtual string Name { get; set; }
        public virtual object? Tag { get; set; }
        public object? Group { get; set; }
        public virtual string? Text { get; internal set; }
        public virtual string? ComponentType { get; internal set; }
        public virtual AutoLayoutPadding Margin { get; internal set; }
        public virtual object? Binding { get; internal set; }

        // TODO: Group: Assigning certain Properties to groups of components in one go.
    }
}
