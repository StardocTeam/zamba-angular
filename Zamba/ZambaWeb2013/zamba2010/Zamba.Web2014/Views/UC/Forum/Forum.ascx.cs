using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Web.Security;
using System.IO;
using System.Collections.Generic;
using Zamba.Membership;

namespace Controls.Forum
{
    public partial class Controls_Forum_WCForum : ResultTab
    {
        #region "Atributos"
        string selId;
        IUser _user = null;
        SForum sforum;
        #endregion

        #region "Propiedades"
        
        private Int64 DocTypeId 
        {
            get { return Int64.Parse(hdnDocTypeId.Value); }
            set { hdnDocTypeId.Value = value.ToString(); } 
        }

        private Int64 DocId
        {
            get { return Int64.Parse(hdnDocId.Value); }
            set { hdnDocId.Value = value.ToString(); }
        }

        private bool DeleteEnabled
        {
            set {
                btnDeleteTop.Enabled = value;
                btnDeleteBottom.Enabled = value;
            }
        }

        private bool ReplyEnabled
        {
            set
            {
                btnReplyTop.Enabled = value;
                btnReplyBottom.Enabled = value;
            }
        }

        private bool DeleteRights
        {
            get
            {
                if (String.IsNullOrEmpty(hdnCanDelete.Value))
                {
                    SRights sRights = new SRights();
                    hdnCanDelete.Value = sRights.GetUserRights(ObjectTypes.DocTypes, RightsType.DeleteMsgForum, Int32.Parse(DocTypeId.ToString())).ToString();
                }

                return Boolean.Parse(hdnCanDelete.Value);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ZOptBusiness zopt = new ZOptBusiness();
            if (null == Session["UserId"])
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                _user = (IUser)Session["User"];

                if (zopt.GetValue("ShowForumAttachsTab") == null)
                {
                   ZOptBusiness.Insert("ShowForumAttachsTab", "true");
                }
                else if (bool.Parse(zopt.GetValue("ShowForumAttachsTab")) == false)

                {
                    TabAdjuntos.Visible = false;
                }

                if (Page.IsPostBack)
                {
                    popUpMensaje.OnAdd += new Added(ucNuevoMensaje_OnAdd);
                    popUpMensaje.OnClosed += new Closed(ucNuevoMensaje_OnClosed);
                }
                else
                {
                    try
                    {
                        Int64 id;
                        bool continuar = false;

                        string tempId = Request.QueryString["TaskId"];
                        if (Int64.TryParse(tempId,out id))
                        {
                            STasks t = new STasks();
                            DataTable dt = t.GetResultExtraData(id);
                            if (dt.Rows.Count > 0)
                            {
                                DocId = Convert.ToInt64(dt.Rows[0].ItemArray[0]);
                                DocTypeId = Convert.ToInt64(dt.Rows[0].ItemArray[1]);
                                Page.Title = "Foro  -  " + dt.Rows[0].ItemArray[2].ToString();
                                continuar = true;
                            }
                        }
                        else
                        {
                            DocId = Convert.ToInt64(Request.QueryString["ResultId"]);
                            DocTypeId = Convert.ToInt64(Request.QueryString["DocTypeId"]);
                            continuar = true;
                        }

                        if (continuar)
                        {
                            MostrarMensajesForo();

                            if (!tvwMensajesForo.Visible)
                            {
                                tvwMensajesForo.Visible = true;
                            }

                            pnlRespuesta.Visible = false;
                            pnlMensaje.Visible = true;

                            SetGUIRights();

                            TabForo.ActiveTabIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.pnlErrors.Visible = true;
                        this.txtErrors.Text = "No se ha podido cargar el foro del documento";
                        this.txtErrors.ForeColor = System.Drawing.Color.Red;
                        this.Foro.Visible = false;
                        ZClass.raiseerror(ex);
                    }

                    Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
                    Page.Title = (string)ZOptBusines.GetValue("WebViewTitle") + " - Foro";
                }
            }
            zopt = null;
        }

        /// <summary>
        /// Llamada a MostrarMensajesForo. Se ejecuta al aregar una respuesta.
        /// </summary>
        private void ucNuevoMensaje_OnAdd()
        {
            MostrarMensajesForo();
        }

        /// <summary>
        /// Oculta el cuadro de respuesta y muestra los mensajes nuevamente.
        /// </summary>
        private void ucNuevoMensaje_OnClosed()
        {
            pnlRespuesta.Visible = false;
            Foro.Visible = true;
        }

        /// <summary>
        /// Obtiene los mensajes del documento y se encarga de mostrarlos en el foro.
        /// </summary>
        public void MostrarMensajesForo()
        {
            ArrayList arrayMensajes = new ArrayList();
            ArrayList arrayRespuestas = new ArrayList();

            if (sforum == null)
                sforum = new SForum();

            sforum.GetAllMessages(DocId, DocTypeId, ref arrayMensajes, ref arrayRespuestas, false,_user);
            CargarTreeViewWebForo(arrayMensajes, arrayRespuestas);
        }

        /// <summary>
        /// Carga los mensajes en el foro.
        /// </summary>
        /// <param name="arrayMensajes">ArrayList con los mensajes que inician las conversaciones (con parentId = 0).</param>
        /// <param name="arrayRespuestas">ArrayList con las respuestas totales de cualquier conversación.</param>
        private void CargarTreeViewWebForo(IList arrayMensajes, IList arrayRespuestas)
        {   
            tvwMensajesForo.Nodes.Clear();

            if (arrayMensajes.Count != 0)
            {
                Int32 i;
                string msg2;

                //habilito los botones de responder si hay mensajes en el foro
                btnReplyTop.Enabled = true;
                btnReplyBottom.Enabled = true;

                //Se recorren los nuevos temas (los padres).
                for (i = 0; i != arrayMensajes.Count; i++)
                {
                    MensajeForo mensaje = (MensajeForo)arrayMensajes[i];
                    TreeNode foroNode = new TreeNode(mensaje.Mensaje);
                    Int32 len = mensaje.Mensaje.Length - 1;
                    msg2 = string.Empty;

                    if (len > 70) 
                        len = 70;
                    if (len > -1) 
                        msg2 = mensaje.Mensaje.Substring(0, len);

                    //Configuración del nodo padre. El value es para tener el ID del mensaje y si es hijo o padre.
                    
                    //Se cambia la forma en que se carga el nodo del tema
                    foroNode.Text = string.Concat(mensaje.Name, " (" + mensaje.Fecha.ToShortDateString(), " " + mensaje.UserName + ")");

                    foroNode.Value = mensaje.ParentId + "@" + mensaje.ID;
                    foroNode.ToolTip = mensaje.Mensaje;
                    tvwMensajesForo.Nodes.Add(foroNode);

                    BuscarRespuestas(arrayRespuestas, foroNode, ((MensajeForo)arrayMensajes[i]).ID);
                    //foroNode.ExpandAll();
                }

                if (tvwMensajesForo.Nodes.Count > 0)
                {
                    tvwMensajesForo.Nodes[0].Select();
                    
                    //Activa las solapas.
                    TabForo.Enabled = true;
                    pnlMensaje.Visible = true;
                    pnlRespuesta.Visible = false;
                    //formBrowser.Visible = false;

                    //Guarda el asunto del nodo para poder incluirlo en la respuesta.
                    hdnSelectedNodeSubjet.Value = tvwMensajesForo.SelectedNode.Text;

                    //Guarda el índice del nodo seleccionado.
                    hdnSelectedNodeIndex.Value = tvwMensajesForo.SelectedNode.ValuePath;

                    LoadTab();
                }
            }
            else
            {
                //deshabilito los botones de respuesta si no hay mensajes que mostrar y responder
                btnReplyTop.Enabled = false;
                btnReplyBottom.Enabled = false;
            }
        }
    
        /// <summary>
        /// Busca recursivamente si un mensaje contiene respuestas. Si las tiene las agrega al foro.
        /// </summary>
        /// <param name="arrayRespuestas">ArrayList que contiene todas las respuestas.</param>
        /// <param name="parentNode">TreeNode donde se agregarán las respuestas.</param>
        /// <param name="IdMensaje">Id del mensaje del cual se verificará la existencia de respuestas.</param>
        private static void BuscarRespuestas(IList arrayRespuestas,TreeNode parentNode, Int64 IdMensaje)
        {
            Int32 i;
            string msg;

            //Si tiene respuestas, se agregarán recursivamente.
            for (i = 0; i != arrayRespuestas.Count; i++)
            {
                if (((MensajeForo) arrayRespuestas[i]).ParentId != IdMensaje) continue;

                MensajeForo respuesta = (MensajeForo)arrayRespuestas[i];
                TreeNode foroSubNode = new TreeNode(respuesta.Mensaje);
                msg = string.Empty;

                Int32 len = respuesta.Mensaje.Length - 1;
                if (len > 40)
                    len = 40;
                if (len > -1)
                    msg = respuesta.Mensaje.Substring(0, len);

                //Se cambia la forma en que se carga el nodo del tema
                foroSubNode.Text = string.Concat(respuesta.Name, " (" + respuesta.Fecha.ToShortDateString(), " " + respuesta.UserName + ")");

                foroSubNode.Value = respuesta.ParentId + "@" + respuesta.ID;
                foroSubNode.ToolTip = respuesta.Mensaje;
                parentNode.ChildNodes.Add(foroSubNode);

                BuscarRespuestas(arrayRespuestas, foroSubNode, respuesta.ID);
            }
        }

        /// <summary>
        /// Evento disparado al responder un mensaje. Abre una ventana para generar el nuevo mensaje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResponderForo_Click(object sender, EventArgs e)
        {
            int msgid = GetSelectedMessageId();
            int sourceid = GetSourceMessageId();//GetSourceMessageId();

            if (msgid != -1)
            {
                //2=respuesta
                popUpMensaje.ResponseForum();
                Session["NewSubject"] = "2";
                popUpMensaje.Subject = "Re: " + GetSubject(tvwMensajesForo.SelectedNode.Text); //+ hdnSelectedNodeSubjet.Value;
                //popUpMensaje.Message = txtMensaje.Text;
                popUpMensaje.SetErrorText(String.Empty);
                popUpMensaje.SourceDocId = DocId;
                popUpMensaje.SourceMessageId = sourceid;
                popUpMensaje.ParentMessageId = msgid;
                popUpMensaje.SourceDocTypeId = DocTypeId;
                popUpMensaje.ClearAttachments();
                Foro.Visible = false;
                pnlRespuesta.Visible = true;
                popUpMensaje.LoadParticipants();
            }
            else
            {
                txtErrors.Text = "Seleccione un mensaje para responder";
            }
        }

        //Obtiene el texto del nodo hasta la parte del "(fecha usuario)"
        private string GetSubject(string NodeText) {
            return NodeText.Remove(NodeText.LastIndexOf('('));
        }

        /// <summary>
        /// Evento disparado al crear un tema. Abre una ventana para generar el nuevo mensaje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewThread_Click(object sender, EventArgs e)
        {
            Session["NewSubject"] = "1";
            popUpMensaje.CreateForum();
            popUpMensaje.Subject = "Título del mensaje";
            popUpMensaje.Message = String.Empty;
            popUpMensaje.SetErrorText(String.Empty);
            popUpMensaje.SourceDocId = DocId;
            popUpMensaje.ParentMessageId = 0;
            popUpMensaje.SourceMessageId = 0;
            popUpMensaje.SourceDocTypeId = DocTypeId;
            popUpMensaje.ClearAttachments();

            txtMensaje.Text = string.Empty;
            Foro.Visible = false;
            pnlRespuesta.Visible = true;
            TabForo.Enabled = true;
        }

        /// <summary>
        /// Evento disparado al querer eliminar un mensaje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarForo_Click(object sender, EventArgs e)
        {
            int msgid = GetSelectedMessageId();

            if (msgid != -1)
            {
                if (sforum == null)
                    sforum = new SForum();

                List<int> toDeleteMessages = new List<int>();
                string currNodeValue = hdnSelectedNodeIndex.Value;
                TreeNode currNode = tvwMensajesForo.FindNode(currNodeValue);

                List<TreeNode> flatHierarchy = FlatTreeHierarchy(currNode);

                string[] tempValue;
                foreach (TreeNode item in flatHierarchy)
                {
                    tempValue = item.Value.Split('@');
                    ZOptBusiness zopt = new ZOptBusiness();
                    string deleteAttachs = zopt.GetValue("DeleteForumAttachments");
                    zopt = null;
                    sforum.DeleteMessage(int.Parse(tempValue[1]));
                }

                MostrarMensajesForo();
                TabForo.Enabled = false;
                pnlRespuesta.Visible = false;
                Foro.Visible = true;
                //formBrowser.Visible = false;
                cleanControls();
            }
        }

        List<TreeNode> FlatTreeHierarchy(TreeNode firstNode)
        {
            List<TreeNode> flatHierarchy = new List<TreeNode>();

            flatHierarchy.Add(firstNode);

            if (firstNode.ChildNodes.Count == 0)
            {
                return flatHierarchy;
            }

            foreach (TreeNode currNode in firstNode.ChildNodes)
            {
                flatHierarchy.AddRange(FlatTreeHierarchy(currNode));
            }

            return flatHierarchy;
        }

        /// <summary>
        /// Evento disparado al seleccionar un mensaje. Se encarga de cargar los datos necesarios del mensaje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvwMensajesForo_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvwMensajesForo.SelectedNode == null) return;

            //Activa las solapas.
            TabForo.Enabled = true;
            pnlMensaje.Visible = true;
            pnlRespuesta.Visible = false;

            //Guarda el asunto del nodo para poder incluirlo en la respuesta.
            hdnSelectedNodeSubjet.Value = tvwMensajesForo.SelectedNode.Text;

            //Guarda el índice del nodo seleccionado.
            hdnSelectedNodeIndex.Value = tvwMensajesForo.SelectedNode.ValuePath;

            LoadTab();
        }

