using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;
using System.IO;
using Subgurim.Controles;
using Zamba.Outlook ;


public partial class WebClient_Results_Results : System.Web.UI.Page
{
    #region "Atributos - Constantes"
    //Maximo tamaño de adjuntos ( 5mb)
    const Int64 MAX_ATTACHS_SIZE = 5120;
    const string ATTACHSLENGHT_1 = "restan ";
    const string ATTACHSLENGHT_2 = " KB para archivos adjuntos";
    #endregion

    private const String FULL_PATH_COLUMN_NAME = "fullpath";
    private const String DOC_ID_COLUMN_NAME = "docId";
    private ExcelReportService.ExcelService oExcelService = new ExcelReportService.ExcelService();

    protected void ResultsGrid_ReloadValues()
    {
        LoadGrid();
        
    }
    


                /// <summary>
    /// Devuelve el Id de usuario logeado
    /// </summary>
    private Int64? UserId
    {
        get
        {
            Int64? Id = null;

            if (null != Session["UserId"])
            {
                Int64 TryValue;
                if (Int64.TryParse(Session["UserId"].ToString(), out TryValue))
                    Id = TryValue;
            }

            return Id;
        }
    }


        

    /// <summary>
    /// Evento que se ejecuta cuando se carga la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Ezequiel] 27/01/2009 Modified - Se modifico asi se puede visualizar los documentos al entrar por link directo
    ///     [Gaston]   16/02/2009  Modified  Si se ejecuta el evento NewSubject del webControl "WCForum" entonces se ejecuta el método "showNewSubject"
    ///                                     Cuando se carga la página por primera vez entonces se llena el usuario y su mail actual
    ///                                     Si se adjunta un archivo se llama al método "attach"
    ///     [sebastian]26/03/2009   Modified - New event in method to come back the last view after select list of emails to send a notification.                                
    /// </history>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ForumControl.RespFor.Click += new EventHandler(RespFor_Click);
        IndexsControl.SaveChanges.Visible = true;
        IndexsControl.SaveChanges.Click += new ImageClickEventHandler(SaveChanges_Click);
        //[sebastian 26-03-09] this event is used to come back to set multiview to view2
        ucMailList.GoBackToMailEvent += new Controls_Notifications_WebUserControl.GoBackToMail(GoBackToMail);
        if (!Page.IsPostBack)
        {



            if (!UserId.HasValue)
                FormsAuthentication.RedirectToLoginPage();

            if (Request.Url != null && !Request.Url.ToString().Contains("WebClient/Results/Results.aspx?Subgurim"))
                Session["NewSubject"] = "0";
            txtCurrentUserMail.Text = UserBusiness.CurrentUser().eMail.Mail;
            txtCurrentUserName.Text = UserBusiness.CurrentUser().Name;
        }

        if (System.Web.Configuration.WebConfigurationManager.AppSettings["ShowToolBar"].ToString() == "true" && this.ToolbarTable.Visible == false)
            this.ToolbarTable.Visible = true;
        else if (System.Web.Configuration.WebConfigurationManager.AppSettings["ShowToolBar"].ToString() == "false" && this.ToolbarTable.Visible == true)
            this.ToolbarTable.Visible = false;
        //this.imgbtnImprimir.Click += new ImageClickEventHandler(this.imgbtnImprimir_Click);
        //this.imgbtnEnviarPorMail.Click += new ImageClickEventHandler(this.imgbtnEnviarPorMail_Click);
        this.imgbtnExportarExcel.Click += new ImageClickEventHandler(this.imgbtnExportarExcel_Click);

        if (Request.QueryString["docid"] != null && Request.QueryString["doctid"] != null)
        {
            this.Results = new List<IResult>();
            this.Results.Add(Results_Business.GetResult(Convert.ToInt64(Request.QueryString["docid"]), Convert.ToInt64(Request.QueryString["doctid"])));
        }

            LoadGrid();

        ResultsGrid.OnSelectResult += new Controls_Core_WCResults.SelectedResult(InitializePanels);
        ForumControl.NewSubject += new Controls_Forum_WCForum.NewSubjectClick(showNewSubject);

