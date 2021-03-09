﻿using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutContainer<T> where T : IFormsController
    {
        public AutoLayoutContainer(string name)
        {
            Name = name;
        }

        private List<AutoLayoutComponent<T>> _components;
        private AutoLayoutComponents<T> _chainComponents;

        public AutoLayoutComponents<T> BuildComponents()
        {
            if (_chainComponents is null)
            {
                _chainComponents = new AutoLayoutComponents<T>()
                {
                    Components = Components,
                    LastComponent = null
                };
            }
            return _chainComponents;
        }

        public string Name { get; }
        public AutoLayoutPadding Padding { get; set; }

        public List<AutoLayoutComponent<T>> Components
        {
            get
            {
                if (_components is null)
                {
                    _components = new List<AutoLayoutComponent<T>>();
                }

                return _components;
            }
        }
    }
}