using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using ZambaLink;
using Zamba.Servers;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Zamba.Link
{
    public class ConfValues
    {
        public string ZLinkURL { get; set; }
        public string ZLinkWS { get; set; }
        public string ZColl { get; set; }
        public string GlobalSearchURL { get; set; }
        public string ZambaWebRestApiURL { get; set; }
        public string ZLCollaborationServer { get; set; }
        public bool ZLenable { get; set; }
        public long ZLuserId { get; set; }
        public long ZLuserExtId { get; set; }
        public ConfValues()
        {
            try
            {
                ZLinkURL = GetZLinkUrl();
                ZLinkWS = GetZLinkWS();
                ZColl = ZOptBusiness.GetValue("ZColl");
                ZLenable = ZOptBusiness.GetValue("ZLenable").ToUpper() == "TRUE" ? true : false;
                GlobalSearchURL = ZOptBusiness.GetValue("GlobalSearchURL");
                ZambaWebRestApiURL = ZOptBusiness.GetValue("ZambaWebRestApiURL");              
                ZLCollaborationServer = ZOptBusiness.GetValue("ZLCollaborationServer");
                ZLuserId = Frm_ZLink.GetUser() == null ? 0 : Frm_ZLink.GetUser().ID;
                var userExtern = GetExtUserInfo(ZLCollaborationServer, GetIntUserInfo(ZLuserId, "Email"), "Id");
                if (userExtern == string.Empty)
                {
                    ZLuserExtId = 0;
                }
                else
                {
                    ZLuserExtId = int.Parse(userExtern);
                }



                               
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        public static string GetZLinkWS()
        {
            return ZOptBusiness.GetValue("ZLinkWS");
        }
        private DataTable GetIntUserDB(long zLuserId)
        {
            var ds = new DataTable();
            try
            {
                string query = "select * from chatusers where id=" + zLuserId;
                ds = Server.get_Con().ExecuteDataset(CommandType.Text, query).Tables[0];
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return ds;
        }

        private string GetIntUserInfo(long zLuserId, string field)
        {
            var dt = GetIntUserDB(zLuserId);
            return dt.Rows.Count == 0 ? string.Empty : dt.Rows[0].Field<string>(field);
        }
        public static string GetZLinkUrl()
        {
            if (!Frm_ZLink.IsZambaUser())
            {
                return (ZOptBusiness.GetValue("ZLinkWS") + "/collaboration/login");
            }
            else
            {
                return ZOptBusiness.GetValue("ZLinkURL");
            }
        }
        private string GetExtUserInfo(string url, string mail, string field)
        {
            var user = GetExtUserDB(url, mail);
            if (!(user == string.Empty))
            {
                return JObject.Parse(user)[field].ToString();
            }
            return string.Empty;

        }

        private string GetExtUserDB(string url, string mail)
        {
            var userStr = string.Empty;
            if (mail == string.Empty) return userStr;
            try
            {
                var client = new HttpClient();
                var response = client.GetAsync(url + "/restcollaboration/GetMyUserInfo?mail=" + mail).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    if (responseString == "\"\"") return string.Empty;
                    userStr = JObject.Parse(responseString)["User"].ToString();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return userStr;

        }
    }
}
