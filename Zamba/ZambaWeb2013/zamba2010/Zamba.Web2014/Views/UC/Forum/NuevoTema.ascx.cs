using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using Zamba.Services;
using Zamba.Core;
using System.Web.Security;

namespace Controls.Forum
{
    public delegate void Added();
    public delegate void Closed();

    public partial class ControlsForumWebUserControl : System.Web.UI.UserControl
    {
        #region "Atributos y propiedades"
        const string Attachslenght1 = "Restan ";
        const string Attachslenght2 = " KB para archivos adjuntos";
        private bool _sendNotification;
        private IUser _user = null;
        SForum sforum;
        SRights srights;
        SMail smail;
        SResult sresult;
        private Added _dAdd = null;
        private Closed _dClose = null;
        public event Added OnAdd
        {
            add
            {
                _dAdd += value;
            }
            remove
            {
                _dAdd -= value;
            }
        }
        public event Closed OnClosed
        {
            add
            {
                _dClose += value;
            }
            remove
            {
                _dClose -= value;
            }
        }

        public string Subject
        {
            set { txtAsunto.Text = value; }
        }
        public string Message
        {
            set { txtMensaje.Text = value; }
        }
        public Int64 SourceDocTypeId
        {
            private get { return Int64.Parse(hdnSourceDocTypeId.Value); }
            set { hdnSourceDocTypeId.Value = value.ToString(); }
        }
        public Int64 SourceDocId
        {
            private get { return Int64.Parse(hdnSourceDocId.Value); }
            set { hdnSourceDocId.Value = value.ToString(); }
        }
        public Int32 SourceMessageId
        {
            private get
            {
                if (String.IsNullOrEmpty(hdnSourceMessageId.Value))
                {
                    hdnSourceMessageId.Value = "0";
                    return 0;
                }
                else
                {
                    return Int32.Parse(hdnSourceMessageId.Value);
                }
            }
            set { hdnSourceMessageId.Value = value.ToString(); }
        }
        public Int32 ParentMessageId
        {
            private get
            {
                if (String.IsNullOrEmpty(hdnParentMessageId.Value))
                {
                    hdnParentMessageId.Value = "0";
                    return 0;
                }
                else
                {
                    return Int32.Parse(hdnParentMessageId.Value);
                }
            }
            set { hdnParentMessageId.Value = value.ToString(); }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            _user = (IUser)Session["User"];

            if (null == Session["UserId"] || _user == null)
                FormsAuthentication.RedirectToLoginPage();

            ZOptBusiness zopt = new ZOptBusiness();

            if (bool.Parse(zopt.GetValue("ShowForumAttachsTab")) == false)
            {
                Div5.Visible = false;
            }
            zopt = null;
        }

        public void LoadParticipants()
        {
            if (lstGroupAsig.Items.Count > 0 || lstUserAsig.Items.Count > 0)
                return;

            //Cargo todos los usuarios
            List<User> TempUserList = UserBusiness.GetOnlyUsersNames();

            //Cargo todos los grupos
            List<UserGroup> TempGroupList = UserGroupBusiness.GetUserGroups();

            SForum sforum = new SForum();

            List<Int64> idRecipes;

            if (SourceMessageId > 0)
                idRecipes = sforum.GetUserAndGroupsParticipantsIdAsList(SourceMessageId);
            else
                idRecipes = new List<Int64>();

            //Esta parte de filtrar los usuarios para que no muestre los que estan con nombres vacios es una crotada
            //pero funciona para las base de datos desactualizadas que permitian usuarios sin nombre y apellido.
            //El alta de usuario fue modificado para que sea imposible un user sin nombre ni apellido
            //[Andres] 20/03/2007
            for (Int32 i = 0; i < TempUserList.Count; i++)
                if (String.IsNullOrEmpty(TempUserList[i].Description.Trim()))
                {
                    TempUserList[i].Description = TempUserList[i].Apellidos + " " + TempUserList[i].Nombres;
                    if (String.IsNullOrEmpty(TempUserList[i].Description.Trim()) == false)
                    {
                        if (idRecipes.Contains(TempUserList[i].ID))
                            lstUserAsig.Items.Add(new ListItem(TempUserList[i].Description, TempUserList[i].ID.ToString()));
                        else
                            lstUserNotAsig.Items.Add(new ListItem(TempUserList[i].Description, TempUserList[i].ID.ToString()));
                    }
                }
                else
                {
                    if (idRecipes.Contains(TempUserList[i].ID))
                        lstUserAsig.Items.Add(new ListItem(TempUserList[i].Description, TempUserList[i].ID.ToString()));
                    else
                        lstUserNotAsig.Items.Add(new ListItem(TempUserList[i].Description, TempUserList[i].ID.ToString()));
                }

            for (Int32 i = 0; i < TempGroupList.Count; i++)
                if (String.IsNullOrEmpty(TempGroupList[i].Description.Trim()))
                {
                    if (idRecipes.Contains(TempGroupList[i].ID))
                        lstGroupAsig.Items.Add(new ListItem(TempGroupList[i].Name, TempGroupList[i].ID.ToString()));
                    else
                        lstGroupNotAsig.Items.Add(new ListItem(TempGroupList[i].Name, TempGroupList[i].ID.ToString()));
                }
                else
                {
                    if (idRecipes.Contains(TempGroupList[i].ID))
                        lstGroupAsig.Items.Add(new ListItem(TempGroupList[i].Description, TempGroupList[i].ID.ToString()));
                    else
                        lstGroupNotAsig.Items.Add(new ListItem(TempGroupList[i].Description, TempGroupList[i].ID.ToString()));
                }
        }

