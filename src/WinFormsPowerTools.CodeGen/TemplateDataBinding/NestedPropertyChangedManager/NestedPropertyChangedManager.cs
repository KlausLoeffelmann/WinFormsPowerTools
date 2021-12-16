using System;
using System.ComponentModel;

namespace System.Windows.Forms.TemplateBinding
{
    public class NestedPropertyChangedManager : Chain
    {
        Action<ChainLink>? _removeAction = (node) =>
         {

         };

        //Action<PcmTreeNode>? _valueChangedAction = (node, oldValue) =>
        //   {
        //   };

        public NestedPropertyChangedManager(object? dataContext, 
            EventHandler<ChainValueChangedEventArgs> nodeValueChangedAction) 
            : base(dataContext)
        {
            //RootNode.RemoveAction = _removeAction;
            //RootNode.ValueChangedAction = _valueChangedAction;
        }
    }
}
