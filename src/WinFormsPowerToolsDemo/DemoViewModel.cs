using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinForms.PowerTools.Components;
using WinForms.PowerTools.Controls;

namespace WinForms.PowerToolsDemo
{
    public partial class DemoViewModel : ObservableObject
    {
        private ThemingMode _themingMode = ThemingMode.LightMode;

        [ObservableProperty]
        private SegoeFluentIcons _openSymbol=SegoeFluentIcons.OpenFile;

        [ObservableProperty]
        private SegoeFluentIcons _closeSymbol = SegoeFluentIcons.View;

        [ObservableProperty]
        private SegoeFluentIcons _editSymbol = SegoeFluentIcons.Edit;

        [ObservableProperty]
        private SegoeFluentIcons _optionsSymbol = SegoeFluentIcons.Settings;

        public ThemingMode ThemingMode
        {
            get => _themingMode;
            set => SetProperty(ref _themingMode, value);
        }

        [RelayCommand]
        public void SwitchToDarkMode() => ThemingMode = ThemingMode.DarkMode;

        [RelayCommand]
        public void SwitchToLightMode() => ThemingMode = ThemingMode.LightMode;

        [RelayCommand]
        public void SwitchToSystemMode() => ThemingMode = ThemingMode.System;
    }
}
