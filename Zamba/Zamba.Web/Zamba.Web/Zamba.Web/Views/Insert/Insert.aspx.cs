using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Zamba.Services;
using System.Collections;
using Zamba.Core;
using Zamba;
using Zamba.Membership;
using System.Data;
using Zamba.Core.WF.WF;

public partial class Views_Insert_Insert : Page
{
    long _ruleCallTaskId;

    UserPreferences UP = new UserPreferences();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //if(Request.Params["__EVENTTARGET"] == "lnkInsertar")
            //{
            //    return;
            //}


            if (MembershipHelper.CurrentUser == null && Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined")
            {
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(long.Parse(Request.QueryString["userid"]), ClientType.Web);
            }

            if (MembershipHelper.CurrentUser != null)
            {

                ucDocTypes._DocTypesSelected += new ucDocTypes.DocTypesSelected(ucDocTypes__DocTypesSelected);

                String NR = string.Empty;
                string RefreshParentDataFromChildWindowScript = string.Empty;

                if (!Page.IsPostBack)
                {
                    ucDocTypesIndexs.Visible = false;
                    // ucUploadFile.Visible = false;
                    //  lnkInsertar.Visible = false;
                    lnkReplicar.Visible = false;
                    lnkRefresh.Visible = false;
                }
                if ((Request.Form["__EVENTTARGET"] == "UserControlBody" && Request.Form["__EVENTARGUMENT"] == "Refresh:0,1,2") )
                {

                    var script2 = "swal('Insertar', 'Su documento se inserto correctamente', 'success')";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fileuploaded", "$(document).ready(function(){" +  script2 + "});", true);
                }
                if (Request.Form["__EVENTTARGET"] == "UserControlBodyValidation" && Request.Form["__EVENTARGUMENT"] == "Refresh:0,1,2")
                {

                    var script2 = "swal('','Ingrese al menos un archivo', 'warning')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fileuploaded", "$(document).ready(function(){" +  script2 + "});", true);
                }
                if (Request.Form["__EVENTTARGET"] == "UserControlBodyValidationInputs" && Request.Form["__EVENTARGUMENT"] == "Refresh:0,1,2")
                {

                    var script2 = "swal('','Por favor completar campos obligatorios', 'warning')";                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fileuploaded", "$(document).ready(function(){" +  script2 + "});", true);
                }

                ucDocTypesIndexs.SaveButtonName = lnkInsertar.ClientID;
                //ucDocTypes.LoadDocTypes();
                ucDocTypes.LoadDocTypesByCreatePermission();
                SZOptBusiness zOptBusines = new SZOptBusiness();
                Page.Title = (string)zOptBusines.GetValue("WebViewTitle") + " - Insertar Documento";
                lblMsj.Text = string.Empty;
                currentUserName.InnerText = MembershipHelper.CurrentUser.Apellidos + " " + MembershipHelper.CurrentUser.Nombres;
                //string script = "$(document).ready(function() { CloseInsertingDialog(); });";
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "closeDialogSript", script, true);

