using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEntryForms.AutoLayout
{
    public class AutoLayoutGroup : AutoLayoutComponent
    {
        public AutoLayoutGroup(string name) : base(name)
        {
        }

        private List<AutoLayoutComponent> _components;
        private AutoLayoutComponents _chainComponents;

        public AutoLayoutComponents ChainComponents()
        {
            if (_chainComponents is null)
            {
                _chainComponents = new AutoLayoutComponents()
                {
                    Components = Components,
                    LastComponent = null
                };
            }
            return _chainComponents;
        }

        public Padding Padding { get; set; }

        public List<AutoLayoutComponent> Components
        {
            get
            {
                if (_components is null)
                {
                    _components = new List<AutoLayoutComponent>();
                }

                return _components;
            }
        }
    }
}
