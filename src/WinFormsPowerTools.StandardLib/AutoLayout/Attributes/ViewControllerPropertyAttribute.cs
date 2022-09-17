using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ViewControllerPropertyAttribute : Attribute
    {
        public ViewControllerPropertyAttribute(
            string? propertyName = default, 
            string? displayName = default, 
            Type? converter = default, 
            Scope getAccessorScope = Scope.@public, 
            Scope setAccessorScope = Scope.@public)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            Converter = converter;
            GetAccessorScope = getAccessorScope;
            SetAccessorScope = setAccessorScope;
        }

        public string? PropertyName { get; }
        public string? DisplayName { get; }
        public Type? Converter { get; }

        // TODO: Implement scope handling.
        public Scope GetAccessorScope { get; }
        public Scope SetAccessorScope { get; }
    }
}
