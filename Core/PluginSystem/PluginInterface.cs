using Avalonia.Controls;

namespace KeybMacro.Core.PluginSystem
{
    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        bool HasGui { get; }
        bool HasCli { get; }
        void OnLoad();
        void OnUnload();

        UserControl? GetConfigurationUI(); // pour l'UI Avalonia si nécessaire
        void ExecuteCommandLine(string[] args); // pour la CLI si activée
    }
}