        private TreeNode getSourceID(TreeNode node)
        {
            if (node.Parent != null)
            {
                return getSourceID(node.Parent);
            }
            else
                return node;
        }

        /// <summary>
        /// Carga los datos del tab activo.
        /// </summary>
        private void LoadTab()
        {
            DataTable dt;
            ZOptBusiness zopt = new ZOptBusiness();

            if (bool.Parse(zopt.GetValue("ShowForumAttachsTab")) == false)
                //se aplica la reasignacion de tabs al ocultar el tab de adjuntos
                switch (TabForo.ActiveTabIndex)
                {
                    case 1:
                        TabForo.ActiveTabIndex = 2;
                        break;
                    case 2:
                        TabForo.ActiveTabIndex = 3;
                        break;
                }

            zopt = null;

            switch (TabForo.ActiveTabIndex)
            {
                case 0: //Mensaje
                    try
                    {

                   
                //Verifica si debe habilitar o no el botón para eliminar mensajes.
                    DeleteEnabled = DeleteRights;

                    //Si el nodo fue el tema inicial, se habilita el botón de responder.
                    ReplyEnabled = true;

                    txtMensaje.Text = tvwMensajesForo.SelectedNode.ToolTip;
                    }
                    catch (Exception ex)
                    {

                        ZClass.raiseerror(ex);
                    }  
              break;  
                case 1:
                 try
                    {

                 
                    
                    if (sforum == null)
                        sforum = new SForum();
                    /////////////////////////////////////////
                    //Adjuntos

                    //string[] filesSource = sforum.GetAttachsNamesByMessageIdWS(GetSelectedMessageId(), _user.ID);
                    string[] filesSource = sforum.GetAttachsNamesByMessageId(GetSelectedMessageId());

                    if (filesSource != null && filesSource.Length > 0)
                    {
                        //Activa los controles.
                        grdAdjuntos.Visible = true;
                        lblAdjuntos.Visible = false;

                        DataTable tblAttachments = new DataTable();
                        tblAttachments.Columns.Add("ID", typeof(string));
                        tblAttachments.Columns.Add("Nombre",typeof(string));
                        foreach (string str in filesSource)
                        {
                            tblAttachments.Rows.Add(str,str);
                        }

                        //Carga los datos en la grilla.
                        LoadGrid(grdAdjuntos, tblAttachments, true);
                    }
                    else
                    {
                        //Desactivo los controles.
                        lblAdjuntos.Visible = true;
                        grdAdjuntos.Visible = false;
                        //formBrowser.Visible = false;
                    }
                
                /////////////////////////////////////////
                    }
                 catch (Exception ex)
                 {

                     ZClass.raiseerror(ex);
                 }
                break; 
                case 2:
                  try
                    {

                   
                    if (sforum == null)
                        sforum = new SForum();
                    
                    //Participantes  
                    dt = sforum.GetUserAndGroupsParticipantsId(GetParentMessageId());
                    if (dt.Rows.Count > 0)
                    {
                        grdParticipantes.Visible = true;
                        //Carga los datos en la grilla.
                        LoadGrid(grdParticipantes, dt,false);
                    }

                 
                    }
                  catch (Exception ex)
                  {

                      ZClass.raiseerror(ex);
                  }
                   break;
                case 3:
                    try
                    {

                   
                    if (sforum == null)
                        sforum = new SForum();

                    //Informacion                           
                    dt = sforum.GetInformation(GetSelectedMessageId());                                      
                    if (dt.Rows.Count > 0)
                    {
                        //Carga los datos en la grilla.
                        UsuarioCreador.Text = dt.Rows[0][0].ToString();
                        FechaCreacion.Text = dt.Rows[0][1].ToString();

                        //DateTime fecha = (DateTime)dt.Rows[0][1];
                        //FechaVencimiento.Text = string.IsNullOrEmpty(dt.Rows[0][2].ToString()) ? string.Empty : fecha.AddDays(Int32.Parse(dt.Rows[0][2].ToString())).ToString();
                        //IdMensaje.Text = GetSelectedMessageId().ToString();
                    }

                  
                    }
                    catch (Exception ex)
                    {

                        ZClass.raiseerror(ex);
                    }
          break;  
            }
            zopt = null;
        }

