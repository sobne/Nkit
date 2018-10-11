using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace UdpClientTest
{
    public delegate bool ConsoleCtrlDelegate(int dwCtrlType);
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handlerRoutine, bool add);
        private const int CtrlCEvent = 0;//CTRL_C_EVENT = 0;//一个Ctrl+C的信号被接收，该信号或来自键盘，或来自GenerateConsoleCtrlEvent    函数   
        private const int CtrlBreakEvent = 1;//CTRL_BREAK_EVENT = 1;//一个Ctrl+Break信号被接收，该信号或来自键盘，或来自GenerateConsoleCtrlEvent    函数  
        private const int CtrlCloseEvent = 2;//CTRL_CLOSE_EVENT = 2;//当用户系统关闭Console时，系统会发送此信号到此   
        private const int CtrlLogoffEvent = 5;//CTRL_LOGOFF_EVENT = 5;//当用户退出系统时系统会发送这个信号给所有的Console程序。该信号不能显示是哪个用户退出。   
        private const int CtrlShutdownEvent = 6;//CTRL_SHUTDOWN_EVENT = 6;//当系统将要关闭时会发送此信号到所有Console程序
        bool HandlerRoutine(int ctrlType)
        {
            Console.WriteLine("Set    SetConsoleCtrlHandler    success!!");
            switch (ctrlType)
            {
                case CtrlCEvent: Console.WriteLine("Ctrl+C keydown"); break;

                case CtrlBreakEvent: Console.WriteLine("Ctrl+Break keydown"); break;

                case CtrlCloseEvent: Console.WriteLine("window closed"); break;

                case CtrlLogoffEvent: Console.WriteLine("log off or shut down"); break;

                case CtrlShutdownEvent: Console.WriteLine("system shut down"); break;

                default: Console.WriteLine(ctrlType.ToString()); break;
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory +"log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "HandlerRoutine:"+ ctrlType);
            }
            return false;
        }
        public Program()
        {
            if (SetConsoleCtrlHandler(new ConsoleCtrlDelegate(HandlerRoutine), true))
            {
                Console.WriteLine("Set    SetConsoleCtrlHandler    success!!");
            }
            else
            {
                Console.WriteLine("Set    SetConsoleCtrlHandler    Error!!");
                //AsReportFile.WriteFile("", "test.txt", "who close?");
            }
            //Console.ReadLine();

        }
        [STAThread]
        static void Main(string[] args)
        {
            Program cl = new Program();

            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("127.0.0.1", 2000);
            Console.WriteLine("ping 127.0.0.1: " + reply.Status.ToString());

            sendata();

            Console.ReadLine();

        }
        static void sendata()
        {
            string sendStatusStr = string.Empty;
            sendStatusStr = ("0x530x060x34 0x300x00 0x00 0x00 0x000x020x43").Replace(" ", "").Replace("0x", "").Trim();
            int len = sendStatusStr.Length / 2;
            byte[] sendData = new byte[len];
            for (int j = 0; j < len; j++)
            {
                sendData[j] = Convert.ToByte(Convert.ToInt16(sendStatusStr.Substring(j * 2, 2), 16));
            }
            try
            {
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                IPEndPoint server = new IPEndPoint(serverIP, 50000);
                UdpClient udpClient = new UdpClient();
                udpClient.Send(sendData, sendData.Length, server);
                Console.WriteLine("发送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送失败: " + ex.ToString());
                return;
            }
        }
    }
}
