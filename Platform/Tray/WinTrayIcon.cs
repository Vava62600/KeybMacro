#if WINDOWS
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrayApp
{
    public class WinTrayIcon : ITrayIconImpl
    {
        private readonly NotifyIcon _notifyIcon;

        public WinTrayIcon(string iconPath, string title)
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon(iconPath),
                Text = title,
                Visible = true
            };
        }

        public void SetMenu((string Label, Action? Callback)[] items)
        {
            var contextMenu = new ContextMenuStrip();

            foreach (var (label, callback) in items)
            {
                var item = new ToolStripMenuItem(label);
                item.Click += (_, _) => callback?.Invoke();
                contextMenu.Items.Add(item);
            }

            _notifyIcon.ContextMenuStrip = contextMenu;
        }
    }
}
#endif