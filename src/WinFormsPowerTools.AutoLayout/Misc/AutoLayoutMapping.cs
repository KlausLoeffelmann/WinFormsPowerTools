using System;
using System.Collections.Generic;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutTypeMapping
    {
        private static Dictionary<Type, Type>? myMapping;
        
        public static Dictionary<Type, Type> GetDefaultMapping()
        {
            if (myMapping is null)
            {
                myMapping ??= new Dictionary<Type, Type>();

                myMapping.Add(typeof(string), typeof(AutoLayoutTextEntry<>));
                myMapping.Add(typeof(int), typeof(AutoLayoutIntegerEntry<>));
            }
            return myMapping;
        }
    }
}