        /// <summary>
        /// Obtiene el id del mensaje seleccionado.
        /// </summary>
        /// <returns>Devuelve un entero con el id del mensaje seleccionado.</returns>
        private int GetSelectedMessageId()
        {
            if (String.IsNullOrEmpty(hdnSelectedNodeIndex.Value))
            {
                return -1;
            }
            else
            {
                selId = hdnSelectedNodeIndex.Value;
                int i = Int32.Parse(selId.Substring(selId.LastIndexOf('@') + 1));
                return i;
            }
        }

        /// <summary>
        /// Obtiene el id del mensaje seleccionado.
        /// </summary>
        /// <returns>Devuelve un entero con el id del mensaje seleccionado.</returns>
        private int GetSourceMessageId()
        {
            if (String.IsNullOrEmpty(hdnSelectedNodeIndex.Value))
            {
                return 0;
            }
            else
            {
                selId = hdnSelectedNodeIndex.Value;
                String value= selId.Substring(selId.IndexOf('@') + 1);
                if (value.Contains("/"))
                    value = value.Substring(0, value.IndexOf('/'));
                
                int i = Int32.Parse(value);
                                
                return i;
            }
        }

        /// <summary>
        /// Obtiene el ID del mensaje que inicio la conversación.
        /// </summary>
        /// <returns>Devuelve un entero con el id del mensaje que inició la conversación.</returns>
        private int GetParentMessageId()
        {
            selId = hdnSelectedNodeIndex.Value;

            if (selId.Contains("/"))
            {
                //Obtiene solamente el path del parent.
                selId = selId.Remove(selId.IndexOf("/"));
            }

            return Int32.Parse(selId.Split('@')[1]); 
        }

