using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.WF.WF;
using Zamba;

public partial class UC_WF_Rules_UCDoRequestData : UserControl, IRule
{
    private const string HIERARCHICALFUNCTIONTEMPLATE = "GetHierarchyOptions({0},{1},{2},{3});";
    private const string PARENTVALUEACCESS = "$('#{0}').val()";
    private List<ITaskResult> _results;

    private Int64 _taskID;
    private bool nonVisibleTaskWithGuiRules;
    private IUser user;

    public Index[] CurrentIndexs
    {
        get
        {
            if (Session[_taskID + "DoRequestDataIndexs"] != null)
                return (Index[])Session[_taskID+"DoRequestDataIndexs"];

            return new Index[0];
        }
        set { Session[_taskID+"DoRequestDataIndexs"] = value; }
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

    #region IRule Members

    public event WFExecution._continueExecution ContinueExecution;

    public RuleExecutionResult ExecutionResult
    {
        get { return (RuleExecutionResult)Session[_taskID + "ExecutionResult"]; }
        set { Session[_taskID + "ExecutionResult"] = value; }
    }

    public RulePendingEvents PendigEvent
    {
        get { return (RulePendingEvents)Session[_taskID + "PendigEvent"]; }
        set { Session[_taskID + "PendigEvent"] = value; }
    }

    public List<long> ExecutedIDs
    {
        get { return (List<long>)Session[_taskID + "ExecutedIDs"]; }
        set { Session[_taskID + "ExecutedIDs"] = value; }
    }

    public List<long> PendingChildRules
    {
        get { return (List<long>)Session[_taskID + "PendingChildRules"]; }
        set { Session[_taskID + "PendingChildRules"] = value; }
    }

    public Hashtable Params
    {
        get { return (Hashtable)Session[_taskID + "Params"]; }
        set { Session[_taskID + "Params"] = value; }
    }

    public long RuleID
    {
        get { return (long)Session[_taskID + "RuleID"]; }
        set { Session[_taskID + "RuleID"] = value; }
    }

    public string Path
    {
        get { return (string)Session[_taskID + "Path"]; }
        set { Session[_taskID + "Path"] = value; }
    }

    public List<ITaskResult> results
    {
        get { return (List<ITaskResult>)Session[_taskID + "results"]; }
        set { Session[_taskID + "results"] = value; }
    }

    public long TaskID
    {
        get { return _taskID; }
        set
        {
            _taskID = value;
            _results = results;
        }
    }

    /// <summary>
    /// Define si se encuentra ejecutando reglas con GUI para tareas cerradas (no visibles).
    /// </summary>
    public bool NonVisibleTaskWithGuiRules
    {
        get { return nonVisibleTaskWithGuiRules; }
        set { nonVisibleTaskWithGuiRules = value; }
    }

    /// <summary>
    /// Limpia el estado de la ejecución dependiendo de si es una ejecución normal
    /// o si es una ejecución de reglas con interfaz gráfica con la tarea cerrada.
    /// </summary>
    public void ClearCurrentExecutionSession()
    {
        if (nonVisibleTaskWithGuiRules)
            Session["EntryRulesExecution"] = null;
        else
            Session[TaskID + "CurrentExecution"] = null;
    }

    public void _btnOk_Click(object sender, EventArgs e)
    {
        //Obtengo los indices modificas desde la dorequestdata
        List<Index> indlst = GetModifiedIndexs();
        Params["indexlist"] = indlst;
        ClearCurrentExecutionSession();
        Boolean RefreshRule = false;
        CurrentIndexs = null;

        ContinueExecution(RuleID, ref _results, PendigEvent, ExecutionResult,
                          ExecutedIDs, Params, PendingChildRules, ref RefreshRule,
                          (List<long>)Session["TaskIdsToRefresh"]);
    }

    public void _btnCancel_Click(object sender, EventArgs e)
    {
        ClearCurrentExecutionSession();
        Params.Add("CancelRule", true);
        Boolean RefreshRule = false;
        CurrentIndexs = null;
        if (nonVisibleTaskWithGuiRules)
            PendigEvent = RulePendingEvents.CancelExecution;

        ContinueExecution(RuleID, ref _results, PendigEvent, ExecutionResult,
                          ExecutedIDs, Params, PendingChildRules, ref RefreshRule,
                          (List<long>)Session["TaskIdsToRefresh"]);
    }

    public void LoadOptions()
    {
        //Muestro los indices en el UC.
        var indexlist = (List<Index>)(Params["indexlist"]);
        ShowIndexs(indexlist.ToArray<Index>());
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        user = (IUser)Session["User"];
    }

    /// <summary>
    ///  Crea una grilla a partir de una 
    ///  lista de atributos
    /// </summary>    
    /// <param name="indexs">Lista de atributos</param>    
    /// <history>
    /// </history>
    /// 
    public void ShowIndexs(Index[] indexs)
    {
        var Index = new SIndex();

        long UserId = long.Parse(Session["UserId"].ToString());

        //if (Page.IsPostBack || Session["ShowIndexOfInsert"] != null)
            CurrentIndexs = indexs;

        tblIndices.Controls.Clear();

        TableRow currentRow = null;
        TableCell tcIndexName = null;
        TableCell tcIndexValue = null;
        //TableCell tcCmdPopup;
        Label lbName;
        TextBox tbValue = null;
        //TextBox TbDescription;
        //Button btPopup;
        DropDownList cbValue;
        DropDownList cbFilters = null;
        //Table tbNewIndex = null;
        TableRow trNewIndex = null;
        TableCell tcNewIndex = null;

        StringBuilder sbHierarchyEvent;
        int maxArray;
        //UpdatePanel upNewIndex;
        //TableCell tcFilterSearch = null;

        var AditionalScript = new StringBuilder("$(document).ready(function(){");
        string containerID = ClientID;

        int lenind = 0;
        int indexCount = 0;

        foreach (Index currentIndex in indexs)
        {
            if (currentIndex.Name.Length > lenind)
                lenind = currentIndex.Name.Length;
        }

        lenind *= 6;

        //Obtengo los permisos de los indices
        long doctypeid = Int64.Parse(Params["DocTypeId"].ToString());
        Hashtable IRI = UserBusiness.Rights.GetIndexsRights(doctypeid, UserId, true, true);
        //Variables para ver o habilitar la vista o modificacion de atributos segun permisos
        bool isIndexVisible, isIndexEnable;
        
        
        
        foreach (Index currentIndex in indexs)
        {
            trNewIndex = new TableRow();
            tcNewIndex = new TableCell();
            //tbNewIndex = new Table();
            trNewIndex.Cells.Add(tcNewIndex);

            tcIndexName = new TableCell();
            lbName = new Label
            {
                Text = currentIndex.Name,
                Width = lenind,
                ID = currentIndex.ID + "L"
            };

            lbName.Style.Add(HtmlTextWriterStyle.FontSize, "X-Small");
            tcIndexName.Controls.Add(lbName);
            tcIndexName.Style.Add(HtmlTextWriterStyle.PaddingRight, "3 pt");

            //tcFilterSearch = new TableCell();

            if (UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid))
            {
                //Obtengo los permisos del indice actual y los asigno a variables dedicadas
                var IR = (IndexsRightsInfo)IRI[currentIndex.ID];
                isIndexEnable = (IR == null ? true : IR.GetIndexRightValue(RightsType.IndexEdit));
                isIndexVisible = (IR == null ? true : IR.GetIndexRightValue(RightsType.IndexView));
            }
            else
            {
                isIndexEnable = true;
                isIndexVisible = true;
            }

            if (currentIndex.Type == IndexDataType.Fecha || currentIndex.Type == IndexDataType.Fecha_Hora)
            {
                tbValue = new TextBox
                {
                    Text = currentIndex.Data,
                    Width = 400,
                    ToolTip =
                        (isIndexEnable ? currentIndex.Data : "No tiene permisos para editar este índice."),
                    ID = currentIndex.ID.ToString(),
                    //Habilita o muestra el control segun permisos
                    Enabled = isIndexEnable,
                    BackColor =
                        (isIndexEnable ? Color.FromArgb(248, 248, 248) : Color.FromArgb(204, 204, 204))
                };

                if (!isIndexEnable)
                {
                    tbValue.CssClass += " readOnly";
                    tbValue.Attributes.Add("readonly", "readonly");
                }
                else
                {
                    //Se agregan los atributos necesarios para generar las validaciones
                    if (this._results != null && this._results.Count > 0 && this._results[0] != null)
                    {
                        tbValue = (TextBox)Helpers.Validators.GetControlWithValidations(currentIndex, tbValue, WebModuleMode.Result, this._results[0], ZFieldType.RuleField);
                    }
                    else
                    {
                        STasks service = new STasks();
                        tbValue = (TextBox)Helpers.Validators.GetControlWithValidations(currentIndex, tbValue, WebModuleMode.Result, service.GetTask(TaskID), ZFieldType.RuleField);
                        service = null;
                    }
                }
                

                //Declaramos un datepicker de jquery.
                AditionalScript.Append("AddCalendar('" + containerID + '_' + tbValue.ID + "');");
                //Lo ocultamos, ya que al mostrar el dialogo se ve mal.
                AditionalScript.Append("setTimeout(\"$('#ui-datepicker-div').css('top',-1000);$('#" + containerID + '_' +
                                       tbValue.ID + "').datepicker('hide');\",1);");

                tcIndexValue = new TableCell();
                tcIndexValue.Controls.Add(tbValue);
                tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                currentRow = new TableRow();
                currentRow.Cells.Add(tcIndexName);

                //if (tcFilterSearch != null)
                //    currentRow.Cells.Add(tcFilterSearch);

                currentRow.Cells.Add(tcIndexValue);
                currentRow.Visible = isIndexVisible;

                if (!currentIndex.Type.ToString().Contains("Alfanumerico"))
                {
                    tbValue = new TextBox
                    {
                        Text = currentIndex.Data,
                        Width = 400,
                        ToolTip =
                            (isIndexEnable
                                 ? currentIndex.Data
                                 : "No tiene permisos para editar este índice."),
                        ID = currentIndex.ID + "ID2",
                        //Habilita o muestra el control segun permisos
                        Enabled = isIndexEnable,
                        BackColor =
                            (isIndexEnable ? Color.FromArgb(248, 248, 248) : Color.FromArgb(204, 204, 204))
                    };

                    tcIndexValue = new TableCell { Visible = false };
                    tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                    tcIndexValue.Controls.Add(tbValue);
                    currentRow.Cells.Add(tcIndexValue);
                    currentRow.Visible = isIndexVisible;
                }
            }
            else
            {
                switch (currentIndex.DropDown)
                {
                    case IndexAdditionalType.AutoSustitución:
                        cbValue = new DropDownList
                        {
                            AutoPostBack = false,
                            //Habilita o muestra el control segun permisos
                            Enabled = isIndexEnable,
                            BackColor =
                                (isIndexEnable
                                     ? Color.FromArgb(248, 248, 248)
                                     : Color.FromArgb(204, 204, 204))
                        };
                        cbValue.Items.Add("");

                        foreach (DataRow row in Index.GetIndexData((Int32)currentIndex.ID, false).Rows)
                        {
                            cbValue.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                        }

                        cbValue.ToolTip = (isIndexEnable
                                               ? currentIndex.Data
                                               : "No tiene permisos para editar este índice.");
                        cbValue.ID = currentIndex.ID.ToString();
                        cbValue.Width = 400;


                        cbValue.EnableViewState = true;

                        cbValue.SelectedValue = currentIndex.Data;

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

                        tcIndexValue = new TableCell();
                        tcIndexValue.Controls.Add(cbValue);
                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                        currentRow = new TableRow();
                        currentRow.Cells.Add(tcIndexName);

                        currentRow.Cells.Add(tcIndexValue);
                        currentRow.Visible = isIndexVisible;

                        break;

                    case IndexAdditionalType.AutoSustituciónJerarquico:

                        cbValue = new DropDownList
                        {
                            AutoPostBack = false,
                            //Habilita o muestra el control segun permisos
                            Enabled = isIndexEnable,
                            BackColor =
                                (isIndexEnable
                                     ? Color.FromArgb(248, 248, 248)
                                     : Color.FromArgb(204, 204, 204))
                        };

                        cbValue.Items.Clear();

                        DataTable resutlsTable;
                        int maxRow;
                        if (currentIndex.HierarchicalParentID > 0)
                        {
                            resutlsTable = Index.GetHierarchyTableByValue(currentIndex.ID,
                                                                          currentIndex.HierarchicalParentID,
                                                                          SearchIndexData(currentIndex.HierarchicalParentID, indexs));
                            maxRow = resutlsTable.Rows.Count;

                            for (int i = 0; i < maxRow; i++)
                            {
                                cbValue.Items.Add(new ListItem(resutlsTable.Rows[i]["Description"].ToString(),
                                                               resutlsTable.Rows[i]["Value"].ToString()));
                            }
                        }
                        else
                        {
                            cbValue.Items.Add("");

                            resutlsTable = Index.GetIndexData((Int32)currentIndex.ID, false);
                            maxRow = resutlsTable.Rows.Count;

                            for (int i = 0; i < maxRow; i++)
                            {
                                cbValue.Items.Add(new ListItem(resutlsTable.Rows[i][1].ToString(),
                                                               resutlsTable.Rows[i][0].ToString()));
                            }
                        }

                        cbValue.ToolTip = (isIndexEnable
                                               ? currentIndex.Data
                                               : "No tiene permisos para editar este índice.");
                        cbValue.ID = currentIndex.ID.ToString();
                        cbValue.Width = 400;
                        cbValue.EnableViewState = true;

                        cbValue.SelectedValue = currentIndex.Data;

                        tcIndexValue = new TableCell();
                        tcIndexValue.Controls.Add(cbValue);
                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                        currentRow = new TableRow();
                        currentRow.Cells.Add(tcIndexName);

                        currentRow.Cells.Add(tcIndexValue);
                        currentRow.Visible = isIndexVisible;


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

                        //cbValue.Items.Add("");

                        break;

                    case IndexAdditionalType.DropDown:
                        cbValue = new DropDownList { Enabled = true, CssClass = "EntryIndex", AutoPostBack = false };
                        cbValue.Items.Clear();
                        cbValue.Items.Add(String.Empty);

                        foreach (object item in (Index.GetDropDownList((Int32)currentIndex.ID)))
                        {
                            cbValue.Items.Add(new ListItem(Server.HtmlEncode(item.ToString()),
                                                           Server.HtmlEncode(item.ToString())));
                        }

                        cbValue.ToolTip = currentIndex.Data;
                        cbValue.ID = currentIndex.ID.ToString();
                        cbValue.Width = 400;
                        cbValue.EnableViewState = true;

                        cbValue.SelectedValue = Server.HtmlEncode(currentIndex.Data);

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

                        tcIndexValue = new TableCell();
                        tcIndexValue.Controls.Add(cbValue);
                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                        currentRow = new TableRow();
                        currentRow.Cells.Add(tcIndexName);

                        //if (Request.Url.ToString().ToLower().Contains("search.aspx"))
                        //    if (tcFilterSearch != null)
                        //        currentRow.Cells.Add(tcFilterSearch);

                        currentRow.Cells.Add(tcIndexValue);

                        break;
                    case IndexAdditionalType.DropDownJerarquico:
                        cbValue = new DropDownList
                        {
                            AutoPostBack = false,
                            //Habilita o muestra el control segun permisos
                            Enabled = isIndexEnable,
                            BackColor =
                                (isIndexEnable
                                     ? Color.FromArgb(248, 248, 248)
                                     : Color.FromArgb(204, 204, 204))
                        };

                        cbValue.Items.Clear();

                        DataTable resutlsTableSearch;
                        int maxRowSearch;

                        if (currentIndex.HierarchicalParentID > 0)
                        {
                            resutlsTableSearch = Index.GetHierarchyTableByValue(currentIndex.ID,
                                                                                currentIndex.HierarchicalParentID,
                                                                                SearchIndexData(currentIndex.HierarchicalParentID, indexs));
                            maxRowSearch = resutlsTableSearch.Rows.Count;

                            for (int i = 0; i < maxRowSearch; i++)
                            {
                                cbValue.Items.Add(new ListItem(resutlsTableSearch.Rows[i]["Value"].ToString(),
                                                               resutlsTableSearch.Rows[i]["Value"].ToString()));
                            }
                        }
                        else
                        {
                            cbValue.Items.Clear();
                            cbValue.Items.Add("");

                            foreach (object item in (IndexsBusiness.GetDropDownList((Int32)currentIndex.ID)))
                            {
                                cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));
                            }
                        }

                        cbValue.ToolTip = currentIndex.Data;
                        cbValue.ID = currentIndex.ID.ToString();
                        cbValue.Width = 400;
                        cbValue.EnableViewState = true;

                        cbValue.SelectedValue = currentIndex.Data;


                        tcIndexValue = new TableCell();
                        tcIndexValue.Controls.Add(cbValue);
                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                        currentRow = new TableRow();
                        currentRow.Cells.Add(tcIndexName);
                        currentRow.Cells.Add(tcIndexValue);

                        if (currentIndex.HierarchicalChildID!= null && currentIndex.HierarchicalChildID.Count > 0)
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
                        tbValue = new TextBox
                        {
                            Text = currentIndex.Data,
                            Width = 400,
                            ToolTip =
                                (isIndexEnable
                                     ? currentIndex.Data
                                     : "No tiene permisos para editar este índice."),
                            ID = currentIndex.ID.ToString(),
                            //Habilita o muestra el control segun permisos
                            Enabled = isIndexEnable,
                            BackColor =
                                (isIndexEnable
                                     ? Color.FromArgb(248, 248, 248)
                                     : Color.FromArgb(204, 204, 204))
                        };

                        if (currentIndex.Type == IndexDataType.Alfanumerico ||
                            currentIndex.Type == IndexDataType.Alfanumerico_Largo)
                        {
                            tbValue.TextMode = TextBoxMode.MultiLine;
                            tbValue.CssClass = "overflowHidden";
                            tbValue.Wrap = true;
                            tbValue.Rows = 1;
                            tbValue.Attributes.Add("ondblclick", "modifyTextBoxSize(this)");
                        }

                        if (currentIndex.Type == IndexDataType.Si_No)
                        {
                            cbValue = new DropDownList
                            {
                                Width = 400,
                                CssClass = "EntryIndex",
                                AutoPostBack = false,

                            };
                            cbValue.Items.Add(new ListItem(String.Empty, String.Empty));
                            cbValue.Items.Add(new ListItem("No", "0"));
                            cbValue.Items.Add(new ListItem("Si", "1"));
                            cbValue.ToolTip = currentIndex.Data;
                            cbValue.ID = currentIndex.ID.ToString();
                            cbValue.EnableViewState = true;
                            cbValue.SelectedValue = String.Empty;

                            tcIndexValue = new TableCell();
                            tcIndexValue.Controls.Add(cbValue);
                            tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                            currentRow = new TableRow();
                            currentRow.Cells.Add(tcIndexName);

                            currentRow.Cells.Add(tcIndexValue);
                            currentRow.Visible = isIndexVisible;
                            break;
                        }

                        if (string.IsNullOrEmpty(tbValue.Text))
                        {
                            if (currentIndex.Type == IndexDataType.Numerico ||
                                currentIndex.Type == IndexDataType.Numerico_Decimales ||
                                currentIndex.Type == IndexDataType.Numerico_Largo ||
                                currentIndex.Type == IndexDataType.Moneda)
                            {
                                tbValue.Text = "0";
                            }
                        }

                        if (!isIndexEnable)
                        {
                            tbValue.CssClass += " readOnly";
                            tbValue.Attributes.Add("readonly", "readonly");
                        }
                        else
                        {
                            //Se agregan los atributos necesarios para generar las validaciones
                            if (this._results != null && this._results.Count > 0 && this._results[0] != null)
                            {
                                tbValue = (TextBox)Helpers.Validators.GetControlWithValidations(currentIndex, tbValue, WebModuleMode.Result, this._results[0], ZFieldType.RuleField);
                            }
                            else
                            {
                                STasks service = new STasks();
                                tbValue = (TextBox)Helpers.Validators.GetControlWithValidations(currentIndex, tbValue, WebModuleMode.Result, service.GetTask(TaskID), ZFieldType.RuleField);
                                service = null;
                            }
                            
                                                      
                        }

                        tcIndexValue = new TableCell();
                        tcIndexValue.Controls.Add(tbValue);
                        tcIndexValue.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3 pt");
                       
                        currentRow = new TableRow();
                        currentRow.Cells.Add(tcIndexName);

                        //if (tcFilterSearch != null)
                        //    currentRow.Cells.Add(tcFilterSearch);

                        currentRow.Cells.Add(tcIndexValue);
                        currentRow.Visible = isIndexVisible;

                        break;
                    default:
                        break;
                }
            }

