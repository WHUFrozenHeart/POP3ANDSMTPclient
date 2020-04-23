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
    /// timer.xaml 的交互逻辑
    /// </summary>
    public partial class Timer : Window
    {

        public List<int> getTime = new List<int>();//①定义一个可读可写的公用的列表,获取时间：getTime

        public Timer()
        {
            InitializeComponent();
        }

        public void setTime()
        {
            getTime.Add(int.Parse(iHour.Text.ToString()));
            getTime.Add(int.Parse(iMinute.Text.ToString()));
        }

        // 定时发送
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            setTime();
            Close();
        }
    }
}