                hdnUserId.Value = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

               
                //if (!String.IsNullOrEmpty(Request.QueryString["NR"]))
                //{
                //    NR = Request.QueryString["NR"];
                //    RefreshParentDataFromChildWindowScript = $" RefreshParentDataFromChildWindow({NR}); ";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "fileuploaded", "$(document).ready(function(){" + RefreshParentDataFromChildWindowScript + "});", true);
                //}


            }
            else
            {
                this.ucDocTypes.Visible = false;
                this.ucDocTypesIndexs.Visible = false;
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
            var entityChange = (Request.QueryString["entitychange"] == "true") ? true : false;
            //Si el pedido de insersión es desde el docViewer, llenar los atributos con los atributos del documento (así quedará asociado)

            if (Request["isview"] == "true")
            {
                Int32 tempDoctypeId;
                Int32 tempdocId;
                //19/07/11:Esta variable se usará en caso que el insert sea llamado de la regla DoAddAsociateDocument
                Int32 fillIndxDocTypeId;
                //Si cambia la entidad tiene que usar el docTypeId pasado por parametro para actualizar lista de index cuando se adjunta desde la tarea
                Int32.TryParse(entityChange ? docTypeId.ToString() : Request.QueryString["doctypeid"], out tempDoctypeId);
                Int32.TryParse(Request.QueryString["docid"], out tempdocId);
                //19/07/11:Llenamos la variable.
                Int32.TryParse(Request.QueryString["FillIndxDocTypeID"], out fillIndxDocTypeId);

                //Se llena primero los indices, si viene de regla traeria atributos sin valores.
                IResult res1 = null;
                SResult sResult = new SResult();

                if (tempDoctypeId > 0)
                {
                    res1 = sResult.GetResult(tempdocId, tempDoctypeId, true);

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
                    res1 = sResult.GetResult(tempdocId, fillIndxDocTypeId, true);

                //Si existe la configuracion de atributos procedemos a la carga
                if (haveSpecificAttributes)
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
                                indexToFill.DataTemp = indexWithData.Data;
                                indexToFill.dataDescription = indexWithData.dataDescription;
                                indexToFill.dataDescriptionTemp = indexWithData.dataDescription;
                            }
                        }
                    }
                }

                ucDocTypesIndexs.CurrentIndexs = indexs;
            }
            else
            {
                indexs = ZCore.GetInstance().FilterIndex(docTypeId,true,true );
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

            RightsBusiness RiB = new RightsBusiness();
            //Si se hace una busqueda combinada, si algun doctype tiene permiso para no filtrar indices
            //Bastaria para aplicar ese permiso a todos
            bool permisosFiltrarIndices = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, (Zamba.Membership.MembershipHelper.CurrentUser).ID);
            RiB = null;

            if (permisosFiltrarIndices == false)
                viewSpecifiedIndex = false;

            if (viewSpecifiedIndex)
            {
                Hashtable iri = new UserBusiness().GetIndexsRights(docTypeId, (Zamba.Membership.MembershipHelper.CurrentUser).ID);

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
                ///lnkInsertar.Visible = true;
                lnkReplicar.Visible = false;
                lnkRefresh.Visible = false;
                NavPanel.Update();
                //  this.DropPanel.Update();
            }
            else
            {
                ucDocTypesIndexs.Visible = false;
                //  ucUploadFile.Visible = false;
                //lnkInsertar.Visible = false;
                lnkReplicar.Visible = false;
                lnkRefresh.Visible = false;
                //  this.DropPanel.Update();
            }
            //$(document).ready(function() { parent.ResizeInsertDialogToShow(600); });
            var javaScript = "$(document).ready(function() {RemoveClassDropPanelDisplay(); SetAutocompleteIndex16();});";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "AutocompleteIndex16", script, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AutocompleteIndex16", javaScript, true);




            UpdatePanel2.Update();
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "resizeScript", script, true);
        lblMsj.Visible = true;
    }
    protected void lnkRefresh_click(object sender, EventArgs e)
    {
        Server.TransferRequest(Request.Url.AbsolutePath, false);
    }
    protected void lnkInsertar_clic(object sender, EventArgs e)
    {
        //return;
        if (Session["Insert_UploadedFile"] != null)
        {
            var lst = (List<string>)Session["Insert_UploadedFile"];
            bool resultInsert = true;

            if (lst.Count() > 0)
            {
                resultInsert = InsertDoc();
            }

            Session["LastInsert_UploadedFile"] = Session["Insert_UploadedFile"];
            Session["Insert_UploadedFile"] = null;

        }
        else
        {
            var script = "__doPostBack('UserControlBodyValidation','Refresh:0,1,2');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "FixUploadFile2", "$(document).ready(function(){" + script + "});", true);
            lnkReplicar.Visible = false;
            lnkRefresh.Visible = false;
        }
    }

    protected void lnkReplicar_clic(object sender, EventArgs e)
    {
        //return;
        bool resultInsert = true;
        if (Session["LastInsert_UploadedFile"] != null)
        {
            Session["Insert_UploadedFile"] = Session["LastInsert_UploadedFile"];

            var lst = (List<string>)Session["LastInsert_UploadedFile"];
            if (lst.Count() > 0)
                resultInsert = InsertDoc();
        }
        else
        {
            var script = "swal('', 'No hay docuemnto para replicar', 'warning');";
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                 "Noreplicascript",
                  "$(document).ready(function(){" + script + "});",
                  true
              );
        }
        Session["Insert_UploadedFile"] = null;
    }

    private bool InsertDoc()
    {
        try
        {
            UserPreferences UP = new UserPreferences();
            ZOptBusiness zopt = new ZOptBusiness();
            List<IIndex> indices = ucDocTypesIndexs.CurrentIndexs;
            List<IIndex> Indexs = new List<IIndex>();

            foreach (IIndex ind in indices)
            {
                Indexs.Add(ind);
                if (ind.Column == "I16")
                {
                    var OriginalText = ind.Data;

                    ind.Data = ind.Data.Split('-').First().Trim();
                    ind.Data2 = ind.Data2.Split('-').First().Trim();
                    ind.DataTemp = (string.IsNullOrEmpty(ind.DataTemp)) ? ind.Data.Split('-').First().Trim() : ind.DataTemp.Split('-').First().Trim();
                    ind.DataTemp2 = ind.DataTemp2.Split('-').First().Trim();

                    var indicesID = Convert.ToInt32(ind.Column.Replace("I", ""));

                    WFTaskBusiness WTB = new WFTaskBusiness();
                    List<WFTaskBusiness.CodigoDescripcion> list;

                    Boolean correctCode = false;


                    if (!string.IsNullOrEmpty(OriginalText))
                    {
                        var OriginalDes = string.Empty;
                        var OriginalCode = OriginalText.Split('-')[0].Trim();

                        if (OriginalText.Split('-').Length > 1 && OriginalText.Split('-').Length < 3)
                        {
                            OriginalDes = OriginalText.Split('-')[1].Trim();
                        }
                        if (OriginalText.Split('-').Length >= 3)
                        {
                            var a = @"-";
                            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(a);
                            string[] result = regex.Split(OriginalText, 2);
                            OriginalDes = result[1].Trim();

                        }

                        AutoSubstitutionBusiness Asb = new AutoSubstitutionBusiness();
                        var descripcionAsb = Asb.getDescription(OriginalCode, indicesID);

                        if ((!string.IsNullOrEmpty(descripcionAsb)) && descripcionAsb != OriginalCode)
                        {
                            if (OriginalDes.Length > 0)
                            {
                                if (OriginalDes == descripcionAsb)
                                {
                                    correctCode = true;
                                }

                            }
                            else
                            {
                                correctCode = true;
                                ind.Data = OriginalCode + " - " + descripcionAsb;
                                ind.DataTemp = OriginalCode + " - " + descripcionAsb;
                            }
                        }
                        else
                        {
                            var CodeAsb = Asb.getCode(OriginalCode, indicesID);
                            if (!string.IsNullOrEmpty(CodeAsb) && OriginalCode != CodeAsb)
                            {
                                correctCode = false;
                                ind.Data = CodeAsb + " - " + OriginalCode;
                                ind.DataTemp = CodeAsb + " - " + OriginalCode;
                            }
                        }
                    }
                    else
                    {
                        correctCode = true;
                    }


                    if (!correctCode)
                    {
                        string scriptFormato = String.Empty;
                        scriptFormato += "swal('', 'El campo cliente es incorrecto', 'warning');";
                        //Si se inserto limpiamos la variable de session.
                        Session.Remove("SpecificAttrubutes" + _ruleCallTaskId);
                        // Session["SelectedsDocTypesIds"] = null;
                        Session["CurrentInsertIndexs"] = null;

                        pnlDatos.Visible = true;
                        //script += "swal('Insertar', 'Su documento se inserto correctamente', 'success');";

                        var script2 = "__doPostBack('UserControlBodyValidationInputs','Refresh:0,1,2');";

                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                           "scriptFormato",
                            "$(document).ready(function(){" + scriptFormato + script2 + "});",
                            true
                        );
                        zopt = null;
                        return false;
                    }




                }


                if (ind.Required && (string.IsNullOrEmpty(ind.DataTemp) && string.IsNullOrEmpty(ind.Data)))
                {
                    lblMsj.Text = "Los indices marcados con (*) son de ingreso obligatorio";
                    lblMsj.Visible = true;
                    return false;
                }
                else if ((ind.DropDown == IndexAdditionalType.AutoSustitución || ind.DropDown == IndexAdditionalType.AutoSustituciónJerarquico) &&
                    !string.IsNullOrEmpty(ind.Data))
                {
                    var description = string.Empty;
                    description = new AutoSubstitutionBusiness().getDescription(ind.Data, ind.ID);
                    ind.dataDescription = description;
                    ind.dataDescriptionTemp = description;
                }
            }

            //string filename = Session["Insert_UploadedFile"].ToString();
            List<string> filenames = (List<string>)Session["Insert_UploadedFile"];
            long docTypeId = long.Parse(Session["Insert_DocTypeId"].ToString());
            InsertResult res = InsertResult.NoInsertado;
            SResult sResult = new SResult();
            string script = String.Empty;

            string doctypeidsexc = zopt.GetValue("DTIDShowDocAfterInsert");
            bool opendoc = false;
            bool showDocAfterInsert = bool.Parse(UP.getValue("ShowDocAfterInsert", UPSections.InsertPreferences, "true"));

            //Se crea el result a insertar
            foreach (string filename in filenames)
            {
                INewResult newresult = new SResult().GetNewNewResult(docTypeId);
                res = sResult.Insert(ref newresult, filename, docTypeId, Indexs, MembershipHelper.CurrentUser.ID);
                if (res == InsertResult.Insertado)
                {
                    if (newresult.Disk_Group_Id > 0 &&
                        VolumesBusiness.GetVolumeType(newresult.Disk_Group_Id) != (int)VolumeType.DataBase)
                    {
                        //Guarda el documento en el volumen utilizando un webservice
                        string useWebService = zopt.GetValue("UseWebService");
                        string wsResultsUrl = ZOptBusiness.GetValueOrDefault("WSResultsUrl", "http://www.zamba.com.ar/zambastardoc");
                        if (!String.IsNullOrEmpty(useWebService) && bool.Parse(useWebService) && !String.IsNullOrEmpty(wsResultsUrl))
                        {
                            sResult.CopyBlobToVolumeWS(newresult.ID, newresult.DocTypeId);
                        }
                    }

                    //Codigo de ejecucion de reglas de entrada de la nueva tarea
                    STasks stasks = new STasks();
                    ITaskResult Task = stasks.GetTaskByDocId(newresult.ID);

                    if (!string.IsNullOrEmpty(doctypeidsexc))
                    {
                        foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (string.Compare(dtid.Trim(), newresult.DocTypeId.ToString()) == 0)
                                if (Task.ISVIRTUAL) opendoc = true; else opendoc = false;
                        }
                    }

                    if (showDocAfterInsert || opendoc)
                    {
                        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                        if (Task != null)
                        {
                            if (Page.Session["Entrada" + Task.ID] == null)
                                Page.Session.Add("Entrada" + newresult.ID, true);
                        }

                        //string urlTask = "../WF/TaskSelector.ashx?docid=" + newresult.ID.ToString() + "&doctype=" + docTypeId.ToString() + "&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                        //script += "SelectTaskFromModal();";
                        //script += string.Format("OpenDocTask3({0},{1},{2},{3},'{4}','{5}',{6});", 0, newresult.ID, newresult.DocTypeId, "false", newresult.Name, urlTask, hdnUserId.Value);
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

                        //script += "SetNewEntryRulesGroup();";
                    }

                    if (!String.IsNullOrEmpty(Request.QueryString["FillIndxDocTypeID"]) && !String.IsNullOrEmpty(Request.QueryString["isview"]) && Task != null)
                    {
                        Session[Task.TaskId + "CurrentExecution"] = null;
                        Session["EntryRulesExecution"] = null;
                    }

                        var script2 = "__doPostBack('UserControlBody','Refresh:0,1,2');";

                    script += "swal('', 'Su documento se inserto correctamente, cantidad de archivos(" + filenames.Count.ToString() + ")', 'success'); " + script2;

                }
                else if (res == InsertResult.ErrorIndicesIncompletos || res == InsertResult.ErrorIndicesInvalidos)
                {
                    script += "swal('', 'No se pudo insertar el documento debido que existen indices incompletos', 'warning');";
                    //lblMsj.Text = "Los indices marcados con (*) son de ingreso obligatorio";
                    //lblMsj.Visible = true;
                }
                else if (res == InsertResult.NoInsertado)
                {
                    script += "swal('', 'Su documento no se pudo insertar correctamente', 'error');";
                    //lblMsj.Text += "Se produjo un error al insertar el documento: " + filename;
                    //lblMsj.Visible = true;
                }
            }

            //Si se inserto limpiamos la variable de session.
            Session.Remove("SpecificAttrubutes" + _ruleCallTaskId);
            Session["SelectedsDocTypesIds"] = null;
            Session["CurrentInsertIndexs"] = null;

            pnlDatos.Visible = true;
            //script += "swal('Insertar', 'Su documento se inserto correctamente', 'success');";

            String NR = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["NR"]))
            {
                NR = Request.QueryString["NR"];
            }

            String PrincipalView = String.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["InsertView"]))
            {
                PrincipalView = Request.QueryString["InsertView"];
            }

            if (PrincipalView != "Main")
            {
                var RefreshParentDataFromChildWindowScript = $" RefreshParentDataFromChildWindow({NR}); ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FixUploadFile3", " $(document).ready(function(){" + RefreshParentDataFromChildWindowScript + script + "});", true);
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FixUploadFile3", " $(document).ready(function(){" + script + "});", true);
            }
            
            //Page.ClientScript.RegisterStartupScript(this.GetType(),
            //   "DoOpenTaskScript",
            //    "$(document).ready(function(){" + RefreshParentDataFromChildWindowScript + script + "});",
            //    true
            //);


            
            zopt = null;
            return true;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            var script = "swal('', 'Ocurrio un error al insertar el documento.', 'error');";
            var script2 = " __doPostBack('UserControlBodyValidationInputs','Refresh:0,1,2');";
       
            Page.ClientScript.RegisterStartupScript(this.GetType(),
               "InsertErrorScript",
                "$(document).ready(function(){" + script + script2 +  "});",
                true
            );

            return false;
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
        //List<IIndex> currIndexs = new List<IIndex>();

        //Iteramos por los atributos a compleatar
        for (int i = 0; i < maxIndexsNewResult; i++)
        {
            //Cacheamos el atributo para evitar casteos
            index1 = (IIndex)indexs[i];

            //si el diccionario tiene la key del atributo es que fue configurado para no completarse
            if (indexValues.ContainsKey(index1.ID))
            {
                ((IIndex)indexs[i]).Data = indexValues[index1.ID];
                ((IIndex)indexs[i]).DataTemp = indexValues[index1.ID];
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
                        ((IIndex)indexs[i]).Data = index2.Data;
                        ((IIndex)indexs[i]).DataTemp = index2.DataTemp;
                        break;
                    }
                }
            }
        }

        return indexs;
    }

    /// <summary>
    /// Se usa el cancel para limpiar la variable de session.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkCancel_clic(object sender, EventArgs e)
    {
        Server.TransferRequest(Request.Url.AbsolutePath, false);
    }
}
