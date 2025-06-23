using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace KeybMacro.UI.Windows
{
    public partial class SettingsWindow : Window
    {
        private string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeybMacro", "config", "UserSettings.json");

        public SettingsWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnHelpClick(object? sender, RoutedEventArgs e) => Process.Start(new ProcessStartInfo("https://github.com/vava62600/keybmacro/wiki") { UseShellExecute = true });

        private void OnAboutClick(object? sender, RoutedEventArgs e) => new AboutWindow().ShowDialog(this);

        private void OnGitHubClick(object? sender, RoutedEventArgs e) => Process.Start(new ProcessStartInfo("https://github.com/vava62600/keybmacro") { UseShellExecute = true });

        private void OnResetClick(object? sender, RoutedEventArgs e)
        {
            if (File.Exists(configPath)) File.Delete(configPath);
            Close();
        }

        private async void OnExportClick(object? sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog { DefaultExtension = ".json", Filters = { new FileDialogFilter { Name = "JSON", Extensions = { "json" } } } };
            var result = await sfd.ShowAsync(this);
            if (!string.IsNullOrEmpty(result)) File.Copy(configPath, result, true);
        }

        private async void OnImportClick(object? sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { AllowMultiple = false };
            ofd.Filters.Add(new FileDialogFilter { Name = "JSON", Extensions = { "json" } });
            var result = await ofd.ShowAsync(this);
            if (result?.Length > 0 && File.Exists(result[0])) File.Copy(result[0], configPath, true);
        }

        private void OnCancelClick(object? sender, RoutedEventArgs e) => Close();

        private void OnApplyClick(object? sender, RoutedEventArgs e)
        {
            var settings = new
            {
                Language = (LanguageCombo.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                Theme = (ThemeCombo.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                Palette = (PaletteCombo.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                Startup = StartupCheck.IsChecked ?? false,
                AutoUpdate = AutoUpdateCheck.IsChecked ?? false,
                DeveloperMode = DevModeCheck.IsChecked ?? false,
                DebugMode = DebugModeCheck.IsChecked ?? false,
                DetailedLogs = DetailedLogsCheck.IsChecked ?? false
            };
            Directory.CreateDirectory(Path.GetDirectoryName(configPath)!);
            File.WriteAllText(configPath, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));

            // Démarrage auto
            string startupPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "KeybMacro.lnk")
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "autostart", "KeybMacro.desktop");

            if (settings.Startup)
            {
                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                ShortcutCreator.CreateShortcut(startupPath, exePath);
            }
            else if (File.Exists(startupPath)) File.Delete(startupPath);

            // Debug Console
            if (settings.DebugMode)
            {
                ConsoleManager.Show();
                Console.WriteLine("[DEBUG] Debug Mode activé");
            }
            else ConsoleManager.Hide();

            Close();
        }
    }

    public static class ConsoleManager
    {
        [DllImport("kernel32.dll")] private static extern bool AllocConsole();
        [DllImport("kernel32.dll")] private static extern bool FreeConsole();
        [DllImport("kernel32.dll")] private static extern IntPtr GetConsoleWindow();
        public static void Show() { if (GetConsoleWindow() == IntPtr.Zero) AllocConsole(); }
        public static void Hide() { if (GetConsoleWindow() != IntPtr.Zero) FreeConsole(); }
    }

    public static class ShortcutCreator
    {
        public static void CreateShortcut(string shortcutPath, string targetPath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = targetPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
                shortcut.Save();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(shortcutPath)!);
                File.WriteAllText(shortcutPath,
                    $"[Desktop Entry]\nType=Application\nName=KeybMacro\nExec={targetPath}\nX-GNOME-Autostart-enabled=true\n");
            }
        }
    }
}
