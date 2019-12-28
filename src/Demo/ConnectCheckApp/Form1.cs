using Nkit.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Nkit.Core.Utils;

namespace CheckNetworkApp
{
    public partial class Form1 : Form
    {
        public IntPtr Handle1;
        private bool bForeground = false;
        private List<string> Logs = new List<string>();
        private List<string> Ips = new List<string>();
        public Form1()
        {
            InitializeComponent();
            var ip = ConfigurationManager.AppSettings["ip"]?.ToString();
            if (!string.IsNullOrEmpty(ip))
            {
                var arrip = ip.Split(';');
                foreach (var item in arrip)
                {
                    var nameip = item.Split('|');
                    Ips.Add($"{nameip[0].PadRight(6, ' ')}:{nameip[1]}");
                }
            }
            foreach (var item in Ips)
            {
                listBox1.Items.Add(item);
            }
            Handle1 = this.Handle;
            timer1.Enabled = true;
            new Thread(checkNetwork).Start();
        }

        #region  日志记录
        public static void log(string msg)
        {
            try
            {
                var logPath = AppDomain.CurrentDomain.BaseDirectory + @"\log\";
                if(!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                using (StreamWriter sw = new StreamWriter(logPath + DateTime.Now.ToString("yyyy-MM-dd_hh") + ".txt", true))
                {
                    sw.WriteLine("【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + msg);
                }
            }
            catch (Exception)
            {
            }
        }

        public delegate void LogAppendDelegate(Color color, string text);
        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(DateTime.Now.ToString("HH:mm:ss ") + text);
            richTextBox1.AppendText("\n");
            richTextBox1.ScrollToCaret();


            log(text);

            //if (richTextBox1.Lines.Length > 100) richTextBox1.Clear();
            //richTextBox1.Focus();
            //richTextBox1.Select(richTextBox1.TextLength, 0);
            //richTextBox1.ScrollToCaret();
            //richTextBox1.SelectionColor = color;
            //richTextBox1.AppendText(DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBox1.Invoke(la, Color.Red, text);
        }
        /// <summary> 
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBox1.Invoke(la, Color.Violet, text);
        }
        /// <summary> 
        /// 显示信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBox1.Invoke(la, Color.Black, text);
        }

        #endregion



        private delegate void delInfoList(string text);
        private void SetrichTextBox(string value)
        {

            if (richTextBox1.InvokeRequired)
            {
                delInfoList d = new delInfoList(SetrichTextBox);
                richTextBox1.Invoke(d, value);
            }
            else
            {
                if (richTextBox1.Lines.Length > 100)
                {
                    richTextBox1.Clear();
                }

                richTextBox1.Focus();
                richTextBox1.Select(richTextBox1.TextLength, 0);
                richTextBox1.ScrollToCaret();
                richTextBox1.AppendText(value);
            }
        }

        SoundPlayer player = null;
        private void checkNetwork()
        {

            //TestConnection("127.0.0.1", 11205, 1000, out string msg0);
            //LogMessage(msg0);

            var wav = ConfigurationManager.AppSettings["sound"]?.ToString();
            if(wav.StartsWith("\\")) wav = AppDomain.CurrentDomain.BaseDirectory + wav;
            if (!File.Exists(wav)) wav = AppDomain.CurrentDomain.BaseDirectory + @"\wav\01.wav";
            player = new SoundPlayer(wav);
            
            while (true)
            {
                foreach (var nameip in Ips)
                {
                    var b = NetHelper.PingIpOrDomainName(nameip.Split(':')[1].Trim(), out string msg);
                    if (b)
                    {
                        LogMessage(nameip.PadRight(20, ' ') + " " + msg);
                    }
                    else
                    {
                        LogError(nameip.PadRight(20, ' ') + " " + msg);
                        player.Play();
                        bForeground = true;
                        //MessageBox.Show("网络中断", "标题");
                        //ShowMsg("网络中断", "标题");
                    }
                    Thread.Sleep(3000);
                }
                Thread.Sleep(5000);
            }
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            e.Cancel=true;
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmShow();
        }

        private void ToolStripMenuItem_Active_Click(object sender, EventArgs e)
        {
            frmShow();
        }
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }

        private void frmShow()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (Handle1 != DllImportUtil.GetForegroundWindow() && bForeground)
            {
                frmShow();
                this.Activate();
                this.TopMost = true;
                DllImportUtil.SetForegroundWindow(Handle1);
                bForeground = false;
            }
        }

        System.Windows.Forms.Timer timer = null;
        string WM_Caption = string.Empty;
        public const int WM_CLOSE = 0x10;
        void ShowMsg(string msg, string caption)
        {
            WM_Caption = caption;

            if (timer != null && timer.Enabled == false)
            {
                timer.Enabled = true;
            }
            else
            {
                timer = new System.Windows.Forms.Timer(); 
                timer.Interval = (3 * 1000); 
                timer.Tick += new EventHandler(SetTimer_Tick);
                timer.Start();
            }

            MessageBox.Show(msg, caption, MessageBoxButtons.OK);
        }


        void SetTimer_Tick(object sender, EventArgs e)
        {
            KillMessageBox(); 
            ((System.Windows.Forms.Timer)sender).Enabled = false; 
        }
        private void KillMessageBox()
        {
            IntPtr ptr = DllImportUtil.FindWindow(null, WM_Caption);
            if (ptr != IntPtr.Zero)
            {
                DllImportUtil.PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
