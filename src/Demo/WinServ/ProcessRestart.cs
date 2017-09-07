using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace WinServ
{
    public class ProcessRestart
    {
        private Process _process;
        private string _address;

        public ProcessRestart()
        { }
        public ProcessRestart(Process process, string address)
        {
            this._process = process;
            this._address = address;
        }


        public void RestartProcess()
        {
            try
            {
                while (true)
                {
                    this._process.WaitForExit();
                    this._process.Close();
                    this._process.StartInfo.FileName = this._address;
                    this._process.StartInfo.UseShellExecute = true;
                    this._process.StartInfo.Verb = "runas";
                    this._process.Start();

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                utils.LogHelper _logHelper = new utils.LogHelper(logPath);
                _logHelper.Log("RestartProcess() 出错，监控程序已取消对进程("
                    + this._process.Id.ToString() + ")(" + this._process.ProcessName.ToString()
                    + ")的监控，错误描述为：" + ex.Message.ToString());
            }
        }
    }
}
