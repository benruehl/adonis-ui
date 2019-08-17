using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AdonisUI.Helpers
{
    public class HwndInterop
    {
        private const Int32 WM_SYSCOMMAND = 0x112;

        private const Int32 SC_MAXIMIZE = 0xF030;
        private const Int32 SC_RESTORE = 0xF120;
        private const Int32 SC_MINIMIZE = 0xF020;

        private readonly IntPtr _handle;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public HwndInterop(Window window)
        {
            _handle = new WindowInteropHelper(window).Handle;
        }

        public void Maximize()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
        }

        public void Restore()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
        }

        public void Minimize()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_MINIMIZE, IntPtr.Zero);
        }
    }
}
