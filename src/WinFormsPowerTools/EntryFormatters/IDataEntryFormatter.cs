using System.ComponentModel;

namespace System.Windows.Forms.DataEntryForms.EntryFormatters
{
    [TypeConverter(typeof(DataEntryFormatterTypeConverter))]
    public interface IDataEntryFormatter<T>
        : INotifyPropertyChanged
    {
        T ConvertToValue(string stringValue);

        string ConvertToDisplay(T value);

        string InitializeEditedValue(T value);
    }
}
