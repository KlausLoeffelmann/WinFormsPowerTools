using System;

namespace WinFormsPowerTools.AutoLayout
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FormsControllerPropertyAttribute : Attribute
    {
        public FormsControllerPropertyAttribute()
        {
        }

        public FormsControllerPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }

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
}
