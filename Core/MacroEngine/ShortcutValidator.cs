using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;

namespace KeybMacro.Core.MacroEngine
{
    public static class ShortcutValidator
    {
        private const string ShortcutDatabasePath = "Assets/Settings/ReservedShortcuts.json";

        private static Dictionary<string, string> ReservedShortcuts = new();
        private static Dictionary<string, string> AssignedShortcuts = new();

        public static void LoadReservedShortcuts()
        {
            if (File.Exists(ShortcutDatabasePath))
            {
                var json = File.ReadAllText(ShortcutDatabasePath);
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (dict != null)
                    ReservedShortcuts = dict;
            }
            else
            {
                GenerateShortcutSnapshot();
            }
        }

        public static void GenerateShortcutSnapshot()
        {
            ReservedShortcuts.Clear();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                AddWindowsShortcuts();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                AddLinuxShortcuts();
            }

            File.WriteAllText(ShortcutDatabasePath, JsonSerializer.Serialize(ReservedShortcuts, new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void AddShortcut(string shortcut, string description)
        {
            if (!ReservedShortcuts.ContainsKey(shortcut))
                ReservedShortcuts.Add(shortcut, description);
        }

        private static void AddWindowsShortcuts()
        {
            AddShortcut("Alt+F4", "Fermer la fenêtre active");
            AddShortcut("Ctrl+Alt+Del", "Ouvrir l'écran de sécurité Windows");
            AddShortcut("Ctrl+Shift+Esc", "Ouvrir le gestionnaire des tâches");
            AddShortcut("Win+Tab", "Afficher la vue des tâches");
            AddShortcut("Win+D", "Afficher le bureau");
            AddShortcut("Win+E", "Ouvrir l'explorateur de fichiers");
            AddShortcut("Win+R", "Ouvrir la boîte de dialogue Exécuter");
            AddShortcut("Win+L", "Verrouiller la session");
            AddShortcut("Win+Pause", "Ouvrir les propriétés système");
            AddShortcut("Alt+Tab", "Changer d'application en cours");
        }

        private static void AddLinuxShortcuts()
        {
            AddShortcut("Ctrl+Alt+T", "Ouvrir un terminal");
            AddShortcut("Ctrl+Alt+L", "Verrouiller la session");
            AddShortcut("Alt+F2", "Ouvrir une commande rapide");
            AddShortcut("Ctrl+Alt+Del", "Écran de fermeture de session");
            AddShortcut("Ctrl+Alt+Esc", "Tuer un processus avec xkill");
            AddShortcut("Ctrl+Q", "Quitter l'application (GNOME/GTK)");
            AddShortcut("Ctrl+W", "Fermer l'onglet actif");
            AddShortcut("Alt+Tab", "Changer de fenêtre");
            AddShortcut("Ctrl+Alt+Flèche", "Changer de bureau virtuel");
            AddShortcut("PrtSc", "Capturer l'écran");
        }

        public static void RegisterExistingMacros(Dictionary<string, string> macroShortcuts)
        {
            AssignedShortcuts = macroShortcuts;
        }

        public static (bool isValid, string conflictReason) Validate(string shortcut)
        {
            if (ReservedShortcuts.TryGetValue(shortcut, out var systemUse))
                return (false, $"Conflit avec le système : {systemUse}");

            if (AssignedShortcuts.TryGetValue(shortcut, out var macroName))
                return (false, $"Conflit avec macro : {macroName}");

            return (true, "");
        }
    }
}