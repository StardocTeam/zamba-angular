using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Data;
using Zamba.Servers;
using Zamba.Membership;
using System.Text;
using Zamba.Core;
using System.Data;
using Newtonsoft.Json.Linq;
using ZambaWeb.RestApi.Models;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Controllers
{
    public class ZssFactory
    {
        public ZssFactory()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        private string FormatDate(DateTime d)
        {
            return Server.get_Con().ConvertDateTime(d.ToString());
        }

        public void SetZssValues(Zss zss)
        {
            //var delete = "Delete From Zss where Token=" +z  ;
            var select = "SELECT count(1) FROM ZSS  " + (Zamba.Servers.Server.isSQLServer ? " WITH(NOLOCK) " : "") + "   WHERE USERID=" + zss.UserId;
            var insert = "INSERT INTO ZSS (TOKEN, USERID, CREATEDATE, TOKENEXPIREDATE, CONNECTIONID) VALUES('" + zss.Token + "'," + zss.UserId + "," + FormatDate(zss.CreateDate) + "," + FormatDate(zss.TokenExpireDate) + "," + zss.ConnectionId + ")";
            var update = "UPDATE ZSS SET TOKEN='" + zss.Token + "', CREATEDATE= " + FormatDate(zss.CreateDate) + ", TOKENEXPIREDATE=" + FormatDate(zss.TokenExpireDate) + " WHERE USERID= " + zss.UserId;

            //if (Server.isOracle)
            //{}
            //else
            //{
            bool isNewRow = Int64.Parse(Server.get_Con().ExecuteScalar(CommandType.Text, select).ToString()) == 0 ? true : false;
            var success = Server.get_Con().ExecuteNonQuery(CommandType.Text, isNewRow ? insert : update);
            //}

        }
        public JObject GetTokenIfIsValid(IUser user)
        {
            if (user == null)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Error: Obtencion de Token, el Usuario es NULO");
                return null;
            }
            else
            {
                JObject tokenJObject = GetZss(user);
                return tokenJObject;
            }
        }
        private JObject GetZss(IUser user)
        {
            String select = "SELECT * FROM ZSS  " + (Zamba.Servers.Server.isSQLServer ? " WITH(NOLOCK) " : "") + "   WHERE USERID = " + user.ID.ToString();
            DataSet ds;
            try
            {
                
                ds = Server.get_Con().ExecuteDataset(CommandType.Text, select);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    var accessToken = dt.Rows[0].Field<string>("Token");
                    DateTime expires = dt.Rows[0].Field<DateTime>("TokenExpireDate");
                    DateTime createDate = dt.Rows[0].Field<DateTime>("CreateDate");
                    var connectionId = dt.Rows[0]["ConnectionId"];

                    if (expires <= DateTime.Now)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsWarning, string.Format("Obtencion de Token: El Token Expiro: {0}", expires.ToString()));
                        return null;
                    }
                    else
                    {
                        Zss zss = new Zss();
                        zss.ConnectionId = int.Parse(connectionId.ToString());
                        zss.CreateDate = createDate;
                        zss.Token = accessToken;
                            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                        var tokenExpiration = TimeSpan.FromMinutes(Int16.Parse(UP.getValue("TokenExpireMinutes", Zamba.UPSections.WFService, 30, user.ID)));
                        zss.TokenExpireDate = DateTime.UtcNow.Add(tokenExpiration);
                        zss.UserId = user.ID;

                        SetZssValues(zss);
                    }


                    JObject tokenResponse = new JObject(
                                                new JProperty("userName", user.Name),
                                                new JProperty("access_token", accessToken),
                                                new JProperty("token_type", "bearer"),
                                                new JProperty("expires_in", expires.ToString()),
                                                new JProperty("issued", createDate.ToString()),
                                                new JProperty("connectionId", connectionId.ToString()),
                                                new JProperty("expiredate", expires.ToString()));
                    return tokenResponse;
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsWarning, "Obtencion de Token: No hay token existente.");

                    return null;
                }
                //}            

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }


        public void RemoveZss(long userid)
        {
            var select = "Delete FROM ZSS WHERE USERID=" + userid;

            var RemoveUser = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, select);

        }


        public bool ChekTokenInDatabase(int Userid, string token)
        {
            try
            {
                var select = $"SELECT Count(1) FROM ZSS  " + (Zamba.Servers.Server.isSQLServer ? " WITH(NOLOCK) " : "") + "   WHERE USERID={Userid} and TOKEN ='{token}'";
                object count = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, select);
                if (count != null && int.Parse(count.ToString()) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return false;
        }

    }
}