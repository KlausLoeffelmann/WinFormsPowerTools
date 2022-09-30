using System;
using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutMenuExtensions
    {
        public static AutoLayoutMenu<T> AddMenuItem<T>(
            this AutoLayoutMenu<T> menu,
            string? name = "menuEntry1",
            string? text = default,
            ICommand? command = default,
            bool isEnabled = true,
            bool isChecked = false,
            params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            var menuItem = new AutoLayoutMenuItem<T>(name, text, command, isEnabled, isChecked, bindings)
            {
                IsEnabled = isEnabled
            };

            menu.Add(menuItem);
            return menu;
        }

        public static AutoLayoutMenu<T> AddMenuItem<T>(
            this AutoLayoutMenu<T> menu,
            AutoLayoutMenuItem<T> menuItem,
            params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            menu.Add(menuItem);
            return menu;
        }

        public static AutoLayoutMenuItem<T> AddMenuItem<T>(
        this AutoLayoutMenuItem<T> menuItem,
        string? name = "menuEntry1",
        string? text = default,
        ICommand? command = default,
        bool isEnabled = true,
        bool isChecked = false,
        params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            var subMenuItem = new AutoLayoutMenuItem<T>(name, text, command, isEnabled, isChecked, bindings)
            {
                IsEnabled = isEnabled
            };

            menuItem.Add(subMenuItem);
            return menuItem;
        }
    }
}
