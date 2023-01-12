using System;
using System.Data;
using Zamba.Core;
using Zamba.Services;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Configuration;
using Zamba.AppBlock;
using Zamba.Web.App_Code.Helpers;
using Zamba.Web;
using Zamba.Filters;
using Zamba.Data;

public partial class ucTaskGrid : webGrid
{
    private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
    private const string DOC_TYPE_ID_COLUMNNAME = "doc_type_id";
    private const string SITUACION_COLUMNNAME = "Situacion";
    private const string FAVORITE_COLUMNNAME = "IsFavorite";
    private const string IMPORTANT_COLUMNNAME = "IsImportant";

    long _gridTotalCount;

    UserPreferences UP = new UserPreferences();
    WFFactory WFF = new WFFactory();

    public Views_UC_Grid_CustomFilterControl ucTaskGridFilter;

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


    ///// <summary>
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
        try
        {
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                SetLicenceConsume();
                grvTaskGrid.OnNeedDataSource += new ZGridView.NeedDataSource(grvTaskGrid_OnNeedDataSource);

                bool filtersChanged = false;

                if (!string.IsNullOrEmpty(cmbDocType.SelectedValue))
                {
                    int filtersCount = new FiltersComponent().GetDocumentFiltersCount(long.Parse(cmbDocType.SelectedValue), true);
                    filtersChanged = filtersCount != (int)Session["FiltersCount"];
                    Session["FiltersCount"] = filtersCount;
                }

                if (!IsPostBack || filtersChanged)
                {
                    BindZGrid(true);
                    Session["FiltersCount"] = new FiltersComponent().GetDocumentFiltersCount(string.IsNullOrEmpty(cmbDocType.SelectedValue) ? 0 : long.Parse(cmbDocType.SelectedValue), true);
                }

                if (PrevStepId != StepId)
                {
                    var grid = ((ZGridView)(FindControl("grvTaskGrid")));
                    (grid.FindControl("hiddenCurrentPage") as HiddenField).Value = string.Empty;
                    grvTaskGrid.PageIndex = 1;
                }
                PrevStepId = StepId;
            }
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }

    public void CmbDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindZGrid(true);
    }

    /// <summary>
    /// Actualiza la conexion y la licencia
    /// </summary>
    private void SetLicenceConsume()
    {
        try
        {
            IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
            SRights rights = new SRights();
            Int32 type = 0;

            if (user.WFLic)
            {
                type = 1;
            }
            else
            {
                Ucm ucm = new Ucm();
                ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, Request.UserHostAddress.Replace("::1","127.0.0.1"), Int16.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), 1);
                Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                (Zamba.Membership.MembershipHelper.CurrentUser).WFLic = true;
                type = 1;
            }

            if (user.ConnectionId > 0)
            {
                UserPreferences UserPreferences = new UserPreferences();
                //Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
                UserPreferences = null;
            }

        }
        catch (Exception ex)
        {
            ZException.Log(ex);
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
                DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}\" border=0/ >",
                DataTextField = "ICONID"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"~/Views/WF/TaskSelector.ashx?doctype={0}&docid={1}&taskid={2}&wfstepid={3}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

            grvTaskGrid.CurrentGrid.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }

    private void replaceTaskStateByImage(DataTable dt)
    {
        try
        {
            String[] a = { "DOCTYPEID", "DOCID", "TASK_ID", "WFSTEPID" };

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Situacion",
                Target = "_blank",
                Text = "Situacion",
                DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}&type=taskstate\" border=0/ >",
                DataTextField = "SITUACION"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"~/Views/WF/TaskSelector.ashx?doctype={0}&docid={1}&taskid={2}&wfstepid={3}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
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
                    if (c.Caption != SITUACION_COLUMNNAME)
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
                    else
                    {
                        var a = 9;
                    }
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
            NotFound.Visible = false;
            NotFoundText.Visible = false;
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

                    if (headerText.StartsWith("I") && GridHelper.IsNumeric(headerText.Substring(1, headerText.Length - 1))
                        || headerText == FAVORITE_COLUMNNAME || headerText == IMPORTANT_COLUMNNAME)
                        col.Visible = false;
                }
            }
            else
            {
                NotFound.Visible = true;
                NotFoundText.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region 
    private ArrayList getDocTypeSelected()
    {
        long SelectedDocType = DocTypeIdDropHidden.Value.Equals(string.Empty) ? 0 : Convert.ToInt64(DocTypeIdDropHidden.Value);

        if (Convert.ToInt64(Session["LastWFIdSelected"]) != WFF.GetWorkflowIdByStepId(StepId) || SelectedDocType == 0)
        {
            if (cmbDocType.Items.Count > 0)
                cmbDocType.SelectedIndex = 0;
            SelectedDocType = 0;
        }
        else
        {
            cmbDocType.SelectedValue = SelectedDocType.ToString();
        }

        WFBusiness WFB = new WFBusiness();
        Session["LastWFIdSelected"] = WFB.GetWorkflowIdByStepId(Convert.ToInt32(Session["StepId"]));
        WFB = null;

        ArrayList docTypeIds = new ArrayList();
        if (SelectedDocType == 0)
        {
            foreach (ListItem item in cmbDocType.Items)
            {
                if (long.Parse(item.Value) > 0)
                    docTypeIds.Add(long.Parse(item.Value));
            }
        }
        else
        {
            docTypeIds.Add(long.Parse(cmbDocType.SelectedValue));
        }

        Session["docTypeIds"] = docTypeIds;
        return docTypeIds;
    }

    private void LoadDocTypes(long stepid)
    {
        try
        {
            WFStepBusiness WFSB = new WFStepBusiness();
            DataTable dt = WFSB.GetDocTypesByWfStepAsDT(stepid, (Zamba.Membership.MembershipHelper.CurrentUser).ID).Copy();
            WFSB = null;

            cmbDocType.Items.Clear();
            cmbDocType.SelectedValue = null;
            cmbDocType.DataSource = dt;

            if (dt.Rows.Count > 1)
            {
                cmbDocType.Enabled = true;
                cmbDocType.Visible = true;

                if (Convert.ToInt64(dt.Rows[0][DOC_TYPE_ID_COLUMNNAME]) != 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[DOC_TYPE_ID_COLUMNNAME] = 0;
                    dr[DOC_TYPE_NAME_COLUMNNAME] = "Todas las tareas";
                    dt.Rows.InsertAt(dr, 0);
                }
            }
            else
            {
                cmbDocType.Enabled = false;
            }

            cmbDocType.DataTextField = DOC_TYPE_NAME_COLUMNNAME;
            cmbDocType.DataValueField = DOC_TYPE_ID_COLUMNNAME;
            cmbDocType.DataBind();
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }
    #endregion

    public void ApplyFilter()
    {
        BindZGrid(false);
    }

    void grvTaskGrid_OnNeedDataSource(object sender, EventArgs e)
    {
        BindZGrid(false);
    }

    /// <summary>
    /// Obtiene el DataSource para la grilla y setea las variables de la misma para armar el paginado
    /// </summary>//carga la grilla
    /// 
    public void BindZGrid(bool reBind)
    {
        try
        {
            if (StepId > 0)
            {
                LoadDocTypes(StepId);
                STasks sTasks = new STasks();
                ArrayList dtSelected = getDocTypeSelected();
                _gridTotalCount = 0;
                //Cargamos la tabla de resultados y con ella la cantidad de los mismos
                if (PrevStepId != StepId)
                {
                    var grid = ((ZGridView)(FindControl("grvTaskGrid")));
                    (grid.FindControl("hiddenCurrentPage") as HiddenField).Value = string.Empty;
                    grvTaskGrid.PageIndex = 1;
                }

                FiltersComponent FC = new FiltersComponent();
                DataTable dt = sTasks.getTasksAsDT(StepId, dtSelected, grvTaskGrid.PageIndex, PageItemCount, FC, ref _gridTotalCount);

                grvTaskGrid.StepCount = _gridTotalCount;

                double pageCount = (double)((decimal)_gridTotalCount / (decimal)PageItemCount);

                grvTaskGrid.PageCount = (int)Math.Ceiling(pageCount);

                FormatGridview();

                if (dt.Columns[SITUACION_COLUMNNAME] != null)
                    replaceTaskStateByImage(dt);

                generateGridColumns(dt);
                grvTaskGrid.DataSource = dt;
                formatValues(dt);

                if (reBind)
                    grvTaskGrid.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
        }
    }

   

}