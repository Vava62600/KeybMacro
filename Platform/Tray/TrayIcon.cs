using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace TrayApp
{
    public class TrayIcon
    {
        public string IconPath { get; set; } = string.Empty;
        public string Title { get; set; } = "KeybMacro";

        public event Action? OnOpenRequested;
        public event Action? OnQuitRequested;
        public event Action? OnHelpRequested;

        private ITrayIconImpl? _impl;

        public void Initialize()
        {
            _impl = TrayFactory.Create(IconPath, Title);

            _impl?.SetMenu(new (string, Action?)[]
            {
                ("Ouvrir KeybMacro", () => OnOpenRequested?.Invoke()),
                ("Aide (GitHub)", () => OnHelpRequested?.Invoke()),
                ("Quitter", () => OnQuitRequested?.Invoke())
            });
        }
    }

    public interface ITrayIconImpl
    {
        void SetMenu((string Label, Action? Callback)[] items);
    }

    public static class TrayFactory
    {
        public static ITrayIconImpl? Create(string iconPath, string title)
        {
#if WINDOWS
            return new WinTrayIcon(iconPath, title);
#elif LINUX
            return new LinuxTrayIcon(iconPath, title);
#else
            return null;
#endif
        }
    }
}