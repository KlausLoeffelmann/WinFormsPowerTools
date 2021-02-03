using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataEntryForms.TestData;

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

    public class AutoLayoutComponents
    {
        public List<AutoLayoutComponent> Components { get; set; }
        public AutoLayoutComponent LastComponent { get; set; }
    }

    public class AutoLayoutComponent
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string ComponentTypename { get; set; }
        public Padding Margin { get; set; }
        public PropertyDescriptor Binding { get; set; }
    }

    public class AutoLayoutGroup : AutoLayoutComponent
    {
        public AutoLayoutGroup(string name) : base(name)
        {
        }

        private List<AutoLayoutComponent> _components;
        private AutoLayoutComponents _chainComponents;

        public AutoLayoutComponents ChainComponents()
        {
            if (_chainComponents is null)
            {
                _chainComponents = new AutoLayoutComponents()
                {
                    Components = Components,
                    LastComponent = null
                };
            }
            return _chainComponents;
        }

        public Padding Padding { get; set; }

        public List<AutoLayoutComponent> Components
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

        public string Title { get; set; }
    }

    public class AutoLayoutTab : AutoLayoutGroup
    {
        public AutoLayoutTab(string tabName) : base(tabName)
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

    public class AutoLayoutPropertyDescriptor : PropertyDescriptor
    {
        public AutoLayoutPropertyDescriptor(PropertyDescriptor propertyDescriptor, Attribute[] attributes)
            : base(propertyDescriptor, attributes)
        {
        }

        public override Type ComponentType => throw new NotImplementedException();

        public override bool IsReadOnly => throw new NotImplementedException();

        public override Type PropertyType => throw new NotImplementedException();

        public override bool CanResetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void ResetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            throw new NotImplementedException();
        }
    }

    public class AutoLayoutProperty<T> where T : class
    {
        public AutoLayoutProperty(object @object, string propertyname)
        {
            PropertyName = PropertyName;
        }

        public string PropertyName { get; }

        public AutoLayoutPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return new AutoLayoutPropertyDescriptor(TypeDescriptor.GetProperties(typeof(T))[PropertyName], null);
            }
        }
    }

    public class AutoLayoutViewModelBase<T> where T : class
    {
        public AutoLayoutDocument Document { get; set; } = new AutoLayoutDocument("document1");
        public T DataContext { get; set; }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class vmOptionsExtensions
    {
        public static AutoLayoutComponents AddIdContact(this AutoLayoutComponents components)
        {
            if (components is null)
            {
                components = new AutoLayoutComponents() { Components = new List<AutoLayoutComponent>() };
            }
            var component = new AutoLayoutComponent("Context");
            components.Components.Add(component);
            components.LastComponent = component;
            return components;
        }
    }

    public partial class vmOptions : AutoLayoutViewModelBase<Foo>
    {

        private AutoLayoutComponent _idContactProperty;
        private AutoLayoutComponent IdContextComponent
        {
            get
            {
                // Return new Component.
                return null;
            }
        }

    }

    public partial class vmOptions : AutoLayoutViewModelBase<Foo>
    {
        public vmOptions()
        {
            Document.AddTab("TabName").ChainComponents().AddIdContact();

            Document.ChainComponents().AddIdContact().AddIdContact();

            Document.
                SetTitle("My Main Form.").SetPadding(new Padding(4, 4, 4, 4));
        }

        public DirectoryInfo PathToDataFiles { get; set; }
        public PropertyDescriptor PathToDataFilesTemplate { get; set; }
    }
}
