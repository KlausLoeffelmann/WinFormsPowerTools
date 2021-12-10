using System;
using System.ComponentModel;

namespace System.Windows.Forms.TemplateBinding
{
    public class NestedPropertyChangedManager : PcmTree<object>
    {
        Action<PcmTreeNode<object>>? _removeAction = (node) =>
         {

         };

        Action<PcmTreeNode<object>, object?>? _valueChangedAction = (node, oldValue) =>
           {
           };

        public NestedPropertyChangedManager(object? rootValue, 
            EventHandler<NodeValueChangedEventArgs<object>> nodeValueChangedAction) 
            : base(rootValue, nodeValueChangedAction)
        {
            RootNode.RemoveAction = _removeAction;
            RootNode.ValueChangedAction = _valueChangedAction;
        }

        // TODO: Implement remove action and valuechangedaction.
        public object? DataContext
        {
            get => RootNode?.Value;
            set
            {
                if (!Equals(value, DataContext))
                {
                    RootNode.Remove();
                }

                RootNode = new PcmTreeNode<object>(value, _removeAction, _valueChangedAction);
            }
        }
    }
}
