using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //try
            //{
            //    Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            //    Login window = new Login();
            //    bool? dialogResult = window.ShowDialog();
            //    if (dialogResult != null && dialogResult == true)
            //    {
            //        base.OnStartup(e);
            //        Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //    }
            //    else
            //    {
            //        this.Shutdown();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}
