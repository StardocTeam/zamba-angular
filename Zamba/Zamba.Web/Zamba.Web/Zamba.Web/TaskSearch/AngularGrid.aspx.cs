using System;
using System.Data;
using Zamba.Core.WF.WF;
using Zamba.Core;
using Zamba.Membership;
using Zamba.Services;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Configuration;
using System.Web.UI;
using Zamba.AppBlock;
using Zamba.Web.App_Code.Helpers;
using Zamba.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;

public partial class ucTaskGrid : webGrid
{
    private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
    private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
    long _gridTotalCount;

    //public Views_UC_Grid_CustomFilterControl ucTaskGridFilter;

    public int PageItemCount
    {
        get
        {
            return Int16.Parse(WebConfigurationManager.AppSettings["PageSize"]);
        }
    }

    /// <summary>
    /// Etapa actual
    /// </summary>
    public int StepId
    {
        get
        {
            if (Session["StepId"] != null)
                return int.Parse(Session["StepId"].ToString());
            else
                return -1;
        }
    }

    /// <summary>
    /// Etapa anterior
    /// </summary>
    public int PrevStepId
    {
        get
        {
            if (Session["PrevStepId"] != null)
                return int.Parse(Session["PrevStepId"].ToString());
            else
                return -1;
        }
        set
        {
            Session["PrevStepId"] = value;
        }
    }

