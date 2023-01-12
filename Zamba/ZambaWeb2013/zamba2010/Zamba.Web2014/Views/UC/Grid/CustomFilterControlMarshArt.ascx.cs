using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Services;


public partial class Views_UC_Grid_CustomFilterControlMarshArt : System.Web.UI.UserControl
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

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnClear_Click(object sender, ImageClickEventArgs e) 
    {
        long stepId = -1;
        if (Session["StepId"] != null)
            stepId = long.Parse(Session["StepId"].ToString());
        ClearCurrentFilters(stepId);
        Session["FilterChage"] = true;

        OnFilterCall(this, EventArgs.Empty);
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        ZOptBusiness zoptb = new ZOptBusiness();
        String CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        if (CurrentTheme == "Marsh")
        {
            long stepId = -1;
            if (Session["StepId"] != null)
                stepId = long.Parse(Session["StepId"].ToString());

            if (stepId > -1)
            {
                if (_fc == null) _fc = new Zamba.Filters.FiltersComponent();
                ClearCurrentFilters(stepId);
                SetFilters(stepId);
            }
            Session["FC"] = _fc;

            OnFilterCall(this, EventArgs.Empty);
        }
    }

    public void ClearCurrentFilters(long stepid)
    {
        sDocType sDocType = new sDocType();

        if(_dtDoctypes == null)
            _dtDoctypes = sDocType.GetDocTypesByWfStepAsDT(stepid, ((IUser)Session["User"]).ID);

        if (_dtDoctypes != null)
        {
            foreach (DataRow dr in _dtDoctypes.Rows)
            {
                if (_fc == null) _fc = new Zamba.Filters.FiltersComponent();
                _fc.ClearFilters(long.Parse(dr[0].ToString()), int.Parse(this.Session["UserId"].ToString()), true, null, false);
            }
        }
    }

    public void SetFilters(long stepId)
    {
        sDocType sDocType = new sDocType();

        if (_dtDoctypes == null)
            _dtDoctypes = sDocType.GetDocTypesByWfStepAsDT(stepId, ((IUser)Session["User"]).ID);

        long dtId;

        foreach (DataRow dr in _dtDoctypes.Rows)
        { 
            dtId = long.Parse(dr[0].ToString());

            if (this.txtapellido.Value != string.Empty) _fc.SetNewFilter(1355, "1355", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtapellido.Value, dtId, false, "Apellido", IndexAdditionalType.LineText, "manual", true);
            if (this.txtcuil.Value != string.Empty) _fc.SetNewFilter(1358, "1358", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtcuil.Value, dtId, false, "Cuil", IndexAdditionalType.LineText, "manual", true);
            if (this.txtpoliza.Value != string.Empty) _fc.SetNewFilter(1361, "1361", IndexDataType.Numerico, int.Parse(this.Session["UserId"].ToString()), "=", this.txtpoliza.Value, dtId, false, "Poliza", IndexAdditionalType.LineText, "manual", true);
            if (this.txtcliente.Value != string.Empty) _fc.SetNewFilter(1362, "1362", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtcliente.Value, dtId, false, "Cliente", IndexAdditionalType.LineText, "manual", true);
        }
    }
}
