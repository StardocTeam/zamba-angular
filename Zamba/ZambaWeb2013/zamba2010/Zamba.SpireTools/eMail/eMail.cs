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
using System.Threading;
using System.Text.RegularExpressions;

namespace Zamba.SpireTools
{
    public class eMail : ISpireEmailTools
    {
        public string ReadInBox()
        {
            try
            {
                UserPreferences UP = new UserPreferences();

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
            catch (Exception ex)
            {
                throw ex;
            }
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
                throw ex;
            }
        }

        //private String GetMails_WithRules(ImapClient imapClient, Dictionary<string, string> Params)
        private List<ImapMessage> GetMailsFiltered(DTOObjectImap Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de metodo de filtrado.");

                imapClient.Select(Params.Carpeta);
                List<ImapMessage> List_ImapMessageDTO = new List<ImapMessage>();

                if (Convert.ToInt32(Params.Filtrado.ToString()) == 0)
                {
                    foreach (ImapMessage item in imapClient.GetAllMessageHeaders())
                    {
                        if (Val_ParamsToGetMails(Params, item))
                            List_ImapMessageDTO.Add(item);
                    }
                }
                else if (Convert.ToInt32(Params.Filtrado.ToString()) != 0)
                {
                    ImapMessageCollection IMAP_Colection = null;

                    try
                    {
                        IMAP_Colection = FilterFolderSelected(Params.Filtro_campo, Params.Filtro_valor);
                    }
                    catch (Exception)
                    {
                    }

                    if (IMAP_Colection != null)
                    {
                        foreach (ImapMessage item in IMAP_Colection)
                        {
                            if (Val_ParamsToGetMails(Params, item))
                                List_ImapMessageDTO.Add(item);
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
                throw ex;
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
                throw ex;
            }
        }

        private List<ImapMessage> GetEmailsFromServer(DTOObjectImap Dic_paramRequest)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo correos de '" + Dic_paramRequest.Carpeta + "'...");
                List<ImapMessage> ListaDeEmails = GetMailsFiltered(Dic_paramRequest);

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
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciada ejecucion de proceso con ID:" + imapProcess.Id_proceso + " Nombre del proceso:" + imapProcess.Nombre_proceso);
                        ConnectToExchange(imapProcess);

                        //GetEmails               
                        List<ImapMessage> ListaDeEmails = GetEmailsFromServer(imapProcess).OrderByDescending(i => i.SequenceNumber).ToList();

                        imapProcess.Nombre_usuario = originalUserName;

                        foreach (ImapMessage eMail in ListaDeEmails)
                        {
                            try
                            {

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando correo: UniqueId: " + eMail.UniqueId + " - SequenceNumber:" + eMail.SequenceNumber);
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto del correo en iteracion: " + eMail.Subject);

                                string SubjectName = GetNewSubjectName(eMail);
                                string TempMsgPath = GetNewFileName(Zamba.Membership.MembershipHelper.AppTempPath + "\\Imap\\Temp\\", SubjectName + ".msg");

                                //Se obtiene mail completo.
                                MailMessage FullMessage = imapClient.GetFullMessage(eMail.UniqueId);

                                setSrcFromBody(eMail, FullMessage);

                                //Se guarda el archivo msg en el sistema.
                                SaveMsgFile(imapProcess, FullMessage, TempMsgPath);

                                //Se inserta la tarea en el sistema.
                                InsertResult eMailInsertResult = InsertResult.NoInsertado;
                                INewResult eMailNewResult = InsertEMail(imapProcess, FullMessage, eMail.UniqueId, ref eMailInsertResult, RB, TempMsgPath);

                                //Se mueve el correo dentro de la casilla.
                                if (eMailInsertResult == InsertResult.Insertado)
                                {
                                    if (imapProcess.Exportar_adjunto_por_separado == 1)
                                    {
                                        foreach (var attach in FullMessage.Attachments)
                                        {
                                            InsertResult attachInsertResult = InsertResult.NoInsertado;
                                            INewResult attachNewResult = InsertEMail(imapProcess, FullMessage, eMail.UniqueId, ref attachInsertResult, RB, TempMsgPath);
                                        }
                                    }

                                    MoveEmail(eMail.SequenceNumber, imapProcess.CarpetaDest);
                                }

                                FullMessage.Attachments.Clear();
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo con uniqueID: " + eMail.UniqueId);
                                ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo con uniqueID: " + imapProcess.Id_proceso);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
                    }
                }
            }
        }

