using POP3client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace POP3client
{
    public class FunctionItem
    {
        public List<Function> items { get; set; }

        public static FunctionItem Instance
        {
            get
            {
                return new FunctionItem();
            }
        }

        private FunctionItem()
        {
            items = new List<Function>()
            {
                new Function()
                {
                    Icon = new BitmapImage(new Uri(@"\icons\RecievedMail.png", UriKind.Relative)),
                    Name = "收件箱"
                },
                new Function()
                {
                    Icon = new BitmapImage(new Uri(@"\icons\DraftMail.png", UriKind.Relative)),
                    Name = "草稿箱"
                },
                new Function()
                {
                    Icon = new BitmapImage(new Uri(@"\icons\SendedMail.png", UriKind.Relative)),
                    Name = "已发送"
                },
                new Function()
                {
                    Icon = new BitmapImage(new Uri(@"\icons\DeletedMail.png", UriKind.Relative)),
                    Name = "已删除"
                },
            };
        }
    }
}
