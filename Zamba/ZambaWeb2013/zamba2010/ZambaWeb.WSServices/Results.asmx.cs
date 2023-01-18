using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Zamba.Core;
using System.IO;
using Zamba.Services;
using System.Collections;
using System.Data;
using System.Text;
using System.Diagnostics;
using Zamba.Membership;
using System.Xml.Serialization;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace ZambaWeb.WSServices
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
   
    public class Results : System.Web.Services.WebService
    {

        public Results()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Z.RS");
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(22242, ClientType.Web);
                UB = null;
            }

        }

        #region Trace
        // <summary>
        // Activa los trace y configura los niveles dependiendo del UserConfig
        // </summary>
        // <history>
        //     [Tomás] - 04/06/2009 - Modified - Se modifica la forma de trabajar con los Trace
        // </history>
      

        #endregion
        [WebMethod]
        public byte[] GetWebDocFile(Int64 DocTypeId, Int64 DocId, Int64 UserID)
        {
            ZOptBusiness zoptb;
          
            IResult result;
            Results_Business RB = new Results_Business();
            UserBusiness UB = new UserBusiness();
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando GetWebDocFile con DocTypeId=" + DocTypeId.ToString()
                    + ", DocId=" + DocId.ToString() + ", UserId=" + UserID.ToString());

                zoptb = new ZOptBusiness();
               

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando el usuario");
                UB.ValidateLogIn(int.Parse(UserID.ToString()), ClientType.Web);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el result");
                result = RB.GetResult(DocId, DocTypeId, false);

                //volumen db
                if (result.Disk_Group_Id > 0 &&
                    (VolumesBusiness.GetVolumeType(result.Disk_Group_Id) == (int)VolumeType.DataBase ||
                    (!String.IsNullOrEmpty(zoptb.GetValue("ForceBlob")) && bool.Parse(zoptb.GetValue("ForceBlob")))))
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se buscará el documento en el volumen digital");
                    RB.LoadFileFromDB(ref result);

                    //si es un archivo viejo no esta codificado
                    if (result.EncodedFile == null)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha recuperado el documento digital. Se intentará recuperar desde el volumen físico.");

                        try
                        {
                            //se lo codifica e inserta en la base
                            result.EncodedFile = FileEncode.Encode(result.RealFullPath());

                            if (result.EncodedFile != null)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha recuperado el documento digital. Insertándolo en la base de datos.");
                                RB.InsertIntoDOCB((IResult)result);
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento digital insertado");
                            }
                            else 
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha recuperado el documento físico " + result.RealFullPath());
                                throw new ArgumentNullException("EncodedFile", "El resultado de codificar " + result.RealFullPath() + " es nulo."); 
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(new Exception(ex.ToString() + " " + ((result.EncodedFile == null) ? "Encoded file vacio. " + result.RealFullPath() : "Byte array lenght: " + result.EncodedFile.Length + " " + result.FullPath), ex));
                            return result.EncodedFile;
                        }
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha recuperado el documento digital");
                    }
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se buscará el documento en el volumen físico");
                    if (File.Exists(result.RealFullPath()))
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento físico encontrado. Comienza la serialización.");
                        result.EncodedFile = FileEncode.Encode(result.RealFullPath());
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento físico serializado");
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado el documento físico " + result.RealFullPath());
                    }
                }

                return result.EncodedFile;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                zoptb = null;
               
                result = null;
                RB = null;
            }

            return null;
        }

        [WebMethod]
        public bool CopyBlobToVolume(Int64 docId, Int64 docTypeId)
        {
            byte[] doc;
            IResult result;
            Results_Business RB = new Results_Business();
            try
            {
                //Se obtiene el documento del volumen de la base de datos
                doc = RB.LoadFileFromDB(docId, docTypeId);

                //Ruta del temporal
                result = RB.GetResult(docId, docTypeId, false);

                //Guarda el archivo directamente en el volumen físico
                FileEncode.Decode(result.RealFullPath(), doc);

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
            finally
            {
                doc = null;
                result = null;
                RB = null;
            }
        }

        [WebMethod]
        public bool InsertDocFile(long docID,long docTypeId, byte[] file, string fileName, long userID)
        {
            IResult res;
            IUser usr;
            SResult sRes = new SResult();

            try
            {
                res = sRes.GetResult(docID, docTypeId, true);
                usr = new SUsers().GetUser(userID);

                //Si el result y el usuario son validos
                if (res != null && usr != null)
                {
                    //Hace la insersion
                    sRes.InserDocFile(res, file, fileName);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException("Usuario o documento no valido. UsrID=" + userID + " docID=" + docID);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
            finally 
            {
                //Se liberan los recursos
                file = null;
                res = null;
                usr = null;
                sRes = null;
            }
        }

        [WebMethod]
        public bool InsertForumAttach(int MessageID, byte[] file, Int64 UserID, string fileName)
        {
            UserBusiness UB = new UserBusiness();
            try
            {
                SRights Rights = new SRights();
                UB.ValidateLogIn(int.Parse(UserID.ToString()), ClientType.Web);
                SForum sForum = new SForum();

                if (MessageID < 0 && !sForum.ValidateExistMessage(MessageID))
                {
                    throw new Exception("Id de mensaje incorrecto.");
                }

                if (file.Length < 0)
                {
                    throw new Exception("Archivo inválido");
                }
                ZOptBusiness zoptb = new ZOptBusiness();

                string maxAttachsSize = zoptb.GetValue("MaxLenghtForumAttach");
                Int64 maxSize;
                if (string.IsNullOrEmpty(maxAttachsSize))
                    maxSize = 11111111;
                else
                    maxSize = (Int64)Int64.Parse(maxAttachsSize) / 1024;

                if (sForum.ValidateExistAttach(MessageID))
                    sForum.InsertAttachInAExistRecord(MessageID, ref file, maxSize, fileName);
                else
                    sForum.InsertAttach(MessageID, ref file, maxSize, fileName);


                //Guardamos el archivo en volumen físico también.
                if (zoptb.GetValue("ServAdjuntosRuta") == null)
                    throw new Exception("No esta configurada la ruta de los adjuntos (ServAdjuntosRuta)");
                string serverPathForAttachs = zoptb.GetValue("ServAdjuntosRuta");

                zoptb = null;
                DirectoryInfo dInfo = new DirectoryInfo(serverPathForAttachs);
                if (string.IsNullOrEmpty(serverPathForAttachs) || !dInfo.Exists)
                {
                    throw new Exception("Directorio para archivos adjuntos inválido.");
                }

                DirectoryInfo folderDestiny = new DirectoryInfo(Path.Combine(serverPathForAttachs, MessageID.ToString()));
                if (!folderDestiny.Exists)
                {
                    folderDestiny.Create();
                }

                FileEncode.Decode(Path.Combine(folderDestiny.FullName, fileName), file);

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
            finally {
                UB = null;
            }
        }

        [WebMethod]
        public byte[] GetAttachFileByMessageIdAndName(int MessageId, string fileName, Int64 UserID)
        {
            UserBusiness UB = new UserBusiness();
            try
            {
                SRights Rights = new SRights();
                UB.ValidateLogIn(int.Parse(UserID.ToString()), ClientType.Web);
                SForum sForum = new SForum();

                if (MessageId < 0 && !sForum.ValidateExistMessage(MessageId))
                {
                    throw new Exception("Id de mensaje incorrecto.");
                }
                ZOptBusiness zoptb = new ZOptBusiness();
                string serverPathForAttachs = Path.Combine(zoptb.GetValue("ServAdjuntosRuta"), MessageId.ToString());
                zoptb = null;
                DirectoryInfo dInfo = new DirectoryInfo(serverPathForAttachs);
                if (string.IsNullOrEmpty(serverPathForAttachs) || !dInfo.Exists)
                {
                    throw new Exception("Directorio para archivos adjuntos inválido.");
                }

                byte[] returnValue = FileEncode.Encode(Path.Combine(serverPathForAttachs, fileName));

                if (returnValue == null || returnValue.Length == 0)
                {
                    throw new Exception("No se ha podido obtener el archivo:" + fileName);
                }

                ////ESTO ESTA MAL. ¿OBTIENE EL ADJUNTO Y LO VUELVE A INSERTAR?
                //string maxAttachsSize = zopt.GetValue("MaxLenghtForumAttach");
                //int maxSize;
                //if (string.IsNullOrEmpty(maxAttachsSize))
                //    maxSize = 11111111;
                //else
                //    maxSize = (int)int.Parse(maxAttachsSize) / 1024;
                //sForum.InsertAttach(MessageId, ref returnValue, maxSize, fileName);

                return returnValue;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
            finally { UB = null; }
        }

        [WebMethod]
        public string[] GetAttachsNamesByMessageId(int MessageId, Int64 UserId)
        {
            UserBusiness UB = new UserBusiness();
            try
            {
                SRights Rights = new SRights();
                UB.ValidateLogIn(int.Parse(UserId.ToString()), ClientType.Web);
                SForum sForum = new SForum();

                if (MessageId < 0 && !sForum.ValidateExistMessage(MessageId))
                {
                    throw new Exception("Id de mensaje incorrecto.");
                }

                DataTable tempTable = sForum.GetAttachsNames(MessageId);
                List<string> returnList = new List<string>();
                int rowCount = tempTable.Rows.Count;

                if (rowCount > 0)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        object fileName = tempTable.Rows[i][0];
                        if (fileName is DBNull || string.IsNullOrEmpty((string)fileName))
                            returnList.Add("ADJUNTO Nº" + (i + 1).ToString());
                        else
                            returnList.Add((string)fileName);
                    }

                    return returnList.ToArray();
                }

                return null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
            finally { UB = null; }
        }

        [WebMethod]
        public bool SaveMailHistory(string To,
                                    string CC,
                                    string CCO,
                                    string Subject,
                                    string Body,
                                    List<string> Attachs,
                                    long DocId,
                                    long DocTypeID,
                                    long UserID,
                                    string ExportPath)
        {
            try
            {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Guardando historial de mail con parámetros:");
                    sb.AppendLine("To: " + (string.IsNullOrEmpty(To) ? "nulo/vacio" : To));
                    sb.AppendLine("CC: " + (string.IsNullOrEmpty(CC) ? "nulo/vacio" : CC));
                    sb.AppendLine("CCO: " + (string.IsNullOrEmpty(CCO) ? "nulo/vacio" : CCO));
                    sb.AppendLine("Subject: " + (string.IsNullOrEmpty(Subject) ? "nulo/vacio" : Subject));
                    sb.AppendLine("Body: " + (string.IsNullOrEmpty(Body) ? "nulo/vacio" : Body));
                    sb.AppendLine("Cantidad Atachs: " + (Attachs == null ? "nulo" : Attachs.Count.ToString()));
                    sb.AppendLine("DocId: " + DocId.ToString());
                    sb.AppendLine("DocTypeId: " + DocTypeID.ToString());
                    sb.AppendLine("Userid: " + UserID.ToString());
                    sb.AppendLine("ExportPath: " + (string.IsNullOrEmpty(ExportPath) ? "nulo/vacio" : ExportPath));

                    ZTrace.WriteLineIf(ZTrace.IsInfo, sb.ToString());
                 
                MessagesBusiness.SaveHistory(To, CC, CCO, Subject, Body, Attachs, DocId, DocTypeID, UserID, ExportPath);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Historial de mails guardado exitosamente.");

                return true;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar el historial de mails: " + ex.Message);
                ZClass.raiseerror(ex);
                return false;
            }
        }

        [WebMethod]
        public byte[] GetMail(Int64 id, Int64 userId)
        {
            UserBusiness UB = new UserBusiness();
            try
            {
                //Valida la sesión

                UB.ValidateLogIn(int.Parse(userId.ToString()), ClientType.Web);


                byte[] file;
                SendMailConfig mail = MessagesBusiness.GetMessage(id);
                file = mail.OriginalDocument;

                if (file == null)
                {
                    ZOptBusiness zoptb = new ZOptBusiness();
                    string isBlob = zoptb.GetValue("UseBlobMails");
                    zoptb = null;

                    if (!String.IsNullOrEmpty(isBlob) && bool.Parse(isBlob))
                    {
                        file = MessagesBusiness.GetMessageFile(id);

                        //Verifica si el mail se encontrada codificado
                        //En caso de no estarlo se lo codifica y guarda en la base
                        if (file == null || file.Length == 0)
                        {
                            file = FileEncode.Encode(mail.OriginalDocumentFileName);
                            MessagesBusiness.SaveMessageFile(file, id);
                        }
                    }
                    else
                    {
                        file = FileEncode.Encode(mail.OriginalDocumentFileName);
                    }
                }

                return file;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
            finally { UB = null; }
        }

        [WebMethod]
        public bool SaveMessageFileBlob(Int64 id, byte[] file)
        {
            try
            {
                SMail smb = new SMail();
                smb.SaveMessageFile(file, id);
                smb = null;
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [WebMethod]
        public bool ZSendMail(string from,
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
            SRights Rights = new SRights();
            IUser validUser = null;
            UserBusiness UB = new UserBusiness();
            try
            {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Ingresando a envio de mail con los siguientes parámetros");
                    sb.AppendLine("From: " + (string.IsNullOrEmpty(from) ? "nulo/vacio" : from));
                    sb.AppendLine("To: " + (string.IsNullOrEmpty(to) ? "nulo/vacio" : to));
                    sb.AppendLine("CC: " + (string.IsNullOrEmpty(cc) ? "nulo/vacio" : cc));
                    sb.AppendLine("CCO: " + (string.IsNullOrEmpty(cco) ? "nulo/vacio" : cco));
                    sb.AppendLine("Subject: " + (string.IsNullOrEmpty(subject) ? "nulo/vacio" : subject));
                    sb.AppendLine("Cantidad Atachs: " + (attachs == null ? "nulo" : attachs.Count.ToString()));
                    sb.AppendLine("Userid: " + userid.ToString());
                    sb.AppendLine("smtp: " + (string.IsNullOrEmpty(smtp) ? "nulo/vacio" : smtp));
                    sb.AppendLine("port: " + (string.IsNullOrEmpty(port) ? "nulo/vacio" : port));
                    sb.AppendLine("user: " + (string.IsNullOrEmpty(user) ? "nulo/vacio" : user));
                    sb.AppendLine("pass: " + (string.IsNullOrEmpty(pass) ? "nulo/vacio" : pass));
                    sb.AppendLine("originalFileName: " + (string.IsNullOrEmpty(originalFileName) ? "nulo/vacio" : originalFileName));
                    sb.AppendLine("originalfile bytes: " + (originalFile == null ? "nulo/vacio" : originalFile.Length.ToString()));
                    sb.AppendLine("enableSsl: " + enableSsl.ToString());
                    sb.AppendLine("Body: " + (string.IsNullOrEmpty(body) ? "nulo/vacio" : body));
                    ZTrace.WriteLineIf(ZTrace.IsInfo, sb.ToString());

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando usuario: " + userid.ToString());
                validUser = UB.ValidateLogIn(userid, ClientType.Web);

                if (validUser != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando mail");

                    SendMailConfig mail = new SendMailConfig();
                    mail.MailType = MailTypes.NetMail;
                    mail.From = from;
                    mail.SMTPServer = smtp;
                    mail.Port = port;
                    mail.UserName = user;
                    mail.Password = pass;
                    mail.MailTo = to;
                    mail.Cc = cc;
                    mail.Cco = cco;
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.UserId = userid;
                    mail.OriginalDocument = originalFile;
                    mail.OriginalDocumentFileName = originalFileName;
                    mail.EnableSsl = enableSsl;
                    mail.AttachFileNames = attachs;
                    mail.UseWebService = false;
                    mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                    return MessagesBusiness.SendMail(mail);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar el usuario");
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error de envio de mail: " + ex.Message);
                ZClass.raiseerror(ex);
            }
            finally
            {
                if (validUser != null)
                {
                    validUser.Dispose();
                    validUser = null;
                }
                UB = null;
            }
            return false;
        }

        [WebMethod()]
        public bool ZSendMailWithAttaches(string from,
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
            SRights Rights = new SRights();
            IUser validUser = null;
            SMail sMail = null;
            SendMailConfig mail = null;
            UserBusiness UB = new UserBusiness();
            try
            {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Ingresando a envio de mail con los siguientes parámetros");
                    sb.AppendLine("From: " + (string.IsNullOrEmpty(from) ? "nulo/vacio" : from));
                    sb.AppendLine("To: " + (string.IsNullOrEmpty(to) ? "nulo/vacio" : to));
                    sb.AppendLine("CC: " + (string.IsNullOrEmpty(cc) ? "nulo/vacio" : cc));
                    sb.AppendLine("CCO: " + (string.IsNullOrEmpty(cco) ? "nulo/vacio" : cco));
                    sb.AppendLine("Subject: " + (string.IsNullOrEmpty(subject) ? "nulo/vacio" : subject));
                    sb.AppendLine("Body: " + (string.IsNullOrEmpty(body) ? "nulo/vacio" : body));
                    sb.AppendLine("Cantidad Atachs: " + (attachs == null ? "nulo" : attachs.Count.ToString()));
                    sb.AppendLine("Userid: " + userid.ToString());
                    sb.AppendLine("smtp: " + (string.IsNullOrEmpty(smtp) ? "nulo/vacio" : smtp));
                    sb.AppendLine("port: " + (string.IsNullOrEmpty(port) ? "nulo/vacio" : port));
                    sb.AppendLine("user: " + (string.IsNullOrEmpty(user) ? "nulo/vacio" : user));
                    sb.AppendLine("pass: " + (string.IsNullOrEmpty(pass) ? "nulo/vacio" : pass));
                    sb.AppendLine("enableSsl: " + enableSsl.ToString());
                    ZTrace.WriteLineIf(ZTrace.IsInfo, sb.ToString());

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando usuario: " + userid.ToString() );
                validUser = UB.ValidateLogIn(userid, ClientType.Web);
                if (validUser != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando mail");
                    sMail = new SMail();

                    mail = new SendMailConfig();
                    mail.MailType = MailTypes.NetMail;
                    mail.From = from;
                    mail.SMTPServer = smtp;
                    mail.Port = port;
                    mail.UserName = user;
                    mail.Password = pass;
                    mail.MailTo = to;
                    mail.Cc = cc;
                    mail.Cco = cco;
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.UserId = userid;
                    mail.EnableSsl = enableSsl;
                    mail.LoadAttaches(ref attachs);
                    mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                    return sMail.SendMail(mail);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar el usuario");
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error de envio de mail: " + ex.Message);
                ZClass.raiseerror(ex);
            }
            finally
            {
                if(mail!=null)
                {
                    mail.Dispose();
                    mail = null;
                }
                if (validUser != null)
                {
                    validUser.Dispose();
                    validUser = null;
                }
                sMail = null;
                UB = null;
            }
            return false;
        }

        [WebMethod]
        public string GetConfigPath()
        {
            return MembershipHelper.AppConfigPath;
        }

        [WebMethod]
        public string GetStartUpPath()
        {
            return MembershipHelper.StartUpPath;
        }

        [WebMethod]
        public string GetAppTempPath()
        {
            return MembershipHelper.AppTempPath;
        }

        [WebMethod]
        public FieldOptions GetZDynamicTable(String controlId, String dataSource, String showColumns,
                String filterFieldId, String editableColumns, String editableColumnsAttributes,
                String filterValues, String additionalValidationButton, String postAjaxFuncion, String tbody)
        {

            FormControlsController formControlConstroller = new FormControlsController();

          

            return formControlConstroller.GetZDynamicTable(controlId, dataSource, showColumns, filterFieldId,
                                                           editableColumns, editableColumnsAttributes, filterValues,
                                                           additionalValidationButton, postAjaxFuncion, tbody);

        }

          [WebMethod(EnableSession = true)]
        public String TestCompleteFormAsociates(Int64 TaskId, Int64 UserId)
        {
            UserBusiness UB = new UserBusiness();
            //Int64 TaskId = 1;
            //Int64 UserId = 1;
            UB.ValidateLogIn(UserId, ClientType.Service);
            UB = null;
            IResult Task = new Zamba.Services.STasks().GetTask(TaskId, UserId);
            IUser user = new Zamba.Services.SUsers().GetUser(UserId);

            //<table  class="tablesorter" id="zamba_zvar(Facturas_Globales)">
            //                <tbody id="zamba_rule_34438/Factura Global/zvar(GastoTaskID=10)">

            //<table id="zamba_zvar(DocumentacionFaltanteIngreso)" class="tablesorter">
            //               <tbody id="zamba_rule_46281/Ver/zvar(DocFaltante=5)">

            //<table id="zamba_associated_documents_26032" class="tablesorter">
            //           <tbody>
            //           </tbody>
            //       </table>
            string TableId = "zamba_associated_documents_26032";
            string TBodyId = ""; //"zamba_rule_0/Previsualizar/zvar(path=length-3)/prev§zamba_rule_6471/Responder/zvar(rutaDocumento=length-3)";
            Int64 AsociatedId = 26032;
            return new FormControlsController().GetAsociatedResults(TableId, TBodyId, Task, user, AsociatedId);
        }

          [WebMethod(EnableSession = true)]
          public String GetZAsociatedResults(Int64 TaskId, Int64 UserId, String ControlId, String TBodyId)
          {
            UserBusiness UB = new UserBusiness();
              UB.ValidateLogIn(UserId, ClientType.Service);
            UB = null;

              IResult Task = new Zamba.Services.STasks().GetTask(TaskId,UserId);
              IUser user = new Zamba.Services.SUsers().GetUser(UserId);
              Int64 AsociatedId = Int64.Parse(ControlId.Replace("ZAsociated_", ""));
              return new FormControlsController().GetAsociatedResults(ControlId, TBodyId, Task, user,AsociatedId);

          }
        [WebMethod]
        public String GetZVarValue(String ZvarName, Int64 TaskId)
        {
            if (ZvarName.Contains("zamba_asoc_")){
                return string.Empty;
            }
            else
            {
            Zamba.Core.VarsBusiness VB = new VarsBusiness();
            try
            {
                var VarValue = VB.GetVariableValue(ZvarName, TaskId);
                return VarValue;
            }
            catch (Exception ex)
            {
                                ZClass.raiseerror(ex);
                                return ex.ToString();
            }
            finally
            {
                VB = null;
            }
            }
        }
        [WebMethod]
        public string GetHardCodedJson()
        {
            List<Object> jsonList = new List<Object>();
            var obj = new
            {
                NroPropuesta = "5100769",
                NroInspeccion = "IPA-293006",
                NroDePoliza = "533294",
                FechaInspeccion = @" ""19\/08\/2014 12:00:00 p.m. ZW3"" ", 
                IdPropuesta = "20B33DA8D8804CC303257DCD0053FE66",
                IdInspeccion = "3E1115ACD5CA916003257D320059AE6F",
                LinkInspeccion = "https://www.google.com.ar/", 
                EstadoInspeccion = "Enviada"
            };

            for (int i = 0; i < 5; i++)
            {
                jsonList.Add(obj);
            }
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            return jsonSerialiser.Serialize(jsonList);
        }
    }
}