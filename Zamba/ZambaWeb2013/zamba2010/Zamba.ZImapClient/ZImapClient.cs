using MimeKit;
using MailKit.Net.Imap;
using System.IO;
using System;
using MailKit.Search;
using MailKit;
using MailKit.Security;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Zamba.Core;
using Zamba.FileTools;
using Zamba;

namespace EmailRetrievalAPI.Controllers
{
    public class ZImapClient //: ISpireEmailTools
    {
        public class ImapConfig
        {
            public string ImapServer = "nasa1mail.mmc.com";
            public int ImapPort = 143;
            public SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto;
            public string ImapUsername = "mgd\\eseleme\\pedidoscaucion@marsh.com";
            public string ImapPassword = "Julio2023";
            public string FolderName = "INBOX";
            public string ExportFolderPath = "exported";

        }

        public class ZMessage {
            public MimeMessage message;
            public UniqueId uniqueId;
            public HeaderList headerList;
        }

        public string RetrieveEmails(ImapConfig config)
        {

            List<string> log = new List<string>();

            try
            {

                // Configure the certificate validation callback to trust the certificate
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                using (var client = new ImapClient())
                {
                    // Ignore certificate validation for the IMAP connection
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    log.Add("Connect");
                    client.Connect(config.ImapServer, config.ImapPort, config.secureSocketOptions);

                    log.Add("Authenticate");
                    client.Authenticate(config.ImapUsername, config.ImapPassword);

                    log.Add("GetFolder");
                    var folder = client.GetFolder(config.FolderName);
                    folder.Open(FolderAccess.ReadWrite);

                    var uids = folder.Search(SearchQuery.Not(SearchQuery.Deleted));

                    if (!Directory.Exists(config.ExportFolderPath))
                        Directory.CreateDirectory(config.ExportFolderPath);

                    foreach (var uid in uids)
                    {
                        try
                        {

                            var message = folder.GetMessage(uid);

                            // Check if the email has been previously retrieved
                            var isPreviouslyRetrieved = IsPreviouslyRetrieved(uid.ToString());
                            if (isPreviouslyRetrieved)
                                continue;

                            // Create a local email file with .msg extension
                            var filePath = Path.Combine(config.ExportFolderPath, $"{uid}.eml");
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                message.WriteTo(FormatOptions.Default, stream);
                            }

                            IMailFolder newfolder = folder;
                            try
                            {
                                newfolder = folder.GetSubfolder("Exported");

                            }
                            catch (FolderNotFoundException)
                            {
                                folder.Create("Exported", true);
                                newfolder = folder.GetSubfolder("Exported");
                            }

                            // Move the email to the exported folder
                            folder.MoveTo(uid, newfolder);

                            log.Add($"Mail OK: {uid} - {message.Subject}");
                        }
                        catch (Exception ex)
                        {
                            log.Add($"Mail ERROR : {uid} - {ex.ToString()}");
                        }
                    }

                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                log.Add($"ERROR : {ex.ToString()}");
            }

            return string.Join(System.Environment.NewLine, log);
        }

        private bool IsPreviouslyRetrieved(string uid)
        {
            // Check if the email with the given UID has been previously retrieved
            // Implement your logic here (e.g., check a database or a file)

            return false;
        }


        //public string ReadInBox()
        //{
        //    try
        //    {
        //        UserPreferences UP = new UserPreferences();

        //        #region Codigo Original
        //        //Create an IMAP client
        //        ImapClient imap = new ImapClient();

        //        // Set host, username, password etc. for the client
        //        imap.Host = UP.getValue("ExchangeServer", UPSections.Mail, "10.6.110.18");
        //        imap.Port = int.Parse(UP.getValue("ExchangeServerPort", UPSections.Mail, "143"));
        //        imap.Username = UP.getValue("ExchangeServerUser", UPSections.Mail, @"pseguros.com\bpmt");
        //        imap.Password = UP.getValue("ExchangeServerPassword", UPSections.Mail, "Agosto2018");
        //        imap.ConnectionProtocols = ConnectionProtocols.Ssl;

        //        //Connect the server
        //        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        //        //System.Net.Security.RemoteCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback() 
        //        imap.Connect();

        //        //Select Inbox folder
        //        imap.Select("Inbox");

        //        //Get the first message by its sequence number
        //        MailMessage message = imap.GetFullMessage(1);

        //        //Parse the message
        //        Console.WriteLine("------------------ HEADERS ---------------");
        //        Console.WriteLine("From   : " + message.From.ToString());
        //        Console.WriteLine("To     : " + message.To.ToString());
        //        Console.WriteLine("Date   : " + message.Date.ToString(CultureInfo.InvariantCulture));
        //        Console.WriteLine("Subject: " + message.Subject);
        //        Console.WriteLine("------------------- BODY -----------------");
        //        Console.WriteLine(message.BodyText);
        //        Console.WriteLine("------------------- END ------------------");

        //        //Save the message to disk using its subject as file name
        //        message.Save(message.Subject + ".eml", MailMessageFormat.Eml);

        //        Console.WriteLine("Message Saved.");

        //        //Console.ReadKey();
        //        var js = JsonConvert.SerializeObject(message);
        //        return js;
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}






