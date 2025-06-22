using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Threading;
using KeybMacro.Core.Settings;
using ReactiveUI;
using System.Reactive;

namespace KeybMacro.UI.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private Window _window;

        public ObservableCollection<string> Themes { get; }
        public ObservableCollection<string> Palettes { get; }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }

        private string _selectedPalette;
        public string SelectedPalette
        {
            get => _selectedPalette;
            set => this.RaiseAndSetIfChanged(ref _selectedPalette, value);
        }

        public ICommand ApplyCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public SettingsViewModel(Window window = null)
        {
            _window = window;

            Themes = new ObservableCollection<string>(ThemeManager.Themes.Keys);
            Palettes = new ObservableCollection<string>(ThemeManager.Palettes.Keys);

            // Charger les valeurs actuelles
            SelectedTheme = SettingsManager.CurrentProfile.Theme;
            SelectedPalette = SettingsManager.CurrentProfile.Palette;

            ApplyCommand = ReactiveCommand.Create(() =>
            {
                ThemeManager.ApplyTheme(SelectedTheme, SelectedPalette);
            });

            SaveCommand = ReactiveCommand.Create(() =>
            {
                SettingsManager.CurrentProfile.Theme = SelectedTheme;
                SettingsManager.CurrentProfile.Palette = SelectedPalette;
                SettingsManager.Save();
            });

            CloseCommand = ReactiveCommand.Create(() =>
            {
                _window?.Close();
            });
        }
    }
}
