using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;
using Zamba.Framework;
using ZambaWeb.RestApi.Controllers.Common;
using ZambaWeb.RestApi.Models;
using System.Linq;
using ZambaWeb.RestApi.AuthorizationRequest;

namespace ZambaWeb.RestApi.Controllers.Task.ObservacionesV2
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class ObservacionesController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getAddComentariosObservaciones")]
        [OverrideAuthorization]
        public IHttpActionResult getAddComentariosObservaciones(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                CommonFuntions cf = new CommonFuntions();
                var user = cf.GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    string AtriubutoMigracion = string.Empty;
                    if (paramRequest.Params != null)
                    {
                        Int64 entityId = Int64.Parse(paramRequest.Params["entityId"]);
                        Int64 parentResultId = Int64.Parse(paramRequest.Params["parentResultId"]);
                        string InputObservacion = paramRequest.Params["InputObservacion"];
                        //Int64 TipoId = Int64.Parse(paramRequest.Params["TipoId"]);
                        Int64 AtributeId = Int64.Parse(paramRequest.Params["AtributeId"]);
                        if (InputObservacion != "")
                        {
                            Results_Business RB = new Results_Business();
                            DataTable result = RB.InsertIndexObservaciones(entityId, parentResultId, InputObservacion, AtributeId, user.ID);
                            long newsID = CoreBusiness.GetNewID(IdTypes.News);
                            NewsBusiness NB = new NewsBusiness();
                            NB.SaveNews(newsID, parentResultId, entityId, "Comento: " + InputObservacion, user.ID,string.Empty);
                            var newresults = string.Empty;
                            return Ok(newresults);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getResultsComentariosObservaciones")]
        [OverrideAuthorization]
        public IHttpActionResult getResultsComentariosObservaciones(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                CommonFuntions cf = new CommonFuntions();
                var user = cf.GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 entityId = Int64.Parse(paramRequest.Params["entityId"]);
                    Int64 parentResultId = Int64.Parse(paramRequest.Params["parentResultId"]);
                    //Int64 TipoId = Int64.Parse(paramRequest.Params["TipoId"]);

                    Int64 AtributeId = Int64.Parse(paramRequest.Params["AtributeId"]);


                    Results_Business RB = new Results_Business();
                    DataTable result = RB.GetObservaciones(entityId, parentResultId, AtributeId);
                    List<ObservacionesDto> TL = new List<ObservacionesDto>();
                    foreach (DataRow r in result.Rows)
                    {
                        ObservacionesDto dto = new ObservacionesDto();
                        DateTime a = DateTime.Parse(r["DATEOBS"].ToString());
                        var GridUser = cf.GetUser(long.Parse(r["USER_ID"].ToString()));
                        dto.id = r["ID"].ToString();
                        dto.docId = r["DOC_ID"].ToString();
                        dto.Nombre = GridUser.Nombres;
                        dto.Apellido = GridUser.Apellidos;
                        dto.dateobs = a.ToString("d");
                        dto.value = r["VALUE"].ToString();
                        dto.Foto = cf.GetBase64Photo(GridUser.ID).ToString();
                        TL.Add(dto);
                    }
                    var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getMigracionObservaciones")]
        [OverrideAuthorization]
        public IHttpActionResult getMigracionObservaciones(genericRequest paramRequest)
        {
            UserBusiness UserBusiness = new UserBusiness();

            List<String> SinExito = new List<string>();
            if (paramRequest != null)
            {
                CommonFuntions cf = new CommonFuntions();
                var user = cf.GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
                Int64 entidad = 0;
                Int64 AtributeId = Int64.Parse(paramRequest.Params["AtributeId"]);
                Int32 entityId = Int32.Parse(paramRequest.Params["entityId"]); 
                Results_Business RBS = new Results_Business();
                Int64 docId=0;
                //DataTable dtEntidad = RBS.getEntidadObservaciones();
                int[] TotalEntidades = new int[] { entityId };

                foreach (var item in TotalEntidades)
                
                    {

                    try
                    {
                        entidad = item;
                        //entidad = 26;

                        ///// Se elimina la data de las tablas zobs para uiniciar la migracion
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia el procese de Migracion para la edtidad " + entidad);
                        try
                        {
                            RBS.DeletMigracionObservaciones(entidad, AtributeId);
                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al borrar los datos de " + entidad);
                            ZClass.raiseerror(ex);
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                       new HttpError(StringHelper.InvalidParameter)));
                        }

                        //// Se inicia migracion

                        DataTable MigracionResult = RBS.MigracionObservaciones(entidad);

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se extrajo de manera exitosa la data de la i84 para la entidad " + entidad);

                        for (int i = 0; i < MigracionResult.Rows.Count; i++)
                        {
                            string ValueMigracion = MigracionResult.Rows[i][0].ToString();

                            string Usuario, Fecha, Value;
                            char[] spearator = { '-' };


                            if (ValueMigracion.Length > 13)
                            {
                                if (ValueMigracion.Substring(12, 1) == "-")
                                {
                                    ValueMigracion = ValueMigracion.Replace("[", "");
                                    ValueMigracion = ValueMigracion.Replace("]", "");
                                }
                            }
                            if (ValueMigracion.Contains("[") && ValueMigracion.Contains("]") && (ValueMigracion.Split('-').Length - 1)  >= 2 )
                            {
                                ValueMigracion = ValueMigracion.Replace("]", "");
                                ValueMigracion = ValueMigracion.Replace("[", "");
                            }
                            if (ValueMigracion.Substring(0,1) == "-")
                            {
                                ValueMigracion = ValueMigracion.Remove(0, 1);
                            }
                            if (ValueMigracion.Substring(0, 1) == "\n")
                            {
                                ValueMigracion = ValueMigracion.Remove(0, 1);

                            }

                            var eval = (ValueMigracion.Length - 1).ToString();
                            var FinalCaracter = ValueMigracion.Substring((ValueMigracion.Length - 1), 1);
                            if (FinalCaracter  == "-")
                            {
                                ValueMigracion = ValueMigracion.Remove(Int32.Parse(eval), 1);
                            }

                            if (ValueMigracion.Contains("["))
                            {

                                try
                                {
                                    String[] strlist = ValueMigracion.Split(spearator);
                                    int ciclo = strlist.Length - 1;
                                    for (int y = 0; y < ciclo; y = y + 2)
                                    {
                                        docId = Int64.Parse(MigracionResult.Rows[i][1].ToString());
                                        var formater = strlist[y];
                                        Fecha = formater.Substring(0, 20);
                                        Fecha = Fecha.Replace("[", "");
                                        Usuario = formater.Remove(0, 22);
                                        Usuario = Usuario.Replace(" ", "");
                                        Value = strlist[y + 1];
                                        Value = Value.Replace("]", "");
                                        Value = Value.Replace("”", "");
                                        Value = Value.Replace("“", "");
                                        docId = Int64.Parse(MigracionResult.Rows[i][1].ToString());
                                        IUser Usr = UserBusiness.GetUserByname(Usuario, true);
                                        RBS.InsertMigracionObservaciones2(entidad, Fecha, Usr.ID, Value, docId, AtributeId);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ZClass.raiseerror(ex);
                                    SinExito.Add(entidad + "-" + docId.ToString());
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se migro sin exito la entidad " + entidad + " Doc_id " + docId.ToString());

                                }
                            }
                            else
                            {
                                try
                                {
                                    if (!(ValueMigracion.IndexOf("\n") == -1))
                                        ValueMigracion = ValueMigracion.Replace("\n", "-");

                                    String[] strlist = ValueMigracion.Split(spearator);
                                    List<String> Newstrlist = new List<String>();
                                    int ciclo = 0;
                                    if (strlist[strlist.Length - 1] == "")
                                    {
                                        ciclo = strlist.Length - 1;
                                        for (int y = 0; y < ciclo; y = y + 3)
                                        {

                                            docId = Int64.Parse(MigracionResult.Rows[i][1].ToString());
                                            Fecha = strlist[y];
                                            Usuario = strlist[y + 1];
                                            Value = strlist[y + 2];
                                            Value = Value.Replace("”", "");
                                            Value = Value.Replace("“", "");
                                            Usuario = Usuario.Replace(" ", "");
                                            IUser Usr = UserBusiness.GetUserByname(Usuario, true);
                                            RBS.InsertMigracionObservaciones(entidad, Fecha, Usr.ID, Value, docId, AtributeId);
                                        }
                                    }
                                    else
                                    {
                                        for (int yz = 0; yz < strlist.Length; yz = yz + 3)
                                        {
                                            
                                            if (yz + 4 <= strlist.Length )
                                            {
                                                if (strlist[yz + 3].Substring(2, 1) != "/" && strlist[yz + 3].Length != 11) {

                                                    Newstrlist.Add(strlist[yz]);
                                                    Newstrlist.Add(strlist[yz + 1]);
                                                    if (yz + 2 == strlist.Length)
                                                    {
                                                        Newstrlist.Add(strlist[yz + 2]);
                                                        yz = yz + 1;
                                                    }
                                                    else
                                                    {
                                                        if (strlist[yz + 4].Substring(2, 1) != "/" && strlist[yz + 3].Length != 11) {
                                                            Newstrlist.Add(strlist[yz + 2] + strlist[yz + 3] + strlist[yz + 4]);
                                                            yz = yz + 2;
                                                        }
                                                        else {
                                                            Newstrlist.Add(strlist[yz + 2] + strlist[yz + 3]);
                                                            yz = yz + 1;
                                                        }
                                                       
                                                    }

                                                    
                                                }
                                              
                                            }
                                            else
                                            {
                                                Newstrlist.Add(strlist[yz]);
                                                Newstrlist.Add(strlist[yz+1]);
                                                Newstrlist.Add(strlist[yz+2]);
                                            }


                                        }

                                        if (Newstrlist.Count > 0) {

                                            for (int y = 0; y < Newstrlist.Count; y = y + 3)
                                            {

                                                docId = Int64.Parse(MigracionResult.Rows[i][1].ToString());
                                                Fecha = Newstrlist[y];
                                                Usuario = Newstrlist[y + 1];
                                                Value = Newstrlist[y + 2];
                                                Value = Value.Replace("”", "");
                                                Value = Value.Replace("“", "");
                                                Usuario = Usuario.Replace(" ", "");
                                                IUser Usr = UserBusiness.GetUserByname(Usuario, true);
                                                RBS.InsertMigracionObservaciones(entidad, Fecha, Usr.ID, Value, docId, AtributeId);
                                            }
                                        }
                                        else {
                                            for (int y = 0; y < strlist.Length; y = y + 3)
                                            {

                                                docId = Int64.Parse(MigracionResult.Rows[i][1].ToString());
                                                Fecha = strlist[y];
                                                Usuario = strlist[y + 1];
                                                Value = strlist[y + 2];
                                                Value = Value.Replace("”", "");
                                                Value = Value.Replace("“", "");
                                                Usuario = Usuario.Replace(" ", "");
                                                IUser Usr = UserBusiness.GetUserByname(Usuario, true);
                                                RBS.InsertMigracionObservaciones(entidad, Fecha, Usr.ID, Value, docId, AtributeId);
                                            }
                                        }

                                        
                                    }
                                   
                                }
                                catch (Exception ex)
                                {
                                    ZClass.raiseerror(ex);
                                    SinExito.Add(entidad + "-" + docId.ToString());
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se migro sin exito la entidad " + entidad + " Doc_id " + docId.ToString());

                                }

                            }
                        }

                        SinExito = SinExito.ToArray().Distinct().ToList();
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se finaliza con exito el migrado de la entidad " + entidad);
                        //SinExito.Clear();

                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                       ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al borrar los datos de Migrado" + entidad);
                        //SinExito.Clear();
                    }
                }
                
            }
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Lista de entidades no migradas: ");

            foreach (var value in SinExito)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, value);
            }
            return null;
        }
    }
}