        //private List<ImapMessageDTO> GetMails_WithRules(Dictionary<string, string> Params)
        //{
        //    try
        //    {
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de GetMails_WithRules:");
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "- Parametros: " + Params.ToString());

        //        //DATO HARDCODEADO
        //        imapClient.Select(Params["Folder"]);
        //        List<ImapMessageDTO> List_ImapMessageDTO = new List<ImapMessageDTO>();

        //        if (bool.Parse(Params["Todos"]))
        //        {
        //            foreach (ImapMessage item in imapClient.GetAllMessageHeaders())
        //            {
        //                if (Val_ParamsToGetMails(Params, item))
        //                    List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
        //            }
        //        }
        //        else if (bool.Parse(Params["Filtrado"]))
        //        {
        //            foreach (ImapMessage item in FilterFolderSelected(Params["Filter_Field"], Params["Filter_Value"]))
        //            {
        //                if (Val_ParamsToGetMails(Params, item))
        //                    List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(item.UniqueId)));
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Ocurrio un error en los parametros.s");
        //        }

        //        return List_ImapMessageDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        ZTrace.WriteLineIf(ZTrace.IsError, "- Parametros: " + Params.ToString());
        //        throw ex;
        //    }
        //}

        //private String GetMails_WithRules(ImapClient imapClient, Dictionary<string, string> Params)
        private List<ZMessage> GetMailsFiltered(DTOObjectImap Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de metodo de filtrado.");

                var folder = imapClient.GetFolder(Params.Carpeta);
                folder.Open(FolderAccess.ReadWrite);

                List<ZMessage> List_ImapMessageDTO = new List<ZMessage>();

                if (Convert.ToInt32(Params.Filtrado.ToString()) == 0)
                {
                    var uids = folder.Search(SearchQuery.Not(SearchQuery.Deleted));  // Params.Filtrado.ToString()

                    foreach (UniqueId uid in uids)
                    {
                        //HeaderList header = folder.GetHeaders(uid);
                      MimeMessage message =  folder.GetMessage(uid);
                        ZMessage zmessage = new ZMessage();
                        zmessage.message = message;
                        zmessage.uniqueId = uid;

                        if (Val_ParamsToGetMails(Params, zmessage))
                            List_ImapMessageDTO.Add(zmessage);
                    }
                }
                else if (Convert.ToInt32(Params.Filtrado.ToString()) != 0)
                {
                    IList<UniqueId> IMAP_Colection = null;

                    try
                    {
                        IMAP_Colection = FilterFolderSelected(folder, Params.Filtro_campo, Params.Filtro_valor);
                    }
                    catch (Exception)
                    {
                    }

                    if (IMAP_Colection != null)
                    {
                        foreach (UniqueId uid in IMAP_Colection)
                        {
//                            HeaderList header = folder.GetHeaders(uid);
                            MimeMessage message = folder.GetMessage(uid);
                            ZMessage zmessage = new ZMessage();
                            zmessage.message = message;
                            zmessage.uniqueId = uid;
                            if (Val_ParamsToGetMails(Params, zmessage))
                                List_ImapMessageDTO.Add(zmessage);
                        }
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
                throw ex;
            }
        }

        //private bool Val_ParamsToGetMails(Dictionary<string, string> Params, ZMessage item)
        //{

        //    //if (Params.ContainsKey("TrashMails") != false)
        //    //{
        //    //    if (bool.Parse(Params["NewEmails"]) == true)
        //    //    {
        //    //        if (RecentMailValidation(item.Date.Ticks))
        //    //        {
        //    //            if (bool.Parse(Params["RecentEmails"]) == true)
        //    //            {
        //    //                if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
        //    //                {
        //    //                    return true;
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                return true;
        //    //            }

        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (bool.Parse(Params["RecentEmails"]) == true)
        //    //        {
        //    //            if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
        //    //            {
        //    //                return true;
        //    //            }

        //    //        }
        //    //        else
        //    //        {
        //    //            return true;
        //    //        }
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    if (bool.Parse(Params["RecentEmails"]) == true)
        //    //    {
        //    //        if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
        //    //        {
        //    //            return true;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return true;
        //    //    }
        //    //}

        //    //return false;

        //    return true;
        //}

        private bool Val_ParamsToGetMails(DTOObjectImap Params, ZMessage item)
        {
            //try
            //{
            //    if (Convert.ToInt32(Params.Filtro_noleidos.ToString()) == 1)
            //    {
            //        if (RecentMailValidation(item.Date.Ticks))
            //        {
            //            if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
            //            {
            //                if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
            //                {
            //                    return true;
            //                }
            //            }
            //            else
            //            {
            //                return true;
            //            }

            //        }
            //    }
            //    else
            //    {
            //        if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
            //        {
            //            if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
            //            {
            //                return true;
            //            }

            //        }
            //        else
            //        {
            //            return true;
            //        }
            //    }

            //    if (Convert.ToInt32(Params.Filtro_recientes.ToString()) == 1)
            //    {
            //        if (TEST_RangodeTiempo(3, DateTime.Now, item.Date))
            //        {
            //            return true;
            //        }

            //    }
            //    else
            //    {
            //        return true;
            //    }

            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
            //    throw;
            //}

            return true;
        }

        private bool RecentMailValidation(long Ticks)
        {
            UserPreferences UP = new UserPreferences();

            if (Ticks > long.Parse(UP.getValue("LastDateObtained", UPSections.Mail, "0")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void ConnectToExchange(Dictionary<string, string> Params)
        //{
        //    try
        //    {
        //        #region Parametros de Conexion
        //        string Host = Params["Host"];
        //        int Puerto = int.Parse(Params["Port"]);
        //        string NomUsuario = Params["UserName"];
        //        string Contraseña = Params["UserPass"];
        //        ConnectionProtocols Protocolo_conexion = (ConnectionProtocols)Enum.Parse(typeof(ConnectionProtocols), Params["ProtCon"]);
        //        #endregion

        //        if (Params.ContainsKey("Cert"))
        //        {
        //            if (!bool.Parse(Params["Cert"]))
        //                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //        }

        //        imapClient = new ImapClient(Host, Puerto, NomUsuario, Contraseña, Protocolo_conexion);
        //        imapClient.UseOAuth = false;
        //        imapClient.Connect();
        //        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");


        //    }
        //    catch (Exception ex)
        //    {
        //        ZTrace.WriteLineIf(ZTrace.IsError, "Fallo la conexion al Exchange:" + ex.Message.ToString());
        //        throw ex;
        //    }
        //}

        ImapClient imapClient = null;
        IMailFolder folder = null;

        public void ConnectToExchange(IDTOObjectImap Params)
        {
            try
            {
                #region Parametros de Conexion
                string Host = Params.Direccion_servidor;
                int Puerto = Convert.ToInt32(Params.Puerto);
                string NomUsuario = Params.Nombre_usuario;
                string Contraseña = Params.Password;
                SecureSocketOptions secureSocketOptions = (SecureSocketOptions)Enum.Parse(typeof(SecureSocketOptions), Params.Protocolo);
                #endregion

                // Configure the certificate validation callback to trust the certificate
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                imapClient = new ImapClient();
                // Ignore certificate validation for the IMAP connection
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                imapClient.Connect(Host, Puerto, secureSocketOptions);

                imapClient.Authenticate(NomUsuario, Contraseña);

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Fallo la conexion al Exchange:" + ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Determina si el rango entre Fecha y Hora del servidor y la Fecha del correo recibido en la casilla son validos.
        /// </summary>
        /// <param name="Dias">Rango en dias en el que se considera un correo como reciente.</param>
        /// <param name="FechaACtual">Fecha actual.</param>
        /// <param name="FechaDelCorreo">Fecha del correo recibido.</param>
        /// <returns></returns>
        //private bool TEST_RangodeTiempo(int Dias, DateTime FechaACtual, DateTime FechaDelCorreo)
        //{
        //    return ((int)(FechaACtual - FechaDelCorreo).Days) < Dias ? true : false;
        //}

        private IList<UniqueId> FilterFolderSelected(IMailFolder folder,string Campo, string Valor)
        {
            //return imapClient.Search("'" + Campo + "' " + "Contains" + " '" + Valor + "'");
            return folder.Search(SearchQuery.Not(SearchQuery.Deleted));

        }

        //private AttachmentCollection ExtraerAdjuntos(IMailFolder folder,UniqueId UniqueId)
        //{
        //    return folder.GetMessage(UniqueId).Attachments;
        //}


        //public List<IListEmail> GetEMailsFromServer(Dictionary<string, string> Dic_paramRequest)
        //{
        //    try
        //    {
        //        //Instanciar el ImapClient
        //        ConnectToExchange(Dic_paramRequest);

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo correos de '" + Dic_paramRequest["Folder"] + "'...");
        //        List<ImapMessageDTO> ListaDeEmails = GetMails_WithRules(Dic_paramRequest);

        //        if (bool.Parse(Dic_paramRequest["NewEmails"]))
        //        {
        //            if (ListaDeEmails.Count > 0)
        //                UserPreferences.setValue("LastDateObtained", DateTime.Now.Ticks.ToString(), Zamba.UPSections.Mail);
        //        }


        //        List<IListEmail> listEmail = new List<IListEmail>();
        //        foreach (var item in ListaDeEmails)
        //        {
        //            listEmail.Add(new ListEmail { UniqueId = item.UniqueId, Date = item.Date, Sender = item.Sender, Subject = item.Subject });
        //        }

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtuvieron " + listEmail.Count + " correos obtenidos del servidor.");
        //        return listEmail;
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        ZTrace.WriteLineIf(ZTrace.IsError, "Hubo un error al intentar obtener los correos de la carpeta '" + Dic_paramRequest["Folder"] + "'.");
        //        throw ex;
        //    }
        //}

        private List<ZMessage> GetEmailsFromServer(DTOObjectImap Dic_paramRequest)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo correos de '" + Dic_paramRequest.Carpeta + "'...");
                List<ZMessage> ListaDeEmails = GetMailsFiltered(Dic_paramRequest);

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
                throw ex;
            }
        }



        public void InsertEmailsInZamba(List<IDTOObjectImap> imapProcessList, Object rb)
        {
            //Agregar trace log
            IResults_Business RB = (IResults_Business)rb;

            foreach (DTOObjectImap imapProcess in imapProcessList)
            {
                if (imapProcess.Is_Active != 0)
                {
                    try
                    {
                        string originalUserName = imapProcess.Nombre_usuario;

                        if (imapProcess.GenericInbox != 0)
                        {
                            string CasillaGenericaImap = ZOptBusiness.GetValue("CasillaGenericaImap");
                            imapProcess.Nombre_usuario = CasillaGenericaImap + "\\" + imapProcess.Nombre_usuario + "\\" + imapProcess.Correo_electronico;
                        }

                        //Instanciar el ImapClient
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Iniciada ejecucion de proceso con ID:" + imapProcess.Id_proceso + " Nombre del proceso:" + imapProcess.Nombre_proceso);
                        ConnectToExchange(imapProcess);

                        //GetEmails               
                        List<ZMessage> ListaDeEmails = GetEmailsFromServer(imapProcess); //.OrderByDescending(i => i.SequenceNumber).ToList();

                        imapProcess.Nombre_usuario = originalUserName;

                        foreach (ZMessage eMail in ListaDeEmails)
                        {
                            try
                            {

                                if (imapProcess.CarpetaDest != null)
                                {

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando correo: UniqueId: " + eMail.uniqueId + " - SequenceNumber:" + eMail.message.MessageId);

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Asunto del correo en iteracion: " + eMail.message.Subject);

                                    string SubjectName = GetNewSubjectName(eMail);
                                    string TempMsgPath = GetNewFileName(Zamba.Membership.MembershipHelper.AppTempPath + "\\Imap\\Temp\\", SubjectName + ".eml");

                                    //Se obtiene mail completo.
                                    // MailMessage FullMessage = imapClient.GetMessage(eMail.UniqueId);

                                    //realizando remapeado de imagenes embebidas
                                    //setSrcFromBody(eMail, eMail);

                                    //Se guarda el archivo msg en el sistema.
                                    //SaveMsgFile(imapProcess, eMail, TempMsgPath);
                                    // Create a local email file with .msg extension
                                    using (var stream = System.IO.File.Create(TempMsgPath))
                                    {
                                        eMail.message.WriteTo(FormatOptions.Default, stream);
                                    }

                                    //Se inserta la tarea en el sistema.
                                    InsertResult eMailInsertResult = InsertResult.NoInsertado;
                                    INewResult eMailNewResult = InsertEMail(imapProcess, eMail, ref eMailInsertResult, RB, TempMsgPath);

                                    //Se mueve el correo dentro de la casilla.
                                    if (eMailInsertResult == InsertResult.Insertado)
                                    {
                                        if (imapProcess.Exportar_adjunto_por_separado == 1)
                                        {
                                            foreach (var attach in eMail.message.Attachments)
                                            {
                                                InsertResult attachInsertResult = InsertResult.NoInsertado;
                                                INewResult attachNewResult = InsertEMail(imapProcess, eMail, ref attachInsertResult, RB, TempMsgPath);
                                            }
                                        }

                                        MoveEmail(folder,eMail.uniqueId, imapProcess.CarpetaDest);
                                    }

                                   // eMail.message.Attachments.Clear();
                                }
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo: UniqueID: " + eMail.uniqueId + " - SequenceNumber:" + eMail.message.MessageId);
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto del correo iterado: " + eMail.message.Subject);
                                ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error en el proceso con ID: " + imapProcess.Id_proceso);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
                    }
                }
            }
        }

        private static string GetNewSubjectName(ZMessage eMail)
        {
            var SubjectName = eMail.message.Subject != "" ? eMail.message.Subject.ToString() : "SIN ASUNTO";

            int MaxNameLimit = 248;

            //Se delimita el largo del asunto para que sea valido como nombre de archivo.
            SubjectName = SubjectName.Length > MaxNameLimit ? SubjectName.Substring(0, MaxNameLimit) : SubjectName;

            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                SubjectName = SubjectName.Replace(c.ToString(), "");
            }

            SubjectName = FixNameLength(SubjectName, MaxNameLimit);
            return SubjectName;
        }

        private static string FixNameLength(string SubjectName, int MaxNameLimit)
        {
            int lengthtotal = (Zamba.Membership.MembershipHelper.AppTempPath + "\\Imap\\Temp").Length + SubjectName.Length;

            if (lengthtotal > MaxNameLimit)
            {
                SubjectName = SubjectName.Substring(0, SubjectName.Length - (lengthtotal - MaxNameLimit));
            }


            return SubjectName;
        }

        /// <summary>
        /// Cambia el src que contiene un CID por una base64 dentro del body del correo.
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="FullMessage"></param>
        //private void setSrcFromBody(MimeMessage eMail, MailMessage FullMessage)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se inicia el proceso de remapeado de imagenes embebidas.");
        //    List<int> ImgIndices = null;
        //    FileNamesDTO ObjectFileNamesDto = null;

        //    try
        //    {
        //        string[] ImageTags = System.Text.RegularExpressions.Regex.Split(FullMessage.Body, "<img", RegexOptions.IgnoreCase);

        //        if (ImageTags.Length > 1)
        //        {
        //            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron " + ImageTags.Length + " imagenes para procesar.");
        //            int FirstPosSrc = GetStringPositionFromBody(ImageTags[1], "src=");

        //            //Obtengo el valor del primer atributo SRC, en el, estara la palabra "cid:"
        //            string FirstSrcValue = ImageTags[1].Substring(FirstPosSrc + 5, ImageTags[1].Substring(FirstPosSrc + 5).IndexOf("\""));

        //            ImgIndices = GetPositionsSrcFromBody(FullMessage.BodyHtml, "<img");

        //            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando tipos de valores 'CID' predominantes...");
        //            if (FirstSrcValue.Contains('@'))
        //            {
        //                ZTrace.WriteLineIf(ZTrace.IsInfo, "CID tipo A");
        //                ObjectFileNamesDto = GetFileNamesFromBody(FullMessage, ImageTags, ImgIndices);
        //                ObjectFileNamesDto.ReverseLists();
        //            }
        //            else
        //            {
        //                ZTrace.WriteLineIf(ZTrace.IsInfo, "CID tipo B");
        //                ObjectFileNamesDto = GetFileNamesFromEmailObject(eMail, ImageTags, ImgIndices);
        //                ObjectFileNamesDto.ReverseLists();
        //            }

        //            if (ObjectFileNamesDto.CountLists() > 0)
        //                FullMessage.BodyHtml = ReplaceCidForSrc(eMail, FullMessage, ObjectFileNamesDto);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo: UniqueID: " + eMail.UniqueId + " - SequenceNumber:" + eMail.SequenceNumber);
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto del correo iterado: " + eMail.Subject);
        //        ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
        //    }
        //}

        /// <summary>
        /// Crea una lista de indices que se utilizaran para obtener imagenes del Cliente IMAP, siempre y cuando el formato de los CIDS sea algo parecido a: cid:HG347FHED
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="CidsIndices"></param>
        /// <param name="ImageTags"></param>
        /// <returns>Lista de indices en formato string.</returns>
        //private FileNamesDTO GetFileNamesFromEmailObject(ZMessage eMail, string[] ImageTags, List<int> CidsIndices)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los nombres de las imagenes a remapear...");
        //    List<string> ReorderedFileList = new List<string>();
        //    List<int> listImgIndices = new List<int>();
        //    int i = 0;

        //    foreach (string ImgItem in ImageTags)
        //    {
        //        int Cid = GetStringPositionFromBody(ImgItem, "cid:");
        //        int Alt = GetStringPositionFromBody(ImgItem, "alt=");
        //        int Src = GetStringPositionFromBody(ImgItem, "src=");

        //        //Valido que el elemento IMG tenga 

        //        if (Cid != 0)
        //        {
        //            if (Alt != 0)
        //            {
        //                string AltValue = ImgItem.Substring(Alt + 5, ImgItem.Substring(Alt + 5).IndexOf("\""));
        //                Boolean FileFound = false;
        //                Attachment Image;

        //                if (Uri.IsWellFormedUriString(AltValue, UriKind.Absolute))
        //                {
        //                    ReorderedFileList.Add(AltValue);
        //                    listImgIndices.Add(CidsIndices[i] + Src + 9);
        //                    FileFound = true;
        //                }
        //                else
        //                {
        //                    //Se Buscan las imagenes por incice
        //                    for (int j = 0; j < CidsIndices.Count; j++)
        //                    {
        //                        if (!ReorderedFileList.Contains((j + 2).ToString()))
        //                        {
        //                            try
        //                            {
        //                                Image = imapClient.GetAttachment(eMail.SequenceNumber, (j + 2).ToString());
        //                            }
        //                            catch (Exception)
        //                            {
        //                                Image = null;
        //                            }

        //                            if (Image != null && Image.ContentType.Name == AltValue)
        //                            {
        //                                ReorderedFileList.Add((j + 2).ToString());
        //                                listImgIndices.Add(CidsIndices[i] + Src + 9);
        //                                FileFound = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }

        //                if (!FileFound)
        //                {
        //                    try
        //                    {
        //                        //Ahora se buscan las imagenes por nombre
        //                        //Reducir numero de veces iterado? int i = ReorderedFileList.Count?? (Performance)
        //                        for (int j = 0; j < CidsIndices.Count; j++)
        //                        {
        //                            Image = imapClient.GetAttachment(eMail.SequenceNumber, AltValue);

        //                            if (Image.ContentType.Name == AltValue)
        //                            {
        //                                ReorderedFileList.Add(AltValue);
        //                                listImgIndices.Add(CidsIndices[i] + Src + 9);
        //                                FileFound = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ZTrace.WriteLineIf(ZTrace.IsError, "Exception no lanzada: " + ex.ToString());
        //                    }
        //                }

        //            }
        //            i++;
        //        }

        //    }


        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtuvieron " + ReorderedFileList.Count + " nombres de archivos efectivas.");
        //    return new FileNamesDTO() { Imgs = listImgIndices, FileNames = ReorderedFileList };
        //}

        /// <summary>
        /// Obtiene los nombres de las diferentes imagenes embebidas dentro del body de un corre, siempre y cuando el formato de los CIDS sea algo parecido a: cid:image001.png@01D93496.D22948C0
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="ImageTags"></param>
        /// <returns></returns>
        //private static FileNamesDTO GetFileNamesFromBody(MailMessage FullMessage, string[] ImageTags, List<int> ImgIndices)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los nombres de las imagenes a remapear...");
        //    List<string> ListEmbeddedImages = new List<string>();
        //    List<int> listImgIndices = new List<int>();
        //    int i = 0;

        //    foreach (string ImgItem in ImageTags)
        //    {
        //        int PosSrc = GetStringPositionFromBody(ImgItem, "src=");
        //        int PosAlt = GetStringPositionFromBody(ImgItem, "alt=");


        //        if (PosSrc != 0)
        //        {
        //            //Valida cuantos '@' hay en el nombre del archivo.
        //            //Se utiliza la sumatoria 9 ya que corresponde al largo de 'src="cid' que es donde se encuentra el valor del 'cid'.
        //            string[] FileNameSplited = ImgItem.Substring(PosSrc + 9, ImgItem.Substring(PosSrc + 9).IndexOf("\"")).Split('@');
        //            //Se utiliza la sumatoria 5 ya que corresponde al largo de 'src="' que es donde se encuentra el valor del atributo 'src' que contiene variantes del 'cid' o no.
        //            string FirstSrcValue = ImgItem.Substring(PosSrc + 5, ImgItem.Substring(PosSrc + 5).IndexOf("\""));

        //            //Valido que haya un CID dentro de la variable FileNameSplited;
        //            //(IMPORTANTE): Tambien se valida que el nombre no cuente con el siguiente formato:
        //            // "cid:part1.ba55C55W.33qw14Ld@LimonDentales.com" (proviene de firmas)
        //            // varias firmas no pueden ser encontradas dentro del Imap, por tanto se filtran momentaneamente.
        //            if (FirstSrcValue.Contains("cid:") && !(FirstSrcValue.Contains(".com") || FirstSrcValue.Contains(".com.ar")) && FirstSrcValue.Contains("."))
        //            {
        //                //Si el nombre del archivo tiene un '@', tomara lo que hay hasta el siguiente '@'; Si no hay ningun @, se tomara el nombre de otras formas.
        //                if (FileNameSplited.Length == 2 || FileNameSplited.First().Contains("~"))
        //                {
        //                    ListEmbeddedImages.Add(FileNameSplited.First());
        //                    listImgIndices.Add(ImgIndices[i] + PosSrc + 9);
        //                }
        //                else if (FileNameSplited.Length > 2) //Si hay mas de un '@', tomara el nombre del archivo hasta el ultimo '@' del valor "CID:".    
        //                {
        //                    string FileName = "";

        //                    foreach (string itemString in FileNameSplited)
        //                    {
        //                        if (itemString == FileNameSplited.First())
        //                            FileName += itemString;
        //                        else if (itemString != FileNameSplited.Last())
        //                            FileName += "@" + itemString;
        //                    }

        //                    ListEmbeddedImages.Add(FileName);
        //                    listImgIndices.Add(ImgIndices[i] + PosSrc + 9);
        //                }
        //            }

        //            //Incremento 'i' para que vaya a la par de las iteraciones de 'ImageTags' sin importar los resultados de las condiciones.
        //            i++;
        //        }
        //    }

        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtuvieron " + ListEmbeddedImages.Count + " nombres de archivos efectivas.");
        //    return new FileNamesDTO() { Imgs = listImgIndices, FileNames = ListEmbeddedImages };
        //}

        //public class FileNamesDTO
        //{
        //    public List<int> Imgs { get; set; }
        //    public List<string> FileNames { get; set; }

        //    public void ReverseLists()
        //    {
        //        this.Imgs.Reverse();
        //        this.FileNames.Reverse();
        //    }

        //    public int CountLists()
        //    {
        //        if (this.Imgs.Count == this.FileNames.Count)
        //            return this.Imgs.Count;
        //        else
        //            return -1;
        //    }
        //}

        /// <summary>
        /// Reemplaza las coincidencias de indices pasado por parametros por valores base64 correspondientes. ambas listas deben ser igual de largas y coincidir sus indices y valores entre ellas.
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="FullMessage"></param>
        /// <param name="CidsIndicesList"></param>
        /// <param name="ListEmbeddedImages"></param>
        /// <returns>Devuelve el body del mail actualizado.</returns>
        //private string ReplaceCidForSrc(ImapMessage eMail, MailMessage FullMessage, FileNamesDTO ObjectFileNames)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comenzo la operacion de remapeado de imagenes...");
        //    StringBuilder SB = new StringBuilder().Append(FullMessage.BodyHtml);
        //    MemoryStream ms;

        //    for (int i = 0; i < ObjectFileNames.Imgs.Count; i++)
        //    {
        //        try
        //        {
        //            ZTrace.WriteLineIf(ZTrace.IsError, "Mapeando: " + ObjectFileNames.FileNames[i] + "; Posicion: " + (i + int.Parse(1.ToString())) + "/" + ObjectFileNames.Imgs.Count);
        //            if (!Uri.IsWellFormedUriString(ObjectFileNames.FileNames[i], UriKind.Absolute))
        //            {
        //                ms = new MemoryStream();
        //                imapClient.GetAttachment(eMail.SequenceNumber, ObjectFileNames.FileNames[i]).Data.CopyToAsync(ms);
        //                byte[] data = ms.ToArray();
        //                string Base64 = Convert.ToBase64String(data);

        //                ZTrace.WriteLineIf(ZTrace.IsError, "Inidice a mapear: " + ObjectFileNames.Imgs[i] + "Hasta: " + ("data:image/" + ObjectFileNames.FileNames[i].Split('.').Last() + ";base64," + Base64).Length.ToString() + " caracteres.");
        //                ZTrace.WriteLineIf(ZTrace.IsError, "Largo total del HTML (cuerpo del correo): " + FullMessage.BodyHtml.Length.ToString());
        //                SB.Replace(FullMessage.BodyHtml.Substring(ObjectFileNames.Imgs[i], FullMessage.BodyHtml.Substring(ObjectFileNames.Imgs[i]).IndexOf("\"")),
        //                "data:image/" + ObjectFileNames.FileNames[i].Split('.').Last() + ";base64," + Base64);
        //            }
        //            else
        //            {
        //                ZTrace.WriteLineIf(ZTrace.IsError, "Inidice a mapear: " + ObjectFileNames.Imgs[i] + "Hasta: " + FullMessage.BodyHtml.Substring(ObjectFileNames.Imgs[i]).IndexOf("\"").ToString() + " caracteres.");
        //                ZTrace.WriteLineIf(ZTrace.IsError, "Largo total del HTML (cuerpo del correo): " + FullMessage.BodyHtml.Length.ToString());
        //                SB.Replace(FullMessage.BodyHtml.Substring(ObjectFileNames.Imgs[i], FullMessage.BodyHtml.Substring(ObjectFileNames.Imgs[i]).IndexOf("\"")),
        //                ObjectFileNames.FileNames[i]);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString() + "Retomando mapeado... ");
        //            ZTrace.WriteLineIf(ZTrace.IsError, "La funcion de mapeado de imagenes fallo en la imagen: " + ObjectFileNames.FileNames[i] + "; Posicion: " + (i + int.Parse(1.ToString())) + "/" + ObjectFileNames.Imgs.Count);
        //        }
        //    }

        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se reemplazaron los CIDS por Base64 de todas las imagenes.");
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Remapeado finalizado con exito.");
        //    return SB.ToString();
        //}

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        //private static int GetStringPositionFromBody(string body, string str)
        //{
        //    int index = 0;
        //    int indexStr = 0;

        //    while (index == 0 && indexStr != -1)
        //    {
        //        for (; ; indexStr += str.Length)
        //        {
        //            indexStr = body.IndexOf(str, indexStr);

        //            if (indexStr == -1)
        //                break;
        //            else
        //            {
        //                index = indexStr;
        //                break;
        //            }
        //        }
        //    }

        //    return index;
        //}

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        //private static List<int> GetPositionsSrcFromBody(string body, string str)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando ubicacion de elementos 'IMG'...");
        //    List<int> IndicesList = new List<int>();
        //    int index = 0;
        //    while (index != -1)
        //    {
        //        for (; ; index += str.Length)
        //        {
        //            index = body.IndexOf(str, index);

        //            if (index == -1)
        //                break;
        //            else
        //            {
        //                IndicesList.Add(index);
        //            }
        //        }
        //    }

        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron " + IndicesList.Count + " ubicaciones efectivas.");
        //    return IndicesList;
        //}

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        //private static List<int> GetListStringPositionFromBody(string body, string CidString)
        //{
        //    List<int> CidsIndicesList = new List<int>();
        //    int indexCid = 0;
        //    while (indexCid != -1)
        //    {
        //        for (; ; indexCid += CidString.Length)
        //        {
        //            indexCid = body.IndexOf(CidString, indexCid);

        //            if (indexCid == -1)
        //                break;
        //            else
        //                CidsIndicesList.Add(indexCid);
        //        }
        //    }

        //    return CidsIndicesList;
        //}

        //private void SaveMsgFile(DTOObjectImap imapProcess, ZMessage eMail, string TemporaryPath)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicia la operacion de guardado temporal (msgKit).");
        //    List<string> Path_List = GetAndSaveMailFiles(imapProcess, eMail);

        //    saveEmailWithKit(eMail.message.From, eMail.message.Cc, "", eMail.message.To,
        //        eMail.message.Subject, eMail.message.Body, eMail.message.Date,
        //        eMail.message.Attachments,
        //        TemporaryPath, Path_List);

        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Finalizo operacion de guardado temporal (msgKit).");
        //}

        //private List<string> GetAndSaveMailFiles(DTOObjectImap imapProcess, ZMessage eMail)
        //{
        //    List<string> Path_List = new List<string>();
        //    List<string> Path_List_OldEml = new List<string>();
        //    List<Attachment> Path_AttachmentsFixed = new List<Attachment>();

        //    //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetAndSaveMailFiles: Comienza proceso de iteracion de adjuntos del correo " + eMail.UniqueId.ToString());
        //    foreach (Attachment item in eMail.message.Attachments)
        //    {
        //        Attachment At = GetFilesFromMail(imapProcess, eMail, item, ref Path_AttachmentsFixed);


        //        if (At == null)
        //            continue;

        //        string TemporaryPath_attachment;
        //        string BasePath = Zamba.Membership.MembershipHelper.AppTempPath + "\\Imap\\Attachments\\";

        //        if (item.ContentType.Name.Split('.').Last() == "rfc822")
        //        {
        //            TemporaryPath_attachment = GetNewFileName(BasePath, At.FileName);
        //        }
        //        else
        //        {
        //            if (item.FileName == "")
        //            {
        //                TemporaryPath_attachment = GetNewFileName(BasePath, At.FileName);
        //            }
        //            else
        //            {
        //                TemporaryPath_attachment = GetNewFileName(BasePath, item.FileName);
        //            }
        //        }

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: GetAndSaveMailFiles: Ruta de archivo temporal: " + TemporaryPath_attachment);

        //        if (At.FileName.Split('.').Last() != "eml")
        //        {
        //            SaveMailFiles(At, TemporaryPath_attachment);
        //        }
        //        else
        //        {
        //            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comienza el guardado de EML a MSG...");
        //            MailMessage miMailMessage = MailMessage.Load(At.Data, MailMessageFormat.Eml);
        //            MailPreview miMailPreview = new MailPreview(miMailMessage, true);

        //            At.FileName = miMailPreview.subject.ToString() + ".msg";

        //            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        //            foreach (char c in invalid)
        //            {
        //                At.FileName = At.FileName.Replace(c.ToString(), "");
        //            }


        //            //TODO: Quitar bloque IF?
        //            if (File.Exists(BasePath + At.FileName))
        //                At.FileName = FileNameIncrementer(BasePath, At.FileName);

        //            TemporaryPath_attachment = BasePath + At.FileName;

        //            //Se guarda el archivo en formato MSG
        //            miMailMessage.Save(TemporaryPath_attachment, MailMessageFormat.Msg);
        //            Path_List_OldEml.Add(TemporaryPath_attachment);
        //            Console.WriteLine("Guardado completado!");
        //        }

        //        Path_List.Add(TemporaryPath_attachment);
        //    }

        //    //Borramos archivos rfc822.
        //    foreach (Attachment item in Path_AttachmentsFixed)
        //    {
        //        for (int i = 0; i < eMail.Attachments.Count; i++)
        //        {
        //            if (item.ContentId == eMail.Attachments[i].ContentId)
        //            {
        //                eMail.Attachments[i].Dispose();
        //            }
        //        }
        //    }

        //    //Agrego los archivos MSG a la coleccion de Attachments de eMail para luego ser insertados al sistema (ZambaWeb)
        //    foreach (string item in Path_List_OldEml)
        //    {
        //        Attachment NewMsg = new Attachment(item);
        //        NewMsg.FileName = NewMsg.ContentType.Name;
        //        eMail.Attachments.Add(NewMsg);
        //    }

        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetAndSaveMailFiles: finalizo proceso de iteracion de adjuntos del correo. ");
        //    return Path_List;
        //}

        private string GetNewFileName(string TemporaryPath_attachment, string FileName)
        {
            if (Directory.Exists(TemporaryPath_attachment) == false)
                Directory.CreateDirectory(TemporaryPath_attachment);

            string TempMsgPath = "";

            if (File.Exists(TemporaryPath_attachment + FileName))
            {
                var FileNameIncremented = FileNameIncrementer(TemporaryPath_attachment, FileName);
                TempMsgPath = new DirectoryInfo(TemporaryPath_attachment + FileNameIncremented).FullName;
            }
            else
            {
                TempMsgPath = new DirectoryInfo(TemporaryPath_attachment + FileName).FullName;
            }

            return TempMsgPath;
        }

        //private Attachment GetFilesFromMail(DTOObjectImap imapProcess, ZMessage eMail, Attachment item, ref List<Attachment> Path_AttachmentsFixed)
        //{
        //    imapClient.Select(imapProcess.Carpeta);

        //    //Si el adjunto fue creado como elemento de outlook
        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetFilesFromMail: ");
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + item.FileName.ToString() != string.Empty ? item.FileName.ToString() : "<SIN NOMBRE>");
        //    Attachment At;

        //    if (item.FileName == "")
        //    {
        //        At = new Attachment(item.Data);
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentId: " + item.ContentId.ToString());
        //        At.ContentId = item.ContentId;

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.MediaType: " + item.ContentType.MediaType.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.Name: " + item.ContentType.Name.ToString());
        //        At.ContentType = item.ContentType;

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: DispositionType: " + item.DispositionType.ToString());
        //        At.DispositionType = item.DispositionType;

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + item.ContentId.ToString());
        //        //At.FileName = item.ContentId + ".msg";
        //        At.FileName = item.ContentType.Name.ToString();

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Size: " + item.Size.ToString());
        //        At.Size = item.Size;

        //        FixAttachedImageFormat(At);

        //        if (!(At.FileName.Split('.').Count() > 1))
        //        {
        //            At = null;
        //            ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Se desestimo este adjunto debido a que no contiene formato");
        //            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: Se desestimo este adjunto para evitar fallas en el sistema, es posible que se trate de una imagen dentro de la firma que no fue interpretada por el cliente IMAp adecuadamente.");
        //        }
        //    }
        //    else
        //    {
        //        At = new Attachment(item.Data);
        //        //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: SequenceNumber: " + eMail.SequenceNumber.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: FileName: " + item.FileName.ToString());
        //        //At = imapClient.GetAttachment(eMail.SequenceNumber, item.FileName);

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentId: " + At.ContentId.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.MediaType: " + item.ContentType.MediaType.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.Name: " + At.ContentType.Name.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: DispositionType: " + At.DispositionType.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + At.ContentId.ToString());
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Size: " + At.Size.ToString());
        //        At = item;

        //        FixAttachedImageFormat(At);

        //        if (!(At.FileName.Split('.').Count() > 1))
        //        {
        //            At = null;
        //            ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Se desestimo este adjunto debido a que no contiene formato");
        //            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: Se desestimo este adjunto para evitar fallas en el sistema, es posible que se trate de una imagen dentro de la firma que no fue interpretada por el cliente IMAp adecuadamente.");
        //        }

        //    }

        //    if (item.ContentType.Name.Split('.').Last() == "rfc822")
        //    {
        //        Zamba.FileTools.MailPreview Msg = new Zamba.FileTools.SpireTools().ConvertEmlToMsg(item.Data);

        //        At.ContentId = item.ContentId;
        //        At.FileName = Msg.subject.ToString() != "" ? Msg.subject.ToString() + ".eml" : "SIN ASUNTO" + ".eml";

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Caracteres ilegales quitados.");
        //        string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        //        foreach (char c in invalid)
        //        {
        //            At.FileName = At.FileName.Replace(c.ToString(), "");
        //        }

        //        At.ContentType.Name = At.FileName;
        //        At.ContentType.MediaType = "message/rfc822";

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: nombre de archivo final: " + At.FileName);
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: MediaType: " + At.ContentType.MediaType);
        //        Path_AttachmentsFixed.Add(At);
        //    }

        //    return At;

        //}

        //private static void FixAttachedImageFormat(Attachment At)
        //{
        //    switch (At.ContentType.MediaType)
        //    {
        //        case "image/jpeg":
        //            At.FileName += !(At.FileName.Split('.').Last() == "jpeg") ? ".jpeg" : "";
        //            break;

        //        case "image/png":
        //            At.FileName += !(At.FileName.Split('.').Last() == "png") ? ".png" : "";
        //            break;

        //        default:
        //            At.FileName = At.ContentType.Name;
        //            break;
        //    }
        //}

        /// <summary>
        /// Devuelve el nombre del archivo pasado por parametro con un incrementador de archivo duplicado.
        /// </summary>
        /// <param name="BasePath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string FileNameIncrementer(string BasePath, string FileName)
        {
            var LastIndexOfPoint = FileName.LastIndexOf('.');

            for (int i = 1; true; i++)
            {
                if (!(File.Exists(BasePath + FileName.Insert(LastIndexOfPoint, "(" + i.ToString() + ")"))))
                    return FileName.Insert(LastIndexOfPoint, "(" + i.ToString() + ")");
            }
        }


        //private static void SaveMailFiles(Attachment At, string TemporaryPath_attachment)
        //{
        //    byte[] buffer = new byte[8 * 1024];
        //    MemoryStream ms = new MemoryStream();

        //    int read = 0;
        //    while ((read = At.Data.Read(buffer, 0, buffer.Length)) > 0)
        //    {
        //        ms.Write(buffer, 0, read);
        //    }

        //    File.WriteAllBytes(TemporaryPath_attachment, ms.ToArray());

        //    ms.Close();
        //    ms.Dispose();
        //}

        //public string saveEmailWithKit(string From, MailAddressCollection eMailcc, string eMailcco, MailAddressCollection eMailTo, string Subject,
        //    string Body, DateTime sendOn, AttachmentCollection Attachments, string TemporaryPath,
        //    List<string> Path_List)
        //{
        //    try
        //    {
        //        using (var email = new MsgKit.Email(
        //        new MsgKit.Sender(From, string.Empty),
        //        Subject,
        //        false,
        //        false))
        //        {
        //            foreach (MailAddress item in eMailTo)
        //            {
        //                email.Recipients.AddTo(item.Address);
        //            }

        //            foreach (MailAddress item in eMailcc)
        //            {
        //                email.Recipients.AddCc(item.Address);
        //            }

        //            email.Recipients.AddBcc(eMailcco);
        //            email.Subject = Subject;
        //            email.BodyText = Body;
        //            email.BodyHtml = Body;
        //            email.SentOn = sendOn.ToUniversalTime();
        //            email.IconIndex = MsgKit.Enums.MessageIconIndex.ReceiptMail;

        //            foreach (string item in Path_List)
        //            {
        //                email.Attachments.Add(item);
        //            }

        //            email.Save(TemporaryPath);
        //            return TemporaryPath;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
        //        throw new Exception(ex.ToString());
        //    }
        //}

        private static INewResult InsertEMail(DTOObjectImap imapProcess, ZMessage eMail, ref InsertResult insertResult, IResults_Business RB, string TempMsgPath)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicia el proceso de insercion del correo al sistema.");

                INewResult newResult;
                newResult = RB.GetNewNewResult(imapProcess.Entidad);

                if (imapProcess.Codigo_mail != 0)
                {
                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                    string ExportCode = UserPreferencesFactory.getValueDB("IdAutoIncrementExporta", UPSections.ExportaPreferences, 0);
                    newResult.get_GetIndexById(imapProcess.Codigo_mail).DataTemp = ExportCode;
                    UserPreferencesFactory.setValueDB("IdAutoIncrementExporta", (int.Parse(ExportCode) + 1).ToString(), UPSections.ExportaPreferences, 0);
                }

                newResult.get_GetIndexById(imapProcess.Enviado_por).DataTemp = string.Join(";",eMail.message.From);

                string StringTo = "";

                foreach (InternetAddress item in eMail.message.To)
                {
                    StringTo += item.Name;

                    if (eMail.message.To.Last() != item)
                        StringTo += "; ";
                }

                newResult.get_GetIndexById(imapProcess.Para).DataTemp = StringTo;

                if (imapProcess.Cc != 0)
                {
                    string StringCc = "";
                    foreach (InternetAddress item in eMail.message.Cc)
                    {
                        StringCc += item.Name;

                        if (eMail.message.Cc.Last() != item)
                            StringCc += "; ";
                    }

                    newResult.get_GetIndexById(imapProcess.Cc).DataTemp = StringCc;
                }

                newResult.get_GetIndexById(imapProcess.Fecha).DataTemp = eMail.message.Date.ToString();
                newResult.get_GetIndexById(imapProcess.Asunto).DataTemp = eMail.message.Subject;

                MailAddress addressFrom = new MailAddress(eMail.message.From.ToString());

                newResult.File = TempMsgPath;
                insertResult = RB.Insert(ref newResult, false, false, false, false, false, false, false);
                File.Delete(TempMsgPath);
                return newResult;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception(ex.ToString());
            }
        }

        public void MoveEmail(IMailFolder folder,UniqueId MessageId, string folderName)
        {
            try
            {
                IMailFolder newfolder;
                try
                {
                    newfolder = folder.ParentFolder.GetSubfolder(folderName);

                }
                catch (FolderNotFoundException)
                {
                    folder.ParentFolder.Create(folderName, true);
                    newfolder = folder.ParentFolder.GetSubfolder(folderName);
                }

                // Move the email to the exported folder
                folder.MoveTo(MessageId, newfolder);

                //var a = imapClient.BeginCopy(MessageId, folderName, MoveCB);
                //imapClient.EndCopy(a);

                //var b = imapClient.BeginMarkAsDeleted(MessageId, DeleteCB);
                //imapClient.EndMarkAsDeleted(b);
                //imapClient.DeleteMarkedMessages();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("NO Some of the requested messages no longer exist.")
                    || ex.Message.ToString().Contains("NO The specified message set is invalid."))
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, ex.Message.ToString());
                }
                else
                {
                    throw ex;
                }
            }
        }

        //private void MoveCB(IAsyncResult ar)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail copiado");
        //}
        //private void DeleteCB(IAsyncResult ar)
        //{
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail Borrado");
        //}

    }





    //public class ImapMessageDTO : IZImapMessageDTO
    //{
    //    #region Atributos
    //    public string UniqueId { get; set; }
    //    public bool IsAnswered { get; set; }
    //    public bool IsDeleted { get; set; }
    //    public bool IsDraft { get; set; }
    //    public bool IsFlagged { get; set; }
    //    public bool IsRecent { get; set; }
    //    public bool IsRead { get; set; }
    //    public long Size { get; set; }
    //    public int SequenceNumber { get; set; }
    //    public string Subject { get; set; }

    //    public string From { get; set; }

    //    public string To { get; set; }

    //    public string Cc { get; set; }

    //    public string ReplyTo { get; set; }

    //    public string Sender { get; set; }
    //    public string Body { get; set; }

    //    public string Date { get; set; }

    //    private int _Attachments_Count;
    //    public int Attachments_Count
    //    {
    //        get { return _Attachments_Count; }
    //        set { _Attachments_Count = value; }
    //    }


    //    private AttachmentCollection _Attachments;
    //    public AttachmentCollection Attachments
    //    {
    //        get
    //        {
    //            return _Attachments;
    //        }

    //        set
    //        {
    //            _Attachments = value;
    //            this._Attachments_Count = value.Count;
    //        }
    //    }

    //    #endregion

    //    public ImapMessageDTO(ImapMessage Obj)
    //    {
    //        this.Cc = string.Concat(Obj.Cc.Select(n => n.Address + "; ")
    //                .AsEnumerable())
    //                .TrimEnd(' ')
    //                .TrimEnd(';');

    //        this.Date = Obj.Date.ToLocalTime().ToString();
    //        this.From = Obj.From.Address;
    //        this.IsAnswered = Obj.IsAnswered;
    //        this.IsDeleted = Obj.IsDeleted;
    //        this.IsDraft = Obj.IsDraft;
    //        this.IsFlagged = Obj.IsFlagged;
    //        this.IsRecent = Obj.IsRecent;
    //        this.IsRead = Obj.IsRead;
    //        this.Size = Obj.Size;
    //        //this.ReplyTo = Obj.ReplyTo.Address; 
    //        this.Sender = Obj.Sender.Address;
    //        this.SequenceNumber = Obj.SequenceNumber;
    //        this.Subject = Obj.Subject;

    //        this.To = string.Concat(Obj.To.Select(n => n.Address + "; ")
    //                .AsEnumerable())
    //                .TrimEnd(' ')
    //                .TrimEnd(';');

    //        this.UniqueId = Obj.UniqueId;
    //    }

    //    public ImapMessageDTO(ImapMessage Obj, AttachmentCollection Attachs) : this(Obj)
    //    {
    //        this.Attachments = Attachs;
    //    }
    //}
}



