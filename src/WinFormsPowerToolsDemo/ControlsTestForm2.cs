using System;
using System.Windows.Forms;
using WinForms.PowerTools.Controls;
using static WinForms.PowerTools.Components.BindingTypeConverterExtender;

namespace WinForms.PowerToolsDemo
{
    public partial class ControlsTestForm2 : Form
    {
        public ControlsTestForm2()
        {
            InitializeComponent();
            bindingConverterExtender1.SetBindingTypeConverters(_toLightMode, new BindingTypeConverterCollection()
            {
                new TypeConverterPropertyDescriptor(nameof(_toLightMode.Command)),
            });
        }

        private void ControlsTestForm2_Load(object sender, EventArgs e)
        {
            _mainBindingSource.DataSource = new DemoViewModel();
        }

        private void bindingConverterManager1_Format(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(int) && e.Value is SegoeFluentIcons enumValue)
            {
                e.Value = (int)enumValue;
            }
        }

        private void bindingConverterManager1_Parse(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(SegoeFluentIcons) && e.Value is int intValue)
            {
                e.Value = (SegoeFluentIcons)intValue;
            }
        }
    }
}
