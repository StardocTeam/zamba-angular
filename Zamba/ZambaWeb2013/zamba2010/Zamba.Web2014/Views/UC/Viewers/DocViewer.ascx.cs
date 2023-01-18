using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using Zamba;
using Zamba.Core;
using Zamba.Services;

public partial class Views_UC_Viewers_DocViewer : System.Web.UI.UserControl
{
    const string _INDEXUPDATE1 = "Atributo '";
    const string _INDEXUPDATE2 = "' de '";
    const string _INDEXUPDATE3 = "' a '";
    const string _INDEXUPDATE4 = "', ";
    const string _URLFORMAT = "{0}{1}/Services/GetDocFile.ashx?DocTypeId={2}&DocId={3}&UserID={4}";
    Int64 docTypeId;
    Int64 docId;
    string url = string.Empty;
    static string fileName = string.Empty;
    IResult result;
    private ArrayList hideColumns;
    bool useVersion;
    public Views_ImageViewer ImageViewerControl { get; set; }
    IUser user;
    bool isReindex;
    bool isShared;

    public IResult Result
    {
        set
        {
            result = value;
            docTypeId = result.DocTypeId;
            docId = result.ID;
        }
        get
        {
            return result;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        user = (IUser)Session["User"];
        if (null == Session["UserId"])
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            try
            {
                if (Result != null)
                {
                    user = (Zamba.Core.IUser)Session["User"];
                    completarindice.Visible = false;

                    SUserPreferences SUserPreferences = new SUserPreferences();
                    hideColumns = new ArrayList();
                    useVersion = Boolean.Parse(SUserPreferences.getValue("UseVersion", Sections.UserPreferences, "False"));
                    
                    Boolean print = this.Result.IsWord || this.Result.IsExcel;
                    docTB.ShowPrint = print.ToString().ToLower();
                    docTB.DocId = docId.ToString();
                    docTB.DocTypeId = docTypeId.ToString();
                    docTB.StepId = Request["wfstepid"];
                    string[] temp = url.Split(new char[1] { '.' });
                    docTB.DocExt = temp[temp.Length - 1];

                    ShowDocument();
                }
            }
            catch (Exception ex)
            {
                formBrowser.Visible = false;
                lblDoc.Text = "<br><br>No se ha podido cargar el documento.";
                lblDoc.ForeColor = System.Drawing.Color.Red;
                Zamba.AppBlock.ZException.Log(ex);
            }

            //Verifica si debe mostrar el panel de atributos
            if (Boolean.Parse(UserPreferences.getValue("ShowIndexLinkUnderTask", Sections.UserPreferences, "False")))
            {
                LoadIndexsPanel(Page.IsPostBack);
                completarindice.OnSave += new Controls.Indexs.Saved(completarindice_OnSave);
            }

            //Actualiza el timemout
            SRights rights = new SRights();
            Int32 type = 0;
            SUserPreferences sUserPreferences = new SUserPreferences();
            if (user.WFLic) type = 1;
            if (user.ConnectionId > 0)
            {
                rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(sUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
            }
            else
                Response.Redirect("~/Views/Security/LogIn.aspx");
            rights = null;
        }
    }

    private void ShowDocument()
    {
        SIndex Index = new SIndex();

        //Verifica si debe mostrar el panel de atributos
        if (Boolean.Parse(UserPreferences.getValue("ShowIndexLinkUnderTask", Sections.UserPreferences, "False")))
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
            srights.SaveActionWebView(result.ID, ObjectTypes.Documents, Environment.MachineName, RightsType.View, result.Name);
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
            url = string.Format(Tools.GetProtocol(Request) + _URLFORMAT, Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, result.DocTypeId, result.ID, Session["UserId"]);
            docTB.FilePath = url;
            formBrowser.Attributes["src"] = url;
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
        IUser user = (Zamba.Core.IUser)Session["User"];

        isShared = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, result.DocTypeId);
        isReindex = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, result.DocTypeId);
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
                if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, docTypeId))
                {
                    if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID == result.OwnerID && !isReindex)
                        isReindex = true;

                    if (Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, docTypeId) && user.ID != result.OwnerID && isReindex)
                    {
                        if (!Rights.DisableOwnerChanges(user, docTypeId))
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
                        Rights.SaveActionWebView(result.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, description.ToString());
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
}
