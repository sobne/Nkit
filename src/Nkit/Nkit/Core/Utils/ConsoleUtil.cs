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
                switch (ctrlType)
                {
                    case CtrlCEvent: Console.WriteLine("Ctrl+C keydown"); break;

                    case CtrlBreakEvent: Console.WriteLine("Ctrl+Break keydown"); break;

                    case CtrlCloseEvent: Console.WriteLine("window closed"); break;

                    case CtrlLogoffEvent: Console.WriteLine("log off or shut down"); break;

                    case CtrlShutdownEvent: Console.WriteLine("system shut down"); break;

                    default: Console.WriteLine(ctrlType.ToString()); break;
                }
                var e = new CloseConsoleEventArgs((CloseConsoleCategory)ctrlType);
                ClosedConsole("Console", e);
                return e.IsCancel;
            }
            return false; //忽略处理，让系统进行默认操作
        }

        private const int CtrlCEvent = 0;//CTRL_C_EVENT = 0;//一个Ctrl+C的信号被接收，该信号或来自键盘，或来自GenerateConsoleCtrlEvent    函数   
        private const int CtrlBreakEvent = 1;//CTRL_BREAK_EVENT = 1;//一个Ctrl+Break信号被接收，该信号或来自键盘，或来自GenerateConsoleCtrlEvent    函数  
        private const int CtrlCloseEvent = 2;//CTRL_CLOSE_EVENT = 2;//当用户系统关闭Console时，系统会发送此信号到此   
        private const int CtrlLogoffEvent = 5;//CTRL_LOGOFF_EVENT = 5;//当用户退出系统时系统会发送这个信号给所有的Console程序。该信号不能显示是哪个用户退出。   
        private const int CtrlShutdownEvent = 6;//CTRL_SHUTDOWN_EVENT = 6;//当系统将要关闭时会发送此信号到所有Console程序

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
