using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Zamba.Core;
using Zamba.Framework;

namespace Zamba.Core
{
    public class ConsumeServiceRestApi
    {
        public string CallServiceRestApi(string url, string Method, string JsonMessage)
        {
            HttpWebRequest webRequest;
            webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webRequest.Timeout = 700000;

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamada a rest api:" + "\n" + url + "\n" + Method + "\n" + JsonMessage);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = Method;
            if (Method == "POST")
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(JsonMessage);
                Stream NewStream = webRequest.GetRequestStream();
                NewStream.Write(bytes, 0, bytes.Length);
                NewStream.Close();
            }
            WebResponse HttpResponse;
            Stream ObjStream;
            HttpResponse = webRequest.GetResponse();
            ObjStream = HttpResponse.GetResponseStream();
            StreamReader ObjStreamReader = new StreamReader(ObjStream);
            string ContenidoRespuesta = ObjStreamReader.ReadToEnd();
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta rest api:" + ContenidoRespuesta);
            return ContenidoRespuesta;
        }

        public string CallServiceZambaRestApi(string Controller, string ServiceName, string Method, object Obj)
        {
            string JsonMessage = "";

            if (Method.ToLower() == "post")
                JsonMessage = JsonConvert.SerializeObject(Obj);

            string RestApi = System.Web.Configuration.WebConfigurationManager.AppSettings["RestApiUrl"];
            string Domain = HttpContext.Current.Request.Url.Host;

            //TODO: revisar si en la url figura api
            string Url = Domain + "/" + RestApi + "/api/" + Controller + "/" + ServiceName;

            string response = CallServiceRestApi(Url, Method, JsonMessage);

            return response;
        }

        public DocumentData GetDocumentData(long userId, string doctypeId, string docId, bool convertToPDf, bool MsgPreview, bool includeAttachs)
        {
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("DocId", docId);
            Params.Add("DocTypeId", doctypeId);
            Params.Add("includeAttachs", includeAttachs.ToString());
            Params.Add("MsgPreview", MsgPreview.ToString());
            Params.Add("convertToPDf", convertToPDf.ToString());

            genericRequest GRequest = new genericRequest()
            {
                UserId = userId,
                Params = Params
            };

            //TODO: revisar si en la url figura api        
            string JsonResponse = CallServiceZambaRestApi("b2b", "GetResultFileByDocId", "post", GRequest);
            DocumentData response = JsonConvert.DeserializeObject<DocumentData>(JsonResponse);

            return response;
        }
    }

    public class DocumentData
    {
        public string fileName { get; set; }
        public string ContentType { get; set; }
        public MsgData dataObject { get; set; }
        public byte[] data { get; set; }
        public byte[] thumbImage { get; set; }

        public string iframeID { get; set; }
    }

    public class MsgData
    {
        public string id { get; set; }
        public string body { get; set; }
        public string date { get; set; }
        public string subject { get; set; }
        public string from { get; set; }
        public bool isMsg { get; set; }
        public List<string> to { get; set; }
        public List<object> attachs { get; set; }

    }
}