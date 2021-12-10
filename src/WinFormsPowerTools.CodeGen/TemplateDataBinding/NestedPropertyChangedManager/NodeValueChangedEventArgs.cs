using System;

namespace System.Windows.Forms.TemplateBinding
{
    public class NodeValueChangedEventArgs<T> : EventArgs
    {
        public NodeValueChangedEventArgs(PcmTreeNode<T>? node, string propertyName)
        {
            Node = node;
            PropertyName = propertyName;
        }

        public PcmTreeNode<T>? Node { get; set; }
        public string PropertyName { get; set; }
    }
}