    /// <summary>
    /// Filtros utilizados
    /// </summary>
    public IFiltersComponent Filter
    {
        get
        {
            if (Session["FC"] != null)
                return (IFiltersComponent)Session["FC"];
            else
                return null;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Definido en Arbol.ascx.cs que no cargue grilla hasta que se haya seleccionado 'Listado' mejora velocidad carga
        //if (Session["RefreshGrid"] == null)
        //    return;
        //Si no es postback y hay usuario
        if (Session["User"] != null)
        {
            SetLicenceConsume();
            /*  BindZGrid(true); *///}//else//{

            if (PrevStepId != StepId)
            {
                var grid = ((ZGridView)(this.FindControl("grvTaskGrid")));
                (grid.FindControl("hiddenCurrentPage") as HiddenField).Value = string.Empty;
                //grvTaskGrid.PageIndex = 1;
            }
            PrevStepId = StepId;
            GetDataTable();
         
        }

        //grvTaskGrid.OnNeedDataSource += new ZGridView.NeedDataSource(grvTaskGrid_OnNeedDataSource);
    }

    /// <summary>
    /// Actualiza la conexion y la licencia
    /// </summary>
    private void SetLicenceConsume()
    {
        try
        {
            IUser user = (IUser)Session["User"];
            SRights rights = new SRights();
            Int32 type = 0;

            if (user.WFLic)
            {
                type = 1;
            }
            else
            {
                SUserPreferences SUserPreferences = new SUserPreferences();
                Ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), 1);
                SUserPreferences = null;
                Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                ((IUser)Session["User"]).WFLic = true;
                type = 1;
            }

            if (user.ConnectionId > 0)
            {
                SUserPreferences SUserPreferences = new SUserPreferences();
                //Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                SUserPreferences = null;
            }

        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    #region Grid formating
    private void FormatGridview()
    {
        try
        {
            String[] a = { "DOCTYPEID", "DOCID", "TASK_ID", "WFSTEPID" };

            grvTaskGrid.CurrentGrid.Columns.Clear();

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Ver",
                Target = "_blank",
                Text = "Ver",
                DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}\" border=0/>",
                DataTextField = "ICONID"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"~/Views/WF/TaskSelector.ashx?doctype={0}&docid={1}&taskid={2}&wfstepid={3}";

            grvTaskGrid.CurrentGrid.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }

    private void generateGridColumns(DataTable _dt)
    {
        try
        {
            if (_dt != null && _dt.Columns.Count > 0)
            {
                foreach (DataColumn c in _dt.Columns)
                {
                    BoundField f = new BoundField
                    {
                        DataField = c.Caption,
                        ShowHeader = true,
                        HeaderText = c.Caption,
                        SortExpression = c.Caption + " ASC"
                    };

                    grvTaskGrid.CurrentGrid.Columns.Add(f);
                }
            }
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }

    private void formatValues(DataTable _dt)
    {
        try
        {
            string headerText;
            lblinfo.Visible = false;

            if (_dt.Rows.Count > 0)
            {
                for (int col = 0; col < grvTaskGrid.CurrentGrid.Columns.Count; col++)
                {
                    string colname = grvTaskGrid.CurrentGrid.Columns[col].HeaderText;

                    if (colname.ToLower() == "Original")
                        grvTaskGrid.CurrentGrid.Columns[col].Visible = false;

                    if (GridHelper.GetVisibility(colname.ToLower(), GridHelper.GridType.Task) == false)
                    {
                        grvTaskGrid.CurrentGrid.Columns[col].Visible = false;
                    }
                }

                foreach (DataControlField col in grvTaskGrid.CurrentGrid.Columns)
                {
                    headerText = col.HeaderText;

                    if (headerText.StartsWith("I") && GridHelper.IsNumeric(headerText.Substring(1, headerText.Length - 1)))
                        col.Visible = false;
                }
            }
            else
            {
                lblinfo.Text = "La etapa actual no posee tareas";
                lblinfo.Visible = true;
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Font.Size = 14;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region DocTypes
    private ArrayList getDocTypeSelected()
    {
        ArrayList docTypeIds = new ArrayList();

        if (cmbDocType.SelectedValue != null && !string.IsNullOrEmpty(cmbDocType.SelectedValue.ToString()))
        {
            if (long.Parse(cmbDocType.SelectedValue.ToString()) == 0)
            {
                foreach (ListItem item in cmbDocType.Items)
                {
                    if (long.Parse(item.Value) > 0)
                    {
                        docTypeIds.Add(long.Parse(item.Value));
                    }
                }
            }
            else
            {
                docTypeIds.Add(long.Parse(cmbDocType.SelectedValue.ToString()));
            }
        }

        Session["docTypeIds"] = docTypeIds;

        return docTypeIds;
    }

    private void LoadDocTypes(long stepid)
    {
        sDocType sDocType = new sDocType();
        DataTable dt = sDocType.GetDocTypesByWfStepAsDT(stepid, ((IUser)Session["User"]).ID);

        cmbDocType.DataTextField = DOC_TYPE_NAME_COLUMNNAME;
        cmbDocType.DataValueField = DOC_TYPE_ID_COLUMNNAME;
        cmbDocType.DataSource = dt;
        cmbDocType.DataBind();

        ZOptBusiness zopt = new ZOptBusiness();

        if (zopt.GetValue("WebViewDocTypesInTaskGrid") != null)
        {
            if (bool.Parse(zopt.GetValue("WebViewDocTypesInTaskGrid")) && dt.Rows.Count > 1)
            {
                cmbDocType.Enabled = true;
                //pnlFilters.Visible = true;

                if ((decimal)dt.Rows[dt.Rows.Count - 1][DOC_TYPE_ID_COLUMNNAME] != 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[DOC_TYPE_ID_COLUMNNAME] = 0;
                    dr[DOC_TYPE_NAME_COLUMNNAME] = "Todas las tareas";
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                cmbDocType.Enabled = false;
                //pnlFilters.Visible = false;
            }
        }
        else
        {
            cmbDocType.Enabled = false;
            //pnlFilters.Visible = false;
        }
        zopt = null;
    }
    #endregion

    //public void ApplyFilter()
    //{
    //    BindZGrid(true);
    //}

    //void grvTaskGrid_OnNeedDataSource(object sender, EventArgs e)
    //{
    //    BindZGrid(false);
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetDataTable()
    {

        cmbDocType.Enabled = false;
        //pnlFilters.Visible = false;
        LoadDocTypes(StepId);

        STasks sTasks = new STasks();
        ArrayList dtSelected = getDocTypeSelected();
        _gridTotalCount = 0;
        //Cargamos la tabla de resultados y con ella la cantidad de los mismos
        DataTable dt = sTasks.getTasksAsDT(StepId, dtSelected, grvTaskGrid.PageIndex, this.PageItemCount, Filter, ref _gridTotalCount);
       
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in dt.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }



    public string DataTableToJSON(DataTable table)
    {
        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = (Convert.ToString(row[col]));
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        return serializer.Serialize(list);


    }



    //[WebMethod]
    ////[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
    //public  string ReturnGrid()
    //{

    //    //JavaScriptSerializer js = new JavaScriptSerializer();
    //    //Context.Response.Clear();
    //    //Context.Response.ContentType = "application/json";
    //    //ReturnGridData data = new ReturnGridData();
    //    var jj = DataTableToJSON(GetDataTable());
    //    //Context.Response.Write(js.Serialize(jj));
    //    return Newtonsoft.Json.JsonConvert.SerializeObject(jj);
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static string terere()
    {
        return "fuckyou";
    }

    [WebMethod]
    public static DataTable A()
    {
        DataTable table = new DataTable();
        table.Columns.Add("date", typeof(string));
        table.Columns.Add("text", typeof(string));

        table.Rows.Add("20/05/2012", "A");
        table.Rows.Add("20/05/2012", "B");
        table.Rows.Add("20/05/2012", "C");

        return table;
    }
    [WebMethod]
    public static DataTable B()
    {
        DataTable table = new DataTable();
        table.Columns.Add("date", typeof(string));
        table.Columns.Add("text", typeof(string));

        table.Rows.Add("20/05/2012", "P");
        table.Rows.Add("20/05/2012", "Q");
        table.Rows.Add("20/05/2012", "R");

        return table;
    }
    [WebMethod]
    public static DataTable C()
    {
        DataTable table = new DataTable();
        table.Columns.Add("date", typeof(string));
        table.Columns.Add("text", typeof(string));

        table.Rows.Add("20/05/2012", "X");
        table.Rows.Add("20/05/2012", "Y");
        table.Rows.Add("20/05/2012", "Z");

        return table;
    }

}


//public class ReturnGridData
//  {
//        public string Message;
//    }
//}

