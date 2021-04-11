using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;

            var timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Normal;
            //this.WindowStyle = WindowStyle.None;
            //this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            //this.Width = SystemParameters.PrimaryScreenWidth;
            //this.Height = SystemParameters.PrimaryScreenHeight;


            WindowLoading();

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeOutput.Content = DateTime.Now.ToString();
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            //if (WindowState == WindowState.Normal)
            //    WindowState = WindowState.Maximized;
            //else
            //    WindowState = WindowState.Normal;
        }

        private void mniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = true;
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnSearch0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
        }


        private void WindowLoading()
        {

            TypeInit();
        }
        private void TypeInit()
        {
            string[] items = { "AA", "BB", "CC", "DDD", "EEEEEE"};

        }
    }
}
