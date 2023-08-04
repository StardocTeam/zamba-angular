using MailKit.Security;
using Zamba.MailKit;
using static Zamba.MailKit.EmailService;

namespace zimap
{
    public partial class Form1 : Form
    {
              

        public Form1()
        {
            InitializeComponent();
        }

        public string ImapServer = "nasa1mail.mmc.com";
        public int ImapPort = 143;
        public SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto;
        public string ImapUsername = "mgd\\eseleme\\pedidoscaucion@marsh.com";
        public string ImapPassword = "Octubre2023";
        public string FolderName = "INBOX";
        public string ExportFolderPath = "exported";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string config = textBox1.Text;
                Dictionary<string, string> list = new Dictionary<string, string>();
                foreach (string pair in config.Split(char.Parse(",")))
                {
                    if (pair.Split(char.Parse("=")).Length > 1)
                    {
                        string key = pair.Split(char.Parse("="))[0].Trim();
                        string value = pair.Split(char.Parse("="))[1].Trim().Replace("\"","");
                        list.Add(key, value);
                    }
                }

                imapConfig iconfig = new Zamba.MailKit.EmailService.imapConfig();
                iconfig.ImapServer = list.GetValueOrDefault("ImapServer", ImapServer);
                iconfig.ImapPort = int.Parse(list.GetValueOrDefault("ImapPort", ImapPort.ToString()));
                iconfig.secureSocketOptions = list.GetValueOrDefault("secureSocketOptions", secureSocketOptions.ToString());
                iconfig.ImapUsername = list.GetValueOrDefault("ImapUsername", ImapUsername);
                iconfig.ImapPassword = list.GetValueOrDefault("ImapPassword", ImapPassword);
                iconfig.FolderName = list.GetValueOrDefault("FolderName", FolderName);
                iconfig.ExportFolderPath = list.GetValueOrDefault("ExportFolderPath", ExportFolderPath);

                EmailService EC = new EmailService();
                this.textBox2.Text = EC.RetrieveEmails(iconfig).ToString();

            }
            catch (Exception ex)
            {
                this.textBox2.Text = ex.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fi = new OpenFileDialog();
                fi.ShowDialog();
                
                EmailService.ConvertEmlToMsg(fi.FileName, fi.FileName.Replace(".eml", ".msg"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}