﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using InvocacionServWDigDepFiel.wDigDepFiel;
using System.IO;
using InvocacionServWDigDepFiel;
using Newtonsoft.Json;
using ClienteLoginCms_CS;
using System.Xml;
using System.Data;

using Zamba.Core;
using System.Security.Cryptography.X509Certificates;
using Zamba.Framework;
using InvocacionServWConsDepFiel;
using InvocacionServWDigDepFiel.wConsDepFiel;

namespace ZambaAfipDepDigi
{

   
    public class SignPDFController
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
       
        #endregion


       
       

       

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


       
       

        
        public void ReceptAll()
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

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
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
            var querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate, i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE  from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
            DataSet dsDespacho = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, querydespacho);

            DataRow r = dsDespacho.Tables[0].Rows[0];

            RecepcionResponse RR = new RecepcionResponse();


            if (r["codigoError"] == null || r["codigoError"].ToString() != "0")
            {
                solicitudFirmaDigital.nroLegajo = r["nroLegajo"].ToString();
                solicitudFirmaDigital.cuitDeclarante = r["cuitDeclarante"].ToString();
                solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                solicitudFirmaDigital.cuitIE = r["cuitIE"].ToString();
                solicitudFirmaDigital.cuitATA = (r["cuitATA"] != null) ? r["cuitATA"].ToString() : string.Empty;
                solicitudFirmaDigital.codigo = r["codigo"].ToString();


                solicitudFirmaDigital.ticket = (r["ticket"] != null) ? r["ticket"].ToString() : string.Empty;

                solicitudFirmaDigital.sigea = (r["sigea"] != null) ? r["sigea"].ToString() : string.Empty;
                solicitudFirmaDigital.nroReferencia = string.Empty;
                solicitudFirmaDigital.nroGuia = r["nroGuia"].ToString();

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
                    var queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}'", solicitudFirmaDigital, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
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
                    var queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}', I149654 = {3},i139577 = getdate() where i139548 = '{4}' and i139603 = '{5}'", solicitudFirmaDigital, 0, "OK. Procesado", solicitudFirmaDigital.userId, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
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
                    var queryAcepted = string.Format(@"update doc_i139072 set I139623 = getdate(), I139619 = '{0}', I139620 = '{1}', I26513 = '{2}' where i139548 = '{3}' and i139603 = '{4}'", newresultsreciboAvisoRecepAcept, reciboAvisoRecepAcept.codError, reciboAvisoRecepAcept.descError, solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
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






        public void GetLegajosAllService(Int64 userId)
        {
            try
            {
                

                SolicitudFirmaDigital solicitudFirmaDigital = new SolicitudFirmaDigital();
                solicitudFirmaDigital.userId = userId;

                string error = string.Empty;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia proceso de ConsultaDespacho: ");

                ListadoResponse RR = _PndListaEndoResponseAll(solicitudFirmaDigital);

                var js = JsonConvert.SerializeObject(RR);


            }
            catch (Exception e)
            {
                Zamba.Core.ZClass.raiseerror(e);
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
            var querydespacho = string.Format(@" select t.doc_id, i139548 nroLegajo, i139600 cuitDeclarante,i26296  cuitIE,i1139 cuitATA,i139603  codigo,i139618  ticket,i139578 sigea,i139614  nroGuia,i139551  fechaDespacho, i139587 indLugarFisico, I26405 FechaGeneracion,crdate,i139577 fechaAceptacion, i149662 vtoEmbarque, i139620 codigoError,i139588 IE  from doc_i139072 i inner join doc_t139072 t on i.doc_id = t.doc_id  inner join disk_volume v on v.disk_vol_id = t.vol_id   where i139548 = '{0}' and i139603 = '{1}'", solicitudFirmaDigital.nroDespacho, solicitudFirmaDigital.codigo);
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


        private ListadoResponse _PndListaEndoResponseAll(SolicitudFirmaDigital solicitudFirmaDigital)
        {
            //0- Obtener Parametros del Servicio


            //3- Aviso Recepcion

            var PSADCUIT = ZOptBusiness.GetValueOrDefault("PSADCUIT", "30714439304");
            var servicioWConsDepFiel = new ServicioWConsDepFiel();

            ClienteLoginCms.ProgramaPrincipal.DEFAULT_URLWSAAWSDL = ZOptBusiness.GetValueOrDefault("AFIPDIGIURL", "https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_SERVICIO = ZOptBusiness.GetValueOrDefault("AFIPCONSSERVICE", "wConsDepFiel").ToLower();
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFX", @"C:\OpenSSL-Win32\bin\PKMODOC2.pfx");
            ClienteLoginCms.ProgramaPrincipal.DEFAULT_CERTSIGNER_PASSWORD = ZOptBusiness.GetValueOrDefault("AFIPDIGIPFXPASSWORD", @"modoc");


            //obtener datos del despacho
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtenieno despachantes para solicitar");
            var querydespacho = " select i139600 cuitDeclarante from doc_i139074 i where i139600 is not null";
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

           

           

            Int64 conteodespachante = 0;
            foreach (DataRow r in dsDespacho.Tables[0].Rows)
            {
                List<string> TraceAFIP = new List<string>();
                Int64 conteodespachanteTotal = 0;
                Int64 conteodespachanteNuevos = 0;

                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
                TraceAFIP.Add(System.Environment.NewLine);

                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Cantidad de Despachantes: " + dsDespacho.Tables[0].Rows.Count.ToString());
                TraceAFIP.Add(System.Environment.NewLine);

                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
                TraceAFIP.Add(System.Environment.NewLine);

                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/" + dsDespacho.Tables[0].Rows.Count.ToString() + " : " + r["cuitDeclarante"].ToString());
                TraceAFIP.Add(System.Environment.NewLine);

                ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "*************************************************************************************************************************");
                ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

                ZTrace.WriteLineIf(ZTrace.IsInfo, ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Despachante: " + conteodespachante.ToString() + "/" + dsDespacho.Tables[0].Rows.Count.ToString() + " : " + r["cuitDeclarante"].ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, System.Environment.NewLine);

                try
                {
                    conteodespachante++;

                    foreach (string codigo in codigos)
                    {
                        try
                        {
                            TraceAFIP.Add(System.Environment.NewLine);
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------- Codigo: " + codigo);
                            TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------  CODIGO: " + codigo);

                            solicitudFirmaDigital.cuitPSAD = PSADCUIT;
                            solicitudFirmaDigital.cuitDeclarante = r["cuitDeclarante"].ToString();
                            solicitudFirmaDigital.codigo = codigo;

                            string ModocDefaultMonthsAfipEndoService = ZOptBusiness.GetValueOrDefault("ModocDefaultMonthsAfipEndoService", "12");
                            DateTime FechaDesde = DateTime.Now.AddMonths(-int.Parse(ModocDefaultMonthsAfipEndoService));
                            DateTime FechaHasta = DateTime.Now;

                            var dicAutenticacion = ClienteLoginCms.ProgramaPrincipal.Autenticacion();

                            InvocacionServWDigDepFiel.wConsDepFiel.Autenticacion autenticacion = servicioWConsDepFiel.CrearAutenticacion(PSADCUIT, "EXTE", dicAutenticacion["sign"], "PSAD", dicAutenticacion["token"]);

                            PndListaEndoResponse listaEstadoResponse = servicioWConsDepFiel.InvocarServicioPndListaEndoRequest(autenticacion,
                                   solicitudFirmaDigital.cuitDeclarante, solicitudFirmaDigital.codigo, FechaDesde, FechaHasta);

                            if (listaEstadoResponse.Body.PndListaEndoResult.Legajos != null)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Despachos Obtenidos para el Despachante: " + r["cuitDeclarante"].ToString() + " Cantidad: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " CANTIDAD: " + listaEstadoResponse.Body.PndListaEndoResult.Legajos.Count().ToString());
                                TraceAFIP.Add(System.Environment.NewLine);

                                Int64 conteo = 0;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la Existencia de cada Despacho");
                                foreach (Legajo l in listaEstadoResponse.Body.PndListaEndoResult.Legajos)
                                {

                                    try
                                    {
                                        conteodespachanteTotal++;
                                        conteo++;

                                        string selectLegajo = string.Empty;
                                        if (codigo == "004")
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072 where i139548 = '{0}' and i139603 = '{1}' and i139578 = '{2}'", l.NroLegajo, l.Codigo, l.Sigea);
                                        }
                                        else
                                        {
                                            selectLegajo = string.Format(@"select count(1) from  doc_i139072 where i139548 = '{0}' and i139603 = '{1}'", l.NroLegajo, l.Codigo);
                                        }
                                        object LegajoExiste = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, selectLegajo);
                                        if (!(LegajoExiste is DBNull) && (LegajoExiste.ToString().Length > 0 && Int64.Parse(LegajoExiste.ToString()) > 0))
                                        {
                                            //el legajo existe lo actualizamos
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Despacho ya existe, se actualiza: " + l.NroLegajo + " Codigo: " + l.Codigo);
                                            TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo  + " : Ya existe, se actualiza");

                                            string updateLegajo = string.Format(@"update doc_i139072 set I26405 =  CONVERT(datetime,'{0}',120), i139618 = '{1}',i139603 = '{3}',i139600 = '{4}',i149651 = '{5}',i139551 =  CONVERT(datetime,'{6}',120),i139559 = '{7}',i139578 = '{8}' where i139548 = '{2}' and i139603 = '{3}'",
                                                l.FechaEndo.ToString("yyyy-MM-dd HH:mm:ss"), l.Ticket, l.NroLegajo, l.Codigo, l.CuitDeclarante, l.CuitIE, l.FechaOfic.ToString("yyyy-MM-dd HH:mm:ss"), l.ImporteLiq, l.Sigea);
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
                                            TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + " " + conteo.ToString() + ": Despacho: " + l.NroLegajo + " Codigo: " + l.Codigo  + " NUEVO ");

                                            Results_Business rb = new Results_Business();
                                            //el legajo no existe lo generamos en zamba
                                            NewResult nr = rb.GetNewNewResult(139072);
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
                                        TraceAFIP.Add(System.Environment.NewLine);
                                        TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "ERROR al procesar el despacho: " + l.NroLegajo + " con Codigo: " + l.Codigo + " : " + ex.ToString());
                                        TraceAFIP.Add(System.Environment.NewLine);

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
                                    TraceAFIP.Add(System.Environment.NewLine);
                                    TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP OK");
                                    TraceAFIP.Add(System.Environment.NewLine);

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
                                    TraceAFIP.Add(System.Environment.NewLine);
                                    TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR: " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.CodErr + "  " + listaEstadoResponse.Body.PndListaEndoResult.Recibo.DescErr);
                                    TraceAFIP.Add(System.Environment.NewLine);

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
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "----------------------------------------- NO HAY DESPACHOS EN AFIP");
                            }

                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            TraceAFIP.Add(System.Environment.NewLine);
                            TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                            TraceAFIP.Add(System.Environment.NewLine);
                            ZClass.raiseerror(ex);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                    TraceAFIP.Add(System.Environment.NewLine);
                    TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "Recepcion de despacho desde AFIP ERROR. " + ex.ToString());
                    TraceAFIP.Add(System.Environment.NewLine);
                    ZClass.raiseerror(ex);
                }

                TraceAFIP.Add(System.Environment.NewLine);
                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN " + conteodespachante.ToString() + " FIN Consulta DESPACHANTE CUIT: " + r["cuitDeclarante"].ToString());


                TraceAFIP.Add(System.Environment.NewLine);
                TraceAFIP.Add(ZTrace.CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(), 12) + "-----------------------------  FIN PROCESO --------------------------------------------");

                try
                {
                    ISendMailConfig mail = new SendMailConfig();

                    mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    mail.MailType = MailTypes.NetMail;
                    mail.SaveHistory = false;
                    mail.MailTo = "soportemodoc@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                    mail.Subject = "Zamba - AFIP: " + r["cuitDeclarante"].ToString() + " TOTAL: " + conteodespachanteTotal.ToString() + "NUEVOS: " + conteodespachanteNuevos.ToString();
                    mail.Body = string.Join(System.Environment.NewLine, TraceAFIP);
                    mail.IsBodyHtml = true;
                    mail.LinkToZamba = false;

                    MessagesBusiness.SendQuickMail(mail);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }


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