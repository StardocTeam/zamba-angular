using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Zamba.Services;
using Zamba.Core;
using System.Web;
using System.Text;
using System.Linq;
using Zamba.Core.WF.WF;
using Zamba;
using Zamba.Web.App_Code.Helpers;

namespace Controls.Indexs
{
    public delegate void Saved();
    public partial class DocTypesIndexs : UserControl
    {
        #region "Atributos y propiedades"
        const Int16 INDEX_CTRL_LENGTH = 6;
        const Int16 CTR_INDEX_TB_WIDTH = 164;
        const Int16 CTR_INDEX_ILST_WIDTH = 164;
        const Int16 CTR_INDEX_DATE_WIDTH = 164;
        const Int16 CTR_INDEX_SLST_WIDTH = 138;

        private const string HIERARCHICALFUNCTIONTEMPLATE = "GetHierarchyOptions({0},{1},{2},{3});";
        private const string PARENTVALUEACCESS = "$('#{0}').val()";
        private Saved _dSaved = null;
        const Int16 INDEX_ID = 0;
        const Int16 INDEX_NAME = 1;
        const Int16 INDEX_VALUE = 2;
        const Int16 INDEX_VALUE_ID = 3;
        public string IdButton;
        private bool _readOnly = false;
        protected WebModuleMode currentmode;
        private SIndex sIndex;
        enum Visibility
        {
            Visible = 1,
            NotVisible = 2,
            Disabled = 3
        };


        public LinkButton SaveChanges
        {
            get { return btnSaveChanges; }
            set { btnSaveChanges = value; }
        }

        public string SaveChangesImgUrl
        {
            get { return btnSaveImage.ImageUrl; }
            set { btnSaveImage.ImageUrl = value; }
        }

        public LinkButton CleanIndexs
        {
            get { return btnCleanIndexs; }
            set { btnCleanIndexs = value; }
        }

        public string CleanIndexImgUrl
        {
            get { return btnCleanImage.ImageUrl; }
            set { btnCleanImage.ImageUrl = value; }
        }

        //public Panel ShowHideIndexs
        //{
        //    get { return btnShowHideContainer; }
        //    set { btnShowHideContainer = value; }
        //}

        public Table IndexsTable
        {
            get { return tblIndices; }
        }

        public Int64? DtId
        {
            private get
            {
                Int64? NullableValue;
                Int64 Value;

                if (Int64.TryParse(hdDTId.Value, out Value))
                    NullableValue = Value;
                else
                    NullableValue = null;

                return Value;
            }
            set
            {
                if (value.HasValue)
                {

                    if (sIndex == null)
                        sIndex = new SIndex();

                    LoadEsquemaIndexs(value.Value);
                    hdDTId.Value = value.Value.ToString();
                }
                else
                {
                    Clear();
                    hdDTId.Value = String.Empty;
                }
            }
        }

