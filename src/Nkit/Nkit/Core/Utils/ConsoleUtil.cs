using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Core.Utils
{
    public class ConsoleUtil
    {
        //ConsoleUtil.RegisterCloseConsoleHandle();
        //ConsoleUtil.DisableCloseButton();
        //ConsoleUtil.ClosedConsole += (sender, e) => Console.WriteLine("clicked close.");
        #region 禁用控制台关闭按钮

        ///
        /// 禁用关闭按钮
        ///
        public static void DisableCloseButton()
        {
            DisableCloseButton(Console.Title);
        }

        ///
        /// 禁用关闭按钮
        ///
        /// 控制台名字
        public static void DisableCloseButton(string consoleName)
        {

            IntPtr windowHandle = FindWindow(null, consoleName);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint scClose = 0xF060;
            RemoveMenu(closeMenu, scClose, 0x0);
        }

        #region API
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        #endregion

        #endregion

        #region 捕捉控制台关闭事件

        public delegate bool ConsoleCtrlDelegate(int ctrlType);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handlerRoutine, bool add);



        ///
        /// 注册控制台关闭事件,通过事件进行捕捉.
        ///
        public static void RegisterCloseConsoleHandle()
        {
            SetConsoleCtrlHandler(OnClosedConsole, true);
        }

        ///
        /// 当控制台被关闭时,引发事件.
        ///
        public static event EventHandler ClosedConsole;

        private static bool OnClosedConsole(int ctrlType)
        {
            if (ClosedConsole != null)
            {
                var e = new CloseConsoleEventArgs((CloseConsoleCategory)ctrlType);
                ClosedConsole("Console", e);
                return e.IsCancel;
            }
            return false; //忽略处理，让系统进行默认操作
        }

        #endregion
    }
    public class CloseConsoleEventArgs : EventArgs
    {
        public CloseConsoleEventArgs()
        {

        }

        public CloseConsoleEventArgs(CloseConsoleCategory category)
        {
            Category = category;
        }

        public CloseConsoleCategory Category { get; set; }

        ///
        /// 是否取消操作.
        ///
        public bool IsCancel { get; set; }
    }
    public enum CloseConsoleCategory
    {
        ///
        /// 当用户关闭Console
        ///
        CloseEvent = 2,
        ///
        /// Ctrl+C
        ///
        CtrlCEvent = 0,
        ///
        /// 用户退出（注销）
        ///
        LogoffEvent = 5,
        ///
        /// Ctrl+break
        ///
        CtrlBreakEvent = 1,
        ///
        /// 系统关闭
        ///
        ShutdownEvent = 6,
    }
}
