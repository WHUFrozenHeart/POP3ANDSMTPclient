using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace POP3client.Model
{
    public class Mail
    {
        public string sender { get; set; }
        public string subject { get; set; }
        public string textcontent { get; set; }
        public List<Attachment> attachment { get; set; }
        public string time { get; set; }
    }
}
