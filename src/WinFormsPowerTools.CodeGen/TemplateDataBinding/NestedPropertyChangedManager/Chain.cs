using System;
using System.Diagnostics;

namespace System.Windows.Forms.TemplateBinding
{
    [DebuggerDisplay($"")]
    public class Chain
    {
        public event EventHandler<ChainValueChangedEventArgs>? ChainValueChanged;

        private Action<ChainLink, string, ChainValueChangedReason>? _valueChangedAction;
        private object? _dataContext;

        public ChainLink RootLink { get; protected set; }

        public Chain(object? dataContext)
        {
            DataContext = dataContext;
            _valueChangedAction = ChainValueChangedProc;

            RootLink = new ChainLink(
                dataContextValueGetter: dataContext => dataContext,
                "Root",
                chain: this,
                _valueChangedAction);
        }

        public object? DataContext
        {
            get => _dataContext;
            set => AssignDataContext(value);
        }

        private void AssignDataContext(object? value)
        {
            _dataContext = value;

            var childLink = RootLink;
            while (childLink is not null)
            {
                childLink.Value = childLink.DataContextNodeValueGetAction(DataContext);
                childLink = childLink.Link;
            }
        }

        protected virtual void ChainValueChangedProc(ChainLink link, string propertyName, ChainValueChangedReason valueChangedReason)
        {
            ChainValueChanged?.Invoke(this, new ChainValueChangedEventArgs(link, propertyName, valueChangedReason));

            // Now we need to feed the chain's link down the road with the new values,
            // of the valueChangedReason wasn't a PropertyChanged, but a path changed.
            var childLink = link.Link;
            while (childLink is not null)
            {
                childLink.Value = childLink.DataContextNodeValueGetAction(DataContext);
                childLink = childLink.Link;
            }
        }
    }
}
