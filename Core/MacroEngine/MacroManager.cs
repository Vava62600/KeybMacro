using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace KeybMacro.Core.MacroEngine
{
    public class MacroManager
    {
        private static readonly string MacroPath = "~/.keybmacro/macros.json";
        private static Dictionary<string, List<MacroAction>> Macros = new();

        public static void Load()
        {
            var path = Environment.ExpandEnvironmentVariables(MacroPath);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Macros = JsonSerializer.Deserialize<Dictionary<string, List<MacroAction>>>(json) ?? new();
            }
        }

        public static void Save()
        {
            var path = Environment.ExpandEnvironmentVariables(MacroPath);
            string json = JsonSerializer.Serialize(Macros);
            File.WriteAllText(path, json);
        }

        public static void AddMacro(string name, List<MacroAction> actions)
        {
            Macros[name] = actions;
            Save();
        }

        public static Dictionary<string, List<MacroAction>> GetAllMacros() => Macros;
    }

    public class MacroAction
    {
        public string Type { get; set; } = string.Empty; // e.g., "key_press", "mouse_click"
        public string Key { get; set; } = string.Empty;
        public int Delay { get; set; } = 0;
    }
}
