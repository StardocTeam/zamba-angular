using System;
using System.Drawing;
using System.Windows.Forms;
//using Zamba;
using Zamba.Core;
//using Zamba.Core.Users;
using Zamba.Link.Properties;
using System.Net;
using System.Text.RegularExpressions;

namespace ZambaLink
{
    public partial class Frm_ZLink : IChromiumZLink
    {
        delegate void DDoAction(string action);
        public void DoAction(string action)
        {
            if (this.InvokeRequired)
            {
                var delegatedoaction = new DDoAction(DoAction);
                this.Invoke(delegatedoaction, new object[] { action });
            }
            else
            {
                switch (action)
                {
                    case "minimize":
                        this.WindowState = FormWindowState.Minimized;
                        break;
                    case "shake":
                        Shake(this);
                        break;
                    case "ready":
                        isReady = true;
                        EnableFeatures();                      
                        break;
                }
            }
        }

        private void EnableFeatures()
        {
            menuStrip.Enabled = true;
            ControlBox = true;
            ShowSuggestions();
        }

        Timer timerDelay = new Timer();

        private void TimerDelay_Tick(object sender, EventArgs e)
        {
            timerDelay.Enabled = false;
            SuggestedPeople.Show(this);
        }
        private void ShowSuggestions()
        {
            if (enableSuggestions)
            {
              //  InitializeSP();
                try
                {
                    timerDelay.Interval = 5000;
                    timerDelay.Enabled = true;
                    timerDelay.Tick += TimerDelay_Tick;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else
                personasToolStripMenuItem.Enabled = false;
        }


        delegate string DShowMessage(string title, string message);
        public string ShowMessage(string title, string message)
        {
            var winState = WindowState.ToString();
            if (this.InvokeRequired)
            {
                var delegateshowmessage = new DShowMessage(ShowMessage);
                this.Invoke(delegateshowmessage, new object[] { title, message });
            }
            else
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(message);
                    var msg = Regex.Replace(htmlDoc.DocumentNode.InnerText, "<.*?>", String.Empty);
                    msg = string.IsNullOrEmpty(msg) ? "Nuevo Mensaje" : (htmlDoc.DocumentNode.InnerText).Replace("&nbsp;", string.Empty);
                    notifyIcon.Icon = ((Icon)(Resources.ZLinkMsg));
                    notifyIcon.ShowBalloonTip(5, title, msg, ToolTipIcon.Info);
                }
            }
            return winState;
        }

        delegate void DDownloadFile(string file);
        public void DownloadFile(string file)
        {
            if (this.InvokeRequired)
            {
                var delegatedownloadfile = new DDownloadFile(DownloadFile);
                this.Invoke(delegatedownloadfile, new object[] { file });
            }
            else
            {
                try
                {
                    Uri uri = new Uri(file);
                    var name = System.IO.Path.GetFileName(uri.LocalPath);
                    saveFileDialog.FileName = name;
                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string local = saveFileDialog.FileName;
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(file, local);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }
        private static void Shake(Form form)
        {
            var original = form.Location;
            var rnd = new Random(1337);
            const int shake_amplitude = 18;
            for (int i = 0; i < 40; i++)
            {
                form.Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude), original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                System.Threading.Thread.Sleep(25);
            }
            form.Location = original;
        }

        public void SelectResult(int docTypeId, int docId, int taskId = 0, int stepId = 0)
        {
            throw new NotImplementedException();
        }

        public void EnableSuggestions()
        {
            enableSuggestions = true; 
        }

    }
}

