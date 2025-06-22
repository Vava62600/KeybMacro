using System.Collections.Generic;

namespace KeybMacro.Core.Settings
{
    public class UserProfile
    {
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "Default";
        public string Palette { get; set; } = "Blue";
        public Dictionary<string, string> CustomBindings { get; set; } = new();
    }
}
