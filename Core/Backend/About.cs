using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace KeybMacro.Core.Backend
{
    public static class About
    {
        public const string ProjectName = "KeybMacro";
        public const string GithubRepo = "https://github.com/vava62600/KeybMacro";
        public const string GithubVersionBadge = "https://img.shields.io/github/v/release/vava62600/KeybMacro?style=for-the-badge";

        private static readonly string LogoPath = "Assets/Icons/logo_128.png";

        public static readonly string[] TeamMembers =
        {
            "Vava (Lead Developer & Architect)",
            "Contributeurs de la communauté"
        };

        public static readonly string[] Translators =
        {
            "Vava (FR, EN)",
            "Traducteurs communautaires"
        };

        public static readonly string[] LanguagesUsed =
        {
            "C# (backend, UI)",
            "C++ (hooks natifs)",
            "Python (scripts)",
            "Lua (macros)",
            "HTML/CSS/JS (plugins, futur)"
        };

        public static readonly string[] LibrariesUsed =
        {
            "Avalonia",
            "NLua",
            "System.Text.Json",
            "System.Net.Http",
            "Microsoft.Extensions.Logging",
            "WinAPI, X11, Quartz"
        };

        public static readonly string[] SupportedPlatforms =
        {
            "Windows",
            "Linux (X11, futur Wayland)",
            "MacOS (à venir)"
        };

        public static string GetTeamInfo() => string.Join(", ", TeamMembers);
        public static string GetTranslatorsInfo() => string.Join(", ", Translators);
        public static string GetLanguagesInfo() => string.Join(", ", LanguagesUsed);
        public static string GetLibrariesInfo() => string.Join(", ", LibrariesUsed);
        public static string GetPlatformsInfo() => string.Join(", ", SupportedPlatforms);
        public static string GetGithubUrl() => GithubRepo;
        public static string GetVersionBadgeUrl() => GithubVersionBadge;

        /// <summary>
        /// Retourne le logo du projet en tant que Bitmap (Avalonia compatible).
        /// </summary>
        public static Bitmap? GetProjectIcon()
        {
            try
            {
                if (!File.Exists(LogoPath))
                    return null;

                using var stream = File.OpenRead(LogoPath);
                return new Bitmap(stream);
            }
            catch
            {
                return null;
            }
        }
    }
}
