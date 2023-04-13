using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvocacionServWDigDepFiel.wDigDepFiel;
using System.IO;
using InvocacionServWDigDepFiel;
using Newtonsoft.Json;
using ClienteLoginCms_CS;
using System.Xml;
using System.Data;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using System.Security.Cryptography.X509Certificates;
using Zamba.Framework;
using InvocacionServWConsDepFiel;
using InvocacionServWDigDepFiel.wConsDepFiel;

namespace ZambaWeb.RestApi.Controllers.Web
{

    [RoutePrefix("api/SignPDF")]
    public class SignPDFController : ApiController
    {

        #region Constructor&ClassHelpers
        public SignPDFController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("User"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
                }


                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }
        #endregion


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SignSinglePDF")]
        public IHttpActionResult SignSinglePDF(genericRequest paramRequest)
        {
            try
            {
                string signedFile = _signSinglePDF(paramRequest);
                return Ok(signedFile);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return Ok(ex);
            }


        }

        private static string _signSinglePDF(genericRequest paramRequest)
        {
            var serialNumber = ZOptBusiness.GetValueOrDefault("SignSerialNumber", "2455f7ea0000000420d6");
            // var serialNumber = ZOptBusiness.GetValueOrDefault("SignSerialNumber", "25D87E4BE1A1ED8D");
            var SignStoreName = ZOptBusiness.GetValueOrDefault("SignStoreName", "MY");

            X509Store store = new X509Store(SignStoreName, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var collection = store.Certificates;
            var fcollection = collection.Find(X509FindType.FindBySerialNumber, serialNumber, false);

            string Source = paramRequest.Params["File"].ToString();
            string BKSource = Source.ToUpper().Replace(".PDF", "BK-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF");
            string Target = Source.ToUpper().Replace(".PDF", "Signed" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF");

            FileInfo TargetFile = new FileInfo(Target);

            if (TargetFile.Exists == false)
            {
                FileInfo SourceFile = new FileInfo(Source);
                SourceFile.CopyTo(BKSource);
                FirmaDocumento.SignHashed(Source, Target, FirmaDocumento.ObtenerCertificadoPorSerialNumber(serialNumber, SignStoreName), "", "", true);
                //Pasar archivo temp al de la ruta origen
                TargetFile.CopyTo(Source, true);
                //ver el borrado y validacion de si ya esta firmado.
                //TargetFile.Delete();
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Firma de Documento OK: " + TargetFile.FullName);
                try
                {
                    TargetFile.Delete();
                    File.Delete(BKSource);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Firma de Documento: Ya existe el Archivo Destino: " + TargetFile.FullName);
            }
            //Mocks.FirmaDocumentoMock.SignHashed(@"C:\WSASS_como_adherirse.pdf",
            //    @"C:\WSASS_como_adherirse.pdf", fcollection[1],
            //    "", @"C:\", true);

            store.Close();

            return Source;
        }

        public class SignPDFResponse
        {
            public results result { get; internal set; }
            public string error;
            public string file { get; internal set; }

            public enum results
            {
                error = 1,
                alreadySigned = 3,
                signed = 2
            }

            public RecepcionResponse RecepcionResponse { get; internal set; }
            public DigitalizacionResponse DigitalizacionResponse { get; internal set; }
        }

        public class RecepcionResponse
        {
            public results result { get; internal set; }
            public string error;
            public InvocacionServWDigDepFiel.wDigDepFiel.Autenticacion autenticacion { get; internal set; }
            public InvocacionServWDigDepFiel.wDigDepFiel.Recibo reciboAvisoRecepAcept { get; internal set; }
            public SolicitudFirmaDigital solicitudFirmaDigital { get; internal set; }
            public int codError { get; internal set; }
            public string descError { get; internal set; }

            public enum results
            {
                error = 1,
                Ok = 2,
                alreadyAcepted = 3
            }
        }



        public class DigitalizacionResponse
        {
            public results result { get; internal set; }
            public string error;
            public InvocacionServWDigDepFiel.wDigDepFiel.Recibo reciboAvisoDigit { get; internal set; }
            public InvocacionServWDigDepFiel.wDigDepFiel.Autenticacion autenticacion { get; internal set; }
            public SolicitudFirmaDigital solicitudFirmaDigital { get; internal set; }

            public int codError { get; internal set; }
            public string descError { get; internal set; }
            public enum results
            {
                error = 1,
                Ok = 2,
                alreadyDigitalized = 3
            }
        }


        [AcceptVerbs("GET")]
        [AllowAnonymous]
        [Route("SignPDFAll")]
        public IHttpActionResult SignPDFAll()
        {
            try
            {

                DataSet dsall = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select d.i139548 Despacho, w.user_asigned,u.Name, ws.name Etapa, w.step_id,d.i139614 Guia, i139628 RCodigo,i139630 RDesc,i139620 DCodigo,i26513 dDesc,d.i139600 cuitDespachante, d.i139645 Despachante, e.i139579,  d.i149651 cuitImpoExpo,  F.i139562 ImpoExpo,  '' CodigoServicio, d.i139608 cantidadPaginas , i139588 TipoIE, i139551 FechaOficializacion, i149662 FechaVtoEmbarque  from doc_i139072 d     inner join doc_i139074 E on d.i139600 = e.i139600 	inner join doc_i139073 F on d.i149651 = f.i26296  inner join wfdocument w on w.doc_id = d.doc_id inner join wfstep ws on ws.step_id = w.step_id inner join ZUSER_OR_GROUP u on u.id = w.User_Asigned where d.i139600 is not null and not ( i139551 is null  and i139588 = 'IMPORTACION' ) and not ( i149662 is null and i139588 = 'EXPORTACION' ) and not (w.step_id = 139109 and i139628 = 0 and i139620 = 0) and d.i139614 < 621");

                foreach (DataRow r in dsall.Tables[0].Rows)
                {
                    try
                    {

                        SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();

                        solicitudFirmaDigital.userId = 160419;
                        var cuitDespachante = r["cuitDespachante"].ToString();
                        var nroDespacho = r["Despacho"].ToString();
                        solicitudFirmaDigital.cuitDeclarante = cuitDespachante;
                        solicitudFirmaDigital.nroDespacho = nroDespacho;



                        string error = string.Empty;

                        SignPDFResponse SR = _firmarPDF(solicitudFirmaDigital);

                        if (SR.result == SignPDFResponse.results.signed || SR.result == SignPDFResponse.results.alreadySigned)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");

                            RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                            if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Digitalizacion: ");
                                DigitalizacionResponse DR = _digitalizacionDespacho(solicitudFirmaDigital);
                                SR.DigitalizacionResponse = DR;
                                SR.RecepcionResponse = RR;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se Finaliza proceso de Digitalizacion: ");
                                if (DR.result == DigitalizacionResponse.results.Ok)
                                {
                                    // var js = JsonConvert.SerializeObject(SR);
                                    // return Ok(js);
                                }
                                else
                                {
                                    ZClass.raiseerror(new Exception(DR.descError));
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Digitalizacion: " + DR.descError);

                                }
                            }
                            else
                            {
                                ZClass.raiseerror(new Exception(RR.descError));
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                            }
                        }
                        else
                        {
                            ZClass.raiseerror(new Exception(SR.error));
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Firma: " + SR.error);
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(ex);
            }
        }

        [AcceptVerbs("GET")]
        [AllowAnonymous]
        [Route("ReceptAll")]
        public IHttpActionResult ReceptAll()
        {
            try
            {

                DataSet dsall = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select d.i139548 Despacho, w.user_asigned,u.Name, ws.name Etapa, w.step_id,d.i139614 Guia, i139628 RCodigo,i139630 RDesc,i139620 DCodigo,i26513 dDesc,d.i139600 cuitDespachante, d.i139645 Despachante, e.i139579,  d.i149651 cuitImpoExpo,  F.i139562 ImpoExpo,  '' CodigoServicio, d.i139608 cantidadPaginas , i139588 TipoIE, i139551 FechaOficializacion, i149662 FechaVtoEmbarque  from doc_i139072 d     inner join doc_i139074 E on d.i139600 = e.i139600 	inner join doc_i139073 F on d.i149651 = f.i26296  inner join wfdocument w on w.doc_id = d.doc_id inner join wfstep ws on ws.step_id = w.step_id inner join ZUSER_OR_GROUP u on u.id = w.User_Asigned where d.i139600 is not null and not(i139551 is null  and i139588 = 'IMPORTACION') and not(w.step_id = 139109 and i139628 = 0 and i139620 = 0) and w.step_id in (139106, 139107, 139108)");

                foreach (DataRow r in dsall.Tables[0].Rows)
                {
                    try
                    {

                        SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();

                        solicitudFirmaDigital.userId = 160419;
                        var cuitDespachante = r["cuitDespachante"].ToString();
                        var nroDespacho = r["Despacho"].ToString();
                        solicitudFirmaDigital.cuitDeclarante = cuitDespachante;
                        solicitudFirmaDigital.nroDespacho = nroDespacho;



                        string error = string.Empty;


                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");

                        RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                        if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                        {
                        }
                        else
                        {
                            ZClass.raiseerror(new Exception(RR.descError));
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                        }

                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(ex);
            }
        }

        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("SignPDF")]
        public IHttpActionResult SignPDF(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {
                var user = GetUser(solicitudFirmaDigital.userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                string error = string.Empty;

                SignPDFResponse SR = _firmarPDF(solicitudFirmaDigital);

                if (SR.result == SignPDFResponse.results.signed || SR.result == SignPDFResponse.results.alreadySigned)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");

                    RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                    if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Digitalizacion: ");
                        DigitalizacionResponse DR = _digitalizacionDespacho(solicitudFirmaDigital);
                        SR.DigitalizacionResponse = DR;
                        SR.RecepcionResponse = RR;
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se Finaliza proceso de Digitalizacion: ");
                        if (DR.result == DigitalizacionResponse.results.Ok)
                        {
                            var js = JsonConvert.SerializeObject(SR);
                            return Ok(js);
                        }
                        else
                        {
                            ZClass.raiseerror(new Exception(DR.descError));
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Digitalizacion: " + DR.descError);
                            var js = JsonConvert.SerializeObject(DR);
                            return Ok(js);
                        }
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception(RR.descError));
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                        var js = JsonConvert.SerializeObject(RR);
                        return Ok(js);

                    }
                }
                else
                {
                    ZClass.raiseerror(new Exception(SR.error));
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Firma: " + SR.error);
                    var js = JsonConvert.SerializeObject(SR);
                    return Ok(js);

                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(ex);
            }
        }

        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("FirmarPDF")]
        public IHttpActionResult FirmarPDF(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {
                SignPDFResponse SignPDFResponse = _firmarPDF(solicitudFirmaDigital);
                var js = JsonConvert.SerializeObject(SignPDFResponse);
                return Ok(js);
            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        private SignPDFResponse _firmarPDF(SolicitudFirmaDigital solicitudFirmaDigital)
        {

            SignPDFResponse SR = new SignPDFResponse();

            //0- Obtener Parametros del Servicio
            var NroDespacho = solicitudFirmaDigital.nroDespacho;
            var Codigo = solicitudFirmaDigital.codigo;

            var queryIsSigned = string.Empty;
            if (Codigo == "004" || Codigo == "002" || Codigo == "003")
            {
                queryIsSigned = string.Format(@"select i139615 from  doc_i139072  where i139548 = '{0}' and i139603 = '{1}'  and i139578 = '{2}'", NroDespacho, Codigo, solicitudFirmaDigital.sigea);
            }
            else
            {
                queryIsSigned = string.Format(@"select i139615 from  doc_i139072  where i139548 = '{0}' and i139603 = '{1}'", NroDespacho, Codigo);
            }

            object IsSigned = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, queryIsSigned);

            if (IsSigned != null && IsSigned.ToString() != string.Empty)
            {
                var archivo = string.Empty;
                if (Codigo == "004" || Codigo == "002" || Codigo == "003")
                {
                    archivo = string.Format(@"select (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from  doc_i139072  where i139548 = '{0}' and i139603 = '{1}'  and i139578 = '{2}'", NroDespacho, Codigo, solicitudFirmaDigital.sigea);
                }
                else
                {
                    archivo = string.Format(@"select (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from  doc_i139072  where i139548 = '{0}' and i139603 = '{1}'  ", NroDespacho, Codigo);
                }
                SR.result = SignPDFResponse.results.alreadySigned;
                SR.file = archivo;
                return SR;
            }

            //1-GENERATE ONE PDF
            var query = string.Empty;
            if (Codigo == "004" || Codigo == "002" || Codigo == "003")
            {
                query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo ,i139590 familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'  and i139578 = '{2}'", NroDespacho, Codigo, solicitudFirmaDigital.sigea);
            }
            else
            {
                query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo ,i139590 familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", NroDespacho, Codigo);
            }
            DataSet dsFiles = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

            if (dsFiles is null || dsFiles.Tables.Count == 0 || dsFiles.Tables[0].Rows.Count == 0)
            {
                throw new Exception("No se han digitalizado o monitoreado las imagenes de este legajo aun. Para continuar debe realizar la digitalizacion");
            }
            //1-BIZ - GET FULL PDF

            int CantidadTotal = int.Parse(dsFiles.Tables[0].Rows[0]["cantidadTotal"].ToString());

            var familias = new List<Familia>();
            var familiaList = new SynchronizedHashtable();

            foreach (DataRow rf in dsFiles.Tables[0].Rows)
            {
                // GR.Params.Add(rf["doc_id"].ToString(), rf["Archivo"].ToString());

                var familiacode = rf["familia"].ToString();

                if (familiaList.ContainsKey(familiacode))
                {
                    int count = int.Parse(familiaList[familiacode].ToString());
                    familiaList[familiacode] = count + 1;
                }
                else
                {
                    int count = 1;
                    familiaList.Add(familiacode, count + 1);
                }
            }

            foreach (string fr in familiaList.Keys)
            {
                Familia f = new Familia();
                f.codigo = fr;
                f.cantidad = int.Parse(familiaList[fr].ToString());
                familias.Add(f);
            }


            // TasksController TC = new TasksController();
            //TC.GeneraPDFFromAnotherPDFs(GR);

            //1-BIZ - GET FULL PDF
            var queryFull = string.Empty;
            if (Codigo == "004" || Codigo == "002" || Codigo == "003")
            {
                queryFull = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", NroDespacho, Codigo, solicitudFirmaDigital.sigea);
            }
            else
            {
                queryFull = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}' ", NroDespacho, Codigo);
            }

            DataSet dsFile = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, queryFull);

            string PDFFile = dsFile.Tables[0].Rows[0]["Archivo"].ToString();

            //2-SIGN the PDF
            genericRequest SGR = new genericRequest();
            //SignPDFController SPC = new SignPDFController();
            SGR.Params.Add("File", PDFFile);

            string signedFile = _signSinglePDF(SGR);
            //SPC.SignSinglePDF(SGR);

            if (signedFile != null && signedFile != string.Empty)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Firma de Documento realizada OK: " + signedFile);

                var querySigned = string.Empty;
                if (Codigo == "004" || Codigo == "002" || Codigo == "003")
                {
                    querySigned = string.Format(@"update doc_i139072 set i139608 = {0}, i139615 = getdate(), i139617 = {1} where i139548 = '{2}' and i139603 = '{3}' and i139578 = '{4}'", CantidadTotal, solicitudFirmaDigital.userId, NroDespacho, Codigo, solicitudFirmaDigital.sigea);
                }
                else
                {
                    querySigned = string.Format(@"update doc_i139072 set i139608 = {0}, i139615 = getdate(), i139617 = {1} where i139548 = '{2}' and i139603 = '{3}' ", CantidadTotal, solicitudFirmaDigital.userId, NroDespacho, Codigo);
                }
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, querySigned);

                SR.result = SignPDFResponse.results.signed;
                SR.file = PDFFile;
                return SR;
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Firma de Documento realizada ERROR: ");
                SR.result = SignPDFResponse.results.error;
                SR.file = PDFFile;
                return SR;
            }
        }



        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("RecepcionDespachoByGuia")]
        public IHttpActionResult RecepcionDespachoByGuia(genericRequest genericRequest)
        {
            Int64 nro_guia = Convert.ToInt64(genericRequest.Params["nro_guia"].ToString());
            long user_id = Convert.ToInt64(genericRequest.Params["user_id"].ToString());
            Int64 doc_id = Convert.ToInt64(genericRequest.Params["doc_id"].ToString());
            var rooturl = ZOptBusiness.GetValueOrDefault("ThisDomain", "https://gd.modoc.com.ar/Zamba.Web");
            try
            {
                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");
                DataSet dsall = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select  d.i139548 NroDespacho, w.user_asigned,u.Name, ws.name Etapa, i139603 codigoDespacho, w.step_id,d.i139614 Guia, I139578 Sigea, i139628 RCodigo,i139630 RDesc,i139620 DCodigo,i26513 dDesc,d.i139600 cuitDespachante, d.i139645 Despachante, e.i139579,  d.i149651 cuitImpoExpo,  F.i139562 ImpoExpo,  '' CodigoServicio, d.i139608 cantidadPaginas , i139588 TipoIE, i139551 FechaOficializacion, i149662 FechaVtoEmbarque  from doc_i139072 d     inner join doc_i139074 E on d.i139600 = e.i139600 	inner join doc_i139073 F on d.i149651 = f.i26296  inner join wfdocument w on w.doc_id = d.doc_id inner join wfstep ws on ws.step_id = w.step_id inner join ZUSER_OR_GROUP u on u.id = w.User_Asigned where d.i139600 is not null and not(i139551 is null  and i139588 = 'IMPORTACION') and not(w.step_id = 139109 and i139628 = 0 and i139620 = 0) and w.step_id in (139106, 139107, 139108) and d.i139614=" + nro_guia.ToString());

                foreach (DataRow r in dsall.Tables[0].Rows)
                {
                    SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                    solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", doc_id.ToString());
                    solicitudFirmaDigital.userId = user_id;
                    String nroDespacho = r["NroDespacho"].ToString();
                    String  Sigea = r["Sigea"].ToString();
                    String codDespacho = r["codigoDespacho"].ToString();
                    solicitudFirmaDigital.sigea = Sigea;
                    solicitudFirmaDigital.nroDespacho = nroDespacho;
                    solicitudFirmaDigital.codigo = codDespacho;
                    solicitudFirmaDigital.nroGuia = nro_guia.ToString();
                    

                    // faltan datos
                    /*
                    userid
                    DocTypeId=139072
                    doc_id
                    url: solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", r["doc_id"].ToString());
                    nroDespacho (query)
                    sigea (query)
                    codigo (query)

                    139614	Nro Guia                                                                                            
                    139578	Nro Sigea                                                                                           
                    139603	Codigo                                                                                              
                    139548	Nro Despacho                                                                                        


                     */
                    RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                    if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                    {
                        var js = JsonConvert.SerializeObject(RR);
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception(RR.descError));
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                        var js = JsonConvert.SerializeObject(RR);
                        return Ok(js);
                    }
                }
                return Ok();



            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }


        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("SignPDFByGuia")]
        public IHttpActionResult SignPDFByGuia(genericRequest genericRequest)
        {
            Int64 nro_guia = Convert.ToInt64(genericRequest.Params["nro_guia"].ToString());
            long user_id = Convert.ToInt64(genericRequest.Params["user_id"].ToString());
            Int64 doc_id = Convert.ToInt64(genericRequest.Params["doc_id"].ToString());
            try
            {
                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");
                DataSet dsall = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select  d.i139548 NroDespacho, w.user_asigned,u.Name, ws.name Etapa, i139603 codigoDespacho, w.step_id,d.i139614 Guia, I139578 Sigea, i139628 RCodigo,i139630 RDesc,i139620 DCodigo,i26513 dDesc,d.i139600 cuitDespachante, d.i139645 Despachante, e.i139579,  d.i149651 cuitImpoExpo,  F.i139562 ImpoExpo,  '' CodigoServicio, d.i139608 cantidadPaginas , i139588 TipoIE, i139551 FechaOficializacion, i149662 FechaVtoEmbarque  from doc_i139072 d     inner join doc_i139074 E on d.i139600 = e.i139600 	inner join doc_i139073 F on d.i149651 = f.i26296  inner join wfdocument w on w.doc_id = d.doc_id inner join wfstep ws on ws.step_id = w.step_id inner join ZUSER_OR_GROUP u on u.id = w.User_Asigned where d.i139600 is not null and not(i139551 is null  and i139588 = 'IMPORTACION') and not(w.step_id = 139109 and i139628 = 0 and i139620 = 0) and w.step_id in (139106, 139107, 139108) and d.i139614=" + nro_guia.ToString());

                foreach (DataRow r in dsall.Tables[0].Rows)
                {
                    // faltan datos
                    /*
                    userid (parametro)
                    DocTypeId=139072
                    doc_id (parametro)
                    url: solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", r["doc_id"].ToString());
                    nroDespacho (query)
                    sigea (query)
                    codigo (query)*/

                    SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();

                    solicitudFirmaDigital.userId = user_id;
                    var cuitDespachante = r["cuitDespachante"].ToString();
                    var rooturl = ZOptBusiness.GetValueOrDefault("ThisDomain", "https://gd.modoc.com.ar/Zamba.Web");
                    solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", doc_id.ToString());
                    
                    String nroDespacho = r["NroDespacho"].ToString();
                    String Sigea = r["Sigea"].ToString();
                    String codDespacho = r["codigoDespacho"].ToString();
                    solicitudFirmaDigital.sigea = Sigea;
                    solicitudFirmaDigital.nroDespacho = nroDespacho;
                    solicitudFirmaDigital.codigo = codDespacho;
                    solicitudFirmaDigital.nroGuia = nro_guia.ToString();
                    solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", doc_id.ToString());
                    solicitudFirmaDigital.cuitDeclarante = cuitDespachante;
                    // faltan datos
                    try
                    {
                        var user = GetUser(solicitudFirmaDigital.userId);
                        if (user == null)
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                new HttpError(StringHelper.InvalidUser)));

                        SignPDFResponse SR = _firmarPDF(solicitudFirmaDigital);

                        if (SR.result == SignPDFResponse.results.signed || SR.result == SignPDFResponse.results.alreadySigned)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");

                            RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                            if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Digitalizacion: ");
                                DigitalizacionResponse DR = _digitalizacionDespacho(solicitudFirmaDigital);
                                SR.DigitalizacionResponse = DR;
                                SR.RecepcionResponse = RR;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se Finaliza proceso de Digitalizacion: ");
                                if (DR.result == DigitalizacionResponse.results.Ok)
                                {
                                    var js = JsonConvert.SerializeObject(SR);
                                }
                                else
                                {
                                    ZClass.raiseerror(new Exception(DR.descError));
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Digitalizacion: " + DR.descError);
                                    var js = JsonConvert.SerializeObject(DR);
                                    return Ok(js);
                                }
                            }
                            else
                            {
                                ZClass.raiseerror(new Exception(RR.descError));
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                                var js = JsonConvert.SerializeObject(RR);
                                return Ok(js);

                            }
                        }
                        else
                        {
                            ZClass.raiseerror(new Exception(SR.error));
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Firma: " + SR.error);
                            var js = JsonConvert.SerializeObject(SR);
                            return Ok(js);

                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        return Ok(ex);
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }




        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("RecepcionDespacho")]
        public IHttpActionResult RecepcionDespacho(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {
                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de Recepcion: ");

                RecepcionResponse RR = _recepcionDespacho(solicitudFirmaDigital);
                if (RR.result == RecepcionResponse.results.Ok || RR.result == RecepcionResponse.results.alreadyAcepted)
                {
                    var js = JsonConvert.SerializeObject(RR);
                    return Ok(js);
                }
                else
                {
                    ZClass.raiseerror(new Exception(RR.descError));
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en proceso de Recepcion: " + RR.descError);
                    var js = JsonConvert.SerializeObject(RR);
                    return Ok(js);
                }


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        private static RecepcionResponse _recepcionDespacho(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio

            //3- Aviso Recepcion

            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWDigDepFiel = new InvocacionServWDigDepFiel.ServicioWDigDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPDIGISERVICE", "wDigDepFiel");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");

            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

            InvocacionServWDigDepFiel.wDigDepFiel.Autenticacion autenticacion = servicioWDigDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

            //obtener datos del despacho
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno datos del despacho para la recepcion");

            var querydespacho = string.Empty;
            if (solicitudFirmaDigital.codigo == "004" || solicitudFirmaDigital.codigo == "002" || solicitudFirmaDigital.codigo == "003")
            {
                querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate, i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE , i139608 cantidadfojas from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
            }
            else
            {
                querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate, i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE, i139608 cantidadfojas  from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
            }
            DataSet dsDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, querydespacho);

            DataRow r = dsDespacho.Tables[0].Rows[0];

            RecepcionResponse RR = new RecepcionResponse();


            if (r["codigoError"] == null || (r["codigoError"].ToString() != "0" && r["codigoError"].ToString() != "102"))
            {
                solicitudFirmaDigital.nroLegajo = r["nroLegajo"].ToString();
                solicitudFirmaDigital.cuitDeclarante = r["cuitDeclarante"].ToString();
                solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                solicitudFirmaDigital.codigo = r["codigo"].ToString();

                solicitudFirmaDigital.cuitIE = r["cuitIE"].ToString();

                solicitudFirmaDigital.cuitATA = (r["cuitATA"] != null) ? r["cuitATA"].ToString() : string.Empty;



                solicitudFirmaDigital.ticket = (r["ticket"] != null) ? r["ticket"].ToString() : string.Empty;

                solicitudFirmaDigital.sigea = (r["sigea"] != null) ? r["sigea"].ToString() : string.Empty;
                solicitudFirmaDigital.nroReferencia = string.Empty;
                solicitudFirmaDigital.nroGuia = r["nroGuia"].ToString();
                try
                {
                    solicitudFirmaDigital.cantidadFojas = int.Parse(r["cantidadFojas"].ToString());
                }
                catch (Exception)
                {

                    solicitudFirmaDigital.cantidadFojas = 0;
                }



                if (r["IE"].ToString() == "EXPORTACION")
                {

                    if ((r["vtoEmbarque"] is DBNull || r["vtoEmbarque"] is null || r["vtoEmbarque"].ToString() == string.Empty))
                    {
                        //  throw new Exception("La Fecha de Vto Embarque es nula");
                        solicitudFirmaDigital.fechaDespacho = DateTime.Now;
                    }
                    else
                    {
                        solicitudFirmaDigital.fechaDespacho = (DateTime)(r["vtoEmbarque"]);
                    }
                }
                else
                {
                    if (r["fechaDespacho"] is DBNull || r["fechaDespacho"] is null || r["fechaDespacho"].ToString() == string.Empty)
                    {
                        throw new Exception("La Fecha de Despacho es nula");
                    }
                    else
                    {
                        solicitudFirmaDigital.fechaDespacho = (DateTime)(r["fechaDespacho"]);
                    }

                }

                if (r["fechaGeneracion"] == null || r["fechaGeneracion"] is System.DBNull)
                {
                    solicitudFirmaDigital.fechaGeneracion = ((DateTime)(r["crdate"]));  //DateTime.Today;
                }
                else
                {
                    solicitudFirmaDigital.fechaGeneracion = ((DateTime)(r["fechaGeneracion"]));  //DateTime.Today;
                }

                if (solicitudFirmaDigital.fechaDespacho < solicitudFirmaDigital.fechaGeneracion)
                {
                    solicitudFirmaDigital.fechaDespacho = solicitudFirmaDigital.fechaGeneracion;
                }

                if (solicitudFirmaDigital.fechaDespacho > DateTime.Now)
                {
                    solicitudFirmaDigital.fechaDespacho = DateTime.Now;
                }

                if (r["fechaAceptacion"] is DBNull)
                {
                    solicitudFirmaDigital.fechaHoraAcept = DateTime.Now;
                }
                else
                {
                    solicitudFirmaDigital.fechaHoraAcept = (DateTime)(r["fechaAceptacion"]);
                }

                if (solicitudFirmaDigital.fechaHoraAcept < solicitudFirmaDigital.fechaGeneracion)
                {
                    solicitudFirmaDigital.fechaHoraAcept = solicitudFirmaDigital.fechaGeneracion;
                }
                solicitudFirmaDigital.idEnvio = solicitudFirmaDigital.nroDespacho + DateTime.Today.ToString("yyyyMMddHHmm");
                solicitudFirmaDigital.indLugarFisico = r["indLugarFisico"].ToString();


                ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando recepcion de despacho en AFIP");

                InvocacionServWDigDepFiel.wDigDepFiel.Recibo reciboAvisoRecepAcept = servicioWDigDepFiel.InvocarServicioAvisoRecepAceptRequest(autenticacion, solicitudFirmaDigital.nroLegajo,
                solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.cuitPSAD, solicitudFirmaDigital.cuitIE,
                solicitudFirmaDigital.cuitATA, solicitudFirmaDigital.codigo, solicitudFirmaDigital.url,
                null, solicitudFirmaDigital.ticket,
                solicitudFirmaDigital.sigea, solicitudFirmaDigital.nroReferencia, solicitudFirmaDigital.nroGuia, solicitudFirmaDigital.cantidadFojas, solicitudFirmaDigital.fechaDespacho, solicitudFirmaDigital.fechaGeneracion, solicitudFirmaDigital.fechaHoraAcept, solicitudFirmaDigital.idEnvio, solicitudFirmaDigital.indLugarFisico);

                var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(reciboAvisoRecepAcept, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
                //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);



                if (reciboAvisoRecepAcept.codError == 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP OK");
                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}'", solicitudFirmaDigital, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}' and i139578 = '{6}'", solicitudFirmaDigital, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    RR.result = RecepcionResponse.results.Ok;
                    RR.reciboAvisoRecepAcept = reciboAvisoRecepAcept;
                    RR.autenticacion = autenticacion;
                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                    RR.codError = reciboAvisoRecepAcept.codError;
                    RR.descError = reciboAvisoRecepAcept.descError;
                }
                else if (reciboAvisoRecepAcept.codError == 102 && reciboAvisoRecepAcept.descError.Contains("Estado Actual") && reciboAvisoRecepAcept.descError.Contains("PSAD"))
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP OK");
                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}'", solicitudFirmaDigital, 0, "OK. Procesado", solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}' and i139578 = '{6}'", solicitudFirmaDigital, 0, "OK. Procesado", solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    RR.result = RecepcionResponse.results.Ok;
                    RR.reciboAvisoRecepAcept = reciboAvisoRecepAcept;
                    RR.autenticacion = autenticacion;
                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                    RR.codError = 0;
                    RR.descError = "OK. Procesado";
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP ERROR: " + reciboAvisoRecepAcept.codError + "  " + reciboAvisoRecepAcept.descError);
                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}' where i139548 = '{3}' and i139603 = '{4}'", newresultsreciboAvisoRecepAcept, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}' where i139548 = '{3}' and i139603 = '{4}' and i139578 = '{5}'", newresultsreciboAvisoRecepAcept, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    RR.result = RecepcionResponse.results.error;
                    RR.reciboAvisoRecepAcept = reciboAvisoRecepAcept;
                    RR.autenticacion = autenticacion;
                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                    RR.codError = reciboAvisoRecepAcept.codError;
                    RR.descError = reciboAvisoRecepAcept.descError;
                }

            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "codigoError del despacho: " + r["codigoError"].ToString() + " despacho ya recepcionado en AFIP");
                RR.result = RecepcionResponse.results.alreadyAcepted;
            }
            return RR;
        }

        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("DigitalizacionDespacho")]
        public IHttpActionResult DigitalizacionDespacho(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {

                DigitalizacionResponse DR = _digitalizacionDespacho(solicitudFirmaDigital);

                var newresultsreciboAvisoDigit = JsonConvert.SerializeObject(DR.reciboAvisoDigit, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                //XmlDocument docreciboAvisoDigit = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoDigit);



                var js = JsonConvert.SerializeObject(DR);

                return Ok(js);
            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        private static DigitalizacionResponse _digitalizacionDespacho(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio

            DigitalizacionResponse DR = new DigitalizacionResponse();

            try
            {
                var queryFull = string.Empty;
                //1-BIZ - GET FULL PDF
                if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                {
                    queryFull = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                }
                else
                {
                    queryFull = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139072\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'  and i139578 = '{2}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);

                }

                DataSet dsFile = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, queryFull);

                string PDFFile = dsFile.Tables[0].Rows[0]["Archivo"].ToString();

                if (new FileInfo(PDFFile).Exists == false)
                {
                    DR.result = DigitalizacionResponse.results.error;
                    DR.error = "El despacho no tiene el PDF completo digitalizado";
                    DR.codError = 999;
                    DR.descError = DR.error;
                    return DR;
                }
                var query = string.Empty;

                //1-GENERATE ONE PDF
                if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                {
                    query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo ,i139590 familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                }
                else
                {
                    query = string.Format(@"select t.doc_id, (v.DISK_VOL_PATH + '\139089\' + convert(nvarchar,t.OFFSET)  + '\' + t.DOC_FILE) Archivo ,i139590 familia,i139608  cantidadTotal, i139609 Pagina from doc_i139089 i inner join doc_t139089 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'   and i139578 = '{2}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
                }

                DataSet dsFiles = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

                var PathToSave = Zamba.Membership.MembershipHelper.AppTempDir("Temp").FullName + solicitudFirmaDigital.nroDespacho + ".pdf";

                if (dsFiles is null || dsFiles.Tables.Count == 0 || dsFiles.Tables[0].Rows.Count == 0)
                {

                    DR.result = DigitalizacionResponse.results.error;
                    DR.error = "No se han digitalizado o monitoreado las imagenes de este legajo aun. Para continuar debe realizar la digitalizacion";
                    DR.codError = 999;
                    DR.descError = DR.error;
                    return DR;
                }
                //1-BIZ - GET FULL PDF

                int CantidadTotal = int.Parse(dsFiles.Tables[0].Rows[0]["cantidadTotal"].ToString());

                var familias = new List<Familia>();
                var familiaList = new SynchronizedHashtable();

                genericRequest GR = new genericRequest();
                GR.Params.Add("PathToSave", PathToSave);

                foreach (DataRow rf in dsFiles.Tables[0].Rows)
                {
                    GR.Params.Add(rf["doc_id"].ToString(), rf["Archivo"].ToString());

                    var familiacode = rf["familia"].ToString();

                    if (familiaList.ContainsKey(familiacode))
                    {
                        int count = int.Parse(familiaList[familiacode].ToString());
                        familiaList[familiacode] = count + 1;
                    }
                    else
                    {
                        int count = 1;
                        familiaList.Add(familiacode, count + 1);
                    }
                }

                foreach (string fr in familiaList.Keys)
                {
                    Familia f = new Familia();
                    f.codigo = "0" + fr;
                    f.cantidad = int.Parse(familiaList[fr].ToString());
                    familias.Add(f);
                }




                // TasksController TC = new TasksController();
                //TC.GeneraPDFFromAnotherPDFs(GR);




                //4- Aviso Digitalizacion


                var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
                var servicioWDigDepFiel = new InvocacionServWDigDepFiel.ServicioWDigDepFiel();

                ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
                ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPDIGISERVICE", "wDigDepFiel");
                ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
                ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");

                var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

                InvocacionServWDigDepFiel.wDigDepFiel.Autenticacion autenticacion = servicioWDigDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

                var querydespacho = string.Empty;

                //obtener datos del despacho
                if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                {
                    querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate,i139577 fechaAceptacion, i149662 vtoEmbarque,i139588 IE   from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}'  and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                }
                else
                {
                    querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate,i139577 fechaAceptacion, i149662 vtoEmbarque,i139588 IE   from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}'  and i139603 = '{1}'   and i139578 = '{2}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);

                }

                DataSet dsDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, querydespacho);

                DataRow r = dsDespacho.Tables[0].Rows[0];
                solicitudFirmaDigital.nroLegajo = r["nroLegajo"].ToString();
                solicitudFirmaDigital.cuitDeclarante = r["cuitDeclarante"].ToString();
                solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                solicitudFirmaDigital.cuitIE = r["cuitIE"].ToString();
                solicitudFirmaDigital.cuitATA = (r["cuitATA"] != null) ? r["cuitATA"].ToString() : string.Empty;
                solicitudFirmaDigital.codigo = r["codigo"].ToString();

                Boolean found01 = false, found02 = false, found03 = false, found04 = false, found05 = false;

                if (solicitudFirmaDigital.codigo == "000")
                {
                    foreach (Familia f in familias)
                    {
                        if (f.codigo == "01") found01 = true;
                        if (f.codigo == "02") found02 = true;
                        if (f.codigo == "03") found03 = true;
                        if (f.codigo == "04") found04 = true;
                        if (f.codigo == "05") found05 = true;
                    }
                    if (found01 == false)
                    {
                        Familia f = new Familia();
                        f.codigo = "01";
                        f.cantidad = 0;
                        familias.Add(f);
                    }
                    if (found02 == false)
                    {
                        Familia f = new Familia();
                        f.codigo = "02";
                        f.cantidad = 0;
                        familias.Add(f);
                    }
                    if (found03 == false)
                    {
                        Familia f = new Familia();
                        f.codigo = "03";
                        f.cantidad = 0;
                        familias.Add(f);
                    }
                    if (found04 == false)
                    {
                        Familia f = new Familia();
                        f.codigo = "04";
                        f.cantidad = 0;
                        familias.Add(f);
                    }
                    if (found05 == false)
                    {
                        Familia f = new Familia();
                        f.codigo = "05";
                        f.cantidad = 0;
                        familias.Add(f);
                    }
                }

                solicitudFirmaDigital.ticket = (r["ticket"] != null) ? r["ticket"].ToString() : string.Empty;
                solicitudFirmaDigital.cantidadTotal = CantidadTotal;
                solicitudFirmaDigital.sigea = (r["sigea"] != null) ? r["sigea"].ToString() : string.Empty;
                solicitudFirmaDigital.nroReferencia = string.Empty;
                solicitudFirmaDigital.nroGuia = r["nroGuia"].ToString();

                if (r["IE"].ToString() == "EXPORTACION")
                {

                    if ((r["vtoEmbarque"] is DBNull || r["vtoEmbarque"] is null || r["vtoEmbarque"].ToString() == string.Empty))
                    {
                        // throw new Exception("La Fecha de Vto Embarque es nula");
                        solicitudFirmaDigital.fechaDespacho = DateTime.Now;
                    }
                    else
                    {
                        solicitudFirmaDigital.fechaDespacho = (DateTime)(r["vtoEmbarque"]);
                    }
                }
                else
                {
                    if (r["fechaDespacho"] is DBNull || r["fechaDespacho"] is null || r["fechaDespacho"].ToString() == string.Empty)
                    {
                        DR.result = DigitalizacionResponse.results.error;
                        DR.error = "La Fecha de Despacho es nula";
                        DR.codError = 999;
                        DR.descError = DR.error;
                        return DR;
                    }
                    else
                    {
                        solicitudFirmaDigital.fechaDespacho = (DateTime)(r["fechaDespacho"]);
                    }

                }


                if (r["fechaGeneracion"] == null || r["fechaGeneracion"] is System.DBNull)
                {
                    solicitudFirmaDigital.fechaGeneracion = ((DateTime)(r["crdate"]));  //DateTime.Today;
                }
                else
                {
                    solicitudFirmaDigital.fechaGeneracion = ((DateTime)(r["fechaGeneracion"]));  //DateTime.Today;
                }
                if (solicitudFirmaDigital.fechaDespacho < solicitudFirmaDigital.fechaGeneracion)
                {
                    solicitudFirmaDigital.fechaDespacho = solicitudFirmaDigital.fechaGeneracion;
                }
                solicitudFirmaDigital.fechaHoraAcept = (DateTime)(r["fechaAceptacion"]);
                if (solicitudFirmaDigital.fechaHoraAcept < solicitudFirmaDigital.fechaGeneracion)
                {
                    solicitudFirmaDigital.fechaHoraAcept = solicitudFirmaDigital.fechaGeneracion;
                }
                if (solicitudFirmaDigital.fechaDespacho > DateTime.Now)
                {
                    solicitudFirmaDigital.fechaDespacho = DateTime.Now;
                }
                solicitudFirmaDigital.idEnvio = solicitudFirmaDigital.nroDespacho + DateTime.Today.ToString("yyyyMMddHHmm");
                solicitudFirmaDigital.indLugarFisico = r["indLugarFisico"].ToString();

                var rooturlFS = ZOptBusiness.GetValueOrDefault("ThisDomainFS", "https://gd.modoc.com.ar/ZambaWeb.FS");
                var rootVolumes = ZOptBusiness.GetValueOrDefault("ThisDomainFSVolumes", "\\\\modocsa.lan\\Recursos\\VOLUMEN ZAMBA");
                var rooturl = ZOptBusiness.GetValueOrDefault("ThisDomain", "https://gd.modoc.com.ar/Zamba.Web");

                //\\modocsa.lan\Recursos\VOLUMEN ZAMBA\Volumenes Zamba\Doc_Despacho02\Vol001
                //\\modocsa.lan\Recursos\VOLUMEN ZAMBA\Volumenes Zamba\Doc_Despacho02\Vol001\139089\3\xxxx.pdf
                //solicitudFirmaDigital.url = PDFFile.Replace(rootVolumes, rooturlFS).Replace("\\","/");
                //https://gd.modoc.com.ar/ZambaWeb.FS/Volumenes Zamba/Doc_Despacho02/Vol001/139089/3/xxxx.pdf
                solicitudFirmaDigital.url = rooturl + string.Format("/Services/GetDocFile.ashx?DocTypeId=139072&DocId={0}&m=sc", r["doc_id"].ToString());

                string newresultsavisoDigitRequest = string.Empty;
                string hashing = string.Empty;
                InvocacionServWDigDepFiel.wDigDepFiel.Recibo reciboAvisoDigit = servicioWDigDepFiel.InvocarServicioAvisoDigitRequest(autenticacion, solicitudFirmaDigital.nroLegajo,
                     solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.cuitPSAD, solicitudFirmaDigital.cuitIE,
                     solicitudFirmaDigital.cuitATA, solicitudFirmaDigital.codigo, solicitudFirmaDigital.url,
                     familias.ToArray(), solicitudFirmaDigital.ticket, solicitudFirmaDigital.cantidadTotal,
                     solicitudFirmaDigital.sigea, solicitudFirmaDigital.nroReferencia, PDFFile, ref newresultsavisoDigitRequest, ref hashing);


                solicitudFirmaDigital.hashing = hashing;


                DR.reciboAvisoDigit = reciboAvisoDigit;
                DR.autenticacion = autenticacion;
                DR.solicitudFirmaDigital = solicitudFirmaDigital;

                var newreciboAvisoDigit = JsonConvert.SerializeObject(reciboAvisoDigit, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                //                if (newresultsavisoDigitRequest.Length > 199) newresultsavisoDigitRequest = newresultsavisoDigitRequest.Substring(0, 199);
                //              if (newreciboAvisoDigit.Length > 199) newreciboAvisoDigit = newreciboAvisoDigit.Substring(0, 150);

                if (reciboAvisoDigit.codError == 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Digitalizacion de despacho en AFIP OK: " + reciboAvisoDigit.codError + "  " + reciboAvisoDigit.descError);

                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}', i139608 = {4}  where i139548 = '{2}' and i139603 = '{3}' ", reciboAvisoDigit.codError, reciboAvisoDigit.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}', i139608 = {4}  where i139548 = '{2}' and i139603 = '{3}'   and i139578 = '{5}'", reciboAvisoDigit.codError, reciboAvisoDigit.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal, solicitudFirmaDigital.sigea);
                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    DR.result = DigitalizacionResponse.results.Ok;
                    DR.reciboAvisoDigit = reciboAvisoDigit;
                    DR.autenticacion = autenticacion;
                    DR.solicitudFirmaDigital = solicitudFirmaDigital;
                    DR.codError = reciboAvisoDigit.codError;
                    DR.descError = reciboAvisoDigit.descError;
                }
                else if ((reciboAvisoDigit.codError == 102 && reciboAvisoDigit.descError.Contains("DIGI")) || (reciboAvisoDigit.codError == 112 && reciboAvisoDigit.descError.Contains("El Hash informado ya existe, si quiere redigitalizar debe enviar otro Hash.")))
                {
                    //falta validar primero el codigo y si cambia el hashing invocar, de lo contrario no hacerlo.
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Digitalizacion de despacho en AFIP OK: " + reciboAvisoDigit.codError + "  " + reciboAvisoDigit.descError);

                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}', i139608 = {4}  where i139548 = '{2}' and i139603 = '{3}'", 0, "OK. Procesado", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}', i139608 = {4}  where i139548 = '{2}' and i139603 = '{3}'  and i139578 = '{5}' ", 0, "OK. Procesado", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal, solicitudFirmaDigital.sigea);

                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    DR.result = DigitalizacionResponse.results.Ok;
                    DR.reciboAvisoDigit = reciboAvisoDigit;
                    DR.autenticacion = autenticacion;
                    DR.solicitudFirmaDigital = solicitudFirmaDigital;
                    DR.codError = 0;
                    DR.descError = "OK. Procesado";

                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Digitalizacion de despacho en AFIP ERROR: " + reciboAvisoDigit.codError + "  " + reciboAvisoDigit.descError);
                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}',  i139608 = {4} where i139548 = '{2}' and i139603 = '{3}' ", reciboAvisoDigit.codError, reciboAvisoDigit.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(),  i139628 = '{0}', i139630 = '{1}',  i139608 = {4} where i139548 = '{2}' and i139603 = '{3}'  and i139578 = '{5}'", reciboAvisoDigit.codError, reciboAvisoDigit.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.cantidadTotal, solicitudFirmaDigital.sigea);

                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);

                    DR.result = DigitalizacionResponse.results.error;
                    DR.reciboAvisoDigit = reciboAvisoDigit;
                    DR.autenticacion = autenticacion;
                    DR.solicitudFirmaDigital = solicitudFirmaDigital;
                    DR.codError = reciboAvisoDigit.codError;
                    DR.descError = reciboAvisoDigit.descError;
                }
                return DR;
            }
            catch (Exception ex)
            {
                try
                {
                    ZClass.raiseerror(ex);
                    var exstr = ex.ToString();
                    if (exstr.Length > 199) exstr = exstr.Substring(0, 199);

                    var queryAcepted = string.Empty;
                    if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
                    {

                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(), i139630 = '{0}' where i139548 = '{1}'  and i139603 = '{2}'", exstr, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
                    }
                    else
                    {
                        queryAcepted = string.Format(@"update doc_i139072 set i139627 = GETDATE(), i139630 = '{0}' where i139548 = '{1}'  and i139603 = '{2}'  and i139578 = '{3}'", exstr, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
                    }
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryAcepted);
                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                DR.reciboAvisoDigit = null;
                DR.autenticacion = null;
                DR.solicitudFirmaDigital = solicitudFirmaDigital;
                DR.error = ex.ToString();
                DR.codError = 999;
                DR.descError = DR.error;
                return DR;
            }

        }


        [AcceptVerbs("POST")]
        [AllowAnonymous]
        [Route("ConsultaDespacho")]
        public IHttpActionResult ConsultaDespacho(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {
                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                EstadoResponse RR = _listaEstadoResponse(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ConsultaDespachoAdhoc")]
        public IHttpActionResult ConsultaDespachoAdhoc(int userId, string nrodespacho, string codigo, string sigea)
        {
            try
            {
                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));

                string error = string.Empty;

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.nroDespacho = nrodespacho;
                solicitudFirmaDigital.codigo = codigo;
                solicitudFirmaDigital.sigea = sigea;


                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                EstadoResponse RR = _listaEstadoResponseAdHoc(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        //[AcceptVerbs("GET", "POST")]
        //[AllowAnonymous]
        //[Route("GetLegajos")]
        //public IHttpActionResult GetLegajos(SolicitudFirmaDigital solicitudFirmaDigital)
        //{
        //    try
        //    {
        //        var user = GetUser(solicitudFirmaDigital.userId);
        //        if (user == null)
        //            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
        //                new HttpError(StringHelper.InvalidUser)));


        //        string error = string.Empty;

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

        //        ListadoResponse RR = _PndListaEndoResponse(solicitudFirmaDigital);

        //        var js = JsonConvert.SerializeObject(RR);
        //        return Ok(js);


        //    }
        //    catch (Exception e)
        //    {
        //        Zamba.Core.ZClass.raiseerror(e);
        //        return Ok(e);
        //    }

        //}


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLegajosAll")]
        public IHttpActionResult GetLegajosAll(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            try
            {
                var user = GetUser(solicitudFirmaDigital.userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));


                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                ListadoResponse RR = _PndListaEndoResponseAll(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLegajosAllService")]
        public IHttpActionResult GetLegajosAllService(Int64 userId)
        {
            try
            {
                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.userId = userId;

                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                ListadoResponse RR = _PndListaEndoResponseAll(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLegajosAllDesp")]
        public IHttpActionResult GetLegajosAllDesp(Int64 despachante, Int64 userId, Int64 days)
        {
            try
            {

                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.userId = userId;

                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachante: " + despachante.ToString());

                ListadoResponse RR = _PndListaEndoResponseAllDesp(solicitudFirmaDigital, despachante, days);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLegajosAllDespByDate")]
        public IHttpActionResult GetLegajosAllDespByDate(Int64 despachante, Int64 userId, string Date)
        {
            try
            {

                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.userId = userId;

                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachante: " + despachante.ToString());

                ListadoResponse RR = _PndListaEndoResponseAllDespByDate(solicitudFirmaDigital, despachante, Date);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLegajosAllApi/{id}")]
        public IHttpActionResult GetLegajosAllApi(int id)
        {
            try
            {
                var user = GetUser(id);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.userId = id;

                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                ListadoResponse RR = _PndListaEndoResponseAll(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);
                return Ok(js);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
                return Ok(e);
            }

        }


        private static EstadoResponse _listaEstadoResponse(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio

            //3- Aviso Recepcion

            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");

            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

            InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

            //obtener datos del despacho
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno datos del despacho para la recepcion");

            var querydespacho = string.Empty;

            if (string.IsNullOrEmpty(solicitudFirmaDigital.sigea))
            {
                querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate,i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE  from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
            }
            else
            {
                querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate,i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE  from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'  and i139578 = '{2}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo, solicitudFirmaDigital.sigea);
            }
            DataSet dsDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, querydespacho);

            DataRow r = dsDespacho.Tables[0].Rows[0];

            EstadoResponse RR = new EstadoResponse();

            solicitudFirmaDigital.nroLegajo = r["nroLegajo"].ToString();
            solicitudFirmaDigital.cuitPSAD = PSADCUIT;
            solicitudFirmaDigital.codigo = r["codigo"].ToString();
            solicitudFirmaDigital.ticket = (r["ticket"] != null) ? r["ticket"].ToString() : string.Empty;
            solicitudFirmaDigital.sigea = (r["sigea"] != null) ? r["sigea"].ToString() : string.Empty;

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando consulta de despacho en AFIP");

            ListaEstadoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioListaEstadoRequestRequest(autenticacion, solicitudFirmaDigital.nroLegajo,
            solicitudFirmaDigital.cuitPSAD, solicitudFirmaDigital.codigo, solicitudFirmaDigital.ticket,
            solicitudFirmaDigital.sigea);

            var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
            //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


            if (listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr == 0)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP OK");

                RR.result = EstadoResponse.results.Ok;
                RR.reciboEstado = listaEstadoResponse.Body.ListaEstadoResult.Recibo;
                RR.autenticacion = autenticacion;
                RR.solicitudFirmaDigital = solicitudFirmaDigital;
                RR.codError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr;
                RR.descError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr;
                RR.estado = listaEstadoResponse.Body.ListaEstadoResult.LegajoEstado;
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP ERROR: " + listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr);

                RR.result = EstadoResponse.results.error;
                RR.reciboEstado = listaEstadoResponse.Body.ListaEstadoResult.Recibo;
                RR.autenticacion = autenticacion;
                RR.solicitudFirmaDigital = solicitudFirmaDigital;
                RR.codError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr;
                RR.descError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr;
            }


            return RR;
        }




        private static EstadoResponse _listaEstadoResponseAdHoc(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio

            //3- Aviso Recepcion

            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");

            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

            InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

            //obtener datos del despacho
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno datos del despacho para la recepcion");



            EstadoResponse RR = new EstadoResponse();

            solicitudFirmaDigital.nroLegajo = solicitudFirmaDigital.nroDespacho;
            solicitudFirmaDigital.cuitPSAD = PSADCUIT;
            solicitudFirmaDigital.codigo = solicitudFirmaDigital.codigo;
            solicitudFirmaDigital.ticket = string.Empty;
            solicitudFirmaDigital.sigea = (solicitudFirmaDigital.sigea != null) ? solicitudFirmaDigital.sigea : string.Empty;

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando consulta de despacho en AFIP");

            ListaEstadoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioListaEstadoRequestRequest(autenticacion, solicitudFirmaDigital.nroLegajo,
            solicitudFirmaDigital.cuitPSAD, solicitudFirmaDigital.codigo, solicitudFirmaDigital.ticket,
            solicitudFirmaDigital.sigea);

            var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
            //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


            if (listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr == 0)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP OK");

                RR.result = EstadoResponse.results.Ok;
                RR.reciboEstado = listaEstadoResponse.Body.ListaEstadoResult.Recibo;
                RR.autenticacion = autenticacion;
                RR.solicitudFirmaDigital = solicitudFirmaDigital;
                RR.codError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr;
                RR.descError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr;
                RR.estado = listaEstadoResponse.Body.ListaEstadoResult.LegajoEstado;
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP ERROR: " + listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr);

                RR.result = EstadoResponse.results.error;
                RR.reciboEstado = listaEstadoResponse.Body.ListaEstadoResult.Recibo;
                RR.autenticacion = autenticacion;
                RR.solicitudFirmaDigital = solicitudFirmaDigital;
                RR.codError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.CodErr;
                RR.descError = listaEstadoResponse.Body.ListaEstadoResult.Recibo.DescErr;
            }


            return RR;
        }




        //private ListadoResponse _PndListaEndoResponse(SolicitudFirmaDigital solicitudFirmaDigital)
        //{
        //    //0- Obtener Parametros del Servicio


        //    //3- Aviso Recepcion

        //    var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
        //    var servicioWConsDepFiel = new ServicioWConsDepFiel();

        //    ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
        //    ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
        //    ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
        //    ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");

        //    var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

        //    InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

        //    //obtener datos del despacho
        //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno datos del despacho para la recepcion");

        //    List<string> codigos = new List<string>();
        //    codigos.Add("000");
        //    codigos.Add("001");
        //    codigos.Add("002");
        //    codigos.Add("003");

        //    ListadoResponse RR = new ListadoResponse();


        //    foreach (string codigo in codigos)
        //    {

        //        solicitudFirmaDigital.cuitPSAD = PSADCUIT;
        //        solicitudFirmaDigital.codigo = codigo;

        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando consulta de despacho en AFIP");

        //        DateTime FechaDesde = DateTime.Now.AddMonths(-2);
        //        DateTime FechaHasta = DateTime.Now;

        //        PndListaEndoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioPndListaEndoRequest(autenticacion,
        //               solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.codigo, FechaDesde, FechaHasta);

        //        if (listaEstadoResponse.Body.PndListaEndoResult.Legajos != null)
        //        {
        //            foreach (Legajo l in listaEstadoResponse.Body.PndListaEndoResult.Legajos)
        //            {

        //                try
        //                {
        //                    string selectLegajo = string.Format(@"select count(1) from  doc_i139072 where i139548 = '{0}' and i139603 = '{1}'", l.NroLegajo, l.Codigo);
        //                    object LegajoExiste = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, selectLegajo);
        //                    if (!(LegajoExiste is DBNull) && (LegajoExiste.ToString().Length > 0 && Int64.Parse(LegajoExiste.ToString()) > 0))
        //                    {
        //                        //el legajo existe lo actualizamos
        //                        string updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = '{5}',i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}'",
        //                            l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
        //                        //  Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, updateLegajo);

        //                        try
        //                        {
        //                            EstadoResponse ER = _listaEstadoResponse(new SolicitudFirmaDigital() { nroDespacho = l.NroLegajo, codigo = l.Codigo });
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            ZClass.raiseerror(ex);
        //                        }


        //                    }
        //                    else
        //                    {
        //                        Results_Business rb = new Results_Business();
        //                        //el legajo no existe lo generamos en zamba
        //                        NewResult nr = rb.GetNewNewResult(139072);
        //                        nr.get_GetIndexById(139603).DataTemp = l.Codigo;

        //                        nr.get_GetIndexById(139600).DataTemp = l.CuitDeclarante;
        //                        nr.get_GetIndexById(139600).dataDescriptionTemp = l.DescDeclarante;
        //                        nr.get_GetIndexById(139645).DataTemp = l.CuitDeclarante;
        //                        nr.get_GetIndexById(139579).DataTemp = l.DescDeclarante;

        //                        nr.get_GetIndexById(149651).DataTemp = l.CuitIE;
        //                        nr.get_GetIndexById(149651).dataDescriptionTemp = l.DescIE;
        //                        nr.get_GetIndexById(26296).DataTemp = l.CuitIE;
        //                        nr.get_GetIndexById(139562).DataTemp = l.DescIE;

        //                        nr.get_GetIndexById(26405).DataTemp = l.FechaEndo.ToString();
        //                        nr.get_GetIndexById(139551).DataTemp = l.FechaOfic.ToString();
        //                        nr.get_GetIndexById(139559).DataTemp = l.ImporteLiq.ToString();
        //                        nr.get_GetIndexById(139548).DataTemp = l.NroLegajo;
        //                        //nr.get_GetIndexById().DataTemp = l.NroReferencia;
        //                        //nr.get_GetIndexById().DataTemp = l.OptoCambioVia;
        //                        nr.get_GetIndexById(139578).DataTemp = l.Sigea;
        //                        nr.get_GetIndexById(139618).DataTemp = l.Ticket;
        //                        // nr.get_GetIndexById(139558).DataTemp = "2";
        //                        nr.get_GetIndexById(139638).DataTemp = "ENDO";
        //                        rb.InsertNew(ref nr, false, false, false, false, true, false, false);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    RR.result = ListadoResponse.results.error;
        //                    RR.autenticacion = autenticacion;
        //                    RR.codError = 9999;
        //                    RR.descError = ex.ToString();
        //                }
        //            }

        //            var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented,
        //new JsonSerializerSettings
        //{
        //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //});
        //            //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
        //            //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


        //            if (listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr == 0)
        //            {
        //                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP OK");

        //                RR.result = ListadoResponse.results.Ok;
        //                RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
        //                RR.autenticacion = autenticacion;
        //                RR.solicitudFirmaDigital = solicitudFirmaDigital;
        //                RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
        //                RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
        //                RR.Legajos.AddRange(listaEstadoResponse.Body.PndListaEndoResult.Legajos);
        //            }
        //            else
        //            {
        //                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho en AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);

        //                RR.result = ListadoResponse.results.error;
        //                RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
        //                RR.autenticacion = autenticacion;
        //                RR.solicitudFirmaDigital = solicitudFirmaDigital;
        //                RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
        //                RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
        //            }
        //        }

        //    }
        //    return RR;
        //}

        public class onlineLog
        {
            public string title { get; set; }
            public long id { get; set; }
            public DateTime date { get; set; }
            public string details { get; set; }

            public onlineLog(long id, string title, string details, DateTime date)
            {
                this.id = id;
                this.title = title;
                this.details = details;
                this.date = date;
            }
        }

        private void AddonlineLog(List<string> lista, string title)
        {
            try
            {
                lista.Add(title);
                if (OnlineLog.Count > 5000) OnlineLog.Clear();
                OnlineLog.Add(new onlineLog(OnlineLog.Count + 1, title, string.Empty, DateTime.Now));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private static List<onlineLog> OnlineLog = new List<onlineLog>();


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetOnlineLog")]
        public IHttpActionResult GetOnlineLog(long? lastId)
        {
            var _lastId = lastId.HasValue ? lastId.Value : 0;
            if (_lastId > OnlineLog.Count) _lastId = 0;
            var results = OnlineLog.Where(x => x.id > _lastId).ToList();
            return Ok(results);
        }
        private ListadoResponse _PndListaEndoResponseAll(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio


            //3- Aviso Recepcion
            string currentdatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");


            //obtener datos del despacho
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno despachantes para solicitar");
            var querydespacho = " select i139600 cuitDeclarante from doc_i139074 i  WITH(NOLOCK)  where i139600 is not null";
            DataSet dsDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, querydespacho);

            List<string> codigos = new List<string>();
            codigos.Add("000");
            codigos.Add("001");
            codigos.Add("002");
            codigos.Add("003");
            codigos.Add("004");
            codigos.Add("100");
            codigos.Add("101");

            ListadoResponse RR = new ListadoResponse();

            List<string> TraceAFIPNoNews = new List<string>();
            List<string> TraceAFIPBrief = new List<string>();

            Int64 conteodespachante = 0;
            Int64 conteototal = 0;
            Int64 totalbackdays = Int64.Parse(ZOptBusiness.GetValueOrDefault("ModocTotalBackDays", "30"));

            if (DateTime.Now.Hour == 2) totalbackdays = 365;
            if (DateTime.Now.Hour == 19) totalbackdays = 90;

            foreach (DataRow r in dsDespacho.Tables[0].Rows)
            {
                conteodespachante++;

                List<string> TraceAFIP = new List<string>();
                Int64 conteodespachanteTotal = 0;
                Int64 conteodespachanteNuevos = 0;

                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");

                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Cantidad de Despachantes: " + dsDespacho.Tables[0].Rows.Count.ToString());

                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
                AddonlineLog(TraceAFIP, System.Environment.NewLine);

                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/" + dsDespacho.Tables[0].Rows.Count.ToString() + " : " + r["cuitDeclarante"].ToString());

                ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
                ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

                ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/" + dsDespacho.Tables[0].Rows.Count.ToString() + " : " + r["cuitDeclarante"].ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

                try
                {

                    Boolean foundLegajos = true;

                    foreach (string codigo in codigos)
                    {
                        int currentDays = 1;
                        int emptyDays = int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                        DateTime FechaDesde = DateTime.Now.AddDays(-currentDays);
                        DateTime FechaHasta = DateTime.Now;
                        foundLegajos = true;
                        emptyDays = 0;

                        while (foundLegajos == true || emptyDays <= totalbackdays)
                        {


                            try
                            {
                                AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                                solicitudFirmaDigital.cuitDeclarante = r["cuitDeclarante"].ToString();
                                solicitudFirmaDigital.codigo = codigo;

                                ////string ModocDefaultMonthsAfipEndoService = ZOptBusiness.GetValueOrDefault("ModocMonthsAfip-" + r["cuitDeclarante"].ToString(), "1");
                                //DateTime FechaDesde = DateTime.Now.AddDays(-currentDaysD);
                                //DateTime FechaHasta = DateTime.Now.AddDays(-currentDaysH); 

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "------- Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------  Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());


                                var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

                                InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

                                PndListaEndoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioPndListaEndoRequest(autenticacion,
                                       solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.codigo, FechaDesde, FechaHasta);
                                //try
                                //{
                                //    ZOptBusiness.InsertUpdateValue("ModocMonthsAfip-" + r["cuitDeclarante"].ToString(), "1");
                                //}
                                //catch (Exception)
                                //{

                                //}


                                if (listaEstadoResponse.Body.PndListaEndoResult.Legajos != null)
                                {

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachos Obtenidos para el Despachante: " + r["cuitDeclarante"].ToString() + " Cantidad: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " CANTIDAD: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                    Int64 conteo = 0;
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la Existencia de cada Despacho");
                                    foreach (Legajo l in listaEstadoResponse.Body.PndListaEndoResult.Legajos)
                                    {

                                        try
                                        {
                                            conteodespachanteTotal++;
                                            conteo++;
                                            conteototal++;

                                            string selectLegajo = string.Empty;
                                            if (codigo == "004" || codigo == "002" || codigo == "003")
                                            {
                                                selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK)  where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", l.NroLegajo, l.Codigo, l.Sigea);
                                            }
                                            else
                                            {
                                                selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK) where i139548 = '{0}' and i139603 = '{1}'", l.NroLegajo, l.Codigo);
                                            }
                                            object LegajoExiste = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, selectLegajo);
                                            if (!(LegajoExiste is DBNull) && (LegajoExiste.ToString().Length > 0 && Int64.Parse(LegajoExiste.ToString()) > 0))
                                            {
                                                //el legajo existe lo actualizamos

                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho ya existe, se actualiza: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " : Ya existe, se actualiza");

                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho ya existe, se actualiza: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " : Ya existe, se actualiza");

                                                string updateLegajo = string.Empty;
                                                if (codigo == "004" || codigo == "003")
                                                {
                                                    updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}' and i139578 = '{8}'", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
                                                }
                                                else if (codigo == "101")
                                                {
                                                    updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i139551 =  CONVERT(datetime,'{5}',120),i139559 = '{6}',i139578 = '{7}' where i139548 = '{2}' and i139603 = '{3}' and i139578 = '{7}'", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
                                                }
                                                else
                                                {
                                                    updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}' where i139548 = '{2}' and i139603 = '{3}' ", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq);
                                                }

                                                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, updateLegajo);

                                                //try
                                                //{
                                                //    EstadoResponse ER = _listaEstadoResponse(new SolicitudFirmaDigital() { nroDespacho = l.NroLegajo, codigo = l.Codigo });
                                                //}
                                                //catch (Exception ex)
                                                //{
                                                //    ZClass.raiseerror(ex);
                                                //}


                                            }
                                            else
                                            {
                                                conteodespachanteNuevos++;
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho NO existe, se inserta: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " NUEVO ");

                                                Results_Business rb = new Results_Business();
                                                //el legajo no existe lo generamos en zamba
                                                INewResult nr = rb.GetNewNewResult(139072);
                                                nr.get_GetIndexById(139603).DataTemp = l.Codigo;

                                                nr.get_GetIndexById(139600).DataTemp = l.CuitDeclarante;
                                                nr.get_GetIndexById(139600).dataDescriptionTemp = l.DescDeclarante;
                                                nr.get_GetIndexById(139645).DataTemp = l.CuitDeclarante;
                                                nr.get_GetIndexById(139579).DataTemp = l.DescDeclarante;

                                                nr.get_GetIndexById(149651).DataTemp = l.CuitIE;
                                                nr.get_GetIndexById(149651).dataDescriptionTemp = l.DescIE;
                                                nr.get_GetIndexById(26296).DataTemp = l.CuitIE;
                                                nr.get_GetIndexById(139562).DataTemp = l.DescIE;

                                                nr.get_GetIndexById(26405).DataTemp = l.FechaEndo.ToString();
                                                nr.get_GetIndexById(139551).DataTemp = l.FechaOfic.ToString();
                                                nr.get_GetIndexById(139559).DataTemp = l.ImporteLiq.ToString();
                                                nr.get_GetIndexById(139548).DataTemp = l.NroLegajo;
                                                //nr.get_GetIndexById().DataTemp = l.NroReferencia;
                                                //nr.get_GetIndexById().DataTemp = l.OptoCambioVia;
                                                nr.get_GetIndexById(139578).DataTemp = l.Sigea;
                                                nr.get_GetIndexById(139618).DataTemp = l.Ticket;
                                                // nr.get_GetIndexById(139558).DataTemp = "2";
                                                nr.get_GetIndexById(139638).DataTemp = "ENDO";
                                                rb.InsertNew(ref nr, false, false, false, false, true, false, false);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                            AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                            AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                            RR.result = ListadoResponse.results.error;
                                            RR.autenticacion = autenticacion;
                                            RR.codError = 9999;
                                            RR.descError = ex.ToString();
                                        }
                                    }

                                    var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                                    //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
                                    //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


                                    if (listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr == 0)
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP OK");
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                        AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP OK");

                                        RR.result = ListadoResponse.results.Ok;
                                        RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                        RR.autenticacion = autenticacion;
                                        RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                        RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                        RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                        RR.Legajos.AddRange(listaEstadoResponse.Body.PndListaEndoResult.Legajos);
                                    }
                                    else
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                        AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                        RR.result = ListadoResponse.results.error;
                                        RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                        RR.autenticacion = autenticacion;
                                        RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                        RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                        RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                    }
                                }
                                else
                                {
                                    foundLegajos = false;
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                                }

                            }
                            catch (Exception ex)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                ZClass.raiseerror(ex);
                            }

                            if (foundLegajos)
                            {
                                FechaDesde = FechaDesde.AddDays(-currentDays);
                                FechaHasta = FechaHasta.AddDays(-currentDays);
                            }
                            else
                            {
                                FechaHasta = FechaDesde;
                                FechaDesde = FechaDesde.AddDays(-Int64.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5")));
                                emptyDays += int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                    ZClass.raiseerror(ex);
                }

                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN " + conteodespachante.ToString() + " FIN Consulta DESPACHANTE CUIT: " + r["cuitDeclarante"].ToString());



                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN PROCESO --------------------------------------------");

                AddonlineLog(TraceAFIPBrief, r["cuitDeclarante"].ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString());

                if (conteodespachanteTotal > 0)
                {

                    try
                    {
                        if (ZOptBusiness.GetValueOrDefault("ModocSendOKeMail", "false") == "true")
                        {
                            ISendMailConfig mail = new SendMailConfig();

                            mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                            mail.MailType = MailTypes.NetMail;
                            mail.SaveHistory = false;
                            mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                            mail.Subject = "Zamba - AFIP: " + r["cuitDeclarante"].ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString() + " " + currentdatetime;
                            mail.Body = string.Join(System.Environment.NewLine, TraceAFIP);
                            mail.IsBodyHtml = true;
                            mail.LinkToZamba = false;

                            MessagesBusiness.SendQuickMail(mail);
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }
                else
                {
                    TraceAFIPNoNews.AddRange(TraceAFIP.ToArray());
                }
            }

            try
            {
                if (ZOptBusiness.GetValueOrDefault("ModocSendSinDespachoseMail", "false") == "true")
                {
                    ISendMailConfig mail = new SendMailConfig();

                    mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    mail.MailType = MailTypes.NetMail;
                    mail.SaveHistory = false;
                    mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                    mail.Subject = "Zamba - AFIP: DESPACHANTES SIN DESPACHOS EN ENDO " + currentdatetime;
                    mail.Body = string.Join(System.Environment.NewLine, TraceAFIPNoNews);
                    mail.IsBodyHtml = true;
                    mail.LinkToZamba = false;

                    MessagesBusiness.SendQuickMail(mail);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            try
            {

                AddonlineLog(TraceAFIPBrief, "Total Despachos ENDO: " + conteototal.ToString());

                ISendMailConfig mail = new SendMailConfig();

                mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                mail.MailType = MailTypes.NetMail;
                mail.SaveHistory = false;
                mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                mail.Subject = "Zamba - AFIP: RESUMEN " + currentdatetime;
                mail.Body = string.Join(System.Environment.NewLine, TraceAFIPBrief);
                mail.IsBodyHtml = true;
                mail.LinkToZamba = false;

                MessagesBusiness.SendQuickMail(mail);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return RR;
        }


        private ListadoResponse _PndListaEndoResponseAllDespByDate(SolicitudFirmaDigital solicitudFirmaDigital, Int64 Despachante, string date)
        {
            //0- Obtener Parametros del Servicio


            //3- Aviso Recepcion
            string currentdatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");


            //obtener datos del despacho

            List<string> codigos = new List<string>();
            codigos.Add("000");
            codigos.Add("001");
            codigos.Add("002");
            codigos.Add("003");
            codigos.Add("004");
            codigos.Add("100");
            codigos.Add("101");

            ListadoResponse RR = new ListadoResponse();

            List<string> TraceAFIPNoNews = new List<string>();
            List<string> TraceAFIPBrief = new List<string>();

            Int64 conteodespachante = 0;
            Int64 conteototal = 0;
            Int64 totalbackdays = 100;


            conteodespachante++;

            List<string> TraceAFIP = new List<string>();
            Int64 conteodespachanteTotal = 0;
            Int64 conteodespachanteNuevos = 0;

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Cantidad de Despachantes: 1");

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
            AddonlineLog(TraceAFIP, System.Environment.NewLine);

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/1 : " + Despachante.ToString());

            ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
            ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

            ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/1 : " + Despachante.ToString());
            ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

            try
            {

                Boolean foundLegajos = true;

                foreach (string codigo in codigos)
                {
                    int currentDays = 1;
                    int emptyDays = int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                    DateTime FechaDesde = DateTime.Now.AddDays(-currentDays);
                    DateTime FechaHasta = DateTime.Now;
                    foundLegajos = true;
                    emptyDays = 0;

                    while (foundLegajos == true || emptyDays <= totalbackdays)
                    {


                        try
                        {
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);

                            solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                            solicitudFirmaDigital.cuitDeclarante = Despachante.ToString();
                            solicitudFirmaDigital.codigo = codigo;

                            ////string ModocDefaultMonthsAfipEndoService = ZOptBusiness.GetValueOrDefault("ModocMonthsAfip-" + Despachante.ToString(), "1");
                            //DateTime FechaDesde = DateTime.Now.AddDays(-currentDaysD);
                            //DateTime FechaHasta = DateTime.Now.AddDays(-currentDaysH); 

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "------- Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());
                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------  Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());


                            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

                            InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

                            PndListaEndoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioPndListaEndoRequest(autenticacion,
                                   solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.codigo, FechaDesde, FechaHasta);
                            //try
                            //{
                            //    ZOptBusiness.InsertUpdateValue("ModocMonthsAfip-" + Despachante.ToString(), "1");
                            //}
                            //catch (Exception)
                            //{

                            //}


                            if (listaEstadoResponse.Body.PndListaEndoResult.Legajos != null)
                            {

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachos Obtenidos para el Despachante: " + Despachante.ToString() + " Cantidad: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " CANTIDAD: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                Int64 conteo = 0;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la Existencia de cada Despacho");
                                foreach (Legajo l in listaEstadoResponse.Body.PndListaEndoResult.Legajos)
                                {

                                    try
                                    {
                                        conteodespachanteTotal++;
                                        conteo++;
                                        conteototal++;

                                        string selectLegajo = string.Empty;
                                        if (codigo == "004" || codigo == "002" || codigo == "003")
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK)  where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", l.NroLegajo, l.Codigo, l.Sigea);
                                        }
                                        else
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK) where i139548 = '{0}' and i139603 = '{1}'", l.NroLegajo, l.Codigo);
                                        }
                                        object LegajoExiste = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, selectLegajo);
                                        if (!(LegajoExiste is DBNull) && (LegajoExiste.ToString().Length > 0 && Int64.Parse(LegajoExiste.ToString()) > 0))
                                        {
                                            //el legajo existe lo actualizamos
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho ya existe, se actualiza: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " : Ya existe, se actualiza");

                                            string updateLegajo = string.Empty;
                                            if (codigo == "004")
                                            {
                                                updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}' and i139578 = '{8}'", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
                                            }
                                            else
                                            {
                                                updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}' ", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq);
                                            }

                                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, updateLegajo);

                                            //try
                                            //{
                                            //    EstadoResponse ER = _listaEstadoResponse(new SolicitudFirmaDigital() { nroDespacho = l.NroLegajo, codigo = l.Codigo });
                                            //}
                                            //catch (Exception ex)
                                            //{
                                            //    ZClass.raiseerror(ex);
                                            //}


                                        }
                                        else
                                        {
                                            conteodespachanteNuevos++;
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho NO existe, se inserta: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " NUEVO ");

                                            Results_Business rb = new Results_Business();
                                            //el legajo no existe lo generamos en zamba
                                            INewResult nr = rb.GetNewNewResult(139072);
                                            nr.get_GetIndexById(139603).DataTemp = l.Codigo;

                                            nr.get_GetIndexById(139600).DataTemp = l.CuitDeclarante;
                                            nr.get_GetIndexById(139600).dataDescriptionTemp = l.DescDeclarante;
                                            nr.get_GetIndexById(139645).DataTemp = l.CuitDeclarante;
                                            nr.get_GetIndexById(139579).DataTemp = l.DescDeclarante;

                                            nr.get_GetIndexById(149651).DataTemp = l.CuitIE;
                                            nr.get_GetIndexById(149651).dataDescriptionTemp = l.DescIE;
                                            nr.get_GetIndexById(26296).DataTemp = l.CuitIE;
                                            nr.get_GetIndexById(139562).DataTemp = l.DescIE;

                                            nr.get_GetIndexById(26405).DataTemp = l.FechaEndo.ToString();
                                            nr.get_GetIndexById(139551).DataTemp = l.FechaOfic.ToString();
                                            nr.get_GetIndexById(139559).DataTemp = l.ImporteLiq.ToString();
                                            nr.get_GetIndexById(139548).DataTemp = l.NroLegajo;
                                            //nr.get_GetIndexById().DataTemp = l.NroReferencia;
                                            //nr.get_GetIndexById().DataTemp = l.OptoCambioVia;
                                            nr.get_GetIndexById(139578).DataTemp = l.Sigea;
                                            nr.get_GetIndexById(139618).DataTemp = l.Ticket;
                                            // nr.get_GetIndexById(139558).DataTemp = "2";
                                            nr.get_GetIndexById(139638).DataTemp = "ENDO";
                                            rb.InsertNew(ref nr, false, false, false, false, true, false, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                        AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                        RR.result = ListadoResponse.results.error;
                                        RR.autenticacion = autenticacion;
                                        RR.codError = 9999;
                                        RR.descError = ex.ToString();
                                    }
                                }

                                var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                                //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
                                //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


                                if (listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr == 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP OK");
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP OK");

                                    RR.result = ListadoResponse.results.Ok;
                                    RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                    RR.autenticacion = autenticacion;
                                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                    RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                    RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                    RR.Legajos.AddRange(listaEstadoResponse.Body.PndListaEndoResult.Legajos);
                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                    RR.result = ListadoResponse.results.error;
                                    RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                    RR.autenticacion = autenticacion;
                                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                    RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                    RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                }
                            }
                            else
                            {
                                foundLegajos = false;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                            }

                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);
                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);
                            ZClass.raiseerror(ex);
                        }

                        if (foundLegajos)
                        {
                            FechaDesde = FechaDesde.AddDays(-currentDays);
                            FechaHasta = FechaHasta.AddDays(-currentDays);
                        }
                        else
                        {
                            FechaHasta = FechaDesde;
                            FechaDesde = FechaDesde.AddDays(-Int64.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5")));
                            emptyDays += int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                ZClass.raiseerror(ex);
            }

            AddonlineLog(TraceAFIP, System.Environment.NewLine);
            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN " + conteodespachante.ToString() + " FIN Consulta DESPACHANTE CUIT: " + Despachante.ToString());



            AddonlineLog(TraceAFIP, System.Environment.NewLine);
            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN PROCESO --------------------------------------------");

            AddonlineLog(TraceAFIPBrief, Despachante.ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString());

            if (conteodespachanteTotal > 0)
            {

                try
                {
                    if (ZOptBusiness.GetValueOrDefault("ModocSendOKeMail", "false") == "true")
                    {
                        ISendMailConfig mail = new SendMailConfig();

                        mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        mail.MailType = MailTypes.NetMail;
                        mail.SaveHistory = false;
                        mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                        mail.Subject = "Zamba - AFIP: " + Despachante.ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString() + " " + currentdatetime;
                        mail.Body = string.Join(System.Environment.NewLine, TraceAFIP);
                        mail.IsBodyHtml = true;
                        mail.LinkToZamba = false;

                        MessagesBusiness.SendQuickMail(mail);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else
            {
                TraceAFIPNoNews.AddRange(TraceAFIP.ToArray());
            }


            try
            {
                if (ZOptBusiness.GetValueOrDefault("ModocSendSinDespachoseMail", "false") == "true")
                {
                    ISendMailConfig mail = new SendMailConfig();

                    mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    mail.MailType = MailTypes.NetMail;
                    mail.SaveHistory = false;
                    mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                    mail.Subject = "Zamba - AFIP: DESPACHANTES SIN DESPACHOS EN ENDO " + currentdatetime;
                    mail.Body = string.Join(System.Environment.NewLine, TraceAFIPNoNews);
                    mail.IsBodyHtml = true;
                    mail.LinkToZamba = false;

                    MessagesBusiness.SendQuickMail(mail);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            try
            {

                AddonlineLog(TraceAFIPBrief, "Total Despachos ENDO: " + conteototal.ToString());

                ISendMailConfig mail = new SendMailConfig();

                mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                mail.MailType = MailTypes.NetMail;
                mail.SaveHistory = false;
                mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                mail.Subject = "Zamba - AFIP: RESUMEN " + currentdatetime;
                mail.Body = string.Join(System.Environment.NewLine, TraceAFIPBrief);
                mail.IsBodyHtml = true;
                mail.LinkToZamba = false;

                MessagesBusiness.SendQuickMail(mail);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return RR;
        }

        private ListadoResponse _PndListaEndoResponseAllDesp(SolicitudFirmaDigital solicitudFirmaDigital, Int64 Despachante, Int64 days)
        {
            //0- Obtener Parametros del Servicio


            //3- Aviso Recepcion
            string currentdatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");


            //obtener datos del despacho

            List<string> codigos = new List<string>();
            codigos.Add("000");
            codigos.Add("001");
            codigos.Add("002");
            codigos.Add("003");
            codigos.Add("004");
            codigos.Add("100");
            codigos.Add("101");

            ListadoResponse RR = new ListadoResponse();

            List<string> TraceAFIPNoNews = new List<string>();
            List<string> TraceAFIPBrief = new List<string>();

            Int64 conteodespachante = 0;
            Int64 conteototal = 0;
            Int64 totalbackdays = days;


            conteodespachante++;

            List<string> TraceAFIP = new List<string>();
            Int64 conteodespachanteTotal = 0;
            Int64 conteodespachanteNuevos = 0;

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Cantidad de Despachantes: 1");

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
            AddonlineLog(TraceAFIP, System.Environment.NewLine);

            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/1 : " + Despachante.ToString());

            ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
            ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

            ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/1 : " + Despachante.ToString());
            ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

            try
            {

                Boolean foundLegajos = true;

                foreach (string codigo in codigos)
                {
                    int currentDays = 1;
                    int emptyDays = int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                    DateTime FechaDesde = DateTime.Now.AddDays(-currentDays);
                    DateTime FechaHasta = DateTime.Now;
                    foundLegajos = true;
                    emptyDays = 0;

                    while (foundLegajos == true || emptyDays <= totalbackdays)
                    {


                        try
                        {
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);

                            solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                            solicitudFirmaDigital.cuitDeclarante = Despachante.ToString();
                            solicitudFirmaDigital.codigo = codigo;

                            ////string ModocDefaultMonthsAfipEndoService = ZOptBusiness.GetValueOrDefault("ModocMonthsAfip-" + Despachante.ToString(), "1");
                            //DateTime FechaDesde = DateTime.Now.AddDays(-currentDaysD);
                            //DateTime FechaHasta = DateTime.Now.AddDays(-currentDaysH); 

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "------- Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());
                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------  Codigo: " + codigo + "  -  Fecha Desde: " + FechaDesde.ToShortDateString() + " - Fecha Hasta: " + FechaHasta.ToShortDateString());


                            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

                            InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

                            PndListaEndoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioPndListaEndoRequest(autenticacion,
                                   solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.codigo, FechaDesde, FechaHasta);
                            //try
                            //{
                            //    ZOptBusiness.InsertUpdateValue("ModocMonthsAfip-" + Despachante.ToString(), "1");
                            //}
                            //catch (Exception)
                            //{

                            //}


                            if (listaEstadoResponse.Body.PndListaEndoResult.Legajos != null)
                            {

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachos Obtenidos para el Despachante: " + Despachante.ToString() + " Cantidad: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " CANTIDAD: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                Int64 conteo = 0;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la Existencia de cada Despacho");
                                foreach (Legajo l in listaEstadoResponse.Body.PndListaEndoResult.Legajos)
                                {

                                    try
                                    {
                                        conteodespachanteTotal++;
                                        conteo++;
                                        conteototal++;

                                        string selectLegajo = string.Empty;
                                        if (codigo == "004" || codigo == "002" || codigo == "003")
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK)  where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", l.NroLegajo, l.Codigo, l.Sigea);
                                        }
                                        else
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072  WITH(NOLOCK) where i139548 = '{0}' and i139603 = '{1}'", l.NroLegajo, l.Codigo);
                                        }
                                        object LegajoExiste = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, selectLegajo);
                                        if (!(LegajoExiste is DBNull) && (LegajoExiste.ToString().Length > 0 && Int64.Parse(LegajoExiste.ToString()) > 0))
                                        {
                                            //el legajo existe lo actualizamos
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho ya existe, se actualiza: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " : Ya existe, se actualiza");

                                            string updateLegajo = string.Empty;
                                            if (codigo == "004")
                                            {
                                                updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}' and i139578 = '{8}'", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
                                            }
                                            else
                                            {
                                                updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = {5},i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}' ", l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq);
                                            }

                                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, updateLegajo);

                                            //try
                                            //{
                                            //    EstadoResponse ER = _listaEstadoResponse(new SolicitudFirmaDigital() { nroDespacho = l.NroLegajo, codigo = l.Codigo });
                                            //}
                                            //catch (Exception ex)
                                            //{
                                            //    ZClass.raiseerror(ex);
                                            //}


                                        }
                                        else
                                        {
                                            conteodespachanteNuevos++;
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho NO existe, se inserta: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo + " NUEVO ");

                                            Results_Business rb = new Results_Business();
                                            //el legajo no existe lo generamos en zamba
                                            INewResult nr = rb.GetNewNewResult(139072);
                                            nr.get_GetIndexById(139603).DataTemp = l.Codigo;

                                            nr.get_GetIndexById(139600).DataTemp = l.CuitDeclarante;
                                            nr.get_GetIndexById(139600).dataDescriptionTemp = l.DescDeclarante;
                                            nr.get_GetIndexById(139645).DataTemp = l.CuitDeclarante;
                                            nr.get_GetIndexById(139579).DataTemp = l.DescDeclarante;

                                            nr.get_GetIndexById(149651).DataTemp = l.CuitIE;
                                            nr.get_GetIndexById(149651).dataDescriptionTemp = l.DescIE;
                                            nr.get_GetIndexById(26296).DataTemp = l.CuitIE;
                                            nr.get_GetIndexById(139562).DataTemp = l.DescIE;

                                            nr.get_GetIndexById(26405).DataTemp = l.FechaEndo.ToString();
                                            nr.get_GetIndexById(139551).DataTemp = l.FechaOfic.ToString();
                                            nr.get_GetIndexById(139559).DataTemp = l.ImporteLiq.ToString();
                                            nr.get_GetIndexById(139548).DataTemp = l.NroLegajo;
                                            //nr.get_GetIndexById().DataTemp = l.NroReferencia;
                                            //nr.get_GetIndexById().DataTemp = l.OptoCambioVia;
                                            nr.get_GetIndexById(139578).DataTemp = l.Sigea;
                                            nr.get_GetIndexById(139618).DataTemp = l.Ticket;
                                            // nr.get_GetIndexById(139558).DataTemp = "2";
                                            nr.get_GetIndexById(139638).DataTemp = "ENDO";
                                            rb.InsertNew(ref nr, false, false, false, false, true, false, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                        AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                        AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                        RR.result = ListadoResponse.results.error;
                                        RR.autenticacion = autenticacion;
                                        RR.codError = 9999;
                                        RR.descError = ex.ToString();
                                    }
                                }

                                var newresultsreciboAvisoRecepAcept = JsonConvert.SerializeObject(listaEstadoResponse, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                                //  newresultsreciboAvisoRecepAcept = newresultsreciboAvisoRecepAcept.Replace("$", "");
                                //  XmlDocument docreciboAvisoRecepAcept = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsreciboAvisoRecepAcept);


                                if (listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr == 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP OK");
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP OK");

                                    RR.result = ListadoResponse.results.Ok;
                                    RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                    RR.autenticacion = autenticacion;
                                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                    RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                    RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                    RR.Legajos.AddRange(listaEstadoResponse.Body.PndListaEndoResult.Legajos);
                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);
                                    AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                    AddonlineLog(TraceAFIP, System.Environment.NewLine);

                                    RR.result = ListadoResponse.results.error;
                                    RR.reciboEstado = listaEstadoResponse.Body.PndListaEndoResult.Recibo;
                                    RR.autenticacion = autenticacion;
                                    RR.solicitudFirmaDigital = solicitudFirmaDigital;
                                    RR.codError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr;
                                    RR.descError = listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr;
                                }
                            }
                            else
                            {
                                foundLegajos = false;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                            }

                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);
                            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            AddonlineLog(TraceAFIP, System.Environment.NewLine);
                            ZClass.raiseerror(ex);
                        }

                        if (foundLegajos)
                        {
                            FechaDesde = FechaDesde.AddDays(-currentDays);
                            FechaHasta = FechaHasta.AddDays(-currentDays);
                        }
                        else
                        {
                            FechaHasta = FechaDesde;
                            FechaDesde = FechaDesde.AddDays(-Int64.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5")));
                            emptyDays += int.Parse(ZOptBusiness.GetValueOrDefault("ModocemptyDays", "5"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                AddonlineLog(TraceAFIP, System.Environment.NewLine);
                ZClass.raiseerror(ex);
            }

            AddonlineLog(TraceAFIP, System.Environment.NewLine);
            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN " + conteodespachante.ToString() + " FIN Consulta DESPACHANTE CUIT: " + Despachante.ToString());



            AddonlineLog(TraceAFIP, System.Environment.NewLine);
            AddonlineLog(TraceAFIP, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN PROCESO --------------------------------------------");

            AddonlineLog(TraceAFIPBrief, Despachante.ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString());

            if (conteodespachanteTotal > 0)
            {

                try
                {
                    if (ZOptBusiness.GetValueOrDefault("ModocSendOKeMail", "false") == "true")
                    {
                        ISendMailConfig mail = new SendMailConfig();

                        mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        mail.MailType = MailTypes.NetMail;
                        mail.SaveHistory = false;
                        mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                        mail.Subject = "Zamba - AFIP: " + Despachante.ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + " NUEVOS: " + conteodespachanteNuevos.ToString() + " " + currentdatetime;
                        mail.Body = string.Join(System.Environment.NewLine, TraceAFIP);
                        mail.IsBodyHtml = true;
                        mail.LinkToZamba = false;

                        MessagesBusiness.SendQuickMail(mail);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else
            {
                TraceAFIPNoNews.AddRange(TraceAFIP.ToArray());
            }


            try
            {
                if (ZOptBusiness.GetValueOrDefault("ModocSendSinDespachoseMail", "false") == "true")
                {
                    ISendMailConfig mail = new SendMailConfig();

                    mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    mail.MailType = MailTypes.NetMail;
                    mail.SaveHistory = false;
                    mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                    mail.Subject = "Zamba - AFIP: DESPACHANTES SIN DESPACHOS EN ENDO " + currentdatetime;
                    mail.Body = string.Join(System.Environment.NewLine, TraceAFIPNoNews);
                    mail.IsBodyHtml = true;
                    mail.LinkToZamba = false;

                    MessagesBusiness.SendQuickMail(mail);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            try
            {

                AddonlineLog(TraceAFIPBrief, "Total Despachos ENDO: " + conteototal.ToString());

                ISendMailConfig mail = new SendMailConfig();

                mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                mail.MailType = MailTypes.NetMail;
                mail.SaveHistory = false;
                mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                mail.Subject = "Zamba - AFIP: RESUMEN " + currentdatetime;
                mail.Body = string.Join(System.Environment.NewLine, TraceAFIPBrief);
                mail.IsBodyHtml = true;
                mail.LinkToZamba = false;

                MessagesBusiness.SendQuickMail(mail);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return RR;
        }

        public class ListadoResponse
        {
            public results result { get; internal set; }
            public string error;
            public InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion { get; internal set; }
            public InvocacionServWDigDepFiel.wConsDepFiel.Recibo reciboEstado { get; internal set; }
            public SolicitudFirmaDigital solicitudFirmaDigital { get; internal set; }
            public int codError { get; internal set; }
            public string descError { get; internal set; }
            public List<Legajo> Legajos { get; internal set; } = new List<Legajo>();

            public enum results
            {
                error = 1,
                Ok = 2
            }
        }



        public class EstadoResponse
        {
            public results result { get; internal set; }
            public string error;
            public InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion { get; internal set; }
            public InvocacionServWDigDepFiel.wConsDepFiel.Recibo reciboEstado { get; internal set; }
            public SolicitudFirmaDigital solicitudFirmaDigital { get; internal set; }
            public int codError { get; internal set; }
            public string descError { get; internal set; }
            public LegajoEstado estado { get; internal set; }

            public enum results
            {
                error = 1,
                Ok = 2,
                alreadyAcepted = 3
            }
        }



    }
}
