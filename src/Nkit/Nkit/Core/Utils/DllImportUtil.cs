using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Core.Utils
{
    public class DllImportUtil
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        #region 设置控制台标题 禁用关闭按钮
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        #endregion
        #region 关闭控制台 快速编辑模式、插入模式
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_INSERT_MODE = 0x0020;
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);
        #endregion



        #region gdi
        [DllImport("gdi32.dll")]
        private static extern int BitBlt(
        IntPtr hdcDest,     // handle to destination DC (device context)
        int nXDest,         // x-coord of destination upper-left corner
        int nYDest,         // y-coord of destination upper-left corner
        int nWidth,         // width of destination rectangle
        int nHeight,        // height of destination rectangle
        IntPtr hdcSrc,      // handle to source DC
        int nXSrc,          // x-coordinate of source upper-left corner
        int nYSrc,          // y-coordinate of source upper-left corner
        System.Int32 dwRop  // raster operation code
        );
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        [DllImportAttribute("gdi32.dll")]
        public static extern void DeleteObject(IntPtr obj);
        [DllImport("gdi32", EntryPoint = "StretchBlt")]
        public static extern int StretchBlt(
              IntPtr hdc,
              int x,
              int y,
              int nWidth,
              int nHeight,
              IntPtr hSrcDC,
              int xSrc,
              int ySrc,
              int nSrcWidth,
              int nSrcHeight,
              int dwRop
        );
        [DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);


        #endregion



        #region public method
        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;    //移除快速编辑模式
            mode &= ~ENABLE_INSERT_MODE;        //移除插入模式
            SetConsoleMode(hStdin, mode);
        }

        static void DisableCloseButton(string title)
        {
            IntPtr windowHandle = FindWindow(null, title);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }
        protected static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
    }
}
