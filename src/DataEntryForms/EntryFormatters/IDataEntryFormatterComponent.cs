using System.ComponentModel;

namespace System.Windows.Forms.DataEntryForms.EntryFormatters
{
    public interface IDataEntryFormatterComponent : IComponent, ISupportInitialize
    {
        bool TryConvertToValue(DataEntry dataEntry, string stringValue);
        string ConvertToDisplay(DataEntry dataEntry);
        string InitializeEditedValue(DataEntry dataEntry);
        object GetValue(DataEntry dataEntry);
        void SetValue(DataEntry dataEntry, object value);
        void SetDefaultFormatterInstanceOnDemand(DataEntry dataEntry);
        object GetDefaultValue();
    }
}
