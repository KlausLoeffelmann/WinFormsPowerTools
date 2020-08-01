namespace System.Windows.Forms.DataEntryForms.EntryFormatters
{
    public partial class DecimalEntryFormatterComponent : DataEntryFormatterComponent<Decimal>
    {
        public override decimal GetValue(Control dataEntry)
        {
            return base.GetValueInternal((DataEntry) dataEntry);
        }

        public override void SetValue(Control dataEntry, decimal value)
        {
            base.SetValueInternal((DataEntry) dataEntry, value);
        }

        public string InitializeEditedValue(decimal value)
        {
            return value.ToString();
        }

        protected override IDataEntryFormatter<decimal> GetDefaultFormatterInstance()
        {
            return (IDataEntryFormatter<decimal>) new DecimalDataEntryFormatter();
        }
    }
}
