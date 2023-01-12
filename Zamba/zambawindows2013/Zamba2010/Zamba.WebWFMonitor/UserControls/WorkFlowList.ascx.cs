
using System;
public partial class UserControls_WorkFlowList : System.Web.UI.UserControl
{
    public void SelectWf(Int32 WfId)
    {
        lstWorkFlow.SelectedValue = WfId.ToString();
        Session.Add("WfId", WfId);
    }

    protected void WFList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Session.Add("WfId", lstWorkFlow.SelectedValue);
    }
}
