using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Collections.Generic;
using System.IO;
using Zamba.Services;

namespace Controls.Notifications
{
    public partial class Controls_WCSendMail : System.Web.UI.UserControl
    {
        #region "Atributos - Constantes"

        //Maximo tamaño de adjuntos ( 5mb)
        const Int64 MaxAttachsSize = 5120;
        const string Attachslenght1 = "Restan ";
        const string Attachslenght2 = " KB para archivos adjuntos";
        
        IUser _user;
        Result _res;
        long _docid, _doctype;
        SMail smail;
        SResult sresults;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            long userid = 0;

            if (Session["UserId"] != null)
                long.TryParse(Session["UserId"].ToString(), out userid);

            _user = (IUser)Session["User"];
        
            if (Session["UserId"] == null || _user == null)
                FormsAuthentication.RedirectToLoginPage();
            else
            {
                long.TryParse(Request["docid"], out _docid);
                long.TryParse(Request["doctype"], out _doctype);

                try
                {
                    if (sresults == null)
                        sresults = new SResult();

                    _res = (Result)sresults.GetResult(_docid, _doctype);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                if (!Page.IsPostBack)
                {
                    if (_res != null)
                        txtAsunto.Text = _res.Name;

                    if (_user.eMail.Mail == string.Empty)
                    {
                        lblErrors.Text = "No se ha configurado una cuenta de email para este usuario";
                        lblErrors.Visible = true;
                        DivPrincipal.Visible = false;
                    }
                }
            }
        }
        
        /// <summary>
        /// Evento click para envio de mail, se enviara el mail y en caso satisfactorio se eliminan los archivos temporales
        /// y se oculta gran parte del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>Diego 18-09-2008 Created</history>
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string user = string.Empty;
            string pass = string.Empty;
            string from = string.Empty;
            string port = string.Empty;
            string smtp = string.Empty;
            bool enableSsl = false;
            long userid = long.Parse(Session["UserId"].ToString());
            string to = txtEmailTo.Text;
            string subject = txtAsunto.Text;
            string body = txtMessageBody.Text;

            // esta configurada la opcion de smtp global en Zamba?
            ZOptBusiness zopt = new ZOptBusiness();
            if (zopt.GetValue("WebView_SendBySMTP") != null)
            {
                user = zopt.GetValue("WebView_UserSMTP");
                pass = zopt.GetValue("WebView_PassSMTP");
                from = zopt.GetValue("WebView_FromSMTP");
                port = zopt.GetValue("WebView_PortSMTP");
                smtp = zopt.GetValue("WebView_SMTP");
                bool.TryParse(zopt.GetValue("WebView_SslSMTP"), out enableSsl);
            }
            else if (_user.eMail.Type == MailTypes.NetMail)  // el usuario usa smtp?
            {
                user = _user.eMail.UserName;
                pass = _user.eMail.Password;
                from = _user.eMail.Mail;
                port = _user.eMail.Puerto.ToString();
                smtp = _user.eMail.ProveedorSMTP;
                enableSsl = _user.eMail.EnableSsl;
            }
            else if (ConfigurationManager.AppSettings["WebView_SendBySMTP"] != string.Empty) // usar config smtp definida en la aplicacion?
            {
                user = ConfigurationManager.AppSettings["WebView_UserSMTP"];
                pass = ConfigurationManager.AppSettings["WebView_PassSMTP"];
                from = ConfigurationManager.AppSettings["WebView_FromSMTP"];
                port = ConfigurationManager.AppSettings["WebView_PortSMTP"];
                smtp = ConfigurationManager.AppSettings["WebView_SMTP"];
                bool.TryParse(ConfigurationManager.AppSettings["WebView_SslSMTP"], out enableSsl);
            }

