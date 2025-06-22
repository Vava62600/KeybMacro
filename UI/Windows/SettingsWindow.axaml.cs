using Avalonia.Controls;
using Avalonia.Interactivity;
using KeybMacro.Core.Settings;
using KeybMacro.UI.ViewModels;

namespace KeybMacro.UI.Windows
{
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel ViewModel => DataContext as SettingsViewModel ?? new SettingsViewModel();

        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(this);
        }
    }
}
