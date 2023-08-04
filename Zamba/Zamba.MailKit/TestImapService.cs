using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.MailKit
{
    public class TestImapService
    {
        public void TestImap(Zamba.MailKit.EmailService.imapConfig imapConfig) { 
        EmailService ES = new EmailService();
            ES.RetrieveEmails(imapConfig);
       
        }
    }  
}