        private static string GetNewSubjectName(ImapMessage eMail)
        {
            var SubjectName = eMail.Subject != "" ? eMail.Subject.ToString() : "SIN ASUNTO";

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
        private void setSrcFromBody(ImapMessage eMail, MailMessage FullMessage)
        {
            try
            {

                string[] ImageTags = System.Text.RegularExpressions.Regex.Split(FullMessage.BodyHtml, "<img", RegexOptions.IgnoreCase);

                string FirstSrcValue = "";
                string FirstCidValue = "";

                if (ImageTags.Length > 1)
                {
                    //Busco el SRC de la primera imagen, este indice sera el modificado en "ReplaceCidForSrc()"
                    int FirstPosSrc = GetStringPositionFromBody(ImageTags[1], "src=");

                    int FirstPosAlt = GetStringPositionFromBody(ImageTags[1], "alt=");

                    //Obtengo el valor del primer atributo SRC, en el, estara la palabra "cid:"
                    FirstSrcValue = ImageTags[1].Substring(FirstPosSrc + 5, ImageTags[1].Substring(FirstPosSrc + 5).IndexOf("\""));

                    //Obtengo el nombre de la imagen embebida no importa como esta formado su valor CID.
                    //FirstCidValue = FirstSrcValue.Split('@').First();


                    //List<int> CidsIndices = GetListStringPositionFromBody(FullMessage.BodyHtml, "cid:");
                    //List<int> CidsIndices = GetListStringPositionFromBody(FullMessage.BodyHtml, "src=");

                    if (FirstSrcValue.Contains('@'))
                    {
                        List<int> SrcIndices = GetPositionsSrcFromBody(FullMessage.BodyHtml, "src=");

                        List<string> EmbeddedFileNames = GetFileNamesFromBody(FullMessage, ImageTags);

                        EmbeddedFileNames.Reverse();
                        SrcIndices.Reverse();

                        if (EmbeddedFileNames.Count != 0)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[setSrcFromBody]: Se reemplazaron los CIDS por Base64.");
                            FullMessage.BodyHtml = ReplaceCidForSrc(eMail, FullMessage, SrcIndices, EmbeddedFileNames);
                        }

                    }
                    else
                    {
                        List<int> CidsIndices = GetListStringPositionFromBody(FullMessage.BodyHtml, "cid:");

                        List<string> EmbeddedImageIndexes = GetEmbeddedImageIndexes(eMail, CidsIndices, ImageTags);

                        EmbeddedImageIndexes.Reverse();
                        CidsIndices.Reverse();

                        if (EmbeddedImageIndexes.Count != 0)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[setSrcFromBody]: Se reemplazaron los CIDS por Base64.");
                            FullMessage.BodyHtml = ReplaceCidForSrc(eMail, FullMessage, CidsIndices, EmbeddedImageIndexes);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error al procesar el correo con uniqueID: " + eMail.UniqueId);
                ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Crea una lista de indices que se utilizaran para obtener imagenes del Cliente IMAP, siempre y cuando el formato de los CIDS sea algo parecido a: cid:HG347FHED
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="CidsIndices"></param>
        /// <param name="ImageTags"></param>
        /// <returns>Lista de indices en formato string.</returns>
        private List<string> GetEmbeddedImageIndexes(ImapMessage eMail, List<int> CidsIndices, string[] ImageTags)
        {
            List<string> ReorderedFileList = new List<string>();
            foreach (string ImgItem in ImageTags)
            {
                //TODO: TESTEAR ESTE METODO NUEVO
                int Cid = GetStringPositionFromBody(ImgItem, "cid:");
                int Alt = GetStringPositionFromBody(ImgItem, "alt=");

                //Valido que el elemento IMG tenga 
                if (Cid != 0 && Alt != 0)
                {
                    string AltValue = ImgItem.Substring(Alt + 5, ImgItem.Substring(Alt + 5).IndexOf("\""));

                    Attachment Image;
                    //Se Buscan las imagenes por incice
                    for (int i = 0; i < CidsIndices.Count; i++)
                    {
                        if (!ReorderedFileList.Contains((i + 2).ToString()))
                        {
                            try
                            {
                                Image = imapClient.GetAttachment(eMail.SequenceNumber, (i + 2).ToString());
                            }
                            catch (Exception)
                            {
                                Image = null;
                            }

                            if (Image != null && Image.ContentType.Name == AltValue)
                            {
                                ReorderedFileList.Add((i + 2).ToString());
                                break;
                            }
                        }
                    }

                    //Ahora se buscan las imagenes por nombre
                    //Reducir numero de veces iterado? int i = ReorderedFileList.Count?? (Performance)
                    for (int i = 0; i < CidsIndices.Count; i++)
                    {
                        Image = imapClient.GetAttachment(eMail.SequenceNumber, AltValue);

                        if (Image.ContentType.Name == AltValue)
                        {
                            ReorderedFileList.Add(AltValue);
                            break;
                        }
                    }
                }
            }

            return ReorderedFileList;
        }

        /// <summary>
        /// Obtiene los nombres de las diferentes imagenes embebidas dentro del body de un corre, siempre y cuando el formato de los CIDS sea algo parecido a: cid:image001.png@01D93496.D22948C0
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="ImageTags"></param>
        /// <returns></returns>
        private static List<string> GetFileNamesFromBody(MailMessage FullMessage, string[] ImageTags)
        {
            List<string> ListEmbeddedImages = new List<string>();

            foreach (string ImgItem in ImageTags)
            {
                //int PosCid = GetStringPositionFromBody(ImgItem, "cid:");
                int PosSrc = GetStringPositionFromBody(ImgItem, "src=");
                int PosAlt = GetStringPositionFromBody(ImgItem, "alt=");

                //Todo: completar validacion
                if (PosSrc != 0)
                {
                    //Valida cuantos '@' hay en el nombre del archivo.  
                    string[] FileNameSplited = ImgItem.Substring(PosSrc + 5 + 4, ImgItem.Substring(PosSrc + 5 + 4).IndexOf("\"")).Split('@');

                    //Si el nombre del archivo tiene un '@', tomara el nombre del archivo hasta el ultimo '@' del valor "CID:". 
                    //Si no hay ningun @, no tomara el nombre iterado.
                    if (FileNameSplited.Length == 2)
                    {
                        ListEmbeddedImages.Add(FileNameSplited[0]);
                    }
                    else if (FileNameSplited.Length > 2)
                    {
                        string FileName = "";

                        foreach (string itemString in FileNameSplited)
                        {
                            if (itemString == FileNameSplited.First())
                                FileName += itemString;
                            else if (itemString != FileNameSplited.Last())
                                FileName += "@" + itemString;
                        }

                        ListEmbeddedImages.Add(FileName);
                    }
                }


            }

            return ListEmbeddedImages;
        }

        /// <summary>
        /// Reemplaza las coincidencias de indices pasado por parametros por valores base64 correspondientes. ambas listas deben ser igual de largas y coincidir sus indices y valores entre ellas.
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="FullMessage"></param>
        /// <param name="CidsIndicesList"></param>
        /// <param name="ListEmbeddedImages"></param>
        /// <returns>Devuelve el body del mail actualizado.</returns>
        private string ReplaceCidForSrc(ImapMessage eMail, MailMessage FullMessage, List<int> CidsIndicesList, List<string> ListEmbeddedImages)
        {
            StringBuilder SB = new StringBuilder().Append(FullMessage.BodyHtml);
            List<Attachment> ListaDeAttachs = new List<Attachment>();
            MemoryStream ms;

            for (int i = 0; i < CidsIndicesList.Count; i++)
            {
                ms = new MemoryStream();
                imapClient.GetAttachment(eMail.SequenceNumber, ListEmbeddedImages[i]).Data.CopyToAsync(ms);
                byte[] data = ms.ToArray();
                string Base64 = Convert.ToBase64String(data);

                SB.Replace(
                    FullMessage.BodyHtml.Substring(
                        CidsIndicesList[i],
                        FullMessage.BodyHtml.Substring(CidsIndicesList[i]).IndexOf("\"")
                        ),
                    "data:image/" + ListEmbeddedImages[i].Split('.').Last() + ";base64," + Base64);
            }

            return SB.ToString();
        }

        /// <summary>
        /// Obtiene los nombres de las imagenes embebidas dentro del body del mail
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="AltString"></param>
        /// <param name="AltIndicesList"></param>
        /// <returns></returns>
        private static List<string> GetFileNamesFromBody(MailMessage FullMessage, string AltString, List<int> AltIndicesList)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo nombres de imagenes embebidas:");
            List<string> ListEmbeddedImages = new List<string>();

            foreach (int item in AltIndicesList)
            {
                string FileName = FullMessage.BodyHtml.Substring(item + AltString.Length, FullMessage.BodyHtml.Substring(item + AltString.Length).IndexOf("\\") - 1);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo encontrado: " + FileName);
                ListEmbeddedImages.Add(FileName);
            }

            return ListEmbeddedImages;
        }

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        private static int GetStringPositionFromBody(string body, string str)
        {
            int index = 0;
            int indexStr = 0;

            while (indexStr != -1)
            {
                for (; ; indexStr += str.Length)
                {
                    indexStr = body.IndexOf(str, indexStr);

                    if (indexStr == -1)
                        break;
                    else
                        index = indexStr;
                }
            }

            return index;
        }

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        private static List<int> GetPositionsSrcFromBody(string body, string str)
        {
            List<int> IndicesList = new List<int>();
            int index = 0;
            while (index != -1)
            {
                for (; ; index += str.Length)
                {
                    index = body.IndexOf(str, index);

                    // + 5 por que luego de "src=" viene un '"'.
                    string ValueOfStr = body.Substring(index + 5, body.Substring(index + 5).IndexOf("\""));

                    if (index == -1)
                        break;
                    else
                    {
                        //Se valida que es un cid con '@' nuevamente, Refactorizar?
                        if (ValueOfStr.Contains('@'))
                            IndicesList.Add(index + 5);
                    }
                }
            }

            return IndicesList;
        }

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        private static List<int> GetListStringPositionFromBody(string body, string CidString)
        {
            List<int> CidsIndicesList = new List<int>();
            int indexCid = 0;
            while (indexCid != -1)
            {
                for (; ; indexCid += CidString.Length)
                {
                    indexCid = body.IndexOf(CidString, indexCid);

                    if (indexCid == -1)
                        break;
                    else
                        CidsIndicesList.Add(indexCid);
                }
            }

            return CidsIndicesList;
        }

        private void SaveMsgFile(DTOObjectImap imapProcess, MailMessage eMail, string TemporaryPath)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: SaveMsgFile: Comienza operacion de guardado temporal (msgKit).");
            List<string> Path_List = GetAndSaveMailFiles(imapProcess, eMail);


            saveEmailWithKit(eMail.From.Address.ToString(), eMail.Cc, "", eMail.To,
                eMail.Subject, eMail.BodyHtml, eMail.Date,
                eMail.Attachments,
                TemporaryPath, Path_List);

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: SaveMsgFile: Finalizo operacion de guardado temporal (msgKit).");
        }

        private List<string> GetAndSaveMailFiles(DTOObjectImap imapProcess, MailMessage eMail)
        {
            List<string> Path_List = new List<string>();
            List<string> Path_List_OldEml = new List<string>();
            List<Attachment> Path_AttachmentsFixed = new List<Attachment>();

            //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetAndSaveMailFiles: Comienza proceso de iteracion de adjuntos del correo " + eMail.UniqueId.ToString());
            foreach (Attachment item in eMail.Attachments)
            {
                Attachment At = GetFilesFromMail(imapProcess, eMail, item, ref Path_AttachmentsFixed);


                if (At == null)
                    continue;

                string TemporaryPath_attachment;
                string BasePath = Zamba.Membership.MembershipHelper.AppTempPath + "\\Imap\\Attachments\\";

                if (item.ContentType.Name.Split('.').Last() == "rfc822")
                {
                    TemporaryPath_attachment = GetNewFileName(BasePath, At.FileName);
                }
                else
                {
                    if (item.FileName == "")
                    {
                        TemporaryPath_attachment = GetNewFileName(BasePath, At.FileName);
                    }
                    else
                    {
                        TemporaryPath_attachment = GetNewFileName(BasePath, item.FileName);
                    }
                }

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: GetAndSaveMailFiles: Ruta de archivo temporal: " + TemporaryPath_attachment);

                if (At.FileName.Split('.').Last() != "eml")
                {
                    SaveMailFiles(At, TemporaryPath_attachment);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comienza el guardado de EML a MSG...");
                    MailMessage miMailMessage = MailMessage.Load(At.Data, MailMessageFormat.Eml);
                    MailPreview miMailPreview = new MailPreview(miMailMessage, true);

                    At.FileName = miMailPreview.subject.ToString() + ".msg";

                    string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                    foreach (char c in invalid)
                    {
                        At.FileName = At.FileName.Replace(c.ToString(), "");
                    }


                    //TODO: Quitar bloque IF?
                    if (File.Exists(BasePath + At.FileName))
                        At.FileName = FileNameIncrementer(BasePath, At.FileName);

                    TemporaryPath_attachment = BasePath + At.FileName;

                    //Se guarda el archivo en formato MSG
                    miMailMessage.Save(TemporaryPath_attachment, MailMessageFormat.Msg);
                    Path_List_OldEml.Add(TemporaryPath_attachment);
                    Console.WriteLine("Guardado completado!");
                }

                Path_List.Add(TemporaryPath_attachment);
            }

            //Borramos archivos rfc822.
            foreach (Attachment item in Path_AttachmentsFixed)
            {
                for (int i = 0; i < eMail.Attachments.Count; i++)
                {
                    if (item.ContentId == eMail.Attachments[i].ContentId)
                    {
                        eMail.Attachments[i].Dispose();
                    }
                }
            }

            //Agrego los archivos MSG a la coleccion de Attachments de eMail para luego ser insertados al sistema (ZambaWeb)
            foreach (string item in Path_List_OldEml)
            {
                Attachment NewMsg = new Attachment(item);
                NewMsg.FileName = NewMsg.ContentType.Name;
                eMail.Attachments.Add(NewMsg);
            }

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetAndSaveMailFiles: finalizo proceso de iteracion de adjuntos del correo. ");
            return Path_List;
        }

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

        private Attachment GetFilesFromMail(DTOObjectImap imapProcess, MailMessage eMail, Attachment item, ref List<Attachment> Path_AttachmentsFixed)
        {
            imapClient.Select(imapProcess.Carpeta);

            //Si el adjunto fue creado como elemento de outlook
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: GetFilesFromMail: ");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + item.FileName.ToString() != string.Empty ? item.FileName.ToString() : "<SIN NOMBRE>");
            Attachment At;

            if (item.FileName == "")
            {
                At = new Attachment(item.Data);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentId: " + item.ContentId.ToString());
                At.ContentId = item.ContentId;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.MediaType: " + item.ContentType.MediaType.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.Name: " + item.ContentType.Name.ToString());
                At.ContentType = item.ContentType;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: DispositionType: " + item.DispositionType.ToString());
                At.DispositionType = item.DispositionType;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + item.ContentId.ToString());
                //At.FileName = item.ContentId + ".msg";
                At.FileName = item.ContentType.Name.ToString();

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Size: " + item.Size.ToString());
                At.Size = item.Size;

                FixAttachedImageFormat(At);

                if (!(At.FileName.Split('.').Count() > 1))
                {
                    At = null;
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Se desestimo este adjunto debido a que no contiene formato");
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: Se desestimo este adjunto para evitar fallas en el sistema, es posible que se trate de una imagen dentro de la firma que no fue interpretada por el cliente IMAp adecuadamente.");
                }
            }
            else
            {
                At = new Attachment(item.Data);
                //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: SequenceNumber: " + eMail.SequenceNumber.ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: FileName: " + item.FileName.ToString());
                //At = imapClient.GetAttachment(eMail.SequenceNumber, item.FileName);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentId: " + At.ContentId.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.MediaType: " + item.ContentType.MediaType.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: ContentType.Name: " + At.ContentType.Name.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: DispositionType: " + At.DispositionType.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: FileName: " + At.ContentId.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Size: " + At.Size.ToString());
                At = item;

                FixAttachedImageFormat(At);

                if (!(At.FileName.Split('.').Count() > 1))
                {
                    At = null;
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Se desestimo este adjunto debido a que no contiene formato");
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[IMAP]: Item: Se desestimo este adjunto para evitar fallas en el sistema, es posible que se trate de una imagen dentro de la firma que no fue interpretada por el cliente IMAp adecuadamente.");
                }

            }

            if (item.ContentType.Name.Split('.').Last() == "rfc822")
            {
                Zamba.FileTools.MailPreview Msg = new Zamba.FileTools.SpireTools().ConvertEmlToMsg(item.Data);

                At.ContentId = item.ContentId;
                At.FileName = Msg.subject.ToString() != "" ? Msg.subject.ToString() + ".eml" : "SIN ASUNTO" + ".eml";

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: Caracteres ilegales quitados.");
                string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                foreach (char c in invalid)
                {
                    At.FileName = At.FileName.Replace(c.ToString(), "");
                }

                At.ContentType.Name = At.FileName;
                At.ContentType.MediaType = "message/rfc822";

                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: nombre de archivo final: " + At.FileName);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[IMAP]: Item: MediaType: " + At.ContentType.MediaType);
                Path_AttachmentsFixed.Add(At);
            }

