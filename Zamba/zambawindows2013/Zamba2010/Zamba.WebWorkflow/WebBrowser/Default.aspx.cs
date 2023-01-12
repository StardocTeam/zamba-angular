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
using System.Text;

public partial class WebBrowser_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        prueba.Attributes.Add("src", System.Web.HttpRuntime.AppDomainAppVirtualPath + "/temp/paginaprueba.html");
        
        string s = prueba.InnerText; 
    }

}
