using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Zamba.Services;
using Zamba.Core;
using System.Text;
using Zamba;

public partial class Views_UC_Index_ucDocTypeIndexsForInsert : System.Web.UI.UserControl
{
    public string IdButton;

    private const string HIERARCHICALFUNCTIONTEMPLATE = "GetHierarchyOptions({0},{1},{2},{3},event);";
    private const string PARENTVALUEACCESS = "$('#{0}').val()";
    private SIndex sIndex;
    private WebModuleMode currentmode;

    protected void Page_Load(object sender, EventArgs e)
    {

        sIndex = new SIndex();

        if (Page.IsPostBack)
        {
            //if (Request.Form["__EVENTTARGET"] != lnkClearIndexs.UniqueID)
            //{
            //CurrentIndexs = ParsePostBackFormToIndexs(CurrentIndexs);
            //}

            CurrentIndexs = ParsePostBackFormToIndexs(CurrentIndexs);
            ShowIndexs(CurrentIndexs, currentmode);

        }
    }

    public Table IndexsTable
    {
        get { return tblIndices; }
    }

    public Int32? DtId
    {
        private get
        {
            Int32? NullableValue;
            Int32 Value;

            if (Int32.TryParse(hdDTId.Value, out Value))
                NullableValue = Value;
            else
                NullableValue = null;

            return Value;
        }
        set
        {
            if (value.HasValue)
            {
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

    //Modificada a public la propiedad del control.
    public List<IIndex> CurrentIndexs
    {
        get
        {
            if (Session["CurrentInsertIndexs"] != null)
                return (List<IIndex>)Session["CurrentInsertIndexs"];

            return new List<IIndex>();
        }
        set
        {
            Session["CurrentInsertIndexs"] = value;
        }
    }

    //Propiedad utilizada para linkear el evento de click y la validacion
    public string SaveButtonName
    {
        get;
        set;
    }


    #region Eventos

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
        DropDownList dp;

        SIndex sIndex = new SIndex();
        DataTable dtOptions;

        if (parentIndex.HierarchicalChildID != null && parentIndex.HierarchicalChildID.Count > 0)
        {
            if (childIndex != null)
            {
                dtOptions = sIndex.GetHierarchyTableByValue(childIndex.ID, parentIndex);

                IEnumerable<Control> allControls = FlattenHierachy(Page);
                foreach (Control ct in allControls)
                {
                    if (ct.GetType() != typeof(DropDownList)) continue;
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

                if (parentIndex.HierarchicalChildID != null && childIndex.HierarchicalChildID.Count > 0)
                {
                    UpdateHierachicalIndexFromParent(childIndex, childIndex.Data);
                }
            }
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

    protected void btnClearIndexs_Click(object sender, EventArgs e)
    {
        ShowIndexs(CurrentIndexs, WebModuleMode.Insert);
    }

    #endregion

    private void LoadEsquemaIndexs(Int32 dtId)
    {

        SIndex Index = new SIndex();

        Visible = true;



        hdDTId.Value = dtId.ToString();

        DataTable dtIndexs = Index.getIndexByDocTypeId(dtId).Tables[0];

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
                ToolTip = currentIndex.ItemArray[1].ToString(),
                BorderStyle = BorderStyle.None
            };

            //            tcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

            lbValue = new TextBox { Enabled = true, Text = "", ToolTip = "" };
            lbValue.Attributes.Add("OnClick", "IndexsChanged");
            lbValue.ID = "Index" + currentIndex.ItemArray[0];
            tcIndexValue = new TableCell();
            tcIndexValue.Controls.Add(lbValue);
            tcIndexValue.BorderStyle = BorderStyle.None;

            currentRow = new TableRow();
            currentRow.Cells.Add(tcIndexName);
            currentRow.Cells.Add(tcIndexValue);
            currentRow.BorderStyle = BorderStyle.None;
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
        foreach (Zamba.Core.Index indice in CurrentIndexs)
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
    private void Clear()
    {
        tblIndices.Controls.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<IIndex> GetIndexs()
    {
        List<IIndex> indices = CurrentIndexs;
        foreach (TableRow row in tblIndices.Rows)
        {
            foreach (Zamba.Core.Index indice in indices)
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
        List<IIndex> indexToReturn = indexToParse;
        System.Collections.Specialized.NameValueCollection postBackForm = HttpContext.Current.Request.Form;
        int keysCount = postBackForm.AllKeys.Length;

        for (int i = 0; i < indexCount; i++)
        {
            for (int j = 0; j < keysCount; j++)
            {

                var SplitIndiceO = postBackForm.AllKeys[j];
                if (SplitIndiceO != null)
                {
                    var SplitIndice = SplitIndiceO.ToString().Split('$');

                    if (SplitIndice.Length == 2)
                    {
                        var indice = SplitIndice[1].ToString();

                        if (postBackForm.AllKeys[j] != null && indice == indexToReturn[i].ID.ToString() &&
                        indexToReturn[i].DropDown != IndexAdditionalType.AutoSustitución &&
                        indexToReturn[i].DropDown != IndexAdditionalType.AutoSustituciónJerarquico && indexToReturn[i].Data == "")
                        {
                            indexToReturn[i].Data = postBackForm[postBackForm.AllKeys[j]];
                            indexToReturn[i].DataTemp = postBackForm[postBackForm.AllKeys[j]];
                        }
                        else
                        {
                            if (postBackForm.AllKeys[j] != null &&
                                postBackForm.AllKeys[j].Contains("Value§" + indexToReturn[i].ID.ToString()))
                            {
                                indexToReturn[i].Data = postBackForm[postBackForm.AllKeys[j]];
                                indexToReturn[i].DataTemp = postBackForm[postBackForm.AllKeys[j]];
                            }
                            else
                                if (!Request.Url.ToString().Contains("search") &&
                                    postBackForm.AllKeys[j] != null && postBackForm.AllKeys[j].Contains(indexToReturn[i].ID.ToString()) &&
                                    (indexToReturn[i].DropDown == IndexAdditionalType.AutoSustitución ||
                                    indexToReturn[i].DropDown == IndexAdditionalType.AutoSustituciónJerarquico))
                            {
                                string[] splitedValues = postBackForm[postBackForm.AllKeys[j]].Split(new char[] { '-' });
                                indexToReturn[i].Data = indexToReturn[i].DataTemp = splitedValues[0].Trim();
                            }
                        }
                    }
                }
            }
        }

        return indexToReturn;
    }

    ArrayList autocompleteIndexsindex = new ArrayList();
    ArrayList autocompleteIndexsindexIds = new ArrayList();
    /// <summary>
    ///  Crea una grilla a partir de una 
    ///  lista de atributos
    /// </summary>    
    /// <param name="indexs">Lista de atributos</param>    
    /// <history>
    /// </history>
    public void ShowIndexs(List<IIndex> indexs, WebModuleMode mode)
    {
        currentmode = mode;

        SIndex Index = new SIndex();

        if (Page.IsPostBack || Session["ShowIndexOfInsert"] != null)
        {
            CurrentIndexs = indexs;
        }

        tblIndices.Controls.Clear();

        TableRow currentRow = null;
        TableCell tcIndexName = null;
        TableCell tcIndexValue = null;
        Label lbName;
        TextBox tbValue = null;
        DropDownList cbFilters = null;
        Table tbNewIndex = null;
        TableRow trNewIndex = null;
        TableCell tcNewIndex = null;
        UpdatePanel upNewIndex;
        TableCell tcFilterSearch = null;

        int lenind = 0;

        foreach (Index currentIndex in (Page.IsPostBack ? CurrentIndexs : indexs))
        {
            if (currentIndex.Name.Length > lenind)
                lenind = currentIndex.Name.Length;
        }

        lenind *= 6;

        //Obtengo los permisos de los indices
        long doctypeid = (long)Session["Insert_DocTypeId"];
        IUser user = (IUser)Zamba.Membership.MembershipHelper.CurrentUser;
        Hashtable IRI = new UserBusiness().GetIndexsRights(doctypeid, user.ID);
        //Variables para ver o habilitar la vista o modificacion de atributos segun permisos
        bool isIndexVisible, isIndexEnable;
        //05/07/11: se ha eliminado la posibilidad que preguntaba si es posback y usaba el objeto CurrentIndexes en el foreach,
        ////Esto se realiza porque no es necesario buclear por este objeto, si hace falta agregar: Page.IsPostBack ? indexs : CurrentIndexs
        ///

        autocompleteIndexsindex = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(doctypeid);
        autocompleteIndexsindexIds.Clear();
        if (autocompleteIndexsindex != null)
        {
            foreach (IIndex I in autocompleteIndexsindex)
            {
                autocompleteIndexsindexIds.Add(I.ID);
            }
        }
        foreach (Index currentIndex in (indexs))
        {


            trNewIndex = new TableRow();
            tcNewIndex = new TableCell();
            tcNewIndex.BorderStyle = BorderStyle.None;
            tbNewIndex = new Table();
            //1 genera celdas
            trNewIndex.Cells.Add(tcNewIndex);


            //genera label
            tcIndexName = new TableCell();
            tcIndexName.BorderStyle = BorderStyle.None;

            lbName = new Label
            {
                Text = currentIndex.Name,
                Width = lenind,
                ID = currentIndex.ID + "L"
            };

            lbName.Style.Add(HtmlTextWriterStyle.FontSize, "Small");
            tcIndexName.Controls.Add(lbName);
            tcIndexName.Width = lenind;

            tcFilterSearch = new TableCell();
            tcFilterSearch.BorderStyle = BorderStyle.None;

            if (new RightsBusiness().GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid))
            {
                //Obtengo los permisos del indice actual y los asigno a variables dedicadas
                IndexsRightsInfo IR = (IndexsRightsInfo)IRI[currentIndex.ID];
                isIndexEnable = (IR == null ? true : IR.GetIndexRightValue(RightsType.IndexEdit));
                isIndexVisible = (IR == null ? true : IR.GetIndexRightValue(RightsType.IndexView));
            }
            else
            {
                isIndexEnable = true;
                isIndexVisible = true;
            }

            currentRow = new TableRow();

            //agrega los label
            currentRow.Cells.Add(tcIndexName);

            if (tcFilterSearch != null)

                currentRow.Cells.Add(tcFilterSearch);
            tcIndexValue = GetIndexCell(currentIndex, isIndexEnable, isIndexVisible);
            currentRow.Cells.Add(tcIndexValue);
            currentRow.Visible = isIndexVisible;



            TableCell tcObligatorio = new TableCell();
            Label lblObligatorio;

            if (currentIndex.Required)
            {
                lblObligatorio = new Label
                {
                    Text = "(*)",
                    Width = 20,
                    ID = currentIndex.ID + "Required"
                };

                lblObligatorio.Style.Add(HtmlTextWriterStyle.FontSize, "Small");
                lblObligatorio.Style.Add(HtmlTextWriterStyle.Color, "Red");
                tcObligatorio.Controls.Add(lblObligatorio);
            }

            if (tbNewIndex != null)
            {
                //deshabilita o habilita las celdas segun los permisos
                trNewIndex.Visible = isIndexVisible;


                if (currentRow != null)
                {
                    currentRow.Cells.Add(tcObligatorio);

                    //agrega items
                    tbNewIndex.Rows.Add(currentRow);

                }
            }

            upNewIndex = new UpdatePanel
            {


                EnableViewState = true,
                ID = currentIndex.ID + "UP",
                UpdateMode = UpdatePanelUpdateMode.Conditional
            };

            if (tbNewIndex != null)
            {
                tbNewIndex.ID = currentIndex.ID + "TB";
                //agrega input
                upNewIndex.ContentTemplateContainer.Controls.Add(tbNewIndex);
            }

            if (tcNewIndex != null)
            {
                //genera nombre a las celdas
                tcNewIndex.Controls.Add(upNewIndex);
                tblIndices.Rows.Add(trNewIndex);
            }

            if (string.IsNullOrEmpty(currentIndex.Operator))
                continue;

            if (cbFilters != null)
            {
                cbFilters.SelectedValue = currentIndex.Operator;
            }



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

    /// <summary>
    /// Obtiene la celda conteniendo los controles necesarios para generar la UI del Índice
    /// </summary>
    /// <param name="Index">Índice a generar UI</param>
    /// <param name="IndexEnable">Indice habilitado </param>
    /// <param name="IndexVisible">Indice visible</param>
    /// <returns></returns>
    private TableCell GetIndexCell(IIndex Index, bool IndexEnable, bool IndexVisible)
    {
        try
        {
            TableCell tcToReturn = new TableCell();
            Control indexControl = null;
            Control additionalControl = null;
            // es un parche para la entidad cliente exclusiva y temporalmente, falta que felipe le haga el autocomplete (Sebastian)
            if (Index.Column == "I16")
                Index.DropDown = IndexAdditionalType.LineText;
            switch (Index.DropDown)
            {
                case IndexAdditionalType.AutoSustitución:
                case IndexAdditionalType.AutoSustituciónJerarquico:
                    indexControl = GetASustitutionIndexControl(Index, IndexEnable);
                    break;
                case IndexAdditionalType.DropDown:
                case IndexAdditionalType.DropDownJerarquico:
                    indexControl = GetDropDownIndexControl(Index, IndexEnable);
                    break;
                case IndexAdditionalType.LineText:
                    indexControl = GetLineTextIndexControl(Index, IndexEnable);
                    if (Index.Type == IndexDataType.Fecha || Index.Type == IndexDataType.Fecha_Hora)
                    {
                        additionalControl = new AjaxControlToolkit.CalendarExtender
                        {
                            Format = GetDateFormat(),
                            TargetControlID = indexControl.ID
                        };
                        ((TextBox)indexControl).ToolTip = "Haga click para insertar la fecha.";
                    }
                    break;
                case IndexAdditionalType.NoIndex:
                default:
                    break;
            }

            if (indexControl != null)
            {

                tcToReturn.Controls.Add(indexControl);
                if (additionalControl != null)
                    tcToReturn.Controls.Add(additionalControl);
            }

            return tcToReturn;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    private Control GetDropDownIndexControl(IIndex Index, bool IndexEnable)
    {
        try
        {
            Control controlToReturn;

            if (IndexEnable)
            {
                controlToReturn = new DropDownList
                {
                    AutoPostBack = false,
                    Width = 320
                };

                DropDownList cbValue = (DropDownList)controlToReturn;

                cbValue.ToolTip = Index.Data;

                ListItem[] items = GetOptions(Index);

                if (items != null)
                    cbValue.Items.AddRange(items);

                cbValue.SelectedValue = Server.HtmlEncode(Index.Data);

                if (Index.HierarchicalChildID != null && Index.HierarchicalChildID.Count > 0)
                {
                    StringBuilder sbHierarchyEvent = new StringBuilder();
                    int maxArray = Index.HierarchicalChildID.Count;
                    for (int i = 0; i < maxArray; i++)
                    {
                        sbHierarchyEvent.AppendFormat(HIERARCHICALFUNCTIONTEMPLATE,
                                                                   Index.HierarchicalChildID[i],
                                                                   Index.ID,
                                                                   string.Format(PARENTVALUEACCESS,
                                                                                 this.ClientID + '_' +
                                                                                 Index.ID),
                                                                   "'" + this.ClientID + '_' +
                                                                   Index.HierarchicalChildID[i] +
                                                                   "'");
                    }
                    cbValue.Attributes["onchange"] = sbHierarchyEvent.ToString();
                }

                //Se agregan validaciones en base a configuracion de índice
                StringBuilder sbClasses = new StringBuilder();

                sbClasses.Append(" dataType");
                cbValue.Attributes.Add("dataType", GetTypeToValidateFromIndex(Index.Type));
                if (Index.Required)
                    sbClasses.Append(" isRequired");
                sbClasses.Append(" length");
                cbValue.Attributes.Add("length", Index.Len.ToString());
                if (!string.IsNullOrEmpty(Index.DefaultValue))
                {
                    sbClasses.Append(" haveDefaultValue");
                    cbValue.Attributes.Add("DefaultValue", Index.DefaultValue);
                }
                sbClasses.Append(" insertAttribute");
                cbValue.CssClass += sbClasses.ToString();

                if (autocompleteIndexsindexIds.Contains(Index.ID))
                {
                    ((DropDownList)controlToReturn).Attributes.Add("onchange", "autocompleteIndexForInsert(this);");
                    ((DropDownList)controlToReturn).CssClass += " autocompleteIndex";
                }
            }
            else
            {
                controlToReturn = new TextBox
                {
                    Text = Index.Data,
                    CssClass = "readOnly",
                    ToolTip = "No tiene permisos para editar este índice.",
                    Width = 320
                };

                ((TextBox)controlToReturn).Attributes.Add("readOnly", "readOnly");
            }

          

            controlToReturn.ID = Index.ID.ToString();
            return controlToReturn;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    private Control GetASustitutionIndexControl(IIndex Index, bool IndexEnable)
    {
        try
        {
            Control controlToReturn;

            if (IndexEnable)
            {
                controlToReturn = new DropDownList
                {
                    AutoPostBack = false,
                    Width = 320
                };

                DropDownList cbValue = (DropDownList)controlToReturn;

                cbValue.ToolTip = Index.Data;
                ListItem[] items = GetOptions(Index);

                if (items != null)
                    cbValue.Items.AddRange(items);

                cbValue.SelectedValue = Server.HtmlEncode(Index.Data);

                //if (Index.HierarchicalChildID.Count > 0)
                //    cbValue.Attributes["onchange"] = string.Format(HIERARCHICALFUNCTIONTEMPLATE, Index.HierarchicalChildID,
                //        Index.ID, string.Format(PARENTVALUEACCESS, this.ClientID + '_' + Index.ID),
                //        "'" + this.ClientID + '_' + Index.HierarchicalChildID + "'");

                if (Index.HierarchicalChildID != null && Index.HierarchicalChildID.Count > 0)
                {
                    StringBuilder sbHierarchyEvent = new StringBuilder();
                    int maxArray = Index.HierarchicalChildID.Count;
                    for (int i = 0; i < maxArray; i++)
                    {
                        sbHierarchyEvent.AppendFormat(HIERARCHICALFUNCTIONTEMPLATE,
                                                                   Index.HierarchicalChildID[i],
                                                                   Index.ID,
                                                                   string.Format(PARENTVALUEACCESS,
                                                                                 this.ClientID + '_' +
                                                                                 Index.ID),
                                                                   "'" + this.ClientID + '_' +
                                                                   Index.HierarchicalChildID[i] +
                                                                   "'");
                    }

                    cbValue.Attributes["onchange"] = sbHierarchyEvent.ToString();
                }

                //Se agregan validaciones en base a configuracion de índice
                StringBuilder sbClasses = new StringBuilder();

                sbClasses.Append(" dataType");
                cbValue.Attributes.Add("dataType", GetTypeToValidateFromIndex(Index.Type));
                if (Index.Required)
                    sbClasses.Append(" isRequired");
                sbClasses.Append(" length");
                cbValue.Attributes.Add("length", Index.Len.ToString());
                if (!string.IsNullOrEmpty(Index.DefaultValue))
                {
                    sbClasses.Append(" haveDefaultValue");
                    cbValue.Attributes.Add("DefaultValue", Index.DefaultValue);
                }
                sbClasses.Append(" insertAttribute");
                cbValue.CssClass += sbClasses.ToString();

                if (autocompleteIndexsindexIds.Contains(Index.ID))
                {
                    ((DropDownList)controlToReturn).Attributes.Add("onchange", "autocompleteIndexForInsert(this);");
                    ((DropDownList)controlToReturn).CssClass += " autocompleteIndex";
                }
            }
            else
            {
                string textOfControl = string.Empty;
                if (!string.IsNullOrEmpty(Index.Data) && !string.IsNullOrEmpty(Index.dataDescription))
                {
                    textOfControl = Index.Data + " - " + Index.dataDescription;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Index.Data))
                    {

                        string textDescription = new AutoSubstitutionBusiness().getDescription(Index.Data, Index.ID);
                        textOfControl = Index.Data + " - " + textDescription;
                    }
                }

                controlToReturn = new TextBox
                {
                    Text = textOfControl,
                    CssClass = "readOnly",
                    ToolTip = "No tiene permisos para editar este índice.",
                    Width = 320
                };

                ((TextBox)controlToReturn).Attributes.Add("readOnly", "readOnly");
            }

           

            controlToReturn.ID = Index.ID.ToString();
            return controlToReturn;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    private Control GetLineTextIndexControl(IIndex Index, bool IndexEnable)
    {
        try
        {
            TextBox tbValue = new TextBox
            {
                Text = Server.HtmlEncode(Index.Data),
                Width = 320,
                ID = Index.ID.ToString()
            };

            if (IndexEnable)
            {
                //Se agregan validaciones en base a configuracion de índice
                StringBuilder sbClasses = new StringBuilder();
                if (Index.ID == 16)
                    sbClasses.Append(" generatelistdinamic");
                sbClasses.Append(" dataType");
                tbValue.Attributes.Add("dataType", GetTypeToValidateFromIndex(Index.Type));
                if (Index.Required)
                    sbClasses.Append(" isRequired");
                sbClasses.Append(" length");
                tbValue.Attributes.Add("length", Index.Len.ToString());
                if (!string.IsNullOrEmpty(Index.DefaultValue))
                {
                    sbClasses.Append(" haveDefaultValue");
                    tbValue.Attributes.Add("DefaultValue", Index.DefaultValue);
                }

                if (!string.IsNullOrEmpty(Index.MinValue))
                {
                    sbClasses.Append(" haveMinValue");
                    tbValue.Attributes.Add("ZMinValue", Index.MinValue);
                }

                if (!string.IsNullOrEmpty(Index.MaxValue))
                {
                    sbClasses.Append(" haveMaxValue");
                    tbValue.Attributes.Add("ZMaxValue", Index.MaxValue);
                }

                sbClasses.Append(" insertAttribute");
                tbValue.CssClass += sbClasses.ToString();
            }
            else
            {
                tbValue.Attributes.Add("readOnly", "readOnly");
                tbValue.CssClass = " readOnly";
            }

            if (Index.Type == IndexDataType.Alfanumerico || Index.Type == IndexDataType.Alfanumerico_Largo)
            {
                tbValue.TextMode = TextBoxMode.MultiLine;
                tbValue.CssClass += (IndexEnable) ? " overflowHidden" : " overflowHidden readOnly";
                tbValue.Wrap = true;
                tbValue.Rows = 1;
                tbValue.Attributes.Add("ondblclick", "modifyTextBoxSize(this);");
            }

            if (currentmode == WebModuleMode.Rule)
            {
                tbValue.CssClass += " RuleField";
            }

            if (autocompleteIndexsindexIds.Contains(Index.ID))
            {
                tbValue.Attributes.Add("onchange", "autocompleteIndexForInsert(this);");
                tbValue.CssClass += " autocompleteIndex";
            }
            return tbValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    private ListItem[] GetOptions(IIndex Index)
    {
        try
        {
            ListItem[] items = null;
            switch (Index.DropDown)
            {
                case IndexAdditionalType.AutoSustitución:
                case IndexAdditionalType.AutoSustituciónJerarquico:
                    DataTable resutlsTable;
                    int maxRow;
                    if (Index.HierarchicalParentID > 0)
                    {
                        IIndex parentIndex = null;
                        foreach (IIndex indx in CurrentIndexs)
                        {
                            if (Index.HierarchicalParentID == indx.ID)
                            {
                                parentIndex = indx;
                                break;
                            }
                        }
                        IndexsBusiness IB = new IndexsBusiness();

                        if (parentIndex == null) parentIndex = IB.GetIndex(Index.HierarchicalParentID);

                        resutlsTable = new SIndex().GetHierarchyTableByValue(Index.ID, parentIndex);
                        maxRow = resutlsTable.Rows.Count;
                        items = new ListItem[maxRow];

                        for (int i = 0; i < maxRow; i++)
                        {
                            items[i] = new ListItem(resutlsTable.Rows[i]["Description"].ToString(), resutlsTable.Rows[i]["Value"].ToString());
                        }
                    }
                    else
                    {

                        resutlsTable = new SIndex().GetIndexData((Int32)Index.ID, false);
                        maxRow = resutlsTable.Rows.Count;
                        items = new ListItem[maxRow + 1];
                        items[0] = new ListItem(string.Empty);

                        for (int i = 1; i < maxRow + 1; i++)
                        {
                            items[i] = new ListItem(resutlsTable.Rows[i - 1][1].ToString(),
                                resutlsTable.Rows[i - 1][0].ToString());
                        }
                    }
                    break;
                case IndexAdditionalType.DropDown:
                case IndexAdditionalType.DropDownJerarquico:
                    if (Index.HierarchicalParentID > 0)
                    {
                        IIndex parentIndex = null;
                        foreach (IIndex indx in CurrentIndexs)
                        {
                            if (Index.HierarchicalParentID == indx.ID)
                            {
                                parentIndex = indx;
                                break;
                            }
                        }
                        IndexsBusiness IB = new IndexsBusiness();

                        if (parentIndex == null) parentIndex = IB.GetIndex(Index.HierarchicalParentID);
                        DataTable resutlsTableSearch = new SIndex().GetHierarchyTableByValue(Index.ID, parentIndex);
                        int maxRowSearch = resutlsTableSearch.Rows.Count;
                        items = new ListItem[maxRowSearch];

                        for (int i = 0; i < maxRowSearch; i++)
                        {
                            items[i] = new ListItem(resutlsTableSearch.Rows[i]["Value"].ToString(),
                                resutlsTableSearch.Rows[i]["Value"].ToString());
                        }
                    }
                    else
                    {

                        List<String> values = new SIndex().GetDropDownList((int)Index.ID);
                        int max = values.Count;
                        items = new ListItem[max + 1];
                        items[0] = new ListItem(string.Empty);

                        for (int i = 1; i < max + 1; i++)
                        {
                            items[i] = (new ListItem(values[i - 1].ToString(), values[i - 1].ToString()));
                            //items[i] = (new ListItem(Server.HtmlEncode(values[i - 1].ToString()), Server.HtmlEncode(values[i - 1].ToString())));
                        }
                    }
                    break;
            }
            return items;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
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
    public List<IIndex> MakeSearchIndexsList()
    {
        List<IIndex> indices = CurrentIndexs;

        try
        {
            foreach (TableRow row in tblIndices.Rows)
            {
                foreach (Index indice in indices)
                {
                    if (indice.Name !=
                        ((Label)
                         ((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0]
                             .Cells[0].Controls[0]).Text) continue;

                    string dato;
                    ControlCollection containerToTry = (((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[2].Controls);

                    if (containerToTry.Count > 0)
                    {
                        dato = GetControlText(containerToTry[0]);
                    }
                    else
                        dato = GetControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[1].Controls[0]);

                    //indice.Operator = GetControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[1].Controls[0]);
                    indice.DataTemp = dato;
                    indice.Data = dato;

                    //if (((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells.Count == 4 && ((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[3].Visible)
                    //{
                    //    dato = GetControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[3].Controls[0]);
                    //    indice.DataTemp2 = dato;
                    //    indice.Data2 = dato;
                    //}
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

    /// <summary>
    /// Obtiene el tipo de dato para completar en las validaciones de javascript
    /// </summary>
    /// <param name="IndexType"></param>
    /// <returns></returns>
    private string GetTypeToValidateFromIndex(IndexDataType IndexType)
    {
        switch (IndexType)
        {
            case IndexDataType.Fecha:
            case IndexDataType.Fecha_Hora:
                return "date";
            case IndexDataType.Moneda:
            case IndexDataType.Numerico_Decimales:
                return "decimal_2_16";
            case IndexDataType.Numerico_Largo:
            case IndexDataType.Numerico:
                return "numeric";
            case IndexDataType.Alfanumerico:
            case IndexDataType.Alfanumerico_Largo:
            case IndexDataType.None:
            case IndexDataType.Si_No:
            default:
                return string.Empty;
        }
    }
}
