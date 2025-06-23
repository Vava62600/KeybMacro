using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Diagnostics;
using System.Text.Json;

namespace KeybMacro.UI.Windows
{
    public partial class UpdateWindow : Window
    {
        private readonly string downloadUrl = "https://github.com/vava62600/KeybMacro/releases/latest/download/KeybMacro_" +
            (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
             (Environment.Is64BitOperatingSystem ? "win64.zip" : "win32.zip") :
             (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "linux-x64.zip" : "unknown"));

        public UpdateWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void OnInstallUpdateClick(object? sender, RoutedEventArgs e)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "KeybMacro_Update");
            Directory.CreateDirectory(tempPath);
            var zipPath = Path.Combine(tempPath, "update.zip");

            using HttpClient client = new HttpClient();
            using var response = await client.GetAsync(downloadUrl);
            if (response.IsSuccessStatusCode)
            {
                await using var fs = new FileStream(zipPath, FileMode.Create);
                await response.Content.CopyToAsync(fs);
            }
            else
            {
                await MessageBox.ShowAsync(this, "Erreur lors du téléchargement de la mise à jour.", "Erreur");
                return;
            }

            try
            {
                ZipFile.ExtractToDirectory(zipPath, tempPath, true);
                var updaterPath = Path.Combine(tempPath, "installer.exe");
                if (File.Exists(updaterPath))
                {
                    Process.Start(new ProcessStartInfo(updaterPath) { UseShellExecute = true });
                    Environment.Exit(0);
                }
                else
                {
                    await MessageBox.ShowAsync(this, "Fichier d’installation introuvable.", "Erreur");
                }
            }
            catch (Exception ex)
            {
                await MessageBox.ShowAsync(this, $"Erreur d’extraction : {ex.Message}", "Erreur");
            }
        }

        private void OnCancelClick(object? sender, RoutedEventArgs e) => Close();
    }
}