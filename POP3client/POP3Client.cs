using System.Net;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using POP3client.Model;

namespace POP3client
{
    class POP3Client
    {
        public string decodestring = "UTF-8";
        public TcpClient Server;
        public NetworkStream NetStrm;
        public StreamReader RdStrm;
        public string Data;
        public byte[] szData;
        public string CRLF = "\r\n";//發送空行
        public POP3Client(string user, string pass)
        {
            Login(user, pass);
        }

        //用用户与密码进行连接
        public void Login(string Username, string Password)
        {
            Server = new TcpClient("pop3.163.com", 110);
            NetStrm = Server.GetStream();
            RdStrm = new StreamReader(Server.GetStream());
            //登录服务器过程 
            Data = "USER " + Username + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            RdStrm.ReadLine();

            Data = "PASS " + Password + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            RdStrm.ReadLine();

        }
        //获得邮件的数量以及大小，返回的是邮件的数量
        public string Stat()
        {

            //向服务器发送STAT命令，从而取得邮箱的相关信息：邮件数量和大小 
            Data = "STAT" + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            string szTemp = RdStrm.ReadLine();
            string[] mm = Regex.Split(szTemp, "\\s+", RegexOptions.IgnoreCase);
            string all = mm[1];
            return all;

        }

        //获得每一个邮件的编号，以及大小（暂时不知有何用，因为默认都是从1开始的，按时间从远到近）
        public void List()
        {
            string szTemp = "";
            Data = "LIST " + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            while (szTemp != ".")
            {
                szTemp = RdStrm.ReadLine();
                //Console.WriteLine(szTemp);
            }

        }