            var tcObligatorio = new TableCell();
            Label lblObligatorio;

            if (currentIndex.Required)
            {
                lblObligatorio = new Label
                {
                    Text = "(*)",
                    Width = 20,
                    ID = currentIndex.ID + "Required"
                };

                lblObligatorio.Style.Add(HtmlTextWriterStyle.FontSize, "X-Small");
                lblObligatorio.Style.Add(HtmlTextWriterStyle.Color, "Red");
                tcObligatorio.Controls.Add(lblObligatorio);
            }

            //if (tbNewIndex != null)
            //{
            if (currentRow != null)
            {
                currentRow.Cells.Add(tcObligatorio);
                tblIndices.Rows.Add(currentRow);
                indexCount += 1;
            }
            //}

            //upNewIndex = new UpdatePanel
            //{
            //    EnableViewState = true,
            //    ID = currentIndex.ID + "UP",
            //    UpdateMode = UpdatePanelUpdateMode.Conditional
            //};

            //if (tbNewIndex != null)
            //{
            //    tbNewIndex.ID = currentIndex.ID + "TB";
            //    //upNewIndex.ContentTemplateContainer.Controls.Add(tbNewIndex);
            //}

            //if (tcNewIndex != null)
            //{
            //    tcNewIndex.Controls.Add(tbNewIndex);
            //    tblIndices.Rows.Add(trNewIndex);
            //}

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

