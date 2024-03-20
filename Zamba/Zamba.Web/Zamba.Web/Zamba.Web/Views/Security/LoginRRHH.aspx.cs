using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (Request.Params["user"] == null || Request.Params["token"] == null)
            {
                return;
            }
            Int64  userID = Convert.ToInt64(Request.Params["user"].ToString());
            string token = Request.Params["token"].ToString();
            Results_Business rb = new Results_Business();
            if (!rb.getValidateActiveSession(userID, token))
            {
                return;
            }                
            ZssFactory zssFactory = new ZssFactory();
            Zss zss = new Zss();
            User user = new User();
            user.ID = userID ;
            zss = zssFactory.GetZss(user);
            hdnAuthorizationData.Value=JsonConvert.SerializeObject(zss);
        }
    }
}