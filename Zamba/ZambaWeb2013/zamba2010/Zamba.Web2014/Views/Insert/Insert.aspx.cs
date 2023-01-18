using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Zamba.Services;
using System.Collections;
using Zamba.Core;
using Zamba;

public partial class Views_Insert_Insert : Page
{
    long _ruleCallTaskId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                ucUploadFile._FileUploadedEvent += new Views_UC_Upload_ucUploadFile.FileUploaded(ucUploadFile__FileUploadedEvent);
                ucUploadFile._FileUploadedErrorEvent += new Views_UC_Upload_ucUploadFile.FileUploadedError(ucUploadFile__FileUploadedErrorEvent);
                ucDocTypes._DocTypesSelected += new ucDocTypes.DocTypesSelected(ucDocTypes__DocTypesSelected);

                if (!Page.IsPostBack)
                {
                    //ucDocTypes.LoadDocTypes();
                    ucDocTypesIndexs.Visible = false;
                    ucUploadFile.Visible = false;
                    lnkInsertar.Visible = false;
                }


                ucDocTypesIndexs.SaveButtonName = lnkInsertar.ClientID;
                ucDocTypes.LoadDocTypes();
                SZOptBusiness zOptBusines = new SZOptBusiness();
                Page.Title = (string)zOptBusines.GetValue("WebViewTitle") + " - Insertar Documento";
                lblMsj.Text = string.Empty;

