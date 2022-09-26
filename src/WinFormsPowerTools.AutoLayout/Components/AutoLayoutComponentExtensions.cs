using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutComponentExtensions
    {
        public static AutoLayoutComponent<T> SetName<T>(
            this AutoLayoutComponent<T> component, 
            string name) 
            where T : INotifyPropertyChanged
        {
            component.Name = name;
            return component;
        }

        public static AutoLayoutComponent<T> SetTag<T>(
            this AutoLayoutComponent<T> component, 
            object? tag) 
            where T : INotifyPropertyChanged
        {
            component.Tag = tag;
            return component;
        }

        public static AutoLayoutComponent<T> SetMargin<T>(
            this AutoLayoutComponent<T> component, 
            AutoLayoutPadding margin) 
            where T : INotifyPropertyChanged
        {
            component.Margin = margin;
            return component;
        }
    }
}