            return At;

        }

        private static void FixAttachedImageFormat(Attachment At)
        {
            switch (At.ContentType.MediaType)
            {
                case "image/jpeg":
                    At.FileName += !(At.FileName.Split('.').Last() == "jpeg") ? ".jpeg" : "";
                    break;

                case "image/png":
                    At.FileName += !(At.FileName.Split('.').Last() == "png") ? ".png" : "";
                    break;

                default:
                    At.FileName = At.ContentType.Name;
                    break;
            }
        }

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


        private static void SaveMailFiles(Attachment At, string TemporaryPath_attachment)
        {
            byte[] buffer = new byte[8 * 1024];
            MemoryStream ms = new MemoryStream();

            int read = 0;
            while ((read = At.Data.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            File.WriteAllBytes(TemporaryPath_attachment, ms.ToArray());

            ms.Close();
            ms.Dispose();
        }

        public string saveEmailWithKit(string From, MailAddressCollection eMailcc, string eMailcco, MailAddressCollection eMailTo, string Subject,
            string Body, DateTime sendOn, AttachmentCollection Attachments, string TemporaryPath,
            List<string> Path_List)
        {
            try
            {
                using (var email = new MsgKit.Email(
                new MsgKit.Sender(From, string.Empty),
                Subject,
                false,
                false))
                {
                    foreach (MailAddress item in eMailTo)
                    {
                        email.Recipients.AddTo(item.Address);
                    }

                    foreach (MailAddress item in eMailcc)
                    {
                        email.Recipients.AddCc(item.Address);
                    }

                    email.Recipients.AddBcc(eMailcco);
                    email.Subject = Subject;
                    email.BodyText = Body;
                    email.BodyHtml = Body;
                    email.SentOn = sendOn.ToUniversalTime();
                    email.IconIndex = MsgKit.Enums.MessageIconIndex.ReceiptMail;

                    foreach (string item in Path_List)
                    {
                        email.Attachments.Add(item);
                    }

                    email.Save(TemporaryPath);
                    return TemporaryPath;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        private static INewResult InsertEMail(DTOObjectImap imapProcess, MailMessage eMail, string UniqueId, ref InsertResult insertResult, IResults_Business RB, string TempMsgPath)
        {
            try
            {
                INewResult newResult;

                newResult = RB.GetNewNewResult(imapProcess.Entidad);

                if (imapProcess.Codigo_mail != 0)
                {
                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                    string ExportCode = UserPreferencesFactory.getValueDB("IdAutoIncrementExporta", UPSections.ExportaPreferences, 0);
                    newResult.get_GetIndexById(imapProcess.Codigo_mail).DataTemp = ExportCode;
                    UserPreferencesFactory.setValueDB("IdAutoIncrementExporta", (int.Parse(ExportCode) + 1).ToString(), UPSections.ExportaPreferences, 0);
                }

                newResult.get_GetIndexById(imapProcess.Enviado_por).DataTemp = eMail.From.Address.ToString();

                string StringTo = "";

                foreach (MailAddress item in eMail.To)
                {
                    StringTo += item.Address;

                    if (eMail.To.Last() != item)
                        StringTo += "; ";
                }

                newResult.get_GetIndexById(imapProcess.Para).DataTemp = StringTo;

                if (imapProcess.Cc != 0)
                {
                    string StringCc = "";
                    foreach (MailAddress item in eMail.Cc)
                    {
                        StringCc += item.Address;

                        if (eMail.Cc.Last() != item)
                            StringCc += "; ";
                    }

                    newResult.get_GetIndexById(imapProcess.Cc).DataTemp = StringCc;
                }

                newResult.get_GetIndexById(imapProcess.Fecha).DataTemp = eMail.Date.ToString();
                newResult.get_GetIndexById(imapProcess.Asunto).DataTemp = eMail.Subject;

                MailAddress addressFrom = new MailAddress(eMail.From.ToString());

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

        public void MoveEmail(int sequenceNo, string folderName)
        {
            try
            {
                var a = imapClient.BeginCopy(sequenceNo, folderName, MoveCB);
                imapClient.EndCopy(a);

                var b = imapClient.BeginMarkAsDeleted(sequenceNo, DeleteCB);
                imapClient.EndMarkAsDeleted(b);
                imapClient.DeleteMarkedMessages();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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





    public class ImapMessageDTO : IImapMessageDTO
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

        public ImapMessageDTO(ImapMessage Obj)
        {
            this.Cc = string.Concat(Obj.Cc.Select(n => n.Address + "; ")
                    .AsEnumerable())
                    .TrimEnd(' ')
                    .TrimEnd(';');

            this.Date = Obj.Date.ToLocalTime().ToString();
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

            this.To = string.Concat(Obj.To.Select(n => n.Address + "; ")
                    .AsEnumerable())
                    .TrimEnd(' ')
                    .TrimEnd(';');

            this.UniqueId = Obj.UniqueId;
        }

        public ImapMessageDTO(ImapMessage Obj, AttachmentCollection Attachs) : this(Obj)
        {
            this.Attachments = Attachs;
        }
    }
}