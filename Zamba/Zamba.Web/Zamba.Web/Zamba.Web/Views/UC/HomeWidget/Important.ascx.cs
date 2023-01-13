using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

namespace Zamba.Web.Views.UC.HomeWidget
{
    public partial class Important : System.Web.UI.UserControl
    {
		public string ZambaWebAdminRestApiURL;

		protected void Page_Init(object sender, System.EventArgs e)
		{
            new Zamba.Core.ZCore().InitWebPage("Zamba.HomeWidget");

            ZambaWebAdminRestApiURL = ZOptBusiness.GetValueOrDefault("ZambaWebAdminRestApiURL", "http://localhost/zamba.WebAdmin");
		}

		protected void Page_Load(object sender, EventArgs e)
        {
            //USERID.Value = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

        }
    }
}