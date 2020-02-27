using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace WpfApp2
{
    class DX : HwndHost
    {
        IntPtr app = IntPtr.Zero;

        [Flags]
        enum WindowStyle : int
        {
            WS_CHILD = 0x40000000,
            WS_VISIBLE = 0x10000000,
            LBS_NOTIFY = 0x00000001,
            HOST_ID = 0x00000002,
            LISTBOX_ID = 0x00000001,
            WS_VSCROLL = 0x00200000,
            WS_BORDER = 0x00800000,
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            IntPtr hwndHost = CreateWindowEx(
                0, "STATIC", "",
                WindowStyle.WS_CHILD | WindowStyle.WS_VISIBLE,
                0, 0,
                (int)ActualWidth, (int)ActualHeight,
                hwndParent.Handle,
                (IntPtr)WindowStyle.HOST_ID,
                IntPtr.Zero,
                0);
            return new HandleRef(this, hwndHost);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (app == IntPtr.Zero)
            {
                // BuildWindowCoreでInitしたかったが、は矩形が0のままなのでDepthBufferが作れない。
                // 本当はリサイズも考慮してデバイスのInitとRenderTarger/DepthBufferの生成を分けるべき。
                app = Init(hwnd, (int)ActualWidth, (int)ActualHeight);
            }

            Render(app);
            handled = false;
            return IntPtr.Zero;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            DestroyWindow(hwnd.Handle);
            Dispose(app);
        }

        [DllImport("user32.dll")]
        static extern IntPtr CreateWindowEx(int dwExStyle,
                                              string lpszClassName,
                                              string lpszWindowName,
                                              WindowStyle style,
                                              int x, int y,
                                              int width, int height,
                                              IntPtr hwndParent,
                                              IntPtr hMenu,
                                              IntPtr hInst,
                                              [MarshalAs(UnmanagedType.AsAny)] object pvParam);
        [DllImport("user32.dll")]
        static extern bool DestroyWindow(IntPtr hwnd);


        [DllImport("02_SimpleTriangle.dll")]
        static extern IntPtr Init(IntPtr hwnd, int width, int height);
        [DllImport("02_SimpleTriangle.dll")]
        static extern void Render(IntPtr app);
        [DllImport("02_SimpleTriangle.dll")]
        static extern IntPtr Dispose(IntPtr app);
    }
}
