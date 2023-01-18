using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Zamba.AdminControls;
using Zamba.Core;
using Zamba.Servers;
using Zamba.Tools;
using Zamba.WFActivity.Regular;
using Zamba.ZTimers;
using Zamba.PreLoad;
using Zamba.Framework;

namespace Zamba.Start
{
    public partial class Start : Form
    {
        public static NetworkConnection NetworkConnection;
        private string line = string.Empty;
        private IntPtr cbdata;
        private bool firstTime = true;
        private Login login;
        public int userId;
        private bool UserValidatedFlag;
        private ContextMenu notifyIconContextMenu;
        private MenuItem notifyIconMenuItem;
        FUP.UPBusiness FUP = new FUP.UPBusiness();

        public Start()
        {
            try
            {


                try
                {

                    AppIniManager AM = new AppIniManager();
                    AM.LoadAppIniFromServer();
                    AM.ExecuteFilesFromServer();
                    AM = null;

                }
                catch (Exception)
                {
                }

                if (Server.ConInitialized == false)
                {
                    string status = string.Empty;
                    DBBusiness.InitializeSystem(ObjectTypes.Start, null, false, ref status, new ErrorReportBusiness());
                    if ((bool.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, false).ToString())))
                    {
                        NetworkCredential MasterCredentials = new NetworkCredential();
                        MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com");
                        MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice");
                        MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPassword", "sodio2017_nzt");
                        string ServerShare = ZOptBusiness.GetValueOrDefault("DomainServerShare", "\\\\svrimage\\zamba");
                        NetworkConnection = new NetworkConnection(ServerShare, MasterCredentials);
                    }
                }

            }
        catch(Win32Exception ex)
        {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            InitializeComponent();

            notifyIconContextMenu = new ContextMenu();
            notifyIconMenuItem = new MenuItem();
            notifyIconMenuItem.Index = 0;
            notifyIconMenuItem.Text = "Cerrar Zamba Start";
            notifyIconMenuItem.Click += NotifyIconMenuItem_Click;
            notifyIconContextMenu.MenuItems.Add(notifyIconMenuItem);
            notifyIcon1.ContextMenu = notifyIconContextMenu;
        }

