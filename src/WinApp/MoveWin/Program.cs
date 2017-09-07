using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MoveWin
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //move();
        }
        static void move()
        {
            Process p0 = Process.GetCurrentProcess();
            Process[] ps = Process.GetProcessesByName("MoveWin");
            ps = ps.OrderByDescending(z => z.StartTime).ToArray();
            int i = 0;
            int x = 20;
            int y = 50;
            int nWidth = 700;
            int nHeight = 500;
            foreach (Process p in ps)
            {
                if (p0.Id != p.Id)
                {

                }
                if (i > 3) continue;
                IntPtr intptr = p.MainWindowHandle;
                //IntPtr intptr = FindWindow(null, "Form1");
                if (i == 1)
                {
                    x = 750;
                    y = 50;
                }
                if (i == 2)
                {
                    x = 20;
                    y = 600;
                }
                if (i == 3)
                {
                    x = 750;
                    y = 600;
                }
                MoveWindow(intptr, x, y, nWidth, nHeight, true);
                MessageBox.Show(i.ToString());
                i++;
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