            if (to == string.Empty || subject == string.Empty || body == string.Empty)
            {
                lblErrors.Text = "Por favor, complete todos los campos.";
            }
            else
            {
                if (chkAddOriginalDocument.Checked)
                {
                    string docfile = Request["docid"];

                    if (!string.IsNullOrEmpty(docfile))
                    {
                        string file = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp", docfile);
                        AddAtachToSession(docfile, file);
                    }
                }

                if (smail == null)
                    smail = new SMail();

                using (var mail = new SendMailConfig())
                {
                    mail.MailType = MailTypes.NetMail;
                    mail.SMTPServer = smtp;
                    mail.From = from;
                    mail.Port = port;
                    mail.UserName = user;
                    mail.Password = pass;
                    mail.MailTo = to;
                    mail.Cc = string.Empty;
                    mail.Cco = string.Empty;
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.AttachFileNames = GetAttachs();
                    mail.UserId = userid;
                    mail.ImagesToEmbedPaths = null;
                    mail.OriginalDocument = null;
                    mail.OriginalDocumentFileName = null;
                    mail.EnableSsl = enableSsl;
                    mail.SourceDocId = _docid;
                    mail.SourceDocTypeId = _doctype;
                    mail.LinkToZamba = chkAddWebLink.Checked;
                    mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                    if (smail.SendMail(mail))
                    {
                        DivPrincipal.Visible = false;
                        lblErrors.Text = "El mail ha sido enviado con exito";
                    }
                    else
                    {
                        DivPrincipal.Visible = false;
                        lblErrors.Text = "Se produjo un error al enviar mail. Por favor verifique los datos de conexion";
                    }
                }
            }
         
            lblErrors.Visible = true;
            zopt = null;
        }

        /// <summary>
        /// evento click del boton de cancelar, vacia la session y oculta gran parte del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>Diego 18-09-2008 Created</history>
        protected void btnCancelMail_Click(object sender, EventArgs e)
        {
            Session["Attachs"] = null;
            DivPrincipal.Visible = false;
            lblErrors.Text = "Envio de mail cancelado";
            lblErrors.Visible = true;
        }

