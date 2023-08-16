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
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, "Se inicia la llamada al servicio api del despachante");
            HttpWebRequest webRequest;
            webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webRequest.Timeout = 700000;

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamada a rest api:" + "\n" + url + "\n" + Method + "\n" + JsonMessage);
            webRequest.Accept = "*/*";
            webRequest.ContentType = "application/json";
            webRequest.Method = Method;
            webRequest.ServerCertificateValidationCallback = CertificateValidationCallBack;
            if (Method.ToUpper() == "POST")
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(JsonMessage);
                Stream NewStream = webRequest.GetRequestStream();
                NewStream.Write(bytes, 0, bytes.Length);
                NewStream.Close();
            }
            WebResponse HttpResponse;
            Stream ObjStream;
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, "Se obtiene la respuesta");
            HttpResponse = webRequest.GetResponse();
            ObjStream = HttpResponse.GetResponseStream();
            StreamReader ObjStreamReader = new StreamReader(ObjStream);
            string ContenidoRespuesta = ObjStreamReader.ReadToEnd();
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta rest api longitud: " + ContenidoRespuesta.Length);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Respuesta rest api: " + ContenidoRespuesta);
            return ContenidoRespuesta;
        }

        private static bool CertificateValidationCallBack(
   object sender,
   System.Security.Cryptography.X509Certificates.X509Certificate certificate,
   System.Security.Cryptography.X509Certificates.X509Chain chain,
   System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {


            return true;
            
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if (
                          (certificate.Subject == certificate.Issuer) &&
                          (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }


        public string CallServiceZambaRestApi(string Controller, string ServiceName, string Method, object Obj)
        {
            string JsonMessage = "";

            if (Method.ToLower() == "post")
                JsonMessage = JsonConvert.SerializeObject(Obj);

            string RestApi = System.Web.Configuration.WebConfigurationManager.AppSettings["RestApiUrl"];
            string Domain = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;

            //TODO: revisar si en la url figura api
            string Url = Domain + "/" + RestApi + "/api/" + Controller + "/" + ServiceName;
            string response = CallServiceRestApi(Url, Method, JsonMessage);
            return response;
        }

        public DocumentData GetDocumentData(long userId, string doctypeId, string docId, bool convertToPDf, bool MsgPreview, bool includeAttachs, string newPDFFile)
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