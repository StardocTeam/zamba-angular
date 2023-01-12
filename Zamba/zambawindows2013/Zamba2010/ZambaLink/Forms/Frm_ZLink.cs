using CefSharp.WinForms;
using System;
using System.Windows.Forms;
using Zamba;
using Zamba.Core;
using Zamba.Link;
using Zamba.Link.Properties;
using System.Deployment.Application;
using Microsoft.VisualBasic;
using Zamba.WFActivity.Regular;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using static Zamba.ZChromium.ZChromium;
using System.Threading;
using Zamba.PreLoad;
using System.Net;
using System.ComponentModel;

namespace ZambaLink
{
    public partial class Frm_ZLink : Form, IChromiumZLink
    {
        public bool isReady { get; set; }
        private bool enableSuggestions { get; set; }
        private ChromiumWebBrowser cwb;
        private ChromiumWebBrowser cwbGS;
        private ChromiumWebBrowser cwbSP;
        private User user { get; set; }
        private bool closeApp = false;
        public string line = String.Empty;
        private bool showHideNotification;
        private static bool isZambaUser;
        public static Frm_GlobalSearch gsForm { get; set; }
        public static Frm_SuggestedPeople spForm { get; set; }

        delegate void DInitializeSP();
        delegate void DInitializeGS();
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if (gsForm != null)
                gsForm.Location = new Point(Left + Width, Top);
            if (spForm != null)
                spForm.Location = new Point(Left - Width, Top);
        }

        public void HideBrowser()
        {

        }

        public static bool IsZambaUser()
        {
            return isZambaUser;
        }
        public static User GetUser()
        {
            return (User)Zamba.Membership.MembershipHelper.CurrentUser;
        }
        private Version GetRunningVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            else
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public Frm_ZLink(string commandLine = "")
        {
            string status = string.Empty;

            try
            {
                if (Zamba.Servers.Server.ConInitialized == false)
                    DBBusiness.InitializeSystem(ObjectTypes.Link, null, true, ref status, new ErrorReportBusiness());
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
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            InitializeComponent();
        }

        private void InitializeGS()
        {
            if (this.InvokeRequired)
            {
                var dGS = new DInitializeGS(InitializeGS);
                this.Invoke(dGS);
            }
            else
            {
                gsForm = new Frm_GlobalSearch();

                var gsBrowser = new InitZLink(this);
                cwbGS = gsBrowser.Init(ApplicationType.GlobalSearch);
                gsForm.Controls.Add(cwbGS);
            }
        }

        private void InitializeSP()
        {
            if (this.InvokeRequired)
            {
                var dSP = new DInitializeSP(InitializeSP);
                this.Invoke(dSP);
            }
            else
            {                
                spForm = new Frm_SuggestedPeople();
                var spBrowser = new InitZLink(this);
                cwbSP = spBrowser.Init(ApplicationType.SuggestedPeople);
                spForm.Controls.Add(cwbSP);
                SuggestedPeople.Initialize(this);
            }
            Invoke(new DInitializeGS(() =>
            {

            }));
        }

        private void Frm_ZLink_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assem = new RulesInstance().GetWFActivityRegularAssembly();

            if (!IsCollaboration())
            {
                if (!String.IsNullOrEmpty(line))
                {
                    line = line.ToUpper();
                }
                else
                {
                    var loginform = new Zamba.AdminControls.Login(true, false, false, ObjectTypes.Link, string.Empty, string.Empty, string.Empty, assem);
                    if (loginform.ShowDialog() == DialogResult.OK)
                    {
                        isZambaUser = true;
                        user = (User)Zamba.Membership.MembershipHelper.CurrentUser;
                    }
                    else
                        return;
                }
            }
            else
            {
                isZambaUser = false;
            }

            //var tSP = new Thread(new ThreadStart(InitializeSP));
            //tSP.Start();
            //var tGS = new Thread(new ThreadStart(InitializeGS));
            //tGS.Start();

            ZLinkSetProperties();

            this.WindowState = FormWindowState.Normal;
            if (!String.IsNullOrEmpty(line))
            {
                if (!UserBusiness.Rights.ValidateLogIn(int.Parse((line.Split(Char.Parse("="))[1]).Split(Char.Parse(" "))[0]), ClientType.Desktop).Equals(null))
                {
                    user = (User)Zamba.Membership.MembershipHelper.CurrentUser;
                    isZambaUser = true;
                }
            }
            var browser = new InitZLink(this);
            cwb = browser.Init(ApplicationType.ZambaLink);
            this.SetStyle(ControlStyles.Selectable, true);

            this.Controls.Add(cwb);
            ZLinkEvents();
        }

