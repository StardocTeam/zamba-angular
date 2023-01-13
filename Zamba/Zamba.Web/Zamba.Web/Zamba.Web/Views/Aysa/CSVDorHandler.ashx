<%@ WebHandler Language="C#" Class="CSVDorHandler" %>

using System;
using System.Web;
using Zamba.Core;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class CSVDorHandler : IHttpHandler {
    int _monthToFilter;
    int _yearToFilter;
    
    public void ProcessRequest (HttpContext context) {
        UTF8Encoding encoder;
        byte[] bytes;
        string csvTemplate, csvToReturn;
        
        try
        {
            if (!int.TryParse(context.Request.QueryString["month"], out _monthToFilter))
                _monthToFilter = DateTime.Now.Month;

            if (!int.TryParse(context.Request.QueryString["year"], out _yearToFilter))
                _yearToFilter = DateTime.Now.Year;

            csvTemplate = LoadCSVTemplete();
            csvToReturn = ReplaceVarData(csvTemplate);
            encoder = new UTF8Encoding();
            bytes = encoder.GetBytes(csvToReturn);
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.ContentType = "application/octet-stream";
            context.Response.AppendHeader("content-disposition", "inline; filename=TableroDor.csv");
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.OutputStream.Flush();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            bytes = null;
            encoder = null;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string LoadCSVTemplete()
    {
        ZOptBusiness zopt;
        FileInfo fi;
        
        try
        {
            zopt = new ZOptBusiness();
            fi = new FileInfo(zopt.GetValue("TabDORCSVTemplate"));

            if (fi.Exists)
            {
                string strToReturn = string.Empty;
                using (StreamReader sr = new StreamReader(fi.FullName))
                {
                    strToReturn = sr.ReadToEnd();
                    sr.Close();
                }
                return strToReturn;
            }
            else
                throw new Exception("Archivo template no encontrado");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            zopt = null;
            fi = null;
        }
    }
    
    private string ReplaceVarData(string Text)
    {
        if (string.IsNullOrEmpty(Text))
        {
            throw new Exception("No se ha cargado correctamente el template.");
        }

        string strToReturn;

        strToReturn = Text.Replace("var66", NineQuery(0).ToString());
        strToReturn = strToReturn.Replace("var65", NineQuery(4).ToString());
        strToReturn = strToReturn.Replace("var64", NineQuery(3).ToString());
        strToReturn = strToReturn.Replace("var63", NineQuery(5).ToString());
        strToReturn = strToReturn.Replace("var62", NineQuery(2).ToString());
        strToReturn = strToReturn.Replace("var61", NineQuery(1).ToString());
        strToReturn = strToReturn.Replace("var60", EightQuery(0).ToString());
        strToReturn = strToReturn.Replace("var59", EightQuery(4).ToString());
        strToReturn = strToReturn.Replace("var58", EightQuery(3).ToString());
        strToReturn = strToReturn.Replace("var57", EightQuery(5).ToString());
        strToReturn = strToReturn.Replace("var56", EightQuery(2).ToString());
        strToReturn = strToReturn.Replace("var55", EightQuery(1).ToString());
        strToReturn = strToReturn.Replace("var54", "0");
        strToReturn = strToReturn.Replace("var53", "0");
        strToReturn = strToReturn.Replace("var52", "0");
        strToReturn = strToReturn.Replace("var51", "0");
        strToReturn = strToReturn.Replace("var50", "0");
        strToReturn = strToReturn.Replace("var49", "0");
        strToReturn = strToReturn.Replace("var48", SixCQuery(0).ToString());
        strToReturn = strToReturn.Replace("var47", SixCQuery(4).ToString());
        strToReturn = strToReturn.Replace("var46", SixCQuery(3).ToString());
        strToReturn = strToReturn.Replace("var45", SixCQuery(5).ToString());
        strToReturn = strToReturn.Replace("var44", SixCQuery(2).ToString());
        strToReturn = strToReturn.Replace("var43", SixCQuery(1).ToString());
        strToReturn = strToReturn.Replace("var42", SixBQuery(0).ToString());
        strToReturn = strToReturn.Replace("var41", SixBQuery(4).ToString());
        strToReturn = strToReturn.Replace("var40", SixBQuery(3).ToString());
        strToReturn = strToReturn.Replace("var39", SixBQuery(5).ToString());
        strToReturn = strToReturn.Replace("var38", SixBQuery(2).ToString());
        strToReturn = strToReturn.Replace("var37", SixBQuery(1).ToString());
        strToReturn = strToReturn.Replace("var36", SixAQuery(0).ToString());
        strToReturn = strToReturn.Replace("var35", SixAQuery(4).ToString());
        strToReturn = strToReturn.Replace("var34", SixAQuery(3).ToString());
        strToReturn = strToReturn.Replace("var33", SixAQuery(5).ToString());
        strToReturn = strToReturn.Replace("var32", SixAQuery(2).ToString());
        strToReturn = strToReturn.Replace("var31", SixAQuery(1).ToString());
        strToReturn = strToReturn.Replace("var30", FifthQuery(0).ToString());
        strToReturn = strToReturn.Replace("var29", FifthQuery(4).ToString());
        strToReturn = strToReturn.Replace("var28", FifthQuery(3).ToString());
        strToReturn = strToReturn.Replace("var27", FifthQuery(5).ToString());
        strToReturn = strToReturn.Replace("var26", FifthQuery(2).ToString());
        strToReturn = strToReturn.Replace("var25", FifthQuery(1).ToString());
        strToReturn = strToReturn.Replace("var24", FourthQuery(0).ToString());
        strToReturn = strToReturn.Replace("var23", FourthQuery(4).ToString());
        strToReturn = strToReturn.Replace("var22", FourthQuery(3).ToString());
        strToReturn = strToReturn.Replace("var21", FourthQuery(5).ToString());
        strToReturn = strToReturn.Replace("var20", FourthQuery(2).ToString());
        strToReturn = strToReturn.Replace("var19", FourthQuery(1).ToString());
        strToReturn = strToReturn.Replace("var18", ThirdQuery(0).ToString());
        strToReturn = strToReturn.Replace("var17", ThirdQuery(4).ToString());
        strToReturn = strToReturn.Replace("var16", ThirdQuery(3).ToString());
        strToReturn = strToReturn.Replace("var15", ThirdQuery(5).ToString());
        strToReturn = strToReturn.Replace("var14", ThirdQuery(2).ToString());
        strToReturn = strToReturn.Replace("var13", ThirdQuery(1).ToString());
        strToReturn = strToReturn.Replace("var12", SeccondQuery(0).ToString());
        strToReturn = strToReturn.Replace("var11", SeccondQuery(4).ToString());
        strToReturn = strToReturn.Replace("var10", SeccondQuery(3).ToString());
        strToReturn = strToReturn.Replace("var9", SeccondQuery(5).ToString());
        strToReturn = strToReturn.Replace("var8", SeccondQuery(2).ToString());
        strToReturn = strToReturn.Replace("var7", SeccondQuery(1).ToString());
        strToReturn = strToReturn.Replace("var6", FirstQuery(0).ToString());
        strToReturn = strToReturn.Replace("var5", FirstQuery(4).ToString());
        strToReturn = strToReturn.Replace("var4", FirstQuery(3).ToString());
        strToReturn = strToReturn.Replace("var3", FirstQuery(5).ToString());
        strToReturn = strToReturn.Replace("var2", FirstQuery(2).ToString());
        strToReturn = strToReturn.Replace("var1", FirstQuery(1).ToString());

        return strToReturn;
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
        StringBuilder sb;

        try
        {
            sb = new StringBuilder();
            sb.Append("select QueryContent ");
            sb.Append("from ZDORTableQuerys ");
            sb.Append("where QueryName = '");
            sb.Append(QueryName);
            sb.Append("'");

            return (string)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text, sb.ToString());
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return string.Empty;
        }
        finally
        {
            sb = null;
        }
    }

    private IDbDataParameter[] GetOptionsParams(int RegionCode)
    {
        SqlParameter pRegion = new SqlParameter("@RegionCode", RegionCode);
        SqlParameter pMonth = new SqlParameter("@Month", _monthToFilter);
        SqlParameter pYear = new SqlParameter("@Year", _yearToFilter);

        return new IDbDataParameter[] { pRegion,pMonth,pYear };
    }
}