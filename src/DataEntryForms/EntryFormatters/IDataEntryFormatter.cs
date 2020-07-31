using System.ComponentModel;

namespace ExtenderPropertiesTest
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
