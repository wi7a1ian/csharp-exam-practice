using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqTest
{
    public interface IMailClient
    {
        string Server { get; set; }
        string Port { get; set; }

        bool SendMail(string from, string to, string subject, string body);
    }
}
