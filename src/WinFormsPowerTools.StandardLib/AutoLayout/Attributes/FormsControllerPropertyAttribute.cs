using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FormsControllerPropertyAttribute : Attribute
    {
        public FormsControllerPropertyAttribute(
            string propertyName = default, 
            string displayName = default, 
            Type converter = default, 
            Scope getAccessorScope = Scope.@public, 
            Scope setAccessorScope = Scope.@public)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            Converter = converter;
            GetAccessorScope = getAccessorScope;
            SetAccessorScope = setAccessorScope;
        }

        public string PropertyName { get; }
        public string DisplayName { get; }
        public Type Converter { get; }

        // TODO: Implement scope handling.
        public Scope GetAccessorScope { get; }
        public Scope SetAccessorScope { get; }
    }

    public enum Scope
    {
        @public,
        @private,
        @internal,
        @protected
    }

    //public enum FormsControllerPropertyOptions
    //{
    //    SupressDisplayProperty
    //}

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property  , AllowMultiple = false, Inherited = true)]
    public class FormsControllerDisplayAttribute : Attribute
    {
        public FormsControllerDisplayAttribute(string displayName, string mapsModelProperty)
        {
            DisplayName = displayName;
            MapsModelProperty = mapsModelProperty;
        }

        public string DisplayName { get; }
        public string MapsModelProperty { get; }
    }
}
