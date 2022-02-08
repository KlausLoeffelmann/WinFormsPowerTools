#nullable enable

namespace System.Windows.Forms
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
