using System;
using System.Windows.Forms;
using WinForms.PowerTools.Controls;
using WinForms.PowerToolsDemo.DataBindingConverters;
using static WinForms.PowerTools.Components.BindingTypeConverterExtender;

namespace WinForms.PowerToolsDemo
{
    public partial class ControlsTestForm2 : Form
    {
        public ControlsTestForm2()
        {
            InitializeComponent();
        }

        private void ControlsTestForm2_Load(object sender, EventArgs e)
        {
            _demoViewModelBindingSource.DataSource = new DemoViewModel();

            _bindingTypeConverterExtender.SetBindingConverterSettings(_optLightMode,
            [
                new BindingConverterSetting(
                    _optLightMode,
                    nameof(_optLightMode.Command), 
                    typeof(IntToSegoeFluentIconsConverter)),
            ]);
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
