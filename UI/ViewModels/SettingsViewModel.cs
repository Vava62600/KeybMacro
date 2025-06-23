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
        private int _languageIndex;
        public int LanguageIndex
        {
            get => _languageIndex;
            set => this.RaiseAndSetIfChanged(ref _languageIndex, value);
        }

        private int _themeIndex;
        public int ThemeIndex
        {
            get => _themeIndex;
            set => this.RaiseAndSetIfChanged(ref _themeIndex, value);
        }

        private int _paletteIndex;
        public int PaletteIndex
        {
            get => _paletteIndex;
            set => this.RaiseAndSetIfChanged(ref _paletteIndex, value);
        }

        private bool _startupAtBoot;
        public bool StartupAtBoot
        {
            get => _startupAtBoot;
            set
            {
                this.RaiseAndSetIfChanged(ref _startupAtBoot, value);
                Services.StartupHelper.SetStartup(value);
                UserProfile.StartupAtBoot = value;
            }
        }

        private bool _autoUpdate;
        public bool AutoUpdate
        {
            get => _autoUpdate;
            set
            {
                this.RaiseAndSetIfChanged(ref _autoUpdate, value);
                UserProfile.AutoUpdate = value;
            }
        }

        private bool _developerMode;
        public bool DeveloperMode
        {
            get => _developerMode;
            set
            {
                this.RaiseAndSetIfChanged(ref _developerMode, value);
                UserProfile.DeveloperMode = value;
            }
        }

        private bool _debugMode;
        public bool DebugMode
        {
            get => _debugMode;
            set
            {
                this.RaiseAndSetIfChanged(ref _debugMode, value);
                UserProfile.DebugMode = value;
            }
        }

        private bool _detailedLogs;
        public bool DetailedLogs
        {
            get => _detailedLogs;
            set
            {
                this.RaiseAndSetIfChanged(ref _detailedLogs, value);
                UserProfile.DetailedLogs = value;
            }
        }

        private UserProfile UserProfile => SettingsManager.CurrentUserProfile;

        public ReactiveCommand<Unit, Unit> ApplyCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public SettingsViewModel()
        {
            LoadFromProfile();

            ApplyCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await SettingsManager.SaveAsync();
            });

            CancelCommand = ReactiveCommand.Create(() =>
            {
                LoadFromProfile();
            });
        }

        private void LoadFromProfile()
        {
            LanguageIndex = UserProfile.LanguageIndex;
            ThemeIndex = UserProfile.ThemeIndex;
            PaletteIndex = UserProfile.PaletteIndex;
            StartupAtBoot = UserProfile.StartupAtBoot;
            AutoUpdate = UserProfile.AutoUpdate;
            DeveloperMode = UserProfile.DeveloperMode;
            DebugMode = UserProfile.DebugMode;
            DetailedLogs = UserProfile.DetailedLogs;
        }
    }
}
