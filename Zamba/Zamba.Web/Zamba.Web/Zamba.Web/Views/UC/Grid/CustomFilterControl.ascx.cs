using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Zamba;
using Zamba.Core;
using Zamba.Services;

public partial class Views_UC_Grid_CustomFilterControl : System.Web.UI.UserControl
{
    IFiltersComponent _fc = null;
    DataTable _dtDoctypes = null;

    public delegate void FilterCall(object sender, EventArgs e);
    public event FilterCall OnFilterCall;

    public IFiltersComponent FC
    {
        get
        {
            return _fc;
        }
    }
    public int StepLive
    {
        get
        {
            if (Session["StepLive"] != null)
                return int.Parse(Session["StepLive"].ToString());
            else
                return -1;
        }
    }



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
    protected void Page_Init(object sender, EventArgs e)
    {

        Page.EnableEventValidation = false;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ZOptBusiness zoptb = new ZOptBusiness();
        String CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        if (PrevStepId > -1 && StepLive < 0)

        {
            if (_fc == null) _fc = new Zamba.Filters.FiltersComponent();
            GetCMB(PrevStepId);
        }

        if (StepLive > -1)
        {
            GetCMB(StepLive);
        }



    }

  

    public void ClearCurrentFilters(long stepid)
    {
        WFStepBusiness WFSB = new WFStepBusiness();

        if (_dtDoctypes == null)
            _dtDoctypes = WFSB.GetDocTypesByWfStepAsDT(stepid, (Zamba.Membership.MembershipHelper.CurrentUser).ID);

        WFSB = null;

        if (_dtDoctypes != null)
        {
            foreach (DataRow dr in _dtDoctypes.Rows)
            {
                if (_fc == null) _fc = new Zamba.Filters.FiltersComponent();
                _fc.ClearFilters(long.Parse(dr[0].ToString()), Zamba.Membership.MembershipHelper.CurrentUser.ID, true, null, false);
            }
        }
    }


    public void GetCMB(long stepId)
    {
        WFStepBusiness WFSB = new WFStepBusiness();

        if (_dtDoctypes == null)
            _dtDoctypes = WFSB.GetDocTypesByWfStepAsDT(stepId, (Zamba.Membership.MembershipHelper.CurrentUser).ID);
        WFSB = null;

        if (_dtDoctypes.Rows.Count > 0)
        {
            long dtId = Int64.Parse(_dtDoctypes.Rows[0]["Doc_Type_Id"].ToString());
            //List<string> ZColumns = new List<string>();
            Dictionary<long, string> Columns = new Dictionary<long, string>();

            foreach (var colum in GridColumns.VisibleColumns)
            {
                Columns.Add(colum.Value , CultureInfo.CurrentCulture.TextInfo.ToTitleCase(colum.Key));
            }


            List<IIndex> Indexs = ZCore.GetInstance().FilterIndex(dtId, false, true);
            foreach (var indice in Indexs)
            {
                Columns.Add(indice.ID, indice.Name);
            }

            IndexsDropdown.DataSource = Columns.OrderBy(c => c.Value);
            IndexsDropdown.DataTextField = "Value";
            IndexsDropdown.DataValueField = "Key";
            IndexsDropdown.DataBind();

        }
        else
        {


        }
    }
}