        #region Mouse events to move form
        //private bool drag;
        //private int mousex;
        //private int mousey;
        //private new void MouseUp(object sender, MouseEventArgs e)
        //{
        //    drag = false;
        //}
        //private new void MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (drag)
        //    {
        //        Top = Cursor.Position.Y - mousey;
        //        Left = Cursor.Position.X - mousex;
        //    }
        //}
        //private new void MouseDown(object sender, MouseEventArgs e)
        //{
        //    drag = true;
        //    mousex = Cursor.Position.X - Left;
        //    mousey = Cursor.Position.Y - Top;
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_NCHITTEST)
        //        m.Result = (IntPtr)(HT_CAPTION);
        //}
        //private const int WM_NCHITTEST = 0x84;
        //private const int HT_CLIENT = 0x1;
        //private const int HT_CAPTION = 0x2;

        // [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        // private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect, // x-coordinate of upper-left corner
        //    int nTopRect, // y-coordinate of upper-left corner
        //    int nRightRect, // x-coordinate of lower-right corner
        //    int nBottomRect, // y-coordinate of lower-right corner
        //    int nWidthEllipse, // height of ellipse
        //    int nHeightEllipse // width of ellipse
        //);
        #endregion

        private bool IsCollaboration()
        {
            bool isCollServer = false;
            var client = new HttpClient();

            var response = client.GetAsync(ConfValues.GetZLinkWS() + "/collaboration/iscollaborationserver").Result;
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                isCollServer = bool.Parse(responseString);
            }
            return isCollServer;
        }

        private void ZLinkSetProperties()
        {
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            saveFileDialog.Title = "Seleccione destino de archivo";
            saveFileDialog.CheckPathExists = true;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(Resources.ZLink));
            this.notifyIcon.Text = "Zamba Link";
            this.notifyIcon.Visible = true;
            var cm = new ContextMenu();
            var btnOpen = new MenuItem("&Abrir");
            btnOpen.Click += BtnOpen_Click;
            var btnHelp = new MenuItem("A&yuda");
            btnHelp.Click += BtnHelp_Click;
            var btnClose = new MenuItem("&Salir");
            btnClose.Click += btnClose_Click;
            cm.MenuItems.Add(btnOpen);
            cm.MenuItems.Add(btnHelp);
            cm.MenuItems.Add(btnClose);

            notifyIcon.ContextMenu = cm;
        }
        private void ZLinkEvents()
        {
            versiónToolStripMenuItem.Text = "Versión " + GetRunningVersion().ToString(4);
            this.notifyIcon.MouseClick += NotifyIcon_MouseDoubleClick;
            this.BringToFront();
            this.Focus();
            this.KeyPreview = true;
            this.notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            this.Resize += Frm_ZLink_Resize;
            this.FormClosing += Frm_ZLink_FormClosing;
            this.Disposed += Frm_ZLink_Disposed;
            //this.menuStrip.MouseHover += MenuStrip_MouseHover;
            //this.menuStrip.MouseLeave += MenuStrip_MouseLeave;
            menuStrip.Renderer = new MyRenderer();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            #region Mouse events with round corner
            //this.notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            //nIcon.MouseClick += NotifyIcon_MouseDoubleClick;
            //nIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            //movePic.MouseUp += MouseUp;
            //movePic.MouseMove += MouseMove;
            //movePic.MouseDown += MouseDown;
            //menuStrip.MouseUp += MouseUp;
            //menuStrip.MouseMove += MouseMove;
            //menuStrip.MouseDown += MouseDown;
            #endregion
        }

        private void Frm_ZLink_Disposed(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            var ZHlp = new ZLink_Help();
            ZHlp.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            ShowZLink();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }
        private void Frm_ZLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeApp)
            {
                if (MessageBox.Show("¿Desea salir de la aplicacion?",
                    "Zamba Link", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    CefSharp.Cef.Shutdown();
                    notifyIcon.Visible = false;
                }
                else
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                }
            }
            else
            {
                notifyIcon.Visible = false;
            }
            closeApp = false;
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowZLink();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowZLink();
        }
        private void ShowZLink()
        {
            notifyIcon.Icon = ((System.Drawing.Icon)(Resources.ZLink));
            ShowInTaskbar = true;
            StartPosition = FormStartPosition.CenterScreen;
            this.BringToFront();
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Frm_ZLink_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (!showHideNotification)
                {
                    notifyIcon.ShowBalloonTip(500, "Zamba Link",
                        "La aplicación se encuentra minimizada, haga click para restaurar", ToolTipIcon.Info);
                    showHideNotification = true;
                }
                InitZLink.ExecuteJSFunction(cwb, "minimize");
                Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                InitZLink.ExecuteJSFunction(cwb, "restore");
            }
            else
            {
                InitZLink.ExecuteJSFunction(cwb, "maximize");
            }
        }        
        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitZLink.ExecuteJSFunction(cwb, "refresh");
            WindowState = FormWindowState.Normal;
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var help = new ZLink_Help();
            help.Show();
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleReportViewer.Show(InitZLink.GetConsole(), "Log de consola Chromium");
        }

        private void executeJSCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var jsFn = Interaction.InputBox("Ingrese su código JS", "Ejecutar funcion JavaScript");
            if (!string.IsNullOrEmpty(jsFn))
                InitZLink.ExecuteJSFunction(cwb, "js", jsFn);
        }

        private void ayudaToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var help = new ZLink_Help();
            help.Show();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void webConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitZLink.ExecuteJSFunction(cwb, "js", "alert(conString);");
        }

        private void appiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Zamba.Servers.Server.AppConfig.DB);
        }

        private void zambaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSearch.Show(this);
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuggestedPeople.Show(this);
        }
    }
}
