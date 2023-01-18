using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers.Infoauto
{
    class Auth
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }

    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InfoAutoController : ApiController
    {
        string InfoAuto_Base_URL;
        string InfoAuto_Username;
        string InfoAuto_Password;
        string InfoAuto_EndpointToken;
        string InfoAuto_EndpointGetJson;
        string InfoAuto_FilePathSave;
        Auth Authentication;

        public InfoAutoController()
        {
            Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
            InfoAuto_Base_URL = zopt.GetValue("infoauto_base_url");
            InfoAuto_Username = zopt.GetValue("infoauto_user");
            InfoAuto_Password = zopt.GetValue("infoauto_password");
            InfoAuto_EndpointToken = zopt.GetValue("infoauto_endpoint_token");
            InfoAuto_EndpointGetJson = zopt.GetValue("infoauto_endpoint_getjson");
            InfoAuto_FilePathSave = zopt.GetValue("infoauto_endpoint_filepath");
        }
        [Route("api/InfoAutoServices/GetJsonCarList")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult GetJsonCarList()
        {
            Boolean ProcessOK = false;
            String ret = "";
            ProcessOK = GetAuhtenticationToken();
            if (ProcessOK)
                ProcessOK = GetJSONFile();
            if (ProcessOK)
                ret = "true";
            else
                ret = "false";
            return Ok(ret);
        }

        public Boolean GetAuhtenticationToken()
        {
            try
            {
                string LoginEncondedBase64 = Convert.ToBase64String((
                               InfoAuto_Username + ":" + InfoAuto_Password)
                               .ToCharArray()
                               .Select(c => (byte)c)
                               .ToArray()
                           );
                var httpClient = (HttpWebRequest)WebRequest.Create(new Uri(InfoAuto_Base_URL + InfoAuto_EndpointToken));
                httpClient.Headers.Add("Authorization", "Basic " + LoginEncondedBase64);
                httpClient.Accept = "application/json";
                httpClient.ContentType = "application/json";
                httpClient.Method = "POST";
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Get token service 'InfoAuto'");
                ASCIIEncoding encoding = new ASCIIEncoding();
                String parsedContent = "";
                Byte[] bytes = encoding.GetBytes(parsedContent);
                Stream BCHistorytream = httpClient.GetRequestStream();
                BCHistorytream.Write(bytes, 0, bytes.Length);
                BCHistorytream.Close();
                WebResponse response = null;
                Stream stream = null;
                response = httpClient.GetResponse();
                stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                Authentication = JsonConvert.DeserializeObject<Auth>(content);
                return true;
                ;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        public Boolean GetJSONFile()
        {
            try
            {
                var httpClient = (HttpWebRequest)WebRequest.Create(new Uri(InfoAuto_Base_URL + InfoAuto_EndpointGetJson));
                httpClient.Headers.Add("Authorization", "Bearer " + Authentication.access_token);
                httpClient.Headers.Add("Accept-Encoding", "gzip");
                httpClient.Accept = "application/json";
                httpClient.ContentType = "application/json";
                httpClient.Method = "GET";
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Get Json File service 'InfoAuto'");
                WebResponse response = null;
                response = httpClient.GetResponse();
                MemoryStream ms = new MemoryStream();
                response.GetResponseStream().CopyTo(ms);
                byte[] data = ms.ToArray();
                File.WriteAllBytes(InfoAuto_FilePathSave + "\\InfoAuto " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".json.zip", data);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }
    }
}