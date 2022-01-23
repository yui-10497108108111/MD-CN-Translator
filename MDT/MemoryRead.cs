using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MDT
{
    class MemoryRead
    {
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess,
int lpBaseAddress, byte[] lpBuffer, int dwSize,int lpNumberOfBytesRead);

        public static int Pointer(string ProcessName, object Address, int[] Offsets)
        {
            int BaseAddy = -1;

            Process[] P = Process.GetProcessesByName(ProcessName);
            if (P.Length == 0) return BaseAddy;

            if (Address.GetType() == typeof(Int32))
                BaseAddy = Convert.ToInt32(Address);

            else if (Address.GetType() == typeof(String))
            {
                string[] tmp = Convert.ToString(Address).Split('+');
                foreach (ProcessModule M in P[0].Modules)
                    if (M.ModuleName.ToLower() == tmp[0].ToLower())
                        BaseAddy = M.BaseAddress.ToInt32() + int.Parse(tmp[1], NumberStyles.HexNumber);
            }
            else return BaseAddy;

            byte[] buff = new byte[4];
            for (int i = 0; i < Offsets.Length; i++)
            {
                ReadProcessMemory(P[0].Handle, BaseAddy + Offsets[i], buff, 4,0);
                BaseAddy = BitConverter.ToInt32(buff, 0);
            }
            return BaseAddy;
        }
    }
}