using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Xml.Linq;

namespace ClienteLoginCms
{
    /// <summary>
    /// Clase para crear objetos Login Tickets
    /// </summary>
    /// <remarks>
    /// Ver documentacion: 
    ///    Especificacion Tecnica del Webservice de Autenticacion y Autorizacion
    ///    Version 1.0
    ///    Departamento de Seguridad Informatica - AFIP
    /// </remarks>

    public class AuthenticationResponse
    {
        public uint UniqueId { get; internal set; }
        public DateTime GenerationTime { get; internal set; }
        public DateTime ExpirationTime { get; internal set; }
        public string Sign { get; internal set; }
        public string Token { get; internal set; }
        public string error { get; internal set; }
        public string cmsFirmadoBase64 { get; internal set; }
        public string loginTicketResponse { get; internal set; }
        public string argUrlWsaa { get; internal set; }
        public string XmlLoginTicketRequest { get; internal set; }
    }


    class LoginTicket
    {
        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que expira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta
        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
        private bool _verboseMode = true;
        private static UInt32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE

        // private static string loginTicketResponse = null;

        /// <summary>
        /// Construye un Login Ticket obtenido del WSAA
        /// </summary>
        /// <param name="argServicio">Servicio al que se desea acceder</param>
        /// <param name="argUrlWsaa">URL del WSAA</param>
        /// <param name="argRutaCertX509Firmante">Ruta del certificado X509 (con clave privada) usado para firmar</param>
        /// <param name="argPassword">Password del certificado X509 (con clave privada) usado para firmar</param>
        /// <param name="argProxy">IP:port del proxy</param>
        /// <param name="argProxyUser">Usuario del proxy</param>''' 
        /// <param name="argProxyPassword">Password del proxy</param>
        /// <param name="argVerbose">Nivel detallado de descripcion? true/false</param>
        /// <remarks></remarks>
        public string ObtenerLoginTicketResponse(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, SecureString argPassword, string argProxy, string argProxyUser, string argProxyPassword, bool argVerbose, out AuthenticationResponse authenticationResponse)
        {
            authenticationResponse = new AuthenticationResponse();

            const string ID_FNC = "[ObtenerLoginTicketResponse]";
            this.RutaDelCertificadoFirmante = argRutaCertX509Firmante;
            this._verboseMode = argVerbose;
            CertificadosX509Lib.VerboseMode = argVerbose;
            string cmsFirmadoBase64 = null;
            XmlNode xmlNodoUniqueId = default(XmlNode);
            XmlNode xmlNodoGenerationTime = default(XmlNode);
            XmlNode xmlNodoExpirationTime = default(XmlNode);
            XmlNode xmlNodoService = default(XmlNode);

            string loginTicketResponse = null;

            if (loginTicketResponse == null)
            {


                // PASO 1: Genero el Login Ticket Request
                try
                {
                    _globalUniqueID += 1;

                    XmlLoginTicketRequest = new XmlDocument();
                    XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                    xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                    xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                    xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                    xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");
                    xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                    xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                    xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
                    xmlNodoService.InnerText = argServicio;
                    this.Service = argServicio;

                    if (this._verboseMode) Console.WriteLine(XmlLoginTicketRequest.OuterXml);

                    authenticationResponse.XmlLoginTicketRequest = XmlLoginTicketRequest.OuterXml;

                }
                catch (Exception excepcionAlGenerarLoginTicketRequest)
                {
                    authenticationResponse.error = excepcionAlGenerarLoginTicketRequest.ToString();

                    throw new Exception(ID_FNC + "***Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.ToString());
                }

                // PASO 2: Firmo el Login Ticket Request
                try
                {
                    if (this._verboseMode) Console.WriteLine(ID_FNC + "***Leyendo certificado: {0}", RutaDelCertificadoFirmante);

                    X509Certificate2 certFirmante = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante, argPassword);

                    if (this._verboseMode)
                    {
                        Console.WriteLine(ID_FNC + "***Firmando: ");
                        Console.WriteLine(XmlLoginTicketRequest.OuterXml);
                    }

                    // Convierto el Login Ticket Request a bytes, firmo el msg y lo convierto a Base64
                    Encoding EncodedMsg = Encoding.UTF8;
                    byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);
                    byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
                    cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);

                    authenticationResponse.cmsFirmadoBase64 = cmsFirmadoBase64;

                }
                catch (Exception excepcionAlFirmar)
                {
                    authenticationResponse.error = excepcionAlFirmar.ToString();

                    throw new Exception(ID_FNC + "***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.ToString());
                }

                // PASO 3: Invoco al WSAA para obtener el Login Ticket Response
                try
                {
                    if (this._verboseMode)
                    {
                        Console.WriteLine(ID_FNC + "***Llamando al WSAA en URL: {0}", argUrlWsaa);
                        Console.WriteLine(ID_FNC + "***Argumento en el request:");
                        Console.WriteLine(cmsFirmadoBase64);
                    }

                    ClienteLoginCms_CS.Wsaa.LoginCMSService servicioWsaa = new ClienteLoginCms_CS.Wsaa.LoginCMSService();
                    servicioWsaa.Url = argUrlWsaa;

                    // Veo si hay que salir a traves de un proxy
                    if (argProxy != null)
                    {
                        servicioWsaa.Proxy = new WebProxy(argProxy, true);
                        if (argProxyUser != null)
                        {
                            NetworkCredential Credentials = new NetworkCredential(argProxyUser, argProxyPassword);
                            servicioWsaa.Proxy.Credentials = Credentials;
                        }
                    }

                    authenticationResponse.argUrlWsaa = argUrlWsaa;

                    loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64);

                    authenticationResponse.loginTicketResponse = loginTicketResponse;

                    if (this._verboseMode)
                    {
                        Console.WriteLine(ID_FNC + "***LoguinTicketResponse: ");
                        Console.WriteLine(loginTicketResponse);
                    }

                }
                catch (Exception excepcionAlInvocarWsaa)
                {
                    authenticationResponse.error = excepcionAlInvocarWsaa.ToString();

                    throw new Exception(ID_FNC + "***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.ToString());
                }

            }
            // PASO 4: Analizo el Login Ticket Response recibido del WSAA
            try
            {
                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);

