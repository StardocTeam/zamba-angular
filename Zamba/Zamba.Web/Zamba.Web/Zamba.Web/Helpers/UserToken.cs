using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Zamba.Core;

namespace Zamba.Web.Helpers
{
    public class UserToken
    {
        private static string ConcatParams(Dictionary<string, string> parameters)
        {
            bool FirstParam = true;
            StringBuilder Parametros = null;

            if (parameters != null)
            {
                Parametros = new StringBuilder();
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    Parametros.Append(FirstParam ? "" : "&");
                    Parametros.Append(param.Key + "=" + HttpUtility.UrlEncode(param.Value));
                    FirstParam = false;
                }
            }

            return Parametros == null ? string.Empty : Parametros.ToString();
        }

        public static string GetBearerToken(string userName, string password, string computerNameOrIp, string ServiceURL)
        {
            string responseString = string.Empty;
            try
            {
                //ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault(); //
              //  string url =  ZOptBusiness.GetValueOrDefault("ZambaWebRestApiURL", "http://localhost/zambaweb.Restapi/api") + "/account/login";
                //ConfigurationManager.AppSettings["RestApiUrl"]
                string url = ServiceURL + "/api/account/login";
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Login Web Service Url: " + url);
                var l = new LoginVM()
                {
                    UserName = userName,
                    Password = password,
                    ComputerNameOrIp = computerNameOrIp
                };
                var json = JsonConvert.SerializeObject(l);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Token URL:" + url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 240000;
                
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.ContentType = "application/json; charset=UTF-8";

                //request.ContentLength = arrData.Length;  //se quita ya que tira exception al publicar
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(arrData, 0, arrData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    responseString = (responseString.Trim('"')).TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    responseString = responseString.Replace("\\", "");
                }
              //  HttpContext.Current.Session["BearerToken"] = responseString;
              //  HttpContext.Current.Session["TokenExpires"] = DateTime.Now.AddDays(1).ToString();
                return responseString;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }
        public static IUser GetUserByToken(string token)
        {
            string responseString = string.Empty;
            try
            {
                string url = ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault(); // ZOptBusiness.GetValueOrDefault("ZambaWebRestApiURL", "http://localhost/zambaweb.Restapi/api") + "/account/login";

//                string url = ZOptBusiness.GetValueOrDefault("ZambaWebRestApiURL", "http://localhost/zambaweb.Restapi/api") + "/account/login";
              
                var json = JsonConvert.SerializeObject(token);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.ContentType = "application/json; charset=UTF-8";

                //request.ContentLength = arrData.Length;
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(arrData, 0, arrData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    responseString = (responseString.Trim('"')).TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    responseString = responseString.Replace("\\", "");
                }
              //  HttpContext.Current.Session["BearerToken"] = responseString;
              //  HttpContext.Current.Session["TokenExpires"] = DateTime.Now.AddDays(1).ToString();
                return null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }
    }
}