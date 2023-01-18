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

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnClear_Click(object sender, EventArgs e) 
    {
        long stepId = -1;
        if (Session["StepId"] != null)
            stepId = long.Parse(Session["StepId"].ToString());
        ClearCurrentFilters(stepId);
        Session["FilterChage"] = true;

        //OnFilterCall(this, EventArgs.Empty);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ZOptBusiness zoptb = new ZOptBusiness();
        String CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        if (CurrentTheme == "AysaDiseno")
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

           // OnFilterCall(this, EventArgs.Empty);
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
            IFiltersComponent ss = new FiltersComponent();
            ss.SetNewFilter(1188, "1188", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtrazonsocial.Value, dtId, false, "Razon Social", IndexAdditionalType.LineText, "manual", true);
            if (this.txtrazonsocial.Value != string.Empty) _fc.SetNewFilter(1188, "1188", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtrazonsocial.Value, dtId, false, "Razon Social", IndexAdditionalType.LineText, "manual", true);
            if (this.txtcalle.Value != string.Empty) _fc.SetNewFilter(1192, "1192", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtcalle.Value, dtId, false, "Calle", IndexAdditionalType.LineText, "manual", true);
            if (this.txtnro.Value != string.Empty) _fc.SetNewFilter(1193, "1193", IndexDataType.Numerico, int.Parse(this.Session["UserId"].ToString()), "=", this.txtnro.Value, dtId, false, "Num", IndexAdditionalType.LineText, "manual", true);
            if (this.txtcodificacion.Value != string.Empty) _fc.SetNewFilter(1187, "1187", IndexDataType.Alfanumerico, int.Parse(this.Session["UserId"].ToString()), "Contiene", this.txtcodificacion.Value, dtId, false, "Codificacion de la Industria", IndexAdditionalType.LineText, "manual", true);
        }
    }
}
