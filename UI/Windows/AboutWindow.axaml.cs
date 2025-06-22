using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;

namespace KeybMacro.UI.Windows
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void GitHubLink_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/vava62600/keybmacro",
                    UseShellExecute = true
                });
            }
            catch
            {
                // ignore or log error
            }
        }

        private void Close_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close();
        }
    }
}
