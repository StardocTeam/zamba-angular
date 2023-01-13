using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zamba.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
		Response.Redirect("~/GlobalSearch/search/Search.html");

        }
		public _Default()
		{
		Load += Page_Load;
		}

   
    }
}