using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP3client.Model
{
    public class Attachment
    {
        public string name { get; set; }
        public byte[] content { get; set; }
    }
}
