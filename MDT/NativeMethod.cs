using System;
using System.Runtime.InteropServices;

namespace MDT
{
    internal class NativeMethod
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
