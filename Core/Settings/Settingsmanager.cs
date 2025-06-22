using System;
using System.IO;
using System.Text.Json;
using KeybMacro.Core.Settings;

namespace KeybMacro.Core.Settings
{
    public static class SettingsManager
    {
        private static readonly string WindowsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeybMacro", "config", "settings.json");
        private static readonly string LinuxPath = Path.Combine("/usr/share/keybmacro/config/settings.json");
        private static readonly string MacOSPath = Path.Combine("/Library/Application Support/KeybMacro/config/settings.json");

        public static UserProfile CurrentProfile { get; private set; } = new();

        public static void Load()
        {
            string path = GetConfigPath();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                CurrentProfile = JsonSerializer.Deserialize<UserProfile>(json) ?? new();
            }
        }

        public static void Save()
        {
            string path = GetConfigPath();
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(CurrentProfile, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        private static string GetConfigPath()
        {
            if (OperatingSystem.IsWindows()) return WindowsPath;
            if (OperatingSystem.IsLinux()) return LinuxPath;
            if (OperatingSystem.IsMacOS()) return MacOSPath;
            return Path.Combine(".", "settings.json");
        }
    }
}