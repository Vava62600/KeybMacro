using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace KeybMacro.UI.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public ObservableCollection<MacroModel> Macros { get; } = new();

        private MacroModel? selectedMacro;
        public MacroModel? SelectedMacro
        {
            get => selectedMacro;
            set => this.RaiseAndSetIfChanged(ref selectedMacro, value);
        }

        public ReactiveCommand<Unit, Unit> AddMacroCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveMacroCommand { get; }
        public ReactiveCommand<Unit, Unit> RunMacroCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenSettingsCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }

        public ReactiveCommand<Unit, Unit> NewMacroCommand { get; }
        public ReactiveCommand<Unit, Unit> EditMacroCommand { get; }
        public ReactiveCommand<Unit, Unit> ExportSettingsCommand { get; }
        public ReactiveCommand<Unit, Unit> AboutCommand { get; }

        public ReactiveCommand<Unit, Unit> QuitCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenGithubHelpCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenDocsCommand { get; }
        public ReactiveCommand<Unit, Unit> ReportBugCommand { get; }

        public MainViewModel()
        {
            AddMacroCommand = ReactiveCommand.Create(() =>
            {
                Macros.Add(new MacroModel { Name = $"Macro {Macros.Count + 1}" });
            });

            RemoveMacroCommand = ReactiveCommand.Create(() =>
            {
                if (SelectedMacro != null)
                    Macros.Remove(SelectedMacro);
            }, this.WhenAnyValue(x => x.SelectedMacro, m => m != null));

            RunMacroCommand = ReactiveCommand.Create(() =>
            {
                // TODO: lancer la macro sélectionnée
            }, this.WhenAnyValue(x => x.SelectedMacro, m => m != null));

            OpenSettingsCommand = ReactiveCommand.Create(() =>
            {
                var settingsWindow = new SettingsWindow();
                settingsWindow.Show();
            });

            ExitCommand = ReactiveCommand.Create(() =>
            {
                Avalonia.Application.Current?.Shutdown();
            });

            NewMacroCommand = ReactiveCommand.Create(() =>
            {
                // TODO: création nouvelle macro
            });

            EditMacroCommand = ReactiveCommand.Create(() =>
            {
                // TODO: édition macro sélectionnée
            }, this.WhenAnyValue(x => x.SelectedMacro, m => m != null));

            ExportSettingsCommand = ReactiveCommand.Create(() =>
            {
                // TODO: export des paramètres
            });

            AboutCommand = ReactiveCommand.Create(() =>
            {
                var aboutWin = new AboutWindow();
                aboutWin.Show();
            });

            QuitCommand = ReactiveCommand.Create(() =>
            {
                Avalonia.Application.Current?.Shutdown();
            });

            OpenGithubHelpCommand = ReactiveCommand.Create(() =>
            {
                OpenUrl("https://github.com/vava62600/KeybMacro");
            });

            OpenDocsCommand = ReactiveCommand.Create(() =>
            {
                OpenUrl("https://github.com/vava62600/KeybMacro/wiki");
            });

            ReportBugCommand = ReactiveCommand.Create(() =>
            {
                OpenUrl("https://github.com/vava62600/KeybMacro/issues");
            });
        }

        private void OpenUrl(string url)
        {
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch
            {
                // ignore or log
            }
        }
    }
}
