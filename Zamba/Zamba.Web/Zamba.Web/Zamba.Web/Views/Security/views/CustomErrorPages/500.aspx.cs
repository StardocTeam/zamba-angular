using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

namespace Zamba.Web.Views.CustomErrorPages
{
    public partial class _Security_views_500 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software - Pagina no encontrada" : title + " - Zamba Software - Pagina no encontrada";
            }
            catch { }



        }
    }
}