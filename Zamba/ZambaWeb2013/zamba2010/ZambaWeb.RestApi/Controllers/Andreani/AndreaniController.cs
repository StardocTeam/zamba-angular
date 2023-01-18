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
using System.Threading;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    //[Authorize]
    public class AndreaniServiciosController : ApiController
    {
        Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
        private string Andreani_URL;
        private string Andreani_Username;
        private string Andreani_Password;
        private string Andreani_Nro_contrato;
        private string Andreani_Nro_tracking_demo;

        public AndreaniServiciosController()
        {
            Andreani_URL = zopt.GetValue("andreani_url_service");
            Andreani_Username = zopt.GetValue("andreani_user");
            Andreani_Password = zopt.GetValue("andreani_password");
            Andreani_Nro_contrato = zopt.GetValue("andreani_contrato");
            Andreani_Nro_tracking_demo = zopt.GetValue("andreani_nro_tracking_demo");
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");

            }
        }
        #region "Login"
        [Route("api/andreaniServices/Login")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult Login(AndreaniServiceLoginRequest login)
        {
            try
            {
                GetToken(login);
                return Ok("");
            }
            catch (Exception ex)
            {
                return Ok(new AndreaniServiceLoginResponse() { error = ex.Message });
            }
        }
        private string GetToken(AndreaniServiceLoginRequest login, Boolean SegundoIntento = false)
        {
            string response = "";
            string token = "";
            try
            {
                if (HttpContext.Current.Application["andreani_token_value"] != null && !SegundoIntento)
                {
                    token = HttpContext.Current.Application["andreani_token_value"].ToString();
                }
                else
                {
                    login.userID = Andreani_Username;
                    login.Password = Andreani_Password;
                    string LoginEncondedBase64 = Convert.ToBase64String((
                                login.userID + ":" + login.Password)
                                .ToCharArray()
                                .Select(c => (byte)c)
                                .ToArray()
                            );
                    WebClient client = new WebClient();
                    client.Headers.Add("Authorization", "Basic " + LoginEncondedBase64);
                    client.DownloadString(Andreani_URL + "login");
                    token = client.ResponseHeaders["X-Authorization-token"].ToString();
                    HttpContext.Current.Application["andreani_token_value"] = token;
                }
                response = token;
            }
            catch (WebException exception)
            {
                if (!SegundoIntento)
                    return GetToken(new AndreaniServiceLoginRequest(), true);
                else
                    throw new Exception("Error al intentar autenticar");
            }
            return response;
        }

        #endregion

        #region "Ordenes de envio"
        [Route("api/andreaniServices/ActualizarInformacionEnvio")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult ActualizarInformacionEnvio(AndreaniServiceObtenerDatosEnvio request)
        {
            try
            {
                request.nro_guia = ObtenerNroGuiaConTracking(request.nro_tracking);
                InsertarNuevoItemNotificacionesAndreani(request.nro_tracking);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se llamo al servicio de andreani para actualizar informacion de la orden (" + request.nro_tracking + ")");
                IHttpActionResult ResponseObtenerOrden = ObtenerOrdenCreada(request);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se llamo al servicio de andreani para actualizar informacion del envio  (" + request.nro_tracking + ")");
                IHttpActionResult ResponseObtenerEnvio = ObtenerEnvio(request);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se llamo al servicio de andreani para actualizar informacion del tracking de envio  (" + request.nro_tracking + ")");
                IHttpActionResult ResponseObtenerTrazaEnvio = ObtenerTrackingEnvio(request);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se actualizo correctamente la informacion del envio (" + request.nro_tracking + ")");
                AndreaniServiceCrearNuevaOrdenResponse Notificaciones = new AndreaniServiceCrearNuevaOrdenResponse();
                string QueryNotificaciones = "select * from doc_i149106  where i150682 ='" + request.nro_tracking + "'";
                DataTable tblDatosNotificaciones = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryNotificaciones).Tables[0]; // 
                if (tblDatosNotificaciones.Rows.Count == 0)
                {
                    Notificaciones.NotificacionEnvio = "No hay notificaciones de andreani.";
                    Notificaciones.NotificacionOrdenEnvio = "No hay notificaciones de andreani.";
                    Notificaciones.NotificacionTracking = "No hay notificaciones de andreani.";
                }
                else
                {
                    Notificaciones.NotificacionOrdenEnvio = tblDatosNotificaciones.Rows[0]["I150726"].ToString();
                    Notificaciones.NotificacionEnvio = tblDatosNotificaciones.Rows[0]["I150727"].ToString();
                    Notificaciones.NotificacionTracking = tblDatosNotificaciones.Rows[0]["I150728"].ToString();
                }
                return Ok(Notificaciones);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(new AndreaniServiceCrearNuevaOrdenResponse() { error = "Se produjo un error al actualizar informacion del envio o no se halla la inf." });
            }
        }
        [Route("api/andreaniServices/CrearNuevaOrden")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult CrearNuevaOrden(NuevaOrdenRequest orden)
        {

            var user = GetUser(orden.userId);
            if (user == null)
                throw new Exception("Usuario Invalido");
            if (!String.IsNullOrEmpty(orden.sucursal_id))
                return (CrearNuevaOrdenConSucursal(orden));
            try
            {
                string QueryGuiaDeDespacho = "select i10589 as 'Id_despachante',i150682 as 'nro_tracking'  from doc_i139081 where I139614 = " + orden.nro_guia;
                DataTable tblDatosGuiaDeDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryGuiaDeDespacho).Tables[0]; // 
                if (tblDatosGuiaDeDespacho.Rows.Count == 0)
                    throw new Exception("la guia de despacho no existe o no tiene despachante asociado");
                DataRow RowGuiaDeDespacho = tblDatosGuiaDeDespacho.Rows[0];
                string despachante_id = RowGuiaDeDespacho["Id_despachante"].ToString();
                string nro_tracking = RowGuiaDeDespacho["nro_tracking"].ToString();
                if (nro_tracking.Length > 1)
                {
                    return Ok(new AndreaniServiceCrearNuevaOrdenResponse {  numeroDeTracking = nro_tracking });
                }
                if (despachante_id == "")
                    throw new Exception("la guia de despacho no existe o no tiene despachante asociado");
                string QueryDespacho = "select i139548 as 'Nro_despacho' from doc_i139072 where I139614 = " + orden.nro_guia;
                DataTable tblDatosDespachos = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryDespacho).Tables[0]; // 
                if (tblDatosDespachos.Rows.Count == 0)
                    return Ok(new AndreaniServiceCrearNuevaOrdenResponse() { error = "la guia no tiene despachos" });

                string QueryDespachante = "select i26285 as 'codigo_postal',SLST_S150681.Codigo as 'provincia',SLST_S26215.Descripcion as 'localidad',i139573 as 'calle',i139637 as 'numero',i26217 as 'telefono',i33705 as 'telefono_interno',i139565 as 'email',i139579 as 'nombre',i139600 as 'cuit',i10589 as 'id'from doc_i139074 left join slst_s26283 on doc_i139074.i26283 = slst_s26283.Codigo left join SLST_S150681 on slst_s26283.Descripcion = SLST_S150681.Descripcion left join SLST_S26215 on doc_i139074.I26215 = SLST_S26215.Codigo where i10589 =" + despachante_id;

                var ListaDespachos = String.Join(",", tblDatosDespachos.AsEnumerable().Select(n => n.ItemArray[0].ToString()).ToArray());

                DataTable tblDatosDespachante = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryDespachante).Tables[0]; // 
                if (tblDatosDespachante.Rows.Count == 0)
                    throw new Exception("la guia no tiene despachante asociado");
                AndreaniServiceCrearNuevaOrdenConDestinoRequest objNuevaOrden = new AndreaniServiceCrearNuevaOrdenConDestinoRequest();
                DataRow RowDespachante = tblDatosDespachante.Rows[0];
                if (!ValidarDatosDespachante(RowDespachante))
                    throw new Exception("Complete la informacion del despachante correctamente (CP, telefono, nombre, cuit, localidad, provincia, calle, numero, email).");
                objNuevaOrden.origen.postal.codigoPostal = Convert.ToInt32(RowDespachante["codigo_postal"].ToString());
                objNuevaOrden.origen.postal.localidad = RowDespachante["localidad"].ToString();
                objNuevaOrden.origen.postal.pais = "Argentina";
                objNuevaOrden.origen.postal.region = RowDespachante["provincia"].ToString(); // conectar con datos de provincias en la query (corregir la query)
                objNuevaOrden.origen.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido { meta = "calle", contenido = RowDespachante["calle"].ToString() });
                objNuevaOrden.origen.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido { meta = "numero", contenido = RowDespachante["numero"].ToString() });
                objNuevaOrden.remitente.apellidos = ".";
                AndreaniServiceTelefono itemTelefono = new AndreaniServiceTelefono();
                itemTelefono.tipo = 3;
                itemTelefono.numero = RowDespachante["telefono"].ToString();
                if (!String.IsNullOrEmpty(RowDespachante["telefono_interno"].ToString()))
                    itemTelefono.numero += " (int. " + RowDespachante["telefono_interno"].ToString() + ")";
                //                Telefono: Fax = 1 || Celular = 2 || Fijo = 3
                objNuevaOrden.remitente.telefonos.Add(itemTelefono);
                objNuevaOrden.remitente.eMail = RowDespachante["email"].ToString();
                objNuevaOrden.remitente.nombres = RowDespachante["nombre"].ToString();
                objNuevaOrden.remitente.tipoYNumeroDeDocumento = "CUIT " + RowDespachante["cuit"].ToString();
                objNuevaOrden.destino.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido() { meta = "calle", contenido = "San Jose" });
                objNuevaOrden.destino.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido() { meta = "numero", contenido = "1540" });
                objNuevaOrden.destino.postal.localidad = "C.A.B.A.";
                objNuevaOrden.destino.postal.pais = "Argentina";
                objNuevaOrden.destino.postal.codigoPostal = 1151;
                objNuevaOrden.destino.postal.region = "AR-B";
                objNuevaOrden.destinatario.apellidos = "Alcaraz";
                objNuevaOrden.destinatario.nombres = "Romina";
                AndreaniServiceTelefono itemTelefonoRemitente = new AndreaniServiceTelefono();
                itemTelefonoRemitente.tipo = 2;
                itemTelefonoRemitente.numero = "011154515412";
                //                Telefono: Fax = 1 || Celular = 2 || Fijo = 3
                objNuevaOrden.destinatario.telefonos.Add(itemTelefonoRemitente);
                objNuevaOrden.destinatario.tipoYNumeroDeDocumento = "DNI 32523412";
                objNuevaOrden.destinatario.eMail = "produccion@modoc.com.ar";
                objNuevaOrden.sucursalDeImposicion.id = "1"; // se obtiene de la lista
                objNuevaOrden.bultosParaEnviar.Add(new AndreaniServiceBultosCrearOrden
                {
                    altoCm = 30,
                    largoCm = 1,
                    anchoCm = 20,
                    kilos = tblDatosDespachos.Rows.Count, // cantidad de legajos,
                    valorDeclaradoConImpuestos = 4// RowDespachante["total_legajos"] falta total de legajos (kadir)
                }); ;
                objNuevaOrden.contrato = Andreani_Nro_contrato;

                objNuevaOrden.referencias.Add(new AndreaniServiceMetaContenido { meta = "nro_guia", contenido = orden.nro_guia  });
                objNuevaOrden.referencias.Add(new AndreaniServiceMetaContenido { meta = "idCliente", contenido = "1233" });
                string jsonMessage = JsonConvert.SerializeObject(objNuevaOrden);
                string location = "v1/ordenesDeEnvio/";
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);
                AndreaniServiceCrearNuevaOrdenResponse response = new AndreaniServiceCrearNuevaOrdenResponse();
                AndreaniServiceResponseRestAPI responseRestApi = new AndreaniServiceResponseRestAPI();
                responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location, jsonMessage);
                response = JsonConvert.DeserializeObject<AndreaniServiceCrearNuevaOrdenResponse>(responseRestApi.JsonResponse);
                Boolean OrdenInsertada = InsertarOrdenSinSucursalEnBase(objNuevaOrden, response, orden.nro_guia);
                if (!OrdenInsertada)
                    throw new Exception("Fallo la insercion de datos");

                response.originalJsonResponse = responseRestApi.JsonResponse;
                string queryUpdate = "";
                queryUpdate += "update doc_i139081 set ";
                queryUpdate += " I150682= '" + response.numeroDeTracking + "'";
                queryUpdate += " where I139614=" + orden.nro_guia;
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryUpdate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(new AndreaniServiceCrearNuevaOrdenResponse() { error = ex.Message });
            }
        }

        private Boolean ValidarDatosDespachante(DataRow DatosDespachante)
        {
            return !(
                    String.IsNullOrEmpty(DatosDespachante["codigo_postal"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["telefono"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["nombre"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["cuit"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["localidad"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["provincia"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["calle"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["numero"].ToString())
                    ||
                    String.IsNullOrEmpty(DatosDespachante["email"].ToString())
    );
        }

        private string ObtenerNroGuiaConTracking(string nro_tracking)
        {
            String StrSql = "select i139614 from doc_i139081 where i150682=  '" + nro_tracking + "'";
            DataTable tblDatosGuia = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, StrSql).Tables[0];
            if (tblDatosGuia.Rows.Count == 1)
                return tblDatosGuia.Rows[0][0].ToString();
            else
                return "";

        }

        private IHttpActionResult CrearNuevaOrdenConSucursal(NuevaOrdenRequest orden)
        {
            try
            {
                string QueryGuiaDeDespacho = "select i10589 as 'Id_despachante'  from doc_i139081 where I139614 = " + orden.nro_guia;
                DataTable tblDatosGuiaDeDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryGuiaDeDespacho).Tables[0]; // 
                if (tblDatosGuiaDeDespacho.Rows.Count == 0)
                    throw new Exception("No tiene despacho asociado la guia seleccionada.");
                DataRow RowGuiaDeDespacho = tblDatosGuiaDeDespacho.Rows[0];
                string despachante_id = RowGuiaDeDespacho["Id_despachante"].ToString();

                string QueryDespacho = "select i139548 as 'Nro_despacho' from doc_i139072 where I139614 = " + orden.nro_guia;
                DataTable tblDatosDespachos = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryDespacho).Tables[0]; // 
                if (tblDatosDespachos.Rows.Count == 0)
                    throw new Exception("la guia no tiene despachos.");

                string QueryDespachante = "select i26285 as 'codigo_postal',SLST_S150681.Codigo as 'provincia',SLST_S26215.Descripcion as 'localidad',i139573 as 'calle',i139637 as 'numero',i26217 as 'telefono',i33705 as 'telefono_interno',i139565 as 'email',i139579 as 'nombre',i139600 as 'cuit',i10589 as 'id'from doc_i139074 left join slst_s26283 on doc_i139074.i26283 = slst_s26283.Codigo left join SLST_S150681 on slst_s26283.Descripcion = SLST_S150681.Descripcion left join SLST_S26215 on doc_i139074.I26215 = SLST_S26215.Codigo where i10589 =" + despachante_id;

                var ListaDespachos = String.Join(",", tblDatosDespachos.AsEnumerable().Select(n => n.ItemArray[0].ToString()).ToArray());

                DataTable tblDatosDespachante = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryDespachante).Tables[0]; // 
                if (tblDatosDespachante.Rows.Count != 1)
                    return Ok(new AndreaniServiceCrearNuevaOrdenResponse() { error = "no tiene despachante asociado" });

                AndreaniServiceCrearNuevaOrdenConSucursalRequest objNuevaOrden = new AndreaniServiceCrearNuevaOrdenConSucursalRequest();
                DataRow RowDespachante = tblDatosDespachante.Rows[0];
                if (!ValidarDatosDespachante(RowDespachante))
                    throw new Exception("Complete la informacion del despachante correctamente (CP, telefono, nombre, cuit, localidad, provincia, calle, numero, email).");
                objNuevaOrden.origen.postal.codigoPostal = Convert.ToInt32(RowDespachante["codigo_postal"].ToString());
                objNuevaOrden.origen.postal.localidad = RowDespachante["localidad"].ToString();
                objNuevaOrden.origen.postal.pais = "Argentina";
                objNuevaOrden.origen.postal.region = RowDespachante["provincia"].ToString(); // conectar con datos de provincias en la query (corregir la query)
                objNuevaOrden.origen.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido { meta = "calle", contenido = RowDespachante["calle"].ToString() });
                objNuevaOrden.origen.postal.componentesDeDireccion.Add(new AndreaniServiceMetaContenido { meta = "numero", contenido = RowDespachante["numero"].ToString() });
                objNuevaOrden.remitente.apellidos = ".";
                AndreaniServiceTelefono itemTelefono = new AndreaniServiceTelefono();
                itemTelefono.tipo = 3;
                itemTelefono.numero = RowDespachante["telefono"].ToString();
                if (!String.IsNullOrEmpty(RowDespachante["telefono_interno"].ToString()))
                    itemTelefono.numero += " (int. " + RowDespachante["telefono_interno"].ToString() + ")";
                //                Telefono: Fax = 1 || Celular = 2 || Fijo = 3
                objNuevaOrden.remitente.telefonos.Add(itemTelefono);
                objNuevaOrden.remitente.eMail = RowDespachante["email"].ToString();
                objNuevaOrden.remitente.nombres = RowDespachante["nombre"].ToString();
                objNuevaOrden.remitente.tipoYNumeroDeDocumento = "CUIT " + RowDespachante["cuit"].ToString();
                objNuevaOrden.destino.sucursal.id = orden.sucursal_id;
                objNuevaOrden.destinatario.apellidos = "Alcaraz";
                objNuevaOrden.destinatario.nombres = "Romina";
                AndreaniServiceTelefono itemTelefonoRemitente = new AndreaniServiceTelefono();
                itemTelefonoRemitente.tipo = 2;
                itemTelefonoRemitente.numero = "011154515412";
                //                Telefono: Fax = 1 || Celular = 2 || Fijo = 3
                objNuevaOrden.remitente.telefonos.Add(itemTelefonoRemitente);
                objNuevaOrden.destinatario.tipoYNumeroDeDocumento = "DNI 32523412";
                objNuevaOrden.destinatario.eMail = "produccion@modoc.com.ar";
                objNuevaOrden.sucursalDeImposicion.id = "1"; // se obtiene de la lista
                objNuevaOrden.bultosParaEnviar.Add(new AndreaniServiceBultosCrearOrden
                {
                    altoCm = 30,
                    largoCm = 1,
                    anchoCm = 20,
                    kilos = Convert.ToInt32(tblDatosDespachos.Rows.Count.ToString()), // cantidad de legajos,
                    valorDeclaradoConImpuestos = 4// RowDespachante["total_legajos"] falta total de legajos (kadir)
                }); ;
                objNuevaOrden.contrato = "400006709";
                objNuevaOrden.referencias.Add(new AndreaniServiceMetaContenido { meta = "id_despacho", contenido = ListaDespachos });

                string jsonMessage = JsonConvert.SerializeObject(objNuevaOrden);
                string location = "v1/ordenesDeEnvio/";
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);
                AndreaniServiceCrearNuevaOrdenResponse response = new AndreaniServiceCrearNuevaOrdenResponse();
                AndreaniServiceResponseRestAPI responseRestApi = new AndreaniServiceResponseRestAPI();
                responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location, jsonMessage);
                response = JsonConvert.DeserializeObject<AndreaniServiceCrearNuevaOrdenResponse>(responseRestApi.JsonResponse);
                Boolean OrdenInsertada = InsertarOrdenConSucursalEnBase(objNuevaOrden, response, orden.nro_guia);
                if (!OrdenInsertada)
                    throw new Exception("Fallo la insercion de datos");
                response.originalJsonResponse = responseRestApi.JsonResponse;

                return Ok(response);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(new AndreaniServiceCrearNuevaOrdenResponse() { error = ex.Message });
            }
        }


        private Boolean ActualizarDatosEnvioEnBase(string nro_tracking, AndreaniServiceObtenerUnEnvioResponse envio)
        {
            try
            {
                // Consulto nro de guia
                String StrSql = "select i139614 from doc_i139081 where i150682=  '" + nro_tracking + "'";
                DataTable tblDatosGuia = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, StrSql).Tables[0];
                string nro_guia = tblDatosGuia.Rows[0][0].ToString();

                string EliminarEnvioAntiguo = "delete from doc_i149105 where I150682 = '" + envio.numeroDeTracking + "'";
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, EliminarEnvioAntiguo);

                insert insert = new insert();
                insert.indexs = new List<Indexs>();
                insert.DocTypeId = 149105;

                insert.indexs.Add(new Indexs { id = 150660, value = envio.contrato });
                insert.indexs.Add(new Indexs { id = 150687, value = envio.estado });
                insert.indexs.Add(new Indexs { id = 139633, value = envio.fechaCreacion.ToLongDateString() });
                insert.indexs.Add(new Indexs { id = 150683, value = envio.numeroDePermisionaria });
                insert.indexs.Add(new Indexs { id = 150682, value = envio.numeroDeTracking });
                insert.indexs.Add(new Indexs { id = 139614, value = nro_guia });
                insert.indexs.Add(new Indexs { id = 150688, value = envio.referencias[0].ToString() });
                insert.indexs.Add(new Indexs { id = 150684, value = envio.sucursalDeDistribucion.id });

                insert.indexs.Add(new Indexs { id = 150708, value = envio.destino.postal.localidad });
                insert.indexs.Add(new Indexs { id = 150709, value = envio.destino.postal.region });
                insert.indexs.Add(new Indexs { id = 150711, value = envio.destino.postal.pais });
                insert.indexs.Add(new Indexs { id = 150710, value = envio.destino.postal.direccion });
                insert.indexs.Add(new Indexs { id = 150712, value = envio.destino.postal.codigoPostal.ToString() });

                insert.indexs.Add(new Indexs { id = 150717, value = envio.remitente.nombreYApellido }); // xxxxxxxxxx
                insert.indexs.Add(new Indexs { id = 150718, value = envio.remitente.tipoYNumeroDeDocumento });
                insert.indexs.Add(new Indexs { id = 150719, value = envio.remitente.eMail });

                insert.indexs.Add(new Indexs { id = 150714, value = envio.destinatario.nombreYApellido }); // xxxxxxx
                insert.indexs.Add(new Indexs { id = 150716, value = envio.destinatario.tipoYNumeroDeDocumento });
                insert.indexs.Add(new Indexs { id = 150665, value = envio.destinatario.eMail });
                return InsertarDatos(insert);

            }
            catch (Exception)
            {
                return false;
            }
        }

        private Boolean ActualizarDatosOrdenEnBase(string nro_tracking, AndreaniServiceObtenerOrdenCreadaResponse orden)
        {
            string queryUpdate = "";
            queryUpdate += "update doc_i149092 set ";
            queryUpdate += " I26188='" + orden.respuesta.estado + "'"; // xxxxxxxxxxxxxxxx
            queryUpdate += " where I150682='" + nro_tracking + "'";
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryUpdate);
            return true;
        }

        [Route("api/andreaniServices/ObtenerOrdenCreada")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult ObtenerOrdenCreada(AndreaniServiceObtenerDatosEnvio request)
        {
            try
            {
                var user = GetUser(request.userId);
                if (user == null)
                    throw new Exception("Usuario Invalido");
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);
                string location = "v1/ordenesDeEnvio/" + request.nro_tracking;
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);
                AndreaniServiceObtenerOrdenCreadaResponse response = new AndreaniServiceObtenerOrdenCreadaResponse();
                response = JsonConvert.DeserializeObject<AndreaniServiceObtenerOrdenCreadaResponse>(responseRestApi.JsonResponse);
                ActualizarDatosOrdenEnBase(request.nro_tracking, response);
                response.mensaje = "Se actualizo la informacion de la orden de envio.";
                response.originalJsonResponse = responseRestApi.JsonResponse;
                ActualizarNotificacionAndreani(request.nro_tracking, "orden de envio", "Actualizado...");
                return Ok(response);
            }
            catch (Exception ex)
            {
                ActualizarNotificacionAndreani(request.nro_tracking, "orden de envio", ex.Message);
                AndreaniServiceObtenerOrdenCreadaResponse error = new AndreaniServiceObtenerOrdenCreadaResponse();
                error.error = "Fallo la obtencion de la orden creada";
                ZClass.raiseerror(ex);
                return Ok(error);
            }
        }

        [Route("api/andreaniServices/BuscarUnEnvio")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult BuscarEnvio(AndreaniServiceBuscarUnEnvioRequest DatosBusqueda)
        {
            try
            {
                string location = "v1/ordenesDeEnvio"
                                + "?codigoCliente=" + DatosBusqueda.codigoCliente
                                + "&idDeProducto=" + DatosBusqueda.idDeProducto
                                + "&numeroDeDocumento=" + DatosBusqueda.numeroDeDocumentoDestinatario
                                + "&fechaCreacionDesde=" + DatosBusqueda.fechaCreacionDesde.ToString("yyyy-MM-ddTHH:mm:ss")
                                + "&fechaCreacionHasta=" + DatosBusqueda.fechaCreacionHasta.ToString("yyyy-MM-ddTHH:mm:ss");

                string token = "xcasdfkhj3kjfkdsf";
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("token", token);
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);
                AndreaniServiceBuscarUnEnvioResponse response = new AndreaniServiceBuscarUnEnvioResponse();
                response = JsonConvert.DeserializeObject<AndreaniServiceBuscarUnEnvioResponse>(responseRestApi.JsonResponse);
                response.mensaje = "Se busco la orden.";
                response.originalJsonResponse = responseRestApi.JsonResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Ok(new AndreaniServiceBuscarUnEnvioResponse() { error = ex.Message });
            }
        }
        [Route("api/andreaniServices/ObtenerEnvio")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult ObtenerEnvio(AndreaniServiceObtenerDatosEnvio request)
        {
            try
            {
                string nro_tracking_original = "";
                var user = GetUser(request.userId);
                if (user == null)
                    throw new Exception("Usuario Invalido");
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);
                if (!String.IsNullOrEmpty(Andreani_Nro_tracking_demo))
                {
                    nro_tracking_original = request.nro_tracking;
                    request.nro_tracking = Andreani_Nro_tracking_demo;
                }
                string location = "v1/envios/" + request.nro_tracking;
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);
                AndreaniServiceObtenerUnEnvioResponse response = new AndreaniServiceObtenerUnEnvioResponse();
                response = JsonConvert.DeserializeObject<AndreaniServiceObtenerUnEnvioResponse>(responseRestApi.JsonResponse);
                if (!String.IsNullOrEmpty(Andreani_Nro_tracking_demo))
                {
                    request.nro_tracking = nro_tracking_original;
                }
                ActualizarDatosEnvioEnBase(request.nro_tracking, response);
                response.mensaje = "Se actualizo el estado del envio.";
                response.originalJsonResponse = responseRestApi.JsonResponse;
                ActualizarNotificacionAndreani(request.nro_tracking, "envio", "Actualizado...");
                return Ok(response);
            }
            catch (Exception ex)
            {
                ActualizarNotificacionAndreani(request.nro_tracking, "envio", ex.Message);
                AndreaniServiceObtenerOrdenCreadaResponse error = new AndreaniServiceObtenerOrdenCreadaResponse();
                error.error = "Fallo la obtencion de envio";
                ZClass.raiseerror(ex);
                return Ok(error);
            }


        }
        [Route("api/andreaniServices/ObtenerTrackingEnvio")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult ObtenerTrackingEnvio(AndreaniServiceObtenerDatosEnvio request)
        {
            try
            {
                var user = GetUser(request.userId);
                string nro_tracking_original = "";
                if (user == null)
                    throw new Exception("Usuario Invalido");
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);

                if (!String.IsNullOrEmpty(Andreani_Nro_tracking_demo))
                {
                    nro_tracking_original = request.nro_tracking;
                    request.nro_tracking = Andreani_Nro_tracking_demo;
                }
                string location = "v1/envios/" + request.nro_tracking + "/trazas";
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);
                AndreaniServiceObtenerTrazaEnvioResponse response = new AndreaniServiceObtenerTrazaEnvioResponse();
                response = JsonConvert.DeserializeObject<AndreaniServiceObtenerTrazaEnvioResponse>(responseRestApi.JsonResponse);
                if (!String.IsNullOrEmpty(Andreani_Nro_tracking_demo))
                {
                    request.nro_tracking = nro_tracking_original;
                }
                string EliminarTrackingAntiguo = "delete from DOC_I149093 where i150682 = '" + request.nro_tracking + "'";
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, EliminarTrackingAntiguo);

                response.eventos.ForEach(n => InsertarTrackingEnBase(n, request.nro_tracking,request.nro_guia));
                response.mensaje = "Se obtuvo el tracking de envio.";
                ActualizarNotificacionAndreani(request.nro_tracking, "tracking", "Actualizado...");
                response.originalJsonResponse = responseRestApi.JsonResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                ActualizarNotificacionAndreani(request.nro_tracking, "tracking", ex.Message);
                AndreaniServiceObtenerOrdenCreadaResponse error = new AndreaniServiceObtenerOrdenCreadaResponse();
                error.error = "Fallo la obtencion del tracking";
                ZClass.raiseerror(ex);
                return Ok(error);
            }

        }

        #endregion

        #region "Listados"
        [Route("api/andreaniServices/ListarProvincias")]
        [HttpPost, HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult ListarProvincias()
        {
            try
            {
                string location = "v1/regiones";
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("token", token);
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);
                AndreaniServiceListarProvinciasResponse response = new AndreaniServiceListarProvinciasResponse();
                response.provincias = JsonConvert.DeserializeObject<List<AndreaniServiceMetaContenido>>(responseRestApi.JsonResponse);
                response.mensaje = "Se obtuvieron las provincias";
                response.originalJsonResponse = responseRestApi.JsonResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new AndreaniServiceListarProvinciasResponse() { error = ex.Message });
            }
        }
        [Route("api/andreaniServices/ObtenerLinking")]
        [HttpPost, HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult ObtenerLinking(AndreaniServiceObtenerDatosLinking request)
        {
            var nro_guia = request.nro_guia;
            String StrSql = "select * from doc_i149094 as Tracking inner join doc_i149092 as Envios on Envios.I139614 = " + nro_guia + " where Envios.I150682 = Tracking.I150682";
            DataTable tblDatosDespachante = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, StrSql).Tables[0]; // 
            List<AndreaniServiceMetaContenido> respuesta = new List<AndreaniServiceMetaContenido>();
            foreach (DataRow dataRow in tblDatosDespachante.Rows)
            {
                AndreaniServiceMetaContenido link = new AndreaniServiceMetaContenido();
                link.meta = dataRow["i150674"].ToString();
                link.contenido = dataRow["i150675"].ToString();
                respuesta.Add(link);
            }
            return Ok(respuesta);
        }


        [Route("api/andreaniServices/ObtenerListaSucursales")]
        [HttpPost, HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult ObtenerListaSucursales(AndreaniServiceListarSucursales request)
        {
            try
            {
                var user = GetUser(request.userId);
                if (user == null)
                    throw new Exception("Usuario Invalido");
                String StrSql = "select i150684 as ID, i150705  as 'Provincia',i150722 as 'Localidad', i26216 as 'Direccion',i26285 as 'CP'FROM DOC_I149097 AS Sucursales";

                DataTable tblDatosDespachante = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, StrSql).Tables[0]; // 
                List<AndreaniServiceSucursalDistribucion> response = new List<AndreaniServiceSucursalDistribucion>();
                foreach (DataRow dataRow in tblDatosDespachante.Rows)
                {
                    AndreaniServiceSucursalDistribucion item = new AndreaniServiceSucursalDistribucion();
                    item.direccion.codigoPostal = Convert.ToInt32(dataRow["CP"].ToString());
                    item.direccion.region = dataRow["Provincia"].ToString();
                    item.id = dataRow["ID"].ToString();
                    item.direccion.componentesDeDireccion.Add(new AndreaniServiceMetaContenido { meta = "direccion", contenido = dataRow["Direccion"].ToString() });
                    item.direccion.localidad = dataRow["Localidad"].ToString();
                    item.direccion.codigoPostal = Convert.ToInt32(dataRow["CP"].ToString());
                    response.Add(item);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new AndreaniServiceListarSucursalesResponse() { error = ex.Message });
            }
        }


        [Route("api/andreaniServices/ListarSucursales")]
        [HttpPost, HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult ListarSucursales(AndreaniServiceListarSucursales request)
        {
            try
            {
                var user = GetUser(request.userId);
                //if (user == null)
                //    throw new Exception("Usuario Invalido");
                ZTrace.WriteLineIf(ZTrace.IsInfo,"Se llamo al servicio de andreani para la obtener las sucursales");
                string token = GetToken(new AndreaniServiceLoginRequest());
                Dictionary<string, string> Header = new Dictionary<string, string>();
                Header.Add("x-authorization-token", token);
                string location = "v1/sucursales";
                AndreaniServiceListarSucursalesResponse response = new AndreaniServiceListarSucursalesResponse();
                AndreaniServiceResponseRestAPI responseRestApi = SendMessageRestAPI(Andreani_URL, Header, location);

                response.sucursales = (JsonConvert.DeserializeObject<List<AndreaniServiceSucursalDistribucion>>(responseRestApi.JsonResponse))
                    .AsEnumerable()
                    .ToList();
                string EliminarSucursales = "delete from DOC_I149097";
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, EliminarSucursales);
                response.sucursales.ForEach(n => InsertarSucursalesEnBase(n));
                response.mensaje = "Se obtuvieron las sucursales de andreani";
                response.originalJsonResponse = responseRestApi.JsonResponse;
                ZTrace.WriteLineIf(ZTrace.IsInfo,"Se obtuvieron las sucursales");
                return Ok(response);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(new AndreaniServiceListarSucursalesResponse() { error = ex.Message });
            }
        }
        #endregion

        #region "Llamada a Andreani REST API"
        private AndreaniServiceResponseRestAPI SendMessageRestAPI(string url, Dictionary<string, string> headers, string location, string JsonMessage = "", Boolean SegundoIntento = false)
        {
            //Andreani_URL = "https://api.andreani.com/";
            // Andreani_URL = https://api.qa.andreani.com/;
            var baseAddress = Andreani_URL + location;
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Se llamo al servicio de andreani para la url " + baseAddress + " con mensaje json " + JsonMessage);
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            AndreaniServiceResponseRestAPI response = new AndreaniServiceResponseRestAPI();
            headers.AsEnumerable().ToList().ForEach(n => { 
                http.Headers.Add(n.Key, n.Value);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Header Key: " + n.Key + " value: " + n.Value);

            }) ;
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";
            if (JsonMessage != "")
            {
                http.Method = "POST";
                string parsedContent = JsonMessage;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request Message: " + JsonMessage);
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);
                Stream BCHistorytream = http.GetRequestStream();
                BCHistorytream.Write(bytes, 0, bytes.Length);
                BCHistorytream.Close();
            }
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request Address: " + http.Address.ToString());
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request Method: " + http.Method);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request Method: " + http.Accept);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request Method: " + http.ContentType);

            WebResponse response2 = null;
            Stream stream = null;

            try
            {
                response2 = http.GetResponse();
             
                 stream = response2.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                if (content == "")
                    throw new Exception("respuesta vacia.");
                response.JsonResponse = content;
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta mensaje andreani: " + response.JsonResponse ) ;
                response.header = response2.Headers;
                response.withError = false;
            }
            catch (WebException ex)
            {
                ZClass.raiseerror(ex);

                try
                {
                    stream = ex.Response.GetResponseStream();
                    var sr = new StreamReader(stream);
                    var content = sr.ReadToEnd();
                    if (content == "")
                        throw new Exception("respuesta vacia.");
                    response.JsonResponse = content;
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error mensaje andreani: " +  response.JsonResponse);
                }
                catch (Exception ex1)
                {
                    ZClass.raiseerror(ex1);
                }
                              
                    if (!SegundoIntento)
                    {
                    //if (tokenVencido(responseContent))
                    //{
                    Thread.Sleep(3000);
                    string token = GetToken(new AndreaniServiceLoginRequest(), true);
                            headers.Clear();
                            headers.Add("x-authorization-token", token);
                            return SendMessageRestAPI(url, headers, location, JsonMessage, true);
                        //}
                    }
               
            }
            return response;
        }

        private Boolean tokenVencido(string Mensaje)
        {
            string[] separarMensaje = Mensaje.Split('\"');
            int contador = 0;
            foreach (String Linea in separarMensaje)
            {
                if (Linea.ToLower() == "errorcode")
                    if (separarMensaje[contador + 2] == "1001" || separarMensaje[contador + 2] == "1002")
                        return true;
                contador++;
            }
            return false;
        }
        private string BuscarDescripcionError(string Mensaje,string exmessage)
        {
            string[] separarMensaje = Mensaje.Split('\"');
            int contador = 0;
            foreach (String Linea in separarMensaje)
            {
                if (Linea == "reasonPhrase")
                    return separarMensaje[contador + 2].Split(':')[1];
                contador++;
            }
            return "Se ha producido un error (" + exmessage + "." + Mensaje + ")";           
        }

        #endregion

        #region "base de datos"
        //private Boolean ActualizarDatos()
        //{

        //}
        private Boolean InsertarDatos(insert insert)
        {
            List<IIndex> indexs = new List<IIndex>();
            SResult sResult = new SResult();
            InsertResult result = InsertResult.NoInsertado;
            INewResult newresult = new SResult().GetNewNewResult(insert.DocTypeId);

            foreach (var InsertIndex in insert.indexs)
            {
                foreach (var NewResultIndex in newresult.Indexs)
                {
                    if (NewResultIndex.ID == InsertIndex.id)
                    {
                        if (InsertIndex.value == null)
                            InsertIndex.value = string.Empty;
                        NewResultIndex.Data = InsertIndex.value;
                        NewResultIndex.DataTemp = InsertIndex.value;
                        indexs.Add(NewResultIndex);
                        break;
                    }
                }
            }
            result = sResult.Insert(ref newresult, false, false, false, false, true, false, false, false, false);
            return (result == InsertResult.Insertado);
        }

        private Boolean InsertarNuevoItemNotificacionesAndreani(string nro_tracking)
        {

            string EliminarEnvioAntiguo = "delete from doc_i149106 where I150682 = '" + nro_tracking + "'";
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, EliminarEnvioAntiguo);
            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149106;
            insert.indexs.Add(new Indexs { id = 150682, value = nro_tracking });
            return InsertarDatos(insert);
        }
        private Boolean ActualizarNotificacionAndreani(string nro_tracking, string entidad, string notificacion)
        {
            string queryUpdate = "";
            string atributo_id = "";
            switch (entidad.ToLower())
            {
                case "orden de envio":
                    {
                        atributo_id = "I150726";
                        break;
                    }
                case "envio":
                    {
                        atributo_id = "I150727";
                        break;
                    }
                case "tracking":
                    {
                        atributo_id = "I150728";
                        break;
                    }
            }
            notificacion = DateTime.Now.ToString("dd/MM/yy hh:mm") + ". " + notificacion.Replace(@"'", @"''");
            if (notificacion.Length > 4000)
                notificacion = notificacion.Substring(0, 4000);
            queryUpdate += "update doc_i149106 set ";
            queryUpdate += atributo_id + "='" + notificacion + "'"; // xxxxxxxxxxxxxxxx
            queryUpdate += " where I150682='" + nro_tracking + "'";
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryUpdate);
            return true;
        }
        private Boolean InsertarLinkingEnBase(AndreaniServiceMetaContenido Linking, string nro_tracking)
        {
            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149094;
            insert.indexs.Add(new Indexs { id = 150682, value = nro_tracking });
            insert.indexs.Add(new Indexs { id = 150674, value = Linking.meta });
            insert.indexs.Add(new Indexs { id = 150675, value = Linking.contenido });
            return InsertarDatos(insert);
        }
        private Boolean InsertarSucursalesEnBase(AndreaniServiceSucursalDistribucion sucursal)
        {

            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149097;
            insert.indexs.Add(new Indexs { id = 150684, value = sucursal.id });
            insert.indexs.Add(new Indexs { id = 150686, value = sucursal.nomenclatura });
            insert.indexs.Add(new Indexs { id = 26290, value = sucursal.descripcion });
            insert.indexs.Add(new Indexs { id = 150722, value = sucursal.direccion.localidad });
            insert.indexs.Add(new Indexs { id = 150705, value = sucursal.direccion.region });
            insert.indexs.Add(new Indexs { id = 150721, value = sucursal.direccion.pais });
            insert.indexs.Add(new Indexs { id = 26285, value = sucursal.direccion.codigoPostal.ToString() });
            insert.indexs.Add(new Indexs { id = 26216, value = String.Join(" ", sucursal.direccion.componentesDeDireccion.Select(n => n.contenido).ToArray()) });
            return InsertarDatos(insert);
        }



        private Boolean InsertarTrackingEnBase(AndreaniServiceEventos Traza, string nro_tracking,string nro_guia)
        {

            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149093;
            insert.indexs.Add(new Indexs { id = 150682, value = nro_tracking });
            insert.indexs.Add(new Indexs { id = 150693, value = Traza.Motivo });
            insert.indexs.Add(new Indexs { id = 150695, value = Traza.Submotivo });
            insert.indexs.Add(new Indexs { id = 150694, value = Traza.MotivoId.ToString() });
            insert.indexs.Add(new Indexs { id = 150696, value = Traza.SubmotivoId.ToString() });
            insert.indexs.Add(new Indexs { id = 150697, value = Traza.Ciclo });
            insert.indexs.Add(new Indexs { id = 26290, value = Traza.Sucursal });
            insert.indexs.Add(new Indexs { id = 150684, value = Traza.SucursalId.ToString() });
            insert.indexs.Add(new Indexs { id = 33674, value = Traza.Fecha.ToString() });
            insert.indexs.Add(new Indexs { id = 139614, value = nro_guia});
            insert.indexs.Add(new Indexs { id = 150704, value = Traza.Estado });
            return InsertarDatos(insert);

        }
        private Boolean InsertarOrdenSinSucursalEnBase(AndreaniServiceCrearNuevaOrdenConDestinoRequest objNuevaOrden, AndreaniServiceCrearNuevaOrdenResponse OrdenRespuesta, string nro_guia)
        {

            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149092;
            insert.indexs.Add(new Indexs { id = 139614, value = nro_guia });
            insert.indexs.Add(new Indexs { id = 150688, value = objNuevaOrden.referencias[0].contenido });
            insert.indexs.Add(new Indexs { id = 150660, value = objNuevaOrden.contrato });
            insert.indexs.Add(new Indexs { id = 150702, value = objNuevaOrden.origen.postal.codigoPostal.ToString() });
            insert.indexs.Add(new Indexs { id = 150701, value = objNuevaOrden.origen.postal.componentesDeDireccion[0].contenido + " " + objNuevaOrden.origen.postal.componentesDeDireccion[1].contenido });
            insert.indexs.Add(new Indexs { id = 150700, value = objNuevaOrden.origen.postal.pais });
            insert.indexs.Add(new Indexs { id = 150699, value = objNuevaOrden.origen.postal.region });
            insert.indexs.Add(new Indexs { id = 150698, value = objNuevaOrden.origen.postal.localidad });
            insert.indexs.Add(new Indexs { id = 150717, value = objNuevaOrden.remitente.nombres });
            insert.indexs.Add(new Indexs { id = 150718, value = objNuevaOrden.remitente.tipoYNumeroDeDocumento });
            insert.indexs.Add(new Indexs { id = 150719, value = objNuevaOrden.remitente.eMail });
            insert.indexs.Add(new Indexs { id = 150708, value = objNuevaOrden.destino.postal.localidad });
            insert.indexs.Add(new Indexs { id = 150709, value = objNuevaOrden.destino.postal.region });
            insert.indexs.Add(new Indexs { id = 150711, value = objNuevaOrden.destino.postal.pais });
            insert.indexs.Add(new Indexs { id = 150710, value = objNuevaOrden.destino.postal.componentesDeDireccion[0].contenido + " " + objNuevaOrden.destino.postal.componentesDeDireccion[1].contenido });
            insert.indexs.Add(new Indexs { id = 150712, value = objNuevaOrden.destino.postal.codigoPostal.ToString() });
            insert.indexs.Add(new Indexs { id = 150714, value = objNuevaOrden.destinatario.nombres });
            insert.indexs.Add(new Indexs { id = 150715, value = objNuevaOrden.destinatario.apellidos });
            insert.indexs.Add(new Indexs { id = 150716, value = objNuevaOrden.destinatario.tipoYNumeroDeDocumento });
            insert.indexs.Add(new Indexs { id = 150665, value = objNuevaOrden.destinatario.eMail });

            // datos de respuesta

            insert.indexs.Add(new Indexs { id = 150682, value = OrdenRespuesta.numeroDeTracking });
            insert.indexs.Add(new Indexs { id = 26188, value = OrdenRespuesta.estado });
            insert.indexs.Add(new Indexs { id = 139633, value = OrdenRespuesta.fechaCreacion.ToLongDateString() });
            insert.indexs.Add(new Indexs { id = 150683, value = OrdenRespuesta.numeroDePermisionaria });
            insert.indexs.Add(new Indexs { id = 150684, value = OrdenRespuesta.sucursalDeDistribucion.id });
            OrdenRespuesta.linking.ForEach(n => InsertarLinkingEnBase(n, OrdenRespuesta.numeroDeTracking));
            InsertarNuevoItemNotificacionesAndreani(OrdenRespuesta.numeroDeTracking);
            return InsertarDatos(insert);
        }
        private Boolean InsertarOrdenConSucursalEnBase(AndreaniServiceCrearNuevaOrdenConSucursalRequest objNuevaOrden, AndreaniServiceCrearNuevaOrdenResponse OrdenRespuesta, string nro_guia)
        {

            insert insert = new insert();
            insert.indexs = new List<Indexs>();
            insert.DocTypeId = 149092;
            insert.indexs.Add(new Indexs { id = 139614, value = nro_guia });
            insert.indexs.Add(new Indexs { id = 150688, value = objNuevaOrden.referencias[0].contenido });
            insert.indexs.Add(new Indexs { id = 150660, value = objNuevaOrden.contrato });
            insert.indexs.Add(new Indexs { id = 150702, value = objNuevaOrden.origen.postal.codigoPostal.ToString() });
            insert.indexs.Add(new Indexs { id = 150701, value = objNuevaOrden.origen.postal.componentesDeDireccion[0].contenido + " " + objNuevaOrden.origen.postal.componentesDeDireccion[1].contenido });
            insert.indexs.Add(new Indexs { id = 150700, value = objNuevaOrden.origen.postal.pais });
            insert.indexs.Add(new Indexs { id = 150699, value = objNuevaOrden.origen.postal.region });
            insert.indexs.Add(new Indexs { id = 150698, value = objNuevaOrden.origen.postal.localidad });
            insert.indexs.Add(new Indexs { id = 150717, value = objNuevaOrden.remitente.nombres });
            insert.indexs.Add(new Indexs { id = 150718, value = objNuevaOrden.remitente.tipoYNumeroDeDocumento });
            insert.indexs.Add(new Indexs { id = 150719, value = objNuevaOrden.remitente.eMail });
            insert.indexs.Add(new Indexs { id = 150713, value = objNuevaOrden.destino.sucursal.id });
            insert.indexs.Add(new Indexs { id = 150714, value = objNuevaOrden.destinatario.nombres });
            insert.indexs.Add(new Indexs { id = 150715, value = objNuevaOrden.destinatario.apellidos });
            insert.indexs.Add(new Indexs { id = 150716, value = objNuevaOrden.destinatario.tipoYNumeroDeDocumento });
            insert.indexs.Add(new Indexs { id = 150665, value = objNuevaOrden.destinatario.eMail });

            // datos de respuesta

            insert.indexs.Add(new Indexs { id = 150682, value = OrdenRespuesta.numeroDeTracking });
            insert.indexs.Add(new Indexs { id = 26188, value = OrdenRespuesta.estado });
            insert.indexs.Add(new Indexs { id = 150687, value = OrdenRespuesta.estado });
            insert.indexs.Add(new Indexs { id = 139633, value = OrdenRespuesta.fechaCreacion.ToLongDateString() });
            insert.indexs.Add(new Indexs { id = 150683, value = OrdenRespuesta.numeroDePermisionaria });
            insert.indexs.Add(new Indexs { id = 150684, value = OrdenRespuesta.sucursalDeDistribucion.id });
            OrdenRespuesta.linking.ForEach(n => InsertarLinkingEnBase(n, OrdenRespuesta.numeroDeTracking));
            return InsertarDatos(insert);
        }
        #endregion

        #region "Request"
        public class NuevaOrdenRequest
        {
            public string nro_guia;
            public string sucursal_id;
            public Int64 userId;
        }


        public class AndreaniServiceCrearNuevaOrdenConDestinoRequest
        {
            public AndreaniServicePostalRamaSuperior destino = new AndreaniServicePostalRamaSuperior();
            public AndreaniServicePostalRamaSuperior origen = new AndreaniServicePostalRamaSuperior();
            public AndreaniServiceDatosPersonaTipoA remitente = new AndreaniServiceDatosPersonaTipoA();
            public AndreaniServiceDatosPersonaTipoA destinatario = new AndreaniServiceDatosPersonaTipoA();
            public string contrato;
            public AndreaniServiceSucursal sucursalDeImposicion = new AndreaniServiceSucursal();
            public List<AndreaniServiceBultosCrearOrden> bultosParaEnviar = new List<AndreaniServiceBultosCrearOrden>();
            public List<AndreaniServiceMetaContenido> referencias = new List<AndreaniServiceMetaContenido>();
        }
        public class AndreaniServiceCrearNuevaOrdenConSucursalRequest
        {
            public AndreaniServiceDestinoSucursal destino = new AndreaniServiceDestinoSucursal();
            public AndreaniServicePostalRamaSuperior origen = new AndreaniServicePostalRamaSuperior();
            public AndreaniServiceDatosPersonaTipoA remitente = new AndreaniServiceDatosPersonaTipoA();
            public AndreaniServiceDatosPersonaTipoA destinatario = new AndreaniServiceDatosPersonaTipoA();
            public string contrato;
            public AndreaniServiceSucursal sucursalDeImposicion = new AndreaniServiceSucursal();
            public List<AndreaniServiceBultosCrearOrden> bultosParaEnviar = new List<AndreaniServiceBultosCrearOrden>();
            public List<AndreaniServiceMetaContenido> referencias = new List<AndreaniServiceMetaContenido>();
        }

        public class AndreaniServiceListarSucursales
        {
            public long userId;
        }
        public class AndreaniServiceObtenerDatosEnvio
        {
            public string nro_tracking;
            public long userId;
            public string nro_guia;
        }
        public class AndreaniServiceObtenerDatosLinking
        {
            public string nro_guia;
            public long userId;
        }
        public class AndreaniServiceBuscarUnEnvioRequest
        {
            public string codigoCliente;
            public string idDeProducto;
            public string numeroDeDocumentoDestinatario;
            public DateTime fechaCreacionDesde;
            public DateTime fechaCreacionHasta;

        }
        public class AndreaniServiceObtenerTrazaEnvioRequest
        {
            public string numeroAndreani;
        }

        public class AndreaniServiceLoginRequest
        {
            public string userID;
            public string Password;
        }


        #endregion

        #region "Response"

        public class AndreaniServiceLoginResponse : AndreaniServiceResponseBase
        {
            public string token;
        }
        public class AndreaniServiceObtenerOrdenResponse : AndreaniServiceCrearNuevaOrdenResponse { }
        public class AndreaniServiceResponseRestAPI
        {
            public string JsonResponse;
            public WebHeaderCollection header;
            public Boolean withError;
            public string ValidationError;

        }
        public class AndreaniServiceObtenerTrazaEnvioResponse : AndreaniServiceResponseBase
        {
            public List<AndreaniServiceEventos> eventos;

        }
        public class AndreaniServiceCrearNuevaOrdenResponse : AndreaniServiceResponseBase
        {
            public string numeroDeTracking;
            public string numeroDePermisionaria;
            public AndreaniServiceSucursalConDescripcionYNomenclatura sucursalDeDistribucion;
            public DateTime fechaCreacion;
            public string estado;
            public List<AndreaniServiceMetaContenido> linking;
            public string NotificacionOrdenEnvio;
            public string NotificacionEnvio;
            public string NotificacionTracking;

        }


        public class AndreaniServiceListarSucursalesResponse : AndreaniServiceResponseBase
        {
            public List<AndreaniServiceSucursalDistribucion> sucursales = new List<AndreaniServiceSucursalDistribucion>();

        }
        public class AndreaniServiceListarProvinciasResponse : AndreaniServiceResponseBase
        {
            public List<AndreaniServiceMetaContenido> provincias = new List<AndreaniServiceMetaContenido>();

        }
        public class AndreaniServiceObtenerUnEnvioResponse : AndreaniServiceResponseBase
        {
            public string numeroDeTracking;
            public string contrato;
            public string estado;
            public string numeroDePermisionaria;
            public AndreaniServiceSucursalConDescripcionYNomenclatura sucursalDeDistribucion;
            public DateTime fechaCreacion;
            public AndreaniServicePostalRamaSuperiorRespuestaEnvio destino;
            public AndreaniServiceDatosPersonaTipoB remitente;
            public AndreaniServiceDatosPersonaTipoB destinatario;
            public List<AndreaniServiceBultosB> bultos;
            public List<String> referencias;

        }
        public class AndreaniServiceBuscarUnEnvioResponse : AndreaniServiceObtenerUnEnvioResponse { }
        public class AndreaniServiceResponseBase
        {
            public string error;
            public string mensaje;
            public string originalJsonResponse;
        }
        public class AndreaniServiceObtenerOrdenCreadaResponse : AndreaniServiceResponseBase
        {
            public AndreaniServiceOrdenRespuesta respuesta;
            public AndreaniServiceOrdenIngreso ingreso;
            // faltan datos de ingreso
            /*
             {"respuesta":{"numeroDeTracking":"300000009887120","numeroDePermisionaria":"RNPSP Nº 586","sucursalDeDistribucion":{"nomenclatura":"BAR","descripcion":"BARRACAS","id":"3"},"sucursalDeDevolucion":{"nomenclatura":"PSD","descripcion":"SANTO DOMINGO","id":"1"},"fechaCreacion":"2019-09-16T13:20:04.6116590-03:00","estado":"aceptada","linking":[{"meta":"@tracking","contenido":"http://tempuri.org/api/tracking/300000009887120"},{"meta":"@etiqueta","contenido":"https://api.qa.andreani.com/v1/etiquetas/300000009887120"}]},"ingreso":{"destino":{"postal":{"localidad":"C.A.B.A.","region":"AR-B","pais":"Argentina","codigoPostal":"1151","componentesDeDireccion":[{"meta":"calle","contenido":"San Jose"},{"meta":"numero","contenido":"1540"}],"notasAdicionales":[]}},"remitente":{"nombres":"BRU?OL RODRIGO SEBASTIAN","tipoYNumeroDeDocumento":"CUIT 20289784041","eMail":"daniel@redtime.com.ar","telefonos":[],"apellidos":"-"},"destinatario":{"nombres":"Romina","tipoYNumeroDeDocumento":"DNI 32523412","eMail":"produccion@modoc.com.ar","telefonos":[],"apellidos":"Alcaraz"},"contrato":"400006709","bultosParaEnviar":[{"kilos":2.0,"valorDeclaradoConImpuestos":4.0,"largoCm":1.0,"altoCm":30.0,"anchoCm":20.0,"volumen":0.0}],"franjaHoraria":[],"sucursalDeImposicion":{"telefonos":[],"datosAdicionales":[],"id":"123"},"referencias":[{"meta":"id_despacho","contenido":"19001EC01009063X,19001EC01009340G"}],"origen":{"postal":{"localidad":"C.A.B.A.","region":"AR-C","pais":"Argentina","codigoPostal":"1151","componentesDeDireccion":[{"meta":"calle","contenido":"MORENO"},{"meta":"numero","contenido":"502"}],"notasAdicionales":[]}},"notasAdicionales":[]}}
             */
        }
        #endregion

        #region "Clases"


        public class AndreaniServiceOrdenIngreso
        {
            public AndreaniServiceOrdenIngresoOrigenDestino destino;
            public AndreaniServiceDatosPersonaTipoA remitente;
            public AndreaniServiceDatosPersonaTipoA destinatario;
            public string contrato;
            public List<AndreaniServiceBultosA> bultosParaEnviar;
            public List<AndreaniServiceMetaContenido> franjaHoraria;
            public AndreaniServiceSucursalDeImposicion sucursalDeImposicion;
            public List<AndreaniServiceMetaContenido> referencias;
            public AndreaniServiceOrdenIngresoOrigenDestino origen;
            public List<AndreaniServiceMetaContenido> notasAdicionales;
        }
        public class AndreaniServiceOrdenIngresoOrigenDestino
        {
            public AndreaniServicePostalRamaInferiorConNotasAdicionales postal;

        }

        public class AndreaniServiceOrdenRespuesta
        {
            public string numeroDeTracking;
            public string numeroDePermisionaria;
            public AndreaniServiceSucursalConDescripcionYNomenclatura sucursalDeDistribucion;
            public AndreaniServiceSucursalConDescripcionYNomenclatura sucursalDeDevolucion;
            public DateTime fechaCreacion;
            public string estado;
            public List<AndreaniServiceMetaContenido> linking;
        }



        public class AndreaniObtenerOrdenCreada
        {
            public int nro_orden;
        }
        /// </summary>

        public class AndreaniServiceMetaContenido
        {
            public string meta;//{ get; set; }
            public string contenido;// {get; set; }
        }
        public class AndreaniServiceTelefono
        {
            public int tipo;
            public string numero;
        }

        public class AndreaniServicePostalRamaSuperior
        {
            public AndreaniServicePostalRamaInferior postal = new AndreaniServicePostalRamaInferior();
        }
        public class AndreaniServicePostalRamaSuperiorRespuestaEnvio
        {
            public AndreaniServicePostalRamaInferiorRespuestaEnvio postal = new AndreaniServicePostalRamaInferiorRespuestaEnvio();
        }
        public class AndreaniServicePostalRamaInferiorRespuestaEnvio
        {
            public string localidad;
            public string region;
            public string pais;
            public int codigoPostal;
            public string direccion;
        }


        public class AndreaniServicePostalRamaInferior
        {
            public string localidad;
            public string region;
            public string pais;
            public int codigoPostal;
            public List<AndreaniServiceMetaContenido> componentesDeDireccion = new List<AndreaniServiceMetaContenido>();
        }
        public class AndreaniServicePostalRamaInferiorConNotasAdicionales : AndreaniServicePostalRamaInferior
        {
            public List<AndreaniServiceMetaContenido> notasAdicionales;
        }
        public class AndreaniServiceDestinoSucursal
        {
            public AndreaniServiceDestinoSucursalId sucursal = new AndreaniServiceDestinoSucursalId();
        }
        public class AndreaniServiceDestinoSucursalId
        {
            public string id;
        }
        public class AndreaniServiceBultosB
        {
            public double? kilos=0;
            public double? valorDeclaradoConImpuestos=0;
            public double? IdDeProducto=0;
            public double? volumen=0;
        }
        public class AndreaniServiceBultosA
        {
            public double kilos;
            public double valorDeclaradoConImpuestos;
            public double largoCm;
            public double altoCm;
            public double anchoCm;
            public double volumen;
        }
        public class AndreaniServiceBultosCrearOrden
        {
            public double kilos;
            public double valorDeclaradoConImpuestos;
            public double largoCm;
            public double altoCm;
            public double anchoCm;
        }

        public class AndreaniServiceDatosPersonaTipoA
        {
            public string nombres;
            public string tipoYNumeroDeDocumento;
            public string eMail;
            public string apellidos;
            public List<AndreaniServiceTelefono> telefonos = new List<AndreaniServiceTelefono>();

        }
        public class AndreaniServiceDatosPersonaTipoB
        {
            public string nombreYApellido;
            public string tipoYNumeroDeDocumento;
            public string eMail;
        }

        public class AndreaniServiceGeocordenadas
        {
            public string elevacion;
            public string latitud;
            public string longitud;
        }

        public class AndreaniServiceEventos
        {
            public DateTime Fecha;
            public string Estado;
            public int EstadoId;
            public string Motivo;
            public int MotivoId;
            public string Submotivo;
            public int SubmotivoId;
            public string Sucursal;
            public int SucursalId;
            public string Ciclo;
        }
        public class AndreaniServiceSucursal
        {
            public string id;
        }
        public class AndreaniServiceSucursalConDescripcionYNomenclatura
        {
            public string id;
            public string nomenclatura;
            public string descripcion;
        }
        public class AndreaniServiceSucursalDistribucion
        {
            public string nomenclatura;
            public string descripcion;
            public AndreaniServicePostalRamaInferior direccion = new AndreaniServicePostalRamaInferior();
            public AndreaniServiceGeocordenadas geocoordenadas;
            public List<AndreaniServiceMetaContenido> datosAdicionales;
            public string id;
            public string horarioDeAtencion;
        }
        public class AndreaniServiceSucursalDeImposicion
        {
            public List<AndreaniServiceTelefono> telefonos;
            public List<AndreaniServiceMetaContenido> datosAdicionales;
            public String id;
        }

        #endregion

        #region "test"
        [Route("api/andreaniServices/Test")]
        [HttpPost, HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult Test(string SERVICIO,string param1 )
        {
            try
            {
                return Ok("respuesta");
            }
            catch (Exception ex)
            {
                return Ok("respuesta");
            }
        }

        #endregion

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
    }
}





