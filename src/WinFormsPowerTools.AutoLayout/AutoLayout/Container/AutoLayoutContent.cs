using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutContent<T>
        : IAutoLayoutContent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutContent(
            string name, 
            object? tag = null)
        {
            Name = name;
            Tag = tag;
        }

        public string Name { get; set; }
        public object? Tag { get; set; }

        public IAutoLayoutElement<T>? Content { get; set; }
    }
}
