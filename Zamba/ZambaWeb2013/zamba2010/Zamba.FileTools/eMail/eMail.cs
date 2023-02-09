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
using System.Reflection;

namespace Zamba.FileTools.eMail
{
    public class eMail
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

        //public string GetFilteredEmails(Dictionary<string, string> Params)
        public List<ImapMessageDTO> GetFilteredEmails(Dictionary<string, string> Params)
        {
            return GetMails_WithRules(ConnectToExchange(Params), Params);
        }

        /// <summary>
        /// Se conecta al servidor Exchange con una instancia nueva para luego obtener los correos.
        /// </summary>
        /// <param name="Params"></param>
        /// <returns>Los correos de la casilla predefinida mediante parametros</returns>
        public List<ImapMessageDTO> GetFilteredEmails(DTOObjectImap Params)
        {
            return GetMails_WithRules(ConnectToExchange(Params), Params);
        }

        /// <summary>
        /// Obtiene los correos del servidor Exchange con una instancia de conexion previa.
        /// </summary>
        /// <param name="Params"></param>
        /// <returns>Los correos de la casilla predefinida mediante parametros</returns>
        public List<ImapMessageDTO> GetFilteredEmails(DTOObjectImap Params, ImapClient ImapObj)
        {
            return GetMails_WithRules(ImapObj, Params);
        }

        //private String GetMails_WithRules(ImapClient ObjImap, Dictionary<string, string> Params)
        private List<ImapMessageDTO> GetMails_WithRules(ImapClient ObjImap, Dictionary<string, string> Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de GetMails_WithRules:");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "- Parametros: " + Params.ToString());

                //DATO HARDCODEADO
                ObjImap.Select(Params["Folder"]);
                List<ImapMessageDTO> List_ImapMessageDTO = new List<ImapMessageDTO>();

                if (bool.Parse(Params["Todos"]))
                {
                    foreach (ImapMessage item in ObjImap.GetAllMessageHeaders())
                    {
                        if (Val_ParamsToGetMails(ObjImap, Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(ObjImap, item.UniqueId)));
                    }
                }
                else if (bool.Parse(Params["Filtrado"]))
                {
                    foreach (ImapMessage item in FilterFolderSelected(ObjImap, Params["Filter_Field"], Params["Filter_Value"]))
                    {
                        if (Val_ParamsToGetMails(ObjImap, Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(ObjImap, item.UniqueId)));
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

        //private String GetMails_WithRules(ImapClient ObjImap, Dictionary<string, string> Params)
        private List<ImapMessageDTO> GetMails_WithRules(ImapClient ObjImap, DTOObjectImap Params)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecucion de GetMails_WithRules:");
                //ZTrace.WriteLineIf(ZTrace.IsInfo, "- Parametros: " + Params.ToString());

                //DATO HARDCODEADO
                ObjImap.Select(Params.Carpeta);
                List<ImapMessageDTO> List_ImapMessageDTO = new List<ImapMessageDTO>();

                if (Convert.ToInt32(Params.Filtrado.ToString()) == 0)
                {
                    foreach (ImapMessage item in ObjImap.GetAllMessageHeaders())
                    {
                        if (Val_ParamsToGetMails(ObjImap, Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(ObjImap, item.UniqueId)));
                    }
                }
                else if (Convert.ToInt32(Params.Filtrado.ToString()) == 0)
                {
                    foreach (ImapMessage item in FilterFolderSelected(ObjImap, Params.Filtro_campo, Params.Filtro_valor))
                    {
                        if (Val_ParamsToGetMails(ObjImap, Params, item))
                            List_ImapMessageDTO.Add(new ImapMessageDTO(item, ExtraerAdjuntos(ObjImap, item.UniqueId)));
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

        private bool Val_ParamsToGetMails(ImapClient ObjImap, Dictionary<string, string> Params, ImapMessage item)
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
                        if (TEST_RangodeTiempo(5, DateTime.Now, item.Date))
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
                    if (TEST_RangodeTiempo(5, DateTime.Now, item.Date))
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

        private bool Val_ParamsToGetMails(ImapClient ObjImap, DTOObjectImap Params, ImapMessage item)
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
                    if (TEST_RangodeTiempo(5, DateTime.Now, item.Date))
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
                if (TEST_RangodeTiempo(5, DateTime.Now, item.Date))
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

        public ImapClient ConnectToExchange(Dictionary<string, string> Params)
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

                ImapClient imap = new ImapClient(Host, Puerto, NomUsuario, Contraseña, Protocolo_conexion);

                imap.Connect();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");

                return imap;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]: Fallo la conexion al Exchange:" + ex.Message.ToString());
                throw ex;
            }
        }

        public ImapClient ConnectToExchange(DTOObjectImap Params)
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

                ImapClient imap = new ImapClient(Host, Puerto, NomUsuario, Contraseña, Protocolo_conexion);

                imap.Connect();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conexion al Exchange exitosa.");

                return imap;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]: Fallo la conexion al Exchange:" + ex.Message.ToString());
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

        public ImapMessageCollection FilterFolderSelected(ImapClient Obj, string Campo, string Valor)
        {
            return Obj.Search("'" + Campo + "' " + "Contains" + " '" + Valor + "'");
        }

        public AttachmentCollection ExtraerAdjuntos(ImapClient ObjImap, string UniqueId)
        {
            return ObjImap.GetFullMessage(UniqueId).Attachments; 
        }

        public void MoveEmail(object ObjImap, int sequenceNo, string folderName)
        {

            SpireTools sp = new SpireTools();
            sp.MoveEmail(ObjImap, sequenceNo, folderName);

        }


    }

    public class ListEmail {

        public string UniqueId { get; set; }
        public string Sender { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
    }

    public class ImapMessageDTO
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
