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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using POP3client.Model;
using Microsoft.Win32;

namespace POP3client
{


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        POP3Client p;
        string name;
        string pass;
        List<MailItem> items = new List<MailItem>();
        string content;
        //string contentname;
        bool isGetting = false;

        public MainWindow(string name, string pass)
        {
            InitializeComponent();
            InitFunction();
            this.pass = pass;
            this.name = name;
            p = new POP3Client(name, pass);
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        private void GetMail()
        {
            isGetting = true;
            p.Quit();
            p = new POP3Client(name, pass);
            items.Clear();
            string num = p.Stat();
            Console.WriteLine(num);
            for (int i = 1; i <= int.Parse(num); i++)
            {
                Mail temp = p.Retr(i.ToString());
                MailItem mailItem = new MailItem()
                {
                    Sender = temp.sender,
                    Subject = temp.subject,
                    TextContent = temp.textcontent,
                    Attachment = temp.attachment,
                    Time = temp.time,
                    Id = (i - 1).ToString()
                };
                items.Add(mailItem);
                Dispatcher.Invoke(
                    () =>
                    {
                        MailListBox.ItemsSource = items;
                        MailListBox.SelectedValuePath = "Id";
                        MailListBox.Items.Refresh();
                    });
            }
            isGetting = false;
            MessageBox.Show("该账号的所有邮件已经获取完毕", "获取完毕", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InitFunction()
        {
            FunctionListBox.ItemsSource = FunctionItem.Instance.items;
        }

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            /*
             * 点击后拉去邮件，暂时不知道会以什么类型变量进行存储
             * 拉去后应该可以根据邮件动态生成几个选择按钮
             * 按钮都有一个统一的事件用来统一处理最右边的pannel显示邮件内容
             */
            if (isGetting)
            {
                MessageBox.Show("正在获取邮件中，请稍后再试", "正在获取...", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Thread thread = new Thread(GetMail);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        //进入写邮件界面
        private void DraftButton_Click(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit(name, pass);
            edit.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            p.Dele((MailListBox.SelectedIndex + 1).ToString());
            items.Remove(items[MailListBox.SelectedIndex]);
            MailListBox.ItemsSource = items;
            MailListBox.Items.Refresh();
        }

        private void RelayButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void XiaZai_Click(object sender, RoutedEventArgs e)
        {

            //p.LogWrite(items[MailListBox.SelectedIndex].AttachName, items[MailListBox.SelectedIndex].AttachContent);
            //MessageBox.Show("已下载到" + Directory.GetCurrentDirectory() + "文件夹下");
        }

        private void Serch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MailListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //content = items[MailListBox.SelectedIndex].AttachContent;
            //contentname = items[MailListBox.SelectedIndex].AttachName;
            //MailContent.Text = "发送方： " + items[MailListBox.SelectedIndex].Sender
            //                   + "\n" + "时间： " + items[MailListBox.SelectedIndex].Time
            //                   + "\n" + "主题： " + items[MailListBox.SelectedIndex].Subject
            //                   + "\n" + "附件名： " + items[MailListBox.SelectedIndex].AttachName
            //                   + "\n" + "内容如下：" + "\n" + items[MailListBox.SelectedIndex].TextContent;
            //Console.WriteLine(MailListBox.SelectedIndex);
            if (MailListBox.SelectedIndex < 0)
            {
                MailListBox.SelectedIndex = 0;
            }
            content = items[MailListBox.SelectedIndex].TextContent;
            MailContent.NavigateToString("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/></head><body>" + 
                content
                + "</body></html>");
            AttachmentContent.ItemsSource = items[MailListBox.SelectedIndex].Attachment;
            AttachmentContent.Items.Refresh();
        }

        private void ChaKanButton_Click(object sender, RoutedEventArgs e)
        {
            //content = items[int.Parse(MailListBox.SelectedIndex.ToString())].AttachContent;
            //contentname = items[int.Parse(MailListBox.SelectedIndex.ToString())].AttachName;
            //MailContent.Text = "发送方： " + items[int.Parse(MailListBox.SelectedIndex.ToString())].Sender
            //                   + "\n" + "时间： " + items[int.Parse(MailListBox.SelectedIndex.ToString())].Time
            //                   + "\n" + "主题： " + items[int.Parse(MailListBox.SelectedIndex.ToString())].Subject
            //                   + "\n" + "附件名： " + items[int.Parse(MailListBox.SelectedIndex.ToString())].AttachName
            //                   + "\n" + "内容如下：" + "\n" + items[int.Parse(MailListBox.SelectedIndex.ToString())].TextContent;
        }

        private void FunctionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AttachmentContent_LostFocus(object sender, RoutedEventArgs e)
        {
            AttachmentContent.SelectedItem = null;
        }

        private void Download()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = items[MailListBox.SelectedIndex].Attachment[AttachmentContent.SelectedIndex].name;
            saveFile.Filter = "所有文件(*.*)|(*.*)";
            if (saveFile.ShowDialog() != true)
            {
                return;
            }
            if (!File.Exists(saveFile.FileName.ToString()))
            {
                File.Create(saveFile.FileName.ToString()).Dispose();
            }
            else
            {
                File.Delete(saveFile.FileName.ToString());
            }
            File.WriteAllBytes(saveFile.FileName.ToString(),
                items[MailListBox.SelectedIndex].Attachment[AttachmentContent.SelectedIndex].content);
            MessageBox.Show("文件已经保存到" + saveFile.FileName.ToString(), "保存完毕", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AttachmentContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AttachmentContent.SelectedIndex == -1)
            {
                return;
            }
            Download();
        }
    }
}
