using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutBindings
    {
        [AllowNull]
        private Dictionary<string, AutoLayoutBinding> _bindings;

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

        public bool TryGetBinding(string componentPropertyName, out AutoLayoutBinding? binding)
        {
            if (_bindings is null)
            {
                binding = null;
                return false;
            }

            return _bindings.TryGetValue(componentPropertyName, out binding);
        }
    }
}
