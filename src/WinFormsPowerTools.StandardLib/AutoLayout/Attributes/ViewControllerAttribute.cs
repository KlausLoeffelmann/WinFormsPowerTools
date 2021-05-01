using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class ViewControllerAttribute : Attribute
    {
        public ViewControllerAttribute(string displayPropertySuffix = "DisplayName", Type modelType = default, params string[] excludeProperties)
        {
            DisplayPropertySuffix = displayPropertySuffix;
            ModelType = modelType;
            ExcludeProperties = excludeProperties;
        }

        public string DisplayPropertySuffix { get; }
        public Type ModelType { get; }
        public string[] ExcludeProperties { get; }
    }
}
