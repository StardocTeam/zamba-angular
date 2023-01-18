using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zamba.Web.Views.WF.TaskDetails
{
    public partial class TaskChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CurrentUser.Value = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
        }
    }
}