using System;

namespace WinFormsPowerTools.AutoLayout
{
    public class CommandMappingAttribute : Attribute
    {
        public CommandMappingAttribute(
            AutoLayoutTarget targetHint = AutoLayoutTarget.Implicit,
            string? displayName = null,
            string? propertyName = default,
            Scope getAccessorScope = Scope.@public,
            Scope setAccessorScope = Scope.@public)
        {
            TargetHint = targetHint;
            DisplayName = displayName;
            PropertyName = propertyName;
            GetAccessorScope = getAccessorScope;
            SetAccessorScope = setAccessorScope;
        }

        public AutoLayoutTarget TargetHint { get; set; }
        public string? DisplayName { get; set; }
        public string? PropertyName { get; set; }

        // TODO: Implement scope handling.
        public Scope GetAccessorScope { get; set; }
        public Scope SetAccessorScope { get; set; }
    }
}
