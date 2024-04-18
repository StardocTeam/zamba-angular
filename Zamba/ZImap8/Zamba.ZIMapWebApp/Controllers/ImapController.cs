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
            return Ok("Test NO UPDATED OK");
        }


        [HttpGet, HttpPost]
        [Route("GetImapEMails")]
        public IActionResult GetImapEMails([FromBody] imapConfig config)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo("Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Trace\\");
                if (!dir.Exists)
                    dir.Create();

                string tracefile = dir.FullName + "\\Trace ZImapAPI - " + config.ImapUsername + "-" + config.FolderName + "-" + DateTime.Now.ToString("dd-MM-yyyy HH") + ".txt";


                EmailService EC = new EmailService();
                var response = EC.RetrieveEmails(config, tracefile);
                var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                List<string> log = new List<string>();
                List<ZMessage> messages = new List<ZMessage>();

                DirectoryInfo dir = new DirectoryInfo("Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Exceptions\\");
                if (!dir.Exists)
                    dir.Create();
                string errorfile = dir.FullName + "\\Exception ZImapAPI - " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt";
                log.Add($"ZIMAP WebException: {ex.ToString()}");
                StreamWriter sw = new StreamWriter(errorfile);
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
                return StatusCode(500, $"Internal server error: {ex.ToString()}");
            }
        }

        [HttpGet, HttpPost]
        [Route("CleanExceptions")]
        public IActionResult CleanExceptions(int days)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo("Log\\");

                EmailService EC = new EmailService();

                if (dir.Exists)
                    EC.CleanTraceAndExceptions(dir, days);

            }
            catch (Exception)
            {
            }

            return Ok();
        }

       


    }
}