        /// <summary>
        /// Carga un datatable en una grilla.
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="dt"></param>
        private static void LoadGrid(GridView gv, DataTable dt, bool addShowColumn)
        {
            gv.DataSource = null;
            gv.Columns.Clear();
            gv.DataBind();

            if (addShowColumn)
            {
                CommandField cf = new CommandField {SelectText = "Ver", ShowSelectButton = true};
                gv.Columns.Add(cf);
            }
        
            //Se agregan las columnas a la grilla.
            foreach (DataColumn c in dt.Columns)
            {
                BoundField f = new BoundField
                                   {
                                       DataField = c.Caption,
                                       ShowHeader = true,
                                       HeaderText = c.Caption.CompareTo("NAME") == 0 ? "USUARIO" : c.Caption,
                                       SortExpression = c.Caption + " ASC"
                                   };

                gv.Columns.Add(f);
            }

            gv.AutoGenerateColumns = false;
            gv.DataSource = dt;
            gv.DataBind();

            //Se oculta la columna que contiene el ID de la tabla.
            if (addShowColumn)
                gv.Columns[1].Visible = false;
            else
                gv.Columns[0].Visible = false;
        }

        /// <summary>
        /// Evento disparado al cambiar de tab. Se utiliza para cargar el tab seleccionado.
        /// </summary>
        /// <param name="sender">TabControl</param>
        /// <param name="e"></param>
        protected void TabForo_ActiveTabChanged(object sender, EventArgs e)
        {
            //Verifica que exista un nodo seleccionado.
            if (tvwMensajesForo.SelectedNode!=null)
                LoadTab();
        }

