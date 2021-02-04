using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataEntryForms.TestData;

namespace DataEntryForms.AutoLayout
{
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
