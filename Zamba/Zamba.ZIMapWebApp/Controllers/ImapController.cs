using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using Zamba.MailKit;
using static Zamba.MailKit.EmailService;

namespace EmailRetrievalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImapController : ControllerBase
    {

       

        private bool IsPreviouslyRetrieved(string uid)
        {
            // Check if the email with the given UID has been previously retrieved
            // Implement your logic here (e.g., check a database or a file)

            return false;
        }

        [HttpGet, HttpPost]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("Test OK");
        }

        [HttpGet, HttpPost]
        [Route("TestImap")]
        public IActionResult TestImap()
        {
            Zamba.MailKit.EmailService.imapConfig iconfig = new Zamba.MailKit.EmailService.imapConfig();
            EmailService EC = new EmailService();

            var response = EC.RetrieveEmails(iconfig);
            return Ok(response);
        }

        [HttpGet, HttpPost]
        [Route("GetImapEMails")]
        public IActionResult GetImapEMails([FromBody] imapConfig config)
        {
            try
            {
               //  config = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailService.imapConfig>(config);
                EmailService EC = new EmailService();
                var response = EC.RetrieveEmails(config);
                var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(jsonResponse);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
