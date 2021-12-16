using System;
using System.Diagnostics;

namespace System.Windows.Forms.TemplateBinding
{
    [DebuggerDisplay($"")]
    public class Chain
    {
        public event EventHandler<ChainValueChangedEventArgs>? ChainValueChanged;

        private Action<ChainLink>? _removeAction;
        private Action<ChainLink, string, ChainValueChangedReason>? _valueChangedAction;

        public ChainLink RootLink { get; protected set; }

        public Chain(object? dataContext)
        {
            DataContext = dataContext;
            _removeAction = RemoveActionProc;
            _valueChangedAction = ChainValueChangedProc;

            RootLink = new ChainLink(
                dataContextValueGetter: dataContext => dataContext,
                "Root",
                tree: this,
                _removeAction,
                _valueChangedAction);
        }

        public object? DataContext { get; set; }

        private static void RemoveActionProc(ChainLink treeNode)
        {
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
