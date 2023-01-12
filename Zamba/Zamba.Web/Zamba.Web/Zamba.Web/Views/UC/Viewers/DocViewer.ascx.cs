using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using Zamba;
using Zamba.Core;
using Zamba.Membership;
using Zamba.Services;
using Zamba.Web.Helpers;

public partial class Views_UC_Viewers_DocViewer : System.Web.UI.UserControl
{
    const string _INDEXUPDATE1 = "Atributo '";
    const string _INDEXUPDATE2 = "' de '";
    const string _INDEXUPDATE3 = "' a '";
    const string _INDEXUPDATE4 = "', ";
    const string _URLFORMAT = "{0}{1}/Services/GetDocFile.ashx?DocTypeId={2}&DocId={3}&UserID={4}";
    Int64 docTypeId;
    Int64 docId;
    Boolean IsFavorite;
    Boolean IsImportant;
    string url = string.Empty;
    static string fileName = string.Empty;
    IResult result;
    private ArrayList hideColumns;
    bool useVersion;
    public Views_ImageViewer ImageViewerControl { get; set; }
    IUser user;
    bool isReindex;
    bool isShared;

    UserPreferences UP = new UserPreferences();
    RightsBusiness RiB = new RightsBusiness();
    UserBusiness UB = new UserBusiness();

