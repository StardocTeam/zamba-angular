using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Data;
using Zamba.Framework; 


namespace Zamba.Web.Views.Security
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["userid"] == null || Request.Params["token"] == null)
                {
                    throw new Exception("fallo la autenticacion de dashboard RRHH");
                }
                Int64 userID = Convert.ToInt64(Request.Params["userid"].ToString());
                string token = Request.Params["token"].ToString();
                Results_Business rb = new Results_Business();
                if (!rb.getValidateActiveSession(userID, token))
                {
                    throw new Exception("fallo la autenticacion de dashboard RRHH");
                }
                ZssFactory zssFactory = new ZssFactory();
                Zss zss = new Zss();
                User user = new User();
                user.ID = userID;
                zss = zssFactory.GetZss(user);
                hdnAuthorizationData.Value = JsonConvert.SerializeObject(zss);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<html><body><script>");
                sb.AppendLine("window.parent.postMessage('error', '*');");
                sb.AppendLine("</script></body></html>");
                Response.StatusCode = 200;
                Response.ContentType = "text/html";
                Response.Write(sb.ToString());
                Response.End();
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
            }            
        }
    }
}