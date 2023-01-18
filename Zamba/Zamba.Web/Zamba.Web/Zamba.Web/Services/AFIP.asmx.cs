using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using Zamba.Core;
using Zamba.FileTools;

namespace Zamba.Web.Services
{
    /// <summary>
    /// Summary description for AFIP
    /// </summary>
    [WebService(Namespace = "https://dg.modoc.com.ar/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AFIP : System.Web.Services.WebService
    {

        public  AFIP() {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.WebRS");
            }
        }

        [WebMethod(MessageName = "RequestDocumentDefault")]
        [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
        public string RequestDocument()
        {
            return "Hola Mundo";
        }

        [WebMethod(MessageName = "RequestDocument")]
        [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
        public  string RequestDocument( string token, string sign)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("token: {0}", token));
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("sign: {0}", sign));

                if (token == "" && sign == "")
                {
                    return "Hola Mundo";
                }

                if (token == "" && sign != "")
                {
                    return "Falta Parametro token";
                }
                if (token != "" && sign == "")
                {
                    return "Falta Parametro sign";
                }

                //ESTA AREA ES SOLO DE PRUEBA SI EL TOKEN Y SIGN NO VIENEN Y SE GENERAN EN BASE64

                if (token.Length == 0)
                {
                  var ticket = @"<?xml version='1.0'?><req version=""3.0""><id unique_id=""342423"" src=""30567867689"" dst=""30714439304"" gen_time=""1551719747"" exp_time=""1552151747""/><operation type=""[folder - req]"" response=""online""><folder id=""19001EC01009063X"" codigo=""000"" sigea="""" nro_ticket="""" hash=""HASH - INFO""><info-afip envio=""TRANSACCION_AFIP"" tipo=""Original"" situacion=""NoRatificado"" vigente=""S""/> <doctypes> <doctype id=""001"" /> <doctype id=""002"" /> </doctypes> </folder>  </operation> </req> ";

                    //Ver si hace falta o el ticket viene en base64 YA.
                    
                    using (var m = new MemoryStream())
                    {
                        token = Convert.ToBase64String(new UTF8Encoding().GetBytes(ticket));
                        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("ticketBase64: {0}", token));
                    }

                }
               
                //ACA COMIENZA EL PROCESO REAL.

                var certPath = @"c:\afipcert\afip.cer";
                certPath = ZOptBusiness.GetValueOrDefault("AFIPViewerCertPath", certPath);
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("AFIPViewerCertPath: {0}", certPath));

                byte[] tokenDecodeByte = Convert.FromBase64String(token);
                byte[] SignDecodeByte = Convert.FromBase64String(sign);

                req info;

                using (var stream = new MemoryStream(tokenDecodeByte))
                {
                    var serializer = new XmlSerializer(typeof(req));
                    using (var reader = XmlReader.Create(stream))
                    {
                        info = (req)serializer.Deserialize(reader);
                    }
                }