        // Si se presiono el botón "Browse" entonces IsPosting es true y por lo tanto se adjunta el archivo llamando al método attach(...)
        if (FileUploaderAJAX1.IsPosting)
        {
            attach(FileUploaderAJAX1.PostedFile);
        }

        //UCInsertNewOffice.OnAdd += new Controls_Insert_NewOffice_NewOfficeSelector.NewDocumentSelected(UCInsertNewOffice_OnAdd);
        //UCInsertNewOffice.OnErrorOcurred +=new Controls_Insert_NewOffice_NewOfficeSelector.ErrorOcurred(UCInsertNewOffice_OnErrorOcurred);
        //InsertVirtualForm.OnNewDocumentSelected += new Controls_Insert_Forms_NewFormSelector.NewFormSelected(UCInsertNewOffice_OnAdd);
        //InsertVirtualForm.OnErrorOcurred += new Controls_Insert_Forms_NewFormSelector.ErrorOcurred(UCInsertNewOffice_OnErrorOcurred);  
        //InsertDocumentControl.OnInsertOfficeDocument_ErrorOcurred += new Controls_Insert_WCInsert.InsertOfficeDocument_ErrorOcurred(InsertOfficeDocument_ErrorOcurred);
        //InsertDocumentControl.OnInsertOfficeDocument_NewDocumentSelected += new Controls_Insert_WCInsert.InsertOfficeDocument_NewDocumentSelected(InsertOfficeDocument_NewDocumentSelected);
        //UCInsertFindDocument.OnAddDoc += new Controls_Insert_WCInsert.InsertFindDocument(UCInsertNewOffice_OnAdd);
        if (Page.IsPostBack && this.IndexsControl.IndexsTable.Rows.Count == 0 && Session["DocSelTB"] != null)
        {
            InitializePanels(((Result)Session["DocSelTB"]).ID);
        }
    }
    /// <summary>
    /// [sebastian 26-03-09] set multiview to view2 after select user's email in wcMailList
    /// </summary>
    private void GoBackToMail()
    {
        MultiView1.SetActiveView(View2);
        this.UpdPnlMultiView.Update();
        //[sebastian 26-03-09] Load mails in the textbox txtemailto
        LoadMailsToSend();
    }
    /// <summary>
    /// [sebastian 26-03-09]Load emails in the textbox txtemailto
    /// </summary>
    private void LoadMailsToSend()
    {
        try
        {
            foreach (string email in ucMailList.MailsToSend)
            {
                if(string.IsNullOrEmpty(txtEmailTo.Text) == true)

                txtEmailTo.Text += email.ToString() + ";";
                else
                    txtEmailTo.Text += ";" + email.ToString() ;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    private void imgbtnExportarExcel_Click(object sender, ImageClickEventArgs args)
    {
        if (oExcelService.DataTableDirectlyToExcel(MakeDataTable(this.Results), System.Web.HttpRuntime.AppDomainAppPath + "temp\\Lista-Excel.xls"))
        {
            Response.Redirect("~/temp/Lista-Excel.xls");
            
            
   
        }
    }

    /// <summary>
    /// Metodo para imprimir el documento..
    /// </summary>
    /// <history>[Ezequiel] 28/01/09 Created.</history>
    private void imgbtnImprimir_Click(object sender,ImageClickEventArgs args)
    {
        if (Session["DocSelTB"] != null)
        {
            Response.Redirect("~/WebClient/Results/PrintDocument.aspx?fullpath=" + ((Result)Session["DocSelTB"]).FullPath);
        }
    }

    /// <summary>
    /// Metodo para enviar por mail el documento.
    /// </summary>
    /// <history>[Ezequiel] 28/01/09 Created.</history>
    private void imgbtnEnviarPorMail_Click(object sender, ImageClickEventArgs args)
    {
        if (Session["DocSelTB"] != null)
        {
            Response.Redirect("~/WebClient/Results/SendMail.aspx?fullpath=" + ((Result)Session["DocSelTB"]).FullPath);
        }
    }

    private void LoadGrid()
    {
        DataTable DTResults = MakeDataTable(this.Results);
        ResultsGrid.ShowResults(DTResults);
        ResultsGrid.HideColumn(FULL_PATH_COLUMN_NAME);
        ResultsGrid.HideColumn(DOC_ID_COLUMN_NAME);
        UpdatePanel4.Update();
    }

    //protected void InsertOfficeDocument_NewDocumentSelected(Int64 Docid, Int64 dtid)
    //{
    //    Result result = Results_Business.GetResult(Docid, dtid);
    //    this.Results.Add(result);
    //    InitializePanels(result.ID);
    //    LoadGrid();
    //}

    protected void UCInsertNewOffice_OnAdd(Int64 Docid, Int64 dtid)
    {
        Result result = Results_Business.GetResult(Docid, dtid);
        this.Results.Add(result);
        InitializePanels(result.ID);
        LoadGrid();

    }
    protected void UCInsertNewOffice_OnErrorOcurred()
    {
    }

    public System.Collections.Generic.List<Zamba.Core.IResult> Results
    {
        get
        {
            if (Session["CurrentResults"] != null)
                return (System.Collections.Generic.List<Zamba.Core.IResult>)Session["CurrentResults"];
            else
            {
                System.Collections.Generic.List<Zamba.Core.IResult> mresults = new System.Collections.Generic.List<Zamba.Core.IResult>();
                Session["CurrentResults"] = mresults;
                return mresults;
            }
        }
        set
        {
            Session["CurrentResults"] = value;
        }
    }
    
    public DataTable MakeDataTable(System.Collections.Generic.List<Zamba.Core.IResult> Results)
    {
        DataTable DT = new DataTable();

        foreach (Zamba.Core.IResult R in Results)
        {
            DataColumn DC = null;

            if (!DT.Columns.Contains("Nombre Documento"))
                DC = DT.Columns.Add("Nombre Documento");
            else
                DC = DT.Columns["Nombre Documento"];

            if (!DT.Columns.Contains("Tipo"))
                DC = DT.Columns.Add("Tipo");
            else
                DC = DT.Columns["Tipo"];
            //--------------emiliano 10/08/2008 --------------------
            if (!DT.Columns.Contains("Icono"))DC = DT.Columns.Add("Icono");
            else DC = DT.Columns["Icono"];
            //---------------------------------------------------------
            if (!DT.Columns.Contains(FULL_PATH_COLUMN_NAME))
                DC = DT.Columns.Add(FULL_PATH_COLUMN_NAME);
            else
                DC = DT.Columns[FULL_PATH_COLUMN_NAME];

            if (!DT.Columns.Contains(DOC_ID_COLUMN_NAME))
                DC = DT.Columns.Add(DOC_ID_COLUMN_NAME);
            else
                DC = DT.Columns[DOC_ID_COLUMN_NAME];


            foreach (Zamba.Core.IIndex I in R.Indexs)
            {
                DC = null;
                if (!DT.Columns.Contains(I.Name))
                    DC = DT.Columns.Add(I.Name);
                else
                    DC = DT.Columns[I.Name];
            }
        }

        DT.AcceptChanges();


        foreach (Zamba.Core.IResult R in Results)
        {
            DataRow DR = DT.NewRow();

            if (R.ISVIRTUAL == true)
            {
                ZwebForm[] VirtualForms = FormBussines.GetAllForms((Int32)R.DocTypeId);

                if (VirtualForms  != null)
                {
                foreach (ZwebForm VirtualForm in VirtualForms)
                {
                    if (VirtualForm.Type == FormTypes.Edit)
                    {
                        DR[FULL_PATH_COLUMN_NAME] = VirtualForm.Path;
                        break;
                    }
                    
                }
                }
            }
            else
            { DR[FULL_PATH_COLUMN_NAME] = R.FullPath; }

            
            DR["Nombre Documento"] = R.Name;
            DR["Tipo"] = R.DocType.Name;
            DR["Icono"] = R.IconId.ToString();
            DR[DOC_ID_COLUMN_NAME] = R.ID;


            foreach (Zamba.Core.IIndex I in R.Indexs)
            {
                DR[I.Name] = I.Data;
            }
            DT.Rows.Add(DR);
        }
        return DT;
    }

    protected void ResultsGrid_OnSelectedResultChanged(Int64 docId)
    {
            Tabs.Enabled = true;
            Tabs.DataBind();
            AccordionPane1.DataBind();
            PanelIndexs.DataBind();
    }

    public void LoadAssociatedResults(Int64 docId, Int64 docTypeId)
    {
        DocType DocType = DocTypesBusiness.GetDocType(docTypeId);
        IResult Result = Results_Business.GetNewResult(docId, DocType);

        IUser Cuser = UserBusiness.CurrentUser();
//        ArrayList AsociatedResultsList = Zamba.Core.DocAsociatedBussines.getAsociatedResultsFromResult(Result,Cuser.ID);
        ArrayList AsociatedResultsList = Zamba.Core.DocAsociatedBussines.getAsociatedResultsFromResult(Result.DocType.ID , Result);

        if (null != AsociatedResultsList && AsociatedResultsList.Count > 0)
        {
            List<IResult> results = new List<IResult>(AsociatedResultsList.Count);
            
            foreach (object item in AsociatedResultsList)
            {
                if (item is IResult)
                    results.Add((IResult)item);
            }
            
            DataTable dt = MakeDataTable(results);
            AsociatedResults.ShowResults(dt);

            //TODO: Esto esconde la columna de info , pasar a propiedad del control que especifique si 
            //se pueden ver esta columan y la de info.
            AsociatedResults.HideColumn(string.Empty);

        }
        

    }

    void InitializePanels(Int64? DocId)
    {
        try
        {
            this.imgbtnIncorporarDocumento.NavigateUrl = "~/WebClient/Insert/Insert.aspx?DocId=" + DocId.ToString() + "&DocTypeId=" + DocTypesBusiness.GetDocTypeIdByDocId(Convert.ToInt64(DocId)).ToString();
            Session["DocId"] = DocId;
            this.UpdatePanel4.Update();
            Session["DocSelTB"] = Results_Business.GetResult(Convert.ToInt64(DocId), DocTypesBusiness.GetDocTypeIdByDocId(Convert.ToInt64(DocId)));
            IUser Cuser = UserBusiness.CurrentUser();
            if (bool.Parse(UserPreferences.getValue("ShowToolBarPrintButton", UserPreferences.Sections.UserPreferences, true)) == true && UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.Print, -1) == true)
                this.ToolbarTable.FindControl("imgbtnImprimir").Visible = true;
            else
                this.ToolbarTable.FindControl("imgbtnImprimir").Visible = false;
            if (bool.Parse(UserPreferences.getValue("ShowSendByMailButton", UserPreferences.Sections.UserPreferences, true)) == true && UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail, -1) == true)
                this.ToolbarTable.FindControl("imgbtnEnviarPorMail").Visible = true;
            else
                this.ToolbarTable.FindControl("imgbtnEnviarPorMail").Visible = false;
            if (bool.Parse(UserPreferences.getValue("ShowAddFolderButton", UserPreferences.Sections.UserPreferences, true)) == true && UserBusiness.Rights.GetUserRights(Cuser, ObjectTypes.DocTypes, RightsType.Create,Convert.ToInt32(DocTypesBusiness.GetDocTypeIdByDocId(DocId.Value))) == true)
                this.ToolbarTable.FindControl("imgbtnIncorporarDocumento").Visible = true;
            else
                this.ToolbarTable.FindControl("imgbtnIncorporarDocumento").Visible = false;
            if (Zamba.Tools.EnvironmentUtil.getOfficePlatform() > Zamba.Tools.EnvironmentUtil.OfficeVersions.Office2000)
                this.ToolbarTable.FindControl("imgbtnExportarExcel").Visible = true;
            else
                this.ToolbarTable.FindControl("imgbtnExportarExcel").Visible = false;
            //cargar indexs
            IndexsControl.MostrarIndices(DocId.Value);
        }
        catch (Exception ex)
        { }

        try
        {
            foreach (IResult CurrentResult in Results)
            {
                if (null != CurrentResult.DocType && CurrentResult.ID == DocId)
                {
                    LoadAssociatedResults(DocId.Value, CurrentResult.DocType.ID);
                    break;
                }
            }
        }
        catch
        { }

        //asociados
        try
        {
            //relacionados
           // DocRelatedControl.getRelatedDocs((Int32)DocId.Value);
        }
        catch
        { }
        try
        {
            //foro
            ForumControl.MostrarMensajesForo(DocId.Value);
        }
        catch
        { }
        if (Session["DocSelTB"] != null)
            Session["DocId"] = ((Result)Session["DocSelTB"]).ID;
        UpdatePanel1.Update();
    }
    
    /// <summary>
    /// Método que muestra la vista que permite agregar un nuevo tema
    /// </summary>
    /// <param name="value"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    /// </history>
    private void showNewSubject(Int64? value)
    {
        if (value == null)
            Session["ParentIdResp"] = null;
        this.clearValues();

        if (Page.IsPostBack != true && this.MultiView1.ActiveViewIndex == 0 )
        {
            if (Session["DocSelTB"] != null && string.IsNullOrEmpty( ((Result)Session["DocSelTB"]).FullPath) != true )
            {
                string filename = ((Result)Session["DocSelTB"]).FullPath.Remove(0, Session["DocSelTB"].ToString().LastIndexOf("\\") + 1);
                this.addAttach(filename, ((Result)Session["DocSelTB"]).FullPath);
            }
        }

        this.MultiView1.SetActiveView(this.View2);
        this.UpdatePanel1.Update();
        this.UpdPnlMultiView.Update();
    }

    /// <summary>
    /// Método que muestra la vista que permite visualizar y seleccionar un result
    /// </summary>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    /// </history>
    private void showResults()
    {
        if (this.MultiView1.ActiveViewIndex == 1)
        {
            this.MultiView1.SetActiveView(this.View1);
            this.UpdPnlMultiView.Update();
        }
    }

    /// <summary>
    /// Método que limpia el gridview y algunas cajas de texto pertenecientes a la vista 2 (Nuevo Tema)
    /// </summary>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    ///     [Ezequiel]  18/02/2009  Modified - Se modifico para que funcione la opcion de respuesta.  
    /// </history> 
    private void clearValues()
    {
        Session["Attachs"] = null;
        gvAttachs.DataSource = null;
        gvAttachs.DataBind();
        txtSubject.Text = "";
        if (Session["ParentIdResp"] != null)
            txtSubject.Text = "Re: " + Session["ParentIdResp"].ToString().Split('@')[1];
        txtMessage.Text = "";
        txtEmailTo.Text = "";
        lblErrors.Visible = false;
    }

    /// <summary>
    /// Agrega un adjunto en la grilla, utilizado para inicializar el control con el documento que lo llama
    /// </summary>
    /// <param name="filename">Nombre de archivo, con extension</param>
    /// <param name="fullpath">ruta para localizar el archivo en disco</param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  Código original perteneciente a WCSendMail
    ///     [Ezequiel]  18/02/2009  Modified  Se corrigio para quer adjunte bien
    /// </history> 
    private void addAttach(string filename, string fullpath)
    {
        //if (File.Exists(fullpath) == false)
        //    return;

        string pathTemp = Server.MapPath("~/temp");
        string file = System.IO.Path.Combine(pathTemp, (new System.IO.FileInfo(filename)).Name);
        Dictionary<string, string> attachs = null;
        try
        {
            if (File.Exists(file))
                File.Delete(file);

            File.Copy(new System.IO.FileInfo(fullpath).FullName, new System.IO.FileInfo(file).FullName);


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
                attachs = new Dictionary<string, string>();
                attachs.Add(filename, file);
            }
        }
        catch (Exception ex)
        { }

        Session["Attachs"] = attachs;

        LoadGridview();
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona el botón "Guardar"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    /// </history>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((!String.IsNullOrEmpty(txtSubject.Text)) && (!String.IsNullOrEmpty(txtMessage.Text)))
        {
            InsertarNuevoMensaje(txtSubject.Text, txtMessage.Text, Int64.Parse(Session["UserId"].ToString()));
            ForumControl.ucNuevoMensaje_OnAdd();
            showResults();
        }
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona el botón "Cancelar"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    ///     [Gaston]  17/02/2009  Modified  Se agrego Session["NewSubject"]
    /// </history>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Se coloca NewSubject en cero para que se pueda cambiar el documento al seleccionar un documento en la grilla 
        // (Consultar en WCResults - evento gvDocuments_SelectedIndexChanged)
        Session["NewSubject"] = "0";
        showResults();
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona el botón "Enviar"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    ///     [Ezequiel]  18/02/2009  Modified  Se corrigio para que adjunte bien
    /// </history>
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        

        if ((!String.IsNullOrEmpty(txtSubject.Text)) && (!String.IsNullOrEmpty(txtMessage.Text)) && (!string.IsNullOrEmpty(txtEmailTo.Text)))
        {
            List<string> attachs = GetAttachs();
                       

            if (MessagesBussines.SendMail(txtEmailTo.Text, string.Empty, string.Empty, txtSubject.Text, txtMessage.Text, false, attachs))
            {
                try
                {
                    this.FileUploaderAJAX1.Reset();
                    this.pSendMailok.Visible = true;
                    this.UpPop.Update();
                    foreach (string path in attachs)
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                lblErrors.Text = "Error al enviar el mail, verifique los datos de conexión";
            }

            InsertarNuevoMensaje(txtSubject.Text, txtMessage.Text, Int64.Parse(Session["UserId"].ToString()));
        }
        else
        {
            if ((String.IsNullOrEmpty(txtSubject.Text)) || (String.IsNullOrEmpty(txtMessage.Text)))
                lblErrors.Text = "El asunto y el mensaje no pueden estar vacíos";
            else
                lblErrors.Text = "Falta especificar el o los destinatarios del mensaje";
        }

        lblErrors.Visible = true;
    }

    /// <summary>
    /// Método que sirve para insertar un nuevo tema
    /// </summary>
    /// <param name="Asunto"></param>
    /// <param name="Texto"></param>
    /// <param name="IUserId"></param>
    /// <param name="docId"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created and Modified Código original perteneciente a NuevoTema. Se elimino la respuesta
    /// </history>
    private void InsertarNuevoMensaje(string Asunto, string Texto, Int64 IUserId)
    {
        if (Session["DocId"] != null)
        {
            Int64 ResultId = (Int64)Session["DocId"];
            Int32 Parent;
            Int32 IdMsg;
            if (Session["ParentIdResp"] == null)
            {
                Parent = 0;
                IdMsg = ZForoBusiness.SiguienteId(ResultId);
            }
            else
            {
                Parent = ZForoBusiness.SiguienteParent(ResultId, Convert.ToInt32(Session["ParentIdResp"].ToString().Split('@')[0]));
                IdMsg = Convert.ToInt32(Session["ParentIdResp"].ToString().Split('@')[0]);
            }
            string nuevo_asunto = Asunto + "-"; //se agrego ß para realizarf parseo
            //Para guardar un nuevo tema
            ZForoBusiness.InsertMessage(ResultId, 0, IdMsg, Parent, nuevo_asunto, Texto, DateTime.Today, (Int32)IUserId, 0);
            //Después de guardarse el mensaje, se actualiza el U_TIME (fecha y hora de la ùltima acción) y se registra la acción en USER_HST
            UserBusiness.Rights.SaveAction(IdMsg, ObjectTypes.Foro, RightsType.NuevoTemaGuardar, "", 0);
            ForumControl.ucNuevoMensaje_OnAdd();
        }
    }

    /// <summary>
    /// Método que sirve para adjuntar un archivo
    /// --------------------------------------------------------------------------------------------
    /// --------------------------------------------------------------------------------------------
    /// NOTA: Por alguna razón el archivo no se guarda en el servidor y tampoco aparece en la grilla
    /// --------------------------------------------------------------------------------------------
    /// --------------------------------------------------------------------------------------------
    /// </summary>
    /// <param name="pf">Archivo que se quiere adjuntar</param>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  
    /// </history>
    private void attach(HttpPostedFileAJAX pf)
    { 
        Int64 PostedLenght = (pf.ContentLength / 1024);
        Int64 attachsLenght = GetAttachsSizeLenght();

        if ((MAX_ATTACHS_SIZE - (PostedLenght + attachsLenght)) < 0)
        {
            lblAttachsLenght.Text = "No se pudo adjuntar el archivo por ser de tamaño superior al limite";
            return;
        }

        //string pathTemp = "~/temp";
        string pathTemp = "~/temp";
        string file = string.Empty;
        string originalName = string.Empty;

        //Genera el nombre del archivo
        file = System.IO.Path.Combine(pathTemp, pf.FileName);
        originalName = System.IO.Path.GetFileName(pf.FileName);
        //Sube el archivo

        try
        {
            FileUploaderAJAX1.SaveAs(file);
        }
        catch (System.IO.IOException ex)
        {
            throw ex;
        }

        string filename = pf.FileName;
        Dictionary<string, string> attachs = null;

        if (Session["Attachs"] != null)
        {
            attachs = (Dictionary<string, string>)Session["Attachs"];
            if (attachs.ContainsKey(filename) == false)
            {
                //attachs.Add(filename, Server.MapPath("~/temp") + "\\" + (new System.IO.FileInfo(file)).Name.ToString());
                attachs.Add(filename, Server.MapPath("~/temp") + "\\" + originalName );
            }
        }
        else
        {
            attachs = new Dictionary<string, string>();
            attachs.Add(filename, Server.MapPath("~/temp") + "\\" + originalName);
        }

        Session["Attachs"] = attachs;

        LoadGridview();
    }

    /// <summary>
    /// Realiza la carga de la grilla de adjuntos
    /// </summary>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  Código original perteneciente a WCSendMail
    /// </history>
    public void LoadGridview()
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

            foreach (string filepath in attachs.Keys)
            {
                if (File.Exists(attachs[filepath]))
                {
                    file = new FileInfo(attachs[filepath]);
                    dt.Rows.Add(Results_Business.GetFileIcon(attachs[filepath]), filepath, attachs[filepath], (file.Length / 1024) + "KB");
                    AttachsSize += (file.Length / 1024);
                    GC.Collect();
                    GC.Collect();
                }
            }

            if (dt.Rows.Count == 0)
            {
                gvAttachs.DataSource = null;
                gvAttachs.DataBind();
                lblAttachsLenght.Text = ATTACHSLENGHT_1 + MAX_ATTACHS_SIZE + ATTACHSLENGHT_2;
                return;
            }

            gvAttachs.ShowHeader = false;
            gvAttachs.Columns.Clear();

            ImageField Imagen_doc = new ImageField();
            Imagen_doc.ShowHeader = true;
            Imagen_doc.HeaderText = "Icono";
            Imagen_doc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            Imagen_doc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            Imagen_doc.DataImageUrlField = "Icono";
            Imagen_doc.DataImageUrlFormatString = "../../icono.aspx?id={0}";
            this.gvAttachs.Columns.Add(Imagen_doc);

            ButtonField btnDelete = new ButtonField();
            btnDelete.HeaderText = "Eliminar";
            btnDelete.ShowHeader = true;
            btnDelete.DataTextField = "Nombre de Adjunto";
            btnDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            btnDelete.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            btnDelete.CommandName = "Select";
            this.gvAttachs.Columns.Add(btnDelete);

            BoundField f = new BoundField();
            f.DataField = "AttachPath";
            f.ShowHeader = true;
            f.HeaderText = "AttachPath";
            f.Visible = false;
            this.gvAttachs.Columns.Add(f);
            this.gvAttachs.DataKeyNames = new string[] { "Nombre de Adjunto" };

            BoundField g = new BoundField();
            g.DataField = "Tamaño";
            g.ShowHeader = true;
            g.HeaderText = "Tamaño";
            this.gvAttachs.Columns.Add(g);
            lblAttachsLenght.Text = ATTACHSLENGHT_1 + (MAX_ATTACHS_SIZE - AttachsSize).ToString() + ATTACHSLENGHT_2;
            this.gvAttachs.DataSource = dt;
            this.gvAttachs.DataBind();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        //finally
        //{
        //    file = null;
        //}
    
    }

    /// <summary>
    /// retorna una lista con los adjuntos, utilizado para el envio de mail
    /// </summary>
    /// <returns></returns>
    /// <history>
    ///     [Gaston]  16/02/2009  Created  Código original perteneciente a WCSendMail
    /// </history>
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
    /// <history>
    ///     [Gaston]  16/02/2009  Created  Código original perteneciente a WCSendMail
    /// </history>
    private Int64 GetAttachsSizeLenght()
    {
        string filesizes;
        filesizes = lblAttachsLenght.Text.Replace(ATTACHSLENGHT_1, string.Empty);
        filesizes = filesizes.Replace(ATTACHSLENGHT_2, string.Empty);
        try
        {
            return MAX_ATTACHS_SIZE - (Int64.Parse(filesizes));
        }
        catch
        {
            LoadGridview();
            filesizes = lblAttachsLenght.Text.Replace(ATTACHSLENGHT_1, string.Empty);
            filesizes = filesizes.Replace(ATTACHSLENGHT_2, string.Empty);
            return MAX_ATTACHS_SIZE - (Int64.Parse(filesizes));
        }
    }


    /// <summary>
    /// Eventos que permiten guardar los cambios modificados en los indices
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>[Ezequiel] 18/02/09 - Created </history>
    void SaveChanges_Click(object sender, ImageClickEventArgs e)
    {
        this.pnlPopUpModal.Visible = true;
        this.UpPop.Update();
    }
    /// <summary>
    /// Eventos que permiten guardar los cambios modificados en los indices
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>[Ezequiel] 18/02/09 - Created </history>
    public void btnCancelChg_Click(object sender, EventArgs e)
    {
        this.pnlPopUpModal.Visible = false;
    }
    /// <summary>
    /// Eventos que permiten guardar los cambios modificados en los indices
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>[Ezequiel] 18/02/09 - Created </history>
    public void btnAccept_Click(object sender, EventArgs e)
    {
        Result NRChg = ((Result)Session["DocSelTB"]);
        NRChg.Indexs = ArrayList.Adapter(IndexsControl.GetIndexs());
        bool SavedGood = true;
        try
        {
            IResult res = (IResult) NRChg;
            Results_Business.SaveModifiedIndexData2(ref res , true, true);
        }
        catch (Exception x)
        {
        }
        finally
        {
            if (SavedGood)
            {
                foreach (Result x in this.Results)
                    if (x.ID == ((Result)Session["DocSelTB"]).ID)
                        x.Indexs = NRChg.Indexs;
                LoadGrid();
                UpdatePanel4.Update();
            }
            this.pnlPopUpModal.Visible = false;
        }
    }
    protected void btnSendMailOk_Click(object sender, EventArgs e)
    {
        this.pSendMailok.Visible = false;
        this.UpPop.Update();
        Session["NewSubject"] = "0";
        showResults();
    }

    void RespFor_Click(object sender, EventArgs e)
    {
        showNewSubject(1);
    }


  
    protected void btnMails_Click(object sender, EventArgs e)
    {
        this.MultiView1.SetActiveView(this.view3);
        this.UpdPnlMultiView.Update();
    }
 
}
