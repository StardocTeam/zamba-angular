using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Zamba.Core;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using Zamba.Services;
using Zamba;
using Zamba.Web.App_Code.Helpers;

public partial class UC_Viewers_FormBrowser : System.Web.UI.UserControl
{
    IUser user;
    string _url = string.Empty;
    INewResult newResult;
    IZwebForm zfrm;
    bool IsModal;
    UserPreferences UP = new UserPreferences();
    RightsBusiness RiB = new RightsBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Zamba.Membership.MembershipHelper.CurrentUser;

        if (!Page.IsPostBack)
            CargarFormulario();
    }

    protected void btn_insertar_click(object sender, EventArgs e)
    {
        if (Request["modal"] != null && bool.Parse(Request["modal"]) == true)
            IsModal = true;

        saveValues();
    }

    private void CargarFormulario()
    {
        if (Request["formid"] != null)
        {
            user = Zamba.Membership.MembershipHelper.CurrentUser;
            SForms SForms = new SForms();

            zfrm = SForms.GetForm(Int64.Parse(Request["formid"].ToString()));

            if (zfrm != null && navigateToForm(ref zfrm, "4") == true)
                return;

            lblError.Text = "Ha ocurrido un error al cargar el formulario";
        }
    }

    private bool navigateToForm(ref IZwebForm form, string typeForm)
    {
        SForms SForms = new SForms();
       
      
        if (string.IsNullOrEmpty(form.Path) == false)
        {
            try
            {
                System.IO.FileInfo ServerFile = new System.IO.FileInfo(form.Path);
                ZOptBusiness zoptb = new ZOptBusiness();
                String CurrentTheme = zoptb.GetValue("CurrentTheme");
                zoptb = null;
                string rutaTemp = Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "\\" + ServerFile.Name;
                FormBusiness FB = new FormBusiness();
                //Copia el formulario
                FB.CopyWebForm(form, rutaTemp);
                FB = null;
                //Leo el archivo
                StreamReader str = new StreamReader(rutaTemp);
                StreamReader a = new StreamReader(str.BaseStream);

                string strHtml = a.ReadToEnd();

                str.Close();
                str.Dispose();
                a.Close();
                a.Dispose();

                docViewer.Text = " ";

                newResult = GetNewResult();

                //Entrar por aca si el html tiene la palabra iframe
                if (strHtml.ToLower().Contains("iframe"))
                {
                    List<IDtoTag> listaTags = new List<IDtoTag>();
                    String GetDocFileUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&UserID={2}";

                    //Si tiene un iframe, busco el documento asociado
                    MatchCollection matches = default(MatchCollection);
                    matches = SForms.ParseHtml(strHtml, "iframe");
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
                                    //ver cuando el volumen es -2

                                    if (!string.IsNullOrEmpty(newResult.FullPath))
                                    {
                                        string Path = string.Format(GetDocFileUrl, newResult.DocTypeId, newResult.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID);

                                        if (newResult.IsMsg && (File.Exists(newResult.FullPath.ToLower().Replace(".msg", ".html")) || File.Exists(newResult.FullPath.ToLower().Replace(".msg", ".txt"))))
                                        {
                                            DirectoryInfo DI = new DirectoryInfo(System.IO.Path.GetDirectoryName(newResult.FullPath));
                                            System.IO.FileInfo[] fileList = DI.GetFiles();
                                            List<System.IO.FileInfo> fList = new List<System.IO.FileInfo>();

                                            foreach (System.IO.FileInfo fItem in fileList)
                                            {
                                                if (fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(newResult.FullPath)) && !fItem.Name.Trim().ToLower().EndsWith(".msg"))
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
                                    docTypesAsocList = SDocAsociated.getAsociatedResultsFromResult(newResult, Int32.Parse(UP.getValue("CantidadFilas", UPSections.UserPreferences, 100)), Zamba.Membership.MembershipHelper.CurrentUser.ID);

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
                                                        if (bool.Parse(UP.getValue("OpenMsgFileInIFrame", UPSections.FormPreferences, "False")))
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
                        strHtml = SForms.actualizarHtml(listaTags, strHtml);
                    }
                }

                // tags a eliminar del html
                string[] toDelete = { "ZAMBA_SAVE", "ZAMBA_CANCEL" };

                foreach (string delete in toDelete)
                {
                    if (!ContainsCaseInsensitive(strHtml, delete))
                        continue;

                    String item = strHtml.Substring(0, strHtml.LastIndexOf(delete, StringComparison.InvariantCultureIgnoreCase) + delete.Length);
                    item = item.Substring(item.LastIndexOf("<"));

                    String aux2 = strHtml.Substring(strHtml.LastIndexOf(delete, StringComparison.InvariantCultureIgnoreCase) + delete.Length);
                    aux2 = aux2.Substring(0, aux2.IndexOf(">"));

                    item = item + aux2 + ">";

                    strHtml = strHtml.Replace(item, "");
                }

                String strBody;
                String strHead;

                strHtml = CompleteFormIndexs(newResult, strHtml);
                //strHtml = CompleteFormAsociates(_result, strHtml);

                strBody = HTML.getHtmlSection(strHtml, HTML.HTMLSection.FORM); //pido solo lo que esta en el form para no incluirlo ya que en asp.net ya se tiene un formulario
                strHead = HTML.getHtmlSection(strHtml, HTML.HTMLSection.HEAD);

                /* Si no tiene Master es porque se abrio en forma modal */
                if (this.Page.Master != null)
                {
                    docViewer.Text = strBody;
                    this.Page.Master.Page.Header.Controls.Add(new LiteralControl(strHead));
                }
                else
                {
                    docViewer.Text = strHead + strHtml;
                }

                docViewer.Visible = true;
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return (false);
            }
        }
        else
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;
            lblError.Text = "<br><br>No se ha podido acceder al documento para su visualizacion: " + Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "\\" + form.Path;
            lblError.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        return true;
    }

    private bool ContainsCaseInsensitive(string source, string value)
    {
        int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        return results == -1 ? false : true;
    }

    private string CompleteFormIndexs(INewResult newRes, string strHtml)
    {
        long docID;
        long docTypeID;

        SForms SForms = new SForms();

        if (!string.IsNullOrEmpty(Request.QueryString["DocID"]) && !string.IsNullOrEmpty(Request.QueryString["DocTypeID"]) && long.TryParse(Request.QueryString["DocID"], out docID) && long.TryParse(Request.QueryString["DocTypeID"], out docTypeID))
        {
            SResult SResult = new SResult();

            //Ezequiel: Obtengo el result a mapear indices
            IResult res1 = SResult.GetResult(docID, docTypeID, true);


            //Ezequiel: Mapeo indices.
            foreach (IIndex ind in newRes.Indexs)
                foreach (IIndex indaux in res1.Indexs)
                {
                    if (ind.ID == indaux.ID)
                    {
                        ind.Data = indaux.Data;
                        ind.DataTemp = indaux.DataTemp;
                    }
                }

        }
        if (newRes != null)
        {
            foreach (IIndex indice in newRes.Indexs)
            {
                if (ContainsCaseInsensitive(strHtml, "ZAMBA_INDEX_" + indice.ID))
                {
                    String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_INDEX_" + indice.ID, StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_INDEX_ " + indice.ID).Length);
                    item = item.Substring(item.LastIndexOf("<"));
                    String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_INDEX_" + indice.ID, StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_INDEX_ " + indice.ID).Length);
                    aux2 = aux2.Substring(0, aux2.IndexOf(">"));

                    item = item + aux2 + ">";

                    aux2 = SForms.AsignValue(indice, item, newRes);

                    strHtml = strHtml.Replace(item, aux2);
                }
            }

        }
        return strHtml;
    }

    private INewResult GetNewResult()
    {
        SForms SForms = new SForms();
        zfrm = SForms.GetForm(Int64.Parse(Request["formid"].ToString()));

        newResult = SForms.CreateVirtualDocument(zfrm.DocTypeId);

        if (newResult.AutoName == null)
        {
            newResult.AutoName = " Name=" + zfrm.Name + " Id=" + zfrm.ID;
        }
        else
        {
            newResult.AutoName = newResult.AutoName + " Name=" + zfrm.Name + " Id=" + zfrm.ID;
        }

        newResult.CurrentFormID = zfrm.ID;

        return newResult;
    }

    private void saveValues()
    {
        newResult = GetNewResult();

        string indexValue;

        try
        {
            foreach (IIndex I in newResult.Indexs)
            {
                try
                {
                    indexValue = string.Empty;

                    if (!string.IsNullOrEmpty(Request["ZAMBA_INDEX_" + I.ID]))
                    {
                        indexValue = Request["ZAMBA_INDEX_" + I.ID];
                    }
                    else if (!string.IsNullOrEmpty(Request["ZAMBA_INDEX_" + I.Name]))
                    {
                        indexValue = Request["ZAMBA_INDEX_" + I.Name];
                    }
                    else if (!string.IsNullOrEmpty(Request["ZAMBA_INDEX_" + I.ID + "S"]))
                    {
                        indexValue = Request["ZAMBA_INDEX_" + I.ID + "S"];
                    }
                    else if (!string.IsNullOrEmpty(Request["ZAMBA_INDEX_" + I.ID + "N"]))
                    {
                        indexValue = Request["ZAMBA_INDEX_" + I.ID + "N"];
                    }

                    indexValue = indexValue.Trim();

                    if (!string.IsNullOrEmpty(indexValue))
                    {
                        I.Data = indexValue;
                        I.DataTemp = indexValue;

                        if (I.DropDown != IndexAdditionalType.AutoSustitución && I.DropDown != IndexAdditionalType.AutoSustituciónJerarquico)
                        {
                            if (I.Type == IndexDataType.Si_No)
                            {
                                if (bool.Parse(indexValue))
                                {
                                    I.Data = "1";
                                    I.DataTemp = "1";
                                }
                                else
                                {
                                    I.Data = "0";
                                    I.DataTemp = "0";
                                }
                            }
                        }
                        else
                        {
                            if (indexValue.Contains("-"))
                            {
                                I.Data = indexValue.Split('-')[0];
                                I.DataTemp = indexValue.Split('-')[0];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                }
            }

            if (newResult.ID == 0 || newResult.GetType().ToString() == "NewResult")
            {
               
                InsertResult insertresult = InsertResult.NoInsertado;
                INewResult nr = (INewResult)newResult;

                insertresult = IndexDocument(ref nr, 0, true);

                if (insertresult == InsertResult.Insertado)
                {
                    docViewer.Text = "El documento ha sido insertado correctamente";
                    btn_insertar.Visible = false;

                    if (!IsModal)
                    {
                        Response.Redirect(string.Format("http://{0}{1}/Views/WF/TaskSelector.ashx?docid={2}&doctypeid={3}&userId={4}", Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, nr.ID, nr.DocTypeId,Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString()));
                    }
                    else
                    {
                        //Codigo de ejecucion de reglas de entrada de la nueva tarea
                        STasks stasks = new STasks();
                      
                        ITaskResult Task = stasks.GetTaskByDocId(nr.ID);
                        string script = String.Empty;

                        SZOptBusiness ZOptBusines = new SZOptBusiness();
                        string doctypeidsexc = ZOptBusines.GetValue("DTIDShowDocAfterInsert");

                        bool opendoc = false;
                        if (!string.IsNullOrEmpty(doctypeidsexc))
                        {
                            foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (string.Compare(dtid.Trim(), nr.DocTypeId.ToString()) == 0)
                                    opendoc = true;
                            }
                        }

                        if (bool.Parse(UP.getValue("ShowDocAfterInsert", UPSections.InsertPreferences, "true")) || opendoc)
                        {
                            //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                            if (Task != null)
                            {
                                Page.Session.Add("Entrada" + nr.ID, true);
                                SRights sRights = new SRights();

                                //Verifica si tiene permisos de abrir la tarea
                                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.WFSteps, RightsType.Use, Task.StepId))
                                {
                                    script = "parent.OpenTask('../WF/TaskViewer.aspx?taskid=" + Task.TaskId + "'," + Task.TaskId + ",'" + Task.Name + "');";                                    
                                }
                                else
                                    script = "parent.OpenDocTask('../Search/DocViewer.aspx?doctype=" + nr.DocTypeId.ToString() + "&docid=" + nr.ID.ToString() + "'," + nr.ID + ",'" + nr.Name + "');";
                            }
                            else
                            {
                                script = "parent.OpenDocTask('../Search/DocViewer.aspx?doctype=" + nr.DocTypeId.ToString() + "&docid=" + nr.ID.ToString() + "'," + nr.ID + ",'" + nr.Name + "');";
                            }
                            //Se debe quitar este hardcodeo.
                            if (Page.Theme != "Boston")
                            {
                                script += "parent.SelectTaskFromModal();";
                            }
                        }
                        else
                        {
                            if (Session["ListOfTask"] == null)
                            {
                                Session["ListOfTask"] = new List<ITaskResult>();
                            }

                            ((List<ITaskResult>)Session["ListOfTask"]).Add(Task);

                            script = "parent.SetNewEntryRulesGroup();";
                        }

                        if (Request["CallTaskID"] != null)
                        {
                            script += "parent.ShowLoadingAnimation(); parent.RefreshTab('#T" + Request["CallTaskID"] + "');";
                        }
                        if (Request["docid"] != null)
                        {
                            script += "parent.RefreshTab('#D" + Request["docid"] + "');";
                        }

                        script += "parent.eval('tb_remove()');";

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                            "DoOpenTaskScript", "$(document).ready(function(){ " + script + " });", true);
                    }
                }
                else
                {
                    lblError.Text = "Ha ocurrido un error al insertar el formulario";
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            lblError.Text = "Ha ocurrido un error al insertar el formulario";
        }
    }

    private InsertResult IndexDocument(ref INewResult NewResult, Int64 FolderId, bool DisableAutomaticVersion)
    {
        InsertResult InsertResult = InsertResult.NoInsertado;
        try
        {
            if (NewResult != null)
            {
                               try
                {
                    Int64 IncrementedValue = 0;

                    sDocType sDocType = new sDocType();
                    SIndex SIndex = new SIndex();
                    SResult SResult = new SResult();

                    Int64 auto;

                    DataSet dsIndexsToIncrement = sDocType.GetIndexsProperties(NewResult.DocType.ID, true);

                    foreach (DataRow CurrentRow in dsIndexsToIncrement.Tables[0].Rows)
                    {
                        if (Int64.TryParse(CurrentRow["Autoincremental"].ToString(), out auto) && auto == 1)
                        {
                            foreach (IIndex CurrentIndex in NewResult.Indexs)
                            {
                                if (string.Compare(CurrentRow["Index_Name"].ToString().Trim(), CurrentIndex.Name.Trim()) == 0)
                                {
                                    if (CurrentIndex.Data.Trim() == string.Empty)
                                    {
                                        IncrementedValue = SIndex.SelectMaxIndexValue(CurrentIndex.ID, NewResult.DocType.ID);
                                        CurrentIndex.Data = IncrementedValue.ToString();
                                        CurrentIndex.DataTemp = IncrementedValue.ToString();
                                    }

                                }
                            }
                            //[Sebastian 04-11-2009] se carga el valor por defecto si lo tiene. Se hizo para formularios
                        }
                        else if (string.Compare(CurrentRow["DefaultValue"].ToString(), string.Empty) != 0)
                        {
                            foreach (IIndex CurrentIndex in NewResult.Indexs)
                            {
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

                    InsertResult = SResult.Insert(ref NewResult, false, false, false, false, NewResult.ISVIRTUAL, false, false, true, false);
                }
                catch (Exception ex)
                {
                    InsertResult = InsertResult.NoInsertado;
                    Zamba.AppBlock.ZException.Log(ex);
                    lblError.Text = "Error al insertar el formulario";
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            lblError.Text = "Error al insertar el formulario";
        }

        return InsertResult;
    }

}
