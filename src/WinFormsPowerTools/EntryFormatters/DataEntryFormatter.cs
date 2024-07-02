using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms.DataEntryForms.EntryFormatters;

namespace System.Windows.Forms.DataEntryForms
{
    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    public abstract class DataEntryFormatter<T> : INotifyPropertyChanged, IDataEntryFormatter<T>
    {
        /// <summary>
        /// event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and notifies
        /// listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This value
        /// is optional and can be provided automatically when invoked from compilers that support
        /// CallerMemberName.</param>
        /// <param name="actionOnValidate">Action, which will be executes, when the validation succeeds.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.
        /// </returns>
        protected bool SetProperty<PropType>(
            ref PropType storage,
            PropType value,
            [CallerMemberName] string? propertyName = null,
            Func<bool>? actionOnValidate = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            if (!(actionOnValidate is not null && actionOnValidate()))
            {
                storage = value;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This value
        /// is optional and can be provided automatically when invoked from compilers that support
        /// <see cref="CallerMemberNameAttribute"/>.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract T? ConvertToValue(string? stringValue);
        public abstract string? ConvertToDisplay(T? value);
        public abstract string? InitializeEditedValue(T? value);
    }
}
