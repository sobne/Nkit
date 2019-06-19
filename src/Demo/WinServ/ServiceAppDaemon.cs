using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;
using Nkit.IO;

namespace WinServ
{
    public partial class ServiceAppDaemon : ServiceBase
    {
        private string[] _processAddress;
        private LogH _logH;
        
        public ServiceAppDaemon()
        {
            InitializeComponent();

            init();

        }
        private void init()
        {
            try
            {
                _logH = new LogH();
                string strProcessAddress = ConfigurationManager.AppSettings["ProcessAddress"].ToString();
                if (strProcessAddress.Trim() != "")
                {
                    this._processAddress = strProcessAddress.Split(',');
                }
                else
                {
                    throw new Exception("读取配置档ProcessAddress失败，ProcessAddress为空！");
                }
            }
            catch (Exception ex)
            {
                _logH.Log("Watcher()初始化出错！错误描述为：" + ex.Message.ToString());
            }
        }
        protected override void OnStart(string[] args)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Start.");
            }
            _logH.Log("start");
            try
            {
                this.StartWatch();
            }
            catch (Exception ex)
            {
                _logH.Log("OnStart() 出错，错误描述：" + ex.Message.ToString());
            }
        }

        protected override void OnStop()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
            }
            _logH.Log("stop");
        }

        private void StartWatch()
        {
            if (this._processAddress != null && this._processAddress.Length > 0)
            {
                foreach (string str in _processAddress)
                {
                    if (str.Trim() == "") continue;
                    if (File.Exists(str.Trim()))
                    {
                        this.ScanProcessList(str.Trim());
                    }
                }
            }
        }

        private void ScanProcessList(string address)
        {
            Process[] arrayProcess = Process.GetProcesses();
            foreach (Process p in arrayProcess)
            {
                if (p.ProcessName != "System" && p.ProcessName != "Idle")
                {
                    try
                    {
                        if (this.FormatPath(address) == this.FormatPath(p.MainModule.FileName.ToString()))
                        {
                            _logH.Log("进程(" + p.Id.ToString() + ")(" + p.ProcessName.ToString() + ")已经启动。");
                            this.WatchProcess(p, address);
                            return;
                        }
                    }
                    catch
                    {
                        _logH.Log("进程(" + p.Id.ToString() + ")(" + p.ProcessName.ToString() + ")拒绝访问全路径！");
                    }
                }
            }

            Process process = new Process();
            process.StartInfo.FileName = address;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
            _logH.Log("进程(" + process.Id.ToString() + ")(" + process.ProcessName.ToString() + ")未启动，自动启动。");
            this.WatchProcess(process, address);
        }


        private void WatchProcess(Process process, string address)
        {
            Thread thread = new Thread(new ThreadStart(() => RestartProcess(process, address)));
            thread.Start();
        }

        private string FormatPath(string path)
        {
            return path.ToLower().Trim().TrimEnd('\\');
        }
        public void RestartProcess(Process process, string address)
        {
            _logH.Log("进程(" + process.Id.ToString() + ")(" + process.ProcessName.ToString() + ")开启监控。");
            try
            {
                while (true)
                {
                    process.WaitForExit();
                    _logH.Log("进程(" + process.Id.ToString() + ")(" + process.ProcessName.ToString() + ")退出。");
                    process.Close();
                    process.StartInfo.FileName = address;
                    process.Start();
                    _logH.Log("进程(" + process.Id.ToString() + ")(" + process.ProcessName.ToString() + ")重启。");

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                _logH.Log("RestartProcess() 出错，监控程序已取消对进程("
                    + process.Id.ToString() + ")(" + process.ProcessName.ToString()
                    + ")的监控，错误描述为：" + ex.Message.ToString());
            }
        }
        
    }
}