    public IResult Result
    {
        set
        {
            result = value;
            docTypeId = result.DocTypeId;
            IsFavorite = result.IsFavorite;
            IsImportant = result.IsImportant;
            
            docId = result.ID;
        }
        get
        {
            return result;
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (MembershipHelper.CurrentUser == null || !Response.IsClientConnected)
        {
            //FormsAuthentication.RedirectToLoginPage();
            return;
        }

        ZCore ZC = new ZCore();
        Page.Theme = ZC.InitWebPage();
        ZC.VerifyFileServer();
    }


    public void Modal_login(Boolean reloadModalLogin)
    {
        string loginUrl = FormsAuthentication.LoginUrl.ToString();
        string script = "$(document).ready(function() {  var linkSrc = location.origin.trim() + '" + loginUrl.Replace(".aspx", "").Trim() + "?showModal=true&reloadLogin=" + reloadModalLogin + "';  document.getElementById('iframeModalLogin').src = linkSrc; $('#modalLogin').modal('show');});";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //user = Zamba.Membership.MembershipHelper.CurrentUser;
        if (Zamba.Membership.MembershipHelper.CurrentUser == null || !Response.IsClientConnected || (MembershipHelper.CurrentUser != null && Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined" && MembershipHelper.CurrentUser.ID != long.Parse(Request.QueryString["userid"])))
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }
        else
        {
            try
            {
                if (Result != null)
                {
                    user = Zamba.Membership.MembershipHelper.CurrentUser;
                    completarindice.Visible = false;

                    hideColumns = new ArrayList();
                    useVersion = Boolean.Parse(UP.getValue("UseVersion", UPSections.UserPreferences, "False"));



                    Boolean print = this.Result.IsWord || this.Result.IsExcel;

                    // TaskName.Text = this.result.Name;
                    docTB.ShowPrint = print.ToString().ToLower();
                    docTB.DocId = docId.ToString();
                    docTB.DocTypeId = docTypeId.ToString();
                    docTB.IsImportant = IsImportant;
                    docTB.IsFavorite = IsFavorite;
                    docTB.StepId = Request["wfstepid"];
                    string[] temp = url.Split(new char[1] { '.' });
                    docTB.DocExt = temp[temp.Length - 1];

                    ShowDocument();
                }
                else
                {

                    string script = "$(document).ready(function() { swal({ title: '', text: 'No tiene permisos para ver esta tarea', icon: 'info', buttons: true, dangerMode: true, closeOnClickOutside: false, buttons:{ agregar: { text: 'ok' }, }, }) .then((value) => { switch (value) { case 'agregar': window.close(); break; } }); });";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);

                }
            }
            catch (Exception ex)
            {
                //formBrowser.Visible = false;
                lblDoc.Text = "<br><br>No se ha podido cargar el documento.";
                lblDoc.ForeColor = System.Drawing.Color.Red;
                Zamba.AppBlock.ZException.Log(ex);
            }

            //        Verifica si debe mostrar el panel de atributos
            if (Boolean.Parse(UP.getValue("ShowIndexLinkUnderTask", UPSections.UserPreferences, "False")))
            {
                LoadIndexsPanel(Page.IsPostBack);
                completarindice.OnSave += new Controls.Indexs.Saved(completarindice_OnSave);
            }

            //Actualiza el timemout
            SRights rights = new SRights();
            Int32 type = 0;
            if (user.WFLic) type = 1;
            if (user.ConnectionId > 0)
            {
                Ucm ucm = new Ucm();
                ucm.UpdateOrInsertActionTime(user.ID, user.Name, Request.UserHostAddress.Replace("::1", "127.0.0.1"), user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
            }

        }
    }

    private void ShowDocument()
    {
        SIndex Index = new SIndex();

        //Verifica si debe mostrar el panel de atributos
        if (Boolean.Parse(UP.getValue("ShowIndexLinkUnderTask", UPSections.UserPreferences, "False")))
        {
            LoadIndexsPanel(Page.IsPostBack);
        }
        else
        {
            completarindice.Visible = false;
            //pnlHideIndexs.Visible = false;
        }

        SetVisualizer();

        if (!Page.IsPostBack)
        {
            // Se registra la lectura de este documento   
            SRights srights = new SRights();
            srights.SaveActionWebView(result.ID, ObjectTypes.Documents, Request.UserHostAddress.Replace("::1", "127.0.0.1"), RightsType.View, result.DocTypeId.ToString());
        }
    }

    private void SetVisualizer()
    {
        //if (Result.IsImage)
        //{
        //    SetImageVisualizer();
        //}
        //else 
        //{ 
        //Visualizador por defecto: iframe
        url = string.Format(Tools.GetProtocol(Request) + _URLFORMAT, Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, result.DocTypeId, result.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID);
        docTB.FilePath = url;
        //formBrowser.Attributes["src"] = url;
        //formBrowser.Attributes["url"] = url;
        ImageViewerControl = null;
        //}
    }

    //private void SetImageVisualizer()
    //{
    //    if (DivDoc.Controls.Count == 0)
    //    {
    //        Views_ImageViewer viewer = (Views_ImageViewer)LoadControl("~/Views/UC/Viewers/ImageViewer.ascx");
    //        viewer.Result = Result;
    //        ImageViewerControl = viewer;
    //        viewer.SetVisualizer();
    //        DivDoc.Controls.Add(viewer);

    //        //formBrowser.Attributes["style"] = "display:none;" + formBrowser.Attributes["style"];
    //        formBrowser.Visible = false;
    //    }
    //}

    private void LoadIndexsPanel(bool useSessionIndexs)
    {
        SRights Rights = new SRights();

        isShared = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, result.DocTypeId);
        isReindex = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, result.DocTypeId);
        docTypeId = result.DocTypeId;

        //Se validan otros permisos para el caso de reindex
        if (result.DocTypeId != 0)
        {
            if (isShared)
            {
                isReindex = false;
            }
            else
            {
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, docTypeId))
                {
                    if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID == result.OwnerID && !isReindex)
                        isReindex = true;

                    if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID != result.OwnerID && isReindex)
                    {
                        if (!UB.DisableOwnerChanges(Zamba.Membership.MembershipHelper.CurrentUser.ID, docTypeId))
                            isReindex = false;
                    }
                }
            }
        }

        if (result.Indexs.Count > 0)
        {
            List<IIndex> indexs;

            if (useSessionIndexs)
            {
                indexs = (List<IIndex>)Session["CurrentIndexs"];
            }
            else
            {
                indexs = new List<IIndex>();
                for (int i = 0; i < result.Indexs.Count; i++)
                    indexs.Add((Index)result.Indexs[i]);
            }

            completarindice.DtId = result.DocTypeId;
            completarindice.Visible = true;
            completarindice.ShowIndexs(indexs, WebModuleMode.Result);
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
            if (Result != null)
            {
                //Se obtienen los índices actualizados
                List<IIndex> indexs = completarindice.CurrentIndexs;
                StringBuilder description = new System.Text.StringBuilder();

                if (indexs != null)
                {
                    description.Append("Modificaciones realizadas en '" + result.Name + "': ");
                    List<Int64> modifiedIndex = new List<Int64>();

                    //Se comparan los valores ingesados en el panel con los del result.
                    foreach (Index index in indexs)
                    {
                        foreach (Index rIndex in result.Indexs)
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
                        sResult.SaveModifiedIndexs(ref result, modifiedIndex);
                        description = description.Remove(description.Length - 2, 2);
                        Rights.SaveActionWebView(result.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, description.ToString());
                        completarindice.SetMessage("Cambios guardados con éxito", true);
                        Session["SaveIndexResult"] = "true";
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
            Session["SaveIndexResult"] = "false";
        }
    }
}