        private void DeleteShorcuts()
        {
            try
            {
                string filePath = ZOptBusiness.GetValue("shorcutsPath");
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        List<string> shorcutsNonDeleted = new List<string>();
                        ShortcutHandler.DeleteShorcuts(File.ReadLines(filePath), ref shorcutsNonDeleted);
                        if (shorcutsNonDeleted.Count > 0)
                        {
                            foreach (string shorcut in shorcutsNonDeleted)
                                ZTrace.WriteLineIf(ZTrace.IsError, shorcut);
                        }
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No existe el archivo con los nombres de accesos directos a borrar");
                    }
                }
            }
            catch (Exception ex)
            {
               // ZClass.raiseerror(ex);
            }
        }



        private void Start_Resize(object sender, System.EventArgs e)
        {
            //if (!firstTime)
            //{

            //    //si el estado actual de la venta es minimizado
            //    if (this.WindowState == FormWindowState.Minimized)

            //    {

            //        //ocultamos el formulario
            //        this.Visible = false;

            //        //Hacemos visible el icono de la bandeja del sistema
            //        this.notifyIcon1.Visible = true;
            //    }
            //}

        }

        ZTimer TCheck = null;
        Object State = null;
        private void Start_Load(object sender, EventArgs e)
        {
            if (!firstTime)
            {
                //si el estado actual de la venta es minimizado
                if (this.WindowState == FormWindowState.Minimized)
                {
                    //ocultamos el formulario
                    this.Visible = false;
                    //Hacemos visible el icono de la bandeja del sistema
                    this.notifyIcon1.Visible = true;
                }
            }
            else
            {
                this.Text = "Zamba Start";

                if (Environment.GetCommandLineArgs().Count() <= 1)
                {
                    DialogResult frmResult;
                    String noticiaLegal = ZOptBusiness.GetValue("LegalNoticeMessage");

                    if (String.IsNullOrEmpty(noticiaLegal) == true)
                    {
                        frmResult = MessageBox.Show("Este sistema es para ser utilizado solamente por usuarios autorizados. Toda la información contenida en los sistemas es propiedad de la Empresa y pueden ser supervisados, cifrados, leídos, copiados o capturados y dados a conocer de alguna manera, solamente por personas autorizadas." + (char)13 + "El uso del sistema por cualquier persona, constituye de su parte un expreso consentimiento al monitoreo, intervención, grabación, lectura, copia o captura y revelación de tal intervención." + (char)13 + "EL usuario debe saber que en la utilización del sistema no tendrá privacidad frente a los derechos de la empresa responsable del sistema." + (char)13 + "El uso indebido o no autorizado de este sistema genera  responsabilidad para el infractor, quién por ello estará sujeto al resultado de las acciones civiles y penales que la Empresa considere pertinente realizar en defensa de sus derechos y resguardo de la privacidad del sistema." + (char)13 + "Si Usted no presta conformidad con las reglas precedentes y no está de acuerdo con ellas, desconéctese ahora.", "Noticia Legal", MessageBoxButtons.OKCancel);
                    }
                    else
                    {
                        frmResult = MessageBox.Show(noticiaLegal, "Noticia Legal", MessageBoxButtons.OKCancel);
                    }

                    if (frmResult == DialogResult.Cancel)
                    {
                        Application.Exit();

                    }
                }
            }

            FUP.DownloadCompleted += DownloadCompleted;
            FUP.DownloadTryCompleted += DownloadTryCompleted;
            FUP.DownloadInProgress += DownloadInProgress;
            FUP.DownloadError += DownloadError;

            validateUser(false);


            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(Path.Combine(Application.StartupPath, "app.ini"));
                System.IO.DirectoryInfo di = new DirectoryInfo(Membership.MembershipHelper.AppTempPath);
                if (fi.Exists)
                {
                    if (di.Exists == false)
                    { di.Create(); }
                    if (File.Exists(Path.Combine(di.FullName, "app.ini")) == false)
                    {
                        fi.CopyTo(Path.Combine(di.FullName, "app.ini"), true);
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se copia el AppIni de la carpeta de ejecucion del Start");
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                System.Windows.Forms.MessageBox.Show("No se pudo copiar el app.ini de la Exportacion, contacte al administrador del sistema", "Zamba Exportacion", System.Windows.Forms.MessageBoxButtons.OK);
            }


            try
            {
                LoadButtons();
                AppIniManager AM = new AppIniManager();
                AM.SetLastAppServerPathFormConfigFile();
                AM = null;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString());
                Application.Exit();
            }

            try
            {
                System.Threading.TimerCallback CTCB = new System.Threading.TimerCallback(RecurrentCheckClient);
                TCheck = new ZTimers.ZTimer(CTCB, State, 1000, 60000, 7, 23);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString());
                Application.Exit();
            }

            ShowCurrentVersion();

            DeleteShorcuts();

        }

        private void ShowCurrentVersion()
        {
            try
            {
                string CurrentVersion = UpdaterBusiness.GetVersion();
                this.lblcurrentversion.Text = "Zamba " + CurrentVersion;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString());
            }
        }

        public void validateUser(Boolean IsLink)
        {
            try
            {
                if (Membership.MembershipHelper.CurrentUser == null)
                {
                    ValidarUsuarioYContrasenya(IsLink);
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString());
                Application.Exit();
            }


            /*If Me.UserValidatedFlag Then
            Try
                ' Me.AddHandlers()
                ' Me.ConectarAVolumenes()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "FrmMain LOAD TERMINADO AT: Dim Cliente As String = | " & Date.Now.ToString)
                Me.setModulesRights()
                ' Me.WindowState = FormWindowState.Maximized
                ' Me.LoadIcons()
                'Cargo el Zcore con la configuracion Global
                'ZCore.LoadCore()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Else
            Application.Exit()
        End If*/
        }

        private void ValidarUsuarioYContrasenya(Boolean IsLink)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando la validación de usuario at: | " + DateTime.Now.ToString());

            login = new Login(false, false, false, ObjectTypes.Start, string.Empty, string.Empty, string.Empty, new RulesInstance().GetWFActivityRegularAssembly());

            try
            {
                if (!UserBusiness.IsWUPreferenceAndExistUser())
                {
                    login.StartPosition = FormStartPosition.CenterParent;
                    if (login.IsDisposed == false) login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString());
            }

            if (login.DialogResult == DialogResult.OK)
            {
                if (UserBusiness.ValidateDataBase())
                {
                    this.UserValidatedFlag = true;
                    if (IsLink)
                        this.WindowState = FormWindowState.Minimized;
                    else
                        this.WindowState = FormWindowState.Normal;

                }

            }


            // Cuando vuelve a aparecer el formulario de login tras haber expirado el time_out, el formulario principal del administrador se
            // deshabilita. Por lo tanto, si el administrador vuelve a loguearse, y es válido, el formulario principal debe volver a habilitarse
            if (!this.Enabled)
            {
                this.Enabled = true;
            }
            else
            {

            }

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validación de usuario Finalizada at: | " + DateTime.Now.ToString());
        }

        private void RecurrentCheckClient(Object state)
        {
            try
            {


                while (CheckForNews && Checking == false && FUP.Downloading == false)
                {
                    try
                    {
                        Checking = true;
                        if (FUP.CheckNews())
                        {
                            CheckForNews = false;
                        }
                        else
                        {
                            ShowCurrentVersion();
                        }
                        System.Threading.Thread.Sleep(300000);
                        Checking = false;

                    }
                    finally
                    {
                        Checking = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }



        private class App
        {
            public string name { get; set; }
            public string icon { get; set; }
            public int appId { get; set; }
            public string path { get; set; }
            public string imagePath { get; set; }
        }

        Dictionary<Int64, App> apps = new Dictionary<Int64, App>();
        private void LoadButtons()
        {

            string zppSelect = "select id_app, nombre_app, icon_path, exe_app, icon.ID_ICON from zapp application inner join zicons icon on application.id_icon = icon.id_icon order by id_icon";
            try
            {
                IDataReader zapp = Server.get_Con().ExecuteReader(CommandType.Text, zppSelect);

                if (zapp != null)
                {

                    while (zapp.Read())
                    {
                        App newapp = new App();
                        newapp.appId = zapp.GetInt32(0);
                        newapp.name = zapp.GetString(1);
                        newapp.icon = zapp.GetString(2);
                        newapp.path = zapp.GetString(3);

                        if (UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, (ObjectTypes)newapp.appId, RightsType.Use))
                        {
                            AddButton(newapp);
                            apps.Add(newapp.appId, newapp);
                            if (newapp.name == "Cliente") FUP.currentClient = newapp.path;
                        }

                    }

                    if (apps.Count() == 1 && apps[113].name == "Cliente")
                    {
                        if (Environment.GetCommandLineArgs().Count() > 1)
                        {
                            ExecuteCurrentClient(Environment.GetCommandLineArgs()[1]);
                            MinimizedStart();
                        }
                        else ExecuteCurrentClient(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }

            if (apps.Count == 0)
            {
                App newapp = new App();
                newapp.appId = 0;
                newapp.name = "Cliente";
                newapp.icon = "ZambaCliente.ico";
                newapp.path = "Zamba.Cliente.exe";
                apps.Add(newapp.appId, newapp);
                AddButton(newapp);
                FUP.currentClient = newapp.path;
                if (Environment.GetCommandLineArgs().Count() > 1)
                    ExecuteCurrentClient(Environment.GetCommandLineArgs()[1]);
                else ExecuteCurrentClient(string.Empty);

            }

            this.panelBtns.Visible = true;
        }

        private void MinimizedStart()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        private void AddButton(App newapp)
        {
            ToolTip myTag = new ToolTip();
            RadLabel lbl = new RadLabel();
            RadPanel pnl = new RadPanel();

            RadButton btnApp = new RadButton();
            lbl.AutoSize = false;
            lbl.Text = newapp.name.Trim();
            lbl.Height = 25;
            lbl.TextAlignment = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Bottom;
            lbl.ForeColor = Color.White;

            btnApp.Text = string.Empty;
            btnApp.Tag = newapp.appId;

            string pathImageFull = Path.Combine(Application.StartupPath, "Images\\", Convert.ToString(newapp.icon));
            btnApp.Height = 60;
            btnApp.Width = 70;
            btnApp.Dock = DockStyle.Fill;
            btnApp.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            btnApp.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;

            try
            {
                Image img;

                ZTrace.WriteLineIf(ZTrace.IsError, pathImageFull);
                using (FileStream fs = new FileStream(pathImageFull, FileMode.Open, FileAccess.Read))
                {
                    using (Image original = Image.FromStream(fs))
                    {
                        img = original.GetThumbnailImage(48, 48, new Image.GetThumbnailImageAbort(CB), cbdata);
                        ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).Image = img;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Image img;
                    img = Image.FromFile(Path.Combine(Application.StartupPath, "Images\\20.jpg"));
                    img = img.GetThumbnailImage(48, 48, new Image.GetThumbnailImageAbort(CB), cbdata);
                    ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).Image = img;
                }
                catch (Exception)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                }
                //Zamba.AppBlock.ZException.Log(ex);
            }

                ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).Text = newapp.name.Trim();
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).BackColor = Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(btnApp.GetChildAt(0))).Opacity = 0.6;
            pnl.Height = btnApp.Height + lbl.Height;
            pnl.Width = btnApp.Width + 10;
            ((BorderPrimitive)pnl.PanelElement.Children[1]).Width = 0;

            myTag.SetToolTip(btnApp, newapp.name.ToString());
            btnApp.Click += Btn_Click;

            pnl.Controls.Add(lbl);
            pnl.Controls.Add(btnApp);
            btnApp.BringToFront();
            pnl.Layout += new LayoutEventHandler(pnl_Layout);
            pnl.PanelElement.PanelBorder.Visibility = Telerik.WinControls.ElementVisibility.Collapsed; //No le saca el borde 
            pnl.BackColor = Color.Transparent;

            this.panelBtns.Controls.Add(pnl);
            this.panelBtns.PanelElement.PanelBorder.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
        }

        private void pnl_Layout(object sender, LayoutEventArgs e)
        {
            int pnlHeight = 0, pnlWidth = 0;

            for (int i = 0; i < this.panelBtns.Controls.Count; i++)
            {
                Control myControl = this.panelBtns.Controls[i];
                //se cargan 6 apps maximo por linea
                if (i % 6 == 0 && i != 0)
                {
                    pnlWidth = pnlWidth + 100;
                    pnlHeight = 0;
                }
                myControl.Location = new Point(pnlHeight, pnlWidth);
                pnlHeight += myControl.Width + (myControl.Width / 4);
            }
        }

        private bool CB()
        {
            return false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 appId = Int64.Parse(((RadButton)sender).Tag.ToString());
                App currentapp = apps[appId];
                string apptoexecute = currentapp.path;
                CheckClient(apptoexecute);
                MinimizedStart();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);

            }
        }

        public void ExecuteCurrentClient(string Args)
        {
            FUP.ExecuteCurrentClient(Application.StartupPath, Args);
        }

        public bool CheckForNews { get; set; } = true;
        public bool Checking { get; private set; }

        private void CheckClient(string AppToExecute)
        {


            if (!FUP.GetClientToExecute(Application.StartupPath, AppToExecute, string.Empty))
            {
                firstTime = false;
                MinimizedStart();
            }
            else
            {
                this.lblmessage.Text = "Descargando una nueva version de Zamba";

                if (!FUP.FirstTime)
                {
                    this.notifyIcon1.Visible = true;
                }
            }
            this.lblcount.Visible = false;
            MinimizedStart();

        }

        private delegate void dUpdateDownloadError();
        private void DownloadError(object sender, EventArgs e)
        {
            dUpdateDownloadError dUpdate = UpdateDownloadError;
            this.Invoke(dUpdate);
        }
        private void UpdateDownloadError()
        {
            try
            {
                this.lblmessage.Text = "Error al Iniciar por primera vez, contacte al administrador del sistema.";
                System.Threading.Thread.Sleep(10000);
                Application.Exit();
            }
            catch (Exception)
            {
            }
        }



        private delegate void dUpdateStatusWithParam(object[] param);
        private void DownloadInProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                dUpdateStatusWithParam dUpdate = UpdateStatusWithParam;
                this.Invoke(dUpdate, new object[] { e });
            }
            catch (Exception)
            {
            }
        }
        private void UpdateStatusWithParam(object[] param)
        {
            try
            {
                DownloadProgressChangedEventArgs e = (DownloadProgressChangedEventArgs)param[0];

                this.lblmessage.Text = "descargando " + e.BytesReceived / 1024 + "kb de " + e.TotalBytesToReceive / 1024 + "kb";
                this.lblcount.Text = e.ProgressPercentage + "%";
                this.lblmessage.Visible = true;
                this.lblcount.Visible = true;
            }
            catch (Exception)
            {
            }
        }


        private delegate void dUpdateStatus();
        private void DownloadCompleted(object sender, EventArgs e)
        {
            try
            {
                this.CheckForNews = true;
                dUpdateStatus dUpdate1 = UpdateStatus;
                this.Invoke(dUpdate1);
                dUpdateStatus dUpdate2 = ShowCurrentVersion;
                this.Invoke(dUpdate2);

                UpdateShorcutDesktopToClient();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void UpdateShorcutDesktopToClient()
        {
            try
            {
                ShortcutHandler.CreateLink(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "Zamba.lnk"), Path.Combine(FUP.DestinationPath, "compilado\\Cliente.exe"));
            }
            catch (Exception ex)
            {
               // ZClass.raiseerror(ex);
            }
        }

        private void DownloadTryCompleted(object sender, EventArgs e)
        {
            try
            {
                this.CheckForNews = true;
                dUpdateStatus dUpdate1 = UpdateStatusNone;
                this.Invoke(dUpdate1);
                dUpdateStatus dUpdate2 = ShowCurrentVersion;
                this.Invoke(dUpdate2);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateStatus()
        {
            try
            {
                this.lblcount.Visible = false;
                this.lblmessage.Text = "Nueva version de Zamba Instalada!";
            }
            catch (Exception)
            {
            }
        }
        private void UpdateStatusNone()
        {
            try
            {
                this.lblcount.Visible = false;
                this.lblmessage.Text = "";
            }
            catch (Exception)
            {
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MinimizedStart();
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MinimizedStart();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    //hacemos visible el formulario
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    //ocultamos el icono de la bandeja de sistema
                    notifyIcon1.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //hacemos visible el formulario
                this.Show();
                this.WindowState = FormWindowState.Normal;
                //ocultamos el icono de la bandeja de sistema
                notifyIcon1.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void NotifyIconMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (TCheck.TState == ZTimers.ZTState.Run)
                {
                    TCheck.Pause();
                    TCheck.Dispose();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }

            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MenuRegister_Click(object sender, EventArgs e)
        {
            RegisterZamba();
        }

        private void RegisterZamba()
        {
            Register register = new Register();
            try
            {
                if (register.CrearRegZamba() && register.regReportsBuilder())
                {
                    MessageBox.Show("Registracion correcta", "Registro de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar Zamba", "Registro de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                register = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar Zamba: Se denego la clave de acceso al Registro.", "Registro de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.radContextMenu1.Show(this, this.btnTools.Location);

        }

        //Eventos y variables necesarias parque el form sea movible
        private bool mouseDown;
        private Point lastLocation;
        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Start_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Start_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                if (FUP != null)
                    FUP.Dispose(true);

                if (TCheck.TState == ZTimers.ZTState.Run)
                {
                    TCheck.Pause();
                    TCheck.Dispose();
                }

            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception)
            {
            }
            base.OnClosing(e);

        }
    }
}
