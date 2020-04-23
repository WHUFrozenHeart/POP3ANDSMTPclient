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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP3client
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            /*
             * 这里要实现账号的验证
             * 验证通过才能跳转
             * 否则弹出提示
             * 可以添加两个输入框是否输入检测提示
             * 账号输入框名字为AccountTextBox
             * 密码输入框名字为PasswordTextBox
             */


            MainWindow mainWindow = new MainWindow(AcconutTextBox.Text,PasswordTextBox.Text);
            mainWindow.Show();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AcconutTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
