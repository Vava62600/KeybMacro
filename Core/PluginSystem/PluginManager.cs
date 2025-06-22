using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KeybMacro.Core.PluginSystem
{
    public static class PluginManager
    {
        private static readonly string PluginDirectory = "Plugins/";
        private static readonly List<IPlugin> Plugins = new();

        public static void LoadAll()
        {
            if (!Directory.Exists(PluginDirectory))
                Directory.CreateDirectory(PluginDirectory);

            foreach (var file in Directory.GetFiles(PluginDirectory, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(file);
                    foreach (var type in asm.GetTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            if (Activator.CreateInstance(type) is IPlugin plugin)
                                Plugins.Add(plugin);
                        }
                    }
                }
                catch { }
            }
        }

        public static IEnumerable<IPlugin> GetPlugins() => Plugins;
    }

    public interface IPlugin
    {
        string Name { get; }
        void OnLoad();
        void OnUnload();
    }
}