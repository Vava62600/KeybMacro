using System;
using System.IO;
using System.Text;

namespace KeybMacro.Core.Backend
{
    public static class LogManager
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFilePrefix = "KeybMacroLog_";
        private static readonly int MaxLogFiles = 10;
        private static readonly object _lock = new();

        static LogManager()
        {
            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);
        }

        public static void Log(string message, string level = "INFO")
        {
            try
            {
                lock (_lock)
                {
                    string logFile = GetCurrentLogFileName();
                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
                    File.AppendAllText(logFile, logLine + Environment.NewLine, Encoding.UTF8);
                    CleanupOldLogs();
                }
            }
            catch
            {
                // Ignore logging errors
            }
        }

        private static string GetCurrentLogFileName()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            return Path.Combine(LogDirectory, $"{LogFilePrefix}{date}.log");
        }

        private static void CleanupOldLogs()
        {
            var files = new DirectoryInfo(LogDirectory).GetFiles($"{LogFilePrefix}*.log");
            if (files.Length <= MaxLogFiles) return;

            Array.Sort(files, (a, b) => a.CreationTime.CompareTo(b.CreationTime));
            int toDelete = files.Length - MaxLogFiles;

            for (int i = 0; i < toDelete; i++)
            {
                try { files[i].Delete(); } catch { }
            }
        }
    }
}
