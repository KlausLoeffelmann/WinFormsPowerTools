using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutComponentExtensions
    {
        public static AutoLayoutComponent<T> SetName<T>(this AutoLayoutComponent<T> component, string name) where T : INotifyPropertyChanged
        {
            component.Name = name;
            return component;
        }

        public static AutoLayoutComponent<T> SetTag<T>(this AutoLayoutComponent<T> component, object tag) where T : INotifyPropertyChanged
        {
            component.Tag = tag;
            return component;
        }

        //public static AutoLayoutGroup<T> SetPadding<T>(this AutoLayoutGroup<T> group, Padding padding)
        //{
        //    group.Padding = padding;
        //    return group;
        //}

        //public static AutoLayoutComponent SetMargin(this AutoLayoutComponent component, Padding margin)
        //{
        //    component.Margin = margin;
        //    return component;
        //}

        //public static AutoLayoutDocument SetTitle(this AutoLayoutDocument document, string title)
        //{
        //    document.Title = title;
        //    return document;
        //}

        //public static AutoLayoutTab AddTab(this AutoLayoutGroup group, string tabName)
        //{
        //    var tab = new AutoLayoutTab(tabName);
        //    group.Components.Add(tab);
        //    return tab;
        //}

        //public static AutoLayoutComponent AddComponent(this AutoLayoutGroup group, string componentName)
        //{
        //    var component = new AutoLayoutComponent(componentName);
        //    group.Components.Add(component);
        //    return component;
        //}

        //public static void AddComponents(this AutoLayoutGroup group, List<AutoLayoutComponent> components)
        //{
        //    group.Components.AddRange(components);
        //}
    }
}
