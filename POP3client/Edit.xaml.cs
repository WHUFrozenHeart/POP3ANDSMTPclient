
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {

        //SMTP 服务客户端
        private SMTPControler smptControler;



        //一些变量
        private string subject;         //邮件主题
        private string to;              //收件人
        private string from;            //发件人
        private string body;            //邮件正文
        private ArrayList filePaths;    //添加的附件数组



        public Edit(string userName,string password)
        {
            InitializeComponent();
            
            from = userName;
            filePaths = new ArrayList();
            From_Label.Content = from;

            smptControler = new SMTPControler();
            smptControler.Init(userName, password);


            // 读取最近联系人
            RecentContacts recentContacts = new RecentContacts(userName);
            // 展示最近联系人
            List<string> ContactsList = recentContacts.BackEmail();

            ShowRecentContacts(ContactsList);

            Console.WriteLine("最近联系人信息为：");
            foreach (string s in ContactsList)
            {
                Console.WriteLine(s);
            }
        }


        // 展示最近联系人
        private void ShowRecentContacts(List<string> contactsList)
        {
            Recent_Persons.ItemsSource = new List<string>();

            Recent_Persons.ItemsSource = contactsList;
        }

        // 最近联系人选择
        private void Recent_Persons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Recent_Persons.SelectedIndex > -1)
            {
                To_Address.Text = Recent_Persons.SelectedItem.ToString();
            }
        }


        //发送按钮
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendEmail();
        }

        // 发送邮件
        private void SendEmail()
        {
            subject = Subject_TextBox.Text;
            body = GetNewBody(System.Windows.Markup.XamlWriter.Save(Email_Body.Document));
            to = To_Address.Text;

            MailMessage msg = smptControler.CreateMessage(from, to, subject, body, filePaths);

            smptControler.Send(msg);

            // 如果发送成功,更新最近联系人列表
            RecentContacts recentContacts = new RecentContacts(from, to);
            // 展示最近联系人
            List<string> ContactsList = recentContacts.BackEmail();
            ShowRecentContacts(ContactsList);

            MessageBox.Show("发送成功");
        }


        //根据Email_Body里的内容，重新构建正文
        private string GetNewBody(string originalBody)
        {
            string newBody = "";

            string pattern = "<Image Source=\"file:///[^\"]*\"|<Run xml:lang=\"zh-cn\">[^</Run>]*</Run>";
            if(!Regex.IsMatch(originalBody,pattern))
            {
                return "";
            }
            MatchCollection matchs = Regex.Matches(originalBody, pattern);
            foreach(Match match in matchs)
            {
                if(match.Value.Contains("<Image Source=\"file:///"))
                {
                    string picPath = match.Value.Substring(23, match.Value.Length - 24);
                    newBody += smptControler.PicToBitmap(picPath);
                }
                else
                {
                    string text = match.Value.Substring(22, match.Value.Length - 28);
                    newBody += text;
                }
            }
            return newBody;
        }



        // 定制时间
        int iHour;
        int iMinute;
        int iSecond = 00;

        //定时发送按钮
        private void SendLaterButton_Click(object sender, RoutedEventArgs e)
        {

            Timer timerWindow = new Timer();
            timerWindow.ShowDialog();
            try
            {
                if(timerWindow.getTime.Count == 2)
                {
                    iHour = timerWindow.getTime[0];
                    iMinute = timerWindow.getTime[1];
                    MessageBox.Show("定时发送设置成功");
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // 设置定时器
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒
            // timer.AutoReset = false;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);

        }

        // 时间流逝器
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            // 得到 hour minute second  如果等于某个值就开始执行某个程序。
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            //// 定制时间； 比如 在10：30 ：00 的时候执行某个函数
            //int iHour = 20;
            //int iMinute = 18;
            //int iSecond = 00;
            //// 设置　 每秒钟的开始执行一次
            //if (intSecond == iSecond)
            //{
            //    Console.WriteLine("每秒钟的开始执行一次！");
            //}
            //// 设置　每个小时的３０分钟开始执行
            //if (intMinute == iMinute && intSecond == iSecond)
            //{
            //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
            //}

            // 设置　每天的１０：３０：００开始执行程序
            if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            {
                // 如果时间匹配，发送邮件
                //string subject_1;

                this.Subject_TextBox.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        if (Subject_TextBox.Text != null)
                        {
                            SendEmail();
                        }
                        else
                        {
                            SendEmail();
                        }
                    }
               )
            );        

                Console.WriteLine("在" + iHour + "点" + intMinute + "分开始执行！");
            }
        }

        //保存到草稿箱
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }


        //关闭
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //添加附件
        private void Add_FileButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "添加附加";
            ofd.Filter = "所有文件(*.*)|*.*";
            ofd.FileName = "选择文件夹";
            ofd.FilterIndex = 1;
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;

            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {

                foreach(string filePath in ofd.FileNames)
                {
                    //添加到filePaths中，以便发送的时候使用
                    filePaths.Add(filePath);

                    //用于前端展示
                    //动态创建自定义Button，并为其添加Click事件
                    Button b = new Button();
                    b.Click += new RoutedEventHandler(FileName_Click);
                    b.Tag = filePath;
                    b.Content = System.IO.Path.GetFileName(filePath);
                    b.Style = (Style)this.FindResource("LinkButton");

                  
                    File_All.Children.Add(b);

                }
            }
           
        }


        //点击文件名来实现删除
        private void FileName_Click(object sender,RoutedEventArgs e)
        {
            if(MessageBox.Show("您确定要删除吗？", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Button button = sender as Button;
                
                filePaths.Remove(button.Tag.ToString());

                File_All.Children.Remove(button);
            }
           
        }

        //添加图片
        private void Add_PicButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "添加图片";
            ofd.Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp";
            ofd.FileName = "选择文件夹";
            ofd.FilterIndex = 1;
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;

            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {

                ArrayList base64_all = new ArrayList();

                foreach (string filePath in ofd.FileNames)
                {

                    //在richTextBox里进行预览
                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();

                    BitmapImage bImg = new BitmapImage();

                    img.IsEnabled = true;

                    bImg.BeginInit();

                    bImg.UriSource = new Uri(filePath, UriKind.RelativeOrAbsolute);

                    bImg.EndInit();

                    img.Source = bImg;

                    //MessageBox.Show(bImg.Width.ToString() + "," + bImg.Height.ToString());

                    //调整图片大小

                    if (bImg.Height > 100 || bImg.Width > 100)

                    {

                        img.Height = bImg.Height * 0.2;

                        img.Width = bImg.Width * 0.2;

                    }

                    img.Stretch = Stretch.Uniform;  //图片缩放模式
                    new InlineUIContainer(img, Email_Body.Selection.End); //插入图片到选定位置
                }
            }

        }




    }
}
