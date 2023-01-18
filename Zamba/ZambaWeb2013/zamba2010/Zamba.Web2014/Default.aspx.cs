
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

partial class _Default : System.Web.UI.Page
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		Response.Redirect("~/Views/Main/default.aspx");
	}
	public _Default()
	{
		Load += Page_Load;
	}

}

