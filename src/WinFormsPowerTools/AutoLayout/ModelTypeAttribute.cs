using System;

namespace DataEntryForms.AutoLayout
{
    public class ModelTypeAttribute : Attribute
    {
        public ModelTypeAttribute(Type modeltype)
        {
            ModelType = ModelType;
        }

        public Type ModelType { get; }
    }
}
