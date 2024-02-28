using System;
using System.Windows.Forms;
using System.Windows.Forms.TemplateBinding;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public partial class TemplateBindingTestForm : Form
    {
        Chain? _chain;

        public TemplateBindingTestForm()
        {
            InitializeComponent();
        }

        public void Test()
        {
            Type dataSourceType = typeof(Employee);
            string propertyPath = $"{nameof(Employee)}.{nameof(Employee.Contact)}.{nameof(Employee.Contact.Address)}.{nameof(Employee.Contact.Address.City)}";
        }

        public new Employee? DataContext
        {
            get => (Employee?)(_chain is null
                ? null
                : _chain.DataContext);

            set
            {
                if (_chain is null)
                {
                    _chain = new Chain(value);
                    BuildPropertyTree();
                    this.Disposed += TemplateBindingTestForm_Disposed;
                    _chain.ChainValueChanged += _chain_ChainValueChanged;
                }
                else
                {
                    _chain.DataContext = value;
                }
            }
        }

        private void _chain_ChainValueChanged(object? sender, ChainValueChangedEventArgs e)
        {
            // TODO: Assign DataSource to View value changed.
        }

        private void TemplateBindingTestForm_Disposed(object? sender, System.EventArgs e)
        {
            if (_chain is not null)
            {
                _chain.ChainValueChanged -= _chain_ChainValueChanged;
                this.Disposed -= TemplateBindingTestForm_Disposed;
                _chain.DataContext = null;
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

            var node = _chain!.RootLink.AddLink(dataContext => ((Employee?)dataContext)?.Contact, nameof(Employee.Contact));
            node = node.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address, nameof(Employee.Contact.Address));
            node = node.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address?.City, nameof(Employee.Contact.Address.City));
            return node;
        }
    }
}