        public List<IIndex> CurrentIndexs
        {
            get
            {
                if (Session["CurrentIndexs"] != null)
                    return (List<IIndex>)Session["CurrentIndexs"];

                return new List<IIndex>();
            }
            set
            {
                Session["CurrentIndexs"] = value;

            }
        }

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
            }
        }

        public event Saved OnSave
        {
            add
            {
                _dSaved += value;
            }
            remove
            {
                _dSaved -= value;
            }
        }
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            sIndex = new SIndex();

            if (Page.IsPostBack)
            {
                try
                {
                    CurrentIndexs = ParsePostBackFormToIndexs(CurrentIndexs);

                    ScriptManager scriptMngr = ScriptManager.GetCurrent(this.Page);
                    if (scriptMngr != null && !scriptMngr.IsInAsyncPostBack)
                    {
                        ShowIndexs(CurrentIndexs, currentmode);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                string eventTarget = Request.Form["__EVENTTARGET"];
                if (eventTarget.Contains("btnOpenPopup_"))
                {
                    eventTarget = eventTarget.Replace("btnOpenPopup_", string.Empty);
                    Int64 indexId;

                    if (Int64.TryParse(eventTarget, out indexId))
                    {
                        OpenPopUp(indexId);
                    }
                }
            }
        }

        protected void gvSustitutionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hdIndexIdIndex = Session["hdIndexId"].ToString();
            Session["hdIndexId"] = string.Empty;
            Int64 IndexId = Int64.Parse(hdIndexIdIndex);
            if (sIndex == null)
            {
                sIndex = new SIndex();
            }

            GridView gv = (sender as GridView);

            TextBox tx = tblIndices.FindControl(IndexId.ToString()) as TextBox;
            tx.Text = gv.DataKeys[gv.SelectedIndex].Values[1].ToString();

            tx = tblIndices.FindControl("Value§" + IndexId.ToString()) as TextBox;
            tx.Text = gv.DataKeys[gv.SelectedIndex].Values[0].ToString();

            UpdateHierachicalIndexFromParent(sIndex.GetIndex(IndexId), tx.Text);

            pnlListadoIndices.Visible = true;
            pnlSustitutionList.Visible = false;
        }


        protected void btnCancel_Onclick(object sender, EventArgs e)
        {
            pnlListadoIndices.Visible = true;
            pnlSustitutionList.Visible = false;
        }

        private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dp = (DropDownList)sender;

            if (dp.SelectedItem != null)
                UpdateHierachicalIndexFromParent(sIndex.GetIndex(long.Parse(dp.ID)), dp.SelectedValue);
        }

        private void UpdateHierachicalIndexFromParent(IIndex parentIndex, string parentNewValue)
        {
            if (parentIndex.HierarchicalChildID != null && parentIndex.HierarchicalChildID.Count > 0)
            {
                parentIndex.DataTemp = parentNewValue;

                int max = parentIndex.HierarchicalChildID.Count;
                IIndex childIndex;

                for (int i = 0; i < max; i++)
                {
                    childIndex = sIndex.GetIndex(parentIndex.HierarchicalChildID[i]);
                    UpdateHierachicalIndex(parentIndex, childIndex);
                }
            }
        }

        private void UpdateHierachicalIndex(IIndex parentIndex, IIndex childIndex)
        {
            try
            {
                DropDownList dp;
                TextBox tb;
                bool flagClearDesc = false;
                long ClearDescId = -1;
                DataTable dtOptions;

                if (childIndex != null)
                {
                    dtOptions = sIndex.GetHierarchyTableByValue(childIndex.ID, parentIndex);

                    IEnumerable<Control> allControls = FlattenHierachy(Page).Where(t => t.GetType() == typeof(DropDownList) || t.GetType() == typeof(TextBox));

                    foreach (Control ct in allControls)
                    {
                        if (ct is DropDownList)
                        {
                            dp = (DropDownList)ct;

                            long id;
                            if (!long.TryParse(dp.ID, out id) || id != childIndex.ID) continue;

                            dp.Items.Clear();
                            dp.Items.Add(String.Empty);

                            if (childIndex.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                            {
                                foreach (DataRow item in dtOptions.Rows)
                                {
                                    dp.Items.Add(new ListItem(item["Description"].ToString(), item["Value"].ToString()));
                                }
                            }
                            else
                            {
                                foreach (DataRow item in dtOptions.Rows)
                                {
                                    dp.Items.Add(new ListItem(item["Value"].ToString(), item["Value"].ToString()));
                                }
                            }
                        }
                        else
                        {
                            tb = (TextBox)ct;
                            if (tb.ID == "Value§" + childIndex.ID && !IndexsBusiness.ValidateHierarchyValue(tb.Text, childIndex.ID, parentIndex.ID, parentIndex.DataTemp))
                            {
                                tb.Text = string.Empty;
                                flagClearDesc = true;
                                ClearDescId = childIndex.ID;
                            }
                        }
                    }

                    if (flagClearDesc)
                    {
                        ((TextBox)allControls.Where(t => t.GetType() == typeof(TextBox) && t.ID == ClearDescId.ToString()).Single()).Text = string.Empty;
                    }

                    if (childIndex.HierarchicalChildID != null && childIndex.HierarchicalChildID.Count > 0)
                    {
                        UpdateHierachicalIndexFromParent(childIndex, childIndex.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private static IEnumerable<Control> FlattenHierachy(Control root)
        {
            List<Control> list = new List<Control> { root };
            if (root.HasControls())
            {
                foreach (Control control in root.Controls)
                {
                    list.AddRange(FlattenHierachy(control));
                }
            }
            return list.ToArray();
        }

        private void OpenPopUp(Int64 indexId)
        {


            IIndex currentIndex = sIndex.GetIndex(indexId);

            DataTable dt = null;
            if (currentIndex.DropDown == IndexAdditionalType.AutoSustitución || currentIndex.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
            {
                dt = sIndex.GetIndexData(indexId, true);
                gvSustitutionList.DataKeyNames = new[] { "Codigo", "Descripcion" };
            }
            else
            {
                if (currentIndex.HierarchicalParentID > 0)
                {
                    IList<IIndex> currentIndexs = (IList<IIndex>)MakeSearchIndexsList();

                    IIndex parentIndex = null;
                    foreach (IIndex indx in currentIndexs)
                    {
                        if (currentIndex.HierarchicalParentID == indx.ID)
                        {
                            parentIndex = indx;
                            break;
                        }
                    }
                    IndexsBusiness IB = new IndexsBusiness();

                    if (parentIndex == null) parentIndex = IB.GetIndex(currentIndex.HierarchicalParentID);
                    dt = sIndex.GetHierarchyTableByValue(indexId, parentIndex);
                    gvSustitutionList.DataKeyNames = new[] { "Value", "Description" };
                }
            }

            gvSustitutionList.DataSource = dt;
            gvSustitutionList.DataBind();
            gvSustitutionList.Attributes.Add("IndexId", indexId.ToString());
            hdIndexId.Value = indexId.ToString();

            // Se guarda el id del botón en la variable global. De esta forma, cuando se seleccione una fila, parte del contenido de la fila tiene que 
            // ir al textbox que se creo de forma dinámica, y para identificar ese textbox es necesario primero identificar al botón que se cliqueo. Por 
            // eso, cuando se recorra la tabla tblIndices, y el SkinId del botón sea igual al SkinId del idButton, entonces el textbox que esta al lado 
            // del botón guarda el contenido de parte de la fila seleccionada

            //guarda el boton seleccionado en el cliente
            //        ViewState["idButton"] = boton.CommandArgument;
            Session["hdIndexId"] = hdIndexId.Value;
            pnlListadoIndices.Visible = false;
            pnlSustitutionList.Visible = true;
            UpdatePanel Panel = (UpdatePanel)this.Parent.Parent;
            Panel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            Panel.Update();
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"Seleccionar Indice","popupsust();");

            //AutoSubstitutionSrv.Close();
        }
        void btPopup_Click(object sender, EventArgs e)
        {
            Session.Add("popup", true); //UN flag feo muy feo
            //ESTE EVENTO NO DEBE SER BORRADO.
            //SE UTILIZA PARA PODER LLAMAR AL METODO OpenPopUp.
        }

        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {


            DataTable dt = sIndex.GetIndexData(long.Parse(hdIndexId.Value), true);

            if (dt == null) return;
            dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            gvSustitutionList.DataSource = dt.DefaultView.ToTable();
            gvSustitutionList.DataKeyNames = new[] { "Codigo", "Descripcion" };
            gvSustitutionList.DataBind();

            pnlSustitutionList.Visible = true;
            pnlListadoIndices.Visible = false;
        }

        protected void SaveIndexChanges_Click(object sender, EventArgs e)
        {
            
                
            _dSaved();

            var SaveIndexResult = Session["SaveIndexResult"];
            if (SaveIndexResult == null)
            {
                string script = "$(document).ready(function() { swal('', 'Verifique Modificaciones en atributos', 'info'); });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "IndicesPanel", script, true);
            }
            else {

                Session["SaveIndexResult"] = string.Empty;
                if (SaveIndexResult.ToString() == "true")
                {
                    string script = "$(document).ready(function() { swal('', 'Se Actualizo Atributo Correctamente', 'success'); location.reload(); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "IndicesPanel", script, true);


                }
                else
                {


                    string script = "$(document).ready(function() { swal('', 'Debe modificar por lo menos un atributo', 'info'); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "IndicesPanel", script, true);

                }

            }

        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Obtiene la visibilidad del indice dependiendo de sus permisos
        /// </summary>
        /// <param name="index"></param>
        /// <param name="IRI"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private Visibility GetIndexVisibility(IIndex index, Hashtable IRI, IUser user)
        {
            SRights Rights = new SRights();
            Int64 doctypeid = (long)DtId;
            RightsBusiness RiB = new RightsBusiness();
            //Verifica si tiene permisos de visualización
            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid))
            {
                IndexsRightsInfo IR = (IndexsRightsInfo)IRI[index.ID];

                //Si es referencial debe estar deshabilitado
                if (IR == null)
                    if (index.isReference || !index.Enabled)
                    {
                        return Visibility.Disabled;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }

                if (IR.GetIndexRightValue(RightsType.IndexView))
                {
                    //Si es referencial debe estar deshabilitado
                    if (index.isReference || !index.Enabled || !IR.GetIndexRightValue(RightsType.IndexEdit))
                    {
                        return Visibility.Disabled;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return Visibility.NotVisible;
                }
            }
            else
            {
                return Visibility.Visible;
            }
        }

        ///// <summary>
        ///// Obtiene la procedencia del usercontrol. No es lo mejor pero safa.
        ///// </summary>
        ///// <returns></returns>
        //private WebModuleMode GetFrom()
        //{
        //    if (Request.Url.ToString().ToLower().Contains("search.aspx"))
        //        return WebModuleMode.Search;
        //    else if (Page.Request.Url.OriginalString.Contains("/Insert/Insert.aspx"))
        //        return WebModuleMode.Insert;
        //    else
        //        return WebModuleMode.Result;
        //}

        private void LoadEsquemaIndexs(Int64 dtId)
        {
            Visible = true;



            hdDTId.Value = dtId.ToString();

            DataTable dtIndexs = sIndex.getIndexByDocTypeId(dtId).Tables[0];

            tblIndices.Controls.Clear();

            TableRow currentRow = null;
            TableCell tcIndexName = null;
            TableCell tcIndexValue = null;
            TextBox lbValue = null;

            foreach (DataRow currentIndex in dtIndexs.Rows)
            {
                tcIndexName = new TableCell
                {
                    Text = currentIndex.ItemArray[1].ToString(),
                    ToolTip = currentIndex.ItemArray[1].ToString()
                };

                tcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                tcIndexName.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");

                lbValue = new TextBox { Enabled = true, Text = String.Empty, ToolTip = String.Empty };
                lbValue.Attributes.Add("OnClick", "IndexsChanged");
                lbValue.ID = "Index" + currentIndex.ItemArray[0];

                tcIndexValue = new TableCell();
                tcIndexValue.Controls.Add(lbValue);
                tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");



                currentRow = new TableRow();
                currentRow.Cells.Add(tcIndexName);
                currentRow.Cells.Add(tcIndexValue);

                tblIndices.Rows.Add(currentRow);
            }

            if (null != currentRow)
            {
                currentRow.Dispose();
            }

            if (null != tcIndexName)
            {
                tcIndexName.Dispose();
            }

            if (null != tcIndexValue)
            {
                tcIndexValue.Dispose();
            }

            if (null != lbValue)
            {
                lbValue.Dispose();
            }

            dtIndexs.Clear();

            // IndexSrv.Close();
        }

        protected void IndexsChanged(object sender, EventArgs e)
        {
            foreach (Index indice in CurrentIndexs)
            {
                if (indice.ID == int.Parse(((WebControl)sender).ID))
                {
                    indice.Data = ((TextBox)sender).Text;
                }
            }
        }

      

        /// <summary>
        /// Clears the inner controls 
        /// </summary>
        public void Clear()
        {
            CurrentIndexs = null;
            tblIndices.Controls.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<IIndex> GetIndexs()
        {
            List<IIndex> indices = (List<IIndex>)Session["CurrentIndexs"];
            foreach (TableRow row in tblIndices.Rows)
            {
                foreach (IIndex indice in indices)
                {
                    if (indice.Name != row.Cells[0].Text) continue;
                    string dato = GetControlText(row.Cells[1].Controls[0]);
                    indice.DataTemp = dato;
                    indice.Data = dato;

                    break;
                }
            }
            return indices;
        }

        /// <summary>
        /// Retorna el valor de la propiedad text del control.
        /// Si el control no tiene la propiedad text retorna vacio.
        /// </summary>
        /// <param name="ctr">Control</param>
        /// <returns>valor de la propiedad text</returns>
        private string GetControlText(Control ctr)
        {
            //string dato = string.Empty;

            //if (ctr is DropDownList)
            //    dato = Server.HtmlDecode(((DropDownList)ctr).Text);
            //else if (ctr is TextBox)
            //    dato = Server.HtmlDecode(((TextBox)ctr).Text);


            return HttpContext.Current.Request.Form[ctr.UniqueID];
        }

        /// <summary>
        /// Transforma el Reques.Form en un array de índices con sus correspondientes propiedades.
        /// </summary>
        /// <param name="indexToParse">Lista de índices para parsear.</param>
        /// <returns>Lista de índices con valores parseados.</returns>
        private List<IIndex> ParsePostBackFormToIndexs(List<IIndex> indexToParse)
        {
            int indexCount = indexToParse.Count;
            List<IIndex> indexToReturn = new List<IIndex>();
            indexToReturn.AddRange((IIndex[])indexToParse.ToArray<IIndex>().Clone());
            System.Collections.Specialized.NameValueCollection postBackForm = HttpContext.Current.Request.Form;
            int keysCount = postBackForm.AllKeys.Length;
            string indexId, currentKey;

            for (int i = 0; i < indexCount; i++)
            {
                if (indexToReturn[i] != null)
                {
                    indexId = indexToReturn[i].ID.ToString();
                    for (int j = 0; j < keysCount; j++)
                    {
                        currentKey = postBackForm.AllKeys[j];

                        //NOTA: EN CASO DE QUE SE REQUIERA LA DESCRIPCION DEL SLST DEBERIA AGREGARSE EN ESTE BLOQUE DE CODIGO
                        if (currentKey != null)
                        {
                            var CurrentIndexId = currentKey.Split('$').Last();
                            if (CurrentIndexId == indexId &&
                                (indexToReturn[i].DropDown != IndexAdditionalType.AutoSustitución &&
                                indexToReturn[i].DropDown != IndexAdditionalType.AutoSustituciónJerarquico ||
                                indexToReturn[i].Operator == "Es nulo"))
                            {

                                if (indexToReturn[i].Operator == "Es nulo")
                                {
                                    indexToReturn[i].Data = "";
                                    indexToReturn[i].DataTemp = "";
                                }
                                else
                                {
                                    //Se guarda el valor a buscar
                                    indexToReturn[i].Data = postBackForm[currentKey];
                                    indexToReturn[i].DataTemp = postBackForm[currentKey];
                                }

                                //Se guarda el operador
                                indexToReturn[i].Operator = postBackForm[currentKey.Substring(0, currentKey.LastIndexOf("$")) + "$SF" + indexId];

                                //Si el operador es Entre, debe guardar el otro valor por el cual se comparará
                                if (indexToReturn[i].Operator == "Entre")
                                {
                                    indexToReturn[i].Data2 = postBackForm[currentKey + "ID2"];
                                    indexToReturn[i].DataTemp2 = postBackForm[currentKey + "ID2"];
                                }

                                break;
                            }
                            else
                            {
                                 var CurrentIndexIds = CurrentIndexId.Split('§').Last();
                                if (CurrentIndexIds == indexId)
                                {
                                    //Se guarda el valor a buscar
                                    indexToReturn[i].Data = postBackForm[currentKey];
                                    indexToReturn[i].DataTemp = postBackForm[currentKey];

                                    //Se guarda el operador
                                    indexToReturn[i].Operator = postBackForm[currentKey.Substring(0, currentKey.LastIndexOf("$")) + "$SF" + indexId];

                                    break;
                                }
                            }


                        }
                    }
                }
            }

            return indexToReturn;
        }

        /// <summary>
        ///  Crea una grilla a partir de una 
        ///  lista de indices
        /// </summary>    
        /// <param name="indexs">Lista de indices</param>    
        /// <history>
        /// </history>
        /// 
        public void ShowIndexs(List<IIndex> indexs, WebModuleMode mode)
        {
            currentmode = mode;
            try
            {
                if (sIndex == null)
                {
                    sIndex = new SIndex();
                }

                bool showBetweenTextbox = false;
                bool showValueControls = true;


                ITaskResult taskResult;
                if (string.IsNullOrEmpty(lbTaskId.Text.Trim()))
                {
                    taskResult = null;
                }
                else
                {
                    STasks service = new STasks();
                    taskResult = service.GetTask(int.Parse(lbTaskId.Text.Trim()), Zamba.Membership.MembershipHelper.CurrentUser.ID);
                }
                //15/07/11: Se debe reconsiderar los casos de usos de el currentIndexs, ya que esto es un parche rápido
                if ((!Page.IsPostBack && (mode == WebModuleMode.Search || mode == WebModuleMode.Result)) || Page.IsPostBack || Session["ShowIndexOfInsert"] != null)
                {
                    if (((indexs != null && indexs.Count > 0) || CurrentIndexs == null) && !(Session["popup"] != null && (Boolean)Session["popup"])) CurrentIndexs = indexs;
                    else
                    {
                        indexs = CurrentIndexs;
                        Session["popup"] = false;
                    }

                }

                tblIndices.Controls.Clear();

                if ((CurrentIndexs.Count == 0 && ((List<Int64>)Session["SelectedsDocTypesIds"]) != null && ((List<Int64>)Session["SelectedsDocTypesIds"]).Count == 0 && mode != WebModuleMode.Insert) || (mode == WebModuleMode.Insert && Session["ShowIndexOfInsert"] == null))
                {
                    lblSelectIndex.Visible = true;
                }
                else
                {
                    lblSelectIndex.Visible = false;
                    TableRow currentRow = null;
                    TableCell tcIndexName = null;
                    TableCell tcIndexValue = null;
                    TableCell tcCmdPopup;
                    Label lbName;
                    TextBox tbValue = null;
                    TextBox TbDescription;
                    Button btPopup = null;
                    DropDownList cbValue;
                    DropDownList cbFilters = null;
                    Table tbNewIndex = null;
                    TableRow trNewIndex = null;
                    TableCell tcNewIndex = null;
                    UpdatePanel upNewIndex;
                    TableCell tcFilterSearch = null;
                    int lenind = 0;
                    SRights Rights = new SRights();
                    Hashtable IRI = new UserBusiness().GetIndexsRights((Int64)DtId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                    StringBuilder sbHierarchyEvent;
                    int maxArray;
                    bool isEnabled = true;
                    string strIndexId;

                    //Longitud de los atributos
                    int lenAttrib, lenDescription;
                    if (mode == WebModuleMode.Result)
                    {
                        lenAttrib = 200;
                        lenDescription = 170;
                    }
                    else
                    {
                        lenAttrib = 320;
                        lenDescription = 225;
                    }

                    foreach (Index currentIndex in (Page.IsPostBack ? indexs : CurrentIndexs))
                    {
                        if (currentIndex.Name.Length > lenind)
                            lenind = currentIndex.Name.Length;
                    }

                    //Configura un ancho para la descripción de los índices
                    lenind *= INDEX_CTRL_LENGTH;

                    foreach (Index currentIndex in (Page.IsPostBack ? indexs : CurrentIndexs))
                    {
                        if (mode == WebModuleMode.Result)
                        {
                            //Verifica la visibilidad del índice
                            switch (GetIndexVisibility(currentIndex, IRI, Zamba.Membership.MembershipHelper.CurrentUser))
                            {
                                case Visibility.Disabled:
                                    isEnabled = false;
                                    break;
                                case Visibility.Visible:
                                    isEnabled = true;
                                    break;
                                case Visibility.NotVisible:
                                    continue;
                            }
                        }

                        trNewIndex = new TableRow();
                        tcNewIndex = new TableCell();
                        tbNewIndex = new Table();
                        trNewIndex.Cells.Add(tcNewIndex);
                        // tcNewIndex.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");

                        tcIndexName = new TableCell();
                        strIndexId = currentIndex.ID.ToString();
                        lbName = new Label
                        {
                            Text = currentIndex.Name,
                            Width = lenind,
                            ID = strIndexId + "L"
                        };

                        //lbName.Style.Add(HtmlTextWriterStyle.FontSize, "9px");
                        tcIndexName.Controls.Add(lbName);
                        tcIndexName.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");

                        if (mode == WebModuleMode.Search)
                        {
                            tcFilterSearch = new TableCell();
                            tcFilterSearch.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                            cbFilters = new DropDownList { Width = 80 };

                            if (currentIndex.Type.ToString().Contains("Alfanumerico"))
                            {
                                cbFilters.Items.Add("=");
                                cbFilters.Items.Add("Empieza");
                                cbFilters.Items.Add("Termina");
                                cbFilters.Items.Add("Contiene");
                                cbFilters.Items.Add("Distinto");
                                cbFilters.Items.Add("Alguno");
                                cbFilters.Items.Add("Es nulo");
                            }
                            else if (currentIndex.Type == IndexDataType.Si_No)
                            {
                                cbFilters.Items.Add("=");
                                cbFilters.Items.Add("<>");
                            }
                            else
                            {

                                if (currentIndex.DropDown == IndexAdditionalType.AutoSustitución || currentIndex.DropDown ==
                                                                     IndexAdditionalType.AutoSustituciónJerarquico)
                                {
                                    cbFilters.Items.Add("=");
                                    cbFilters.Items.Add(">");
                                    cbFilters.Items.Add(">=");
                                    cbFilters.Items.Add("<");
                                    cbFilters.Items.Add("<=");
                                    cbFilters.Items.Add("<>");
                                    cbFilters.Items.Add("Es nulo");
                                }
                                else
                                {
                                    cbFilters.Items.Add("=");
                                    cbFilters.Items.Add(">");
                                    cbFilters.Items.Add(">=");
                                    cbFilters.Items.Add("<");
                                    cbFilters.Items.Add("<=");
                                    cbFilters.Items.Add("<>");
                                    cbFilters.Items.Add("Entre");
                                    cbFilters.Items.Add("Es nulo");
                                }

                            }

                            cbFilters.SelectedIndexChanged += SetFilterSearch;
                            cbFilters.ID = "SF" + strIndexId;
                            cbFilters.AutoPostBack = true;

                            tcFilterSearch.Controls.Add(cbFilters);


                            //Verificaciones sobre los operadores
                            if (currentIndex.Operator == "Es nulo")
                            {
                                showValueControls = false;
                            }
                            else
                            {
                                showValueControls = true;
                                if (currentIndex.Operator == "Entre")
                                    showBetweenTextbox = true;
                                else
                                    showBetweenTextbox = false;
                            }
                        }

                        if (currentIndex.Type == IndexDataType.Fecha || currentIndex.Type == IndexDataType.Fecha_Hora)
                        {
                            string ReplaceFormatDate = string.Empty;
                            if (currentIndex.Type == IndexDataType.Fecha)
                            {
                                var FormatDate = currentIndex.Data.Split('/');
                                if (currentIndex.Data != "")
                                {
                                    ReplaceFormatDate = FormatDate[2] + "-" + FormatDate[1] + "-" + FormatDate[0];

                                }
                            }
                            if (currentIndex.Type == IndexDataType.Fecha_Hora)
                            {
                                var FormatDate = currentIndex.Data.Split(' ');
                                FormatDate = FormatDate[0].Split('/');
                                if (currentIndex.Data != "")
                                {
                                    ReplaceFormatDate = FormatDate[2] + "-" + FormatDate[1] + "-" + FormatDate[0];

                                }

                            }
                           
                            

                            tbValue = new TextBox
                            {
                                Text = ReplaceFormatDate,
                                Width = (mode == WebModuleMode.Search) ? 150 : CTR_INDEX_DATE_WIDTH,
                                ToolTip = currentIndex.Data,
                                CssClass = "form-control EntryIndex",
                                ID = strIndexId,
                                Visible = showValueControls
                            };

                            if (!isEnabled)
                            {
                                tbValue.CssClass += " readOnly";
                                tbValue.Attributes.Add("readonly", "readonly");
                            }
                            else
                            {
                                //Se agregan los atributos necesarios para generar las validaciones
                                tbValue = (TextBox)Validators.GetControlWithValidations(currentIndex, tbValue, mode, taskResult, ZFieldType.IndexField);
                            }

                            tcIndexValue = new TableCell();
                            tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                            tcIndexValue.Controls.Add(tbValue);

                            currentRow = new TableRow();
                            currentRow.Cells.Add(tcIndexName);


                            if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                currentRow.Cells.Add(tcFilterSearch);

                            currentRow.Cells.Add(tcIndexValue);

                            if (isEnabled) currentRow.ToolTip = "Haga click para insertar la fecha";

                            if (!currentIndex.Type.ToString().Contains("Alfanumerico"))
                            {
                                tbValue = new TextBox
                                {
                                    Text = currentIndex.Data,
                                    Width = (mode == WebModuleMode.Search) ? 150 : CTR_INDEX_DATE_WIDTH,
                                    ToolTip = currentIndex.Data,
                                    CssClass = "form-control EntryIndex",
                                    ID = strIndexId + "ID2",
                                    Visible = showBetweenTextbox
                                };

                                tbValue = (TextBox)Validators.GetControlWithValidations(currentIndex, tbValue, mode, taskResult, ZFieldType.IndexField);
                                tcIndexValue = new TableCell { Visible = showBetweenTextbox };
                                tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");

                                tcIndexValue.Controls.Add(tbValue);

                                currentRow.Cells.Add(tcIndexValue);
                            }
                        }
                        else
                        {
                            switch (currentIndex.DropDown)
                            {
                                case IndexAdditionalType.AutoSustitución:
                                case IndexAdditionalType.AutoSustituciónJerarquico:
                                    tbValue = new TextBox
                                    {
                                        Text = currentIndex.Data,
                                        Width = 0,
                                        Visible = showValueControls,
                                        CssClass = "form-control IndexStyleClassForIndexPanel EntryIndex",
                                        ID = "Value§" + strIndexId,
                                        EnableViewState = true
                                    };
                                   
                                    if (!isEnabled)
                                    {
                                        tbValue.CssClass += " readOnly";
                                        tbValue.Attributes.Add("readonly", "readonly");
                                    }
                                    else
                                    {
                                        //Se agregan los atributos necesarios para generar las validaciones
                                        tbValue = (TextBox)Validators.GetControlWithValidations(currentIndex, tbValue, mode, taskResult, ZFieldType.IndexField);
                                    }

                                    string description = string.Empty;

                                    if (!string.IsNullOrEmpty(currentIndex.Data))
                                    {
                                        description = new AutoSubstitutionBusiness().getDescription(
                                            currentIndex.Data,
                                            long.Parse(strIndexId));
                                    }

                                    TbDescription = new TextBox
                                    {
                                        Text = description,
                                        Width =
                                                                (mode == WebModuleMode.Search)
                                                                    ? lenDescription
                                                                    : CTR_INDEX_SLST_WIDTH,
                                        ToolTip = description,
                                        CssClass = "form-control IndexStyleClassForIndexPanel EntryIndex",
                                        ID = strIndexId,
                                        Visible = showValueControls
                                    };

                                    TbDescription.CssClass += " readOnly";

                                    TbDescription.Attributes.Add("readonly", "readonly");
                                    TbDescription.Style.Add("background-color", "#fff !important");

                                    if (isEnabled)
                                    {
                                        //Crea el botón que abrirá las opciones disponibles

                                        btPopup = new Button
                                        {
                                            ID = "btnOpenPopup_" + strIndexId,
                                            Enabled = true,
                                            Text = "...",
                                            CssClass = "popupButton",
                                            Visible = showValueControls,
                                            OnClientClick = "ShowLoadingAnimation(); $('#__EVENTTARGET').val('btnOpenPopup_" + strIndexId + "');"
                                        };
                                        btPopup.Click += btPopup_Click;
                                    }

                                    tcIndexValue = new TableCell();
                                    tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                    tbValue.Style.Add(HtmlTextWriterStyle.Padding, "0px");
                                    tbValue.Style.Add(HtmlTextWriterStyle.Margin, "0px");
                                    tbValue.Style.Add(HtmlTextWriterStyle.BorderWidth, "0px");
                                    tbValue.Style.Add("display", "inline-block !important");


                                    tcIndexValue.Controls.Add(tbValue);
                                    tcIndexValue.Controls.Add(TbDescription);

                                    currentRow = new TableRow();
                                    currentRow.Cells.Add(tcIndexName);

                                    if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                        currentRow.Cells.Add(tcFilterSearch);

                                    currentRow.Cells.Add(tcIndexValue);

                                    if (isEnabled)
                                    {
                                        tcCmdPopup = new TableCell();
                                        tcCmdPopup.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");

                                        tcCmdPopup.Controls.Add(btPopup);
                                        currentRow.Cells.Add(tcCmdPopup);
                                    }

                                    break;

                                case IndexAdditionalType.DropDown:
                                    cbValue = new DropDownList
                                    {
                                        Enabled = isEnabled,
                                        CssClass = "form-control EntryIndex",
                                        AutoPostBack = false,
                                        Visible = showValueControls
                                    };
                                    cbValue.Items.Clear();
                                    cbValue.Items.Add(String.Empty);

                                    foreach (object item in (sIndex.GetDropDownList((Int32)currentIndex.ID)))
                                        cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));

                                    cbValue.ToolTip = currentIndex.Data;
                                    cbValue.ID = strIndexId;
                                    cbValue.Width = (mode == WebModuleMode.Search) ? lenAttrib : CTR_INDEX_ILST_WIDTH;
                                    cbValue.EnableViewState = true;
                                    cbValue.SelectedValue = currentIndex.Data;

                                    if (currentIndex.HierarchicalChildID != null && currentIndex.HierarchicalChildID.Count > 0)
                                    {
                                        cbValue.AutoPostBack = true;
                                        cbValue.SelectedIndexChanged += cbValue_SelectedIndexChanged;
                                    }

                                    if (isEnabled)
                                    {
                                        //Se agregan los atributos necesarios para generar las validaciones
                                        cbValue = (DropDownList)Validators.GetControlWithValidations(currentIndex, cbValue, mode, taskResult, ZFieldType.IndexField);
                                    }

                                    tcIndexValue = new TableCell();
                                    tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                    tcIndexValue.Controls.Add(cbValue);

                                    currentRow = new TableRow();
                                    currentRow.Cells.Add(tcIndexName);

                                    if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                        currentRow.Cells.Add(tcFilterSearch);

                                    currentRow.Cells.Add(tcIndexValue);

                                    break;
                                case IndexAdditionalType.DropDownJerarquico:
                                    //System.Collections.Hashtable topIndexs = new Hashtable();//indices padre del indice
                                    cbValue = new DropDownList
                                    {
                                        Enabled = isEnabled,
                                        AutoPostBack = false,
                                        Visible = showValueControls
                                    };

                                    //Si el índice tiene padre
                                    if (currentIndex.HierarchicalParentID > 0)
                                    {
                                        cbValue.Items.Clear();
                                        cbValue.Items.Add(string.Empty);

                                        IList<IIndex> currentIndexs = (IList<IIndex>)MakeSearchIndexsList();
                                        IIndex parentIndex = null;
                                        foreach (IIndex indx in currentIndexs)
                                        {
                                            if (currentIndex.HierarchicalParentID == indx.ID)
                                            {
                                                parentIndex = indx;
                                                break;
                                            }
                                        }
                                        IndexsBusiness IB = new IndexsBusiness();

                                        if (parentIndex == null) parentIndex = IB.GetIndex(currentIndex.HierarchicalParentID);
                                        DataTable dt = sIndex.GetHierarchyTableByValue(currentIndex.ID,parentIndex);
                                        long max = dt.Rows.Count;
                                        for (int i = 0; i < max; i++)
                                            cbValue.Items.Add(new ListItem(dt.Rows[i]["Value"].ToString(),
                                                                           dt.Rows[i]["Value"].ToString()));
                                    }
                                    else
                                    {
                                        cbValue.Items.Clear();
                                        cbValue.Items.Add(String.Empty);

                                        foreach (object item in (IndexsBusiness.GetDropDownList((Int32)currentIndex.ID)))
                                            cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));
                                    }

                                    if (isEnabled)
                                    {
                                        //Se agregan los atributos necesarios para generar las validaciones
                                        cbValue = (DropDownList)Validators.GetControlWithValidations(currentIndex, cbValue, mode, taskResult, ZFieldType.IndexField);
                                    }

                                    cbValue.ToolTip = currentIndex.Data;
                                    cbValue.ID = currentIndex.ID.ToString();
                                    cbValue.Width = (mode == WebModuleMode.Search) ? lenAttrib : CTR_INDEX_ILST_WIDTH;
                                    cbValue.EnableViewState = true;
                                    cbValue.SelectedValue = currentIndex.Data;

                                    //cbValue.AutoPostBack = true;
                                    //cbValue.SelectedIndexChanged += cbValue_SelectedIndexChanged;

                                    tcIndexValue = new TableCell();
                                    tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                    tcIndexValue.Controls.Add(cbValue);

                                    currentRow = new TableRow();
                                    currentRow.Cells.Add(tcIndexName);

                                    if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                        currentRow.Cells.Add(tcFilterSearch);

                                    currentRow.Cells.Add(tcIndexValue);

                                    if (currentIndex.HierarchicalChildID != null && currentIndex.HierarchicalChildID.Count > 0)
                                    {
                                        sbHierarchyEvent = new StringBuilder();
                                        maxArray = currentIndex.HierarchicalChildID.Count;
                                        for (int i = 0; i < maxArray; i++)
                                        {
                                            sbHierarchyEvent.AppendFormat(HIERARCHICALFUNCTIONTEMPLATE,
                                                                                       currentIndex.HierarchicalChildID[i],
                                                                                       currentIndex.ID,
                                                                                       string.Format(PARENTVALUEACCESS,
                                                                                                     this.ClientID + '_' +
                                                                                                     currentIndex.ID),
                                                                                       "'" + this.ClientID + '_' +
                                                                                       currentIndex.HierarchicalChildID[i] +
                                                                                       "'");
                                        }
                                        cbValue.Attributes["onchange"] = sbHierarchyEvent.ToString();
                                    }

                                    break;


                                case IndexAdditionalType.LineText:
                                    if (currentIndex.Type == IndexDataType.Si_No)
                                    {
                                        cbValue = new DropDownList
                                        {
                                            Enabled = isEnabled,
                                            CssClass = "form-control EntryIndex",
                                            AutoPostBack = false,
                                            Visible = showValueControls
                                        };
                                        cbValue.Items.Clear();
                                        cbValue.Items.Add(new ListItem(String.Empty, String.Empty));
                                        cbValue.Items.Add(new ListItem("No", "0"));
                                        cbValue.Items.Add(new ListItem("Si", "1"));
                                        cbValue.ToolTip = currentIndex.Data;
                                        cbValue.ID = currentIndex.ID.ToString();
                                        cbValue.Width = (mode == WebModuleMode.Search) ? lenAttrib : CTR_INDEX_ILST_WIDTH;
                                        cbValue.EnableViewState = true;
                                        cbValue.SelectedValue = currentIndex.Data;

                                        if (currentIndex.HierarchicalChildID != null && currentIndex.HierarchicalChildID.Count > 0)
                                        {
                                            cbValue.AutoPostBack = true;
                                            cbValue.SelectedIndexChanged += cbValue_SelectedIndexChanged;
                                        }

                                        tcIndexValue = new TableCell();
                                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                        tcIndexValue.Controls.Add(cbValue);

                                        currentRow = new TableRow();
                                        currentRow.Cells.Add(tcIndexName);

                                        if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                            currentRow.Cells.Add(tcFilterSearch);

                                        currentRow.Cells.Add(tcIndexValue);

                                        break;
                                    }
                                    else
                                    {
                                        tbValue = new TextBox
                                        {
                                            Text = currentIndex.Data,
                                            Width = (mode == WebModuleMode.Search) ? lenAttrib : CTR_INDEX_TB_WIDTH,
                                            ToolTip = currentIndex.Data,
                                            CssClass = "form-control EntryIndex",
                                            ID = currentIndex.ID.ToString(),
                                            Visible = showValueControls
                                        };
                                        if (!isEnabled)
                                        {
                                            tbValue.CssClass += " readOnly";
                                            tbValue.Attributes.Add("readonly", "readonly");
                                        }
                                        else
                                        {
                                            //Se agregan los atributos necesarios para generar las validaciones
                                            tbValue = (TextBox)Validators.GetControlWithValidations(currentIndex, tbValue, mode, taskResult, ZFieldType.IndexField);
                                        }

                                        tcIndexValue = new TableCell();
                                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                        tcIndexValue.Controls.Add(tbValue);

                                        currentRow = new TableRow();
                                        currentRow.Cells.Add(tcIndexName);

                                        if (mode == WebModuleMode.Search && tcFilterSearch != null)
                                            currentRow.Cells.Add(tcFilterSearch);

                                        currentRow.Cells.Add(tcIndexValue);

                                        if (!currentIndex.Type.ToString().Contains("Alfanumerico"))
                                        {
                                            tbValue = new TextBox
                                            {
                                                Text = currentIndex.Data,
                                                Width = (mode == WebModuleMode.Search) ? lenAttrib : CTR_INDEX_TB_WIDTH,
                                                ToolTip = currentIndex.Data,
                                                CssClass = "form-control EntryIndex",
                                                ID = strIndexId + "ID2",
                                                Visible = showBetweenTextbox
                                            };

                                            tcIndexValue = new TableCell();
                                            tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                                            tcIndexValue.Visible = showBetweenTextbox;
                                            tbValue = (TextBox)Validators.GetControlWithValidations(currentIndex, tbValue, mode, taskResult, ZFieldType.IndexField);
                                            tcIndexValue.Controls.Add(tbValue);
                                            tcIndexValue.Visible = showBetweenTextbox;
                                            currentRow.Cells.Add(tcIndexValue);

                                            if (showBetweenTextbox)
                                            {
                                                ((TextBox)currentRow.Cells[2].Controls[0]).Width = 157;
                                                ((TextBox)currentRow.Cells[3].Controls[0]).Width = 157;
                                            }
                                        }

                                        break;
                                    }
                                default:
                                    break;
                            }
                        }

                        if (mode == WebModuleMode.Search || mode == WebModuleMode.Result)
                        {
                            if (tbNewIndex != null && currentRow != null) tbNewIndex.Rows.Add(currentRow);

                            upNewIndex = new UpdatePanel
                            {
                                EnableViewState = true,
                                ID = strIndexId + "UP",
                                UpdateMode = UpdatePanelUpdateMode.Conditional
                            };
                            if (tbNewIndex != null)
                            {
                                tbNewIndex.ID = strIndexId + "TB";
                                upNewIndex.ContentTemplateContainer.Controls.Add(tbNewIndex);
                            }
                            if (tcNewIndex != null) tcNewIndex.Controls.Add(upNewIndex);
                            if (trNewIndex != null) tblIndices.Rows.Add(trNewIndex);

                        }
                        else if (currentRow != null)
                            tblIndices.Rows.Add(currentRow);

                        if (string.IsNullOrEmpty(currentIndex.Operator)) continue;

                        if (cbFilters != null)
                            cbFilters.SelectedValue = currentIndex.Operator;
                    }


                    if (null != currentRow)
                    {
                        currentRow.Dispose();
                    }

                    if (null != tcIndexName)
                    {
                        tcIndexName.Dispose();
                    }
                    if (null != tcIndexValue)
                    {
                        tcIndexValue.Dispose();
                    }
                    if (null != tbValue)
                    {
                        tbValue.Dispose();
                    }
                }

                btnSaveChanges.Focus();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Gets the date format depending on the user´s culture.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// </history>
        private static string GetDateFormat()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }

        /// <summary>
        /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFilterSearch(object sender, EventArgs e)
        {
            CurrentIndexs = ParsePostBackFormToIndexs(CurrentIndexs);

            DropDownList ctrSender = (DropDownList)sender;
            TableCell currCell = (TableCell)ctrSender.Parent;
            TableRow currRow = (TableRow)currCell.Parent;

            switch (ctrSender.SelectedValue)
            {
                case "Es nulo":
                    for (int i = 2; i < currRow.Cells.Count; i++)
                    {
                        currRow.Cells[i].Visible = false;
                        for (int j = 0; j < currRow.Cells[i].Controls.Count; j++)
                        {
                            currRow.Cells[i].Controls[j].Visible = false;
                        }
                    }

                    break;
                case "Entre":

                    for (int i = 2; i < currRow.Cells.Count; i++)
                    {
                        currRow.Cells[i].Visible = true;
                        for (int j = 0; j < currRow.Cells[i].Controls.Count; j++)
                        {
                            currRow.Cells[i].Controls[j].Visible = true;
                            ((TextBox)currRow.Cells[i].Controls[j]).Width = 157;
                        }
                    }

                    break;
                //Para los demas operadores :
                default:
                    Index ind = new Index();
                    foreach (Index item in CurrentIndexs)
                    {
                        if (item.ID == long.Parse(((Label)currRow.Cells[0].Controls[0]).ID.Replace("L", "")))
                        {
                            ind = item;
                            break;
                        }
                    }
                    currRow.Cells[2].Visible = true;
                    currRow.Cells[2].Controls[0].Visible = true;
                    if (ind.DropDown == IndexAdditionalType.AutoSustitución || ind.DropDown ==
                                      IndexAdditionalType.AutoSustituciónJerarquico)
                    {
                        currRow.Cells[2].Controls[1].Visible = true;
                        currRow.Cells[3].Visible = true;
                        currRow.Cells[3].Controls[0].Visible = true;
                    }
                    else
                    {

                        switch (ind.Type)
                        {
                            case IndexDataType.Alfanumerico:
                            case IndexDataType.Alfanumerico_Largo:
                                if (ind.DropDown == IndexAdditionalType.DropDown || ind.DropDown == IndexAdditionalType.DropDownJerarquico)
                                {
                                    ((DropDownList)currRow.Cells[2].Controls[0]).Width = 320;
                                }
                                else
                                {
                                    ((TextBox)currRow.Cells[2].Controls[0]).Width = 320;
                                }
                                break;
                            case IndexDataType.Fecha:
                            case IndexDataType.Fecha_Hora:
                                ((TextBox)currRow.Cells[2].Controls[0]).Width = 150;
                                if (currRow.Cells.Count > 3)
                                {
                                    currRow.Cells[3].Visible = false;
                                    currRow.Cells[3].Controls[0].Visible = false;
                                }
                                break;
                            case IndexDataType.Moneda:
                                break;
                            case IndexDataType.Numerico:
                            case IndexDataType.Numerico_Decimales:
                            case IndexDataType.Numerico_Largo:
                                if (ind.DropDown == IndexAdditionalType.DropDown || ind.DropDown == IndexAdditionalType.DropDownJerarquico)
                                {
                                    ((DropDownList)currRow.Cells[2].Controls[0]).Width = 320;
                                }
                                else
                                {
                                    ((TextBox)currRow.Cells[2].Controls[0]).Width = 320;
                                }
                                if (currRow.Cells.Count > 3)
                                {
                                    currRow.Cells[3].Visible = false;
                                    currRow.Cells[3].Controls[0].Visible = false;
                                }

                                break;

                            default:
                                break;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
        /// </summary>
        public List<IIndex> MakeSearchIndexsList()
        {
            List<IIndex> indices = CurrentIndexs;

            try
            {
                foreach (TableRow row in tblIndices.Rows)
                {
                    foreach (Index indice in indices)
                    {
                        string indexname;
                        string indexvalue = "";
                        string indexoperator = "";
                        if (row.Cells[0].Controls[0].GetType().Name == "Label")
                        {
                            indexname = ((Label)row.Cells[0].Controls[0]).Text;
                            // indexvalue = ((TextBox)((System.Web.UI.WebControls.TableRow)(row.Cells[0].Controls[0].Parent.Parent)).Cells[2].Controls[0]).Text;
                            // indexoperator = ((DropDownList)((System.Web.UI.WebControls.TableRow)(row.Cells[0].Controls[0].Parent.Parent)).Cells[1].Controls[0]).Text;

                        }
                        else
                        {
                            indexname = ((Label)row.Cells[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[0]).Text;
                            if (row.Cells[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[2].Controls[0].GetType() == typeof(TextBox))
                            {
                                indexvalue = GetControlText(row.Cells[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[2].Controls[0]);
                            }
                            indexoperator = GetControlText(row.Cells[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[1].Controls[0]);
                        }

                        if (indice.Name != indexname) continue;
                        //string dato = indexvalue;// GetControlText(((row.Cells[0].Controls[0].Parent).Parent).Controls[2].Controls[0]);

                        indice.Operator = indexoperator;//GetControlText(((row.Cells[0].Controls[0].Parent).Parent).Controls[1].Controls[0]);
                        indice.DataTemp = indexvalue;
                        indice.Data = indexvalue;

                        if (row.Cells[0].Controls[0].GetType().Name != "Label")
                        {
                            if (row.Controls[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls.Count == 4 && row.Controls[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[3].Visible)
                            {
                                indexvalue = GetControlText(row.Cells[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[2].Controls[0]);// GetControlText(((row.Cells[0].Controls[0].Parent).Parent).Controls[3].Controls[0]);
                                indice.DataTemp2 = indexvalue;
                                indice.Data2 = indexvalue;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
            return indices;
        }


        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        /// <summary>
        /// Muestra un mensaje como resultado de la operacion guardar.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        public void SetMessage(string msg, bool ok)
        {
            if (ok)
                lblSaveMsg.ForeColor = System.Drawing.Color.DarkBlue;
            else
                lblSaveMsg.ForeColor = System.Drawing.Color.DarkRed;

            lblSaveMsg.Text = msg;
        }

        /// <summary>
        /// Obtiene el tipo de dato para completar en las validaciones de javascript
        /// </summary>
        /// <param name="IndexType"></param>
        /// <returns></returns>
        private string GetTypeToValidateFromIndex(IndexDataType IndexType)
        {
            string dataType = string.Empty;
            switch (IndexType)
            {
                case IndexDataType.Fecha:
                case IndexDataType.Fecha_Hora:
                    dataType = "date";
                    break;
                case IndexDataType.Moneda:
                case IndexDataType.Numerico_Decimales:
                    dataType = "decimal_2_16";
                    break;
                case IndexDataType.Numerico_Largo:
                case IndexDataType.Numerico:
                    dataType = "numeric";
                    break;
            }
            return dataType;
        }



        #endregion

    }
}
