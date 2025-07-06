using System;

namespace Warp.AutoLayout
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PropertyMappingAttribute : Attribute
    {
        public PropertyMappingAttribute(
            AutoLayoutTarget targetHint = AutoLayoutTarget.Implicit, 
            string? displayName = null, 
            string? propertyName = default, 
            string? mapsToModelProperty = null,
            Scope getAccessorScope = Scope.@public,
            Scope setAccessorScope = Scope.@public)
        {
            TargetHint = targetHint;
            DisplayName = displayName;
            PropertyName = propertyName;
            MapsToModelProperty = mapsToModelProperty;
            GetAccessorScope = getAccessorScope;
            SetAccessorScope = setAccessorScope;
        }

        public AutoLayoutTarget TargetHint { get; set; }
        public string? DisplayName { get; set; }
        public string? PropertyName { get; set; }
        public string? MapsToModelProperty { get; set; }

        // TODO: Implement scope handling.
        public Scope GetAccessorScope { get; set; }
        public Scope SetAccessorScope { get; set; }
    }
}