        //獲得郵件發送方，主題，txt內容和html內容，返回字符串數組（發送方，主題，txt內容，附件名，附件内容，时间）,以后扩展下载附件
        public Mail Retr(string i)
        {
            string sender = "";
            string subject = "";
            string time = "";
            string szTemp;
            //根据邮件编号从服务器获得相应邮件 
            Data = "RETR " + i + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            szTemp = RdStrm.ReadLine();
            Regex regex1 = new Regex("From: .*");
            Regex regex2 = new Regex("Subject: .*");
            Regex regex3_1 = new Regex("Content-Type: text/html.*");
            Regex regex3_2 = new Regex("------=.*");
            Regex regex4_1 = new Regex("Content-Disposition: attachment.*");
            //Regex regex5 = new Regex("=?gb18030?B?.*");
            Regex regex6 = new Regex("Date: .*");
            Regex regex7 = new Regex("Content-Type: image.*");
            string textcontent = "";
            string attachname = "";
            string attachcontent = "";
            List<Attachment> attachment = new List<Attachment>();
            List<string> photos = new List<string>();
            //不断地读取邮件内容，只到结束标志：英文句号 
            while (szTemp != ".")
            {
                //line += szTemp+CRLF
                //用於得到發送人
                if (regex1.IsMatch(szTemp))
                {
                    string[] mm = Regex.Split(szTemp, "\\s+", RegexOptions.IgnoreCase);
                    sender = mm[mm.Length - 1];
                }
                //用於得到主題
                if (regex2.IsMatch(szTemp))
                {
                    /*string[] mm2 = Regex.Split(szTemp, "\\s+", RegexOptions.IgnoreCase);
                    subject = mm2[1];
                    if (regex5.IsMatch(subject))
                    {
                        subject = subject.Substring(12, subject.Length - 14);
                        subject = DecodeBase64(subject);
                    }*/
                    Regex regex22 = new Regex("Subject: =.*");
                    if (regex22.IsMatch(szTemp))
                    {
                        Regex re = new Regex("(?<=\\?).*?(?=\\?)", RegexOptions.None);
                        MatchCollection mc = re.Matches(szTemp);
                        foreach (Match ma in mc)
                        {
                            decodestring = ma.Value;
                            break;
                        }
                        foreach (Match ma in mc)
                        {
                            subject = ma.Value;

                        }
                        subject = DecodeBase64(subject);
                    }
                    else
                    {
                        subject = szTemp.Substring(9);
                    }

                }
                //用於獲得html內容
                if (regex3_1.IsMatch(szTemp))
                {
                    int temp;
                    Regex re = new Regex("(?<=Content-Transfer-Encoding: ).*", RegexOptions.None);
                    string transferEncoding = "";
                    for (temp = 0; temp <= 2; temp++)
                    {
                        szTemp = RdStrm.ReadLine();
                        if (re.IsMatch(szTemp))
                        {
                            MatchCollection mc = re.Matches(szTemp);
                            foreach (Match ma in mc)
                            {
                                transferEncoding = ma.Value;
                                break;
                            }
                        }
                    }
                    while (szTemp == "\r\n")
                    {
                        szTemp = RdStrm.ReadLine();
                    }
                    while (!regex3_2.IsMatch(szTemp))
                    {
                        textcontent = textcontent + szTemp;
                        szTemp = RdStrm.ReadLine();
                    }
                    Console.WriteLine(textcontent);
                    if (transferEncoding == "base64")
                    {
                        textcontent = DecodeBase64(textcontent);
                    }
                }
                //用於獲得附件名，以及附件内容
                if (regex4_1.IsMatch(szTemp))
                {
                    attachcontent = "";
                    attachname = "";
                    Regex re = new Regex("(?<=\").*?(?=\")", RegexOptions.None);
                    MatchCollection mc = re.Matches(szTemp);
                    foreach (Match ma in mc)
                    {
                        attachname = ma.Value;
                        break;
                    }
                    szTemp = RdStrm.ReadLine();
                    //attachname = szTemp.Substring(7,szTemp.Length-8);
                    while (szTemp == "\r\n")
                    {
                        szTemp = RdStrm.ReadLine();
                    }
                    //Console.WriteLine(attachname);
                    while (!regex3_2.IsMatch(szTemp))
                    {
                        attachcontent += szTemp;
                        szTemp = RdStrm.ReadLine();
                    }
                    //attachcontent = DecodeBase64(attachcontent);
                    //Console.WriteLine(attachcontent);
                    attachment.Add(new Attachment()
                    {
                        name = attachname,
                        content = DecodeAttachment(attachcontent)
                    });
                }
                if (regex6.IsMatch(szTemp))
                {

                    string[] mm = Regex.Split(szTemp, "\\s+", RegexOptions.IgnoreCase);
                    time = mm[2] + "." + mm[3] + "." + mm[4] + " " + mm[5];

                }
                if (regex7.IsMatch(szTemp))
                {
                    string photosource = "";
                    for (int temp = 0; temp < 5; temp++)
                    {
                        szTemp = RdStrm.ReadLine();
                        //用於獲得附件名，以及附件内容
                        if (regex4_1.IsMatch(szTemp))
                        {
                            attachcontent = "";
                            attachname = "";
                            Regex re = new Regex("(?<=\").*?(?=\")", RegexOptions.None);
                            MatchCollection mc = re.Matches(szTemp);
                            foreach (Match ma in mc)
                            {
                                attachname = ma.Value;
                                break;
                            }
                            szTemp = RdStrm.ReadLine();
                            //attachname = szTemp.Substring(7,szTemp.Length-8);
                            while (szTemp == "\r\n")
                            {
                                szTemp = RdStrm.ReadLine();
                            }
                            //Console.WriteLine(attachname);
                            while (!regex3_2.IsMatch(szTemp))
                            {
                                attachcontent += szTemp;
                                szTemp = RdStrm.ReadLine();
                            }
                            //attachcontent = DecodeBase64(attachcontent);
                            //Console.WriteLine(attachcontent);
                            attachment.Add(new Attachment()
                            {
                                name = attachname,
                                content = DecodeAttachment(attachcontent)
                            });
                            continue;
                        }
                    }
                    while (szTemp == "\r\n")
                    {
                        szTemp = RdStrm.ReadLine();
                    }
                    while (!regex3_2.IsMatch(szTemp))
                    {
                        photosource = photosource + szTemp;
                        szTemp = RdStrm.ReadLine();
                    }
                    photos.Add(photosource);
                }
                szTemp = RdStrm.ReadLine();
            }
            Regex regex8 = new Regex("\"cid.*?\"");
            for (int j = 0; j < photos.Count; j++)
            {
                textcontent = regex8.Replace(textcontent, "\"data:image;base64," + photos[j] + "\"", 1);
            }
            return new Mail()
            {
                sender = sender,
                subject = subject,
                textcontent = textcontent,
                attachment = attachment,
                time = time
            };
        }

        //用於解碼
        private string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.WriteLine(decodestring);
            decode = Encoding.GetEncoding(decodestring).GetString(bytes);
            return decode;
        }

        private byte[] DecodeAttachment(string code)
        {
            return Convert.FromBase64String(code);
        }

        //删除指定标号的邮件
        public void Dele(string i)
        {
            string szTemp;
            Data = "DELE " + i + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            szTemp = RdStrm.ReadLine();

        }
        //用于关掉socket，输入输出流等，退出邮箱
        public void Quit()
        {
            string szTemp;
            Data = "QUIT " + CRLF;
            szData = Encoding.ASCII.GetBytes(Data.ToCharArray());
            NetStrm.Write(szData, 0, szData.Length);
            szTemp = RdStrm.ReadLine();
            NetStrm.Close();
            RdStrm.Close();

        }

        public void LogWrite(string filename, string content)
        {          //项目根目录
            string path = Directory.GetCurrentDirectory() + "\\" + filename;

            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                //文本写入
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
    }

}