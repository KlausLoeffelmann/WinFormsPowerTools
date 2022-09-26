using System.ComponentModel;

// Contains the data structure foundation of the AutoLayout concept.
namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutContainerExtensions
    {
        public static U AddComponent<T, U>(
            this U container,
            AutoLayoutComponent<T> component)
            where T : INotifyPropertyChanged
            where U : IAutoLayoutContainer<T>
        {
            container.AddComponent(component);
            return container;
        }
    }
}
