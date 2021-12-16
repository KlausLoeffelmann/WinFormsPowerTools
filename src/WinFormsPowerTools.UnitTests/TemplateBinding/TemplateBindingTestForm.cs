using System.Windows.Forms;
using System.Windows.Forms.TemplateBinding;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public partial class TemplateBindingTestForm : Form
    {
        public TemplateBindingTestForm()
        {
            InitializeComponent();
        }

        internal NestedPropertyChangedManager? PcmTree { get; set; }

        public new Employee? DataContext
        {
            get => (Employee?)PcmTree?.RootLink.Value;
            set
            {
                if (PcmTree is not null)
                {
                    // TODO: Clean Tree.
                    // PcmTree.RootLink.Remove();
                }

                if (value is null)
                {
                    PcmTree = null;
                    return;
                }

                PcmTree = new(value, NodeValueChangedEventProc);
                BuildPropertyTree();
                UpdateValuesFromDataSource();
            }
        }

        internal void UpdateValuesFromDataSource()
        {
            string? value = DataContext?.Contact?.Address?.City;
            this.textBox1.Text = value;
        }

        internal void UpdateValuesToDataSource()
        {
            string value = textBox1.Text;
            if (DataContext?.Contact?.Address is not null)
            {
                var address = DataContext.Contact.Address;
                address.City = value;
            }
        }

        private ChainLink BuildPropertyTree()
        {
            // We are simulating binding City, so we need to have the whole property path
            // build up as a node. This is what later needs to be code-generated from the 
            // binding definition.

            var node = PcmTree!.RootLink.AddLink(dataContext => ((Employee?)dataContext)?.Contact, nameof(Employee.Contact));
            node = node.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address, nameof(Employee.Contact.Address));
            node = node.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address?.City, nameof(Employee.Contact.Address.City));
            return node;
        }

        private void NodeValueChangedEventProc(object? sender, ChainValueChangedEventArgs e)
        {
            if (sender is Contact || sender is Address)
            {
            }
        }
    }
}