        public void CreateForum()
        {
            //btnNotificarGuardar.Visible = false;
            btnGuardarMensaje.Text = "Crear";
            txtAsunto.Enabled = true;
        }

        public void ResponseForum()
        {
            //btnNotificarGuardar.Visible = true;
            btnGuardarMensaje.Text = "Responder";
            txtAsunto.Enabled = false;
            //txtAsunto.Text = "Re: ("+DateTime.Now.ToShortDateString() +" "+_user.Name+")";
        }

        protected void btnParticipantes_Click(object sender, EventArgs e)
        {
            divForo.Visible = false;
            divParticipantes.Visible = true;
            LoadParticipants();
        }

        protected void btnAddPart_Click(object sender, EventArgs e)
        {
            if (lstUserNotAsig.SelectedIndex > -1)
            {
                lstUserAsig.Items.Add(lstUserNotAsig.SelectedItem);
                //NotifyBusiness.SetNewUserToNotify(GroupToNotifyTypes.Foro, SourceDocId, Int64.Parse(lstUserNotAsig.SelectedValue));
                lstUserNotAsig.Items.Remove(lstUserNotAsig.SelectedItem);
                lstUserAsig.SelectedIndex = -1;

            }
            else if (lstGroupNotAsig.SelectedIndex > -1)
            {
                lstGroupAsig.Items.Add(lstGroupNotAsig.SelectedItem);
                //NotifyBusiness.SetNewUserGroupToNotify(GroupToNotifyTypes.Foro,SourceDocId, Int64.Parse(lstGroupNotAsig.SelectedValue));
                lstGroupNotAsig.Items.Remove(lstGroupNotAsig.SelectedItem);
                lstGroupAsig.SelectedIndex = -1;
            }
        }

        protected void btnRemPart_Click(object sender, EventArgs e)
        {
            if (lstUserAsig.SelectedIndex > -1)
            {
                lstUserNotAsig.Items.Add(lstUserAsig.SelectedItem);
                //NotifyBusiness.DeleteUserToNotify(SourceDocId, Int64.Parse(lstUserAsig.SelectedValue));
                lstUserAsig.Items.Remove(lstUserAsig.SelectedItem);
                lstUserNotAsig.SelectedIndex = -1;
            }
            else if (lstGroupAsig.SelectedIndex > -1)
            {
                lstGroupNotAsig.Items.Add(lstGroupAsig.SelectedItem);
                //NotifyBusiness.DeleteUserGroupToNotify(SourceDocId, Int64.Parse(lstGroupAsig.SelectedValue));
                lstGroupAsig.Items.Remove(lstGroupAsig.SelectedItem);
                lstGroupNotAsig.SelectedIndex = -1;
            }
        }

        protected void btnClosePart_Click(object sender, EventArgs e)
        {
            divForo.Visible = true;
            divParticipantes.Visible = false;
        }

