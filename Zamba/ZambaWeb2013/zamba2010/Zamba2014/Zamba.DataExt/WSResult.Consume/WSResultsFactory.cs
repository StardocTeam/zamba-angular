using System;
using System.Collections.Generic;
using System.ServiceModel;
using Zamba.Data;
using Zamba.DataExt.WSResults;

namespace Zamba.DataExt.WSResult.Consume
{
    public class WSResultsFactory: IDisposable
    {
        protected static bool ? _useDefaultCredentials;
        protected static string _wsUrl;
        private Results _wsClient;

        /// <summary>
        /// Obtiene si usar o no credenciales por default
        /// </summary>
        public bool UseDefaultCredentials 
        {
            get
            {
                return (WSResultsFactory._useDefaultCredentials.HasValue)?WSResultsFactory._useDefaultCredentials.Value:false;
            }
        }

        /// <summary>
        /// Url del WS de results
        /// </summary>
        public string WSUrl
        {
            get
            {
                return _wsUrl;
            }
        }

        public WSResultsFactory()
        {
            string sValuesGetted;
            ZOptFactory zoptF = new ZOptFactory();

            //Si no se seteo antes la configuracion de credenciales
            if (!_useDefaultCredentials.HasValue)
	        {
                //Obtener y setear valor
                sValuesGetted = zoptF.GetValue("ConsumeWSWithDefCred");
                _useDefaultCredentials = (string.IsNullOrEmpty(sValuesGetted)) ? false : bool.Parse(sValuesGetted);
	        }

            //Si no se seteo la url del ws
            if (_wsUrl == null)
            {
                //Obtener y setear el valor
                sValuesGetted = zoptF.GetValue("WSResultsUrl");
                _wsUrl = (string.IsNullOrEmpty(sValuesGetted))? string.Empty: sValuesGetted;
            }

            zoptF = null;
            _wsClient = null;
        }

        /// <summary>
        /// Limpia la configuracion de ws: url y si usar o no default credentials
        /// </summary>
        public static void ClearConfiguration()
        {
            _useDefaultCredentials = null;
            _wsUrl = null;
        }
        
        #region ClienInicialization

        /// <summary>
        /// Se instancia el ws, con la configuracion previamente seteada
        /// </summary>
        /// <returns></returns>
        private Results GetWSIntance()
        {
            Results ws = new Results();
            ws.Url = _wsUrl;
            ws.PreAuthenticate = false;

            if (_useDefaultCredentials.Value)
                ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

            return ws;
        }
        #endregion

        public byte[] ConsumeGetWebDocFile(Int64 docTypeId, Int64 docId, Int64 userID)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetWebDocFile(docTypeId, docId, userID);
        }

        public bool ConsumeCopyBlobToVolume(Int64 docId, Int64 docTypeId)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.CopyBlobToVolume(docId,docTypeId);
        }

        public bool ConsumeInsertDocFile(long docID, long docTypeId, byte[] file, string fileName, long userID)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.InsertDocFile(docID,docTypeId,file,fileName,userID);
        }

        public bool ConsumeInsertForumAttach(int messageID, byte[] file, Int64 userID, string fileName)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.InsertForumAttach(messageID,file,userID,fileName);
        }

        public byte[] ConsumeGetAttachFileByMessageIdAndName(int messageId, string fileName, Int64 userID)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetAttachFileByMessageIdAndName(messageId,fileName,userID);
        }

        public string[] ConsumeGetAttachsNamesByMessageId(int messageId, Int64 userId)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();
            
            return _wsClient.GetAttachsNamesByMessageId(messageId, userId);
        }

        public bool ConsumeSaveMailHistory(string to, string cC, string cCO, string subject, string body, List<string> attachs, long docId, long docTypeID, long userID, string exportPath)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            string[] atts = null;
            if (attachs != null)
            {
                atts = attachs.ToArray();
            }

            return _wsClient.SaveMailHistory(to, cC, cCO, subject, body, atts, docId, docTypeID, userID, exportPath);
        }

        public byte[] ConsumeGetMail(string url, Int64 userId)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetMail(url,userId);
        }

        public void ConsumeSaveMessageFileBlob(string url, byte[] file)
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            _wsClient.SaveMessageFileBlob(url, file);
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
            if (_wsClient == null)
                _wsClient = GetWSIntance();

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
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            BlobDocument [] atts = null;
            if (attachs != null)
            {
                atts = attachs.ToArray();
            }

            return _wsClient.ZSendMailWithAttaches(from, smtp, port, user, pass, to, cc, cco, subject, body, atts, userid, enableSsl);
        }

        public string ConsumeGetConfigPath()
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetConfigPath();
        }

        public string ConsumeGetStartUpPath()
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetStartUpPath();
        }

        public string ConsumeGetAppTempPath()
        {
            if (_wsClient == null)
                _wsClient = GetWSIntance();

            return _wsClient.GetAppTempPath();
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
