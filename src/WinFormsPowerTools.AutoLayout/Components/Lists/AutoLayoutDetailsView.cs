using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTableView<T, U>
        : AutoLayoutComponent<T>
        where T : INotifyPropertyChanged
        where U : INotifyPropertyChanged
    {
        public AutoLayoutTableView(
            AutoLayoutSchema<U> autoLayoutSchema,
            string name = "detailsView1",
            ObservableCollection<U>? dataSource = default,
            params AutoLayoutBinding[] bindings)
            : base(name, bindings: bindings)
        {
            AutoLayoutSchema = autoLayoutSchema;
            DataSource = dataSource;
        }

        public AutoLayoutSchema<U> AutoLayoutSchema { get; init; }
        public ObservableCollection<U>? DataSource { get; init; }
    }
}