                this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;

                authenticationResponse.UniqueId = UniqueId;
                authenticationResponse.GenerationTime = GenerationTime;
                authenticationResponse.ExpirationTime = ExpirationTime;
                authenticationResponse.Sign = Sign;
                authenticationResponse.Token = Token;

            }
            catch (Exception excepcionAlAnalizarLoginTicketResponse)
            {
                authenticationResponse.error = excepcionAlAnalizarLoginTicketResponse.ToString();

                throw new Exception(ID_FNC + "***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.ToString());
            }
            return loginTicketResponse;
        }
    }

    /// <summary>
    /// Libreria de utilidades para manejo de certificados
    /// </summary>
    /// <remarks></remarks>
    class CertificadosX509Lib
    {
        public static bool VerboseMode = false;

        /// <summary>
        /// Firma mensaje
        /// </summary>
        /// <param name="argBytesMsg">Bytes del mensaje</param>
        /// <param name="argCertFirmante">Certificado usado para firmar</param>
        /// <returns>Bytes del mensaje firmado</returns>
        /// <remarks></remarks>
        public static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
        {
            const string ID_FNC = "[FirmaBytesMensaje]";
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
                ContentInfo infoContenido = new ContentInfo(argBytesMsg);
                SignedCms cmsFirmado = new SignedCms(infoContenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante
                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                if (VerboseMode) Console.WriteLine(ID_FNC + "***Firmando bytes del mensaje...");

                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                if (VerboseMode) Console.WriteLine(ID_FNC + "***OK mensaje firmado");

                // Encodeo el mensaje PKCS #7.
                return cmsFirmado.Encode();
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception(ID_FNC + "***Error al firmar: " + excepcionAlFirmar.Message);
            }
        }

        /// <summary>
        /// Lee certificado de disco
        /// </summary>
        /// <param name="argArchivo">Ruta del certificado a leer.</param>
        /// <returns>Un objeto certificado X509</returns>
        /// <remarks></remarks>
        public static X509Certificate2 ObtieneCertificadoDesdeArchivo(string argArchivo, SecureString argPassword)
        {
            const string ID_FNC = "[ObtieneCertificadoDesdeArchivo]";
            X509Certificate2 objCert = new X509Certificate2();
            try
            {
                if (argPassword.IsReadOnly())
                {
                    objCert.Import(File.ReadAllBytes(argArchivo), argPassword, X509KeyStorageFlags.PersistKeySet);
                }
                else
                {
                    objCert.Import(File.ReadAllBytes(argArchivo));
                }
                return objCert;
            }
            catch (Exception excepcionAlImportarCertificado)
            {
                throw new Exception(ID_FNC + "***Error al leer certificado: " + excepcionAlImportarCertificado.Message);
            }
        }
    }

    /// <summary>
    /// Clase principal
    /// </summary>
    /// <remarks></remarks>
    public class tokenServiceAfip
    {
        public string serviceName;
        public string token;
        public string sign;
        public DateTime expiredate;
    }
    public static class ProgramaPrincipal
    {
        // Valores por defecto, globales en esta clase
        public static string DEFAULT_URLWSAAWSDL = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        public static string DEFAULT_SERVICIO = "wDigDepFiel";
        public static string DEFAULT_CERTSIGNER = @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx";
        public static string DEFAULT_CERTSIGNER_PASSWORD = "modoc";
        public static string DEFAULT_PROXY = null;
        public static string DEFAULT_PROXY_USER = null;
        public static string DEFAULT_PROXY_PASSWORD = null;
        public static bool DEFAULT_VERBOSE = true;

        public static void Main(string[] args)
        {
            Autenticacion();
        }


        private static List<tokenServiceAfip> LstTokenServicesAfip = new List<tokenServiceAfip>();

        public static Dictionary<string, string> Autenticacion()
        {
            if (LstTokenServicesAfip.Where(n => n.serviceName == DEFAULT_SERVICIO).ToList().Count == 1)
            {
                tokenServiceAfip tokenServicio = LstTokenServicesAfip.Where(n => n.serviceName == DEFAULT_SERVICIO).ToList().First();
                if (tokenServicio.token != null && tokenServicio.token != string.Empty && tokenServicio.sign != null && tokenServicio.sign != string.Empty && tokenServicio.expiredate >= DateTime.Now)
                {
                    var result = new Dictionary<string, string>();
                    result["token"] = tokenServicio.token;
                    result["sign"] = tokenServicio.sign;
                    return result;
                }
                else
                {
                    LstTokenServicesAfip.Remove(tokenServicio);
                }
            }
            const string ID_FNC = "[Main]";

            string strUrlWsaaWsdl = DEFAULT_URLWSAAWSDL;
            string strIdServicioNegocio = DEFAULT_SERVICIO;
            string strRutaCertSigner = DEFAULT_CERTSIGNER;
            SecureString strPasswordSecureString = new SecureString();

            foreach (var item in DEFAULT_CERTSIGNER_PASSWORD.ToCharArray())
            {
                strPasswordSecureString.AppendChar(item);
            }
            strPasswordSecureString.MakeReadOnly();


            string strProxy = DEFAULT_PROXY;
            string strProxyUser = DEFAULT_PROXY_USER;
            string strProxyPassword = DEFAULT_PROXY_PASSWORD;
            bool blnVerboseMode = DEFAULT_VERBOSE;


            // Argumentos OK, entonces procesar normalmente...

            LoginTicket objTicketRespuesta = null;
            string strTicketRespuesta = null;


            try
            {
                objTicketRespuesta = new LoginTicket();
                if (blnVerboseMode) Console.WriteLine(ID_FNC + "***Accediendo a {0}", strUrlWsaaWsdl);

                AuthenticationResponse authenticationResponse = null;

                strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, strProxy, strProxyUser, strProxyPassword, blnVerboseMode, out authenticationResponse);
                string token, sign;
                DateTime expiredate;
                token = ((System.Xml.Linq.XElement)((System.Xml.Linq.XElement)XElement.Parse(strTicketRespuesta).LastNode).FirstNode).Value;
                sign = ((System.Xml.Linq.XElement)((System.Xml.Linq.XContainer)XElement.Parse(strTicketRespuesta).LastNode).LastNode).Value;
                expiredate = DateTime.Parse(((System.Xml.Linq.XElement)((System.Xml.Linq.XContainer)XElement.Parse(strTicketRespuesta).FirstNode).LastNode).Value);
                LstTokenServicesAfip.Add(new tokenServiceAfip()
                {
                    serviceName = DEFAULT_SERVICIO,
                    sign = sign,
                    token = token,
                    expiredate = expiredate
                });
                var result = new Dictionary<string, string>();
                result["token"] = token;
                result["sign"] = sign;
                return result;

            }
            catch (Exception excepcionAlObtenerTicket)
            {
                throw new Exception(ID_FNC + "***EXCEPCION AL OBTENER TICKET: " + excepcionAlObtenerTicket.Message);

            }

        }

    }
}
