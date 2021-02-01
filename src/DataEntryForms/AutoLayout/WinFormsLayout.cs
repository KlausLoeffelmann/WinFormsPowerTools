using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace DataEntryForms.AutoLayout
{
    public class ModelTypeAttribute : Attribute
    {
        public ModelTypeAttribute(Type modeltype)
        {
            ModelType = ModelType;
        }

        public Type ModelType { get; }
    }

    public class AutoLayoutComponent
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        internal string Name { get; set; }
        internal string Caption { get; set; }
        internal string ComponentTypename { get; set; }
        internal PropertyDescriptor Binding { get; set; }
    }

    public class AutoLayoutGroup : AutoLayoutComponent
    {
        public AutoLayoutGroup(string name) : base(name)
        {
        }

        private List<AutoLayoutComponent> _components;

        internal List<AutoLayoutComponent> Components
        {
            get
            {
                if (_components is null)
                {
                    _components = new List<AutoLayoutComponent>();
                }

                return _components;
            }
        }

    }

    public class AutoLayoutDocument : AutoLayoutGroup
    {
        public AutoLayoutDocument(string title) : base(title)
        {
        }

        internal Padding Padding { get; set; }
        internal string Title { get; set; }
    }

    public class WinFormsLayoutTab : AutoLayoutGroup
    {
        public WinFormsLayoutTab(string tabName) : base(tabName)
        {
        }
    }

    public class AutoLayoutFrame : AutoLayoutGroup
    {
        public AutoLayoutFrame(string frameName) : base(frameName)
        {
        }
    }

    public static class AutoLayoutExtensions
    {
        public static AutoLayoutDocument SetPadding(this AutoLayoutDocument document, Padding padding)
        {
            document.Padding = padding;
            return document;
        }

        public static AutoLayoutDocument SetTitle(this AutoLayoutDocument document, string title)
        {
            document.Title = title;
            return document;
        }

        public static WinFormsLayoutTab AddTab(this AutoLayoutDocument document, string tabName)
        {
            var tab = new WinFormsLayoutTab(tabName);
            document.Components.Add(tab);
            return tab;
        }

        public static AutoLayoutComponent AddComponent(this AutoLayoutGroup group, string componentName)
        {
            var component = new AutoLayoutComponent(componentName);
            group.Components.Add(component);
            return component;
        }
    }

    class WinFormsViewModelBase
    {
        public AutoLayoutDocument Document { get; set; } = new AutoLayoutDocument("document1");
    }

    //[ModelType(typeof(Foo))]
    class vmOptions : WinFormsViewModelBase
    {
        public vmOptions()
        {
            Document.
                SetPadding(new Padding(4, 4, 4, 4)).
                SetTitle("My Main Form.");

            Document.
                AddTab("TabName");
                //AddComponent("Button")
        }

        public DirectoryInfo PathToDataFiles { get; set; }
        public PropertyDescriptor PathToDataFilesTemplate { get; set; }
    }
}
