namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    [Serializable]
    public class BindingConverters : List<BindingConverterSetting>
    {
    }

    [Serializable]
    public class BindingConverterSettingsCollection : Dictionary<string, BindingConverters>
    {
        public void Add(IBindableComponent targetComponent, string propertyName, Type converterType)
        {
            if (!this.TryGetValue(targetComponent.GetName(), out var bindingConverters))
            {
                bindingConverters = new BindingConverters();
                this.Add(targetComponent.GetName(), bindingConverters);

                return;
            }

            var bindingConverterSetting = new BindingConverterSetting(
                targetComponent: targetComponent,
                propertyName: propertyName,
                typeConverterType: converterType);

            bindingConverters=new BindingConverters();
            bindingConverters.Add(bindingConverterSetting);

            Add(targetComponent.GetName(), bindingConverters);
        }

        public BindingConverterSettingsCollection GetUsedItems()
        {
            var usedItems = new BindingConverterSettingsCollection();

            foreach (var item in this)
            {
                if (item.Value.Count == 0)
                {
                    continue;
                }

                usedItems.Add(item.Key, item.Value);
            }

            return usedItems;
        }
    }
}
