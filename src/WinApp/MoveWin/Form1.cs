using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MoveWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = DateTime.Now.ToString("HH:mm:ss fff");
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
        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr intptr = FindWindow(null, "Form1");

            int x = 200;
            int y = 200;
            int nWidth = 389;
            int nHeight = 225;
            MoveWindow(intptr, x, y, nWidth, nHeight, true);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void Form1_Load(object sender, EventArgs e)
        {
            //move();
        }
    }
}
