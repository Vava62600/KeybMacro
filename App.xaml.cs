using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KeybMacro.Platform.Tray;
using KeybMacro.UI.Windows;
using KeybMacro.Core.Settings;

namespace KeybMacro
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            SettingsManager.Load();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;

                TrayService.Init(mainWindow);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
