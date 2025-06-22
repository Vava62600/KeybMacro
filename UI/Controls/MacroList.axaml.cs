using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace KeybMacro.UI.Controls
{
    public partial class MacroList : UserControl
    {
        public MacroList()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnMacroClick(object? sender, RoutedEventArgs e)
        {
            // TODO: Lancer macro spécifique
        }
    }
}