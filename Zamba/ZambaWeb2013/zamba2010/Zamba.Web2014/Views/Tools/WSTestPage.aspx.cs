using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.DataExt.WSResult.Consume;

public partial class Views_TestWS_WSTestPage : System.Web.UI.Page
{
    static WSResultsFactory wsFacotry = new WSResultsFactory();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userTools"] == null)
            Response.Redirect("../../Views/Security/Login_WebTools.aspx");

        lblCredentials.Text = (wsFacotry.UseDefaultCredentials) ? "Default" : "Anonimo";
        lblUrl.Text = wsFacotry.WSUrl;
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        try
        {
            string test = wsFacotry.ConsumeGetAppTempPath();
            lblResult.Text = "Consumido con exito : " + test;
        }
        catch (Exception ex)
        {
            lblResult.Text = "Error" + ex.ToString();
        }
    }

    protected void btnAppConfigLocation_Click(object sender, EventArgs e)
    {
        try
        {
            string test = wsFacotry.ConsumeGetConfigPath();
            lblResult.Text = "Locacion app config: " + test;
        }
        catch (Exception ex)
        {
            lblResult.Text = "Error" + ex.ToString();
        }
    }

    protected void btnLogLocation_Click(object sender, EventArgs e)
    {
        try
        {
            string test = wsFacotry.ConsumeGetAppTempPath();
            lblResult.Text = "Locacion log: " + test;
        }
        catch (Exception ex)
        {
            lblResult.Text = "Error" + ex.ToString();
        }
    }
}