        /// <summary>
        /// Muestra un adjunto en el iframe.
        /// </summary>
        /// <param name="path">Ruta del archivo a mostrar.</param>
        private void ShowAttach(string path)
        {
            //string url;
            string fileName = string.Empty;

            //formBrowser.Visible = true;
            lblAttachError.Visible = false;

            try
            {
                //Lo carga en el iframe
                string url = string.Format(MembershipHelper.Protocol + Request.ServerVariables["HTTP_HOST"] + 
                    Request.ApplicationPath + "/Services/GetAttachBlobForum.ashx?MessageId={0}&FileName={1}&UserID={2}", GetSelectedMessageId().ToString(), path, _user.ID);
                //formBrowser.Attributes["src"] = url;

                //Lo abre en una ventana aparte. WI: 7737 (originalmente era ampliar el alto del iframe de adjuntos, 
                //pero dado que se complica creo que es mejor abrirlo en una ventana aparte)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenAttach", String.Format("window.open('{0}');", url), true);
            }
            catch (Exception ex)
            {
                //formBrowser.Visible = false;
                lblAttachError.Visible = true;
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Hace la copia local del archivo y devuelve la ruta de la copia.
        /// </summary>
        /// <param name="path">Ruta del archivo a copiar (string)</param>
        /// <returns>Ruta de la copia (string)</returns>
        private static String MakeLocalCopy(String path)
        {
            System.IO.FileInfo fi;
            System.IO.FileInfo fa;

            try
            {
                fi = new System.IO.FileInfo(path);
                fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "temp\\" + fi.Name);

                if (fa.Exists)
                {
                    if (fa.IsReadOnly)
                    {
                        fa.IsReadOnly = false;
                    }
                    fa.Delete();
                }

                if (fa.Directory.Exists == false)
                    fa.Directory.Create();

                System.IO.File.Copy(fi.FullName, fa.FullName);

                return ".\\temp\\" + fi.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fi = null;
                fa = null;
            }
        }

        /// <summary>
        /// Evento disparado al presionar el link Select de una fila.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdAdjuntos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pasa como parámetro el dato de la columna PATH.
            ShowAttach(grdAdjuntos.SelectedRow.Cells[1].Text);
        }

        /// <summary>
        /// Limpia el contenido de los controles
        /// </summary>
        private void cleanControls()
        {
            txtMensaje.Text = string.Empty;               

            lblAdjuntos.Visible = false;
            grdAdjuntos.Visible = false;
            //formBrowser.Visible = false;

            grdParticipantes.Visible = false;        

            UsuarioCreador.Text = string.Empty;
            FechaCreacion.Text = string.Empty;
            //FechaVencimiento.Text = string.Empty;
            //IdMensaje.Text = string.Empty;

        }

        /// <summary>
        /// Verifica si el usuario tiene permisos de eliminar mensajes
        /// </summary>
        private void SetGUIRights()
        {
            SRights srights = new SRights();

            if (!srights.CheckAllRights(ObjectTypes.DocTypes, RightsType.DeleteMsgForum, DocTypeId))
            {
                btnDeleteBottom.Visible = false;
                btnDeleteTop.Visible = false;
            }
        }     
    }
}
  

