using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp.Internals;
using CefSharp;
using System.Runtime.Serialization.Json;
using System.IO;
using Zamba.ZChromium;
using Zamba.Core;
using Zamba.WFActivity.Regular;
using static Zamba.ZChromium.ZChromium;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Zamba.ZChromium
{
    public partial class Frm_News : Form, IChromiumZLink
    {
        private Notice n = new Notice();
        private Timer timer = new Timer();
        private User user;

        public Frm_News(User _user)
        {
            user = _user;
            InitializeComponent();

            ApplyEvents();
            ApplyStyles();
            InitChromium(user.ID);
        }
        private void ApplyEvents()
        {
            this.MouseMove += FadeOutFn;
            this.MouseLeave += FadeOutFn;
            this.MouseCaptureChanged += FadeOutFn;
            this.MouseDown += FadeOutFn;
            this.MouseEnter += FadeOutFn;
            this.MouseHover += FadeOutFn;
            this.MouseUp += FadeOutFn;
            this.MouseWheel += FadeOutFn;
        }
        private void InitChromium(Int64 userId)
        {
            try
            {
                string newsUrl = ZOptBusiness.GetValue("NewsURL");
                var notice = GetNoticeId(newsUrl, userId);
                if (notice.Id == 0)
                {
                    this.Close();
                    return;
                }
                var browser = new InitZChromium(this, newsUrl + "/newviews/newtooltip/" + notice.Id);
                this.Controls.Add(browser.Init());
                this.Text = "Zamba News - " + notice.Title;
                LinkLabel.Link link = new LinkLabel.Link();
                link.LinkData = newsUrl + "newviews/new/" + notice.Id;
                lnkLbl.Links.Add(link);
                lnkLbl.LinkClicked += LnkLbl_LinkClicked;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            FadeOutFn(null, null);
        }
        private void FadeOutFn(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
        }
        private Notice GetNoticeId(string url, Int64 userId)
        {
            var client = new HttpClient();

            var response = client.GetAsync(url + "/news/GetNewByUser?userId=" + userId).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                if (responseString == "\"\"") return n;
                JObject jObject = JObject.Parse(responseString);
                n = new Notice()
                {
                    Id = (int)jObject["Id"],
                    Title = (string)jObject["Title"]
                };
            }
            return n;
        }
        private class Notice
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
        private void ApplyStyles()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                                   Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        #region Event components
        private void LnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            this.FadeOut();
        }
        #endregion      

        #region Appearance show-hide methods
        public async void FadeIn(int interval = 100)
        {
            if (n.Id > 0)
            {
                this.Opacity = 0;
                this.Show();
                //Object is not fully invisible. Fade it in
                while (this.Opacity < 1.0)
                {
                    await Task.Delay(interval);
                    this.Opacity += 0.05;
                }
                this.Opacity = 1; //make fully visible       
            }
        }

        public async void FadeOut(int interval = 100)
        {
            //Object is fully visible. Fade it out
            while (this.Opacity > 0.0)
            {
                await Task.Delay(interval);
                this.Opacity -= 0.05;
            }
            //this.Opacity = 0; //make fully invisible                   
            this.Close();
        }
        #endregion

        #region Interface Methods
        void IChromiumZLink.DoAction(string action)
        {
           
        }

        void IChromiumZLink.DownloadFile(string file)
        {
           
        }

        void IChromiumZLink.SelectResult(int docTypeId, int docId, int taskId, int stepId)
        {
          
        }

        string IChromiumZLink.ShowMessage(string title, string message)
        {
            return null;
        }

        public void EnableSuggestions()
        {
           
        }

        public void HideBrowser()
        {
           
        }
        #endregion
    }
}
