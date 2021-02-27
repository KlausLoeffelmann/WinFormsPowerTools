using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEntryForms.AutoLayout
{
    public static class AutoLayoutExtensions
    {
        public static AutoLayoutGroup SetPadding(this AutoLayoutGroup group, Padding padding)
        {
            group.Padding = padding;
            return group;
        }

        public static AutoLayoutComponent SetMargin(this AutoLayoutComponent component, Padding margin)
        {
            component.Margin = margin;
            return component;
        }

        public static AutoLayoutDocument SetTitle(this AutoLayoutDocument document, string title)
        {
            document.Title = title;
            return document;
        }

        public static AutoLayoutTab AddTab(this AutoLayoutGroup group, string tabName)
        {
            var tab = new AutoLayoutTab(tabName);
            group.Components.Add(tab);
            return tab;
        }

        public static AutoLayoutComponent AddComponent(this AutoLayoutGroup group, string componentName)
        {
            var component = new AutoLayoutComponent(componentName);
            group.Components.Add(component);
            return component;
        }

        public static void AddComponents(this AutoLayoutGroup group, List<AutoLayoutComponent> components)
        {
            group.Components.AddRange(components);
        }
    }
}
