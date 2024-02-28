using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms.TemplateBinding
{
    /// <summary>
    ///  Represents a link in a chain for data binding.
    /// </summary>    
    public class ChainLink : IEnumerable<ChainLink>
    {
        private ChainLink? _link;
        private object? _value;
        private bool _valueInitializationFlag;
        private readonly bool _isSealingLink;

        /// <summary>
        ///  Internal constructor for creating a ChainLink instance.
        /// </summary>
        internal ChainLink(
            Func<object?, object?> dataContextValueGetter,
            string propertyName,
            Chain chain,
            Action<ChainLink, string, ChainValueChangedReason>? valueChangedAction,
            bool isSealingLink = false)
        {
            PropertyName = propertyName;
            Chain = chain;
            DataContextNodeValueGetAction = dataContextValueGetter;
            ValueChangedAction = valueChangedAction;
            _isSealingLink = isSealingLink;
            
            SetValue(
                value: DataContextNodeValueGetAction(Chain.DataContext),
                isValueInitialization: true);
        }

        /// <summary>
        ///  Gets the next link in the chain.
        /// </summary>
        public ChainLink? Link
            => _link;

        /// <summary>
        ///  Gets a value indicating whether the link seals the chain.
        /// </summary>
        public bool IsSealingLink => _isSealingLink;

        /// <summary>
        ///  Gets or sets the Chain to which this ChainLink belongs.
        /// </summary>
        public Chain Chain { get; internal set; }

        /// <summary>
        ///  Gets or sets the parent link in the chain.
        /// </summary>
        public ChainLink? ParentLink { get; internal set; }

        /// <summary>
        ///  Gets or sets the action to execute when the link is removed.
        /// </summary>
        public Action<ChainLink>? RemoveAction { get; internal set; }

        /// <summary>
        ///  Gets or sets the action to execute when the value of the chain changes.
        /// </summary>
        public Action<ChainLink, string, ChainValueChangedReason>? ValueChangedAction { get; internal set; }

        /// <summary>
        ///  Gets or sets the function to extract the value from the data context.
        /// </summary>
        public Func<object?, object?> DataContextNodeValueGetAction { get; internal set; }

        /// <summary>
        ///  Gets the name of the property that this ChainLink represents.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///  Gets or sets the value of the property that this ChainLink represents.
        /// </summary>
        public object? Value
        {
            get => _value;
            set => SetValue(value);
        }

        private void SetValue(object? value, bool isValueInitialization=false)
        {
            if (!Equals(value, _value))
            {
                if (_value is INotifyPropertyChanged oldNotifyPropertyChanged)
                {
                    oldNotifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
                }

                _value = value;

                if (_value is INotifyPropertyChanged newNotifyPropertyChanged)
                {
                    newNotifyPropertyChanged.PropertyChanged += NotifyPropertyChanged_PropertyChanged;
                }

                ChainValueChangedReason valueChangedReason = isValueInitialization
                    ? _isSealingLink
                        ? ChainValueChangedReason.ValueInitialization
                        : ChainValueChangedReason.PathInitialization
                    : _link is null
                        ? ChainValueChangedReason.ValueAssignment
                        : ChainValueChangedReason.PathAssignment;

                ValueChangedAction?.Invoke(this, PropertyName, valueChangedReason);
            }
        }

        private void NotifyPropertyChanged_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Never can Link be null, because it's the Link to the Property which changed.
            // Is the Link was null, PropertyChanged could not have been fired.
            var valueChangedReason = Link!.Link is null
                ? ChainValueChangedReason.PropertyChanged
                : ChainValueChangedReason.PathChanged;

            ValueChangedAction?.Invoke(this, e.PropertyName, valueChangedReason);
        }

        /// <summary>
        ///  Adds a new link to the chain with the given parameters.
        /// </summary>
        /// <param name="dataContextNodeValueGetAction">The function to retrieve the value from the data context.</param>
        /// <param name="propertyName">The name of the property this link represents.</param>
        /// <param name="sealChain">Indicates whether this is the last link in the chain.</param>
        /// <returns>The newly added <see cref="ChainLink"/>.</returns>
        public ChainLink AddLink(Func<object?, object?> dataContextNodeValueGetAction, string propertyName, bool sealChain = false)
            => AddLink(dataContextNodeValueGetAction, propertyName, ValueChangedAction, RemoveAction, sealChain);

        /// <summary>
        ///  Adds a new link to the chain with the given parameters.
        /// </summary>
        /// <param name="dataContextNodeValueGetAction">The function to retrieve the value from the data context.</param>
        /// <param name="propertyName">The name of the property this link represents.</param>
        /// <param name="valueChangedAction">The action to be executed when the property value changes.</param>
        /// <param name="removeAction">The action to be executed when the link is removed.</param>
        /// <param name="sealChain">Indicates whether this is the last link in the chain.</param>
        /// <returns>The newly added <see cref="ChainLink"/>.</returns>
        public ChainLink AddLink(
            Func<object?, object?> dataContextNodeValueGetAction,
            string propertyName,
            Action<ChainLink, string, ChainValueChangedReason>? valueChangedAction,
            Action<ChainLink>? removeAction,
            bool sealChain = false)
        {
            var link = new ChainLink(
                dataContextNodeValueGetAction,
                propertyName,
                Chain,
                valueChangedAction,
                sealChain)
            {
                ParentLink = this
            };

            _link = link;

            return link;
        }

        /// <summary>
        ///  Gets an enumerator for traversing the chain of links.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        /// <summary>
        ///  Gets an enumerator for traversing the chain of links.
        /// </summary>
        /// <returns>An enumerator of ChainLink.</returns>
        public IEnumerator<ChainLink> GetEnumerator()
        {
            return YieldLink(this).GetEnumerator();

            IEnumerable<ChainLink> YieldLink(ChainLink link)
            {
                if (link.Link is not null)
                {
                    foreach (var subLink in YieldLink(link.Link))
                    {
                        yield return subLink;
                    }
                }

                yield return link;
            }
        }

        /// <summary>
        ///  Converts the chain of links to a string representation.
        /// </summary>
        /// <returns>A string representing the chain of links.</returns>
        public override string ToString()
        {
            StringBuilder sb = new($"{PropertyName}:{Value}");
            var parent = ParentLink;
            while (parent is not null)
            {
                sb = sb.Insert(0, $"{parent.PropertyName}.");
                parent = parent.ParentLink;
            }

            return sb.ToString();
        }
    }
}
