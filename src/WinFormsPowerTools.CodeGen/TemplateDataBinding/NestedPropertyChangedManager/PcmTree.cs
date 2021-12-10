using System;

namespace System.Windows.Forms.TemplateBinding
{
    public class PcmTree<T>
    {
        public PcmTreeNode<T> RootNode { get; protected set; }

        // TODO: Make protected when tested.
        public PcmTree(T? rootValue, EventHandler<NodeValueChangedEventArgs<T>> nodeValueChangedAction)
        {
            RootNode = new PcmTreeNode<T>(rootValue, null, null);
            RootNode.NodeValueChangedAction = nodeValueChangedAction;
            RootNode.NodeValueChanged += RootNode.NodeValueChangedAction;
        }

        public PcmTree(T? rootValue, Action<PcmTreeNode<T>>? removeAction, Action<PcmTreeNode<T>, T?> valueChangedAction)
        {
            RootNode = new PcmTreeNode<T>(rootValue, removeAction, valueChangedAction);
        }
    }
}