        DoRequestDataIndexes.Height = (indexCount * 25 > 350) ? 350 : indexCount * 25;

        AditionalScript.Append("});");
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AdditionalScripting", AditionalScript.ToString(), true);
    }

    private string SearchIndexData(int indexId, Index[] indexs)
    {
        IIndex gettedInd = indexs.Where(ind => ind.ID == indexId).SingleOrDefault();
        if (gettedInd != null)
            return gettedInd.Data;
        else
            return string.Empty;
    }

    /// <summary>
    /// Gets the date format depending on the user´s culture.
    /// </summary>
    /// <returns></returns>
    /// <history>
    /// </history>
    private static string GetDateFormat()
    {
        return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
    }

    /// <summary>
    /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
    /// </summary>
    public Index[] MakeSearchIndexsList()
    {
        Index[] indices = CurrentIndexs;

        try
        {
            foreach (TableRow row in tblIndices.Rows)
            {
                foreach (Index indice in indices)
                {
                    if (indice.Name != ((Label)row.Cells[0].Controls[0]).Text) continue;

                    string dato = GetControlText(row.Cells[1].Controls[0]);

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
            ZException.Log(ex);
        }
        return indices;
    }

    /// <summary>
    /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
    /// </summary>
    public List<Index> GetModifiedIndexs()
    {
        var indices = new List<Index>();
        try
        {
            foreach (TableRow row in tblIndices.Rows)
            {
                foreach (Index indice in CurrentIndexs)
                {
                    if (indice.Name != ((Label)row.Cells[0].Controls[0]).Text) continue;

                    string dato = GetControlText(row.Cells[1].Controls[0]);

                    if (indice.Data != dato)
                    {
                        indice.DataTemp = dato;
                        indice.Data = dato;
                        indices.Add(indice);
                    }

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
        return indices;
    }

    private static IEnumerable<Control> FlattenHierachy(Control root)
    {
        var list = new List<Control> { root };
        if (root.HasControls())
        {
            foreach (Control control in root.Controls)
            {
                list.AddRange(FlattenHierachy(control));
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// Retorna el valor de la propiedad text del control.
    /// Si el control no tiene la propiedad text retorna vacio.
    /// </summary>
    /// <param name="ctr">Control</param>
    /// <returns>valor de la propiedad text</returns>
    private static string GetControlText(Control ctr)
    {
        //string dato = string.Empty;

        //if (ctr is DropDownList)
        //    dato = ((DropDownList)ctr).Text;
        //else if (ctr is TextBox)
        //    dato = ((TextBox)ctr).Text;

        return HttpContext.Current.Request.Form[ctr.UniqueID];
    }

    private void LoadEsquemaIndexs(Int32 dtId)
    {
        var Index = new SIndex();

        Visible = true;

        long UserId = long.Parse(Session["UserId"].ToString());

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
                ToolTip = currentIndex.ItemArray[1].ToString()
            };

            tcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

            lbValue = new TextBox { Enabled = true, Text = "", ToolTip = "" };
            lbValue.Attributes.Add("OnClick", "IndexsChanged");
            lbValue.ID = "Index" + currentIndex.ItemArray[0];
            tcIndexValue = new TableCell();
            tcIndexValue.Controls.Add(lbValue);

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

    /// <summary>
    /// Clears the inner controls 
    /// </summary>
    private void Clear()
    {
        tblIndices.Controls.Clear();
    }
}