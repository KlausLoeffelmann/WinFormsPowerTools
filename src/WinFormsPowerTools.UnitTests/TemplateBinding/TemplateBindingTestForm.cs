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
            get => (Employee?)PcmTree?.RootNode.Value;
            set
            {
                if (PcmTree is not null)
                {
                    PcmTree.RootNode.Remove();

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

        private PcmTreeNode<object> BuildPropertyTree()
        {
            var node = PcmTree!.RootNode.AddNode(DataContext?.Contact);
            node = node.AddNode(DataContext?.Contact?.Address);
            node = node.AddNode(DataContext?.Contact?.Address?.City);
            return node;
        }

        private void NodeValueChangedEventProc(object? sender, NodeValueChangedEventArgs<object> e)
        {
            if (sender is Contact || sender is Address)
            {
            }
        }
    }
}
