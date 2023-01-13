using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Zamba.Core;
using System.Collections;
using Zamba.Services;

//Clase utilizada para la regla DoShowTable
//08/07/11: Creada por Javier Páez.
public partial class Views_UC_WF_Rules_UCDoShowTable : System.Web.UI.UserControl, IRule
{
    private DataTable _showValue;
    private long _taskID;
    private string _selectedChecks;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Fill and Configuration
    //Propiedad que cargará la grilla.
    public DataTable ShowValue
    {
        set
        {
            _showValue = value.Copy();
            FillAndConfigurate();
        }
        get { return _showValue; }
    }

    public bool DataIsEmpty
    {
        get;
        set;
    }

    public bool isMultiSelect
    {
        get;
        set;
    }

    public string ShowColumns
    {
        get;
        set;
    }

    public bool JustShow
    {
        get;
        set;
    }

    public string SaveColumns
    {
        get;
        set;
    }

    public string SelectedChecks
    {
        get
        {
            return _selectedChecks;
        }
        set
        {
            _selectedChecks = value;
        }
    }

    //Llena y configura la grilla que mostrará los datos.
    private void FillAndConfigurate()
    {
        try
        {
            if (!DataIsEmpty)
            {
                dgValue.AutoGenerateColumns = false;
                //Obtenemos las columnas de la tabla.
                DataColumnCollection sourceColumns = _showValue.Columns;
                DataControlField tempColumn;
                //Mapeamos las columnas.
                foreach (DataColumn col in sourceColumns)
                {
                    tempColumn = new BoundField();

                    ((BoundField)tempColumn).DataField = col.ColumnName;
                    tempColumn.HeaderText = col.Caption;

                    dgValue.Columns.Add(tempColumn);
                }

                dgValue.DataSource = _showValue;


                if (JustShow)
                {
                    if (dgValue.Columns.Count > 0)
                        dgValue.Columns[0].Visible = false;
                }

                if (!string.IsNullOrEmpty(ShowColumns))
                {
                    string[] strCols = ShowColumns.Split((','));
                    //Oculta todas las columnas
                    foreach (DataControlField column in dgValue.Columns)
                    {
                        column.Visible = false;
                    }

                    Int32 tempIndex;
                    String colname;
                    String colalias;

                    //Muestra las columnas configuradas en la regla
                    foreach (string strCol in strCols)
                    {
                        if (int.TryParse(strCol, out tempIndex))
                        {
                            if (dgValue.Columns.Count > tempIndex - 1)
                                dgValue.Columns[tempIndex].Visible = true;
                        }
                        else
                        {
                            colname = strCol;
                            colalias = string.Empty;

                            if (strCol.ToLower().Contains(" "))
                            {
                                String[] colarray = strCol.Replace("  ", " ").Split(Char.Parse(" "));

                                if (colarray.Length > 1)
                                {
                                    colname = colarray[0].Trim();
                                    colalias = colarray[1].Trim().Replace("[", String.Empty).Replace("]", String.Empty);
                                }
                            }

                            if (int.TryParse(colname, out tempIndex))
                            {
                                if (dgValue.Columns.Count > tempIndex - 1)
                                {
                                    dgValue.Columns[tempIndex].Visible = true;
                                    if (colalias.Length > 0) dgValue.Columns[tempIndex].HeaderText = colalias;
                                }
                            }
                        }
                    }

                    if (!JustShow)
                    {
                        if (dgValue.Columns.Count > 0)
                            dgValue.Columns[0].Visible = true;
                    }
                }

                dgValue.DataBind();
                hdnMultipleCheck.Value = isMultiSelect.ToString();
            }
            else
            {
                isMultiSelect = false;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    #endregion

    #region IRule Members

    public event WFExecution._continueExecution ContinueExecution;

    public Zamba.Core.RuleExecutionResult ExecutionResult
    {
        get
        {
            return (RuleExecutionResult)Session[this._taskID + "ExecutionResult"];
        }
        set
        {
            Session[this._taskID + "ExecutionResult"] = value;
        }
    }

    public Zamba.Core.RulePendingEvents PendigEvent
    {
        get
        {
            return (RulePendingEvents)Session[this._taskID + "PendigEvent"];
        }
        set
        {
            Session[this._taskID + "PendigEvent"] = value;
        }
    }

    public List<long> ExecutedIDs
    {
        get
        {
            return (List<long>)Session[this._taskID + "ExecutedIDs"];
        }
        set
        {
            Session[this._taskID + "ExecutedIDs"] = value;
        }
    }

    public List<long> PendingChildRules
    {
        get
        {
            return (List<long>)Session[this._taskID + "PendingChildRules"];
        }
        set
        {
            Session[this._taskID + "PendingChildRules"] = value;
        }
    }

    public System.Collections.Hashtable Params
    {
        get
        {
            return (Hashtable)Session[this._taskID + "Params"];
        }
        set
        {
            Session[this._taskID + "Params"] = value;
        }
    }

    public long RuleID
    {
        get
        {
            return (long)Session[this._taskID + "RuleID"];
        }
        set
        {
            Session[this._taskID + "RuleID"] = value;
        }
    }

    public string Path
    {
        get
        {
            return (string)Session[this._taskID + "Path"];
        }
        set
        {
            Session[this._taskID + "Path"] = value;
        }
    }

    private List<ITaskResult> _results;
    public List<ITaskResult> results
    {
        get
        {
            if (Session[this._taskID + "results"] != null)
            {
                List<ITaskResult> currentresult = (List<ITaskResult>)Session[this._taskID + "results"];

                return currentresult;
            }
            else
            {
                List<ITaskResult> currentresults = new List<ITaskResult>();
                List<IExecutionRequest> ExecutionResults = (List<IExecutionRequest>)Session["ListOfTask"];
                foreach (IExecutionRequest exeresult in ExecutionResults)
                {
                    currentresults.Add(exeresult.ExecutionTask);
                }

                return currentresults;
            }
        }
        set
        {
            Session[this._taskID + "results"] = value;
        }
    }

    public long TaskID
    {
        get
        {
            return this._taskID;
        }
        set
        {
            this._taskID = value;
            this._results = this.results;
        }
    }

    bool nonVisibleTaskWithGuiRules;

    /// <summary>
    /// Define si se encuentra ejecutando reglas con GUI para tareas cerradas (no visibles).
    /// </summary>
    public bool NonVisibleTaskWithGuiRules
    {
        get
        {
            return nonVisibleTaskWithGuiRules;
        }
        set
        {
            nonVisibleTaskWithGuiRules = value;
        }
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

    #endregion

    public void _btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable returnValue = ((DataTable)_showValue).Clone();

            //Si se van a guardar valores
            if (!JustShow)
            {
                GridViewRowCollection rows = dgValue.Rows;

                //Si no esta marcada la opcion para guardar una columna especifica
                if (string.IsNullOrEmpty(SaveColumns))
                {
                    if (!string.IsNullOrEmpty(SelectedChecks))
                    {
                        string[] selectedChecksIndexes = SelectedChecks.Split(',');
                        Int32 max;

                        for (int k = 0; k < selectedChecksIndexes.Length; k++)
                        {
                            max = returnValue.Columns.Count;
                            object[] valueToAdd = new object[max];

                            for (int i = 0; i < max; i++)
                            {
                                if (_showValue.Rows[int.Parse(selectedChecksIndexes[k])][i].ToString().Trim().Length == 0 &&
                                    (_showValue.Columns[i].DataType.FullName.Contains("Int") ||
                                    _showValue.Columns[i].DataType.FullName.Contains("Decimal")))
                                {
                                    valueToAdd[i] = null;
                                }
                                else
                                {
                                    valueToAdd[i] = _showValue.Rows[int.Parse(selectedChecksIndexes[k])][i];
                                }
                            }

                            returnValue.Rows.Add(valueToAdd);
                        }
                    }
                }
                else
                {
                    //Guarda los indices de las columnas que se van a guardar
                    string[] strCols = SaveColumns.Split((','));
                    returnValue = new DataTable();

                    //Quito las columnas de mas de la tabla que se va a devolver
                    //DataTable tempTable = returnValue.Clone();
                    foreach (DataColumn col in ((DataTable)_showValue).Columns)
                    {
                        if (SaveColumns.Contains((col.Ordinal + 1).ToString()))
                        {
                            returnValue.Columns.Add(col.ColumnName);
                            //tempTable.Columns.Remove(col.ColumnName);
                        }
                    }

                    //returnValue = tempTable.Clone();

                    //Si hubo algun item seleccionado
                    if (!string.IsNullOrEmpty(SelectedChecks))
                    {

                        //Obtengo todas las filas seleccionadas
                        string[] selectedChecksIndexes = SelectedChecks.Split(',');
                        int indx;

                        //Recorre todos los checks checkeados
                        for (int k = 0; k < selectedChecksIndexes.Length; k++)
                        {
                            //Objeto donde se guardaran las celdas seleccionadas
                            object[] valueToAdd = new object[strCols.Length];

                            //Por cada columna, obtengo el valor de la celda
                            for (int i = 0; i < strCols.Length; i++)
                            {
                                if (int.TryParse(strCols[i], out indx))
                                {
                                    valueToAdd[i] = Server.HtmlDecode(rows[int.Parse(selectedChecksIndexes[k])].Cells[indx].Text).Trim();
                                }
                            }

                            //Guardo los valores seleccionados para devolverlos
                            returnValue.Rows.Add(valueToAdd);
                        }
                    }
                }
            }

            SelectedChecks = null;

            //Si hay un solo valor, se lo guarda directamente, esto es para replicar la funcionalidad de windows.
            if (returnValue.Rows.Count == 1 && returnValue.Columns.Count == 1)
            {
                this.Params.Add("returnValue", returnValue.Rows[0][0]);
            }
            else
            {
                this.Params.Add("returnValue", returnValue);
            }
            ClearCurrentExecutionSession();
            Boolean RefreshRule = false;

            ContinueExecution(this.RuleID, ref this._results, this.PendigEvent, this.ExecutionResult,
                this.ExecutedIDs, this.Params, this.PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"]);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            ExecutionResult = RuleExecutionResult.FailedExecution;
        }
    }

    public void _btnCancel_Click(object sender, EventArgs e)
    {
        //Configuración antes de cancelar la ejecución
        ClearCurrentExecutionSession();
        Params.Add("CancelRule", true);
        Boolean RefreshRule = false;

        if (nonVisibleTaskWithGuiRules)
            PendigEvent = RulePendingEvents.CancelExecution;

        //Se cancela la ejecución
        ContinueExecution(this.RuleID, ref this._results, this.PendigEvent, this.ExecutionResult,
            this.ExecutedIDs, this.Params, this.PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"]);
    }

    public void LoadOptions()
    {
        DataIsEmpty = (bool)Params["dataIsEmpty"];
        isMultiSelect = (bool)Params["isMultiSelect"];
        ShowColumns = (string)Params["showColumns"];
        JustShow = (bool)Params["justShow"];
        SaveColumns = (string)Params["saveColumns"];
        ShowValue = (DataTable)Params["tableToView"];
        SelectedChecks = (string)Params["selectedChecks" + this.RuleID];
    }
}
