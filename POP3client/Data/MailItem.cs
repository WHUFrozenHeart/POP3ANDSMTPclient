using POP3client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP3client
{
    public class MailItem
    {
        public static List<MailItem> items = new List<MailItem>();

        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
        public string TextContent { get; set; }
        public List<Attachment> Attachment { get; set; }
        public string Id { get; set; }

    }
}
