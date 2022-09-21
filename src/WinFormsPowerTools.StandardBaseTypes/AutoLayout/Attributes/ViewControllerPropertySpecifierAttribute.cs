using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class ViewControllerPropertySpecifierAttribute : Attribute
    {
        public ViewControllerPropertySpecifierAttribute(
            string propertyName,
            string? displayName = default)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
        }
        public string? PropertyName { get; }
        public string? DisplayName { get; }
    }
}
