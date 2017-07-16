using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace RemoteWebBrowserServer
{
    public static class WinApi
    {
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public const int SRCCOPY = 13369376;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public static Image CaptureWindow(IntPtr handle)
        {
            try
            {
                IntPtr hdcSrc = GetWindowDC(handle);

                RECT clientRect = new RECT();
                GetClientRect(handle, ref clientRect);

                RECT windowRect = new RECT();
                GetWindowRect(handle, ref windowRect);

                int width = clientRect.right - clientRect.left;
                int height = clientRect.bottom - clientRect.top;

                IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
                IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);

                IntPtr hOld = SelectObject(hdcDest, hBitmap);
                BitBlt(hdcDest, 0, 0, width, height, hdcSrc,
                    (windowRect.right - windowRect.left - width) / 2,
                    (windowRect.bottom - windowRect.top - height) - (windowRect.right - windowRect.left - width) / 2,
                    SRCCOPY);
                SelectObject(hdcDest, hOld);
                DeleteDC(hdcDest);
                ReleaseDC(handle, hdcSrc);

                Image image = Image.FromHbitmap(hBitmap);
                DeleteObject(hBitmap);

                return image;
            }
#if DEBUG
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + "\n\n" + exp.StackTrace);
            }
#else
            catch
            {
                // Ignore.
            }
#endif

            return Icon.FromHandle(handle).ToBitmap();
        }

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);
        
        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [DllImport("user32")]
        private static extern Int32 GetCursorPos(out POINT pt);

        public static Point GetCursorPosition()
        {
            POINT pt;
            GetCursorPos(out pt);

            return new Point(pt.x, pt.y);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        private static uint MouseButtonToCode(MouseButtons key)
        {
            switch (key)
            {
                case MouseButtons.Left:
                    return MOUSEEVENTF_LEFTDOWN;
                case MouseButtons.Right:
                    return MOUSEEVENTF_RIGHTDOWN;
                case MouseButtons.Middle:
                    return MOUSEEVENTF_MIDDLEDOWN;
            }

            return 0u;
        }

        public static void SendMouseDown(IntPtr wndHandle, MouseButtons key, Point clientPoint)
        {
            var oldPos = GetCursorPosition();

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            SetCursorPos(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = MouseButtonToCode(key);

            var inputs = new INPUT[] { inputMouseDown };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            /// return mouse 
            SetCursorPos(oldPos.X, oldPos.Y);
        }

        public static void SendMouseUp(IntPtr wndHandle, MouseButtons key, Point clientPoint)
        {
            var oldPos = GetCursorPosition();

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            SetCursorPos(clientPoint.X, clientPoint.Y);

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = (MouseButtonToCode(key) << 1);

            var inputs = new INPUT[] { inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            /// return mouse 
            SetCursorPos(oldPos.X, oldPos.Y);
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(int vk, uint scan, uint flags, uint extraInfo);
    }
}
