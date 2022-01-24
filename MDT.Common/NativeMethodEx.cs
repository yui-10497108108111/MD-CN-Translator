using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class NativeMethodEx
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
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
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

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(
     IntPtr hdc // handle to DC
     );
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(
     IntPtr hdc,         // handle to DC
     int nWidth,      // width of bitmap, in pixels
     int nHeight      // height of bitmap, in pixels
     );
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(
     IntPtr hdc,           // handle to DC
     IntPtr hgdiobj    // handle to object
     );
    [DllImport("gdi32.dll")]
    private static extern int DeleteDC(
     IntPtr hdc           // handle to DC
     );
    [DllImport("user32.dll")]
    private static extern bool PrintWindow(
     IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
     IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
     UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
     );
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(
     IntPtr hwnd
     );

    public static Bitmap GetWindowCapture(IntPtr hWnd)
    {
        IntPtr hscrdc = GetWindowDC(hWnd);
        Rectangle windowRect = new Rectangle();
        GetWindowRect(hWnd, ref windowRect);
        int width = Math.Abs(windowRect.X - windowRect.Width);
        int height = Math.Abs(windowRect.Y - windowRect.Height);
        IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
        IntPtr hmemdc = CreateCompatibleDC(hscrdc);
        SelectObject(hmemdc, hbitmap);
        PrintWindow(hWnd, hmemdc, 2);
        Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
        DeleteDC(hscrdc);
        DeleteDC(hmemdc);
        return bmp;
    }

}
