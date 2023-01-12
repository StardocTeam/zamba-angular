using System;
using System.ComponentModel;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.WFActivity.Regular;
using System.Net;
using Zamba.PreLoad;

namespace Zamba.ZChromium
{
    public partial class Form1 : Form, IChromiumZLink
    {
        private User user { get; set; }

        public User GetUser()
        {
            return user;
        }
        public Form1()
        {
            System.Reflection.Assembly assem = new RulesInstance().GetWFActivityRegularAssembly();
            try
            {
                string status = string.Empty;
                if (Zamba.Servers.Server.ConInitialized == false)
                    DBBusiness.InitializeSystem(ObjectTypes.Cliente, null, false, ref status, new ErrorReportBusiness());
                try
                {

                if ((bool.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, false).ToString())))
                {
                    NetworkCredential MasterCredentials = new NetworkCredential();
                    MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com");
                    MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice");
                    MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPasswordEncrypted", "sodio2017_nzt");
                    string ServerShare = ZOptBusiness.GetValueOrDefault("DomainServerShare", "\\\\svrimage\\zamba");
                    NetworkConnection NetworkConnection = new NetworkConnection(ServerShare, MasterCredentials);
                }
                }
                catch (Win32Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                //var loginform = new Zamba.AdminControls.Login(true, false, false, ObjectTypes.Link,string.Empty, string.Empty, string.Empty, assem);
                var loginform = new Zamba.AdminControls.Login(true, false, false, ObjectTypes.Link, string.Empty, string.Empty, string.Empty);
                if (loginform.ShowDialog() == DialogResult.OK)
                    user = (User)Zamba.Membership.MembershipHelper.CurrentUser;
                else
                    return;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            var browser = new InitZChromium(this);
            this.Controls.Add(browser.Init());
        }

        public void DoAction(string action)
        {

        }

        public void SelectResult(int docTypeId, int docId, int taskId = 0, int stepId = 0)
        {

        }

        public void ShowMessage(string title, string message)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string IChromiumZLink.ShowMessage(string title, string message)
        {
            return null;
        }

        public void DownloadFile(string file)
        {

        }

        public void EnableSuggestions()
        {

        }

        public void HideBrowser()
        {

        }
    }
}

