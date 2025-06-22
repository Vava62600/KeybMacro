using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace KeybMacro.UI.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnAboutClick(object? sender, RoutedEventArgs e)
        {
            var about = new AboutWindow();
            about.ShowDialog(this);
        }

        private void OnQuitClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new Window
            {
                Title = "Quitter KeybMacro",
                Width = 300,
                Height = 150,
                Content = new StackPanel
                {
                    Margin = new Thickness(10),
                    Spacing = 10,
                    Children =
                    {
                        new TextBlock { Text = "Souhaitez-vous quitter complètement ou laisser en tâche de fond ?" },
                        new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                            Spacing = 10,
                            Children =
                            {
                                new Button { Content = "Quitter complètement", Command = ReactiveCommand.Create(() => { Tray.TrayService.QuitAll(); Close(); }) },
                                new Button { Content = "Tâche de fond", Command = ReactiveCommand.Create(() => Close()) }
                            }
                        }
                    }
                }
            };
            dialog.ShowDialog(this);
        }

        private void OnGitHubHelpClick(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/vava62600/KeybMacro") { UseShellExecute = true });
        }

        private void OnDocClick(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/vava62600/KeybMacro/wiki") { UseShellExecute = true });
        }

        private void OnBugClick(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/vava62600/KeybMacro/issues") { UseShellExecute = true });
        }
    }
}
