using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using Zamba.Core;
using System.Web;
using Zamba.Services;

public partial class DocViewer : Page
{
    Int32 _doctypeId;
    Int32 _docId;
    Boolean _showoriginal;
    string _url = string.Empty;
    static string _fileName = string.Empty;
    //int _docTypeAuxiliarTab;
    IResult _result;
    private ArrayList _hideColumns;

    bool _useVersion;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        ZOptBusiness zoptb = new ZOptBusiness();
        Page.Theme = zoptb.GetValue("CurrentTheme");
        zoptb = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (null == Session["UserId"])
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
                //Se agrega este if para que si se pulsa el botón cerrar no instancie nada.
            if (!string.IsNullOrEmpty(this.Request["__EVENTTARGET"]))
            {
                if (this.Request["__EVENTTARGET"].Contains("BtnClose"))
                {
                    CerrarTab();
                    return;
                }
            }

            _hideColumns = new ArrayList();
            SUserPreferences sup;
            SForms sf;

            try
            {
                sup = new SUserPreferences();
                _useVersion = Boolean.Parse(sup.getValue("UseVersion", Sections.UserPreferences, false));

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
                    deleteCtrl.Visible = (Boolean.Parse(UserPreferences.getValue("ShowDeleteButton", Sections.UserPreferences, true)) &&
                        UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Delete, _result.DocType.ID));
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
            finally
            {
                sup = null;
            }
        }
    }

    protected void BtnClose_Click(Object sender, EventArgs e)
    {
        this.CerrarTab();
    }

    public void CerrarTab()
    {
        //Se cambia la forma que usa el script dado que no se debe escribir el scrip directamente en la página.
        //Se cambia por un document.ready para que reconozca funciones de la master.
        //Response.Write("<script language='javascript'> { hideLoading(); parent.CloseTask(1,true);}</script>");
        string Script = "$(document).ready(function(){ hideLoading(); parent.CloseTask(1,true);});";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClosingScript", Script, true);
    }
    
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
            ZTrace.WriteLineIf(ZTrace.IsInfo,"adentro de copia local");

            fileVolumen = new FileInfo(path);
            fileLocal = new FileInfo(HttpRuntime.AppDomainAppPath + "temp\\" + fileVolumen.Name);

            ZTrace.WriteLineIf(ZTrace.IsInfo,"valor de fileVolumen: " + fileVolumen);
            ZTrace.WriteLineIf(ZTrace.IsInfo,"valor de fileLocal: " + fileLocal);

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo,"dentro del try para comprobar si el archivo existe");

                if (fileLocal.Exists)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"el archivo ya existia y no se copiara desde el volumen");
                }
                else
                {
                    if (fileLocal.Directory.Exists == false)
                        fileLocal.Directory.Create();

                    ZTrace.WriteLineIf(ZTrace.IsInfo,"el diretorio fue creado");
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"se copiara el archivo " + fileVolumen.FullName + " a " + fileLocal.FullName);

                    File.Copy(fileVolumen.FullName, fileLocal.FullName);
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"el archivo fue copiado");
                }

                if (isVirtual)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"es formulario virtual");
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"fullname del formulario: " + fileLocal.FullName);
                    return fileLocal.FullName;
                }

                _fileName = fileVolumen.Name;

                return ".\\temp\\" + fileVolumen.Name;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString());
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
        IUser user = (IUser)Session["User"];
        SUserPreferences SUserPreferences = new SUserPreferences();
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
            if (Boolean.Parse(SUserPreferences.getValue("ShowGridColumnNombreOriginal", Sections.UserPreferences, "False")) == false)
            {
                _hideColumns.Add("nombre original");
            }

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowResultNameColumn, -1))
                _hideColumns.Add("nombre del documento");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowDocumentTypeColumn, -1))
                _hideColumns.Add("entidad");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionNumberColumn, -1))
                _hideColumns.Add("numero de version");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionColumn, -1))
                _hideColumns.Add("version");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowCreatedDateColumn, -1))
                _hideColumns.Add("fecha creacion");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowLastEditDateColumn, -1))
                _hideColumns.Add("fecha modificacion");

            if (Rights.GetUserRights(ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowOriginalName, -1))
                _hideColumns.Add("nombre original");
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
        return SResult.GetResult(docId, doctypeId);
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
            ZTrace.WriteLineIf(ZTrace.IsInfo,"adentro de copia local");

            fileVolumen = new FileInfo(path);
            
            
            
            fileLocal = new FileInfo(HttpRuntime.AppDomainAppPath + "temp\\" + fileVolumen.Name.Split(Convert.ToChar("."))[0] + "2" + fileVolumen.Extension);

            ZTrace.WriteLineIf(ZTrace.IsInfo,"valor de fileVolumen: " + fileVolumen);
            ZTrace.WriteLineIf(ZTrace.IsInfo,"valor de fileLocal: " + fileLocal);

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo,"dentro del try para comprobar si el archivo existe");

                
                if (fileLocal.Directory.Exists == false)
                        fileLocal.Directory.Create();

                ZTrace.WriteLineIf(ZTrace.IsInfo,"el diretorio fue creado");
                ZTrace.WriteLineIf(ZTrace.IsInfo,"se copiara el archivo " + fileVolumen.FullName + " a " + fileLocal.FullName);

                File.Copy(fileVolumen.FullName, fileLocal.FullName, true);
                ZTrace.WriteLineIf(ZTrace.IsInfo,"el archivo fue copiado");
                

                if (isVirtual)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"es formulario virtual");
                    ZTrace.WriteLineIf(ZTrace.IsInfo,"fullname del formulario: " + fileLocal.FullName);
                    return fileLocal.FullName;
                }

                _fileName = fileVolumen.Name;

                return ".\\temp\\" + fileLocal.Name;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString());
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