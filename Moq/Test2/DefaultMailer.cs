using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqTest
{
    public class DefaultMailer : IMailer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public bool SendMail(IMailClient mailClient)
        {
            return mailClient.SendMail(this.From, this.To, this.Subject, this.Body);
        }
    }
}
