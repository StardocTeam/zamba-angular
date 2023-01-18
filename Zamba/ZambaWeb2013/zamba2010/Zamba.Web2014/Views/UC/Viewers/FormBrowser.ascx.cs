using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Zamba.Core;
using Zamba.Core.Enumerators;
using Zamba.Services;
using Zamba.Tools;
using Zamba.Membership;
using Zamba;

public partial class Views_UC_Viewers_FormBrowser : System.Web.UI.UserControl
{
    #region Attributes and Properties
    const string _INDEXUPDATE1 = "Atributo '";
    const string _INDEXUPDATE2 = "' de '";
    const string _INDEXUPDATE3 = "' a '";
    const string _INDEXUPDATE4 = "', ";
    IUser user;
    private Int64 formIdFromDoShowForm;
    private ITaskResult _ITaskResult;
    private IResult _result;
    private SZOptBusiness zoptB;
    bool isReindex;
    bool isShared;

    #region RuleConfiguration
    bool _continueWithCurrentTasks;
    bool _dontOpenTaskAfterInsert;
    bool _fillCommonAttributes;
    private bool _haveSpecificAttributes;
    private Dictionary<long, string> _htAttributesConfig;
    private long _ruleCallTaskID;
    long _ruleCallDocID;
    long _ruleCallDocTypeID;
    #endregion

    public INewResult NewResult
    {
        get
        {
            return (INewResult)Session["NewResultToInsert"];
        }
        set
        {
            Session["NewResultToInsert"] = value;
        }
    }
    public ITaskResult TaskResult
    {
        get
        {
            return _ITaskResult;
        }
        set
        {
            _ITaskResult = value;
        }
    }
    public IResult Result
    {
        get
        {
            return _result;
        }
        set
        {
            _result = value;
        }
    }
    public string GetDocFileUrl
    {
        get
        {
            return EnvironmentUtil.curProtocol(Request) + @"://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + @"/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&UserID={2}";
        }
    }
    public bool IsShowing
    {
        get;
        set;
    }

    public Dictionary<long, string> CurrentZFrom
    {
        get
        {
            if (Session["FormValues" + HttpContext.Current.Request.Url.ToString()] == null)
                return null;
            else
                return (Dictionary<long, string>)Session["FormValues" + HttpContext.Current.Request.Url.ToString()];
        }
        set
        {
            Session["FormValues" + HttpContext.Current.Request.Url.ToString()] = value;
        }
    }

    public IZwebForm CurrentForm { get; set; }

    public event throwExecuteRule ExecuteRule;
    private const string STR_SLSTPLAIN_SEPARATOR = " - ";
    public delegate void throwExecuteRule(long ruleId, List<ITaskResult> results);
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                zoptB = new SZOptBusiness();
                if (CurrentZFrom == null) CurrentZFrom = new Dictionary<long, string>();

                user = Zamba.Membership.MembershipHelper.CurrentUser;
                completarindice.Visible = false;
                hdnIsShowing.Value = IsShowing.ToString();

                if (IsShowing)
                {
                    if (_ITaskResult != null)
                    {
                        _result = (IResult)_ITaskResult;

                        if (Page.IsPostBack)
                        {
                            //Guardo los indices
                            //Chequeo si se hizo postback desde un boton de un form para ejecutar una regla y la ejecuto si la hay
                            ExecuteFormActions(true);
                        }
                        //Evaluar de que llamó al formbrowser y mostrar el form.
                        EvaluateShowForm();
                    }
                    else
                    {
                        ExecuteFormActions(false);
                        ShowDocument();
                    }
                    
                    //05/08/11: sumada la carga de hiddens para la toolbar.
                    LoadToolbarHiddens();

                    //Verifica si debe mostrar el panel de atributos
                    if (Boolean.Parse(UserPreferences.getValue("ShowIndexLinkUnderTask", Sections.UserPreferences, "False")))
                    {
                        this.DivIndices.Visible = true;
                        LoadIndexsPanel(Page.IsPostBack);
                        completarindice.OnSave += new Controls.Indexs.Saved(completarindice_OnSave);
                    }
                    else
                    {
                        this.DivIndices.Visible = false;
                    }
                }
                else
                {
                    //Si es postback es porque o se llamó al insert o al cancel.
                    if (Page.IsPostBack)
                    {
                        ExecuteFormActions(false);
                    }
                    else
                    {
                        //Si no es postback mostramos el formulario.
                        if (!ShowAndSetInsertForm())
                        {
                            lblDoc.Text = "Ha ocurrido un error al cargar el formulario";
                            return;
                        }
                    }
                    //Verifica si debe mostrar el panel de atributos
                    if (Boolean.Parse(UserPreferences.getValue("ShowIndexLinkUnderTask", Sections.UserPreferences, "False")))
                    {
                        this.DivIndices.Visible = true;
                        LoadIndexsPanel(Page.IsPostBack);
                        completarindice.OnSave += new Controls.Indexs.Saved(completarindice_OnSave);
                    }
                    else
                    {
                        this.DivIndices.Visible = false;
                    }
                    navToolbar.Visible = false;
                }

