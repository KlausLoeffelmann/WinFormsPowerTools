using System;

namespace WinFormsPowerTools.AutoLayout
{
    public class ComponentType
    {
        public ComponentType(string name, string destinationTypeName)
        {
            Name = name;
            DestinationTypeName = destinationTypeName;
        }

        public string Name { get; set; }
        public string DestinationTypeName { get; set; }
        public Func<object>? Factory { get; set; } // Produces the (WinForms) equivilent out of the structure.
    }
}
