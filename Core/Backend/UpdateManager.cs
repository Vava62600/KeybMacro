using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KeybMacro.Core.Backend
{
    public class UpdateInfo
    {
        public string Latest { get; set; } = "";
        public string Current { get; set; } = "";
        public bool HasUpdate => CompareVersions(Latest, Current) > 0;

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

    public static class UpdateManager
    {
        private const string LatestReleaseApiUrl = "https://api.github.com/repos/vava62600/keybmacro/releases/latest";

        public static async Task<UpdateInfo> CheckUpdatesAsync()
        {
            var currentVersion = ChangelogHelper.GetFullVersion();
            try
            {
                using var http = new HttpClient();
                http.DefaultRequestHeaders.UserAgent.ParseAdd("KeybMacroUpdateChecker");
                var jsonString = await http.GetStringAsync(LatestReleaseApiUrl);
                using var doc = JsonDocument.Parse(jsonString);

                string latestTag = doc.RootElement.GetProperty("tag_name").GetString() ?? "";
                if (latestTag.StartsWith("v"))
                    latestTag = latestTag[1..];

                var info = new UpdateInfo
                {
                    Current = currentVersion,
                    Latest = latestTag
                };
                LogManager.Log($"Checked updates: Current={info.Current}, Latest={info.Latest}");
                return info;
            }
            catch (Exception ex)
            {
                LogManager.Log($"Update check failed: {ex.Message}", "ERROR");
                return new UpdateInfo
                {
                    Current = currentVersion,
                    Latest = currentVersion
                };
            }
        }
    }
}
