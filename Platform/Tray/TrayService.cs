using System;
using System.Diagnostics;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Platform;
using KeybMacro.Core.Settings;
using TrayApp;

namespace KeybMacro.Platform.Tray
{
    public static class TrayService
    {
        private static Window? _mainWindow;

        public static void Init(Window mainWindow)
        {
            _mainWindow = mainWindow;

            var iconPath = SettingsManager.IsDarkTheme()
                ? "avares://KeybMacro/Assets/Icons/icon-dark-64.png"
                : "avares://KeybMacro/Assets/Icons/icon-light-64.png";

            TrayIcon icon = new TrayIcon
            {
                IconPath = iconPath,
                Title = "KeybMacro"
            };

            icon.OnOpenRequested += () => _mainWindow?.Show();
            icon.OnQuitRequested += () => Environment.Exit(0);
            icon.OnHelpRequested += () => Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/vava62600/keybmacro",
                UseShellExecute = true
            });

            icon.Initialize();
        }
    }
}
