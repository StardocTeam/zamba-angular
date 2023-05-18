using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Framework;
using Zamba.Data;
using ZambaWeb.RestApi.Controllers.Web;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.FileTools;
using System.IO;
using Zamba.Membership;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Web.Security;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    //[Authorize]
    public class AfipController : ApiController
    {
        public AfipController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }


        public class Update
        {
            [Required]
            public string token { get; set; }
            [Required]
            public string sign { get; set; }
        }

        [Route("api/afip/RequestDocument")]
        [HttpPost, HttpGet]
        [ActionName("RequestDocument")]
        public HttpResponseMessage RequestDocument() //[FromBody] Update request
        {

            ZTrace.WriteLineIf(ZTrace.IsError, "Ingreso al Metodo");

            var jsonContent2 = Request.Content.ReadAsStringAsync().Result;

            string content = HttpUtility.UrlDecode(jsonContent2);

            string[] contentArray = null;
            if (content != null && content.Length > 0)
            {
                contentArray = content.Split(char.Parse("&"));
            }
            string token = string.Empty;
            string sign = string.Empty;

            if (contentArray != null && contentArray.Length > 0)
            {
                token = content.Split(char.Parse("&"))[0].Replace("token=", "");
                sign = content.Split(char.Parse("&"))[1].Replace("sign=", "");
            }

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("token: {0}", token));
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("sign: {0}", sign));

                if (token == "" && sign == "")
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "V001.1");

                    var responseHi = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.001")
                    };
                    ZClass.raiseerror(new Exception("PSAD.V.001"));
                    throw new HttpResponseException(responseHi);

                }

                if (token == "" && sign != "")
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Falta Parametro token");
                    var responseFT = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Falta Parametro token")
                    };

                    ZClass.raiseerror(new Exception("Falta parametro Token"));
                    throw new HttpResponseException(responseFT);
                }
                if (token != "" && sign == "")
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Falta Parametro sign");
                    var responseFS = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Falta Parametro sign")
                    };

                    ZClass.raiseerror(new Exception("Falta parametro Sign"));
                    throw new HttpResponseException(responseFS);
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

                req info;

                byte[] tokenDecodeByte = null;
                byte[] SignDecodeByte = null;

                try
                {
                    tokenDecodeByte = Convert.FromBase64String(token);
                    SignDecodeByte = Convert.FromBase64String(sign);


                    using (var stream = new MemoryStream(tokenDecodeByte))
                    {
                        var serializer = new XmlSerializer(typeof(req));
                        using (var reader = XmlReader.Create(stream))
                        {
                            info = (req)serializer.Deserialize(reader);
                        }
                    }


                    try
                    {



                        string temptokenfile = @"c:\temp\token" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".xml";
                        StreamWriter sw1 = new StreamWriter(temptokenfile);
                        sw1.WriteLine(token);
                        sw1.Flush();
                        sw1.Close();
                        sw1.Dispose();

                        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("tokenfile: {0}", temptokenfile));

                        string tempsignfile = @"c:\temp\sign" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".xml";
                        StreamWriter sw2 = new StreamWriter(tempsignfile);
                        sw2.WriteLine(sign);
                        sw2.Flush();
                        sw2.Close();
                        sw2.Dispose();

                        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));



                        //string temptokenfiles = @"c:\temp\ticket" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".txt";
                        //StreamWriter sw1s = new StreamWriter(temptokenfiles);
                        //string ts = Encoding.UTF8.GetString(tokenDecodeByte);
                        //sw1s.WriteLine(ts);
                        //sw1s.Flush();
                        //sw1s.Close();
                        //sw1s.Dispose();


                        //string tempsignfiles = @"c:\temp\sign" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".txt";
                        //StreamWriter sw2s = new StreamWriter(tempsignfiles);
                        //string ss = Encoding.UTF8.GetString(SignDecodeByte);
                        //sw2s.WriteLine(ss);
                        //sw2s.Flush();
                        //sw2s.Close();
                        //sw2s.Dispose();

                        //ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));

                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    var response1 = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.001.")
                    };
                    throw new HttpResponseException(response1);
                }


                //Verificar la firma digital de la solicitud. Utilizando el correspondiente certificado digital
                //publicado por AFIP, deberá validar la firma del ticket de solicitud emitiendo, en caso de
                //no validación de la firma, el mensaje de error código PSAD.V.001.
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma");

                if (ValidarFirma(token, SignDecodeByte, certPath) == false)
                {
                    var response1 = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.001.")
                    };

                    ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma ERROR");
                    ZClass.raiseerror(new Exception("Validacion de Firma ERROR"));
                    throw new HttpResponseException(response1);
                }

                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma OK");

                //Validar la generación del ticket.La generación del ticket no puede ser posterior a la
                //fecha actual. En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.002.
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion");

                if (ValidarFechaGeneracionTicket(info) == false)
                {
                    var response2 = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.002.")
                    };

                    ZClass.raiseerror(new Exception("PSAD.V.002."));
                    throw new HttpResponseException(response2);
                }


                //Validar la expiración del ticket.La expiración del ticket no puede ser anterior a la fecha
                //actual(ticket vencido).En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.003.

                if (ValidarFechaExpiracionTicket(info) == false)
                {
                    var response3 = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.003.")
                    };


                    ZClass.raiseerror(new Exception("PSAD.V.003."));
                    throw new HttpResponseException(response3);
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
                    var response4 = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.004.")
                    };
                    response4.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                    response4.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                    };

                    ZClass.raiseerror(new Exception("PSAD.V.004."));
                    throw new HttpResponseException(response4);
                }

                //PSAD.V.005 No existen imágenes No se encontraron imágenes digitalizadas para la
                //combinación de Legajo - Número de Carpeta-
                //Número Ticket - HASH - Familia.

                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "ValidarExistenciaImagenesTicket");

                    byte[] fileBytes = ValidarExistenciaImagenesTicket(info);
                    if (fileBytes.Length > 0)
                    {
                        //                        var document = Convert.ToBase64String(fileBytes);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado OK");

                        var response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(fileBytes)
                        };
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                        {
                            FileName = String.Format(info.operation.folder.id.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmssfff"))

                        };


                        response.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelve archivo");
                        return response;
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado con error");
                        var response5 = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("PSAD.V.005.")
                        };
                        response5.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                        response5.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        throw new HttpResponseException(response5);

                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    var responsee = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.005.")
                    };
                    responsee.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                    responsee.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                    };

                    throw new HttpResponseException(responsee);
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }



        [Route("api/afip/RequestDocumentTest")]
        [HttpPost, HttpGet]
        [ActionName("RequestDocumentTest")]
        public HttpResponseMessage RequestDocumentTest() //[FromBody] Update request
        {

            ZTrace.WriteLineIf(ZTrace.IsError, "Ingreso al Metodo");

            var jsonContent2 = Request.RequestUri.Query;

            string content = HttpUtility.UrlDecode(jsonContent2);

            string[] contentArray = null;
            if (content != null && content.Length > 0)
            {
                contentArray = content.Split(char.Parse("&"));
            }
            string token = string.Empty;
            string sign = string.Empty;

            if (contentArray != null && contentArray.Length > 0)
            {
                token = content.Split(char.Parse("&"))[0].Replace("?token=", "");
                sign = content.Split(char.Parse("&"))[1].Replace("sign=", "");
            }

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("token: {0}", token));
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("sign: {0}", sign));

                //if (token == "" && sign == "")
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsError, "V001.1");

                //    var responseHi = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.001")
                //    };
                //    ZClass.raiseerror(new Exception("PSAD.V.001"));
                //    throw new HttpResponseException(responseHi);

                //}

                //if (token == "" && sign != "")
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsError, "Falta Parametro token");
                //    var responseFT = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("Falta Parametro token")
                //    };

                //    ZClass.raiseerror(new Exception("Falta parametro Token"));
                //    throw new HttpResponseException(responseFT);
                //}
                //if (token != "" && sign == "")
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsError, "Falta Parametro sign");
                //    var responseFS = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("Falta Parametro sign")
                //    };

                //    ZClass.raiseerror(new Exception("Falta parametro Sign"));
                //    throw new HttpResponseException(responseFS);
                //}

                //ESTA AREA ES SOLO DE PRUEBA SI EL TOKEN Y SIGN NO VIENEN Y SE GENERAN EN BASE64

                //if (token.Length == 0)
                //{
                //    var ticket = @"<?xml version='1.0'?><req version=""3.0""><id unique_id=""342423"" src=""30567867689"" dst=""30714439304"" gen_time=""1551719747"" exp_time=""1552151747""/><operation type=""[folder - req]"" response=""online""><folder id=""19001EC01009063X"" codigo=""000"" sigea="""" nro_ticket="""" hash=""HASH - INFO""><info-afip envio=""TRANSACCION_AFIP"" tipo=""Original"" situacion=""NoRatificado"" vigente=""S""/> <doctypes> <doctype id=""001"" /> <doctype id=""002"" /> </doctypes> </folder>  </operation> </req> ";

                //    //Ver si hace falta o el ticket viene en base64 YA.

                //    using (var m = new MemoryStream())
                //    {
                //        token = Convert.ToBase64String(new UTF8Encoding().GetBytes(ticket));
                //        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("ticketBase64: {0}", token));
                //    }

                //}

                //ACA COMIENZA EL PROCESO REAL.

                var certPath = @"c:\afipcert\afip.cer";
                certPath = ZOptBusiness.GetValueOrDefault("AFIPViewerCertPath", certPath);
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("AFIPViewerCertPath: {0}", certPath));

                req info = new req();
                info.operation = new operation();
                info.operation.folder = new folder();
                //TEST HARDCODED
                

                if (contentArray != null && contentArray.Length > 0)
                {
                    info.operation.folder.id = content.Split(char.Parse("&"))[2].Replace("d=", "");
                    info.operation.folder.codigo = content.Split(char.Parse("&"))[3].Replace("c=", "");
                    info.operation.folder.sigea = content.Split(char.Parse("&"))[4].Replace("s=", "");

                }
                //info.id = "1";
                //var NroDespacho = info.operation.folder.id;
                // var CUITDespachante = info.operation.folder.cuit_desp;
                //ZTrace.WriteLineIf(ZTrace.IsError, "NroDespacho: " + NroDespacho);
                //var codigo = info.operation.folder.codigo;
                //ZTrace.WriteLineIf(ZTrace.IsError, "codigo: " + codigo);
                //var sigea = info.operation.folder.sigea;
                //ZTrace.WriteLineIf(ZTrace.IsError, "sigea: " + sigea);
                //var nro_ticket = info.operation.folder.nro_ticket;
                //ZTrace.WriteLineIf(ZTrace.IsError, "nro_ticket: " + nro_ticket);
                //var hash = info.operation.folder.hash;
                //ZTrace.WriteLineIf(ZTrace.IsError, "hash: " + hash);


                byte[] tokenDecodeByte = null;
                byte[] SignDecodeByte = null;

                //try
                //{
                //    tokenDecodeByte = Convert.FromBase64String(token);
                //    SignDecodeByte = Convert.FromBase64String(sign);


                //    using (var stream = new MemoryStream(tokenDecodeByte))
                //    {
                //        var serializer = new XmlSerializer(typeof(req));
                //        using (var reader = XmlReader.Create(stream))
                //        {
                //            info = (req)serializer.Deserialize(reader);
                //        }
                //    }


                //    try
                //    {



                //        string temptokenfile = @"c:\temp\token" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".xml";
                //        StreamWriter sw1 = new StreamWriter(temptokenfile);
                //        sw1.WriteLine(token);
                //        sw1.Flush();
                //        sw1.Close();
                //        sw1.Dispose();

                //        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("tokenfile: {0}", temptokenfile));

                //        string tempsignfile = @"c:\temp\sign" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".xml";
                //        StreamWriter sw2 = new StreamWriter(tempsignfile);
                //        sw2.WriteLine(sign);
                //        sw2.Flush();
                //        sw2.Close();
                //        sw2.Dispose();

                //        ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));



                //        //string temptokenfiles = @"c:\temp\ticket" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".txt";
                //        //StreamWriter sw1s = new StreamWriter(temptokenfiles);
                //        //string ts = Encoding.UTF8.GetString(tokenDecodeByte);
                //        //sw1s.WriteLine(ts);
                //        //sw1s.Flush();
                //        //sw1s.Close();
                //        //sw1s.Dispose();


                //        //string tempsignfiles = @"c:\temp\sign" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".txt";
                //        //StreamWriter sw2s = new StreamWriter(tempsignfiles);
                //        //string ss = Encoding.UTF8.GetString(SignDecodeByte);
                //        //sw2s.WriteLine(ss);
                //        //sw2s.Flush();
                //        //sw2s.Close();
                //        //sw2s.Dispose();

                //        //ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("signfile: {0}", tempsignfile));

                //    }
                //    catch (Exception ex)
                //    {
                //        ZClass.raiseerror(ex);
                //    }

                //}
                //catch (Exception ex)
                //{
                //    ZClass.raiseerror(ex);
                //    var response1 = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.001.")
                //    };
                //    throw new HttpResponseException(response1);
                //}


                //Verificar la firma digital de la solicitud. Utilizando el correspondiente certificado digital
                //publicado por AFIP, deberá validar la firma del ticket de solicitud emitiendo, en caso de
                //no validación de la firma, el mensaje de error código PSAD.V.001.
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma");

                //if (ValidarFirma(token, SignDecodeByte, certPath) == false)
                //{
                //    var response1 = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.001.")
                //    };

                //    ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma ERROR");
                //    ZClass.raiseerror(new Exception("Validacion de Firma ERROR"));
                //    throw new HttpResponseException(response1);
                //}

                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Firma OK");

                //Validar la generación del ticket.La generación del ticket no puede ser posterior a la
                //fecha actual. En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.002.
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion");

                //if (ValidarFechaGeneracionTicket(info) == false)
                //{
                //    var response2 = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.002.")
                //    };

                //    ZClass.raiseerror(new Exception("PSAD.V.002."));
                //    throw new HttpResponseException(response2);
                //}


                //Validar la expiración del ticket.La expiración del ticket no puede ser anterior a la fecha
                //actual(ticket vencido).En caso de no validación, emitirá el mensaje de error código
                //PSAD.V.003.

                //if (ValidarFechaExpiracionTicket(info) == false)
                //{
                //    var response3 = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.003.")
                //    };


                //    ZClass.raiseerror(new Exception("PSAD.V.003."));
                //    throw new HttpResponseException(response3);
                //}

                //4.Generar la respuesta a la solicitud realizada, de acuerdo a los parámetros solicitados.

                //5.Generar un registro de auditoria relacionado con la solicitud recibida.El mismo deberá
                //contener la fecha y hora de recepción de la solicitud, el identificador de la solicitud, el
                //Identificador del Legajo solicitado, el contenido completo del ticket de solicitud y el
                //resultado generado.En caso de respuesta exitosa código PSAD.V.000, caso contrario,
                //el código de error emitido. 

                // PSAD.V.004 Formato de SIGEA incorrecto El numero de SIGEA enviado como parametro no
                //tiene el formato esperado.
                //if (ValidarFormatoSigea(info) == false)
                //{
                //    var response4 = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent("PSAD.V.004.")
                //    };
                //    response4.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                //    response4.Headers.CacheControl = new CacheControlHeaderValue()
                //    {
                //        MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                //    };

                //    ZClass.raiseerror(new Exception("PSAD.V.004."));
                //    throw new HttpResponseException(response4);
                //}

                //PSAD.V.005 No existen imágenes No se encontraron imágenes digitalizadas para la
                //combinación de Legajo - Número de Carpeta-
                //Número Ticket - HASH - Familia.

                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "ValidarExistenciaImagenesTicket");

                    byte[] fileBytes = ValidarExistenciaImagenesTicket(info);
                    if (fileBytes.Length > 0)
                    {
                        //                        var document = Convert.ToBase64String(fileBytes);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado OK");

                        var response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(fileBytes)
                        };
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                        {
                            FileName = String.Format(info.operation.folder.id.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmssfff"))

                        };


                        response.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelve archivo");
                        return response;
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado con error");
                        var response5 = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("PSAD.V.005.")
                        };
                        response5.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                        response5.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        throw new HttpResponseException(response5);

                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    var responsee = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.005.")
                    };
                    responsee.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                    responsee.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                    };

                    throw new HttpResponseException(responsee);
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [Route("api/afip/TestDocument")]
        [HttpPost, HttpGet]
        [ActionName("TestDocument")]
        public HttpResponseMessage TestDocument(string pedido) //[FromBody] Update request
        {

            ZTrace.WriteLineIf(ZTrace.IsError, "Ingreso al Metodo");

            var despacho = pedido.Split(char.Parse("|"))[0];
            var codigo = pedido.Split(char.Parse("|"))[1];
            var sigea = pedido.Split(char.Parse("|"))[2];
            var familias = pedido.Split(char.Parse("|"))[3].Split(char.Parse(","));

            List<doctype> familiasList = new List<doctype>();
            foreach (string f in familias)
            {
                if (f != string.Empty)
                {
                    doctype d = new doctype();
                    d.id = f;
                    familiasList.Add(d);
                }
            }

            try
            {


                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "ValidarExistenciaImagenesTicket");

                    byte[] fileBytes = ValidarExistenciaImagenesTicketTest(despacho, codigo, sigea, familiasList);
                    if (fileBytes.Length > 0)
                    {
                        //                        var document = Convert.ToBase64String(fileBytes);
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado OK");

                        var response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(fileBytes)
                        };
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                        {
                            FileName = String.Format(despacho + DateTime.Now.ToString("ddMMyyyy_HHmmssfff"))

                        };


                        response.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelve archivo");
                        return response;
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Archivo generado con error");
                        var response5 = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("PSAD.V.005.")
                        };
                        response5.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                        response5.Headers.CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                        };

                        throw new HttpResponseException(response5);

                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    var responsee = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("PSAD.V.005.")
                    };
                    responsee.Content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                    responsee.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                    };

                    throw new HttpResponseException(responsee);
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
                    ZTrace.WriteLineIf(ZTrace.IsError, "SIGEA: " + info.operation.folder.sigea);
                    //if (info.operation.folder.sigea.Length < 11)
                    //{
                    //    ZTrace.WriteLineIf(ZTrace.IsError, "SIGEA ERROR: " + info.operation.folder.sigea);
                    //    return false;
                    //}
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
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: gen_time: NULA");
                    return false;
                }
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: gen_time: " + info.id.gen_time);
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: gen_timeI: " + int.Parse(info.id.gen_time).ToString());

                DateTime baseDate = new DateTime(1970, 1, 1);
                TimeSpan diff = DateTime.UtcNow - baseDate;
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: diff: " + diff.TotalSeconds.ToString());

                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: gen_time: " + baseDate.AddSeconds(int.Parse(info.id.gen_time)).ToString("dd/MM/yyyy HH:mm:ss:sss"));
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: NOW: " + DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss:sss"));


                if (int.Parse(info.id.gen_time) > diff.TotalSeconds)
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: gen_time mayor que ahora");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: diff: ERROR");
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private bool ValidarFechaExpiracionTicket(req info)
        {
            try
            {
                if (info.id.exp_time == null)
                    return false;

                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: exp_time: " + info.id.exp_time);

                DateTime baseDate = new DateTime(1970, 1, 1);
                TimeSpan diff = DateTime.UtcNow - baseDate;

                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: exp_time: " + baseDate.AddSeconds(int.Parse(info.id.exp_time)).ToString("dd/MM/yyyy HH:mm:ss:sss"));
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Generacion: NOW: " + DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss:sss"));

                if (int.Parse(info.id.exp_time) < diff.TotalSeconds)
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Expiracion: exp_time menor que ahora");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Validacion de Fecha Expiracion: diff: ERROR");

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
                ZTrace.WriteLineIf(ZTrace.IsError, "NroDespacho: " + NroDespacho);
                var codigo = info.operation.folder.codigo;
                ZTrace.WriteLineIf(ZTrace.IsError, "codigo: " + codigo);
                var sigea = info.operation.folder.sigea;
                ZTrace.WriteLineIf(ZTrace.IsError, "sigea: " + sigea);
                var nro_ticket = info.operation.folder.nro_ticket;
                ZTrace.WriteLineIf(ZTrace.IsError, "nro_ticket: " + nro_ticket);
                var hash = info.operation.folder.hash;
                ZTrace.WriteLineIf(ZTrace.IsError, "hash: " + hash);


                List<doctype> doctypes = info.operation.folder.doctypes;
                var query = string.Empty;

                if (codigo == "004" || codigo == "002" || codigo == "003")
                {
                     query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo,  i139590  familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", NroDespacho, codigo,sigea);
                }
                else
                {
                     query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo, i139590  familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id where i139548 = '{0}' and i139603 = '{1}' ", NroDespacho, codigo);
                }

                if (doctypes == null || doctypes.Count == 0)
                {
                    //Se devuelven todas las familias
                    ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelven todas las familias: ");
                }
                else
                {
                    List<string> familias = new List<string>();
                    foreach (doctype dt in doctypes)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelve la familia: " + dt.id.ToString());
                        familias.Add(dt.id);
                    }
                    var familiastr = string.Join(",", familias);
                    query = query + string.Format(" and  i139590 in ({0}) ", familiastr);
                }

                query = query + string.Format("order by i139609");


                DataSet dsFiles = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

                string NewPDF = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\" + info.operation.folder.id + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                List<string> Files = new List<string>();

                foreach (DataRow r in dsFiles.Tables[0].Rows)
                {
                    Files.Add(r["Archivo"].ToString());
                    ZTrace.WriteLineIf(ZTrace.IsError, "Se agrega archivo: " + r["Archivo"].ToString());

                }

                ZTrace.WriteLineIf(ZTrace.IsError, "Se encontraron archivos cantidad: " + dsFiles.Tables[0].Rows.Count);

                GeneraPDFFromAnotherPDFs(Files, NewPDF);

                ZTrace.WriteLineIf(ZTrace.IsError, "Se genero el PDF: " + NewPDF);

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

        private byte[] ValidarExistenciaImagenesTicketTest(string NroDespacho, string codigo, string sigea, List<doctype> doctypes)
        {
            try
            {

                ZTrace.WriteLineIf(ZTrace.IsError, "NroDespacho: " + NroDespacho);
                ZTrace.WriteLineIf(ZTrace.IsError, "codigo: " + codigo);
                ZTrace.WriteLineIf(ZTrace.IsError, "sigea: " + sigea);


                var query = string.Empty;

                if (codigo == "004" || codigo == "002" || codigo == "003")
                {
                    query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo,  i139590  familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", NroDespacho, codigo, sigea);
                }
                else
                {
                    query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo, i139590  familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id where i139548 = '{0}' and i139603 = '{1}' ", NroDespacho, codigo);
                }

                if (doctypes == null || doctypes.Count == 0)
                {
                    //Se devuelven todas las familias
                    ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelven todas las familias: ");
                }
                else
                {
                    List<string> familias = new List<string>();
                    foreach (doctype dt in doctypes)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Se devuelve la familia: " + dt.id.ToString());
                        familias.Add(dt.id);
                    }
                    var familiastr = string.Join(",", familias);
                    query = query + string.Format(" and  i139590 in ({0}) ", familiastr);
                }

                query = query + string.Format("order by i139609");


                DataSet dsFiles = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

                string NewPDF = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\" + NroDespacho + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                List<string> Files = new List<string>();

                foreach (DataRow r in dsFiles.Tables[0].Rows)
                {
                    Files.Add(r["Archivo"].ToString());
                    ZTrace.WriteLineIf(ZTrace.IsError, "Se agrega archivo: " + r["Archivo"].ToString());

                }

                ZTrace.WriteLineIf(ZTrace.IsError, "Se encontraron archivos cantidad: " + dsFiles.Tables[0].Rows.Count);

                GeneraPDFFromAnotherPDFs(Files, NewPDF);

                ZTrace.WriteLineIf(ZTrace.IsError, "Se genero el PDF: " + NewPDF);

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

            [XmlAttribute("gen-time")]
            public string gen_time { get; set; }

            [XmlAttribute("exp-time")]
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
