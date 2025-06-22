#include "Library/X11/include/X11/Xlib.h"
#include "Library/X11/include/X11/xtest.h"

extern "C" {

void SendX11Key(Display* display, unsigned int keycode, bool press) {
    XTestFakeKeyEvent(display, keycode, press, 0);
    XFlush(display);
}

void SendX11MouseButton(Display* display, unsigned int button, bool press) {
    XTestFakeButtonEvent(display, button, press, 0);
    XFlush(display);
}

void MoveX11Mouse(Display* display, int screen, int x, int y) {
    XTestFakeMotionEvent(display, screen, x, y, 0);
    XFlush(display);
}

}
