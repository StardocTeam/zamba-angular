﻿using System;
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
using Zamba.Core;


public partial class UserHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      string  UserId = Session["UserId"].ToString();
      DataSet dsUserHistory = UserBusiness.Actions.GetUserActions(Int64.Parse(UserId));

      gvUserHistory.DataSource = dsUserHistory;
      gvUserHistory.DataBind();
              
    }
}