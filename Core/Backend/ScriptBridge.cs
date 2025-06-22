using System.Diagnostics;

namespace KeybMacro.Core.Backend
{
    public static class ScriptBridge
    {
        // Exécute un script python en mode asynchrone, récupère la sortie standard
        public static string RunPythonScript(string scriptPath, string args = "")
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "python3",
                    Arguments = $"\"{scriptPath}\" {args}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var process = Process.Start(psi);
                if (process == null) return "";
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            }
            catch
            {
                return "";
            }
        }
    }
}
