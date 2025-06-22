using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace KeybMacro.UI.Controls
{
    public partial class ActionEditor : UserControl
    {
        public ActionEditor()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnAddAction(object? sender, RoutedEventArgs e)
        {
            var combo = this.FindControl<ComboBox>("ActionSelector");
            if (combo?.SelectedItem is ComboBoxItem selected)
            {
                Debug.WriteLine($"Action sélectionnée : {selected.Content}");
                // TODO : Ajout dans macro via ViewModel ou PluginManager
            }
        }
    }
}