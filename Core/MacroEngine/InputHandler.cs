using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using KeybMacro.Platform.Windows;
using KeybMacro.Platform.Linux;

namespace KeybMacro.Core.MacroEngine
{
    public static class InputHandler
    {
        [SupportedOSPlatform("windows")]
        public static void InitWindowsHooks()
        {
            WinInputHookInterop.InstallKeyboardHook();
        }

        [SupportedOSPlatform("linux")]
        public static void SendLinuxKey(int keycode, bool press)
        {
            X11InputHookInterop.SendKey(keycode, press);
        }
    }
}
