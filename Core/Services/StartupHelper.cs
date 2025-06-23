using System;
using System.IO;
using IWshRuntimeLibrary;

namespace KeybMacro.Core.Services
{
    public static class StartupHelper
    {
        private static readonly string StartupFolderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        private static readonly string ShortcutName = "KeybMacro.lnk";
        private static readonly string ShortcutPath = Path.Combine(StartupFolderPath, ShortcutName);

        private static readonly string AppExePath =
            System.Reflection.Assembly.GetExecutingAssembly().Location;

        public static bool IsStartupEnabled()
        {
            return File.Exists(ShortcutPath);
        }

        public static void SetStartup(bool enable)
        {
            if (enable)
                CreateShortcut();
            else
                RemoveShortcut();
        }

        private static void CreateShortcut()
        {
            if (File.Exists(ShortcutPath))
                return;

            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(ShortcutPath);
            shortcut.TargetPath = AppExePath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(AppExePath);
            shortcut.WindowStyle = 1;
            shortcut.Description = "Lancer KeybMacro au démarrage";
            shortcut.Save();
        }

        private static void RemoveShortcut()
        {
            if (File.Exists(ShortcutPath))
                File.Delete(ShortcutPath);
        }
    }
}
