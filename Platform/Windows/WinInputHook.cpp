#include <windows.h>

extern "C" {

__declspec(dllexport) void SendWinKey(int keycode, bool press) {
    keybd_event(static_cast<BYTE>(keycode), 0, press ? 0 : KEYEVENTF_KEYUP, 0);
}

__declspec(dllexport) void SendWinMouse(int button, bool press) {
    DWORD flags = 0;
    switch (button) {
        case 1: flags = press ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_LEFTUP; break;
        case 2: flags = press ? MOUSEEVENTF_RIGHTDOWN : MOUSEEVENTF_RIGHTUP; break;
    }
    mouse_event(flags, 0, 0, 0, 0);
}

__declspec(dllexport) void MoveWinMouse(int x, int y) {
    SetCursorPos(x, y);
}

}
