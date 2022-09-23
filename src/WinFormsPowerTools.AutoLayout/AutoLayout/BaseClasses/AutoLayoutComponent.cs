using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutComponent<T>
        : IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutComponent(
            string? name = "componentName", 
            string? text = default, 
            string? bindingPath = default)
        {
            Name = name;
            Text = text;
            BindingPath = bindingPath;
        }

        public virtual string? Name { get; set; }
        public virtual string? Text { get; internal set; }
        public string? BindingPath { get; set; }
        public virtual object? Tag { get; set; }
        public virtual object? Binding { get; internal set; }
        public T? DataContext { get; set ; }
        public virtual AutoLayoutPadding Margin { get; internal set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
    }
}
