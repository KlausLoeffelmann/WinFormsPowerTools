using System;

namespace WinFormsPowerTools.AutoLayout
{
    //public enum FormsControllerPropertyOptions
    //{
    //    SupressDisplayProperty
    //}

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property  , AllowMultiple = false, Inherited = true)]
    public class ViewControllerDisplayAttribute : Attribute
    {
        public ViewControllerDisplayAttribute(string displayName, string mapsModelProperty)
        {
            DisplayName = displayName;
            MapsModelProperty = mapsModelProperty;
        }

        public string DisplayName { get; }
        public string MapsModelProperty { get; }
    }
}
