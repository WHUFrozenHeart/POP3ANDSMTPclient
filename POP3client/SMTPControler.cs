using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace POP3client
{
    class SMTPControler
    {
        private SmtpClient smtpClient;

        //初始化smtp客户端
        public void Init(string userName,string password)
        {
            string host = getHost(userName);// 邮件服务器  
            smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
            smtpClient.Host = host;//邮件服务器
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);//用户名、密码
        }

        //通过userName来确定邮件服务器
        private string getHost(string userName)
        {
            string pattern = "\\@.*\\.com|\\@.*\\.com\\.cn";
            MatchCollection matchs = Regex.Matches(userName, pattern);
            return "smtp." + matchs[0].Value.Substring(1);
        }

        //创建邮件
        public MailMessage CreateMessage(string from,string to,string subject,string body, ArrayList filePaths)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(from);               //发件人
            msg.To.Add(to);                                 //收件人
            msg.BodyEncoding = System.Text.Encoding.UTF8;   //邮件内容编码   
            msg.IsBodyHtml = true;                          //是否是HTML邮件   
            msg.Priority = MailPriority.High;               //邮件优先级

            msg.Subject = subject;
            msg.Body = body;
            foreach(string filePath in filePaths)
            {
                Attachment aPic = new Attachment(filePath);
                msg.Attachments.Add(aPic);
            }
            return msg;
        }

        //发送邮件
        public void Send(MailMessage msg)
        {
            try
            {
                smtpClient.Send(msg);
                Console.WriteLine("发送成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "发送邮件出错");
            }


        }

        //将图片转换为bitmap
        public string PicToBitmap(string picPath)
        {
            //将图片转码
            Bitmap bmp = new Bitmap(picPath);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            String strbaser64 = Convert.ToBase64String(arr);
            string result = "<BODY style=\"MARGIN: 1px\"><DIV><IMG src=\"data:image/jpg;base64,"
                + strbaser64 + "\"> </IMG></DIV></BODY> ";
            return result;
        }

    }
}
