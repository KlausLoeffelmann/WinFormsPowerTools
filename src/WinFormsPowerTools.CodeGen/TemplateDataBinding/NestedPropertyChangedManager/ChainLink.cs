using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms.TemplateBinding
{
    public class ChainLink : IEnumerable<ChainLink>
    {
        private ChainLink? _link;
        private object? _value;
        private bool _valueInitializationFlag;
        private readonly bool _isSealingLink;

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
            _valueInitializationFlag = true;
            Value = dataContextValueGetter.Invoke(Chain.DataContext);
            _valueInitializationFlag = false;
        }

        public ChainLink? Link
            => _link;

        public bool IsSealingLink => _isSealingLink;
        public Chain Chain { get; internal set; }
        public ChainLink? ParentLink { get; internal set; }
        public Action<ChainLink>? RemoveAction { get; internal set; }
        public Action<ChainLink, string, ChainValueChangedReason>? ValueChangedAction { get; internal set; }
        public Func<object?, object?> DataContextNodeValueGetAction { get; internal set; }
        public string PropertyName { get; }

        public object? Value
        {
            get => _value;
            set
            {
                if (!Equals(value, _value))
                {
                    object? oldValue = _value;

                    if (_value is INotifyPropertyChanged notifyPropertyChanged)
                    {
                        notifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
                    }

                    _value = value;

                    if (_value is INotifyPropertyChanged newNotifyPropertyChanged)
                    {
                        newNotifyPropertyChanged.PropertyChanged += NotifyPropertyChanged_PropertyChanged;
                    }

                    oldValue = default;

                    ChainValueChangedReason valueChangedReason = _valueInitializationFlag
                        ? _isSealingLink 
                            ? ChainValueChangedReason.ValueInitialization 
                            : ChainValueChangedReason.PathInitialization
                        : Link is null 
                            ? ChainValueChangedReason.ValueAssignment
                            : ChainValueChangedReason.PathAssignment;

                    ValueChangedAction?.Invoke(this, PropertyName, valueChangedReason);
                }
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

        public ChainLink AddLink(Func<object?,object?> dataContextNodeValueGetAction, string propertyName, bool sealChain=false)
        {
            return AddLink(dataContextNodeValueGetAction, propertyName, ValueChangedAction, RemoveAction, sealChain);
        }

        public ChainLink AddLink(
            Func<object?, object?> dataContextNodeValueGetAction,
            string propertyName,
            Action<ChainLink, string, ChainValueChangedReason>?
            valueChangedAction,
            Action<ChainLink>? removeAction,
            bool sealChain = false)
        {
            var link = new ChainLink(
                dataContextNodeValueGetAction,
                propertyName,
                Chain,
                valueChangedAction,
                sealChain);

            link.ParentLink = this;
            _link = link;

            return link;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ChainLink> GetEnumerator()
        {
            // Layaway along the chain.
            return YieldLink(this);

            IEnumerator<ChainLink> YieldLink(ChainLink link)
            {
                if (link.Link is not null)
                {
                    YieldLink(link.Link);
                }

                yield return link;
            }
        }

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
