#nullable enable

using System.Windows.Forms;

namespace AirQuality.WinForms.Controls
{
    public partial class DataSourceUserControl : UserControl
    {
        public DataSourceUserControl()
        {
            InitializeComponent();
        }

        public BindingSource? BindingSourceComponent { get; set; }
    }
}
