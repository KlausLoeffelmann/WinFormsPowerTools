using System.ComponentModel;

namespace WinFormsPowerTools.StandardLib.ViewControllerBaseClasses
{
    public interface INotifyPropertyChangedDocumentFactory<TSelf> : INotifyPropertyChanged
    {
        public abstract static TSelf CreateDocument();
    }
}
