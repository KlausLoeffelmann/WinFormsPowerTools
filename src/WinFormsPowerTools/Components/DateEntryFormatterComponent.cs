using System.Windows.Forms.DataEntryForms.Controls;
using System.Windows.Forms.DataEntryForms.EntryFormatters;

namespace System.Windows.Forms.DataEntryForms.Components
{
    public partial class DateEntryFormatterComponent : DataEntryFormatterComponent<DateTime>
    {
        public override DateTime GetValue(Control dataEntry)
        {
            return base.GetValueInternal((DataEntry) dataEntry);
        }

        public override void SetValue(Control dataEntry, DateTime value)
        {
            base.SetValueInternal((DataEntry) dataEntry, value);
        }

        public string InitializeEditedValue(DateTime value)
        {
            return value.ToString();
        }

        protected override IDataEntryFormatter<DateTime> GetDefaultFormatterInstance()
        {
            return (IDataEntryFormatter<DateTime>) new DateDataEntryFormatter();
        }

        protected override object GetDefaultValue()
        {
            return DateTime.Now.Date;
        }

        protected override bool CanExtendProperties(DataEntry dataEntry) 
            => dataEntry.Formatter is DateEntryFormatterComponent;
    }
}
