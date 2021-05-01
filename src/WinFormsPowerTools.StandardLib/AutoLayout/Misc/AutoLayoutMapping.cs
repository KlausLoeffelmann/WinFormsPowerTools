using System;
using System.Collections.Generic;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.StandardLib.AutoLayout.Misc
{
    public static class AutoLayoutMapping
    {
        private static Dictionary<Type, ComponentType> myMapping;

        static AutoLayoutMapping()
        {
            myMapping = new Dictionary<Type, ComponentType>();
            myMapping.Add(typeof(AutoLayoutLabel<>), new ComponentType(nameof(AutoLayoutLabel<ViewControllerBase>), "System.Windows.Forms.Label"));
            myMapping.Add(typeof(AutoLayoutTextBox<>), new ComponentType(nameof(AutoLayoutTextBox<ViewControllerBase>), "System.Windows.Forms.TextBox"));
            myMapping.Add(typeof(AutoLayoutButton<>), new ComponentType(nameof(AutoLayoutTextBox<ViewControllerBase>), "System.Windows.Forms.Button"));
            myMapping.Add(typeof(AutoLayoutGrid<>), new ComponentType(nameof(AutoLayoutTextBox<ViewControllerBase>), "System.Windows.Forms.TableLayoutControl"));
        }

        static IDictionary<Type, ComponentType> Mapping => myMapping;

    }
}
