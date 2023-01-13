using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zamba.Collaboration.StardocHelpDesk.Views
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var thisDomain = Zamba.Membership.MembershipHelper.AppUrl;
            ThisDomain.Value  = thisDomain;
        }

       
    }
}