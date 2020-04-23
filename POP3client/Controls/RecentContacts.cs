using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace POP3client
{
    class RecentContacts
    {
        private List<string> ContactsEmail = new List<string>();
        private string ClientAccount;

        public RecentContacts(string clientAccount, string newContact)
        {
            ClientAccount = clientAccount;

            // 从本地文件中读取最近联系人邮箱
            ReadEmail();

            // 对联系人数组进行查重
            bool CheckSame = ContactsEmail.Contains(newContact);


            // 若为True，说明已存在该联系人
            if (CheckSame)
            {
                // 修改最近联系人
                UpdateRecentContact(newContact);
            }

            // 若为False，直接添加
            else
            {
                // 添加最近联系人
                AddRecentContact(newContact);
            }

            // 把最近联系人邮箱保存到本地文件
            WriteEmail();
        }

        public RecentContacts(string clientAccount)
        {
            ClientAccount = clientAccount;

            // 从本地文件中读取最近联系人邮箱
            ReadEmail();


        }



        // 把最近联系人信息返回后台
        public List<string> BackEmail()
        {
            List<string> backEmail = new List<string>();

            int temp = 0;
            foreach(string s in ContactsEmail)
            {
                if(temp < 15)
                {
                    backEmail.Add(s);
                    temp++;
                }
            }
            return backEmail;
        }


        // 从本地文件中读取最近联系人邮箱
        private void ReadEmail()
        {
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader("C:\\Users\\apple\\Desktop\\网络工程与编程实践\\Project\\POP3client v5\\POP3client\\Data\\" + ClientAccount + "-" + "RecentContacts.txt"))
                {
                    string line;

                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        ContactsEmail.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("读取最近联系人信息失败:");
                Console.WriteLine(e.Message);
            }
            // Console.ReadKey();
        }


        // 把最近联系人邮箱保存到本地文件
        public void WriteEmail()
        {
            
            using (StreamWriter sw = new StreamWriter("C:\\Users\\apple\\Desktop\\网络工程与编程实践\\Project\\POP3client v5\\POP3client\\Data\\" + ClientAccount + "-" + "RecentContacts.txt"))
            {
                foreach (string s in ContactsEmail)
                {
                    sw.WriteLine(s);

                }
            }

        }


        // 添加最近联系人
        public void AddRecentContact(string ContactEmail)
        {

            try
            {
                ContactsEmail.Insert(0, ContactEmail);
            }
            catch(Exception e)
            {
                Console.WriteLine("添加最近联系人失败:");
                Console.WriteLine(e.Message);
            }
        }

        // 修改最近联系人
        public void UpdateRecentContact(string ContactEmail)
        {

            try
            {
                // 删除已有元素
                for (int i = 0; i < ContactsEmail.Count; i++)
                {
                    if (ContactsEmail[i] == ContactEmail)
                    {
                        ContactsEmail.Remove(ContactsEmail[i]);
                    }
                }

                // 再添加
                ContactsEmail.Insert(0, ContactEmail);
            }
            catch (Exception e)
            {
                Console.WriteLine("添加最近联系人失败:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
