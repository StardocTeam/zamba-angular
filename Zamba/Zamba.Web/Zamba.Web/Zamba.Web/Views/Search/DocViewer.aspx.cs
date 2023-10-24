using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using Zamba.Core;
using System.Web;
using Zamba.Services;
using Zamba.Membership;
using Zamba;
using System.Text;
using System.Web.Security;
using System.Web.Helpers;

public partial class DocViewer : Page
{
    Int32 _doctypeId;
    Int32 _docId;
    string _url = string.Empty;
    static string _fileName = string.Empty;
    IResult _result;
    private ArrayList _hideColumns;
    bool _showoriginal;
    bool _useVersion;

    UserPreferences UP = new UserPreferences();
    RightsBusiness RiB = new RightsBusiness();

    protected void Page_Init(object sender, EventArgs e)
    {
        AntiForgery.GetHtml();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (MembershipHelper.CurrentUser == null || !Response.IsClientConnected)/*|| (MembershipHelper.CurrentUser != null && Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined"))*/ /*&& MembershipHelper.CurrentUser.ID != long.Parse(Request.QueryString["userid"])))*/
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

        ZCore ZC = new ZCore();
        Page.Theme = ZC.InitWebPage();
        ZC.VerifyFileServer();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Zamba.Membership.MembershipHelper.CurrentUser == null)
        //{
        //    String userId = Request.QueryString["User"];
        //    if (userId == null)
        //    {
        //        userId = Request.QueryString["UserId"];
        //    }

        //    string urlActual = Page.Request.Url.AbsoluteUri;
        //    string urlConfirmar = "bpm/Views/Search/DocViewer.aspx";
        //    bool LoContiene = urlActual.Contains(urlConfirmar);

        //    // se redirecciona si el usuario no esta logueado
        //    if (userId == null)
        //    {
        //        FormsAuthentication.RedirectToLoginPage();
        //        return;
        //    }
        //    else
        //    {
        //        UserBusiness UB = new UserBusiness();
        //        IUser User = UB.ValidateLogIn(Int64.Parse(userId), ClientType.Web);

            //}
            ZssFactory zssFactory = new ZssFactory();
            Zamba.Framework.Zss zss;
            zss = zssFactory.GetZss(MembershipHelper.CurrentUser);
            if (IsPostBack)
            {
                if (hdnToken.Value != zss.Token)
                {
                    Context.Response.StatusCode = 401;
                    Context.Response.StatusDescription = "Unauthorized";
                    throw new HttpException(401, "Unauthorized");
                }
                    
            }
            else
            {
                hdnToken.Value = zss.Token;
            }

        

        if (Zamba.Membership.MembershipHelper.CurrentUser != null)
        {

            //MembershipHelper.CurrentUser = Zamba.Membership.MembershipHelper.CurrentUser;
            //Se agrega este if para que si se pulsa el botón cerrar no instancie nada.
            //if (!string.IsNullOrEmpty(this.Request["__EVENTTARGET"]))
            //{
            //    if (this.Request["__EVENTTARGET"].Contains("BtnClose"))
            //    {
            //        CerrarTab();
            //        return;
            //    }
            //}

            _hideColumns = new ArrayList();
            SForms sf;

            try
            {
                _useVersion = Boolean.Parse(UP.getValue("UseVersion", UPSections.UserPreferences, false));

                Int32.TryParse(Request.QueryString["doctype"], out _doctypeId);
                Int32.TryParse(Request.QueryString["docid"], out _docId);
                Boolean.TryParse(Request.QueryString["showoriginal"], out _showoriginal);

                if (_doctypeId > 0 && _docId > 0)
                {
                    _result = GetResult(_docId, _doctypeId);
                    hdnResultId.Value = _result.ID.ToString();
                    hdnDocTypeId.Value = _result.DocTypeId.ToString();

                    sf = new SForms();

                    //Si tiene formulario se carga el control correspondiente.
                    if (sf.HasForms(_doctypeId) && !_showoriginal)
                    {
                        Views_UC_Viewers_FormBrowser viewer;
                        viewer = (Views_UC_Viewers_FormBrowser)LoadControl("../UC/Viewers/FormBrowser.ascx");
                        viewer.Result = _result;
                        deleteCtrl.Result = _result;
                        viewer.IsShowing = true;
                        pnlViewer.Controls.Add(viewer);
                    }
                    else
                    {
                        Views_UC_Viewers_DocViewer viewer;
                        viewer = (Views_UC_Viewers_DocViewer)LoadControl("../UC/Viewers/DocViewer.ascx");
                        viewer.Result = _result;
                        deleteCtrl.Result = _result;
                        pnlViewer.Controls.Add(viewer);
                    }

                    //Muestra u oculta el boton de eliminar dependindo de los permisos del usuario
                    deleteCtrl.Visible = (Boolean.Parse(UP.getValue("ShowDeleteButton", UPSections.UserPreferences, true)) &&
                        RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Delete, _result.DocType.ID));
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
            finally {

            }
        }
    }

    //protected void BtnClose_Click(Object sender, EventArgs e)
    //{
    //    CerrarTab();
    //}

    //public void CerrarTab()
    //{
    //    Int32.TryParse(Request.QueryString["docid"], out _docId);
    //    string Script = "$(document).ready(function(){ hideLoading(); CloseCurrentTask(" + _docId + ",true);});";
    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClosingScript", Script, true);
    //}

