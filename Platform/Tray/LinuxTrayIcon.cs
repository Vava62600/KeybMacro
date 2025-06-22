#if LINUX
using System;

namespace TrayApp
{
    public class LinuxTrayIcon : ITrayIconImpl
    {
        public LinuxTrayIcon(string iconPath, string title)
        {
            // TODO: Ajouter implémentation AppIndicator / Ayatana
            Console.WriteLine($"Linux tray icon placeholder for {title} with icon {iconPath}");
        }

        public void SetMenu((string Label, Action? Callback)[] items)
        {
            // TODO: Implémentation réelle selon desktop env
        }
    }
}
#endif