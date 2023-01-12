using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Zamba.Core.Pop3Utilities;

namespace Zamba.Pop3Utilities
{
    public class ZPop3Client : IDisposable
    {
        #region CommandConstansFormat
        const string GET_MAILCOUNT = "STAT";
        const string GET_HEADER_FORMAT = "TOP {0} 0";
        const string GET_MAIL_FORMAT = "RETR {0}";
        const string STR_LOGOUT = "RSET";
        #endregion

        #region Members
        private bool disposed = false;
        private StringBuilder _sbCommands;
        #endregion

        #region Properties
        public string Host { get; protected set; }
        public int Port { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public bool IsSecure { get; protected set; }
        public TcpClient Client { get; protected set; }
        public Stream ClientStream { get; protected set; }
        public StreamWriter Writer { get; protected set; }
        public StreamReader Reader { get; protected set; } 
        #endregion

        #region Constructors
        /// <summary>
        /// Instancia el cliente pop3
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="secure"></param>
        public ZPop3Client(string host, int port, string email, string password, bool secure)
        {
            Host = host;
            Port = port;
            Email = email;
            Password = password;
            IsSecure = secure;
            _sbCommands = new StringBuilder();
        } 
        #endregion

        /// <summary>
        /// Conecta el cliente al servidor pop3 y logea la cuenta con la que se instancio
        /// </summary>
        public void Connect()
        {
            if (Client == null)
                Client = new TcpClient();
            if (!Client.Connected)
                Client.Connect(Host, Port);

            if (IsSecure)
            {
                SslStream secureStream = new SslStream(Client.GetStream());
                secureStream.AuthenticateAsClient(Host);
                ClientStream = secureStream;
                secureStream = null;
            }
            else
                ClientStream = Client.GetStream();

            Writer = new StreamWriter(ClientStream);
            Reader = new StreamReader(ClientStream);
            //Con esta linea levanta la conexion
            ReadLine();

            Login();
        }

        /// <summary>
        /// Obtiene la cantidad de correos que contiene la casilla
        /// </summary>
        /// <returns></returns>
        public int GetEmailCount()
        {
            int count = 0;
            //STAT devuelve "<estado de respuesta> <cantidad de mensajes> <tamaño de esos mensajes en bytes>
            string response = SendCommand(GET_MAILCOUNT);

            if (IsResponseOk(response))
            {
                //quita el estado de la respuesta y obtiene la primer parte la misma
                string[] arr = response.Substring(4).Split(' ');
                count = Convert.ToInt32(arr[0]);
            }
            else
                count = -1;

            return count;
        }
        
        /// <summary>
        /// Obtiene el header del mail por id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public IDownloadedEmailHeader FetchEmailHeader(int emailId)
        {
            _sbCommands.Length = 0;

            if (IsResponseOk(SendCommand(_sbCommands.AppendFormat(GET_HEADER_FORMAT, emailId).ToString())))
                 return MailUtilities.ParseHeader(ReadLines(),emailId);
            else
                return null;
        }

        /// <summary>
        /// Obtiene los header de parte del inbox.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<IDownloadedEmailHeader> FetchEmailList(int start, int count)
        {
            List<IDownloadedEmailHeader> emails = new List<IDownloadedEmailHeader>();
            int max = start + count;
            //Recorre los posibles ids de mails para ir trayendodolos
            for (int i = start; i < max; i++)
            {
                //Intenta obtener el mail
                IDownloadedEmailHeader email = FetchEmailHeader(i);
                //Si el mail existe(distinto de null) lo trae.
                if (email != null)
                    emails.Add(email);
            }
            return emails;
        }

        /// <summary>
        /// Obtiene los header de parte del inbox por rango de fechas.
        /// </summary>
        /// <param name="startDate">Menor valor de las dos fechas del rango</param>
        /// <param name="endDate">Mayor valor de las dos fechas del rango</param>
        /// <returns></returns>
        public List<IDownloadedEmailHeader> FetchEmailList(DateTime startDate, DateTime endDate)
        {
            List<IDownloadedEmailHeader> emails = new List<IDownloadedEmailHeader>();
            bool stopDateReached = false;
            IDownloadedEmailHeader currMail;

            // Comienza el recorrido de atras para adelante
            int mailId = this.GetEmailCount();
            
            //Mientras no se llegue al tope de las fechas o no queden mas mails
            while (!stopDateReached && mailId > 0)
            {
                //Busco el mail
                currMail = FetchEmailHeader(mailId);
                //Si existe
                if (currMail != null)
                {
                    //Si el mail esta dentro del intervalo de fechas, lo agrego a la lista
                    if (currMail.UtcDateTime >= startDate && currMail.UtcDateTime <= endDate)
                        emails.Add(currMail);
                    else//Sino verifico que si se llego al tope de fechas
                        if (currMail.UtcDateTime < startDate)
                            stopDateReached = true;
                }
                //Resto el id para obtener el proximo mail
                mailId--;
            }

            return emails;
        }

        /// <summary>
        /// Obtiene los posibles bodies del mail
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public List<IDownloadedEmailBody> FetchMessageParts(int emailId)
        {
            _sbCommands.Length = 0;

            if (IsResponseOk(SendCommand(_sbCommands.AppendFormat(GET_MAIL_FORMAT,emailId).ToString())))
                return MailUtilities.ParseMessageParts(ReadLines());

            return null;
        }

        /// <summary>
        /// Cierra las conexiones al servidor, desloguea la cuenta y libera recursos.
        /// </summary>
        public void Close()
        {
            //Cerrar el cliente tcp
            if (Client != null)
            {
                if (Client.Connected)
                    Logout();
                Client.Close();
                Client = null;
            }

            //Cerrar el stream que abre con el cliente
            if (ClientStream != null)
            {
                ClientStream.Close();
                ClientStream.Dispose();
                ClientStream = null;
            }

            //Cerrar el stream de comandos
            if (Writer != null)
            {
                Writer.Close();
                Writer.Dispose();
                Writer = null;
            }

            //Cerrar el stream de lectura de respuestas
            if (Reader != null)
            {
                Reader.Close();
                Reader.Dispose();
                Reader = null;
            }

            disposed = true;
        }

        /// <summary>
        /// Llama al metodo Close para liberar todos los recursos
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
                Close();
        }

