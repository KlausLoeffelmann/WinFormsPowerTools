using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutComponent<T>
        : IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutComponent(
            string? name = "component1", 
            string? text = default,
            params AutoLayoutBinding[] bindings)
        {
            Name = name;
            Text = text;
            foreach (var binding in bindings) 
            {
                Bindings.AddBinding(binding);
            }
        }

        public virtual string? Name { get; set; }
        public virtual string? Text { get; internal set; }
        public string? BindingPath { get; set; }
        public virtual object? Tag { get; set; }
        public AutoLayoutBindings Bindings { get; } = new();
        public T? DataContext { get; set ; }
        public virtual AutoLayoutPadding Margin { get; internal set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
    }
}
