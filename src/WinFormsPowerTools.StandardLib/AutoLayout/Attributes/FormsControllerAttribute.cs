using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class FormsControllerAttribute : Attribute
    {
        public FormsControllerAttribute()
        {
        }

        public FormsControllerAttribute(Type modelType)
        {
            ModelType = modelType;
        }

        public FormsControllerAttribute(Type modelType, params string[] excludeProperties)
        {
            ModelType = modelType;
            ExcludeProperties = excludeProperties;
        }

        public Type ModelType { get; }
        public string[] ExcludeProperties { get; }
    }
}
