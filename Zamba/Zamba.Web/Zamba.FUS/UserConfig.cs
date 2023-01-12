using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Zamba.Servers;

namespace ZambaWeb.RestApi.Controllers.Class
{
    public class UserConfig
    {

        public UserConfig()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }

        }
        public void SetDefaultView(long UserId,string view)
        {
            try
            {
                var select = "insert into ZUserConfig values('" + UserId + "', 'DefaultWebView', '" + view + "', 1)";
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, select);
            }
            
            catch(Exception e)
            {

                throw e;
            }

        }

        public void UpdateDefaultView(long UserId, string view)
        {
            try
            {
                if (Server.isOracle)
                {

                    var select = "update ZUserConfig set C_Value = '" + view + "' where C_Userid = '" + UserId + "' and C_name = 'DefaultWebView'";
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, select);
                }

                else {

                    var select = "update ZUserConfig set Value = '" + view + "' where Userid = '" + UserId + "' and name = 'DefaultWebView'";
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, select);
                }

                
            }

            catch (Exception e)
            {
                throw e;
            }

        }

        public string GetDefaultView(long UserId)
        {
            try
            {
                var select = "";
                var Data = "";
                if (Server.isOracle)
                {
                    select = "select C_value from ZUserConfig where C_UserId = '" + UserId + "' and C_name = 'DefaultWebView'";
                    DataSet response = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, select);
                    Data = response.Tables[0].Rows[0]["C_value"].ToString();

                }
                else
                {
                    select = "select value from ZUserConfig where UserId = '" + UserId + "' and name = 'DefaultWebView'";
                    DataSet response = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, select);
                    Data = response.Tables[0].Rows[0]["value"].ToString();
                }

                return Data;
            }
            catch
            {
                return "";
            }
        }

    }
}