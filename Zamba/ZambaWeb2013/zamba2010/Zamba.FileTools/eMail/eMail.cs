using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spire.Email;
using Spire.Email.IMap;
using System.Net;
using System.Collections.ObjectModel;
using Zamba.Core;
using Zamba.FUS;
using System.Reflection;
using System.IO;
using Zamba.FileTools;
using Zamba.FileTools.eMail;
using System.Data;
using MsgKit.Enums;

namespace Zamba.SpireTools.SpireT
{
    public class eMail : ISpireEmailTools
    {
        public string ReadInBox()
        {
            
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                #region Codigo Original
                //Create an IMAP client
                ImapClient imap = new ImapClient();

                // Set host, username, password etc. for the client
                imap.Host = UP.getValue("ExchangeServer", UPSections.Mail, "10.6.110.18");
                imap.Port = int.Parse(UP.getValue("ExchangeServerPort", UPSections.Mail, "143"));
                imap.Username = UP.getValue("ExchangeServerUser", UPSections.Mail, @"pseguros.com\bpmt");
                imap.Password = UP.getValue("ExchangeServerPassword", UPSections.Mail, "Agosto2018");
                imap.ConnectionProtocols = ConnectionProtocols.Ssl;

                //Connect the server
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                //System.Net.Security.RemoteCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback() 
                imap.Connect();

                //Select Inbox folder
                imap.Select("Inbox");

                //Get the first message by its sequence number
                MailMessage message = imap.GetFullMessage(1);

                //Parse the message
                Console.WriteLine("------------------ HEADERS ---------------");
                Console.WriteLine("From   : " + message.From.ToString());
                Console.WriteLine("To     : " + message.To.ToString());
                Console.WriteLine("Date   : " + message.Date.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine("Subject: " + message.Subject);
                Console.WriteLine("------------------- BODY -----------------");
                Console.WriteLine(message.BodyText);
                Console.WriteLine("------------------- END ------------------");

                //Save the message to disk using its subject as file name
                message.Save(message.Subject + ".eml", MailMessageFormat.Eml);

                Console.WriteLine("Message Saved.");

                //Console.ReadKey();
                var js = JsonConvert.SerializeObject(message);
                return js;
                #endregion
           
        }






        private List<ImapMessageDTO> GetMails_WithRules(Dictionary<string, string> Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de GetMails_WithRules:");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "- Parametros: " + Params.ToString());

                //DATO HARDCODEADO
                imapClient.Select(Params["Folder"]);
                List<ImapMessageDTO> List_ImapMessageDTO = new List<ImapMessageDTO>();

                if (bool.Parse(Params["Todos"]))
                {
                    foreach (ImapMessage item in imapClient.GetAllMessageHeaders())
                    {
                        if (Val_ParamsToGetMails(Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
                    }
                }
                else if (bool.Parse(Params["Filtrado"]))
                {
                    foreach (ImapMessage item in FilterFolderSelected(Params["Filter_Field"], Params["Filter_Value"]))
                    {
                        if (Val_ParamsToGetMails(Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
                    }
                }
                else
                {
                    throw new Exception("Ocurrio un error en los parametros.s");
                }

                return List_ImapMessageDTO;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "- Parametros: " + Params.ToString());
                throw ;
            }
        }

        //private String GetMails_WithRules(ImapClient imapClient, Dictionary<string, string> Params)
        private List<ImapMessageDTO> GetMails_WithRules(DTOObjectImap Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de metodo de filtrado.");

                imapClient.Select(Params.Carpeta);
                List<ImapMessageDTO> List_ImapMessageDTO = new List<ImapMessageDTO>();

                if (Convert.ToInt32(Params.Filtrado.ToString()) == 0)
                {
                    foreach (ImapMessage item in imapClient.GetAllMessageHeaders())
                    {
                        if (Val_ParamsToGetMails(Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
                    }
                }
                else if (Convert.ToInt32(Params.Filtrado.ToString()) != 0)
                {
                    foreach (ImapMessage item in FilterFolderSelected(Params.Filtro_campo, Params.Filtro_valor))
                    {
                        if (Val_ParamsToGetMails(Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
                    }
                }
                else
                {
                    throw new Exception("Ocurrio un error en los parametros de filtrado.");
                }

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se han filtrado " + List_ImapMessageDTO.Count + " correo/s.");

                return List_ImapMessageDTO;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw;
            }
        }

        private bool Val_ParamsToGetMails(Dictionary<string, string> Params, ImapMessage item)
        {

            if (Params.ContainsKey("TrashMails") != false)
            {
                if (bool.Parse(Params["NewEmails"]) == true)
                {
                    if (RecentMailValidation(item.Date.Ticks))
                    {
                        if (bool.Parse(Params["RecentEmails"]) == true)
                        {
                            if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
                else
                {
                    if (bool.Parse(Params["RecentEmails"]) == true)
                    {
                        if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return true;
                    }
                }

            }
            else
            {
                if (bool.Parse(Params["RecentEmails"]) == true)
                {
                    if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                    {
                        return true;
                    }

                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private bool Val_ParamsToGetMails(DTOObjectImap Params, ImapMessage item)
        {
            try
            {
                if (Convert.ToInt32(Params.Filtro_noleidos.ToString()) == 1)
                {
                    if (RecentMailValidation(item.Date.Ticks))
                    {
                        if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
                        {
                            if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
                else
                {
                    if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
                    {
                        if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return true;
                    }
                }

                if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
                {
                    if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
                    {
                        return true;
                    }

                }
                else
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw;
            }
        }

        private bool RecentMailValidation(long Ticks)
        {
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

            if (Ticks > long.Parse(UP.getValue("LastDateObtained", UPSections.Mail, "0")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ConnectToExchange(Dictionary<string, string> Params)
        {
            try
            {
                #region Parametros de Conexion
                string Host = Params["Host"];
                int Puerto = int.Parse(Params["Port"]);
                string NomUsuario = Params["UserName"];
                string Contraseña = Params["UserPass"];
                ConnectionProtocols Protocolo_conexion = (ConnectionProtocols)Enum.Parse(typeof(ConnectionProtocols), Params["ProtCon"]);
                #endregion

                if (Params.ContainsKey("Cert"))
                {
                    if (!bool.Parse(Params["Cert"]))
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                }

                imapClient = new ImapClient(Host, Puerto, NomUsuario, Contraseña, Protocolo_conexion);

                imapClient.Connect();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");


            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Fallo la conexion al Exchange:" + ex.Message.ToString());
                throw ;
            }
        }

        ImapClient imapClient = null;
        public void ConnectToExchange(IDTOObjectImap Params)
        {
            try
            {
                #region Parametros de Conexion
                string Host = Params.Direccion_servidor;
                int Puerto = Convert.ToInt32(Params.Puerto);
                string NomUsuario = Params.Nombre_usuario;
                string Contraseña = Params.Password;
                ConnectionProtocols Protocolo_conexion = (ConnectionProtocols)Enum.Parse(typeof(ConnectionProtocols), Params.Protocolo);
                #endregion

                //if (!bool.Parse(Params["Cert"]))
                //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                imapClient = new ImapClient(Host, Puerto, NomUsuario, Contraseña, Protocolo_conexion);

                imapClient.Connect();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Fallo la conexion al Exchange:" + ex.Message.ToString());
                throw ;
            }
        }

        /// <summary>
        /// Determina si el rango entre Fecha y Hora del servidor y la Fecha del correo recibido en la casilla son validos.
        /// </summary>
        /// <param name="Dias">Rango en dias en el que se considera un correo como reciente.</param>
        /// <param name="FechaACtual">Fecha actual.</param>
        /// <param name="FechaDelCorreo">Fecha del correo recibido.</param>
        /// <returns></returns>
        private bool TEST_RangodeTiempo(int Dias, DateTime FechaACtual, DateTime FechaDelCorreo)
        {
            return ((int)(FechaACtual - FechaDelCorreo).Days) < Dias ? true : false;
        }

        private ImapMessageCollection FilterFolderSelected(string Campo, string Valor)
        {
            return imapClient.Search("'" + Campo + "' " + "Contains" + " '" + Valor + "'");
        }

        private AttachmentCollection ExtraerAdjuntos(string UniqueId)
        {
            return imapClient.GetFullMessage(UniqueId).Attachments;
        }

        public void saveEmail(string From, string eMailTo, string Subject, string Body, List<attach> Attachments, string TemporaryPath) {


            try
            {
                MailMessage mail = new MailMessage(From, eMailTo);

                mail.Subject = Subject;
                mail.BodyHtml = Body;

                //foreach (Attachment item in eMail.Attachments)
                //{
                //    mail.Attachments.Add(item);
                //}

                foreach (attach item in Attachments)
                {
                    Attachment dto = new Attachment(item.Data, item.FileName);

                    dto.Data = item.Data;
                    dto.ContentId = item.ContentId;
                    dto.ContentType = new Spire.Email.ContentType();
                    dto.ContentType.Boundary = item.ContentType.Boundary;
                    dto.ContentType.CharSet = item.ContentType.CharSet;
                    dto.ContentType.MediaType = item.ContentType.MediaType;
                    dto.ContentType.Name = item.ContentType.Name;


                    dto.DispositionType = item.DispositionType;
                    dto.FileName = item.FileName;
                    dto.Size = item.Size;


                    mail.Attachments.Add(dto);
                }

                mail.Save(TemporaryPath, MailMessageFormat.Msg);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw new Exception(ex.ToString());
            }


        }

        public void saveEmailWithKit(string From, string eMailcc,string eMailcco, string eMailTo, string Subject, string Body, DateTime sendOn, List<attach> Attachments, string TemporaryPath)
        {


            try
            {
                using (var email = new MsgKit.Email(
                new MsgKit.Sender(From, string.Empty),
                Subject,
                false,
                false))
                {
                    email.Recipients.AddTo(eMailTo);
                    email.Recipients.AddCc(eMailcc);
                    email.Recipients.AddBcc(eMailcco);
                    email.Subject = Subject;
                    email.BodyText = Body;
                    email.BodyHtml = Body;
                    email.SentOn = sendOn.ToUniversalTime();

                    

                    email.IconIndex = MessageIconIndex.ReceiptMail;
                    foreach (attach item in Attachments)
                    {
                        //Debe existir en esta ruta el archivo                       
                        email.Attachments.Add(item.FileName);
                        //Esto seria para las imagenes embebidas
                        //email.Attachments.Add("Images\\tinkerbell.jpg", -1, true, "tinkerbell.jpg");
                    }
                    email.Save(TemporaryPath);
                }
              
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw new Exception(ex.ToString());
            }


        }

        public List<IListEmail> GetEMailsFromServer(Dictionary<string, string> Dic_paramRequest)
        {
            try
            {
                //Instanciar el ImapClient
                ConnectToExchange(Dic_paramRequest);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo correos de '" + Dic_paramRequest["Folder"] + "'...");
                List<ImapMessageDTO> ListaDeEmails = GetMails_WithRules(Dic_paramRequest);

                if (bool.Parse(Dic_paramRequest["NewEmails"]))
                {
                    Zamba.Core.UserPreferences UP = new Core.UserPreferences();
                    if (ListaDeEmails.Count > 0)
                        UserPreferences.setValue("LastDateObtained", DateTime.Now.Ticks.ToString(), Zamba.UPSections.Mail);
                }


                List<IListEmail> listEmail = new List<IListEmail>();
                foreach (var item in ListaDeEmails)
                {
                    listEmail.Add(new ListEmail { UniqueId = item.UniqueId, Date = item.Date, Sender = item.Sender, Subject = item.Subject });
                }

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtuvieron " + listEmail.Count + " correos obtenidos del servidor.");
                return listEmail;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "Hubo un error al intentar obtener los correos de la carpeta '" + Dic_paramRequest["Folder"] + "'.");
                throw ;
            }
        }

        public List<ImapMessageDTO> GetEmailsFromServer(DTOObjectImap Dic_paramRequest)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo correos de '" + Dic_paramRequest.Carpeta + "'...");
                List<ImapMessageDTO> ListaDeEmails = GetMails_WithRules(Dic_paramRequest);

                if (Convert.ToInt32(Dic_paramRequest.Filtro_noleidos.ToString()) == 1)
                {
                    if (ListaDeEmails.Count > 0)
                        UserPreferences.setValue("LastDateObtained", DateTime.Now.Ticks.ToString(), Zamba.UPSections.Mail);
                }

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Correos obtenidos del servidor.");
                return ListaDeEmails;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "Hubo un error al intentar obtener los correos de la carpeta '" + Dic_paramRequest.Carpeta + "'.");
                throw ;
            }
        }



        public void InsertEmailsInZamba(List<IDTOObjectImap> imapProcessList, Object rb)
        {
            //Agregar trace log
            IResults_Business RB = (IResults_Business)rb;

            foreach (DTOObjectImap imapProcess in imapProcessList)
            {
                if (imapProcess.Is_Active != 0) { 
                
             
                string originalUserName = imapProcess.Nombre_usuario;

                if (imapProcess.GenericInbox != 0) {

                    //ZOptBusiness zopt = new ZOptBusiness();
                    string CasillaGenericaImap =  ZOptBusiness.GetValue("CasillaGenericaImap");
                    imapProcess.Nombre_usuario = CasillaGenericaImap + "\\" + imapProcess.Nombre_usuario + "\\" + imapProcess.Correo_electronico;

                }
                //Instanciar el ImapClient
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciada ejecucion de proceso con ID:" + imapProcess.Id_proceso + " Nombre del proceso:" + imapProcess.Nombre_proceso);
                ConnectToExchange(imapProcess);

                //IsProcessEnabledAndValid

                //GetEmails               
                List<ImapMessageDTO> ListaDeEmails = GetEmailsFromServer(imapProcess).OrderByDescending(i => i.SequenceNumber).ToList();

                imapProcess.Nombre_usuario = originalUserName;
                foreach (ImapMessageDTO eMail in ListaDeEmails)
                {
                    try
                    {
                        eMail.Body = imapClient.GetFullMessage(eMail.UniqueId).BodyHtml;

                        InsertResult eMailInsertResult = InsertResult.NoInsertado;
                        INewResult eMailNewResult = InsertEMail(imapProcess, eMail, ref eMailInsertResult, RB);

                        if (eMailInsertResult == InsertResult.Insertado)
                        {
                            if (imapProcess.Exportar_adjunto_por_separado == 1)
                            {
                                foreach (var attach in eMail.Attachments)
                                {
                                    InsertResult attachInsertResult = InsertResult.NoInsertado;
                                    INewResult attachNewResult = InsertEMail(imapProcess, eMail, ref attachInsertResult, RB);
                                }
                            }
                               MoveEmail(eMail.SequenceNumber, imapProcess.CarpetaDest);
                        }

                        eMail.Attachments.Clear();

                    }
                    catch (Exception ex)
                    {
                        //ZClass.raiseerror(ex);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo con uniqueID: " + eMail.UniqueId);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
                        continue;
                    }
                }
            }
            }
        }

        public  INewResult InsertEMail(DTOObjectImap imapProcess, ImapMessageDTO eMail, ref InsertResult insertResult, IResults_Business RB)
        {
            try
            {
                INewResult newResult;

                newResult = RB.GetNewNewResult(imapProcess.Entidad);

                if (imapProcess.Codigo_mail != 0)
                    newResult.get_GetIndexById(imapProcess.Codigo_mail).DataTemp = eMail.UniqueId;

                newResult.get_GetIndexById(imapProcess.Enviado_por).DataTemp = string.IsNullOrEmpty(eMail.From) ? eMail.From : eMail.Sender;
                newResult.get_GetIndexById(imapProcess.Para).DataTemp = eMail.To;

                if (imapProcess.Cc != 0)
                    newResult.get_GetIndexById(imapProcess.Cc).DataTemp = eMail.Cc;

                newResult.get_GetIndexById(imapProcess.Fecha).DataTemp = eMail.Date;
                newResult.get_GetIndexById(imapProcess.Asunto).DataTemp = eMail.Subject;

                //Guardar en temporal el archivo BodyText como HTML
                string TemporaryPath = Zamba.Membership.MembershipHelper.AppTempDir(@"\IMAPTemp\Imap").FullName + DateTime.Now.Ticks.ToString() + ".msg";

                //using (FileStream FS = new FileStream(TemporaryPath, FileMode.Create))
                //{
                //    using (StreamWriter SW = new StreamWriter(FS, Encoding.UTF8))
                //    {
                //        SW.Write(eMail.Body);
                //    }
                //}

                MailAddress addressFrom = new MailAddress(eMail.From.ToString());
                //System.Net.Mail.MailAddress addressFrom = new System.Net.Mail.MailAddress();
                if (eMail.Cc != "")
                {
                    MailAddress addressTo = new MailAddress(eMail.Cc);
                }




                MailMessage mail = new MailMessage(eMail.From, eMail.To.ToString());

                mail.Subject = eMail.Subject;
                mail.BodyHtml = eMail.Body;

                foreach (Attachment item in eMail.Attachments)
                {
                    mail.Attachments.Add(item);
                }


                mail.Save(TemporaryPath, MailMessageFormat.Msg);


                newResult.File = TemporaryPath;
                insertResult = RB.Insert(ref newResult, false, false, false, false, false, false, false);
                File.Delete(TemporaryPath);
                return newResult;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception(ex.ToString());
            }
        }

        public void MoveEmail(int sequenceNo, string folderName)
        {
            
                var a = imapClient.BeginCopy(sequenceNo, folderName, MoveCB);
                imapClient.EndCopy(a);

                var b = imapClient.BeginMarkAsDeleted(sequenceNo, DeleteCB);
                imapClient.EndMarkAsDeleted(b);
                imapClient.DeleteMarkedMessages();
           
        }

        private void MoveCB(IAsyncResult ar)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail copiado");
        }
        private void DeleteCB(IAsyncResult ar)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail Borrado");
        }

    }





    public class ImapMessageDTO : Zamba.FileTools.eMail.test.IImapMessageDTO
    {
        #region Atributos
        public string UniqueId { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsRecent { get; set; }
        public bool IsRead { get; set; }
        public long Size { get; set; }
        public int SequenceNumber { get; set; }
        public string Subject { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string ReplyTo { get; set; }

        public string Sender { get; set; }
        public string Body { get; set; }

        public string Date { get; set; }

        private int _Attachments_Count;
        public int Attachments_Count
        {
            get { return _Attachments_Count; }
            set { _Attachments_Count = value; }
        }


        private AttachmentCollection _Attachments;
        public AttachmentCollection Attachments
        {
            get
            {
                return _Attachments;
            }

            set
            {
                _Attachments = value;
                this._Attachments_Count = value.Count;
            }
        }

        #endregion

        public ImapMessageDTO(ImapMessage Obj, AttachmentCollection Attachs)
        {
            this.Cc = string.Concat(Obj.Cc.Select(n => n.Address + ", ")
                    .AsEnumerable())
                    .TrimEnd(' ')
                    .TrimEnd(',');

            this.Date = Obj.Date.ToString();
            this.From = Obj.From.Address;
            this.IsAnswered = Obj.IsAnswered;
            this.IsDeleted = Obj.IsDeleted;
            this.IsDraft = Obj.IsDraft;
            this.IsFlagged = Obj.IsFlagged;
            this.IsRecent = Obj.IsRecent;
            this.IsRead = Obj.IsRead;
            this.Size = Obj.Size;
            //this.ReplyTo = Obj.ReplyTo.Address; 
            this.Sender = Obj.Sender.Address;
            this.SequenceNumber = Obj.SequenceNumber;
            this.Subject = Obj.Subject;

            this.To = string.Concat(Obj.To.Select(n => n.Address + ", ")
                    .AsEnumerable())
                    .TrimEnd(' ')
                    .TrimEnd(',');

            this.UniqueId = Obj.UniqueId;

            this.Attachments = Attachs;
        }


    }
}
