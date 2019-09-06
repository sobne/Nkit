using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            //this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;

            AccountBox.Focus();
            //kbc.Visibility = Visibility.Hidden;
        }


        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void PWDBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    LoginBtn(btnLogin, null);
                    break;

                default:
                    break;
            }
        }

        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "登陆":
                    string Account = AccountBox.Text;
                    string Password = PWDBox.Password;

                    if (Account != "admin")
                    {
                        AccountBox.Effect = new DropShadowEffect { Color = Colors.OrangeRed };
                        PWDBox.Effect = new DropShadowEffect { Color = Colors.OrangeRed };
                        AccountBox.ToolTip = "username & password is invalid";
                        PWDBox.ToolTip = "username & password is invalid";
                        MessageBox.Show("login failed!");
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
                case "取消":
                    App.Current.Shutdown();
                    break;
            }
        }

    }
}
