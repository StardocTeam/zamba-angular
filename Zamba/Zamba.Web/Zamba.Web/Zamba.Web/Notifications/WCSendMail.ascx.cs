
using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Collections.Generic;
using System.IO;
using Zamba.Services;
using System.Collections;
using Zamba.Membership;

public partial class WCSendMail : System.Web.UI.UserControl
{
    #region "Atributos - Constantes"
    //Maximo tamaño de adjuntos ( 5mb)
    const Int64 MaxAttachsSize = 5120;
    const string Attachslength1 = "Restan ";
    const string Attachslength2 = " KB para archivos adjuntos";
    #endregion
    #region Eventos

    SMail sMail;
    byte[] originalFile = null;
    String originalFileName = null;
    IResult _res;
    bool doMail = false;
    string _body, _subject, _to, _cc, _cco;
    long _docid, _doctype;

    protected void Page_Load(object sender, EventArgs e)
    {



        SResult SResult = new SResult();
        sMail = new SMail();

        if (Zamba.Membership.MembershipHelper.CurrentUser == null )
            FormsAuthentication.RedirectToLoginPage();
        else
        {
            long.TryParse(Request["docid"], out _docid);
            long.TryParse(Request["doctypeid"], out _doctype);

            if (Session["Subject" + _docid] != null && Session["To" + _docid] != null && Session["Body" + _docid] != null && Session["CC" + _docid] != null && Session["CCO" + _docid] != null)
            {
                doMail = true;
                _subject = Session["Subject" + _docid].ToString();
                _to = Session["To" + _docid].ToString();
                _body = Session["Body" + _docid].ToString();
                _cc = Session["CC" + _docid].ToString();
                _cco = Session["CCO" + _docid].ToString();
            }

            try
            {
                _res = SResult.GetResult(_docid, _doctype, true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            if (!Page.IsPostBack)
            {
                if (_res != null)
                {
                    if (doMail)
                    {
                        txtAsunto.Text = _res.Name;
                    }
                    else
                    {
                        txtAsunto.Text = _subject;
                    }
                }

                if (Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail == string.Empty)
                {
                    lblErrors.Text = "No se ha configurado una cuenta de email para este usuario";
                    pnlErrors.Visible = true;
                    DivPrincipal.Visible = false;
                }
                else
                {
                    if (doMail)
                    {
                        txtEmailTo.Text = _to;
                        txtMessageBody.Text = _body;
                        txtCC.Text = _cc;
                        txtCCO.Text = _cco;
                    }
                }
            }
        }

        Session["Subject" + _docid] = null;
        Session["To" + _docid] = null;
        Session["Body" + _docid] = null;
        Session["CC" + _docid] = null;
        Session["CCO" + _docid] = null;
        _subject = null;
        _to = null;
        _body = null;
        _cc = null;
        _cco = null;
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
        string to = txtEmailTo.Text;
        string subject = txtAsunto.Text;
        string cc = txtCC.Text;
        string cco = txtCCO.Text;
        string body = txtMessageBody.Text;
        bool enableSsl = false;
      
        SZOptBusiness ZOptB = new SZOptBusiness();
        List<string> attachs = null;
        SendMailConfig mail = null;
        string UseEmailConfigFromAD = ZOptB.GetValue("UseEmailConfigFromAD");

        try
        {
          

            // esta configurada la opcion de smtp global en Zamba?
            if (ZOptB.GetValue("WebView_SendBySMTP") != null && ZOptB.GetValue("WebView_SendBySMTP") == "true")
            {
                user = ZOptB.GetValue("WebView_UserSMTP");
                pass = ZOptB.GetValue("WebView_PassSMTP");
                from = ZOptB.GetValue("WebView_FromSMTP");
                port = ZOptB.GetValue("WebView_PortSMTP");
                smtp = ZOptB.GetValue("WebView_SMTP");
                bool.TryParse(ZOptB.GetValue("WebView_SslSMTP"), out enableSsl);
            }
            else if (Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type == MailTypes.NetMail)  // el usuario usa smtp?
            {
                user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName;
                pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                enableSsl = Zamba.Membership.MembershipHelper.CurrentUser.eMail.EnableSsl;
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

            if ((to == string.Empty && cc == string.Empty && cco == string.Empty) || subject == string.Empty)
            {
                lblErrors.Text = "Por favor, complete todos los campos.";
            }
            else
            {
                if (chkAddOriginalDocument.Checked)
                {
                    string docfile = Request["docid"] + '.' + Request["docext"];

                    if (!string.IsNullOrEmpty(docfile))
                    {
                        string file = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp", docfile);
                        AddAtachToSession(docfile, file);
                    }
                }

                AttachOriginalDocument();

                //var userId = Session["UserId"].ToString();

                mail = new SendMailConfig();
                if (UseEmailConfigFromAD.ToLower()  == "true")
                {
                    mail.MailType = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type;
                    mail.From = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                    mail.UserName = mail.From;
                    mail.Password = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                    mail.SMTPServer = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                    mail.Port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                }
                else
                {
                    mail.MailType = MailTypes.NetMail;
                    mail.From = from;
                    mail.UserName = user;
                    mail.Password = pass;
                    mail.SMTPServer = smtp;
                    mail.Port = port;
                }
                mail.MailTo = to;
                mail.Cc = cc;
                mail.Cco = cco;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.AttachFileNames = attachs;
                mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                mail.OriginalDocument = originalFile;
                mail.OriginalDocumentFileName = originalFileName;
                mail.EnableSsl = enableSsl;
                mail.SourceDocId = _res.ID;
                mail.SourceDocTypeId = _res.DocTypeId;
                mail.LinkToZamba = chkAddWebLink.Checked;
                mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                if (sMail.SendMail(mail))
                {

                   // DivPrincipal.Visible = false;
                    lblErrors.Text = "El mail ha sido enviado con exito";
                    Session["Attachs"] = null;
                    Response.Write("<script type=\"text/javascript\">window.close();</script>");
                }
                else
                {
                   // DivPrincipal.Visible = false;
                    lblErrors.Text = "Se produjo un error al enviar mail. Por favor verifique los datos de conexion";
                }
            }
            pnlErrors.Visible = true;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            lblErrors.Text = "Se produjo un error al enviar mail. Por favor verifique los datos de conexion";
            pnlErrors.Visible = true;
        }
        finally
        {
            ZOptB = null;
            if (attachs != null)
            { 
                attachs.Clear();
                attachs = null;
            }
            if(mail != null)
            {
                mail.Dispose();
                mail = null;
            }
        }
    }

    /// <summary>
    /// evento click del boton de cancelar, vacia la session y oculta gran parte del control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    //protected void btnCancelMail_Click(object sender, EventArgs e)
    //{
    //    Session["Attachs"] = null;
    //    DivPrincipal.Visible = false;
    //    lblErrors.Text = "Envio de mail cancelado";
    //    pnlErrors.Visible = true;
    //}

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

        Int64 postedlength = (FileUpload1.PostedFile.ContentLength / 1024);
        Int64 attachslength = GetAttachsSizelength();

        if ((MaxAttachsSize - (postedlength + attachslength)) < 0)
        {
            lblAttachslength.Text = "No se pudo adjuntar el archivo por ser de tamaño superior al limite";
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
            attachs = new Dictionary<string, string> { { filename, file } };
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

            foreach (string filepath in attachs.Keys)
            {
                if (!File.Exists(attachs[filepath])) continue;
                file = new FileInfo(attachs[filepath]);
                //dt.Rows.Add(Results_Business.GetFileIcon(attachs[filepath]), filepath, attachs[filepath], (file.Length / 1024) + "KB");
                dt.Rows.Add("", filepath, attachs[filepath], (file.Length / 1024) + "KB");
                attachsSize += (file.Length / 1024);
                GC.Collect();
                GC.Collect();
            }

            if (dt.Rows.Count == 0)
            {
                gvAttachs.DataSource = null;
                gvAttachs.DataBind();
                lblAttachslength.Text = Attachslength1 + MaxAttachsSize + Attachslength2;
                return;
            }

            gvAttachs.ShowHeader = false;
            gvAttachs.Columns.Clear();

            ImageField imagenDoc = new ImageField { ShowHeader = true, HeaderText = "Icono" };
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

            BoundField g = new BoundField { DataField = "Tamaño", ShowHeader = true, HeaderText = "Tamaño" };
            gvAttachs.Columns.Add(g);
            lblAttachslength.Text = Attachslength1 + (MaxAttachsSize - attachsSize) + Attachslength2;
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
        List<string> fullpaths = new List<string>();

        //Si no se adjunta link de documento original, adjuntar el documento
        if (!chkAddWebLink.Checked)
        {
            //ver cuando el volumen es -2
            if (!string.IsNullOrEmpty(_res.FullPath))
                fullpaths.Add(_res.FullPath);
        }

        if (attachs == null)
            return fullpaths;

        fullpaths.AddRange(attachs.Values);
        return fullpaths;
    }


    //Recupera el documento original desde la base o el archivo fisico, para luego usarlo como adjunto en el envio de mail.
    private void AttachOriginalDocument()
    {
        SZOptBusiness Zopt = new SZOptBusiness();
        if (!chkAddWebLink.Checked)
        {
            try
            {
                SResult sResult = new SResult();
                Result res = (Result)sResult.GetResult(_res.ID, _res.DocTypeId, true);

                if (res != null)
                {
                    byte[] file;
                    ZOptBusiness zopt = new ZOptBusiness();
                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 &&
                        (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase ||
                        (!String.IsNullOrEmpty(zopt.GetValue("ForceBlob")) && bool.Parse(zopt.GetValue("ForceBlob")))))
                    {
                        sResult.LoadFileFromDB(res);
                    }

                    //Verifica si el result contiene el documento guardado
                    if (res.EncodedFile != null)
                    {
                        file = res.EncodedFile;
                    }
                    else
                    {
                        string sUseWebService = zopt.GetValue("UseWebService");

                        //Verifica si debe utilizar el webservice para obtener el documento
                        if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                            file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        else
                        {
                            Boolean isBlob = false;
                            file = sResult.GetFileFromResultForWeb(res, out isBlob);
                        }
                    }

                    //Verifica la existencia del documento buscado 
                    if (file != null && file.Length > 0)
                    {
                        originalFile = file;
                        originalFileName = _res.Doc_File;
                    }
                    Zopt = null;
                    sResult = null;
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }
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
        pnlErrors.Visible = false;
    }

    /// <summary>
    /// Obtiene el tamaño en kilobytes de los archivos ya adjuntos
    /// </summary>
    /// <returns></returns>
    /// <history>Diego 18-09-2008 Created</history>
    private Int64 GetAttachsSizelength()
    {
        string filesizes = lblAttachslength.Text.Replace(Attachslength1, string.Empty);
        filesizes = filesizes.Replace(Attachslength2, string.Empty);
        try
        {
            return MaxAttachsSize - (Int64.Parse(filesizes));
        }
        catch
        {
            LoadGridview();
            filesizes = lblAttachslength.Text.Replace(Attachslength1, string.Empty);
            filesizes = filesizes.Replace(Attachslength2, string.Empty);
            return MaxAttachsSize - (Int64.Parse(filesizes));
        }
    }
    #endregion
}