                //Actualiza el timemout
                SRights rights = new SRights();
                Int32 type = 0;
                if (user.WFLic) type = 1;
                if (user.ConnectionId > 0)
                {
                    SUserPreferences SUserPreferences = new SUserPreferences();
                    rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                    SUserPreferences = null;
                }
                else
                    Response.Redirect("~/Views/Security/LogIn.aspx");
                rights = null;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private void LoadIndexsPanel(bool useSessionIndexs)
    {
        SRights Rights = new SRights();
        IUser user = Zamba.Membership.MembershipHelper.CurrentUser;

        //Se cargan los permisos del tipo de documento
        Int64 docTypeId = _result.DocTypeId;
        isShared = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, _result.DocTypeId);
        isReindex = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, _result.DocTypeId);

        //Se validan otros permisos para el caso de reindex
        if (_result.DocTypeId != 0)
        {
            if (isShared)
            {
                isReindex = false;
            }
            else
            {
                if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, docTypeId))
                {
                    if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID == _result.OwnerID && !isReindex)
                        isReindex = true;

                    if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID != _result.OwnerID && isReindex)
                    {
                        if (!Rights.DisableOwnerChanges(user, docTypeId))
                            isReindex = false;
                    }
                }
            }
        }

        Rights = null;
        user = null;

        if (_result.Indexs.Count > 0)
        {
            List<IIndex> indexs;

            if (useSessionIndexs)
            {
                indexs = (List<IIndex>)Session["CurrentIndexs"];
            }
            else
            {
                indexs = new List<IIndex>();
                for (int i = 0; i < _result.Indexs.Count; i++)
                    indexs[i] = (Index)_result.Indexs[i];
            }

            completarindice.DtId = _result.DocTypeId;
            completarindice.Visible = true;
            completarindice.ShowIndexs(indexs,   WebModuleMode.Result);
            completarindice.SaveChanges.Visible = isReindex;
            completarindice.CleanIndexs.Visible = isReindex;
            completarindice.SaveChangesImgUrl = "../../../Content/Images/Toolbars/disk_blue.png";
            completarindice.CleanIndexImgUrl = "../../../Content/Images/Toolbars/cleanIndex.png";
        }
        else
        {
            completarindice.Visible = false;
        }
    }

    /// <summary>
    /// Guarda las modificaciones hechas sobre el panel de índices
    /// </summary>
    protected void completarindice_OnSave()
    {
        try
        {
            _result = GetHdnResult();

            if (_result != null)
            {
                //Se obtienen los índices actualizados
                List<IIndex> indexs = completarindice.CurrentIndexs;
                StringBuilder description = new System.Text.StringBuilder();

                if (indexs != null)
                {
                    description.Append("Modificaciones realizadas en '" + _result.Name + "': ");
                    List<Int64> modifiedIndex = new List<Int64>();

                    //Se comparan los valores ingesados en el panel con los del result.
                    foreach (Index index in indexs)
                    {
                        foreach (Index rIndex in _result.Indexs)
                        {
                            if (index.ID == rIndex.ID)
                            {
                                if (String.Compare(index.Data, rIndex.Data) != 0)
                                {
                                    description.Append(_INDEXUPDATE1 + rIndex.Name + _INDEXUPDATE2 + rIndex.Data + _INDEXUPDATE3 + index.Data + _INDEXUPDATE4);

                                    //Actualiza los indices del result
                                    rIndex.Data = index.Data;
                                    rIndex.DataTemp = index.DataTemp;
                                    rIndex.dataDescription = index.dataDescription;
                                    rIndex.dataDescriptionTemp = index.dataDescriptionTemp;

                                    //Si existen cambios los agrega a una lista para marcarlos como editados
                                    modifiedIndex.Add(index.ID);
                                }
                                break;
                            }
                        }
                    }

                    //Verifica si hubieron modificaciones
                    if (modifiedIndex.Count > 0)
                    {
                        SResult sResult = new SResult();
                        SRights Rights = new SRights();

                        //Guarda las modificaciones y el historial
                        sResult.SaveModifiedIndexs(ref _result, modifiedIndex);
                        description = description.Remove(description.Length - 2, 2);
                        Rights.SaveActionWebView(_result.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, description.ToString());
                        completarindice.SetMessage("Cambios guardados con éxito", true);

                        Rights = null;
                        sResult = null;
                        modifiedIndex.Clear();
                    }

                    modifiedIndex = null;
                    description = null;
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            completarindice.SetMessage("Error al guardar los cambios", false);
        }
    }

    private bool ShowAndSetInsertForm()
    {
        if (Request["formid"] != null && NewResult == null)
        {
            user = Zamba.Membership.MembershipHelper.CurrentUser;
            SForms SForms = new SForms();
            IZwebForm zfrm = SForms.GetForm(Int64.Parse(Request["formid"].ToString()));

            if (zfrm != null)
            {
                if (NewResult == null)
                {
                    NewResult = GetNewResult(zfrm);
                }

                return navigateToForm(zfrm, "4");
            }
        }
        else
        {
            user = Zamba.Membership.MembershipHelper.CurrentUser;
            SForms SForms = new SForms();
            IZwebForm zfrm = SForms.GetForm(Int64.Parse(Request["formid"].ToString()));

            if (zfrm != null)
            {
                return navigateToForm(zfrm, "4");
            }
        }
        return false;
    }

    private void LoadToolbarHiddens()
    {
        docTB.DocId = _result.ID.ToString();
        docTB.DocTypeId = _result.DocTypeId.ToString();
        docTB.StepId = Request["wfstepid"];
        docTB.FilePath = Result.FullPath;
        docTB.DocContainerClientId = "divViewer";

        if (string.IsNullOrEmpty(Result.FullPath) == false)
        {
            string[] temp = Result.FullPath.Split(new char[1] { '.' });
            docTB.DocExt = temp[temp.Length - 1];
        }
    }

    private void EvaluateShowForm()
    {
        Int64 FormId;
        if (TaskResult.CurrentFormID > 0)
        {
            Page.Session.Add("CurrentFormID" + TaskResult.ID, TaskResult.CurrentFormID);
            ShowDocumentFromDoShowForm(TaskResult.CurrentFormID);
        }
        else if (Page.Session["CurrentFormID" + TaskResult.ID] != null && Int64.TryParse(Page.Session["CurrentFormID" + TaskResult.ID].ToString(), out FormId))
        {
            ShowDocumentFromDoShowForm(FormId);
        }
        else
        {
            ShowDocument();
        }
    }

    private INewResult GetNewResult(IZwebForm zfrm)
    {
        SForms SForms = new SForms();
        zfrm = SForms.GetForm(Int64.Parse(Request["formid"].ToString()));

        NewResult = SForms.CreateVirtualDocument(zfrm.DocTypeId);

        if (NewResult.AutoName == null)
        {
            NewResult.AutoName = " Name=" + zfrm.Name + " Id=" + zfrm.ID;
        }
        else
        {
            NewResult.AutoName = NewResult.AutoName + " Name=" + zfrm.Name + " Id=" + zfrm.ID;
        }

        NewResult.CurrentFormID = zfrm.ID;

        return NewResult;
    }

    //Javier: Se mejora la iteración entre las variables para ver que acciones realizar.
    /// <summary>
    /// Ejecuta las acciones posibles en un formulario, tales como guardar, cancelar y 
    /// ejecutar reglas
    /// </summary>
    /// <param name="IsTask">Este parámetro se utiliza para verificar si ejecutar reglas</param>
    private void ExecuteFormActions(bool IsTask)
    {
        System.Collections.Specialized.NameValueCollection postBackForm = HttpContext.Current.Request.Form;
        int keysCount = postBackForm.AllKeys.Length;
        string strItem;

        for (int i = 0; i < keysCount; i++)
        {
            if (postBackForm.AllKeys[i] == null) continue;

            strItem = postBackForm.AllKeys[i].ToLower();

            if (strItem == "zamba_showoriginal")
            {
                ScriptDocument();
            }
            if (string.Compare(strItem, "zamba_save") == 0)
            {
                //Si se llama al save pero se está insertando(NewResult != null)
                if (NewResult != null)
                {
                    InsertDocument();
                    //Luego de hacer el comit del documento, se fija si tiene archivos para insertar
                    SaveAttachedDocuments();
                    NewResult = null;
                    //Una vez insertado se limpia la variable de session
                    Session.Remove("SpecificAttrubutes" + _ruleCallTaskID.ToString());
                }
                else
                {
                    SaveFormData(IsTask);
                    //Luego de hacer el comit del documento, se fija si tiene archivos para insertar
                    SaveAttachedDocuments();
                }

                break;
            }

            //Si se presiona cancel solo se realizará una acción si viene del insert.
            if (string.Compare(strItem, "zamba_cancel") == 0)
            {
                if (!IsShowing)
                {
                    //Al cancelar se limpia la variable de session
                    Session.Remove("SpecificAttrubutes" + _ruleCallTaskID.ToString());
                    CloseInsertControl();
                }

                // Al presionarse cancelar se cierra el modal dentro del cual se abrio el form.
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseModal", "CloseInsert();", true);

                break;
            }

            if (IsTask && !(strItem.StartsWith("zamba_rule_cancel") || strItem.StartsWith("zamba_rule_save")) && strItem.StartsWith("zamba_rule_"))
            {

                long RuleId = long.Parse(strItem.Replace("zamba_rule_", String.Empty));

                String RuleZvars = postBackForm[strItem];
                if (RuleZvars != null)
                {
                    String[] Vars = RuleZvars.Split(Char.Parse("&"));

                    foreach (String v in Vars)
                    {
                        String[] VarStrings = v.Split(char.Parse("="));
                        if (VarStrings.Length == 2)
                        {
                            var VarName = VarStrings[0];
                            var VarValue = VarStrings[1];

                            if (VarValue.Contains("totable"))
                            {
                                VarValue = VarValue.Replace(")", "");
                                VarValue = VarValue.Replace("totable(", "");
                                char[] splitseparator1 = { char.Parse(";") };
                                String[] VarValues = VarValue.Split(splitseparator1, StringSplitOptions.RemoveEmptyEntries);
                                if (VarValues.Length > 0)
                                {
                                    DataTable dt = new DataTable();
                                    char[] splitseparator2 = { char.Parse("|") };
                                    Int32 columncount = VarValues[0].Split(splitseparator2, StringSplitOptions.RemoveEmptyEntries).Length;

                                    for (Int32 x = 1; x <= columncount; x++)
                                    {
                                        dt.Columns.Add("c" + x);
                                    }

                                    foreach (String u in VarValues)
                                    {
                                        char[] splitseparator3 = { char.Parse("|") };
                                        dt.Rows.Add(u.Split(splitseparator3, StringSplitOptions.RemoveEmptyEntries));
                                    }
                                    dt.AcceptChanges();

                                    if (VariablesInterReglas.ContainsKey(VarName))
                                    {
                                        VariablesInterReglas.set_Item(VarName, dt);
                                    }
                                    else
                                    {
                                        VariablesInterReglas.Add(VarName, dt);
                                    }

                                }
                            }
                            else
                            {
                                if (VariablesInterReglas.ContainsKey(VarName))
                                {
                                    VariablesInterReglas.set_Item(VarName, VarValue);
                                }
                                else
                                {
                                    VariablesInterReglas.Add(VarName, VarValue);
                                }
                            }
                        }
                    }

                }

                List<ITaskResult> list = new List<ITaskResult>();
                list.Add(TaskResult);

                Session["ExecutingRule"] = RuleId;

                if (string.IsNullOrEmpty(postBackForm["hdnRuleActionType"]) || postBackForm["hdnRuleActionType"] == "Save")
                    SaveFormData(IsTask);

                ExecuteRule(RuleId, list);
            }
        }
    }

    /// <summary>
    /// Verifica si el formulario tiene archivos para salvar, si es asi los guardará en zamba(blob o no, en el doc original o no)
    /// </summary>
    private void SaveAttachedDocuments()
    {
        if (HttpContext.Current.Request.Files.Count > 0)
        {
            ZOptBusiness zopt = new ZOptBusiness();
            IResult currRes = (NewResult != null) ? NewResult : Result;
            HttpPostedFile file;
            SResult sRes = new SResult();
            byte[] fileBytes;
            string sUseWebService = zoptB.GetValue("UseWebService");
            string wsResultsUrl = zoptB.GetValue("WSResultsUrl");
            bool useWebService = (string.IsNullOrEmpty(sUseWebService)) ? false : bool.Parse(sUseWebService);
            FileInfo incomingFi;

            //Por cada archivo que llega
            //(estos normalmente vienen por un input file)
            foreach (string inputFileId in HttpContext.Current.Request.Files.AllKeys)
            {
                //Si el archivo es docOriginal
                if (inputFileId.CompareTo("docOriginal") == 0)
                {
                    //obtengo el nombre de archivo y el array de bytes
                    file = HttpContext.Current.Request.Files[inputFileId];

                    if (file.ContentLength > 0)
                    {
                        incomingFi = new FileInfo(file.FileName);

                        currRes.File = incomingFi.Name;
                        fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);

                        //Verifica si usar web service o no para hacer el insert del archivo(que en este caso es el del documento actual)
                        if (useWebService && !string.IsNullOrEmpty(wsResultsUrl))
                        {
                            sRes.InsertDocFileWS(currRes.ID, currRes.DocTypeId, fileBytes, incomingFi.Name, user.ID);
                        }
                        else
                            sRes.InserDocFile(currRes, fileBytes, incomingFi.Name);
                    }
                }
            }
            zopt = null;
        }
    }

    /// <summary>
    /// Cierra el control modal y libera el nuevo result de la session.
    /// </summary>
    private void CloseInsertControl()
    {
        if (NewResult != null)
        {
            NewResult.Dispose();
            NewResult = null;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClosingScript", 
            "$(document).ready(function(){CloseMe();});", true);
    }
    private void ScriptDocument()
    {
        var idUser = Session["UserId"];
        var url = string.Format("'../Search/DocViewer.aspx?docid={0}&doctype={1}&showoriginal={2}'", _result.ID.ToString(), _result.DocTypeId.ToString(), true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenDocTask2", "$(document).ready(function(){parent.OpenDocTaskfronMaster2(" + 0.ToString() + ',' + _result.ID.ToString() + ',' + _result.DocTypeId + ',' + "true" + ',' + "'" + _result.Name.ToString() + "'" + ',' + url.ToString() + "," + idUser.ToString() + "); } ); ", true);
    }

    /// <summary>
    /// Inserta un documento en base al INewResult almacenado en la session.
    /// </summary>
    private void InsertDocument()
    {
        try
        {
            NewResult.Indexs = FillNewResultIndex(NewResult.Indexs);
            if (NewResult.ID == 0)
            {
               
                     InsertResult insertresult = IndexDocument(NewResult,  true);

                if (insertresult == InsertResult.Insertado)
                {
                    lblDoc.Text = "El documento ha sido insertado correctamente";

                    OpenTask(NewResult);
                }
                else
                {
                    //Si no se insertó, se regenera el control.
                    if (!ShowAndSetInsertForm())
                    {
                        lblDoc.Text = "Ha ocurrido un error al cargar el formulario";
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblDoc.Text = "Ha ocurrido un error al insertar el formulario";
            if (!ShowAndSetInsertForm())
            {
                lblDoc.Text = "Ha ocurrido un error al cargar el formulario";
                return;
            }
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Genera el script necesario para abrir una tarea, en base a un INewResult.
    /// </summary>
    /// <param name="ResultToOpen">Result a abrir.</param>
    private void OpenTask(INewResult ResultToOpen)
    {
        STasks stasks = new STasks();
        //Obtiene la tarea para comprobar si lo es o es documento.
        if (ResultToOpen != null)
        {
            ITaskResult task = stasks.GetTaskByDocId(ResultToOpen.ID);
            stasks = null;

            string script = String.Empty;
            SUserPreferences sUserPreferences = new SUserPreferences();

            SZOptBusiness zOptBusines = new SZOptBusiness();
            string doctypeidsexc = zOptBusines.GetValue("DTIDShowDocAfterInsert");
            zOptBusines = null;

            bool opendoc = false;
            if (!string.IsNullOrEmpty(doctypeidsexc))
            {
                string[] dTidToShow = doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int maxIDs = dTidToShow.Length;

                for (int i = 0; i < maxIDs; i++)
                {
                    if (string.Compare(dTidToShow[i].Trim(), ResultToOpen.DocTypeId.ToString()) == 0)
                        opendoc = true;
                }
            }

            bool showDocAfterInsert = bool.Parse(sUserPreferences.getValue("ShowDocAfterInsert", Sections.InsertPreferences, "true").ToLower());
            sUserPreferences = null;

            if (showDocAfterInsert || opendoc)
            {
                //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                if (task != null)
                {
                    Page.Session.Add("Entrada" + ResultToOpen.ID, true);
                }

                string urlTask = "../WF/TaskSelector.ashx?docid=" + ResultToOpen.ID.ToString() + "&doctype=" + ResultToOpen.DocTypeId.ToString();
                script += "parent.SelectTaskFromModal();";
                script += string.Format("parent.OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6});", 0, ResultToOpen.ID, ResultToOpen.DocTypeId, "false", ResultToOpen.Name, urlTask, user.ID);
            }
            else
            {
                if (Session["ListOfTask"] == null)
                {
                    Session["ListOfTask"] = new List<IExecutionRequest>();
                }

                IExecutionRequest exec = new ExecutionRequest();
                exec.ExecutionTask = task;
                exec.StartRule = -1;
                ((List<IExecutionRequest>)Session["ListOfTask"]).Add(exec);

                script = "parent.SetNewEntryRulesGroup();";
            }

            if (Request["CallTaskID"] != null)
            {
                script += "parent.ShowLoadingAnimation(); parent.RefreshTab('#T" + Request["CallTaskID"] + "');";
            }

            script += "parent.CloseInsert();";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
            ScriptManager.RegisterClientScriptBlock(this, GetType(),
                                                    "DoOpenTaskScript", "$(document).ready(function(){ " + script + " });", true);
        }
    }

    /// <summary>
    /// Inserta un result específico.
    /// </summary>
    /// <param name="ResultToInsert">NewResult a insertar.</param>
  
    /// <param name="DisableAutomaticVersion">Parámetro no tenido en cuenta, evaluar si es necesario.</param>
    /// <returns>>Resultado de la insersión.</returns>
    private InsertResult IndexDocument(INewResult ResultToInsert,  bool DisableAutomaticVersion)
    {
        InsertResult InsertResult = InsertResult.NoInsertado;
        try
        {
            if (ResultToInsert != null)
            {
                       Int64 IncrementedValue = 0;

                sDocType sDocType = new sDocType();
                SIndex SIndex = new SIndex();
                SResult SResult = new SResult();

                Int64 auto;

                DataSet dsIndexsToIncrement = sDocType.GetIndexsProperties(ResultToInsert.DocTypeId, true);
                sDocType = null;

                DataRowCollection rows;
                if (dsIndexsToIncrement.Tables[0].Rows != null)
                {
                    rows = dsIndexsToIncrement.Tables[0].Rows;
                    int max = rows.Count;
                    int maxIndex = ResultToInsert.Indexs.Count;

                    DataRow CurrentRow;
                    IIndex CurrentIndex;
                    for (int i = 0; i < max; i++)
                    {
                        CurrentRow = rows[i];
                        if (Int64.TryParse(CurrentRow["Autoincremental"].ToString(), out auto) && auto == 1)
                        {
                            for (int j = 0; j < maxIndex; j++)
                            {
                                CurrentIndex = (IIndex)ResultToInsert.Indexs[j];
                                if (string.Compare(CurrentRow["Index_Name"].ToString().Trim(), CurrentIndex.Name.Trim()) == 0)
                                {
                                    if (CurrentIndex.Data.Trim() == string.Empty)
                                    {
                                        IncrementedValue = SIndex.SelectMaxIndexValue(CurrentIndex.ID, ResultToInsert.DocType.ID);
                                        CurrentIndex.Data = IncrementedValue.ToString();
                                        CurrentIndex.DataTemp = IncrementedValue.ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (string.Compare(CurrentRow["DefaultValue"].ToString(), string.Empty) != 0)
                            {
                                for (int j = 0; j < maxIndex; j++)
                                {
                                    CurrentIndex = (IIndex)ResultToInsert.Indexs[j];
                                    if (string.Compare(CurrentRow["Index_Name"].ToString().Trim(), CurrentIndex.Name.Trim()) == 0)
                                    {
                                        if (CurrentIndex.Data.Trim() == string.Empty)
                                        {
                                            CurrentIndex.Data = CurrentRow["DefaultValue"].ToString().Trim();
                                            CurrentIndex.DataTemp = CurrentRow["DefaultValue"].ToString().Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                InsertResult = SResult.Insert(ref ResultToInsert, false, false, false, false, ResultToInsert.ISVIRTUAL, false, false, true, false);
                SResult = null;
                SIndex = null;
            }
        }
        catch (Exception ex)
        {
            InsertResult = InsertResult.NoInsertado;
            Zamba.AppBlock.ZException.Log(ex);
            lblDoc.Text = "Ha ocurrido un error al insertar el formulario";
        }

        return InsertResult;
    }

    /// <summary>
    /// Parsea los valores del formulario en un ArrayList de índices.
    /// </summary>
    /// <param name="Indexs">Índices "plantilla" para parsear el formulario.</param>
    /// <returns>ArrayList de Índices con data lleno.</returns>
    private List<IIndex> FillNewResultIndex(List<IIndex> Indexs)
    {
        try
        {
            string indexValue;
            IIndex currIndex;
            int indexsCount = Indexs.Count;

            for (int i = 0; i < indexsCount; i++)
            {
                currIndex = (IIndex)Indexs[i];

                if (CurrentZFrom != null && CurrentZFrom.ContainsKey(currIndex.ID))
                    indexValue = getInputIndexValue(currIndex, true);
                else
                    indexValue = getInputIndexValue(currIndex, false);

                if (!string.IsNullOrEmpty(indexValue))
                {
                    Indexs[i] = SetIndexData(indexValue, currIndex);
                }
            }

            CurrentZFrom = new Dictionary<long, string>();

            return Indexs;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return Indexs;
        }
    }

    private static IIndex SetIndexData(string indexValue, IIndex currIndex)
    {
        int toSplitIndex;

        currIndex.Data = indexValue.Trim();
        currIndex.DataTemp = currIndex.Data;

        if (currIndex.DropDown != IndexAdditionalType.AutoSustitución && currIndex.DropDown != IndexAdditionalType.AutoSustituciónJerarquico)
        {
            if (currIndex.Type == IndexDataType.Si_No)
            {
                if (string.Compare(indexValue, "on") == 0)
                {
                    currIndex.Data = "1";
                    currIndex.DataTemp = "1";
                }
                else
                {
                    currIndex.Data = "0";
                    currIndex.DataTemp = "0";
                }
            }
        }
        else
        {
            toSplitIndex = indexValue.IndexOf(STR_SLSTPLAIN_SEPARATOR);
            if (toSplitIndex > -1)
            {
                currIndex.Data = indexValue.Substring(0, toSplitIndex + 1);
                currIndex.DataTemp = currIndex.Data;
                currIndex.dataDescription = indexValue.Replace(indexValue.Substring(0, toSplitIndex) + STR_SLSTPLAIN_SEPARATOR, string.Empty);
            }
            else
            {
                currIndex.Data = indexValue.Trim();
                currIndex.DataTemp = indexValue.Trim();
                currIndex.dataDescription = AutoSubstitutionBusiness.getDescription(indexValue.Trim(), currIndex.ID, false, currIndex.Type);
            }

            currIndex.dataDescriptionTemp = currIndex.dataDescription;
        }
        return currIndex;
    }

    //03/10/2011: Se agrega este método para salvar los datos que llegan desde el formulario.
    private void SaveFormData(bool IsTask)
    {
        if (_result == null) return;

        isReindex = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, _result.DocTypeId);
       
        if (isReindex)
        {
            List<long> listModifiedIndex = new List<long>();
            string indexValue = string.Empty;
            //Para autocompletar
            string oldDataValue = string.Empty;
            string oldDescriptionValue = string.Empty;
            bool changes = false;
            //Guarda las modificaciones hechas para el historial
            StringBuilder sbIndexHistory = new StringBuilder();
            sbIndexHistory.Append("Modificaciones realizadas en '" + _result.Name + "': ");
            DataTable dtModifiedIndex = new DataTable();
            dtModifiedIndex.Columns.Add("ID", typeof(Int64));
            dtModifiedIndex.Columns.Add("OldValue", typeof(String));
            dtModifiedIndex.Columns.Add("NewValue", typeof(String));

            try
            {
                _result.Indexs = new List<IIndex>((new SIndex().GetIndexs(_result.ID, _result.DocTypeId)));

                IIndex currIndex;
                int indexsCount = _result.Indexs.Count;

                for (int i = 0; i < indexsCount; i++)
                {
                    currIndex = (IIndex)_result.Indexs[i];

                    oldDataValue = currIndex.Data;
                    oldDescriptionValue = currIndex.dataDescription;

                    if (CurrentZFrom != null && CurrentZFrom.ContainsKey(currIndex.ID))
                        indexValue = getInputIndexValue(currIndex, true);
                    else
                        indexValue = getInputIndexValue(currIndex, false);

                    if (!string.IsNullOrEmpty(indexValue) && currIndex.Data.Trim() != indexValue.Trim())
                    {
                        listModifiedIndex.Add(currIndex.ID);
                        DataRow row = dtModifiedIndex.NewRow();
                        row["ID"] = currIndex.ID;
                        row["OldValue"] = currIndex.Data;
                        row["NewValue"] = indexValue.Trim();
                        dtModifiedIndex.Rows.Add(row);

                        currIndex = SetIndexData(indexValue, currIndex);
                    }

                    if (oldDataValue == null)
                    {
                        oldDataValue = String.Empty;
                    }
                    if (_result.ID != 0 && _result.Doc_File != null)
                    {
                        if (oldDataValue != null && String.CompareOrdinal(oldDataValue.Trim(), currIndex.Data.Trim()) != 0)
                        {
                            changes = true;
                            //Si existen cambios se guardan para el historial
                            if (String.IsNullOrEmpty(oldDescriptionValue))
                            {
                                sbIndexHistory.Append("índice '" + currIndex.Name + "' de '" + oldDataValue.Trim() + "' a '" + currIndex.Data.Trim() + "', ");
                            }
                            else
                            {
                                sbIndexHistory.Append("índice '" + currIndex.Name + "' de '" + oldDescriptionValue.Trim() + "' a '" + currIndex.dataDescription.Trim() + "', ");
                            }
                        }
                    }

                }

                if (listModifiedIndex.Count > 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando indices de documento: " + _result.Name);

                    Results_Business rb = new Results_Business();
                    rb.SaveModifiedIndexData(ref _result, true, true, listModifiedIndex, dtModifiedIndex);

                    //Guarda el historial
                    sbIndexHistory = sbIndexHistory.Remove(sbIndexHistory.Length - 2, 2);
                    if (changes)
                        UserBusiness.Rights.SaveAction(_result.ID, ObjectTypes.Documents, RightsType.ReIndex, sbIndexHistory.ToString(), 0);
                    if (IsTask)
                    {
                        SRules rulesS = new SRules();
                        List<IWFRuleParent> rules = rulesS.GetCompleteHashTableRulesByStep(TaskResult.StepId);

                        if (rules != null)
                        {
                            var ruleIDs = from rule in rules
                                          where rule.RuleType == TypesofRules.Indices
                                          select rule.ID;
                            List<ITaskResult> results = new List<ITaskResult>();
                            results.Add(_ITaskResult);

                            foreach (long rid in ruleIDs.ToList())
                            {
                                ExecuteRule(rid, results);
                            }
                        }
                    }
                }

                CurrentZFrom = new Dictionary<long, string>();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }
        else
        {
            string script = "$(document).ready(function(){ $('#divMessage').dialog({ closeOnEscape: false,open: function(event, ui) { $(\".ui-dialog-titlebar-close\").hide(); }, autoOpen: true, modal: true, position: 'top', resizable: false });" +
                "$('#divMessage').parent().css('top', '20px');" +
                "$('#divMessage').parent().css('position', 'absolute'); });";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ErrorMsg", script, true);
        }
    }

    //03/10/2011: Método para obtener valores de un determinado índice en el formulario.
    private string getInputIndexValue(IIndex index, bool indexOnForm)
    {
        string returnValue = null;
        System.Collections.Specialized.NameValueCollection theForm = HttpContext.Current.Request.Form;

        if (theForm["ZAMBA_INDEX_" + index.ID] != null)
        {
            returnValue = theForm.GetValues("ZAMBA_INDEX_" + index.ID)[0];
        }
        else if (theForm["ZAMBA_INDEX_" + index.Name] != null)
        {
            returnValue = theForm.GetValues("ZAMBA_INDEX_" + index.Name)[0];
        }
        else if (theForm["ZAMBA_INDEX_" + index.ID + "S"] != null)
        {
            returnValue = theForm.GetValues("ZAMBA_INDEX_" + index.ID + "S")[0];
        }
        else if (theForm["ZAMBA_INDEX_" + index.ID + "N"] != null)
        {
            returnValue = theForm.GetValues("ZAMBA_INDEX_" + index.ID + "N")[0];
        }//Si el indice esta en el formulario y no es del tipo desplegable, le asigna valor por defecto.(Si es deplegable no aparece en el form si esta deshabilitado, pero tampoco debe guardarlo)
        else if (indexOnForm)
        {
            switch (index.Type)
            {
                case IndexDataType.None:
                    returnValue = string.Empty;
                    break;
                case IndexDataType.Si_No:
                    returnValue = "0";
                    break;
            }
        }

        return returnValue;
    }

    private IWFRuleParent CheckChildRules(IWFRuleParent _rule)
    {
        IWFRuleParent raux = null;

        foreach (IWFRuleParent _ruleaux in _rule.ChildRules)
        {
            raux = this.CheckChildRules(_ruleaux);
        }

        if (_rule.GetType().FullName == "Zamba.WFActivity.Regular.DoShowForm")
        {
            raux = (IWFRuleParent)_rule;
            ((IDoShowForm)_rule).RuleParentType = TypesofRules.AbrirDocumento;
        }

        return raux;
    }

    public void ShowDocumentFromDoShowForm(Int64 formId)
    {
        formIdFromDoShowForm = formId;
        ShowDocumentSinceRuleDOShowForm(formId);
    }

    private void ShowDocumentSinceRuleDOShowForm(Int64 formIDOfTheRule)
    {
        TaskResult.CurrentFormID = formIDOfTheRule;

        if (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") == false)
        {
            ShowFormOfDOShowForm();
        }
        else
        {
            ShowAssociatedFormOfDOShowForm();
        }
    }

    private void ShowFormOfDOShowForm()
    {
        try
        {
            if (TaskResult.CurrentFormID > 0)
            {
                SForms Forms = new SForms();
                IZwebForm Form = Forms.GetForm(TaskResult.CurrentFormID);

                if (Form.Type != FormTypes.Edit)
                    Form.Type = FormTypes.Show;

                navigateToForm(Form, Form.Type.ToString());
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return;
        }
    }

    private void ShowAssociatedFormOfDOShowForm()
    {
        ArrayList ResultsAsociated = null;
        IResult AsociatedResult = null;

        SDocAsociated SDocAsociated = new SDocAsociated();
        SUserPreferences SUserPreferences = new SUserPreferences();
        SForms SForms = new SForms();

        // Se obtienen los documentos asociados
        ResultsAsociated = SDocAsociated.getAsociatedResultsFromResult(TaskResult, Int32.Parse(SUserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)));

        // Se recorren los documentos asociados para buscar el que coincide con el form configurado en la regla
        foreach (IResult asociatedR in ResultsAsociated)
        {
            // Si la variable doc_id no está vacía
            if (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview"))
            {
                // Se busca el valor que contiene la variable doc_id (id del new result que se creo en el play de la regla DOCreateForm) 
                long id_VarDocId = Convert.ToInt64(VariablesInterReglas.get_Item("AsociatedDocIdForPreview"));

                if (asociatedR.ID == id_VarDocId)
                {
                    // Se obtiene el formID
                    asociatedR.CurrentFormID = SDocAsociated.getAsociatedFormId(Convert.ToInt32(asociatedR.DocType.ID));
                    AsociatedResult = asociatedR;
                    break;
                }
            }
        }

        try
        {
            IZwebForm[] Forms = SForms.GetAllForms(Convert.ToInt32(AsociatedResult.DocType.ID));

            if (Forms != null && Forms.Length > 0)
            {
                if (AsociatedResult != null)
                {
                    foreach (IZwebForm F in Forms)
                    {
                        if (F.ID == AsociatedResult.CurrentFormID)
                        {
                            TaskResult = (ITaskResult)AsociatedResult;
                            navigateToForm(F, F.Type.ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void ShowDocument()
    {
        IZwebForm[] Forms;
        string strHtml = string.Empty;
        int i;
        SForms SForms = new SForms();
        DataSet ds;
        docViewer.Visible = false;

        //Ver si es formulario virtual
        Forms = SForms.GetShowAndEditForms((int)_result.DocTypeId);

        if (Forms != null)
        {
            //Verifica si hay más de un formulario para mostrar y se analizan las condiciones para mostrarlos
            if (Forms.Length > 2)
            {
                for (i = 0; i <= Forms.Length - 1; i++)
                {
                    ds = FormBusiness.GetDynamicFormIndexsConditions(Forms[i].ID);

                    //Valido que el formulario tenga condiciones aplicadas
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Zamba.Core.Result result = (Result)_result;
                        if (FormBusiness.EvaluateDynamicFormConditions(ref result, ds))
                        {
                            if (navigateToForm(Forms[i], "1") == true)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            IZwebForm theForm = null;
            isReindex = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, _result.DocTypeId);

            if (isReindex)
            {
                theForm = SearchFromInArray(Forms, FormTypes.WebEdit);
                if (theForm == null)
                {
                    theForm = SearchFromInArray(Forms, FormTypes.Edit);

                    if (theForm == null)
                    {
                        theForm = SearchFromInArray(Forms, FormTypes.WebShow);

                        if (theForm == null)
                            theForm = SearchFromInArray(Forms, FormTypes.Show);
                    }
                }
            }
            else
            {
                theForm = SearchFromInArray(Forms, FormTypes.WebShow);

                if (theForm == null)
                    theForm = SearchFromInArray(Forms, FormTypes.Show);
            }

            if (navigateToForm(theForm, theForm.Type.ToString()))
                return;
        }
    }

    public IZwebForm SearchFromInArray(IZwebForm[] Forms, FormTypes Type)
    {
        IZwebForm formToReturn = null;
        int max = Forms.Length;
        for (int i = 0; i < max; i++)
        {
            if (Forms[i].Type == Type)
            {
                formToReturn = Forms[i];
                break;
            }
        }
        return formToReturn;
    }

    private bool navigateToForm(IZwebForm form, string typeForm)
    {
        try
        {
            user = Zamba.Membership.MembershipHelper.CurrentUser;

            SForms SForms = new SForms();
            SUserPreferences SUserPreferences = new SUserPreferences();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!string.IsNullOrEmpty(form.Path))
            {
                try
                {
                    string CurrentZFrom = LoadZForm(form);

                    docViewer.Text = " ";
                    //string sforceBlob = ZOptBusiness.GetValue("ForceBlob");
                    //bool forceBlob = (string.IsNullOrEmpty(sforceBlob)) ? false : bool.Parse(sforceBlob);

                    if (CurrentZFrom.ToLower().Contains("iframe"))
                    {
                        List<IDtoTag> listaTags = new List<IDtoTag>();

                        //Si tiene un iframe, busco el documento asociado
                        MatchCollection matches = default(MatchCollection);
                        matches = SForms.ParseHtml(CurrentZFrom, "iframe");
                        listaTags.Clear();

                        //Entrar por aca si el html tiene la palabra iframe
                        if (matches != null)
                        {
                            foreach (Match item in matches)
                            {
                                if (SForms.buscarHtmlIframe(item))
                                {
                                    Int64 id = SForms.buscarTagZamba(item);
                                    bool useOriginal = false;

                                    if (id == -1)
                                    {
                                        if (!string.IsNullOrEmpty(_result.FullPath) || !string.IsNullOrEmpty(_result.Doc_File))
                                        {
                                            string Path = string.Format(GetDocFileUrl, _result.DocTypeId, _result.ID, Session["UserId"]);

                                            if (_result.IsMsg && (File.Exists(_result.FullPath.ToLower().Replace(".msg", ".html")) || File.Exists(_result.FullPath.ToLower().Replace(".msg", ".txt"))))
                                            {
                                                DirectoryInfo DI = new DirectoryInfo(System.IO.Path.GetDirectoryName(_result.FullPath));
                                                System.IO.FileInfo[] fileList = DI.GetFiles();
                                                List<System.IO.FileInfo> fList = new List<System.IO.FileInfo>();

                                                foreach (System.IO.FileInfo fItem in fileList)
                                                {
                                                    if (fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(_result.FullPath)) && !fItem.Name.Trim().ToLower().EndsWith(".msg"))
                                                    {
                                                        fList.Add(fItem);
                                                    }
                                                }

                                                if (fList.Count > 0)
                                                {
                                                    Path = fList[0].FullName;
                                                }
                                            }

                                            string tag = item.Value;
                                            SForms.replazarAtributoSrc(ref tag, Path);
                                            IDtoTag dto = SForms.instanceDtoTag(item.Value, tag);
                                            listaTags.Add(dto);
                                            useOriginal = true;
                                        }
                                        else
                                        {
                                            //Si el fullpath esta vacio y el item.Value contiene datos, se busca
                                            //la propiedad src y se la remueve para mostrar un iframe en blanco.
                                            if (item != null && item.Value != null)
                                            {
                                                //Se intenta remover el atributo src.
                                                string tag = SForms.RemoveSrcTag(item.Value);
                                                //Si existieron cambios realizo la modificacion.
                                                if (string.Compare(item.Value, tag) != 0)
                                                {
                                                    IDtoTag dto = SForms.instanceDtoTag(item.Value, tag);
                                                    listaTags.Add(dto);
                                                }

                                                useOriginal = true;
                                            }
                                        }
                                    }
                                    else if (id == 0)
                                    {
                                        useOriginal = true;
                                    }

                                    if (!useOriginal)
                                    {
                                        ArrayList docTypesAsocList = null;
                                        SDocAsociated SDocAsociated = new SDocAsociated();

                                        //Busca el documento asociado                                    
                                        docTypesAsocList = SDocAsociated.getAsociatedResultsFromResult(_result, Int32.Parse(SUserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)));

                                        if (docTypesAsocList != null && docTypesAsocList.Count > 0)
                                        {
                                            foreach (object docAsoc in docTypesAsocList)
                                            {
                                                if (docAsoc is IResult)
                                                {
                                                    IResult myResult = (IResult)docAsoc;

                                                    string path = string.Empty;
                                                    string tag = item.Value;

                                                    //Verifica que sea el DocType correcto
                                                    if (myResult.DocTypeId == id || id == 0)
                                                    {
                                                        if (myResult.ISVIRTUAL)
                                                        {
                                                            //Se obtiene el id del formulario actual
                                                            myResult.CurrentFormID = SDocAsociated.getAsociatedFormId(Convert.ToInt32(myResult.DocType.ID));

                                                            //Agrego una validacion para si no hay form asociado, no tire error - MC
                                                            if (myResult.CurrentFormID != 0)
                                                            {
                                                                //Reemplaza el atributo id
                                                                SForms.replazarAtributoId(ref tag, myResult.ID);
                                                            }
                                                        }

                                                        if (myResult.IsMsg && (File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".html")) || File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".txt"))))
                                                        {
                                                            if (bool.Parse(SUserPreferences.getValue("OpenMsgFileInIFrame", Sections.FormPreferences, "False")))
                                                            {
                                                                DirectoryInfo DI = new DirectoryInfo(System.IO.Path.GetDirectoryName(myResult.FullPath));
                                                                System.IO.FileInfo[] fileList = DI.GetFiles();
                                                                List<System.IO.FileInfo> fList = new List<System.IO.FileInfo>();

                                                                foreach (System.IO.FileInfo fItem in fileList)
                                                                {
                                                                    if (fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(myResult.FullPath)) && !fItem.Name.Trim().ToLower().EndsWith(".msg"))
                                                                    {
                                                                        fList.Add(fItem);
                                                                    }
                                                                }
                                                                if (fList.Count > 0)
                                                                {
                                                                    path = fList[0].FullName;
                                                                }
                                                            }
                                                        }

                                                        //Reemplaza el atributo src
                                                        SForms.replazarAtributoSrc(ref tag, path);

                                                        IDtoTag dto = SForms.instanceDtoTag(item.Value, tag);

                                                        listaTags.Add(dto);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            CurrentZFrom = SForms.actualizarHtml(listaTags, CurrentZFrom);
                        }
                    }

                    String strBody;
                    String strHead;

                    ZOptBusiness zoptb = new ZOptBusiness();
                    String CurrentTheme = zoptb.GetValue("CurrentTheme");
                    zoptb = null;

                    strBody = HTML.getHtmlSection(CurrentZFrom, HTML.HTMLSection.FORM); //pido solo lo que esta en el form para no incluirlo ya que en asp.net ya se tiene un formulario
                    strHead = HTML.getHtmlSection(CurrentZFrom, HTML.HTMLSection.HEAD);

                    if (IsShowing)
                    {
                        strBody = CompleteFormIndexs(_result, strBody);
                        strBody = CompleteFormAsociates(_result, strBody);
                        strBody = CompleteFormZVar(_result, strBody);
                    }
                    else
                    {
                        //Setea la configuacion de la regla, pasada en el query string
                        SetRuleConfiguration();
                        strBody = CompleteFormIndexs(NewResult, strBody);
                        strBody = CompleteFormAsociates(NewResult, strBody);
                        strBody = CompleteFormZVar(NewResult, strBody);
                    }

                    //Verifica la existencia del div que contiene las tablas SLST
                    if (!strBody.Contains("dynamic_filter"))
                    {
                        strBody = CompleteSlstDiv(strBody);
                    }

                    //Completar las condiciones dinámicas
                    string conditionScripts;
                    if (IsShowing)
                    {
                        conditionScripts = new StringBuilder().AppendFormat(HTML.DOCUMENT_READY_FORMAT,
                            HTML.MakeDynamicConditions(strBody, this.CurrentForm.ID, _result.Indexs)).ToString();
                    }
                    else
                    {
                        conditionScripts = new StringBuilder().AppendFormat(HTML.DOCUMENT_READY_FORMAT,
                            HTML.MakeDynamicConditions(strBody, this.CurrentForm.ID, NewResult.Indexs)).ToString();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DynamicConditionScript", conditionScripts, true);

                    docViewer.Visible = true;
                    docViewer.Text = strBody;

                    this.Page.Master.Page.Header.Controls.Add(new LiteralControl(strHead));
                }
                catch (Exception ex)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                    return false;
                }
            }
            else
            {
                ZOptBusiness zoptb = new ZOptBusiness();
                String CurrentTheme = zoptb.GetValue("CurrentTheme");
                zoptb = null;

                lblDoc.Text = "<br><br>No se ha podido acceder al documento para su visualizacion: " + Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "\\" + form.Path;
                lblDoc.ForeColor = System.Drawing.Color.Red;
            }

            return true;
        }
        catch (System.Threading.ThreadAbortException exth)
        {
            Zamba.AppBlock.ZException.Log(exth);
            return false;
        }
    }

    private string LoadZForm(IZwebForm form)
    {
        ZOptBusiness zoptb = new ZOptBusiness();
        String CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        String rutaTemp = Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + form.Path.Substring(form.Path.LastIndexOf("\\"));

        //Copia el formulario si no existe
        if (!File.Exists(rutaTemp))
            FormBusiness.CopyWebForm(form, rutaTemp);

        this.CurrentForm = form;

        //Leo el archivo
        StreamReader str = new StreamReader(rutaTemp);
        StreamReader a = new StreamReader(str.BaseStream);

        string strHtml = a.ReadToEnd();

        str.Close();
        str.Dispose();
        a.Close();
        a.Dispose();
        return strHtml;
    }

    /// <summary>
    /// Carga el contenido de un DataTabla en el body de una tabla Html del documento
    /// </summary>
    /// <param name="table">Tabla HTML donde se cargaran los datos</param>
    /// <param name="drs">Rows que van a ser cargadas</param>
    /// <param name="mydoc">Documento HTML que contiene la tabla</param>
    /// <remarks></remarks>
    private string LoadTableBody(String table, DataRowCollection drs)
    {
        String CurrentRow = String.Empty;
        Int32 i;

        SUserPreferences SUserPreferences = new SUserPreferences();

        foreach (DataRow dr in drs)
        {
            CurrentRow = CurrentRow + "<tr>";
            i = 0;

            String DocTypeId = String.Empty;
            String DocId = String.Empty;
            String TaskId = String.Empty;

            foreach (object CellValue in dr.ItemArray)
            {
                if (i == dr.ItemArray.Length - 1)
                {
                    DocId = Server.HtmlEncode(CellValue.ToString());
                }
                else if (i == dr.ItemArray.Length - 2)
                {
                    DocTypeId = Server.HtmlEncode(CellValue.ToString());
                }

                if (i == dr.ItemArray.Length - 1 && bool.Parse(SUserPreferences.getValue("ResultId", Sections.FormPreferences, "True")) == false)
                {
                    //Ezequiel: Si la columna docid esta para que no se vea en la grilla pongo la columna como no visible.
                    CurrentRow = CurrentRow + "<td style=\"display:none\">" + Server.HtmlEncode(CellValue.ToString()) + "</td>";
                }
                else if (i == dr.ItemArray.Length - 2 && bool.Parse(SUserPreferences.getValue("DoctypeId", Sections.FormPreferences, "True")) == false)
                {
                    DocTypeId = Server.HtmlEncode(CellValue.ToString());
                }
                else if (i == dr.ItemArray.Length - 3 && bool.Parse(SUserPreferences.getValue("RutaDocumento", Sections.FormPreferences, "False")) == false)
                {
                }
                else
                {
                    if (CellValue is DateTime)
                    {
                        string date = CellValue.ToString();
                        if (date.Length > 10)
                            date = date.Substring(0, 10);
                        CurrentRow = CurrentRow + "<td>" + date + "</td>";
                    }
                    else
                        CurrentRow = CurrentRow + "<td>" + Server.HtmlEncode(CellValue.ToString()) + "</td>";
                }

                i = i + 1;
            }

            //se agrega una columna mas con el link.
            CurrentRow = CurrentRow + "</tr>";
            StringBuilder Link = new StringBuilder();
            Link.Append("<tr class=\"FormRowStyle\"><td style='text-decoration: none'  width='20'><a href=" + Convert.ToChar(34));
            Link.Append(MembershipHelper.Protocol);
            Link.Append(Request.ServerVariables["HTTP_HOST"]);
            Link.Append(Request.ApplicationPath);
            Link.Append("/Views/WF/TaskSelector.ashx?docid=");
            Link.Append(DocId);
            Link.Append("&doctypeid=");
            Link.Append(DocTypeId);
            Link.Append(Convert.ToChar(34));
            Link.Append(" style=" + Convert.ToChar(34) + "text-decoration: none" + Convert.ToChar(34) + " ><img height='20' src='");
            Link.Append(Tools.GetProtocol(HttpContext.Current.Request));
            Link.Append(Request.ServerVariables["HTTP_HOST"]);
            Link.Append(Request.ApplicationPath);
            Link.Append("/Content/Images/Toolbars/play.png' border='0'/> </a></td><td>");
            CurrentRow = CurrentRow.Replace("<tr><td>", Link.ToString());
        }
        return table + CurrentRow;
    }

    /// <summary>
    /// Convierte el contenido de un listado de results en un Datatable
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    /// <history> 
    ///     [Gaston]    19/11/2008     Modified     Se agrego la columna "Estado" y verificación del documento asociado para ver si es una tarea
    ///     [Gaston]    20/11/2008     Modified     Se agrego la columna "Usuario Asignado" 
    ///     [Gaston]    05/01/2009     Modified     Verificación de la columna "Nombre del Documento" en el UserPreferences para mostrar o ocultar 
    ///                                             dicha columna
    ///     [Gaston]    06/01/2009     Modified     Validación del valor de "Nombre del Documento" y código comentado en donde se intenta colocar
    ///                                             un String.Empty en una columna que no existe
    ///     Marcelo     05/02/2009     Modified     Se modifico la carga de los indices para mejorar la performance 
    ///     Marcelo     06/01/2010     Modified     Se agrego variable para cargar solo las tareas que esten en WF
    /// </history>
    public DataTable ParseResult(List<IResult> results, bool onlyWF, string tableId)
    {
        //En caso de que el contendido sea vacio retornamos una tabla vacia
        if (results.Count == 0)
            return new DataTable();

        DataTable Dt = new DataTable();
        Dictionary<Int64, Int64> taskids = new Dictionary<Int64, Int64>();

        STasks STasks = new STasks();
        SRights SRights = new SRights();
        SUserPreferences SUserPreferences = new SUserPreferences();

        Char mander = Char.Parse("§");
        Char pipe = Char.Parse("/");
        Char equal = Char.Parse("=");

        //Dt.Columns.Add(new DataColumn("Ver"));

        if (string.IsNullOrEmpty(tableId) == false)
        {
            if (tableId.Split(mander).Length <= 1)
                tableId = string.Empty;
            else
                foreach (string btn in tableId.Split(mander))
                {
                    String[] items = btn.Split(pipe);
                    Dt.Columns.Add(items[1].ToString());
                }
        }

        if ((bool.Parse(SUserPreferences.getValue("NameDocument", Sections.FormPreferences, "True")) == true))
        {
            Dt.Columns.Add(new DataColumn("Nombre del Documento"));
        }

        if ((bool.Parse(SUserPreferences.getValue("State", Sections.FormPreferences, "True")) == true))
        {
            Dt.Columns.Add(new DataColumn("Estado"));
        }

        if ((bool.Parse(SUserPreferences.getValue("UserAsigned", Sections.FormPreferences, "True")) == true))
        {
            Dt.Columns.Add(new DataColumn("Usuario Asignado"));
        }

        try
        {
            Type CurrentIndexType = null;

            //Cargo todos los indices de todos los results , como pueden ser diferentes tipos de documento recorro todos
            //Solo visualizo en la tabla los indices sobre los cuales tiene permiso el documento. Mariela
            Int64 lastDocTypeId = 0;
            Hashtable IRI = null;

            foreach (IResult CurrentResult in results)
            {
                // Se verifica si el documento es una tarea
                Int64 TaskId = -1;

                List<Int64> taskIdList = STasks.GetTaskIDsByDocId(CurrentResult.ID);

                if ((taskIdList != null) && taskIdList.Count == 1)
                {
                    TaskId = taskIdList[0];
                    taskids.Add(CurrentResult.ID, TaskId);
                }

                if (onlyWF == false | TaskId != -1)
                {
                    //Guardo el entidad anterior asi no recargo los indices mas veces de las necesarias
                    if (lastDocTypeId != CurrentResult.DocTypeId)
                    {
                        lastDocTypeId = CurrentResult.DocTypeId;
                        //26/10/2011: Se cambia la forma de obtener los permisos por indices a la misma que en windows. Dado que no los obtenia en forma correcta
                        IRI = UserBusiness.Rights.GetAssociatedIndexsRightsCombined(this._result.DocTypeId, lastDocTypeId, user.ID, true);
                    }

                    foreach (IIndex CurrentIndex in CurrentResult.Indexs)
                    {
                        bool ShowIndex = false;
                      
                        if (IRI.ContainsKey(CurrentIndex.ID))
                        {
                            AssociatedIndexsRightsInfo IR = (AssociatedIndexsRightsInfo)IRI[CurrentIndex.ID];

                                    //26/10/2011: Se cambia el tipo de permiso a buscar.
                                    if (IR.GetIndexRightValue(RightsType.AssociateIndexView))
                                        ShowIndex = true;
                        }
                        else
                        {
                            ShowIndex = true;
                        }

                        if (ShowIndex)
                        {
                            if (!Dt.Columns.Contains(CurrentIndex.Name))
                            {
                                CurrentIndexType = typeof(string);

                                if (CurrentIndex.DropDown == IndexAdditionalType.LineText)
                                {
                                    CurrentIndexType = GetIndexType(CurrentIndex.Type);
                                }
                                else
                                {
                                    CurrentIndexType = typeof(string);
                                }

                                Dt.Columns.Add(CurrentIndex.Name.Trim(), CurrentIndexType);
                            }
                        }
                    }
                }
            }

            if (bool.Parse(SUserPreferences.getValue("FechaCreacion", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Fecha Creacion"));
            }
            if (bool.Parse(SUserPreferences.getValue("TipodeDocumento", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Entidad"));
            }
            if (bool.Parse(SUserPreferences.getValue("FechaModificacion", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Fecha Modificacion"));
            }
            if (bool.Parse(SUserPreferences.getValue("NombreOriginal", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Nombre Original"));
            }
            if (bool.Parse(SUserPreferences.getValue("NumerodeVersion", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Numero de Version"));
            }
            if (bool.Parse(SUserPreferences.getValue("ParentId", Sections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("ParentId"));
            }
         

            Dt.Columns.Add(new DataColumn("Ruta Documento"));
            Dt.Columns.Add(new DataColumn("DoctypeId"));
            Dt.Columns.Add(new DataColumn("IdDoc", typeof(Int64)));
            Dt.AcceptChanges();

            DataRow CurrentRow = null;

            foreach (IResult CurrentResult in results)
            {
                // Se verifica si el documento es una tarea
                if (onlyWF == false | taskids.ContainsKey(CurrentResult.ID))
                {
                    CurrentRow = Dt.NewRow();

                    if (bool.Parse(SUserPreferences.getValue("NameDocument", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Nombre del Documento"] = CurrentResult.Name;
                    }

                    CurrentRow["IdDoc"] = CurrentResult.ID;
                    CurrentRow["Ruta Documento"] = CurrentResult.FullPath;

                    if (bool.Parse(SUserPreferences.getValue("FechaCreacion", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Fecha Creacion"] = (DateTime)CurrentResult.CreateDate;
                    }
                    if (bool.Parse(SUserPreferences.getValue("TipodeDocumento", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Entidad"] = CurrentResult.Parent.Name;
                    }
                    if (bool.Parse(SUserPreferences.getValue("FechaModificacion", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Fecha Modificacion"] = (DateTime)CurrentResult.EditDate;
                    }
                    if (bool.Parse(SUserPreferences.getValue("NumerodeVersion", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Numero de Version"] = CurrentResult.VersionNumber;
                    }
                    if (bool.Parse(SUserPreferences.getValue("ParentId", Sections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["ParentId"] = CurrentResult.ParentVerId;
                    }

                    CurrentRow["DoctypeId"] = CurrentResult.DocType.ID;

                 
                    Type IndexType = typeof(string);

                    foreach (IIndex CurrentIndex in CurrentResult.Indexs)
                    {
                        IndexType = GetIndexType(CurrentIndex.Type);

                        try
                        {
                            //Si Data tiene un valor que se le asigne al Item
                            if (!string.IsNullOrEmpty(CurrentIndex.Data) && CurrentRow.Table.Columns.Contains(CurrentIndex.Name))
                            {
                                if (CurrentIndex.DropDown == IndexAdditionalType.LineText)
                                {
                                    if (CurrentIndex.Type == IndexDataType.Si_No)
                                    {
                                        if (int.Parse(CurrentIndex.Data) == 0)
                                        {
                                            CurrentRow[CurrentIndex.Name] = "No";
                                        }
                                        else
                                        {
                                            CurrentRow[CurrentIndex.Name] = "Si";
                                        }
                                    }
                                    else
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.Data;
                                    }

                                }
                                else
                                {
                                    if (string.Compare(string.Empty, CurrentIndex.dataDescription) != 0)
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.dataDescription;
                                    }
                                    else
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.Data;
                                    }
                                }

                                //Si Data no tiene valor se le asigna el de DataDescription
                                //(si es que no esta vacío)
                            }
                            else if (!string.IsNullOrEmpty(CurrentIndex.dataDescription) && CurrentRow.Table.Columns.Contains(CurrentIndex.Name))
                            {
                                CurrentRow[CurrentIndex.Name] = CurrentIndex.dataDescription;
                                //Si no hay valor para asignar no hace nada
                            }
                        }
                        catch (Exception ex)
                        {
                            Zamba.AppBlock.ZException.Log(ex);
                        }
                    }

                    if (taskids.ContainsKey(CurrentResult.ID))
                    {
                        DataSet dsTask = STasks.GetTaskDs(taskids[CurrentResult.ID]);

                        // Si el documento es una tarea entonces se coloca el estado actual de la tarea, sino, el estado se coloca como vacío
                        if (dsTask != null && dsTask.Tables.Count == 1 && dsTask.Tables[0].Rows.Count >= 1)
                        {
                            if ((bool.Parse(SUserPreferences.getValue("State", Sections.FormPreferences, "True")) == true))
                            {
                                SStepStates SStepStates = new SStepStates(ref user);
                                CurrentRow["Estado"] = SStepStates.GetStateName(Int32.Parse(dsTask.Tables[0].Rows[0]["Do_State_Id"].ToString()));
                            }

                            if ((bool.Parse(SUserPreferences.getValue("UserAsigned", Sections.FormPreferences, "True")) == true))
                            {
                                SUserGroup SUserGroup = new SUserGroup(ref user);
                                CurrentRow["Usuario Asignado"] = SUserGroup.GetUserorGroupNamebyId(Int32.Parse(dsTask.Tables[0].Rows[0]["User_Asigned"].ToString()));
                            }

                            dsTask.Dispose();
                            dsTask = null;
                        }
                    }

                    //Nombre del documento
                    if (bool.Parse(SUserPreferences.getValue("NombreOriginal", Sections.FormPreferences, "True")) == true)
                    {
                        string FileName = CurrentResult.OriginalName;

                        if (FileName == null)
                            FileName = CurrentResult.Name;

                        Int32 indexpath = FileName.LastIndexOf("\\");

                        if (indexpath == -1 || FileName.Length - 1 == -1)
                        {
                        }
                        else
                        {
                            if (indexpath == -1)
                                indexpath = 0;
                            try
                            {
                                FileName = FileName.Substring(indexpath + 1, FileName.Length - indexpath - 1);
                            }
                            catch
                            {
                                FileName = CurrentResult.OriginalName;
                            }
                        }
                        CurrentRow["Nombre Original"] = FileName;
                    }

                    StringBuilder InnerHtml = new StringBuilder();

                    if (string.IsNullOrEmpty(tableId) == false)
                    {
                        string textItem2 = null;
                        string textAux = null;

                        if (tableId.Split(mander).Length <= 1)
                            tableId = string.Empty;
                        else
                            foreach (string btn in tableId.Split(mander))
                            {
                                InnerHtml.Remove(0, InnerHtml.Length);
                                String[] items = btn.Split(pipe);
                                Int32 itemNum = default(Int32);
                                String[] zvarItems = null;
                                string @params = null;

                                InnerHtml.Append("&nbsp;<INPUT id=");
                                InnerHtml.Append(Convert.ToChar(34));

                                //Si tiene zvar
                                if (items.Length > 2)
                                {
                                    textItem2 = items[2].ToString();
                                    InnerHtml.Append(items[0] + "_");

                                    while (string.IsNullOrEmpty(textItem2) == false)
                                    {
                                        textAux = textItem2.Remove(0, 5);
                                        zvarItems = textAux.Remove(textAux.IndexOf(")")).Split(equal);
                                        textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1);

                                        if (Int32.TryParse(zvarItems[1].ToString(), out itemNum) == false)
                                        {
                                            if (zvarItems[1].ToString().ToLower().Contains("length"))
                                            {
                                                itemNum = Dt.Columns.Count - Int32.Parse(zvarItems[1].ToString().Split(Char.Parse("-"))[1]);
                                            }
                                        }
                                        InnerHtml.Append("zvar(" + zvarItems[0].ToString() + "=" + CurrentRow[itemNum].ToString() + ")");

                                        @params = @params + "'" + CurrentRow.ItemArray[itemNum].ToString() + "',";
                                    }
                                }
                                else
                                {
                                    InnerHtml.Append(items[0]);
                                }

                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" type=button onclick=");

                                //Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                                //sino se llama a SetRuleId por default
                                if (items.Length > 3)
                                {
                                    InnerHtml.Append(Convert.ToChar(34));
                                    InnerHtml.Append(items[3] + "(this, ");
                                    InnerHtml.Append(@params.Substring(0, @params.Length - 1).Replace("\\", "\\\\"));
                                    InnerHtml.Append(");");
                                    InnerHtml.Append(Convert.ToChar(34));
                                }
                                else
                                {
                                    InnerHtml.Append(Convert.ToChar(34));
                                    InnerHtml.Append("SetRuleId(this);");
                                    InnerHtml.Append(Convert.ToChar(34));
                                }

                                InnerHtml.Append(" value = ");
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(items[1]);
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" Name = ");
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(items[0]);
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" >");

                                CurrentRow[items[1]] = InnerHtml.ToString();
                                @params = string.Empty;
                            }
                    }

                    InnerHtml = null;
                    Dt.Rows.Add(CurrentRow);
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }

        Dt.AcceptChanges();
        Dt.DefaultView.Sort = "IdDoc DESC";

        return Dt.DefaultView.ToTable();
    }

    /// <summary>
    /// Castea el tipo de un índice a Type
    /// </summary>
    /// <param name="indexType"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private Type GetIndexType(IndexDataType indexType)
    {
        Type ParsedIndexType = null;

        switch (indexType)
        {
            case IndexDataType.Alfanumerico:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Alfanumerico_Largo:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Fecha:
                ParsedIndexType = typeof(System.DateTime);
                break;
            case IndexDataType.Fecha_Hora:
                ParsedIndexType = typeof(DateTime);
                break;
            case IndexDataType.Moneda:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.None:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Numerico:
                ParsedIndexType = typeof(Int64);
                break;
            case IndexDataType.Numerico_Decimales:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.Numerico_Largo:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.Si_No:
                ParsedIndexType = typeof(string);
                break;
            default:
                ParsedIndexType = typeof(string);
                break;
        }

        return ParsedIndexType;
    }

    /// <summary>
    /// Carga las columnas header de un DataTable en el una tabla Html del documento
    /// </summary>
    /// <param name="table"></param>
    /// <param name="dcs"></param>
    /// <remarks></remarks>
    private String LoadTableHeader(String table, DataColumnCollection dcs)
    {
        SUserPreferences SUserPreferences = new SUserPreferences();

        //se agrega una columna para el link de abrir la tarea
        String HeaderRow = "<tr><th width='20'></th>";

        String HeaderColumn = string.Empty;

        ////Agrego columnas de atributos
        foreach (DataColumn Column in dcs)
        {
            if (string.Compare(Column.ColumnName.ToLower(), "iddoc") == 0 && bool.Parse(SUserPreferences.getValue("ResultId", Sections.FormPreferences, "True")) == false)
            {

                HeaderColumn = "<th style=\"display:none\">" + Column.ColumnName + "</th>";
                HeaderRow = HeaderRow + HeaderColumn;
                continue;
            }

            if (string.Compare(Column.ColumnName.ToLower(), "nombre del documento") == 0 && bool.Parse(SUserPreferences.getValue("NameDocument", Sections.FormPreferences, "True")) == false)
            {

                HeaderColumn = "<th style=\"display:none\">" + Column.ColumnName + "</th>";
                HeaderRow = HeaderRow + HeaderColumn;
            }
            else
                if (string.Compare(Column.ColumnName.ToLower(), "doctypeid") == 0 && bool.Parse(SUserPreferences.getValue("DoctypeId", Sections.FormPreferences, "True")) == false)
                {
                }
                else
                    if (string.Compare(Column.ColumnName.ToLower(), "ruta documento") == 0 && bool.Parse(SUserPreferences.getValue("RutaDocumento", Sections.FormPreferences, "False")) == false)
                    {
                    }
                    else
                    {
                        HeaderColumn = "<th>" + Column.ColumnName + "</th>";

                        HeaderRow = HeaderRow + HeaderColumn;
                    }
        }

        return table + HeaderRow + "</tr>";
    }

    /// <summary>
    ///Carga el contenido de un DataTabla en una tabla Html del documento
    /// </summary>
    /// <param name="tableId"></param>
    /// <param name="dt"></param>
    /// <remarks></remarks>
    private String LoadAsocData(String tableName, String strHtml, bool onlyWF, List<Int64> AsociatedResultsIDs)
    {
        try
        {
            String item = strHtml.Substring(0, strHtml.LastIndexOf(tableName, StringComparison.InvariantCultureIgnoreCase) + (tableName).Length);
            item = item.Substring(item.LastIndexOf("<"));
            String aux2 = strHtml.Substring(strHtml.LastIndexOf(tableName, StringComparison.InvariantCultureIgnoreCase) + (tableName).Length);
            aux2 = aux2.Substring(0, aux2.ToLower().IndexOf("</table>"));

            if (aux2.StartsWith("_"))
                return strHtml;

            String itemTotal = item + aux2;
            aux2 = itemTotal.Replace("<tbody>", "").Replace("</tbody>", "");
            aux2 = aux2.Replace("<TBODY>", "").Replace("</TBODY>", "");

            //En aux 2 esta el item.
            //Item total es el que se reemplazará

            item = "<tbody>";

            if (aux2.Contains("id=\"zamba_associated_documents\"") && aux2.Contains("name=\"doc_type_ids("))
            {

                //string docTypeId1 =  aux2.Substring(aux2.IndexOf("doc_type_ids("));
                string docTypeIdAux = aux2.Substring(aux2.LastIndexOf(("doc_type_ids(")));
                docTypeIdAux = docTypeIdAux.Replace("doc_type_ids(", "");
                docTypeIdAux = docTypeIdAux.Substring(0, docTypeIdAux.IndexOf(")"));

                string[] docTypes = docTypeIdAux.Split(',');

                List<Int64> lstAux = new List<Int64>();
                foreach (Int64 DocTypeId in AsociatedResultsIDs)
                {
                    if (DocTypeId.ToString() == docTypes[0] || DocTypeId.ToString() == docTypes[1])
                    {
                        lstAux.Add(DocTypeId);
                    }
                }
                AsociatedResultsIDs = lstAux;
            }

            //return strHtml.Replace(itemTotal, aux2 + item + "</tbody>");
            string scriptToAdd = string.Empty;

            SDocAsociated sda = new SDocAsociated();
            List<IResult> AsociatedResults = new List<IResult>();

            foreach (Int64 DocTypeId in AsociatedResultsIDs)
            {
                AsociatedResults.AddRange(sda.getAsociatedResultsFromResultAsList(DocTypeId, _result));
            }

            sda = null;

            strHtml = strHtml.Replace(itemTotal,
                GetAsociatedDocumentTag(aux2, AsociatedResults, onlyWF, tableName, ref scriptToAdd));

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AsociatedTagsScripts",
            //    "$(document).ready(function(){" + scriptToAdd + "});", true);
            Random ran = new Random();

            if (!string.IsNullOrEmpty(scriptToAdd))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AsociatedTagsScripts" + ran.Next().ToString(),
                    "$(document).ready(function(){" + scriptToAdd + "});", true);

            //strHtml += "$(document).ready(function(){" + scriptToAdd + "});";
            //strHtml.Replace("</body>", "<script>$(document).ready(function(){" + scriptToAdd + "});</scrtipt></body>");
            return strHtml;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return strHtml;
        }
    }

    private string GetAsociatedDocumentTag(string Item, List<IResult> AsociatedResults,
        bool OnlyWF, string TableName, ref string ScriptToAdd)
    {
        int classStart = Item.IndexOf("class");
        string cssClass = Item.Substring(classStart + 7,
            Item.IndexOf("\"", classStart + 8) - (classStart + 8) + 1);
        string itemToReturn = string.Empty;

        switch (cssClass)
        {
            case "tablesorter":
                itemToReturn = Item.Trim();
                DataTable dt = null;
                if (string.IsNullOrEmpty(TableName) == false)
                {
                    dt = ParseResult(AsociatedResults, OnlyWF, TableName);
                }
                else
                {
                    dt = ParseResult(AsociatedResults, OnlyWF, string.Empty);
                }

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    itemToReturn = LoadTableHeader(itemToReturn, dt.Columns);
                    itemToReturn = LoadTableBody(itemToReturn, dt.Rows);
                }
                //itemToReturn += "</table>";
                break;
            case "gallery":
                string ulID = TableName;
                string liTemplate = "<li><img data-frame=\"" + GetDocFileUrl + "\" src=\"" + GetDocFileUrl + "\" title=\"{3}\" description=\"{4}\"/>";
                string ulTemplate = "<ul id=\"{0}\">{1}</ul>";
                string scriptTemplate = "$('#{0}').galleryView({1});";
                StringBuilder sbList = new StringBuilder();
                int count = AsociatedResults.Count;

                string[] temp = TableName.Split('_');
                int indexToDescriptionID = int.Parse(temp[4]);
                string strDescription;
                int indexCount;

                for (int i = 0; i < count; i++)
                {
                    indexCount = AsociatedResults[i].Indexs.Count;
                    strDescription = string.Empty;
                    for (int j = 0; j < indexCount; j++)
                    {
                        if (((IIndex)AsociatedResults[i].Indexs[j]).ID == indexToDescriptionID)
                        {
                            strDescription = ((IIndex)AsociatedResults[i].Indexs[j]).Data;
                        }
                    }
                    sbList.AppendFormat(liTemplate, AsociatedResults[i].DocTypeId,
                        AsociatedResults[i].ID, user.ID, AsociatedResults[i].Name, strDescription);
                }

                itemToReturn = string.Format(ulTemplate, TableName, sbList.ToString());
                ScriptToAdd += string.Format(scriptTemplate, ulID, "{panel_width: 300,panel_height: 200,frame_width: 55,show_overlays: true}");
                itemToReturn += "<table>";
                break;
        }

        return itemToReturn;
    }

    /// <summary>
    /// Completa los componentes de Zvar
    /// </summary>
    /// <param name="res"></param>
    /// <param name="strHtml"></param>
    /// <history>Marcelo 28/11/2012 Created</history>
    /// <returns></returns>
    private String CompleteFormZVar(IResult res, string strHtml)
    {
        SUserPreferences SUserPreferences = new SUserPreferences();
        try
        {
            //Verifica si debe cargar los asociados del formulario
            if (bool.Parse(SUserPreferences.getValue("LoadZVarcomponents", Sections.FormPreferences, "True")) == true && res.ID != 0)
            {
                //Verifica si existen controles html de asociados
                if (strHtml.ToLower().Contains("zamba_zvar"))
                {
                    foreach (String s in VariablesInterReglas.Keys())
                    {
                        if (ContainsCaseInsensitive(strHtml, "zamba_zvar(" + s + ")"))
                        {
                            if (VariablesInterReglas.get_Item(s) is DataSet)
                            {
                                String item = strHtml.Substring(0, strHtml.LastIndexOf("zamba_zvar(" + s + ")", StringComparison.InvariantCultureIgnoreCase) + ("zamba_zvar(" + s + ")").Length);
                                item = item.Substring(item.LastIndexOf("<"));
                                String aux2 = strHtml.Substring(strHtml.LastIndexOf("zamba_zvar(" + s + ")", StringComparison.InvariantCultureIgnoreCase) + ("zamba_zvar(" + s + ")").Length);
                                aux2 = aux2.Substring(0, aux2.ToLower().IndexOf("</table>"));

                                if (aux2.StartsWith("_"))
                                    return strHtml;

                                String itemTotal = item + aux2;

                                String tbody = itemTotal.Substring(itemTotal.IndexOf("<tbody", StringComparison.InvariantCultureIgnoreCase));
                                tbody = tbody.Substring(0, tbody.LastIndexOf("</tbody>")).Replace("<tbody>", "");

                                aux2 = itemTotal.Replace("<tbody>", "").Replace("</tbody>", "");
                                aux2 = aux2.Replace("<TBODY>", "");
                                aux2 = aux2.Replace("</TBODY>", "");

                                String itemToReturn = aux2;
                                itemToReturn = FormControlsController.LoadZVarTableHeader(itemToReturn, ((DataSet)VariablesInterReglas.get_Item(s)).Tables[0].Columns, tbody);
                                itemToReturn = FormControlsController.LoadZVarTableBody(itemToReturn, ((DataSet)VariablesInterReglas.get_Item(s)).Tables[0].Rows, tbody,string.Empty);


                                itemToReturn+="</tbody>";

                                strHtml = strHtml.Replace(itemTotal, itemToReturn);
                            }
                        }
                    }
                }
            }

            if (strHtml.ToLower().Contains("zamba.usuarioactual.id"))
            {
                strHtml = strHtml.Replace("zamba.usuarioactual.id", user.ID.ToString());
            }
            if (strHtml.ToLower().Contains("zamba.usuarioactual.usuario"))
            {
                strHtml = strHtml.Replace("zamba.usuarioactual.usuario", user.Name);
            }
            if (strHtml.ToLower().Contains("zamba.usuarioactual.nombre"))
            {
                strHtml = strHtml.Replace("zamba.usuarioactual.nombre", user.Nombres);
            }
            if (strHtml.ToLower().Contains("zamba.usuarioactual.apellido"))
            {
                strHtml = strHtml.Replace("zamba.usuarioactual.apellido", user.Apellidos);
            }
            if (strHtml.ToLower().Contains("zamba.usuarioactual.mail"))
            {
                strHtml = strHtml.Replace("zamba.usuarioactual.mail", user.eMail.Mail);
            }
            if (strHtml.ToLower().Contains("zamba.fechaactual"))
            {
                strHtml = strHtml.Replace("zamba.fechaactual", DateTime.Now.ToShortDateString());
            }
            //Idea de Martin, ver si se usará o no
            //if (strHtml.ToLower().Contains("zamba.fillselect("))
            //{

            //    selectstringstart = strHtml.IndexOf("zamba.fillselect(") + 17;
            //    selectstringend = strHtml.IndexOf(")",selectstringstart);
            //    selectstring = strHtml.Substring(selectstringstart, selectstringend);

            //    idstringstart = strHtml..Substring(selectstringstart, selectstringend);
            //    idstringend =
            //    idstring =
            //    strHtml = strHtml.Replace("zamba.fillselect(", Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, selectstring));

            //}
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            SUserPreferences = null;
        }

        return strHtml;
    }

    /// <summary>
    /// Completa los datos de los asociados en el formulario
    /// </summary>
    /// <param name="res"></param>
    /// <param name="strHtml"></param>
    /// <returns></returns>
    private String CompleteFormAsociates(IResult res, string strHtml)
    {
        SUserPreferences SUserPreferences = new SUserPreferences();

        //Verifica si debe cargar los asociados del formulario
        if (bool.Parse(SUserPreferences.getValue("LoadAsociatedResultsComponents", Sections.FormPreferences, "True")) == true && res.ID != 0)
        {
            //Verifica si existen controles html de asociados
            if (strHtml.ToLower().Contains("zamba_associated_documents") || strHtml.ToLower().Contains("zamba_asoc"))
            {
                SDocAsociated SDocAsociated = new SDocAsociated();
                DataSet docTypeAsoc = SDocAsociated.getDocTypesAsociated(res.DocTypeId);
                SDocAsociated = null;

                List<Int64> docTypeAsocIDs = new List<long>();

                foreach (DataRow drRow in docTypeAsoc.Tables[0].Rows)
                {
                    Int64 dtid = Int64.Parse(drRow[1].ToString());
                    if (!docTypeAsocIDs.Contains(dtid))
                        docTypeAsocIDs.Add(dtid);
                }

                docTypeAsoc.Dispose();
                docTypeAsoc = null;

                Int64 indexID, lastDocTypeID;
                SForms sforms;
                DataRow dr;
                DataTable dtDocType, dtAsoc;
                //char[] chrToReplace = { Char.Parse("_") };

                //Se cargan las tablas asociadas o de WF
                if (ContainsCaseInsensitive(strHtml, "zamba_associated_documents"))
                    strHtml = LoadAsocData("zamba_associated_documents", strHtml, false, docTypeAsocIDs);
                if (ContainsCaseInsensitive(strHtml, "zamba_associated_documents_WF"))
                    strHtml = LoadAsocData("zamba_associated_documents_WF", strHtml, true, docTypeAsocIDs);

                string strTableName;
                int startID;
                int indexOf;
                string asocIndex, item, aux, body;

                //Se recorren los tipos de documento para cargar los atributos asociados
                foreach (Int64 myDocTypeId in docTypeAsocIDs)
                {
                    indexOf = 0;
                    indexOf = strHtml.IndexOf("zamba_associated_documents_" + myDocTypeId,
                        indexOf, strHtml.Length - indexOf, StringComparison.CurrentCultureIgnoreCase);

                    while (indexOf != -1)
                    {
                        startID = strHtml.IndexOf("zamba_associated_documents_" + myDocTypeId, indexOf);
                        strTableName = strHtml.Substring(startID,
                            strHtml.IndexOf("\"", startID) - startID);
                        strHtml = LoadAsocData(strTableName, strHtml, false, new List<Int64>() { myDocTypeId });

                        indexOf = strHtml.IndexOf("zamba_associated_documents_" + myDocTypeId,
                            indexOf + 1, strHtml.Length - indexOf - 1, StringComparison.CurrentCultureIgnoreCase);
                    }

                    //Atributos asociados
                    asocIndex = "ZAMBA_ASOC_" + myDocTypeId + "_";
                    if (ContainsCaseInsensitive(strHtml, asocIndex))
                    {
                        dr = default(DataRow);
                        dtDocType = default(DataTable);
                        sforms = new SForms();
                        lastDocTypeID = 0;

                        //Se guarda un temporal del body para modificarlo
                        body = strHtml;

                        //Se recorren los atributos encontrados a completar
                        List<String> IndexItems = getIndexItems(asocIndex, body);
                        if (IndexItems.Count > 0)
                        {
                            //Se obtiene el doctype asociado
                            dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(res, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), new List<String>() { myDocTypeId.ToString() });

                            foreach (string indexName in IndexItems)
                            {
                                //Se obtiene el id numérico del índice
                                if (Int64.TryParse(indexName, out indexID) == false)
                                    indexID = IndexsBusiness.GetIndexIdByName(indexName.Replace("_s", string.Empty).Replace("_n", string.Empty));

                                //Valida que no se hayan ingresado atributos mal escritos
                                if (indexID > 0)
                                {
                                    //Verifica que se cargue una sola vez
                                    if (lastDocTypeID != myDocTypeId)
                                    {
                                        lastDocTypeID = myDocTypeId;
                                        dtAsoc.DefaultView.RowFilter = "doc_type_id=" + myDocTypeId;
                                        dtDocType = dtAsoc.DefaultView.ToTable();

                                        if (dtDocType.Rows.Count > 0)
                                            dr = dtDocType.Rows[0];
                                        else
                                            dr = null;
                                    }

                                    //Si existe un doctype
                                    if (dr != null)
                                    {
                                        Index indx = ZCore.GetInstance().GetIndex(indexID);

                                        //Se completa el valor del índice del result
                                        if (indx.DropDown == IndexAdditionalType.AutoSustitución || indx.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                                        {
                                            if (dtDocType.Columns.Contains(indx.Name) && dtDocType.Columns.Contains("I" + indx.ID))
                                            {
                                                if (DBNull.Value != dr["I" + indx.ID])
                                                    indx.Data = dr["I" + indx.ID].ToString();
                                                if (DBNull.Value != dr[indx.Name])
                                                    indx.dataDescription = dr[indx.Name].ToString();
                                            }
                                        }
                                        else
                                        {
                                            if (dtDocType.Columns.Contains(indx.Name))
                                            {
                                                if (DBNull.Value != dr[indx.Name])
                                                    indx.Data = dr[indx.Name].ToString();
                                            }
                                        }

                                        //Se aplican los reemplazos en el html completando el valor de los atributos
                                        String id = asocIndex + "index_" + indexName;
                                        item = strHtml.Substring(0, strHtml.LastIndexOf(id, StringComparison.InvariantCultureIgnoreCase) + id.Length);
                                        item = item.Substring(item.LastIndexOf("<"));
                                        aux = strHtml.Substring(strHtml.LastIndexOf(id, StringComparison.InvariantCultureIgnoreCase) + id.Length);
                                        aux = aux.Substring(0, aux.IndexOf(">"));
                                        item = item + aux + ">";

                                        //14/09/2011:Se aplica html encode para los valores de la tabla de asociados.
                                        indx.Data = Server.HtmlEncode(indx.Data);

                                        aux = sforms.AsignValue(indx, item, res);
                                        strHtml = strHtml.Replace(item, aux);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return strHtml;
    }

    /// <summary>
    /// Obtiene los atributos asociados de un entidad 
    /// </summary>
    /// <param name="docTypeId">Acepta el formato "zamba_asoc_[doctypeid]_" o directamente el docTypeId en formato string</param>
    /// <param name="body">Body donde se obtendrán los atributos</param>
    /// <returns>Un listado de atributos asociados al entidad solicitado. 
    ///         Puede que devuelva un id de tipo string o el nombre del índice.</returns>
    private List<string> getIndexItems(string docTypeId, string body)
    {
        List<string> elements = new List<string>();
        Int64 index;
        Int32 lastPosition;
        string elem;

        //Valida que existan datos a procesar
        if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(docTypeId))
        {
            //Verifica el formato de docTypeId
            if (Int64.TryParse(docTypeId, out index))
                docTypeId = "zamba_asoc_" + docTypeId + "_";
            else
                docTypeId = docTypeId.ToLower();

            //Verifica que en el body exista algún índice asociado del entidad a buscar
            body = body.ToLower();
            while (body.Contains(docTypeId))
            {
                //Obtiene el id del índice y lo agrega a una lista
                lastPosition = body.IndexOf(docTypeId);
                elem = body.Substring(lastPosition);
                elem = elem.Substring(0, elem.IndexOf(" ")).Replace("\"", String.Empty);
                elements.Add(elem.Replace(docTypeId + "index_", String.Empty));

                //Modifica el body para no encontrar el último índice agregado
                body = body.Substring(lastPosition).Replace(elem, String.Empty);
            }
        }

        return elements;
    }

    private string CompleteSlstDiv(string body)
    {
        return "<div id=\"dynamic_filter\"></div>" + body;
    }

    private string CompleteFormIndexs(IResult res, string strHtml)
    {
        SIndex SIndex = new SIndex();
        SForms SForms = new SForms();
        string strIndexId;
        List<IIndex> indices;

        if (IsShowing)
        {
            indices = SIndex.GetIndexs(res.ID, res.DocTypeId);
        }
        else
        {
            SetRuleConfiguration();

            if (_ruleCallDocID > 0 && _ruleCallDocTypeID > 0)
            {
                SResult SResult = new SResult();

                //Ezequiel: Obtengo el result a mapear indices
                IResult res1 = SResult.GetResult(_ruleCallDocID, _ruleCallDocTypeID);

                //Mapeo de indices
                int maxIndexsNewResult = NewResult.Indexs.Count;
                int maxIndexsResultAsoc = res1.Indexs.Count;
                IIndex index1;
                IIndex index2;

                if (_haveSpecificAttributes)
                {
                    //Si tiene la configuracion de atributos especificos, los completa en base a la configuracion de la regla
                    NewResult.Indexs = GetCompleteSpecificAttributes(NewResult.Indexs, ref res1);
                }
                else
                {
                    if (_fillCommonAttributes)
                    {
                        for (int i = 0; i < maxIndexsNewResult; i++)
                        {
                            index1 = (IIndex)NewResult.Indexs[i];
                            for (int j = 0; j < maxIndexsResultAsoc; j++)
                            {
                                index2 = (IIndex)res1.Indexs[j];
                                if (index1.ID == index2.ID)
                                {
                                    ((IIndex)NewResult.Indexs[i]).Data = index2.Data;
                                    ((IIndex)NewResult.Indexs[i]).DataTemp = index2.DataTemp;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable DtReferencedIndexes = IndexsBusiness.GetReferencedIndexsByDocTypeID(_ruleCallDocTypeID, NewResult.DocTypeId);
                        if (DtReferencedIndexes.Rows.Count > 0 && DtReferencedIndexes != null)
                        {
                            DataRow row;

                            long indexFrom;
                            long indexTo;
                            for (int i = 0; i < maxIndexsNewResult; i++)
                            {
                                index1 = (IIndex)NewResult.Indexs[i];
                                for (int j = 0; j < maxIndexsResultAsoc; j++)
                                {
                                    index2 = (IIndex)res1.Indexs[j];

                                    for (int k = 0; k < DtReferencedIndexes.Rows.Count; k++)
                                    {
                                        row = DtReferencedIndexes.Rows[k];
                                        indexFrom = long.Parse(row.ItemArray[0].ToString());
                                        indexTo = long.Parse(row.ItemArray[1].ToString());
                                        if (index1.ID == indexFrom && index2.ID == indexTo)
                                        {
                                            ((IIndex)NewResult.Indexs[i]).Data = index2.Data;
                                            ((IIndex)NewResult.Indexs[i]).DataTemp = index2.DataTemp;
                                            break;
                                        }
                                    }
                                }
                            }
                            DtReferencedIndexes.Clear();
                            DtReferencedIndexes = null;
                            row = null;
                        }
                    }
                }
            }

            indices = new List<IIndex>(NewResult.Indexs.Count);
            indices.AddRange(NewResult.Indexs.OfType<IIndex>());

        }

        ArrayList dateIndex = new ArrayList();
        if (indices != null)
        {
            System.Collections.Generic.List<long> GIDs = new System.Collections.Generic.List<long>();
            //Esta variable debe estar en false para que tome los permisos por atributos
            Boolean UseIndexsRights = Boolean.Parse(UserPreferences.getValue("UseViewRightsForIndexsOnForm", Sections.FormPreferences, "False"));
            if (!(UseIndexsRights))
            {
                foreach (Zamba.Core.UserGroup UGroup in user.Groups)
                {
                    GIDs.Add(((Zamba.Core.ZBaseCore)(UGroup)).ID);
                }
                GIDs.Add(user.ID);
            }

            bool input;
            bool specificAtributesRights;
            bool specificEditAtribute;
            string loweredItem;
            string item;
            string aux2;
            string aux3;
            int idStartIndex;
            isReindex = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, res.DocTypeId);

            foreach (IIndex indice in indices)
            {
                //Busca la existencia del índice en el formulario
                strIndexId = "\"ZAMBA_INDEX_" + indice.ID + "\"";
                if (!ContainsCaseInsensitive(strHtml, strIndexId))
                {
                    strIndexId = "'ZAMBA_INDEX_" + indice.ID + "'";
                    if (!ContainsCaseInsensitive(strHtml, strIndexId))
                    {
                        strIndexId = string.Empty;
                    }
                }

                //Verifica si encontró el índice
                if (!string.IsNullOrEmpty(strIndexId))
                {
                    if (this.CurrentZFrom.ContainsKey(indice.ID))
                        this.CurrentZFrom[indice.ID] = indice.Data;
                    else
                        this.CurrentZFrom.Add(indice.ID, indice.Data);

                    idStartIndex = strHtml.LastIndexOf("id=" + strIndexId, StringComparison.InvariantCultureIgnoreCase) + (strIndexId).Length + 3;
                    item = strHtml.Substring(0, idStartIndex);
                    item = item.Substring(item.LastIndexOf("<"));
                    aux2 = strHtml.Substring(idStartIndex);
                    aux2 = aux2.Substring(0, aux2.IndexOf(">"));
                    item = item + aux2 + ">";
                    loweredItem = item.ToLower();

                    input = loweredItem.Contains("input") || loweredItem.Contains("textarea");
                    aux3 = string.Empty;

                    if (!(UseIndexsRights))
                    {
                        specificEditAtribute = RightsBusiness.GetIndexRightValue(indice.ID, res.DocType.ID, user, RightsType.IndexEdit);
                        //specificAtributesRights = RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.ViewRightsByIndex,res.DocTypeId, this.user.ID);
                        specificAtributesRights = RightsBusiness.GetSpecificAttributeRight(this.user, res.DocTypeId);

                        if (this.IsShowing && (!isReindex || (specificAtributesRights && !specificEditAtribute)))
                        {
                            if (!input)
                            {
                                if (!loweredItem.Contains("disabled"))
                                {
                                    aux3 = item.Substring(0, item.IndexOf(">")) + " disabled=\"disabled\" >";
                                }
                            }
                            else
                            {
                                if (!loweredItem.Contains("readonly"))
                                {
                                    aux3 = item.Substring(0, item.IndexOf(">")) + " readOnly=\"readonly\" class=\"ReadOnly\" >";
                                }
                                else
                                {
                                    aux3 = item.Substring(0, item.IndexOf(">")) + " class=\"ReadOnly\" >";
                                }
                            }

                            //agrego los indices de tipo fecha a un arraylist
                            if (indice.Type == IndexDataType.Fecha)
                            {
                                dateIndex.Add("zamba_index_" + indice.ID.ToString() + "\"");
                            }
                        }
                    }

                    if (String.Compare(aux3, String.Empty) == 0)
                        aux2 = SForms.AsignValue(indice, item, res);
                    else
                        //si no tengo el permiso entonces deshabilito el elemento
                        aux2 = SForms.AsignValue(indice, aux3, res);

                    aux2 = SetIndexAttributes(indice, aux2);
                    if (!string.IsNullOrEmpty(aux2))
                        strHtml = strHtml.Replace(item, aux2);
                }
            }

            if (dateIndex.Count > 0)
            {
                //deshabilito el datePicker de cada indice de tipo fecha
                var i = 0;
                String script = "$(document).ready(function(){";
                for (i = 0; i < dateIndex.Count; i++)
                {
                    script = script + "if ($(\"#" + dateIndex[i] + ").lenght > 0) $(\"#" + dateIndex[i] + ").datepicker(\"disable\");";
            
                }
                script = script + "});";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", script, true);
            }
        }

        if (ContainsCaseInsensitive(strHtml, "ZAMBA_DOC_ID"))
        {
            String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_DOC_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_ID").Length);
            item = item.Substring(item.LastIndexOf("<"));
            String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_DOC_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_ID").Length);
            aux2 = aux2.Substring(0, aux2.IndexOf(">"));

            item = item + aux2 + ">";
            Index indaux = new Index();
            indaux.Data = res.ID.ToString();
            indaux.DataTemp = res.ID.ToString();
            aux2 = SForms.AsignValue(indaux, item, res);

            strHtml = strHtml.Replace(item, aux2);
        }

        if (ContainsCaseInsensitive(strHtml, "ZAMBA_DOC_T_ID"))
        {
            String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_DOC_T_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_T_ID").Length);
            item = item.Substring(item.LastIndexOf("<"));
            String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_DOC_T_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_T_ID").Length);
            aux2 = aux2.Substring(0, aux2.IndexOf(">"));

            item = item + aux2 + ">";
            Index indaux = new Index();
            indaux.Data = res.DocTypeId.ToString();
            indaux.DataTemp = res.DocTypeId.ToString();
            aux2 = SForms.AsignValue(indaux, item, res);

            strHtml = strHtml.Replace(item, aux2);
        }
        return strHtml;
    }

    /// <summary>
    /// Completa los atributos del result en base a la configuracion obtenida en la regla
    /// </summary>
    /// <param name="indexs"> </param>
    /// <param name="resultToAsociate"> </param>
    /// <returns></returns>
    private List<IIndex> GetCompleteSpecificAttributes(List<IIndex> indexs, ref IResult resultToAsociate)
    {
        int maxIndexsNewResult = indexs.Count;
        int maxIndexsResultAsoc = resultToAsociate.Indexs.Count;
        IIndex index1;
        IIndex index2;
        List<IIndex> currIndexs = indexs;

        //Iteramos por los atributos a compleatar
        for (int i = 0; i < maxIndexsNewResult; i++)
        {
            //Cacheamos el atributo para evitar casteos
            index1 = (IIndex)currIndexs[i];

            //si el diccionario tiene la key del atributo es que fue configurado para no completarse
            if (_htAttributesConfig.ContainsKey(index1.ID))
            {
                ((IIndex)currIndexs[i]).Data = _htAttributesConfig[index1.ID];
                ((IIndex)currIndexs[i]).DataTemp = _htAttributesConfig[index1.ID];
            }
            else
            {
                //Si no contenia la key es porque se debe completar con los valores del padre,
                //siempre y cuando el padre tenga ese atributo, por eso se itera
                for (int j = 0; j < maxIndexsResultAsoc; j++)
                {
                    index2 = (IIndex)resultToAsociate.Indexs[j];
                    if (index1.ID == index2.ID)
                    {
                        ((IIndex)currIndexs[i]).Data = index2.Data;
                        ((IIndex)currIndexs[i]).DataTemp = index2.DataTemp;
                        break;
                    }
                }
            }
        }

        return currIndexs;
    }

    /// <summary>
    /// Obtiene del QueryString y de la Session todos los paramentros utilizados para la DoAddAsociatedForm
    /// </summary>
    private void SetRuleConfiguration()
    {
        if (!Boolean.TryParse(Request.QueryString["ContinueWithCurrentTasks"], out _continueWithCurrentTasks))
            _continueWithCurrentTasks = false;

        if (!Boolean.TryParse(Request.QueryString["DontOpenTaskAfterInsert"], out _dontOpenTaskAfterInsert))
            _dontOpenTaskAfterInsert = false;

        if (!Boolean.TryParse(Request.QueryString["FillCommonAttributes"], out _fillCommonAttributes))
            _fillCommonAttributes = false;

        if (!Boolean.TryParse(Request.QueryString["haveSpecificAtt"], out _haveSpecificAttributes))
            _haveSpecificAttributes = false;

        if (!long.TryParse(Request.QueryString["CallTaskID"], out _ruleCallTaskID))
            _ruleCallTaskID = -1;

        if (!long.TryParse(Request.QueryString["DocID"], out _ruleCallDocID))
            _ruleCallDocID = -1;

        if (!long.TryParse(Request.QueryString["DocTypeID"], out _ruleCallDocTypeID))
            _ruleCallDocTypeID = -1;

        if (_haveSpecificAttributes)
        {
            _htAttributesConfig = (Dictionary<long, string>)Session["SpecificAttrubutes" + _ruleCallTaskID.ToString()];
        }
    }

    private string SetIndexAttributes(IIndex index, string HtmlTag)
    {
        string tagToReturn = HtmlTag;

        string strValition = tagToReturn.ToLower().Substring(0, tagToReturn.IndexOf(' '));

        if (string.IsNullOrEmpty(strValition) ||
            !(strValition.Contains("input") || strValition.Contains("select") || strValition.Contains("textarea")))
        {
            return string.Empty;
        }

        int classIndex = tagToReturn.IndexOf("class=\"");
        int closeclassindex;
        string tagtoreplace = "";
        string oldClass;
        if (classIndex > -1)
        {
            closeclassindex = tagToReturn.Substring(classIndex + 7).IndexOf("\"");
            tagtoreplace = tagToReturn.Substring(classIndex, 7 + closeclassindex + 1);
        }
        oldClass = HtmlTag.Substring(classIndex + 7, tagToReturn.IndexOf('\"', classIndex + 7) - (classIndex + 7));
        StringBuilder newClass;
        if (classIndex < 0)
            newClass = new StringBuilder();
        else
            newClass = new StringBuilder(oldClass);

        StringBuilder newAttributes = new StringBuilder();

        string inputType = string.Empty;

        if (tagToReturn.Contains("<input"))
        {
            inputType = HTML.GetAttributeValue(tagToReturn, "type").ToLower();
            if (string.IsNullOrEmpty(inputType))
                inputType = "text";
        }

        string[] attributes;
        if (!string.IsNullOrEmpty(inputType) && inputType != "checkbox")
        {
            attributes = GetTypeToValidateFromIndex(index);
            if (attributes != null && !tagToReturn.Contains("dataType=") && !oldClass.Contains("dataType"))
            {
                newClass.Append(' ');
                newClass.Append(attributes[0]);
                newAttributes.Append(' ');
                newAttributes.Append(attributes[1]);
            }

            attributes = GetLengthFromIndex(index);
            if (attributes != null && !tagToReturn.Contains("length=") && !oldClass.Contains("length"))
            {
                newClass.Append(' ');
                newClass.Append(attributes[0]);
                newAttributes.Append(' ');
                newAttributes.Append(attributes[1]);
            }

            attributes = HTML.GetMinValueAttributes(index, TaskResult);
            if (attributes != null && !tagToReturn.Contains("ZMinValue=") && !oldClass.Contains("haveMinValue"))
            {
                newClass.Append(' ');
                newClass.Append(attributes[0]);
                newAttributes.Append(' ');
                newAttributes.Append(attributes[1]);
            }

            attributes = HTML.GetMaxValueAttributes(index, TaskResult);
            if (attributes != null && !tagToReturn.Contains("ZMaxValue=") && !oldClass.Contains("haveMaxValue"))
            {
                newClass.Append(' ');
                newClass.Append(attributes[0]);
                newAttributes.Append(' ');
                newAttributes.Append(attributes[1]);
            }
        }

        attributes = GetRequiredFromIndex(index);
        if (attributes != null && !oldClass.Contains("isRequired"))
        {
            newClass.Append(' ');
            newClass.Append(attributes[0]);
            newAttributes.Append(' ');
            newAttributes.Append(attributes[1]);
        }

        attributes = GetDefaultValue(index);
        if (attributes != null && !tagToReturn.Contains("DefaultValue=") && !oldClass.Contains("haveDefaultValue"))
        {
            newClass.Append(' ');
            newClass.Append(attributes[0]);
            newAttributes.Append(' ');
            newAttributes.Append(attributes[1]);
        }

        attributes = GetHierarchyFunctionally(index);
        if (attributes != null)
        {
            newClass.Append(' ');
            newClass.Append(attributes[0]);
            newAttributes.Append(' ');
            newAttributes.Append(attributes[1]);
        }

        newAttributes.AppendFormat(" indexName=\"{0}", index.Name + "\"");

        attributes = HTML.GetOriginalValueAttribute(index);
        if (attributes != null)
        {
            newAttributes.Append(' ');
            newAttributes.Append(attributes[1]);
        }


        if (classIndex < 0)
            tagToReturn = tagToReturn.Insert(tagToReturn.IndexOf(' '), " class=\"" + newClass.ToString() + "\" " + newAttributes.ToString() + "\" ");
        else
            tagToReturn = tagToReturn.Replace(tagtoreplace, "class=\"" + newClass.ToString() + "\" " + newAttributes.ToString());

        return tagToReturn;
    }

    private string[] GetTypeToValidateFromIndex(IIndex index)
    {
        switch (index.Type)
        {
            case IndexDataType.Fecha:
            case IndexDataType.Fecha_Hora:
                return new string[] { "dataType", "dataType=\"date\"" };
            case IndexDataType.Moneda:
            case IndexDataType.Numerico_Decimales:
                return new string[] { "dataType", "dataType=\"decimal_2_16\"" };
            case IndexDataType.Numerico_Largo:
            case IndexDataType.Numerico:
                return new string[] { "dataType", "dataType=\"numeric\"" };
            case IndexDataType.Alfanumerico:
            case IndexDataType.Alfanumerico_Largo:
            case IndexDataType.None:
            case IndexDataType.Si_No:
            default:
                return null;
        }
    }

    private string[] GetLengthFromIndex(IIndex index)
    {
        if (index.Len > 0)
        {
            switch (index.Type)
            {
                case IndexDataType.Fecha:
                    return new string[] { "length", "length=\"" + DateTime.Now.ToString("dd/mm/yyyy").Length + "\"" };
                case IndexDataType.Fecha_Hora:
                    return new string[] { "length", "length=\"" + (DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss tt").Length + 2) + "\"" };
                default:
                    return new string[] { "length", "length=\"" + index.Len.ToString() + "\"" };
            }
        }
        else
        {
            return null;
        }
    }

    private string[] GetRequiredFromIndex(IIndex index)
    {
        if (index.Required)
        {
            return new string[] { "isRequired", string.Empty };
        }
        else
        {
            return null;
        }
    }

    private string[] GetDefaultValue(IIndex index)
    {
        if (!string.IsNullOrEmpty(index.DefaultValue))
        {
            return new string[] { "haveDefaultValue", "DefaultValue=\"" + index.DefaultValue + "\"" };
        }
        else
        {
            return null;
        }
    }

    private string[] GetHierarchyFunctionally(IIndex index)
    {
        if (index.HierarchicalChildID != null && index.HierarchicalChildID.Count > 0)
        {
            return new[] { "HierarchicalIndex", "ChildIndexId=\"" + HTML.FormatMultipleChildAttribute(index.HierarchicalChildID) + "\"" };
        }
        return null;
    }

    private static bool ContainsCaseInsensitive(string source, string value)
    {
        int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        return results == -1 ? false : true;
    }

    private static bool IsNumeric(object expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }

    private Zamba.Core.IResult GetHdnResult()
    {
        IResult res1 = null;
        SResult SResult = new SResult();

        res1 = SResult.GetResult(Int64.Parse(docTB.DocId), Int64.Parse(docTB.DocTypeId));

        return res1;
    }
}
