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

    protected void Page_PreInit(object sender, EventArgs e)
    {

        Int64 UrlUserId = 0;
        string userToken = string.Empty;
        try
        {
            // se optiene el user de la url
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["user"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userId"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                userToken = Request.QueryString["t"].ToString().Trim();
            }

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "UrlUserId : " + UrlUserId);

            //HttpContext.Current.Session["User"] = null;

            Results_Business RB = new Results_Business();

            if (MembershipHelper.CurrentUser != null && Response.IsClientConnected)
            {

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1 - El currentUser no es null ");
                Int64 userid = MembershipHelper.CurrentUser.ID;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "userid :  " + userid);
                //Se evalua si el usuario de la url es diferente al del MembershipHelper para hacer relogin

                // si el usuario es diferente el usuario se sacar del currentuser y se relogea, vuelve a pasar y valida ya q el usuario esta bien
                bool reloadModalLogin = userid != UrlUserId ? true : false;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "reloadModalLogin :  " + reloadModalLogin);

                bool isActiveSession = true;

                if (!reloadModalLogin)
                {
                    isActiveSession = RB.getValidateActiveSession(userid, userToken);
                }


                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);


                if (!reloadModalLogin && !isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1.1 - isActiveSession es false se dispara modal ");
                    Modal_login(reloadModalLogin);
                }
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2 - El currentUser  es null ");
                bool isActiveSession = RB.getValidateActiveSession(UrlUserId, userToken);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);

                if (isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1 - isActiveSession  es true, reestablezo la session ");
                    UserBusiness ub = new UserBusiness();
                    ub.ValidateLogIn(UrlUserId, ClientType.Web);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1.1 -  valido el usuario creado" + MembershipHelper.CurrentUser.ID);
                }
                else
                {

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.2 - isActiveSession  es false, se dispara modal ");
                    Modal_login(true);
                }
            }
        }
        catch (global::System.Exception ex)
        {
            FormsAuthentication.RedirectToLoginPage();
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

        //    }
        //}

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
                    Page.Title = _result.Name;


                    sf = new SForms();

                    if (!Page.IsPostBack)
                    {
                        // Se registra la lectura de este documento   
                        SRights srights = new SRights();
                        srights.SaveActionWebView(_result.ID, ObjectTypes.Documents, Request.UserHostAddress.Replace("::1", "127.0.0.1"), RightsType.View, _result.DocTypeId.ToString());
                    }

                    //Si tiene formulario se carga el control correspondiente.
                    if (sf.HasForms(_doctypeId) && !_showoriginal)
                    {
                        Views_UC_Viewers_FormBrowser viewer;
                        viewer = (Views_UC_Viewers_FormBrowser)LoadControl("../UC/Viewers/FormBrowser.ascx");
                        viewer.Result = _result;
                       // deleteCtrl.Result = _result;
                        viewer.IsShowing = true;
                        pnlViewer.Controls.Add(viewer);
                    }
                    else
                    {
                        Views_UC_Viewers_DocViewer viewer;
                        viewer = (Views_UC_Viewers_DocViewer)LoadControl("../UC/Viewers/DocViewer.ascx");
                        viewer.Result = _result;
                        //deleteCtrl.Result = _result;
                        pnlViewer.Controls.Add(viewer);
                    }

                    //Muestra u oculta el boton de eliminar dependindo de los permisos del usuario
                    //deleteCtrl.Visible = (Boolean.Parse(UP.getValue("ShowDeleteButton", UPSections.UserPreferences, true)) &&
                    //    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Delete, _result.DocType.ID));
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