        protected void btnGuardarMensaje_Click(object sender, EventArgs e)
        {
            string asunto = txtAsunto.Text;
            string texto = txtMensaje.Text;

            if (String.IsNullOrEmpty(txtMensaje.Text))
                txtMensaje.Text = " ";

            InsertarNuevoMensaje(asunto, texto);

            if (_dAdd != null)
                _dAdd();
        }

        private void InsertarNuevoMensaje(string asunto, string texto)
        {
            bool showPopUp = false;

            ZOptBusiness zopt = new ZOptBusiness();
            if (bool.Parse(zopt.GetValue("ShowForumAttachsTab")) == false)
            {
                Div5.Visible = false;
            }
            zopt = null;
            string asuntoRes = asunto; //+ "-" ;
            Int32 idMsg = CoreBusiness.GetNewID(IdTypes.ForumMessage);
            Int32 parentId = ParentMessageId;
            Int64 resultId = SourceDocId;
            Int64 userId = Int64.Parse(Session["UserId"].ToString());
            var attachs = (Dictionary<string, string>)Session["Attachs"];
            var resultsIds = new List<long> { resultId };

            //Guarda la respuesta.
            if (sforum == null)
                sforum = new SForum();

            sforum.InsertMessage(idMsg, parentId, asuntoRes, texto, (Int32)userId);

            //Se llama a la sobrecarga de la colección de results y no al de un solo docId
            //porque es el que se encuentra funcionando en Zamba.
            if (smail == null)
                smail = new SMail();

            if (SourceMessageId == 0)
                SourceMessageId = idMsg;

            List<long> notifyIds = new List<long>();//sforum.GetUserAndGroupsParticipantsIdAsList(SourceMessageId);//smail.getAllSelectedUsers(GroupToNotifyTypes.Foro, resultsIds);

            for (Int32 i = 0; i < lstUserAsig.Items.Count; i++)
                notifyIds.Add(Int64.Parse(lstUserAsig.Items[i].Value));

            for (Int32 i = 0; i < lstGroupAsig.Items.Count; i++)
                notifyIds.Add(Int64.Parse(lstGroupAsig.Items[i].Value));

            lstUserAsig.Items.Clear();
            lstGroupAsig.Items.Clear();

            sforum.RemoveMessageParticipants(SourceMessageId);
            //Verifica si se debe notificar o no.
            if (notifyIds.Count < 1)
            {
                sforum.InsertMessageParticipant(SourceMessageId, userId);
            }
            else
            {
                if (!notifyIds.Contains(userId))
                {
                    notifyIds.Add(userId);
                }
                sforum.InsertMessageParticipants(SourceMessageId, notifyIds);
            }

            //Asocia el mensaje al documento.
            sforum.InsertMessageDoc(idMsg, resultId, SourceDocTypeId);

            //Verifica si debe insertar la respuesta en otros documentos.
            DataTable relatedDocs = sforum.GetRealtedDocs(parentId, resultId);
            for (int i = 0; i < relatedDocs.Rows.Count; i++)
            {
                sforum.InsertMessageDoc(idMsg, (long)relatedDocs.Rows[i].ItemArray[0], (long)relatedDocs.Rows[i].ItemArray[1]);
            }

            //Se insertan los adjuntos.
            CreateAttachs(idMsg, attachs);

            if (srights == null)
                srights = new SRights();

            srights.SaveActionWebView(resultId, ObjectTypes.Foro, RightsType.ResponderGuardar, idMsg.ToString());

            //Notificación del mail
            if (_sendNotification)
            {
                try
                {
                    int msgID = int.Parse(hdnSourceMessageId.Value);
                    string to = GetParticipantsMails(msgID);
                    if (!string.IsNullOrEmpty(to)) showPopUp = SendMail(msgID, resultId, to);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    lblErrors.Visible = true;
                    lblErrors.Text = "Ha ocurrido un error al enviar el mail.";
                    showPopUp = true;
                }
            }


            if (showPopUp)
                ShowScreenMessage(lblErrors.Text);

            CloseForm();
        }

