using System;
using System.Collections.Generic;
using System.ServiceModel;
using Zamba.Data;
using Zamba.DataExt.WSResults;

namespace Zamba.DataExt.WSResult.Consume
{
    public class WSResultsFactory: IDisposable
    {
        private static bool? useDefaultCredentials;
        private static string wsUrl;
        private Results _wsClient;

        /// <summary>
        /// Obtiene si usar o no credenciales por default
        /// </summary>
        public bool UseDefaultCredentials 
        {
            get
            {
                return (WSResultsFactory.useDefaultCredentials.HasValue)?WSResultsFactory.useDefaultCredentials.Value:false;
            }
        }

        /// <summary>
        /// Url del WS de results
        /// </summary>
        public string WSUrl
        {
            get
            {
                return wsUrl;
            }
        }

        public WSResultsFactory()
        {
            string sValuesGetted;
            ZOptFactory zoptF = new ZOptFactory();

            //Si no se seteo antes la configuracion de credenciales
            if (!useDefaultCredentials.HasValue)
	        {
                //Obtener y setear valor
                sValuesGetted = ZOptFactory.GetValue("ConsumeWSWithDefCred");
                useDefaultCredentials = (string.IsNullOrEmpty(sValuesGetted)) ? false : bool.Parse(sValuesGetted);
	        }

            //Si no se seteo la url del ws
            if (wsUrl == null)
            {
                //Obtener y setear el valor
                sValuesGetted = ZOptFactory.GetValue("WSResultsUrl");
                wsUrl = (string.IsNullOrEmpty(sValuesGetted))? string.Empty: sValuesGetted;
            }

            zoptF = null;
            _wsClient = null;
        }

        /// <summary>
        /// Limpia la configuracion de ws: url y si usar o no default credentials
        /// </summary>
        public static void ClearConfiguration()
        {
            useDefaultCredentials = null;
            wsUrl = null;
        }
        
        #region ClientInicialization

        /// <summary>
        /// Se instancia el ws, con la configuracion previamente seteada
        /// </summary>
        /// <returns></returns>
        private Results GetWSIntance()
        {
            Results ws = new Results();
            ws.Url = wsUrl;
            ws.PreAuthenticate = false;

            if (useDefaultCredentials.Value)
                ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

            return ws;
        }
        #endregion

        public byte[] ConsumeGetWebDocFile(Int64 docTypeId, Int64 docId, Int64 userID)
        {
            CheckWsClient();
            return _wsClient.GetWebDocFile(docTypeId, docId, userID);
        }

        public bool ConsumeCopyBlobToVolume(Int64 docId, Int64 docTypeId)
        {
            CheckWsClient();
            return _wsClient.CopyBlobToVolume(docId,docTypeId);
        }

        public bool ConsumeInsertDocFile(long docID, long docTypeId, byte[] file, string fileName, long userID)
        {
            CheckWsClient();
            return _wsClient.InsertDocFile(docID,docTypeId,file,fileName,userID);
        }

        public bool ConsumeInsertForumAttach(int messageID, byte[] file, Int64 userID, string fileName)
        {
            CheckWsClient();
            return _wsClient.InsertForumAttach(messageID,file,userID,fileName);
        }

        public byte[] ConsumeGetAttachFileByMessageIdAndName(int messageId, string fileName, Int64 userID)
        {
            CheckWsClient();
            return _wsClient.GetAttachFileByMessageIdAndName(messageId,fileName,userID);
        }

        public string[] ConsumeGetAttachsNamesByMessageId(int messageId, Int64 userId)
        {
            CheckWsClient();
            return _wsClient.GetAttachsNamesByMessageId(messageId, userId);
        }

        public bool ConsumeSaveMailHistory(string to, string cC, string cCO, string subject, string body, List<string> attachs, long docId, long docTypeID, long userID, string exportPath)
        {
            CheckWsClient();
            string[] atts = null;
            if (attachs != null)
            {
                atts = attachs.ToArray();
            }

            return _wsClient.SaveMailHistory(to, cC, cCO, subject, body, atts, docId, docTypeID, userID, exportPath);
        }

        public byte[] ConsumeGetMail(Int64 id, Int64 userId)
        {
            CheckWsClient();
            return _wsClient.GetMail(id,userId);
        }

        public void ConsumeSaveMessageFileBlob(Int64 id, byte[] file)
        {
            CheckWsClient();
            _wsClient.SaveMessageFileBlob(id, file);
        }

        public bool ConsumeZSendMail(string from,
                                                    string smtp,
                                                    string port,
                                                    string user,
                                                    string pass,
                                                    string to,
                                                    string cc,
                                                    string cco,
                                                    string subject,
                                                    string body,
                                                    List<string> attachs,
                                                    long userid,
                                                    byte[] originalFile,
                                                    string originalFileName,
                                                    bool enableSsl)
        {
            CheckWsClient();
            string[] atts = null;
            if (attachs != null)
            {
                atts = attachs.ToArray();
            }

            return _wsClient.ZSendMail(from, smtp, port, user, pass, to, cc, cco, subject, body, atts, userid, originalFile, originalFileName, enableSsl);
        }

        public bool ConsumeZSendMailWithAttaches(string from,
                                                    string smtp,
                                                    string port,
                                                    string user,
                                                    string pass,
                                                    string to,
                                                    string cc,
                                                    string cco,
                                                    string subject,
                                                    string body,
                                                    List<BlobDocument> attachs,
                                                    long userid,
                                                    bool enableSsl)
        {
            CheckWsClient();
            BlobDocument [] atts = null;
            if (attachs != null)
            {
                atts = attachs.ToArray();
            }

            return _wsClient.ZSendMailWithAttaches(from, smtp, port, user, pass, to, cc, cco, subject, body, atts, userid, enableSsl);
        }

        public string ConsumeGetConfigPath()
        {
            CheckWsClient();
            return _wsClient.GetConfigPath();
        }

        public string ConsumeGetStartUpPath()
        {
            CheckWsClient();
            return _wsClient.GetStartUpPath();
        }

        public string ConsumeGetAppTempPath()
        {
            CheckWsClient();
            return _wsClient.GetAppTempPath();
        }

        private void CheckWsClient()
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();
        }

        public void Dispose()
        {
            if (_wsClient != null)
            {
                _wsClient.Dispose();
                _wsClient = null;
            }
        }
    }
}