                string script = "$(document).ready(function() { CloseInsertingDialog(); });";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "closeDialogSript", script, true);
               
                hdnUserId.Value = Session["UserId"].ToString();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    void ucDocTypes__DocTypesSelected(long docTypeId)
    {
        SRights Rights = new SRights();

        Session["Insert_DocTypeId"] = docTypeId;

        try
        {
            var indexList = new List<IIndex>();

            List<IIndex> indexs = null;

            //Si el pedido de insersión es desde el docViewer, llenar los atributos con los atributos del documento (así quedará asociado)
            if (Request["isview"] == "true")
            {
                Int32 tempDoctypeId;
                Int32 tempdocId;
                //19/07/11:Esta variable se usará en caso que el insert sea llamado de la regla DoAddAsociateDocument
                Int32 fillIndxDocTypeId;

                Int32.TryParse(Request.QueryString["doctypeid"], out tempDoctypeId);
                Int32.TryParse(Request.QueryString["docid"], out tempdocId);
                //19/07/11:Llenamos la variable.
                Int32.TryParse(Request.QueryString["FillIndxDocTypeID"], out fillIndxDocTypeId);

                //Se llena primero los indices, si viene de regla traeria atributos sin valores.
                IResult res1 = null;
                SResult sResult = new SResult();

                if (tempDoctypeId > 0 )
                {
                    res1 = sResult.GetResult(tempdocId, tempDoctypeId);

                    Int32 max = res1.Indexs.Count;
                    indexs = new List<IIndex>();

                    for (int i = 0; i < max; i++)
                    {
                        indexs.Add((Index)res1.Indexs[i]);
                    }
                }

                //to-do: Refactorizar esta parte
                bool haveSpecificAttributes;

                if (!Boolean.TryParse(Request.QueryString["haveSpecificAtt"], out haveSpecificAttributes))
                    haveSpecificAttributes = false;

                if (fillIndxDocTypeId > 0)
                    res1 = sResult.GetResult(tempdocId, fillIndxDocTypeId);

                //Si existe la configuracion de atributos procedemos a la carga
                if(haveSpecificAttributes)
                {
                    if (!long.TryParse(Request.QueryString["CallTaskID"], out _ruleCallTaskId))
                        _ruleCallTaskId = -1;

                    //Obtenemos los atributos configurados
                    Dictionary<long, string> specificAttributes = (Dictionary<long, string>)Session["SpecificAttrubutes" + _ruleCallTaskId];

                    //Los completamos
                    indexs = GetCompleteSpecificAttributes(indexs, res1.Indexs, ref specificAttributes);
                }
                else
                {
                    //19/07/11: Ahora se llenan los valores de los atributos en caso que venga desde DoAddAsociateDocument
                    if (fillIndxDocTypeId > 0)
                    {
                        List<IIndex> toFillIndexs = res1.Indexs;

                        //Con los dos foreach se llenan los indices, 
                        //por cada indice a llenar debe buscar entre los indices con data para ver si lo contiene.
                        foreach (Index indexToFill in indexs)
                        {
                            foreach (Index indexWithData in toFillIndexs.Where(indexWithData => indexWithData.ID == indexToFill.ID))
                            {
                                indexToFill.Data = indexWithData.Data;
                            }
                        }
                    }
                }

                ucDocTypesIndexs.CurrentIndexs = indexs;
            }
            else
            {
                var ar = new ArrayList();
                ar.Add(docTypeId);
                indexs = ZCore.GetInstance().FilterSearchIndex(ar);
            }

            // var clonedIndexs = new List<IIndex>();
            // Int32 contador = 0;

            // clonedIndexs.AddRange(indexs);
            //foreach (Zamba.Core.Index ind in indexs)
            //{
            //    var newIndex = ind;
            //    clonedIndexs[contador] = newIndex;
            //    contador++;
            //}

            //indexs = clonedIndexs;

            bool viewSpecifiedIndex = true;
            var docTypesIds64 = new List<Int64>();

            docTypesIds64.Add(docTypeId);

            //Si se hace una busqueda combinada, si algun doctype tiene permiso para no filtrar indices
            //Bastaria para aplicar ese permiso a todos
            bool permisosFiltrarIndices = Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, ((Zamba.Core.IUser)Session["User"]).ID);

            if (permisosFiltrarIndices == false)
                viewSpecifiedIndex = false;

            if (viewSpecifiedIndex)
            {
                Hashtable iri = Rights.GetIndexsRights(docTypesIds64, ((Zamba.Core.IUser)Session["User"]).ID, true);

                foreach (Zamba.Core.Index currentIndex in indexs)
                {
                    if (((Zamba.Core.IndexsRightsInfo)iri[currentIndex.ID]).Search)
                        indexList.Add(currentIndex);
                }
            }
            else
            {
                indexList.AddRange(indexs);
            }

            ucDocTypesIndexs.DtId = int.Parse(docTypeId.ToString());

            WebModuleMode mode = (Request["isview"] == "true") ? WebModuleMode.Rule : WebModuleMode.Insert;

            ucDocTypesIndexs.ShowIndexs(indexList, mode);

            if (docTypeId > 0)
            {
                ucDocTypesIndexs.Visible = true;
                ucUploadFile.Visible = true;
                lnkInsertar.Visible = true;
            }
            else
            {
                ucDocTypesIndexs.Visible = false;
                ucUploadFile.Visible = false;
                lnkInsertar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    void ucUploadFile__FileUploadedErrorEvent(string error)
    {
        lblMsj.Text = error;
        string script = "$(document).ready(function() { parent.ResizeInsertDialogToShow(600); });";
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "resizeScript", script, true);
        lblMsj.Visible = true;
    }

    void ucUploadFile__FileUploadedEvent(string fileName)
    {
        Session["Insert_UploadedFile"] = fileName;
        InsertDoc();
    }

    protected void lnkInsertar_clic(object sender, EventArgs e)
    {
        ucUploadFile.uploadFile();
    }

    private void InsertDoc()
    {
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            List<IIndex> indices = ucDocTypesIndexs.CurrentIndexs;
            List<IIndex> Indexs = new List<IIndex>();

            foreach (IIndex ind in indices)
            {
                Indexs.Add(ind);

                if (ind.Required && string.IsNullOrEmpty(ind.DataTemp))
                {
                    lblMsj.Text = "Los indices marcados con (*) son de ingreso obligatorio";
                    lblMsj.Visible = true;
                    return;
                }
                else if((ind.DropDown== IndexAdditionalType.AutoSustitución || ind.DropDown== IndexAdditionalType.AutoSustituciónJerarquico) && 
                    !string.IsNullOrEmpty(ind.Data) &&
                    string.IsNullOrEmpty(ind.dataDescription))
                {
                    ind.dataDescription = AutoSubstitutionBusiness.getDescription(ind.Data, ind.ID,false,ind.Type);
                }
            }

            string filename = Session["Insert_UploadedFile"].ToString();
            long docTypeId = long.Parse(Session["Insert_DocTypeId"].ToString());
            InsertResult res = InsertResult.NoInsertado;
            SResult sResult = new SResult();

            //Se crea el result a insertar
            INewResult newresult = new SResult().GetNewNewResult(docTypeId);
            res = sResult.Insert(newresult, filename, docTypeId, Indexs);

            if (res == InsertResult.Insertado)
            {
                if (newresult.Disk_Group_Id > 0 && 
                    VolumesBusiness.GetVolumeType(newresult.Disk_Group_Id) != (int)VolumeType.DataBase)
                {
                    //Guarda el documento en el volumen utilizando un webservice
                    string useWebService = zopt.GetValue("UseWebService");
                    string wsResultsUrl = zopt.GetValue("WSResultsUrl");
                    if (!String.IsNullOrEmpty(useWebService) && bool.Parse(useWebService) && !String.IsNullOrEmpty(wsResultsUrl))
                    {
                        sResult.CopyBlobToVolumeWS(newresult.ID, newresult.DocTypeId);
                    }
                }

                //Si se inserto limpiamos la variable de session.
                Session.Remove("SpecificAttrubutes" + _ruleCallTaskId);

                pnlDatos.Visible = false;
                lblInsertado.Visible = true;
                Session["SelectedsDocTypesIds"] = null;
                Session["CurrentInsertIndexs"] = null;
                string script = String.Empty;

                //Si no se llama al insertar por medio del asociar, refresco el tab de insersión.
                if (Request.QueryString["doctypeid"] == null)
                {
                    script += " setTimeout('parent.RefreshGeneralGrid(\"Insertar\", \"../Insert/Insert.aspx\", false);',500);";    
                }

                //Codigo de ejecucion de reglas de entrada de la nueva tarea
                STasks stasks = new STasks();
                ITaskResult Task = stasks.GetTaskByDocId(newresult.ID);

                string doctypeidsexc = zopt.GetValue("DTIDShowDocAfterInsert");
                bool opendoc = false;
                if (!string.IsNullOrEmpty(doctypeidsexc))
                {
                    foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (string.Compare(dtid.Trim(), newresult.DocTypeId.ToString()) == 0)
                            opendoc = true;
                    }
                }

                SUserPreferences SUserPreferences = new SUserPreferences();
                bool showDocAfterInsert = bool.Parse(SUserPreferences.getValue("ShowDocAfterInsert", Sections.InsertPreferences, "true"));
                if (showDocAfterInsert || opendoc)
                {
                    //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                    if (Task != null)
                    {
                        Page.Session.Add("Entrada" + newresult.ID, true);
                    }

                    string urlTask = "../WF/TaskSelector.ashx?docid=" + newresult.ID.ToString() + "&doctype=" + docTypeId.ToString();
                    script += "parent.SelectTaskFromModal();";
                    script += string.Format("parent.OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6});", 0, newresult.ID, newresult.DocTypeId, "false", newresult.Name, urlTask, hdnUserId.Value);
                }
                else
                {
                    if (Session["ListOfTask"] == null)
                    {
                        Session["ListOfTask"] = new List<IExecutionRequest>();
                    }

                    IExecutionRequest exec = new ExecutionRequest();
                    exec.ExecutionTask = Task;
                    exec.StartRule = -1;
                    ((List<IExecutionRequest>)Session["ListOfTask"]).Add(exec);

                    script += "parent.SetNewEntryRulesGroup();";
                }

                if (Request["CallTaskID"] != null)
                {
                    script += "parent.ShowLoadingAnimation(); parent.RefreshTab('#T" + Request["CallTaskID"] + "');";
                }

                if (Request["docid"] != null)
                {
                    script += "parent.RefreshTab('#D" + Request["docid"] + "');";
                    script += "parent.CloseInsert();";
                }

                if (!opendoc && Request["docid"] == null && !showDocAfterInsert)
                {
                    script += "parent.ShowInsertedDialog();";
               
                }

                script += " $('#IFDialogContent').unbind('load');";

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "DoOpenTaskScript",
                    "$(document).ready(function(){ parent.addEvent('IFDialogContent','load', function(){ " + script + "});   });", 
                    true);

                if (!String.IsNullOrEmpty(Request.QueryString["FillIndxDocTypeID"]) && !String.IsNullOrEmpty(Request.QueryString["isview"]) && Task != null)
                {
                    Session[Task.TaskId +  "CurrentExecution"] = null;
                    Session["EntryRulesExecution"] = null;
                }
            }
            else if (res == InsertResult.ErrorIndicesIncompletos || res == InsertResult.ErrorIndicesInvalidos)
            {
                lblMsj.Text = "Los indices marcados con (*) son de ingreso obligatorio";
                lblMsj.Visible = true;
            }
            else if (res == InsertResult.NoInsertado)
            {
                lblMsj.Text = "Se produjo un error al insertar el documento";
                lblMsj.Visible = true;
            }
            zopt = null;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Completa los atributos del result en base a la configuracion obtenida en la regla
    /// </summary>
    /// <param name="indexs"> </param>
    /// <param name="resultToAsociate"> </param>
    /// <param name="indexsToCopy"> </param>
    /// <param name="indexValues"> </param>
    /// <returns></returns>
    private List<IIndex> GetCompleteSpecificAttributes(List<IIndex> indexs, List<IIndex> indexsToCopy, ref Dictionary<long, string> indexValues)
    {
        int maxIndexsNewResult = indexs.Count;
        int maxIndexsResultAsoc = indexsToCopy.Count;
        IIndex index1;
        IIndex index2;
        List<IIndex> currIndexs = new List<IIndex>();

        //Iteramos por los atributos a compleatar
        for (int i = 0; i < maxIndexsNewResult; i++)
        {
            //Cacheamos el atributo para evitar casteos
            index1 = (IIndex) currIndexs[i];

            //si el diccionario tiene la key del atributo es que fue configurado para no completarse
            if (indexValues.ContainsKey(index1.ID))
            {
                ((IIndex)currIndexs[i]).Data = indexValues[index1.ID];
                ((IIndex)currIndexs[i]).DataTemp = indexValues[index1.ID];
            }
            else
            {
                //Si no contenia la key es porque se debe completar con los valores del padre,
                //siempre y cuando el padre tenga ese atributo, por eso se itera
                for (int j = 0; j < maxIndexsResultAsoc; j++)
                {
                    index2 = (IIndex)indexsToCopy[j];
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
    /// Se usa el cancel para limpiar la variable de session.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkCancel_clic(object sender, EventArgs e)
    {
        Session.Remove("SpecificAttrubutes" + _ruleCallTaskId);
    }
}