        private void ShowScreenMessage(string msj)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("Message"))
            {
                string script = "<script>$(document).ready(function() { alert('" + msj + "') });</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", script);
            }
        }

        protected void btnNotificarGuardar_Click(object sender, EventArgs e)
        {
            _sendNotification = true;
            btnGuardarMensaje_Click(sender, e);
        }

        private bool SendMail(int MsgID, Int64 docid, string to)
        {
            string user = string.Empty;
            string pass = string.Empty;
            string from = string.Empty;
            string port = string.Empty;
            string smtp = string.Empty;
            bool enableSsl = false;
            long userId = long.Parse(Session["UserId"].ToString());

            List<string> attachs = GetAttachs();

            string subject = txtAsunto.Text;
            string body = MakeMailBody(docid);

            ZOptBusiness zopt = new ZOptBusiness();
            // esta configurada la opcion de smtp global en Zamba?
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
            else if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["WebView_SendBySMTP"])) // usar config smtp definida en la aplicacion?
            {
                user = ConfigurationManager.AppSettings["WebView_UserSMTP"];
                pass = ConfigurationManager.AppSettings["WebView_PassSMTP"];
                from = ConfigurationManager.AppSettings["WebView_FromSMTP"];
                port = ConfigurationManager.AppSettings["WebView_PortSMTP"];
                smtp = ConfigurationManager.AppSettings["WebView_SMTP"];
                bool.TryParse(ConfigurationManager.AppSettings["WebView_SslSMTP"], out enableSsl);
            }
            zopt = null;

            if (String.IsNullOrEmpty(subject) || String.IsNullOrEmpty(body))
            {
                lblErrors.Visible = true;
                lblErrors.Text = "Por favor, complete el asunto y el texto de mensaje.";
                return true;
            }

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
                mail.AttachFileNames = attachs;
                mail.UserId = userId;
                mail.ImagesToEmbedPaths = null;
                mail.OriginalDocument = null;
                mail.OriginalDocumentFileName = null;
                mail.EnableSsl = enableSsl;
                mail.SourceDocId = SourceDocId;
                mail.SourceDocTypeId = SourceDocTypeId;
                mail.LinkToZamba = chkAddWebLink.Checked;
                mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                if (smail == null)
                    smail = new SMail();

                if (smail.SendMail(mail))
                {
                    return false;
                }
                else
                {
                    lblErrors.Visible = true;
                    lblErrors.Text = "Error al enviar el mail. Verifique los datos de conexión";
                    return true;
                }
            }
        }

        //Genera un cuerpo para el mail como el cliente de Windows
        private string MakeMailBody(Int64 docid)
        {
            StringBuilder sbBody = new StringBuilder();

            sbBody.Append("El usuario ");
            sbBody.Append(_user.Name);
            sbBody.Append('(');
            sbBody.Append(_user.Nombres);
            sbBody.Append(' ');
            sbBody.Append(_user.Apellidos);
            sbBody.Append(") ");
            sbBody.Append("<br/>Ha agregado/respondido una conversación del foro con asunto: ");
            sbBody.Append(txtAsunto.Text);

            Int64 docId = SourceDocId;
            Int64 docTypeId = SourceDocTypeId;

            string name = Results_Business.GetName(docId, docTypeId);
            if (!string.IsNullOrEmpty(name))
            {
                sbBody.Append("<br/><br/>Referente a: ");
                sbBody.Append(name);
            }

            sbBody.AppendLine("<br/><br/>Con el siguiente detalle:<br/>");
            sbBody.Append(txtMensaje.Text);

            return sbBody.ToString();
        }

        private string GetParticipantsMails(int MsgID)
        {
            try
            {
                StringBuilder sbMails = new StringBuilder();
                List<long> notifyIds = ZForoBusiness.GetUserAndGroupsParticipantsId(MsgID);
                //Si el usuario con el cual estoy respondiendo/creando el foro esta dentro de los que notificamos
                //lo remuevo.
                if (notifyIds.Contains(_user.ID))
                {
                    notifyIds.Remove(_user.ID);
                }
                char separator = ';';
                IUser userMail;
                List<IUser> users;

                SUsers Users = new SUsers();

                if (notifyIds != null && notifyIds.Count > 0)
                {
                    int max = notifyIds.Count;
                    int maxInUsers;
                    for (int i = 0; i < max; i++)
                    {
                        userMail = Users.GetUser((int)notifyIds[i]);
                        if (userMail != null && userMail.eMail.Mail != null && !sbMails.ToString().Contains(userMail.eMail.Mail))
                        {
                            sbMails.Append(userMail.eMail.Mail);
                            sbMails.Append(separator);
                        }
                        else
                        {
                            users = UserGroupBusiness.GetUsersByGroup(notifyIds[i]);
                            if (users != null && users.Count > 0)
                            {
                                maxInUsers = users.Count;
                                for (int j = 0; j < maxInUsers; j++)
                                {
                                    if (users[j].eMail.Mail != null)
                                    {
                                        sbMails.Append(users[j].eMail.Mail);
                                        sbMails.Append(separator);
                                    }
                                }
                            }
                        }
                    }
                }

                return sbMails.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            //Llama al delegado que se encarga de ocultar el mensaje.
            if (_dClose != null)
                _dClose();
            else
                Visible = false;
        }

        public void SetErrorText(string message)
        {
            lblErrors.Text = message;
        }

        #region "Adjuntos"
        ///<summary>
        ///Evento click del boton de Agregar adjunto,se genera una copia al servidor del archivo cliente, 
        ///se suma a la coleccion y se llama al metodo que carga la grilla
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        ///<history>Diego 18-09-2008 Created</history>
        protected void btnAddAttach_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false)
            {
                if (FileUpload1.FileName.Length > 0)
                {
                    this.lblErrors.Text = "El tamaño del archivo debe superar los cero kilobytes (0kb)";
                }
                else
                {
                    this.lblErrors.Text = "Por favor, seleccione un archivo y luego presione el boton Adjuntar";
                }
            }
            else
            {

                Int64 PostedLenght = (FileUpload1.PostedFile.ContentLength / 1024);
                Int64 attachsLenght = GetAttachsSizeLenght();
                if ((MaxAttachsSize() - (PostedLenght + attachsLenght)) < 0)
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
                    throw ex;
                }

                string filename = FileUpload1.FileName;

                Dictionary<string, string> attachs = null;
                if (Session["Attachs"] != null)
                {
                    attachs = (Dictionary<string, string>)Session["Attachs"];
                    if (attachs.ContainsKey(filename) == false)
                    {
                        attachs.Add(filename, filename);
                    }
                }
                else
                {
                    attachs = new Dictionary<string, string> { { filename, filename } };
                }
                Session["Attachs"] = attachs;

                LoadGridview();
            }
        }

        /// <summary>
        /// Crea los adjuntos.
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <param name="attachs"></param>
        private void CreateAttachs(int idMensaje, Dictionary<string, string> attachs)
        {
            if (attachs == null) return;
            List<string> tempAttachs = new List<string>();
            List<string> serverAttachs = new List<string>();

            //Se pasan las rutas de los adjuntos a la lista.
            foreach (KeyValuePair<string, string> a in attachs)
            {
                tempAttachs.Add(a.Value);
            }

            if (sforum == null)
                sforum = new SForum();

            try
            {
                string tempPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";

                string sValue = new SZOptBusiness().GetValue("UseWebService");
                bool zoptUseWebService = (string.IsNullOrEmpty(sValue)) ? false : bool.Parse(sValue);

                foreach (string file in tempAttachs)
                {
                    if (zoptUseWebService)
                        sforum.InsertForumAttachWS(idMensaje, FileEncode.Encode(tempPath + file), _user.ID, file);
                    else
                        sforum.InsertForumAttach(idMensaje, FileEncode.Encode(tempPath + file), _user.ID, file);
                }
            }
            catch (Exception exception1)
            {
                ZClass.raiseerror(exception1);
                lblErrors.Visible = true;
                lblErrors.Text = "Ha ocurrido un error al insertar los archivos adjuntos.\r\nConsulte con el Departamento de Sistemas.";
            }
        }

        /// <summary>
        /// Copia archivos a un destino.
        /// </summary>
        /// <param name="attachs"></param>
        /// <param name="destDirectory"></param>
        /// <returns></returns>
        private static List<string> CopyFiles(IEnumerable<string> attachs, string destDirectory)
        {
            List<string> serverAttachs = new List<string>();
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            foreach (string path in attachs)
            {
                string serverAttach = FileBusiness.GetUniqueFileName(destDirectory, Path.GetFileName(path));
                File.Copy(path, serverAttach);
                serverAttachs.Add(serverAttach);
            }
            return serverAttachs;
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
            var attachs = (Dictionary<string, string>)Session["Attachs"];
            attachs.Remove(filename);
            LoadGridview();
        }

        /// <summary>
        /// Limpia los adjuntos de la grilla y de session
        /// </summary>
        /// <history>Diego 18-09-2008 Created</history>
        protected void ClearAttachments(object sender, EventArgs e)
        {
            ClearAttachments();
        }
        public void ClearAttachments()
        {
            Session["Attachs"] = null;
            gvAttachs.DataSource = null;
            gvAttachs.DataBind();
            lblAttachsLenght.Text = Attachslenght1 + MaxAttachsSize() + Attachslenght2;
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
            catch (Exception)
            { }

            Dictionary<string, string> attachs = null;
            if (Session["Attachs"] != null)
            {
                attachs = (Dictionary<string, string>)Session["Attachs"];
                if (attachs.ContainsKey(filename) == false)
                {
                    attachs.Add(filename, file);
                }
            }
            else
            {
                attachs = new Dictionary<string, string> { { filename, file } };
            }
            Session["Attachs"] = attachs;

            LoadGridview();
        }

        /// <summary>
        /// Realiza la carga de la grilla de adjuntos
        /// </summary>
        /// <history>Diego 18-09-2008 Created</history>
        private void LoadGridview()
        {
            FileInfo file = null;
            try
            {
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("Icono");
                DataColumn dc2 = new DataColumn("Nombre de Adjunto");
                DataColumn dc3 = new DataColumn("AttachPath");
                DataColumn dc4 = new DataColumn("Tamaño");
                Int64 AttachsSize = 0;
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];

                if (sresult == null)
                    sresult = new SResult();

                foreach (string filepath in attachs.Keys)
                {
                    string tempDirectory = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp";
                    string tempPathFile = tempDirectory + "\\" + filepath;
                    if (!File.Exists(tempPathFile)) continue;
                    file = new FileInfo(tempPathFile);
                    dt.Rows.Add(sresult.GetFileIcon(tempPathFile), filepath, "", (file.Length / 1024) + "KB");
                    AttachsSize += (file.Length / 1024);
                    GC.Collect();
                }
                if (dt.Rows.Count == 0)
                {
                    gvAttachs.DataSource = null;
                    gvAttachs.DataBind();
                    lblAttachsLenght.Text = Attachslenght1 + MaxAttachsSize() + Attachslenght2;
                    return;
                }
                gvAttachs.ShowHeader = false;
                gvAttachs.Columns.Clear();

                ImageField imagenDoc = new ImageField { ShowHeader = true, HeaderText = "Icono" };
                imagenDoc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                imagenDoc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                imagenDoc.DataImageUrlField = "Icono";
                imagenDoc.DataImageUrlFormatString = "../../Tools/icono.aspx?id={0}";
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

                BoundField bndPath = new BoundField
                                         {
                                             DataField = "AttachPath",
                                             ShowHeader = true,
                                             HeaderText = "AttachPath",
                                             Visible = false
                                         };
                gvAttachs.Columns.Add(bndPath);
                gvAttachs.DataKeyNames = new[] { "Nombre de Adjunto" };

                BoundField bndSize = new BoundField { DataField = "Tamaño", ShowHeader = true, HeaderText = "Tamaño" };
                gvAttachs.Columns.Add(bndSize);
                lblAttachsLenght.Text = Attachslenght1 + (MaxAttachsSize() - AttachsSize) + Attachslenght2;
                gvAttachs.DataSource = dt;
                gvAttachs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrors.Text = "Error al cargar la grilla de adjuntos";
                ZClass.raiseerror(ex);
            }
            finally
            {
                file = null;
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
                return MaxAttachsSize() - (Int64.Parse(filesizes));
            }
            catch
            {
                LoadGridview();
                filesizes = lblAttachsLenght.Text.Replace(Attachslenght1, string.Empty);
                filesizes = filesizes.Replace(Attachslenght2, string.Empty);
                return MaxAttachsSize() - (Int64.Parse(filesizes));
            }
        }
        #endregion

        private Int64 MaxAttachsSize()
        {
            Int64 MaxAttachsSize;
            ZOptBusiness zopt = new ZOptBusiness();

            if (zopt.GetValue("MaxLenghtForumAttach") != null)
                MaxAttachsSize = Int64.Parse(zopt.GetValue("MaxLenghtForumAttach")) / 1024;
            else
                MaxAttachsSize = 11111111;

            zopt = null;
            return MaxAttachsSize;
        }
    }
}