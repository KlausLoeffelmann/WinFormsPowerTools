using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms.DataEntryForms.EntryFormatters
{
    [ProvideProperty("FormattingProperties", typeof(Control)),
     ProvideProperty("Value", typeof(Control))]
    public abstract partial class DataEntryFormatterComponent<T> :
        ErrorProvider, IExtenderProvider, IDataEntryFormatterComponent
    {
        private Dictionary<Control, IDataEntryFormatter<T>> _propertyStorage = new Dictionary<Control, IDataEntryFormatter<T>>();
        private Dictionary<Control, T> _valueStorage = new Dictionary<Control, T>();

        public DataEntryFormatterComponent()
            => InitializeComponent();

        public DataEntryFormatterComponent(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        bool IExtenderProvider.CanExtend(object extendee)
            => extendee is DataEntry dataEntry 
                && dataEntry.Formatter is IDataEntryFormatterComponent;

        protected T GetValueInternal(DataEntry dataEntry)
        {
            if (_valueStorage.TryGetValue(dataEntry, out T value))
            {
                return value;
            }

            return default;
        }

        protected void SetValueInternal(DataEntry dataEntry, T value)
        {
            if (!_valueStorage.TryAdd(dataEntry, value))
            {
                if (!object.Equals(_valueStorage[dataEntry], value))
                {
                    _valueStorage[dataEntry] = value;
                    dataEntry.OnObjectValueChanged();
                }
            }
            else
            {
                dataEntry.OnObjectValueChanged();
            }
        }

        public IDataEntryFormatter<T> GetFormattingProperties(Control dataEntry)
        {
            if (_propertyStorage.TryGetValue(dataEntry, out IDataEntryFormatter<T> value))
            {
                return value;
            }

            return null;
        }

        public void SetFormattingProperties(Control dataEntry, IDataEntryFormatter<T> value)
            => _propertyStorage.TryAdd(dataEntry, value);

        abstract public T GetValue(Control dataEntry);
        abstract public void SetValue(Control dataEntry, T value);

        object IDataEntryFormatterComponent.GetValue(DataEntry dataEntry) 
            => GetValue(dataEntry);

        void IDataEntryFormatterComponent.SetValue(DataEntry dataEntry, object value)
            => SetValue(dataEntry, (T) value);

        public bool TryConvertToValue(DataEntry dataEntry, string stringValue)
        {
            T valueTemp;
            
            try
            {
                valueTemp = GetFormattingProperties(dataEntry).ConvertToValue(stringValue);
            }
            catch (System.Exception)
            {
                return false;
            }

            SetValue(dataEntry, valueTemp);

            return true;
        }

        public string ConvertToDisplay(DataEntry dataEntry) 
            =>  GetFormattingProperties(dataEntry)?.ConvertToDisplay(GetValue(dataEntry));

        public string InitializeEditedValue(DataEntry dataEntry) 
            => GetFormattingProperties(dataEntry)?.InitializeEditedValue(GetValue(dataEntry));

        void IDataEntryFormatterComponent.SetDefaultFormatterInstanceOnDemand(DataEntry dataEntry)
        {
            SetFormattingProperties(dataEntry, GetDefaultFormatterInstance());
        }

        object IDataEntryFormatterComponent.GetDefaultValue()
        {
            return GetDefaultValue();
        }

        abstract protected IDataEntryFormatter<T> GetDefaultFormatterInstance();
        protected virtual object GetDefaultValue()
        {
            return default(T);
        }
    }
}
