using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.TemplateBinding;

namespace WinFormsPowerTools.CodeGen.TemplateDataBinding.NestedPropertyChangedManager
{
    public static class TreeNodeExtension
    {
        //public static void ForEachNodeAndSubNode(this IEnumerable<ChainLink> nodes, Action<ChainLink> nodeTraverseAction)
        //{
        //    foreach (ChainLink node in nodes)
        //    {
        //        if (node.Nodes is { } subNodes && subNodes.Count > 0)
        //        {
        //            subNodes.ForEachNodeAndSubNode(nodeTraverseAction);
        //        }
        //        nodeTraverseAction(node);
        //    }
        //}

        //public static void DeleteSubNodes(this ChainLink node, Action<ChainLink> nodeDeleteAction)
        //{
        //    while (node.Nodes is { } subNodes && subNodes.Count > 0)
        //    {
        //        var currentNode = node.Nodes.First();

        //        currentNode.DeleteSubNodes(nodeDeleteAction);

        //        nodeDeleteAction(currentNode);

        //        node.Nodes.Remove(currentNode);
        //    }
        //}
    }
}
