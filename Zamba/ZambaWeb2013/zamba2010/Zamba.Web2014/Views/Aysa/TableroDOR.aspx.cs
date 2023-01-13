using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.IO;
using System.Text;
using Zamba.Services;
using System.Globalization;
using System.Data.SqlClient;

public partial class Views_Aysa_TableroDOR : System.Web.UI.Page
{
    int _monthToFilter;
    int _yearToFilter;

    protected void Page_Load(object sender, EventArgs e)
    {
        _monthToFilter = (MonthYearPicker.SelectedDate != null) ? ((DateTime)MonthYearPicker.SelectedDate).Month : DateTime.Now.Month;
        _yearToFilter = (MonthYearPicker.SelectedDate != null) ? ((DateTime)MonthYearPicker.SelectedDate).Year : DateTime.Now.Year;

        fileIF.Attributes.Add("src", "about:blank");

        if (!Page.IsPostBack)
        {
            MonthYearPicker.Culture = new CultureInfo("es-AR");
            MonthYearPicker.SelectedDate = DateTime.Now;

            LoadVarData();

            if (Session["User"] != null)
            {
                try
                {
                    IUser user = (IUser)Session["User"];
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        SUserPreferences SUserPreferences = new SUserPreferences();
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                        SUserPreferences = null;
                    }
                    else
                        Response.Redirect("~/Views/Security/LogIn.aspx");
                    rights = null;
                }
                catch (Exception ex)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                }
            }
        }
    }

    private int FirstQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("FirstQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;   
        }
    }

    private int SeccondQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("SeccondQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int ThirdQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("ThirdQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int FourthQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("FourthQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int FifthQuery(int RegionCode) 
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("FifthQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int SixAQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("SixAQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int SixBQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("SixBQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int SixCQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("SixCQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int EightQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("EightQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private int NineQuery(int RegionCode)
    {
        try
        {
            int returnValue = (int)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text,
                GetQuery("NineQuery"), GetOptionsParams(RegionCode));

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return 0;
        }
    }

    private string GetQuery(string QueryName)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select QueryContent ");
            sb.Append("from ZDORTableQuerys ");
            sb.Append("where QueryName = '");
            sb.Append(QueryName);
            sb.Append("'");

            string returnValue = (string)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text, sb.ToString());

            return returnValue;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return string.Empty;
        }
    }

    private IDbDataParameter[] GetOptionsParams(int RegionCode)
    {
        SqlParameter pRegion = new SqlParameter("@RegionCode", RegionCode);
        SqlParameter pMonth = new SqlParameter("@Month", _monthToFilter);
        SqlParameter pYear = new SqlParameter("@Year", _yearToFilter);

        return new IDbDataParameter[] { pRegion,pMonth,pYear };
    }

    private void LoadVarData()
    {
        var1.Text = FirstQuery(1).ToString();
        var2.Text = FirstQuery(2).ToString();
        var3.Text = FirstQuery(5).ToString();
        var4.Text = FirstQuery(3).ToString();
        var5.Text = FirstQuery(4).ToString();
        var6.Text = FirstQuery(0).ToString();
        var7.Text = SeccondQuery(1).ToString();
        var8.Text = SeccondQuery(2).ToString();
        var9.Text = SeccondQuery(5).ToString();
        var10.Text = SeccondQuery(3).ToString();
        var11.Text = SeccondQuery(4).ToString();
        var12.Text = SeccondQuery(0).ToString();
        var13.Text = ThirdQuery(1).ToString();
        var14.Text = ThirdQuery(2).ToString();
        var15.Text = ThirdQuery(5).ToString();
        var16.Text = ThirdQuery(3).ToString();
        var17.Text = ThirdQuery(4).ToString();
        var18.Text = ThirdQuery(0).ToString();
        var19.Text = FourthQuery(1).ToString();
        var20.Text = FourthQuery(2).ToString();
        var21.Text = FourthQuery(5).ToString();
        var22.Text = FourthQuery(3).ToString();
        var23.Text = FourthQuery(4).ToString();
        var24.Text = FourthQuery(0).ToString();
        var25.Text = FifthQuery(1).ToString();
        var26.Text = FifthQuery(2).ToString();
        var27.Text = FifthQuery(5).ToString();
        var28.Text = FifthQuery(3).ToString();
        var29.Text = FifthQuery(4).ToString();
        var30.Text = FifthQuery(0).ToString();
        var31.Text = SixAQuery(1).ToString();
        var32.Text = SixAQuery(2).ToString();
        var33.Text = SixAQuery(5).ToString();
        var34.Text = SixAQuery(3).ToString();
        var35.Text = SixAQuery(4).ToString();
        var36.Text = SixAQuery(0).ToString();
        var37.Text = SixBQuery(1).ToString();
        var38.Text = SixBQuery(2).ToString();
        var39.Text = SixBQuery(5).ToString();
        var40.Text = SixBQuery(3).ToString();
        var41.Text = SixBQuery(4).ToString();
        var42.Text = SixBQuery(0).ToString();
        var43.Text = SixCQuery(1).ToString();
        var44.Text = SixCQuery(2).ToString();
        var45.Text = SixCQuery(5).ToString();
        var46.Text = SixCQuery(3).ToString();
        var47.Text = SixCQuery(4).ToString();
        var48.Text = SixCQuery(0).ToString();
        var49.Text = "0";
        var50.Text = "0";
        var51.Text = "0";
        var52.Text = "0";
        var53.Text = "0";
        var54.Text = "0";
        var55.Text = EightQuery(1).ToString();
        var56.Text = EightQuery(2).ToString();
        var57.Text = EightQuery(5).ToString();
        var58.Text = EightQuery(3).ToString();
        var59.Text = EightQuery(4).ToString();
        var60.Text = EightQuery(0).ToString();
        var61.Text = NineQuery(1).ToString();
        var62.Text = NineQuery(2).ToString();
        var63.Text = NineQuery(5).ToString();
        var64.Text = NineQuery(3).ToString();
        var65.Text = NineQuery(4).ToString();
        var66.Text = NineQuery(0).ToString();
    }

    protected void Export_Click(object sender, EventArgs e)
    {
        fileIF.Attributes.Add("src", "CSVDorHandler.ashx?month="+ _monthToFilter.ToString() +"&year=" + _yearToFilter.ToString());
    }

    protected void SubmitQuery(object sender, EventArgs e) 
    {
        LoadVarData();
    }
}
