#nullable enable

using System.Diagnostics;

namespace System.Windows.Forms.TemplateBinding
{
    /// <summary>
    ///  Represents a chain that tracks changes in a sequence of properties.
    /// </summary>
    /// <remarks>
    ///  This class is part of an infrastructure to improve WinForms DataBinding
    ///  by reducing reflection-based operations. It works by establishing a 
    ///  type-safe chain of properties to track, starting with a root data context.
    /// </remarks>
    public class Chain
    {
        /// <summary>
        /// Event that gets fired when the value in the chain changes.
        /// </summary>
        public event EventHandler<ChainValueChangedEventArgs>? ChainValueChanged;

        /// <summary>
        /// Holds the action that's invoked when a chain link value changes.
        /// </summary>
        private Action<ChainLink, string, ChainValueChangedReason>? _valueChangedAction;

        /// <summary>
        /// The root data context that the chain starts tracking from.
        /// </summary>
        private object? _dataContext;

        /// <summary>
        /// Gets the root link of the chain.
        /// </summary>
        public ChainLink RootLink { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chain"/> class.
        /// </summary>
        /// <param name="dataContext">The initial data context.</param>
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

        /// <summary>
        /// Gets or sets the data context that this chain operates on.
        /// </summary>
        public object? DataContext
        {
            get => _dataContext;
            set => AssignDataContext(value);
        }

        /// <summary>
        /// Assigns a new data context and propagates the changes down the chain.
        /// </summary>
        /// <param name="value">New data context.</param>
        private void AssignDataContext(object? value)
        {
            _dataContext = value;

            // Propagate the new data context value down the chain.
            var childLink = RootLink;
            while (childLink is not null)
            {
                childLink.Value = childLink.DataContextNodeValueGetAction(DataContext);
                childLink = childLink.Link;
            }
        }

        /// <summary>
        /// Called when a chain link value changes.
        /// </summary>
        /// <param name="link">The chain link that changed.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <param name="valueChangedReason">The reason for the value change.</param>
        protected virtual void ChainValueChangedProc(ChainLink link, string propertyName, ChainValueChangedReason valueChangedReason)
        {
            // Trigger the event to notify external subscribers.
            ChainValueChanged?.Invoke(this, new ChainValueChangedEventArgs(link, propertyName, valueChangedReason));

            // If the change reason is other than PropertyChanged, propagate the new values down the chain.
            var childLink = link.Link;
            while (childLink is not null)
            {
                childLink.Value = childLink.DataContextNodeValueGetAction(DataContext);
                childLink = childLink.Link;
            }
        }
    }
}