//AcceptLanguage 0   The Accept-Language header field.
//AdHoc	1	The Ad-Hoc header field.
//AlternateRecipient	2	The Alternate-Recipient header field.
//ApparentlyTo	3	The Apparently-To header field.
//Approved	4	The Approved header field.
//ArcAuthenticationResults	5	The ARC-Authentication-Results header field.
//ArcMessageSignature	6	The ARC-Message-Signature header field.
//ArcSeal	7	The ARC-Seal header field.
//Archive	8	The Archive header field.
//ArchivedAt	9	The Archived-At header field.
//Unknown	-1	An unknown header field.
//Article	10	The Article header field.
//AuthenticationResults	11	The Authentication-Results header field.
//Autocrypt	12	The Autocrypt header field.
//AutocryptGossip	13	The Autocrypt-Gossip header field.
//AutocryptSetupMessage	14	The Autocrypt-Setup-Message header field.
//Autoforwarded	15	The Autoforwarded header field.
//AutoSubmitted	16	The Auto-Submitted header field.
//Autosubmitted	17	The Autosubmitted header field.
//Base	18	The Base header field.
//Bcc	19	The Bcc header field.
//Body	20	The Body header field.
//Bytes	21	The Bytes header field.
//Cc	22	The Cc header field.
//Comments	23	The Comments header field.
//ContentAlternative	24	The Content-Alternative header field.
//ContentBase	25	The Content-Base header field.
//ContentClass	26	The Content-Class header field.
//ContentDescription	27	The Content-Description header field.
//ContentDisposition	28	The Content-Disposition header field.
//ContentDuration	29	The Content-Duration header field.
//ContentFeatures	30	The Content-Features header field.
//ContentId	31	The Content-Id header field.
//ContentIdentifier	32	The Content-Identifier header field.
//ContentLanguage	33	The Content-Language header field.
//ContentLength	34	The Content-Length header field.
//ContentLocation	35	The Content-Location header field.
//ContentMd5	36	The Content-Md5 header field.
//ContentReturn	37	The Content-Return header field.
//ContentTransferEncoding	38	The Content-Transfer-Encoding header field.
//ContentTranslationType	39	The Content-Translation-Type header field.
//ContentType	40	The Content-Type header field.
//Control	41	The Control header field.
//Conversion	42	The Conversion header field.
//ConversionWithLoss	43	The Conversion-With-Loss header field.
//Date	44	The Date header field.
//DateReceived	45	The Date-Received header field.
//DeferredDelivery	46	The Deferred-Delivery header field.
//DeliveryDate	47	The Delivery-Date header field.
//DiscloseRecipients	48	The Disclose-Recipients header field.
//DispositionNotificationOptions	49	The Disposition-Notification-Options header field.
//DispositionNotificationTo	50	The Disposition-Notification-To header field.
//Distribution	51	The Distribution header field.
//DkimSignature	52	The DKIM-Signature header field.
//DomainKeySignature	53	The DomainKey-Signature header field.
//Encoding	54	The Encoding header field.
//Encrypted	55	The Encrypted header field.
//Expires	56	The Expires header field.
//ExpiryDate	57	The Expiry-Date header field.
//FollowupTo	58	The Followup-To header field.
//From	59	The From header field.
//GenerateDeliveryReport	60	The Generate-Delivery-Report header field.
//Importance	61	The Importance header field.
//InjectionDate	62	The Injection-Date header field.
//InjectionInfo	63	The Injection-Info header field.
//InReplyTo	64	The In-Reply-To header field.
//Keywords	65	The Keywords header field.
//Language	66	The Language header.
//LatestDeliveryTime	67	The Latest-Delivery-Time header.
//Lines	68	The Lines header field.
//ListArchive	69	The List-Archive header field.
//ListHelp	70	The List-Help header field.
//ListId	71	The List-Id header field.
//ListOwner	72	The List-Owner header field.
//ListPost	73	The List-Post header field.
//ListSubscribe	74	The List-Subscribe header field.
//ListUnsubscribe	75	The List-Unsubscribe header field.
//ListUnsubscribePost	76	The List-Unsubscribe-Post header field.
//MessageId	77	The Message-Id header field.
//MimeVersion	78	The MIME-Version header field.
//Newsgroups	79	The Newsgroups header field.
//NntpPostingHost	80	The Nntp-Posting-Host header field.
//Organization	81	The Organization header field.
//OriginalFrom	82	The Original-From header field.
//OriginalMessageId	83	The Original-Message-Id header field.
//OriginalRecipient	84	The Original-Recipient header field.
//OriginalReturnAddress	85	The Original-Return-Address header field.
//OriginalSubject	86	The Original-Subject header field.
//Path	87	The Path header field.
//Precedence	88	The Precedence header field.
//PreventNonDeliveryReport	89	The Prevent-NonDelivery-Report header field.
//Priority	90	The Priority header field.
//Received	91	The Received header field.
//ReceivedSPF	92	The Received-SPF header field.
//References	93	The References header field.
//RelayVersion	94	The Relay-Version header field.
//ReplyBy	95	The Reply-By header field.
//ReplyTo	96	The Reply-To header field.
//RequireRecipientValidSince	97	The Require-Recipient-Valid-Since header field.
//ResentBcc	98	The Resent-Bcc header field.
//ResentCc	99	The Resent-Cc header field.
//ResentDate	100	The Resent-Date header field.
//ResentFrom	101	The Resent-From header field.
//ResentMessageId	102	The Resent-Message-Id header field.
//ResentReplyTo	103	The Resent-Reply-To header field.
//ResentSender	104	The Resent-Sender header field.
//ResentTo	105	The Resent-To header field.
//ReturnPath	106	The Return-Path header field.
//ReturnReceiptTo	107	The Return-Receipt-To header field.
//SeeAlso	108	The See-Also header field.
//Sender	109	The Sender header field.
//Sensitivity	110	The Sensitivity header field.
//Solicitation	111	The Solicitation header field.
//Status	112	The Status header field.
//Subject	113	The Subject header field.
//Summary	114	The Summary header field.
//Supersedes	115	The Supersedes header field.
//TLSRequired	116	The TLS-Required header field.
//To	117	The To header field.
//UserAgent	118	The User-Agent header field.
//X400ContentIdentifier	119	The X400-Content-Identifier header field.
//X400ContentReturn	120	The X400-Content-Return header field.
//X400ContentType	121	The X400-Content-Type header field.
//X400MTSIdentifier	122	The X400-MTS-Identifier header field.
//X400Originator	123	The X400-Originator header field.
//X400Received	124	The X400-Received header field.
//X400Recipients	125	The X400-Recipients header field.
//X400Trace	126	The X400-Trace header field.
//XMailer	127	The X-Mailer header field.
//XMSMailPriority	128	The X-MSMail-Priority header field.
//XPriority	129	The X-Priority header field.
//XStatus	130	The X-Status header field.