                string temptokenfile = @"c:\temp\ticket" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                StreamWriter sw1 = new StreamWriter(temptokenfile);
                sw1.WriteLine(token);
                sw1.Flush();
                sw1.Close();
                sw1.Dispose();

                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("tokenfile: {0}", temptokenfile));

                string tempsignfile = @"c:\temp\sign" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                StreamWriter sw2 = new StreamWriter(tempsignfile);
                sw2.WriteLine(sign);
                sw2.Flush();
                sw2.Close();
                sw2.Dispose();

                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));


                //Verificar la firma digital de la solicitud. Utilizando el correspondiente certificado digital
                //publicado por AFIP, deberá validar la firma del ticket de solicitud emitiendo, en caso de
                //no validación de la firma, el mensaje de error código PSAD.V.001.
                if (ValidarFirma(token, SignDecodeByte, certPath) == false)
                {
                    return  "PSAD.V.001.";
                }

                //Validar la generación del ticket.La generación del ticket no puede ser posterior a la
                //fecha actual. En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.002.
                if (ValidarFechaGeneracionTicket(info) == false)
                {
                    return "PSAD.V.002.";
                }

                //Validar la expiración del ticket.La expiración del ticket no puede ser anterior a la fecha
                //actual(ticket vencido).En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.003.

                if (ValidarFechaExpiracionTicket(info) == false)
                {
                    return  "PSAD.V.003.";
                }


                //4.Generar la respuesta a la solicitud realizada, de acuerdo a los parámetros solicitados.

                //5.Generar un registro de auditoria relacionado con la solicitud recibida.El mismo deberá
                //contener la fecha y hora de recepción de la solicitud, el identificador de la solicitud, el
                //Identificador del Legajo solicitado, el contenido completo del ticket de solicitud y el
                //resultado generado.En caso de respuesta exitosa código PSAD.V.000, caso contrario,
                //el código de error emitido. 

                // PSAD.V.004 Formato de SIGEA incorrecto El numero de SIGEA enviado como parametro no
                //tiene el formato esperado.
                if (ValidarFormatoSigea(info) == false)
                {
                    return  "PSAD.V.004.";
                }

                //PSAD.V.005 No existen imágenes No se encontraron imágenes digitalizadas para la
                //combinación de Legajo - Número de Carpeta-
                //Número Ticket - HASH - Familia.

                try
                {

                  byte[] fileBytes = ValidarExistenciaImagenesTicket(info);
                    if (fileBytes.Length > 0)
                    {
                      var document = Convert.ToBase64String(fileBytes);
                        
                        return document;
                    }
                    else
                    {
                        return  "PSAD.V.005.";
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return  "PSAD.V.005.";
                }


               
            }
            catch (Exception ex) 
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [WebMethod, ScriptMethod]
        public string  RequestDocumentB(string token, string sign)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("token: {0}", token));
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("sign: {0}", sign));

                if (token == "" && sign == "")
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("Hola Mundo");
                    return response.ToString();
                }

                if (token == "" && sign != "")
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("Falta Parametro token");
                    return response.ToString();

                }
                if (token != "" && sign == "")
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("Falta Parametro sign");
                    return response.ToString();

                }

                //ESTA AREA ES SOLO DE PRUEBA SI EL TOKEN Y SIGN NO VIENEN Y SE GENERAN EN BASE64

                if (token.Length == 0)
                {
                    var ticket = @"<?xml version='1.0'?><req version=""3.0""><id unique_id=""342423"" src=""30567867689"" dst=""30714439304"" gen_time=""1551719747"" exp_time=""1552151747""/><operation type=""[folder - req]"" response=""online""><folder id=""19001EC01009063X"" codigo=""000"" sigea="""" nro_ticket="""" hash=""HASH - INFO""><info-afip envio=""TRANSACCION_AFIP"" tipo=""Original"" situacion=""NoRatificado"" vigente=""S""/> <doctypes> <doctype id=""001"" /> <doctype id=""002"" /> </doctypes> </folder>  </operation> </req> ";

                    //Ver si hace falta o el ticket viene en base64 YA.

                    using (var m = new MemoryStream())
                    {
                        token = Convert.ToBase64String(new UTF8Encoding().GetBytes(ticket));
                        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("ticketBase64: {0}", token));
                    }

                }

                //ACA COMIENZA EL PROCESO REAL.

                var certPath = @"c:\afipcert\afip.cer";
                certPath = ZOptBusiness.GetValueOrDefault("AFIPViewerCertPath", certPath);
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("AFIPViewerCertPath: {0}", certPath));

                byte[] tokenDecodeByte = Convert.FromBase64String(token);
                byte[] SignDecodeByte = Convert.FromBase64String(sign);

                req info;

                using (var stream = new MemoryStream(tokenDecodeByte))
                {
                    var serializer = new XmlSerializer(typeof(req));
                    using (var reader = XmlReader.Create(stream))
                    {
                        info = (req)serializer.Deserialize(reader);
                    }
                }


                string temptokenfile = @"c:\temp\ticket" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                StreamWriter sw1 = new StreamWriter(temptokenfile);
                sw1.WriteLine(token);
                sw1.Flush();
                sw1.Close();
                sw1.Dispose();

                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("tokenfile: {0}", temptokenfile));

                string tempsignfile = @"c:\temp\sign" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                StreamWriter sw2 = new StreamWriter(tempsignfile);
                sw2.WriteLine(sign);
                sw2.Flush();
                sw2.Close();
                sw2.Dispose();

                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));


                //Verificar la firma digital de la solicitud. Utilizando el correspondiente certificado digital
                //publicado por AFIP, deberá validar la firma del ticket de solicitud emitiendo, en caso de
                //no validación de la firma, el mensaje de error código PSAD.V.001.
                if (ValidarFirma(token, SignDecodeByte, certPath) == false)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("PSAD.V.001.");
                    return response.ToString();

                }

                //Validar la generación del ticket.La generación del ticket no puede ser posterior a la
                //fecha actual. En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.002.
                if (ValidarFechaGeneracionTicket(info) == false)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("PSAD.V.002.");
                    return response.ToString();

                }

                //Validar la expiración del ticket.La expiración del ticket no puede ser anterior a la fecha
                //actual(ticket vencido).En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.003.

                if (ValidarFechaExpiracionTicket(info) == false)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("PSAD.V.003.");
                    return response.ToString();

                }


                //4.Generar la respuesta a la solicitud realizada, de acuerdo a los parámetros solicitados.

                //5.Generar un registro de auditoria relacionado con la solicitud recibida.El mismo deberá
                //contener la fecha y hora de recepción de la solicitud, el identificador de la solicitud, el
                //Identificador del Legajo solicitado, el contenido completo del ticket de solicitud y el
                //resultado generado.En caso de respuesta exitosa código PSAD.V.000, caso contrario,
                //el código de error emitido. 

                // PSAD.V.004 Formato de SIGEA incorrecto El numero de SIGEA enviado como parametro no
                //tiene el formato esperado.
                if (ValidarFormatoSigea(info) == false)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("PSAD.V.004.");
                    return response.ToString();

                }

                //PSAD.V.005 No existen imágenes No se encontraron imágenes digitalizadas para la
                //combinación de Legajo - Número de Carpeta-
                //Número Ticket - HASH - Familia.

                try
                {

                    byte[] fileBytes = ValidarExistenciaImagenesTicket(info);
                    if (fileBytes.Length > 0)
                    {
        //                var document = Convert.ToBase64String(fileBytes);

                        //content length for use in header
                        var contentLength = fileBytes.Length;
                        

                        //200
                        //successful
                        response.StatusCode = HttpStatusCode.OK;
                        response.Content = new StreamContent(new MemoryStream(fileBytes));
                        //response.Content = new ByteArrayContent(fileBytes);
             //           response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
             //           response.Content.Headers.ContentLength = contentLength;
                        //ContentDispositionHeaderValue contentDisposition = null;
                        //if (ContentDispositionHeaderValue.TryParse("inline; filename=" + info.operation.folder.id + ".pdf", out contentDisposition))
                        //{
                        //    response.Content.Headers.ContentDisposition = contentDisposition;
                        //}
            //            ContentDispositionHeaderValue contentDisposition = null;
            //            if (ContentDispositionHeaderValue.TryParse("attachment; filename=" + info.operation.folder.id + ".pdf", out contentDisposition))
             //           {
            //                response.Content.Headers.ContentDisposition = contentDisposition;
            //            }
                        return response.ToString();
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Content = new StringContent("PSAD.V.005.");
                        return response.ToString();

                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("PSAD.V.005.");
                    return response.ToString();

                }


              

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }

        }
        private bool ValidarFormatoSigea(req info)
        {
            try
            {
                if (info.operation.folder.sigea != null && info.operation.folder.sigea != string.Empty)
                {
                    if (info.operation.folder.sigea.Length != 11)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private bool ValidarFechaGeneracionTicket(req info)
        {
            try
            {
                if (info.id.gen_time == null)
                    return true;

                DateTime baseDate = new DateTime(1970, 1, 1);
                TimeSpan diff = DateTime.Now - baseDate;

                if (int.Parse(info.id.gen_time) > diff.TotalSeconds)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private bool ValidarFechaExpiracionTicket(req info)
        {
            try
            {
                if (info.id.exp_time == null)
                    return true;

                DateTime baseDate = new DateTime(1970, 1, 1);
                TimeSpan diff = DateTime.Now - baseDate;

                if (int.Parse(info.id.exp_time) >= diff.TotalSeconds)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private byte[] ValidarExistenciaImagenesTicket(req info)
        {
            try
            {
                var NroDespacho = info.operation.folder.id;
                // var CUITDespachante = info.operation.folder.cuit_desp;
                var codigo = info.operation.folder.codigo;
                var sigea = info.operation.folder.sigea;
                var nro_ticket = info.operation.folder.nro_ticket;
                var hash = info.operation.folder.hash;

                List<doctype> doctypes = info.operation.folder.doctypes;

                var query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo,
 i139590  familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id 
where i139548 = '{0}'", NroDespacho);

                if (doctypes == null || doctypes.Count == 0)
                {
                    //Se devuelven todas las familias
                }
                else
                {
                    List<string> familias = new List<string>();
                    foreach (doctype dt in doctypes)
                    {
                        familias.Add(dt.id);
                    }
                    var familiastr = string.Join(",", familias);
                    query = query + string.Format(" and  i139590 in ({0}) ", familiastr);
                }



                DataSet dsFiles = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

                string NewPDF = Membership.MembershipHelper.AppTempPath + "\\temp\\" + info.operation.folder.id + DateTime.Now.ToString("yyyyMMddHHmmss") +  ".pdf";

                List<string> Files = new List<string>();

                foreach (DataRow r in dsFiles.Tables[0].Rows)
                {
                    Files.Add(r["Archivo"].ToString());
                }

                GeneraPDFFromAnotherPDFs(Files, NewPDF);

                System.IO.FileStream dd = System.IO.File.OpenRead(NewPDF);
                byte[] Bytes = new byte[dd.Length];
                dd.Read(Bytes, 0, Bytes.Length);
                return Bytes;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("No existen imagenes para el ticket");
            }


        }

        public List<string> GeneraPDFFromAnotherPDFs(List<string> TempList, string PathToSave)
        {
            try
            {
                string[] list = TempList.ToArray();
                SpireTools ST = new SpireTools();
                ST.CreatePdfFromAnotherPdf(list, PathToSave);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            return new List<string>();
        }

        private bool ValidarFirma(string token, byte[] sign, string certPath)
        {
            return Verify(token, sign, certPath);
        }


        static bool Verify(string text, byte[] signature, string certPath)

        {
            try
            {

            // Load the certificate we'll use to verify the signature from a file

            X509Certificate2 cert = new X509Certificate2(certPath);

            // Note:

            // If we want to use the client cert in an ASP.NET app, we may use something like this instead:

            // X509Certificate2 cert = new X509Certificate2(Request.ClientCertificate.Certificate);


            // Get its associated CSP and public key

            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;


            // Hash the data

            SHA1Managed sha1 = new SHA1Managed();

            UnicodeEncoding encoding = new UnicodeEncoding();

                byte[] data = Convert.FromBase64String(text);

//                byte[] data = encoding.GetBytes(text);

            byte[] hash = sha1.ComputeHash(data);


            // Verify the signature with the hash

            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }

        }


        [XmlRoot("req")]

        public class req
        {
            [XmlAttribute("version")]
            public string version { get; set; }

            [XmlElement("id")]

            public id id { get; set; }

            [XmlElement("operation")]

            public operation operation { get; set; }
        }

        public class id
        {
            [XmlAttribute("unique_id")]
            public string unique_id { get; set; }

            [XmlAttribute("src")]
            public string src { get; set; }

            [XmlAttribute("dst")]
            public string dst { get; set; }

            [XmlAttribute("gen_time")]
            public string gen_time { get; set; }

            [XmlAttribute("exp_time")]
            public string exp_time { get; set; }


        }

        public class operation
        {
            [XmlAttribute("type")]
            public string type { get; set; }

            [XmlAttribute("response")]
            public string response { get; set; }

            [XmlAttribute("cuit_desp")]
            public string cuit_desp { get; set; }

            [XmlElement("folder")]
            public folder folder { get; set; }

            [XmlElement("trip")]
            public trip trip { get; set; }
        }

        public class folder
        {
            [XmlAttribute("id")]
            public string id { get; set; }
            [XmlAttribute("codigo")]
            public string codigo { get; set; }
            [XmlAttribute("sigea")]
            public string sigea { get; set; }
            [XmlAttribute("nro_ticket")]
            public string nro_ticket { get; set; }
            [XmlAttribute("hash")]
            public string hash { get; set; }

            [XmlElement("info-afip")]
            public infoafip infoafip { get; set; }
            public List<doctype> doctypes { get; set; }
        }

        public class infoafip
        {
            [XmlAttribute("envio")]
            public string envio { get; set; }

            [XmlAttribute("tipo")]
            public string tipo { get; set; }

            [XmlAttribute("situacion")]
            public string situacion { get; set; }

            [XmlAttribute("vigente")]
            public string vigente { get; set; }


        }


        public class doctype
        {
            [XmlAttribute("id")]
            public string id { get; set; }

        }

        public class trip
        {
            [XmlAttribute("id")]
            public string id { get; set; }

            [XmlAttribute("codigo")]
            public string codigo { get; set; }

            public List<doctype> doctypes { get; set; }

            [XmlElement("pages")]
            public pages pages { get; set; }
        }


        public class pages
        {
            [XmlAttribute("from")]
            public string from { get; set; }

            [XmlAttribute("count")]
            public string count { get; set; }
        }



    }
}
