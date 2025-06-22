namespace KeybMacro.Core.Backend
{
    public static class CoreService
    {
        public static void Initialize()
        {
            LogManager.Initialize();
            SettingsManager.Load();
            PluginManager.LoadAll();
            ThemeManager.LoadCurrentTheme();
            UpdateManager.CheckForUpdatesAsync().Wait();
        }

        public static void Shutdown()
        {
            LogManager.Flush();
        }
    }
}