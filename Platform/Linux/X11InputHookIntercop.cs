using System;
using System.Runtime.InteropServices;

namespace KeybMacro.Platform.Linux
{
    public static class X11InputHookInterop
    {
        [DllImport("libX11.so.6")]
        public static extern IntPtr XOpenDisplay(IntPtr display);

        [DllImport("libX11.so.6")]
        public static extern void XCloseDisplay(IntPtr display);

        [DllImport("libXtst.so.6")]
        public static extern int XTestFakeKeyEvent(IntPtr display, uint keycode, bool is_press, ulong delay);

        [DllImport("libXtst.so.6")]
        public static extern int XTestFakeButtonEvent(IntPtr display, uint button, bool is_press, ulong delay);

        [DllImport("libXtst.so.6")]
        public static extern int XTestFakeMotionEvent(IntPtr display, int screen_number, int x, int y, ulong delay);

        [DllImport("libX11.so.6")]
        public static extern int XFlush(IntPtr display);
    }
}
