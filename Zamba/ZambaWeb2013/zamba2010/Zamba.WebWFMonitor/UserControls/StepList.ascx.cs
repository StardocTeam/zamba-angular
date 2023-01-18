using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;


public partial class UserControls_StepList : System.Web.UI.UserControl
{
    public DsSteps DsSteps = new DsSteps();
    public delegate void LoadTasksEventHandler(); 
    public event LoadTasksEventHandler LoadTasks;
    
    public void SelectStep(Int32 StepId)
    {
        lstWF.SelectedValue = StepId.ToString();
    }

    protected void WFList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (!String.IsNullOrEmpty(lstWF.SelectedValue))
        {
            try
            {
                Int32 StepID = Int32.Parse(lstWF.SelectedValue);
                String StepName = lstWF.SelectedItem.Text;
                Session.Add("StepId", lstWF.SelectedValue);
                if (LoadTasks != null)
                    LoadTasks();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }
}
