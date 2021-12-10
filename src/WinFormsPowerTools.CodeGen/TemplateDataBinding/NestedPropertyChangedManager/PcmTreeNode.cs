using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.Windows.Forms.TemplateBinding
{
    public class PcmTreeNode<T> : IEnumerable<PcmTreeNode<T>>
    {
        private readonly List<PcmTreeNode<T>> _nodes = new();
        private T? _value;

        public event EventHandler<NodeValueChangedEventArgs<T>>? NodeValueChanged;

        internal PcmTreeNode(T? nodeValue, Action<PcmTreeNode<T>>? removeAction, Action<PcmTreeNode<T>, T?>? valueChangedAction)
        {
            RemoveAction = removeAction;
            ValueChangedAction = valueChangedAction;
            Value = nodeValue;
        }

        public ICollection<PcmTreeNode<T>>? Nodes
            => _nodes;

        public PcmTreeNode<T>? ParentNode { get; internal set; }
        public Action<PcmTreeNode<T>>? RemoveAction { get; internal set; }
        public Action<PcmTreeNode<T>, T?>? ValueChangedAction { get; internal set; }
        internal EventHandler<NodeValueChangedEventArgs<T>>? NodeValueChangedAction { get; set; }

        public T? Value
        {
            get => _value;
            set
            {
                if (!Equals(value, _value))
                {
                    T? oldValue = _value;

                    if (_value is INotifyPropertyChanged notifyPropertyChanged)
                    {
                        notifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
                    }

                    _value = value;

                    if (_value is INotifyPropertyChanged newNotifyPropertyChanged)
                    {
                        newNotifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
                    }

                    ValueChangedAction?.Invoke(this, oldValue);
                    oldValue = default;
                }
            }
        }

        private void NotifyPropertyChanged_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs<T>(this, e.PropertyName!));
        }

        public PcmTreeNode<T> AddNode(T? nodeValue)
        {
            return AddNode(nodeValue, RemoveAction, ValueChangedAction);
        }

        public PcmTreeNode<T> AddNode(T? nodeValue, Action<PcmTreeNode<T>>? removeAction, Action<PcmTreeNode<T>, T?>? valueChangedAction)
        {
            var node = new PcmTreeNode<T>(nodeValue, removeAction, valueChangedAction);
            node.ParentNode = this;
            node.NodeValueChangedAction = node.ParentNode.NodeValueChangedAction;
            node.NodeValueChanged += node.NodeValueChangedAction;
            
            _nodes.Add(node);
            return node;
        }

        public void Remove()
        {
            while (Nodes?.FirstOrDefault() is PcmTreeNode<T> node)
            {
                node.Remove();
            }

            NodeValueChanged -= NodeValueChangedAction;
            NodeValueChangedAction = null;

            RemoveAction?.Invoke(this);
            ParentNode?._nodes.Remove(this);
            ParentNode = null;

            if (_value is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
            }

            _value = default;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PcmTreeNode<T>> GetEnumerator()
        {
            // Traverse through tree.
            return YieldNode(this);

            IEnumerator<PcmTreeNode<T>> YieldNode(PcmTreeNode<T> node)
            {
                foreach (var nodeItem in node.Nodes!)
                {
                    if (nodeItem.Nodes is not null)
                    {
                        YieldNode(nodeItem);
                    }

                    yield return nodeItem;
                }
            }
        }
    }
}
