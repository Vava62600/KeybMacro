using System;

namespace KeybMacro.Core.Settings
{
    public class UserProfile
    {
        public int LanguageIndex { get; set; } = 0; // 0=English, 1=French
        public int ThemeIndex { get; set; } = 0; // Default theme index
        public int PaletteIndex { get; set; } = 0; // Default palette index

        public bool StartupAtBoot { get; set; } = false;
        public bool AutoUpdate { get; set; } = true;
        public bool DeveloperMode { get; set; } = false;
        public bool DebugMode { get; set; } = false;
        public bool DetailedLogs { get; set; } = false;
    }
}
