using System;
using System.Runtime.InteropServices;


public class NativeMethod
{
    const int GWL_EXSTYLE = -20;
    const int WS_EX_TRANSPARENT = 0x20;
    const int WS_EX_LAYERED = 0x80000;
    const int LWA_ALPHA = 2;
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    public static void SetMousePass(IntPtr hWnd)
    {
        SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED);
    }
    public static void RestoreMousePass(IntPtr hWnd)
    {
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
    }
    public static void SetWindowTransparent(IntPtr hWnd, byte bAlpha)
    {
        SetLayeredWindowAttributes(hWnd, 0, bAlpha, LWA_ALPHA);
    }
}