    private static bool ContainsCaseInsensitive(string source, string value)
    {
        int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        return results == -1 ? false : true;
    }

    /// <summary>
    /// Hace la copia local del archivo
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isVirtual"></param>
    /// <returns></returns>
    private static String MakeLocalCopy(String path, Boolean isVirtual)
    {
        FileInfo fileVolumen;
        FileInfo fileLocal;

        //CleanUpFiles();
        try
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "adentro de copia local");

            fileVolumen = new FileInfo(path);
            fileLocal = new FileInfo(HttpRuntime.AppDomainAppPath + "temp\\" + fileVolumen.Name);

            ZTrace.WriteLineIf(ZTrace.IsInfo, "valor de fileVolumen: " + fileVolumen);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "valor de fileLocal: " + fileLocal);

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "dentro del try para comprobar si el archivo existe");

                if (fileLocal.Exists)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "el archivo ya existia y no se copiara desde el volumen");
                }
                else
                {
                    if (fileLocal.Directory.Exists == false)
                        fileLocal.Directory.Create();

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "el diretorio fue creado");
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "se copiara el archivo " + fileVolumen.FullName + " a " + fileLocal.FullName);

                    File.Copy(fileVolumen.FullName, fileLocal.FullName);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "el archivo fue copiado");
                }

                if (isVirtual)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "es formulario virtual");
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "fullname del formulario: " + fileLocal.FullName);
                    return fileLocal.FullName;
                }

                _fileName = fileVolumen.Name;

                return ".\\temp\\" + fileVolumen.Name;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                Zamba.AppBlock.ZException.Log(ex);
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return string.Empty;
        }
    }

    private bool GetVisibility(string columnName)
    {
        long aux;
        IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
        UserPreferences UserPreferences = new UserPreferences();
        SRights Rights = new SRights();

        if (_hideColumns.Count == 0)
        {

            _hideColumns.Add("doc_id");
            _hideColumns.Add("doc_type_id");
            _hideColumns.Add("ver_parent_id");
            _hideColumns.Add("disk_group_id");
            _hideColumns.Add("platter_id");
            _hideColumns.Add("vol_id");
            _hideColumns.Add("offset");
            _hideColumns.Add("icon_id");
            _hideColumns.Add("shared");
            _hideColumns.Add("rootid");
            _hideColumns.Add("original_filename");
            _hideColumns.Add("disk_vol_id");
            _hideColumns.Add("disk_vol_path");
            _hideColumns.Add("doc_file");

            if (_useVersion)
            {
                _hideColumns.Add("iddoc");
            }
            else
            {
                _hideColumns.Add("version");
                _hideColumns.Add("numero de version");
            }
            if (Boolean.Parse(UP.getValue("ShowGridColumnNombreOriginal", UPSections.UserPreferences, "False")) == false)
            {
                _hideColumns.Add("Original");
            }

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowResultNameColumn, -1))
                _hideColumns.Add("nombre");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowDocumentTypeColumn, -1))
                _hideColumns.Add("entidad");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionNumberColumn, -1))
                _hideColumns.Add("numero de version");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionColumn, -1))
                _hideColumns.Add("version");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowCreatedDateColumn, -1))
                _hideColumns.Add("Creado");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowLastEditDateColumn, -1))
                _hideColumns.Add("Modificado");

            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowOriginalName, -1))
                _hideColumns.Add("Original");
        }
        if (_hideColumns.Contains(columnName.ToLower()))
        {
            return false;
        }
        if (columnName.StartsWith("I") && Int64.TryParse(columnName.Remove(0, 1), out aux))
        {
            return false;
        }
        return true;
    }

    private static bool IsNumeric(object expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }


    private IResult GetResult(Int32 docId, Int32 doctypeId)
    {
        SResult SResult = new SResult();
        return SResult.GetResult(docId, doctypeId, true);
    }

    /// <summary>
    /// Hace la copia local del archivo
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isVirtual"></param>
    /// <returns></returns>
    private static String CopyTempFile(String path, Boolean isVirtual)
    {
        FileInfo fileVolumen;
        FileInfo fileLocal;

        try
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "adentro de copia local");

            fileVolumen = new FileInfo(path);

            fileLocal = new FileInfo(HttpRuntime.AppDomainAppPath + "temp\\" + fileVolumen.Name.Split(Convert.ToChar("."))[0] + "2" + fileVolumen.Extension);

            ZTrace.WriteLineIf(ZTrace.IsInfo, "valor de fileVolumen: " + fileVolumen);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "valor de fileLocal: " + fileLocal);

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "dentro del try para comprobar si el archivo existe");


                if (fileLocal.Directory.Exists == false)
                    fileLocal.Directory.Create();

                ZTrace.WriteLineIf(ZTrace.IsInfo, "el diretorio fue creado");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "se copiara el archivo " + fileVolumen.FullName + " a " + fileLocal.FullName);

                File.Copy(fileVolumen.FullName, fileLocal.FullName, true);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "el archivo fue copiado");

                if (isVirtual)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "es formulario virtual");
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "fullname del formulario: " + fileLocal.FullName);
                    return fileLocal.FullName;
                }

                _fileName = fileVolumen.Name;

                return ".\\temp\\" + fileLocal.Name;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                Zamba.AppBlock.ZException.Log(ex);
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return string.Empty;
        }
    }
}