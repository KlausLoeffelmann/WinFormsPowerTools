using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutBindings
    {
        private Dictionary<string, AutoLayoutBinding>? _bindings;

        public IReadOnlyDictionary<string, AutoLayoutBinding> Bindings
            => _bindings ??= new();

        public void AddBinding(string componentPropertyName, string bindingPath)
        {
            _bindings ??= new();
            _bindings.Add(componentPropertyName, new AutoLayoutBinding(componentPropertyName, bindingPath));
        }

        public void AddBinding(AutoLayoutBinding binding)
        {
            _bindings ??= new();
            _bindings.Add(binding.ComponentPropertyName, binding);
        }
    }
}
