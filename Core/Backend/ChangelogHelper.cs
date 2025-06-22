using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace KeybMacro.Core.Backend
{
    public static class ChangelogHelper
    {
        private const string ChangelogFile = "CHANGELOG.md";

        public static string GetLatestChangelogVersion()
        {
            if (!File.Exists(ChangelogFile))
                return "";

            var text = File.ReadAllText(ChangelogFile);
            var match = Regex.Match(text, @"##\s+(\d+\.\d+\.\d+)", RegexOptions.Multiline);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static string GetChangelogSection(string version)
        {
            if (!File.Exists(ChangelogFile))
                return "";

            var lines = File.ReadAllLines(ChangelogFile);
            var sectionLines = new List<string>();
            bool inSection = false;
            string patternHeader = $@"^##\s+{Regex.Escape(version)}";

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (Regex.IsMatch(line, patternHeader))
                {
                    inSection = true;
                    sectionLines.Add(line);
                    continue;
                }
                if (inSection && Regex.IsMatch(line, @"^##\s+\d+\.\d+\.\d+"))
                {
                    break;
                }
                if (inSection)
                    sectionLines.Add(line);
            }
            return inSection ? string.Join(Environment.NewLine, sectionLines).Trim() : "";
        }

        public static string GetBaseVersion()
        {
            const string VersionFile = "version.txt";
            if (!File.Exists(VersionFile))
                return "0.0.0";
            return File.ReadAllText(VersionFile).Trim();
        }

        public static string GetBuildType()
        {
            const string BuildTypeFile = "build_type.txt";
            if (!File.Exists(BuildTypeFile))
                return "";
            var type = File.ReadAllText(BuildTypeFile).Trim().ToLower();
            return (type == "alpha" || type == "beta" || type == "dev") ? type : "";
        }

        public static string GetFullVersion()
        {
            var baseVersion = GetBaseVersion();
            var changelogVersion = GetLatestChangelogVersion();
            var buildType = GetBuildType();

            if (!string.IsNullOrEmpty(changelogVersion) && CompareVersions(changelogVersion, baseVersion) > 0)
            {
                var suffix = string.IsNullOrEmpty(buildType) ? "" : $"-{buildType}";
                return $"{changelogVersion}{suffix}";
            }
            if (!string.IsNullOrEmpty(buildType))
                return $"{baseVersion}-{buildType}";

            return baseVersion;
        }

        private static int CompareVersions(string v1, string v2)
        {
            var a1 = v1.Split('.');
            var a2 = v2.Split('.');
            for (int i = 0; i < Math.Min(a1.Length, a2.Length); i++)
            {
                if (int.TryParse(a1[i], out var n1) && int.TryParse(a2[i], out var n2))
                {
                    if (n1 > n2) return 1;
                    if (n1 < n2) return -1;
                }
                else
                {
                    return string.Compare(v1, v2, StringComparison.Ordinal);
                }
            }
            if (a1.Length > a2.Length) return 1;
            if (a1.Length < a2.Length) return -1;
            return 0;
        }
    }
}
