using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_Tools_TestFileDirectories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Archivo APP.INI: " + Zamba.Servers.Server.currentfile());
        Response.Write("Ruta Trace: " + Zamba.Core.ZTrace.GetTempDir("\\Exceptions"));

    }
}