        /// <summary>
        /// Evento click del boton de Agregar adjunto,se genera una copia al servidor del archivo cliente, 
        /// se suma a la coleccion y se llama al metodo que carga la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>Diego 18-09-2008 Created</history>
        protected void btnAddAttach_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false)
                return;

            Int64 postedLenght = (FileUpload1.PostedFile.ContentLength / 1024);
            Int64 attachsLenght = GetAttachsSizeLenght();

            if ((MaxAttachsSize - (postedLenght + attachsLenght)) < 0)
            {
                lblAttachsLenght.Text = "No se pudo adjuntar el archivo por ser de tamaño superior al limite";
                return;
            }
            string pathTemp = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp";

            if (Directory.Exists(pathTemp) == false)
                Directory.CreateDirectory(pathTemp);

            //Genera el nombre del archivo
            string file = Path.Combine(pathTemp, FileUpload1.FileName);
           
            //Sube el archivo
            try
            {
                FileUpload1.SaveAs(file);
            }
            catch (IOException ex)
            {
                ZClass.raiseerror(ex);
            }

            AddAtachToSession(FileUpload1.FileName, file);

            LoadGridview();
        }

        private void AddAtachToSession(string filename, string file)
        {
            Dictionary<string, string> attachs;

            if (Session["Attachs"] != null)
            {
                attachs = (Dictionary<string, string>)Session["Attachs"];
                
                if (!attachs.ContainsKey(filename))
                    attachs.Add(filename, file);
            }
            else
            {
                attachs = new Dictionary<string, string> {{filename, file}};
            }

            Session["Attachs"] = attachs;
        }

        /// <summary>
        /// Evento de seleccion en grilla de adjuntos, se utiliza para eliminar un adjunto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>Diego 18-09-2008 Created</history>
        protected void gvAttachs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (null == Session["Attachs"])
                return;

            string filename = ((GridView)(sender)).DataKeys[e.NewSelectedIndex].Value.ToString();
            Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];
            attachs.Remove(filename);
            LoadGridview();
        }    
        #endregion

        #region Metodos
        /// <summary>
        /// Limpia los adjuntos de la grilla y de session
        /// </summary>
        /// <history>Diego 18-09-2008 Created</history>
        public void ClearAttachments()
        {
            Session["Attachs"] = null;
            gvAttachs.DataSource = null;
            gvAttachs.DataBind();
        }

        /// <summary>
        /// Agrega un adjunto en la grilla, utilizado para inicializar el control con el documento que lo llama
        /// </summary>
        /// <param name="filename">Nombre de archivo, con extension</param>
        /// <param name="fullpath">ruta para localizar el archivo en disco</param>
        /// <history>Diego 18-09-2008 Created</history>
        public void AddAttach(string filename, string fullpath)
        {
            if (File.Exists(fullpath) == false)
                return;

            string pathTemp = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp";
            string file = Path.Combine(pathTemp, filename);

            try
            {
                if (File.Exists(file))
                    File.Delete(file);

                File.Copy(fullpath, file);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            AddAtachToSession(filename, file);

            LoadGridview();
        }

        /// <summary>
        /// Realiza la carga de la grilla de adjuntos
        /// </summary>
        /// <history>Diego 18-09-2008 Created</history>
        private void LoadGridview()
        {
            FileInfo file;

            try
            {
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("Icono");
                DataColumn dc2 = new DataColumn("Nombre de Adjunto");
                DataColumn dc3 = new DataColumn("AttachPath");
                DataColumn dc4 = new DataColumn("Tamaño");
                Int64 attachsSize = 0;
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];

                if (sresults == null)
                {
                    sresults = new SResult();
                }

                foreach (string filepath in attachs.Keys)
                {
                    if (!File.Exists(attachs[filepath])) continue;
                    file = new FileInfo(attachs[filepath]);
                    dt.Rows.Add(sresults.GetFileIcon(attachs[filepath]), filepath, attachs[filepath], (file.Length / 1024) + "KB");
                    attachsSize += (file.Length / 1024);
                    //GC.Collect();
                }

                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    gvAttachs.DataSource = null;
                    gvAttachs.DataBind();
                    lblAttachsLenght.Text = Attachslenght1 + MaxAttachsSize + Attachslenght2;
                    return;
                }

                gvAttachs.ShowHeader = false;
                gvAttachs.Columns.Clear();
                
                ImageField imagenDoc = new ImageField {ShowHeader = true, HeaderText = "Icono"};
                imagenDoc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                imagenDoc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                imagenDoc.DataImageUrlField = "Icono";
                imagenDoc.DataImageUrlFormatString = "../../icono.aspx?id={0}";
                gvAttachs.Columns.Add(imagenDoc);

                ButtonField btnDelete = new ButtonField
                                            {
                                                HeaderText = "Eliminar",
                                                ShowHeader = true,
                                                DataTextField = "Nombre de Adjunto"
                                            };

                btnDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                btnDelete.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                btnDelete.CommandName = "Select";
                gvAttachs.Columns.Add(btnDelete);

                BoundField f = new BoundField
                                    {
                                       DataField = "AttachPath",
                                       ShowHeader = true,
                                       HeaderText = "AttachPath",
                                       Visible = false
                                   };

                gvAttachs.Columns.Add(f);
                gvAttachs.DataKeyNames = new[] { "Nombre de Adjunto" };

                BoundField g = new BoundField {DataField = "Tamaño", ShowHeader = true, HeaderText = "Tamaño"};
                gvAttachs.Columns.Add(g);          
                lblAttachsLenght.Text = Attachslenght1 + (MaxAttachsSize - attachsSize) + Attachslenght2;
                gvAttachs.DataSource = dt;
                gvAttachs.DataBind();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// retorna una lista con los adjuntos, utilizado para el envio de mail
        /// </summary>
        /// <returns></returns>
        /// <history>Diego 18-09-2008 Created</history>
        private List<string> GetAttachs()
        {
            Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];

            if (attachs == null)
                return null;

            List<string> fullpaths = new List<string>();
            fullpaths.AddRange(attachs.Values);
            return fullpaths;
        }

        /// <summary>
        /// Inicializa el control
        /// </summary>
        /// <history>Diego 18-09-2008 Created</history>
        public void SetInitialState()
        {
            txtAsunto.Text = "Mail enviado desde Zamba.Web";
            txtEmailTo.Text = string.Empty;
            txtMessageBody.Text = string.Empty;
            DivPrincipal.Visible = true;
            lblErrors.Visible = false;
        }

        /// <summary>
        /// Obtiene el tamaño en kilobytes de los archivos ya adjuntos
        /// </summary>
        /// <returns></returns>
        /// <history>Diego 18-09-2008 Created</history>
        private Int64 GetAttachsSizeLenght()
        {
            string filesizes = lblAttachsLenght.Text.Replace(Attachslenght1, string.Empty);
            filesizes = filesizes.Replace(Attachslenght2, string.Empty);
            try
            {
                return MaxAttachsSize - (Int64.Parse(filesizes));
            }
            catch
            {
                LoadGridview();
                filesizes = lblAttachsLenght.Text.Replace(Attachslenght1, string.Empty);
                filesizes = filesizes.Replace(Attachslenght2, string.Empty);
                return MaxAttachsSize - (Int64.Parse(filesizes));
            }
        }
        #endregion
    }
}