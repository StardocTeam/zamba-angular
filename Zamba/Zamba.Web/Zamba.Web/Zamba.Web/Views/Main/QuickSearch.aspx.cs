using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zamba.Web.Views.Main
{
    public partial class QuickSearch : System.Web.UI.Page
    {


        protected void Page_Init(object sender, EventArgs e)
        {

            Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
            Page.Theme = ZC.InitWebPage("Zamba.QuickSearch");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}