        /// <summary>
        /// Envia las credenciales al servidor y verifica que la respuesta sea OK
        /// </summary>
        protected void Login()
        {
            //Verifico usuario
            if (!IsResponseOk(SendCommand("USER " + Email)))
                throw new Exception("User not accepted");

            //Verifico password
            if (!IsResponseOk(SendCommand("PASS " + Password)))
                throw new Exception("Password not accepted");
        }

        /// <summary>
        /// Desloguea el cliente del servidor
        /// </summary>
        protected void Logout()
        {
            SendCommand(STR_LOGOUT);
        }

        /// <summary>
        /// Envia el comando al servidor por medio del StreamWriter.
        /// Luego lee la siguiente linea para obtener la respuesta.
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        protected string SendCommand(string cmdtext)
        {
            Writer.WriteLine(cmdtext);
            Writer.Flush();
            return ReadLine();
        }

        /// <summary>
        /// Lee una linea del Stream Reader
        /// </summary>
        /// <returns></returns>
        protected string ReadLine()
        {
            return Reader.ReadLine() + "\r\n";
        }

        /// <summary>
        /// Lee las lineas del stream reader y las separa por \r\n
        /// </summary>
        /// <returns></returns>
        protected string ReadLines()
        {
            StringBuilder b = new StringBuilder();
            while (true)
            {
                string temp = ReadLine();
                if (temp == ".\r\n" || temp.IndexOf("-ERR") != -1)
                    break;
                b.Append(temp);
            }
            return b.ToString();
        }

        /// <summary>
        /// Verifica que la respuesta del servidor no haya dado error.
        /// Si la respuesta comienza con +OK es correcta, si comienza con -ERR no
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected static bool IsResponseOk(string response)
        {
            if (response.StartsWith("+OK"))
                return true;
            if (response.StartsWith("-ERR"))
                return false;

            throw new Exception("Cannot understand server response: " + response);
        }

        public IDownloadedEmailBody GetDecodedMailBody(int emailID)
        {
            List<IDownloadedEmailBody> msgParts = FetchMessageParts(emailID);

            IDownloadedEmailBody preferredMsgPart = MailUtilities.FindMessagePart(msgParts, "text/html");

            if (preferredMsgPart == null)
                preferredMsgPart = MailUtilities.FindMessagePart(msgParts, "text/plain");

            if (preferredMsgPart == null)
            {
                preferredMsgPart = MailUtilities.FindMessagePart(msgParts, "multipart/alternative");
                preferredMsgPart = MailUtilities.GetOnlyHtmlMultiPart(preferredMsgPart.MessageText);
            }

            if (preferredMsgPart == null && msgParts.Count > 0)
                preferredMsgPart = msgParts[0];

            string body = null;
            if (preferredMsgPart != null)
            {
                if (preferredMsgPart.ContentTransferEncoding != null)
                {
                    if (preferredMsgPart.ContentTransferEncoding.ToLower() == "base64")
                        body = MailUtilities.DecodeBase64String(preferredMsgPart.MessageText, preferredMsgPart.Charset);
                    else if (preferredMsgPart.ContentTransferEncoding.ToLower() == "quoted-printable")
                        body = MailUtilities.DecodeQuotedPrintableString(preferredMsgPart.MessageText, preferredMsgPart.Charset);
                    else
                        body = preferredMsgPart.MessageText;
                }
                else
                    body = preferredMsgPart.MessageText;
            }

            preferredMsgPart.MessageText = preferredMsgPart != null ? (preferredMsgPart.ContentType.IndexOf("text/plain") != -1 ? "<pre>" + MailUtilities.FormatUrls(body) + "</pre>" : body) : null;
            return preferredMsgPart;
        }
    }
}