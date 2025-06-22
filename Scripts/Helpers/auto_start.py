import os
import platform

def enable_autostart():
    system = platform.system()
    if system == "Linux":
        path = os.path.expanduser("~/.config/autostart/keybmacro.desktop")
        with open(path, 'w') as f:
            f.write("[Desktop Entry]\nType=Application\nExec=keybmacro\n")
    elif system == "Windows":
        startup = os.path.join(os.getenv('APPDATA'), 'Microsoft\\Windows\\Start Menu\\Programs\\Startup')
        with open(os.path.join(startup, 'KeybMacro.bat'), 'w') as f:
            f.write("start keybmacro.exe")