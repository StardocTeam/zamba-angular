using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Framework;
using Zamba.Services;
using ZambaWeb.RestApi.Models;
namespace ZambaWeb.RestApi.Controllers
{

    public class InsertFilesController : ApiController
    {

        public InsertFilesController()
        {
            ZCore ZC = new ZCore();
            if (Zamba.Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.API");

            ZC.VerifyFileServer();
        }

        [AcceptVerbs("GET", "POST")]
        [Route("Login")]
        public IHttpActionResult KeepAlive()
        {
            try
            {
                HttpContext.Current.Session["SessionRefreshToken"] = DateTime.Now;
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Error en KeepAlive: " + ex.ToString())));
            }
        }

        public Zamba.Core.IUser GetUser(long? userId)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("User por Identity: ", User.Identity));
            var user = TokenHelper.GetUser(User.Identity);

            UserBusiness UBR = new UserBusiness();

            if (userId.HasValue && userId > 0 && user == null)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("UserId por Parametro: ", userId));
                user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
            }

            if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
            {
                Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId por Header").FirstOrDefault());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("UserId: ", userId));
                if (UserId > 0)
                {
                    user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                }
            }
            UBR = null;

            return user;
        }


        //[Authorize]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/InsertFiles/GetIndexsFromDocType")]
        public List<IIndexList> GetIndexsFromDocType(int DocTypeId)
        {
            try
            {
                List<IIndexList> indexs = new List<IIndexList>();


                StringBuilder querySt = new StringBuilder();
                querySt.Append("Select doc_type.doc_type_id, doc_type.doc_type_name, doc_index.index_id,doc_index.index_name ");
                querySt.Append("from index_r_doc_type inner join doc_index on index_r_doc_type.indeX_id = doc_index.index_id ");
                querySt.Append("inner join doc_type on doc_type.doc_type_id = index_r_doc_type.doc_type_id where index_r_doc_type.doc_type_id = '" + DocTypeId + "' ");
                string query = string.Format(querySt.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);


                //indexs.Add();

                return indexs;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/InsertFiles/GetDocTypes")]
        public System.Collections.ArrayList GetDocTypes()
        {
            DocTypesBusiness DTB = new DocTypesBusiness();
            System.Collections.ArrayList doctypes = null;
            doctypes = DTB.GetDocTypesArrayList();
            DTB = null;
            return doctypes;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/InsertFiles/UploadFile")]
        public string UploadFile([FromBody] insert insert)
        {
            Int64 newId = 0;
            string pathTemp = string.Empty;
            try
            {
                pathTemp.ToList();
                newId = Zamba.Data.CoreData.GetNewID(IdTypes.DOCID);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, $"new ID: {newId}");
                CopyFileBKP(ref pathTemp, insert, newId);
            }
            catch (Exception ex)
            {
                Dictionary<object, object> datosInsert = new Dictionary<object, object>();

                ZClass.raiseerror(ex);

                try
                {
                    ex.Data.Add("DocTypeId", insert.DocTypeId);

                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                try
                {
                    ex.Data.Add("UserId", insert.Userid);

                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                try
                {
                    ex.Data.Add("Id", string.Empty);

                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                try
                {
                    ex.Data.Add("Value", string.Empty);

                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                foreach (var item in insert.indexs)
                {
                    try
                    {
                        ex.Data["Id"] += string.Format("{0}; ", item.id.ToString());
                        ex.Data["Value"] += string.Format("{0}; ", item.value.ToString());
                    }
                    catch (Exception e)
                    {
                        ZClass.raiseerror(e);
                    }
                }

                try
                {
                    ex.Data.Add("File", insert.file.data);
                }
                catch (Exception e)
                {
                    ZClass.raiseerror(e);
                }

                ZClass.raiseerror(new Exception("Error al insertar un archivo desde UploadFile. Err. 7009", ex));

                return JsonConvert.SerializeObject(ex, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
            }
            if (insert != null)
            {
                var user = GetUser(insert.Userid);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser))).ToString();
                try
                {
                    SUsers us = new SUsers();
                    List<IIndex> indexs = new List<IIndex>();
                    SResult sResult = new SResult();
                    InsertResult result = InsertResult.NoInsertado;
                    INewResult newresult = new SResult().GetNewNewResult(insert.DocTypeId);
                    StringBuilder IndexDataLog = new StringBuilder();
                    foreach (var InsertIndex in insert.indexs)
                    {
                        foreach (var NewResultIndex in newresult.Indexs)
                        {
                            if (NewResultIndex.ID == InsertIndex.id)
                            {
                                NewResultIndex.Data = InsertIndex.value;
                                NewResultIndex.DataTemp = InsertIndex.value;
                                indexs.Add(NewResultIndex);
                                IndexDataLog.Append(NewResultIndex.Name);
                                IndexDataLog.Append(": ");
                                IndexDataLog.Append(InsertIndex.value);
                                IndexDataLog.Append(", ");

                                break;
                            }
                        }
                    }

                    ZTrace.WriteLineIf(ZTrace.IsInfo, IndexDataLog.ToString());


                    result = sResult.Insert(ref newresult, pathTemp, insert.DocTypeId, indexs, insert.Userid);

                    if (result == InsertResult.Insertado)
                    {
                        return JsonConvert.SerializeObject(true, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });
                    }
                    else
                    {
                        Exception ex = new Exception(result.ToString());
                        ZClass.raiseerror(ex);
                        return JsonConvert.SerializeObject(ex, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                    }
                }

                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return JsonConvert.SerializeObject(ex, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                }
                finally
                {
                    System.IO.File.Move(pathTemp, pathTemp + "_INSERTED");
                }
            }
            else
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
    new HttpError(StringHelper.BadInsertParameter))).ToString();

        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/InsertFiles/UploadFileWithIndexReturn")]
        public string UploadFileWithIndexReturn([FromBody] insert insert)
        {
            string pathTemp = string.Empty;
            Int64 newId = 0;

            //1- NEWRESULT
            //2- DOCID
            //3- VALIDAR ATRIBUTOS MINIMO HAYA UNO, OBLIGATORIOS
            //4- AUTOCOMPLETAR DATOS
            //5- VALOR DEFECTO
            //6- RETURNID (INDICE DE RETORNO)
            //7- COPIAR ARCHIVO AL VOLUMEN

            //-----------------------------------------------------------------------
            //8- INSERTAR DOCT Referecia al Archivo
            //9- INSERTAR DOCI Indices
            //10- EJECUTAR WF ENTRADA
            //11- GUARDAR REGISTROS INDEXACION
            //12- GUARDAR HISTORIAL
            //-----------------------------------------------------------------------

            //13- RETORNAR OK(RETURNVALUE) Valor del Indice Pedidod en RETURNID

            //14- ASSOCIADOS?

            //------------------------
            // RETORNAR ERROR


            try
            {

                if (insert != null)
                {
                    var user = GetUser(insert.Userid);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser))).ToString();

                    newId = Zamba.Data.CoreData.GetNewID(IdTypes.DOCID);
                    CopyFileBKP(ref pathTemp, insert, newId);


                    try
                    {
                        SUsers us = new SUsers();
                        List<IIndex> indexs = new List<IIndex>();
                        SResult sResult = new SResult();
                        InsertResult result = InsertResult.NoInsertado;
                        INewResult newresult = new SResult().GetNewNewResult(insert.DocTypeId);
                        StringBuilder IndexDataLog = new StringBuilder();
                        foreach (var InsertIndex in insert.indexs)
                        {
                            Boolean IndexValidate = false;
                            String ErrorMsg = "";
                            foreach (var NewResultIndex in newresult.Indexs)
                            {

                                if (NewResultIndex.ID == InsertIndex.id)
                                {
                                    NewResultIndex.Data = InsertIndex.value;
                                    NewResultIndex.DataTemp = InsertIndex.value;
                                    indexs.Add(NewResultIndex);
                                    IndexDataLog.Append(NewResultIndex.Name);
                                    IndexDataLog.Append(": ");
                                    IndexDataLog.Append(InsertIndex.value);
                                    IndexDataLog.Append(", ");
                                    IndexValidate = true;
                                    break;
                                }
                            }
                            if (!IndexValidate)
                                throw new Exception($"El atributo no existe en la Entidad seleccionada. ({InsertIndex.id} : {InsertIndex.value})");
                        }

                        ZTrace.WriteLineIf(ZTrace.IsInfo, IndexDataLog.ToString());

                        newresult.File = insert.OriginalFileName;
                        result = sResult.Insert(ref newresult, pathTemp, insert.DocTypeId, indexs, insert.Userid, newId);

                        if (result == InsertResult.Insertado)
                        {
                            try
                            {
                                if (insert.OriginalFileName != null && !string.IsNullOrEmpty(insert.OriginalFileName))
                                {
                                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("update doc_t{0} set Original_FileName = '{1}' where doc_id = {2}", insert.DocTypeId, insert.OriginalFileName, newresult.ID));
                                }
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                            }

                            Results_Business rb = new Results_Business();

                            IResult insertedResult = rb.GetResult(newresult.ID, newresult.DocTypeId, true);

                            List<char> DigitisIds = insertedResult.ID.ToString().ToList();
                            string NewID = string.Empty;
                            for (int i = 0; i < DigitisIds.Count - 1; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    NewID += DigitisIds[i] + new Random().Next(9).ToString();
                                }
                                else
                                {
                                    NewID += DigitisIds[i];

                                }
                            }
                            Int64 internalId = Int64.Parse(NewID);

                            IIndex ReturnNewIndex = insertedResult.get_GetIndexById(insert.ReturnId);

                            if (ReturnNewIndex == null)
                                throw new Exception($"El atributo de retorno es indexistenteen la entidad seleccionada. ({insert.ReturnId})");

                            insertResult IR = new insertResult
                            {
                                Id = internalId,
                                ReturnId = insert.ReturnId,
                                ReturnValue = ReturnNewIndex.Data,
                                msg = "OK"
                            };


                            //------------------------------------------AssociatesResults-----------------------------------------

                            List<insertResult> AssociatedIRs = InsertAssociatedResults(newresult, insert, user);
                            IR.AssociatedResults = AssociatedIRs;
                            //--------------------------------------------------------------------------------------------------------------------

                            return JsonConvert.SerializeObject(IR, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                            });
                        }
                        else
                        {
                            Exception ex = new Exception(result.ToString());
                            ZClass.raiseerror(ex);
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,
        new HttpError(ex.ToString()))).ToString();

                        }
                    }

                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,
        new HttpError(ex.ToString()))).ToString();
                    }
                    finally
                    {
                        System.IO.File.Move(pathTemp, pathTemp + "_INSERTED");
                    }
                }
                else
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
        new HttpError(StringHelper.BadInsertParameter))).ToString();


            }
            catch (Exception ex)
            {

                Dictionary<object, object> datosInsert = new Dictionary<object, object>();

                ex.Data.Add("DocTypeId", insert.DocTypeId);
                ex.Data.Add("UserId", insert.Userid);
                ex.Data.Add("Id", string.Empty);
                ex.Data.Add("Value", string.Empty);

                foreach (var item in insert.indexs)
                {
                    ex.Data["Id"] += string.Format("{0}; ", item.id.ToString());
                    ex.Data["Value"] += string.Format("{0}; ", item.value.ToString());
                }

                ex.Data.Add("File", insert.file.data);

                ZClass.raiseerror(ex);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,
                new HttpError(ex.ToString()))).ToString();

            }

        }


        private List<insertResult> InsertAssociatedResults(IResult parentResult, insert insertRequest, IUser User)
        {
            List<insertResult> IRs = new List<insertResult>();
            Results_Business rb = new Results_Business();
            SUsers us = new SUsers();

            if (insertRequest.associated != null)
            {
                foreach (Associated associated in insertRequest.associated)
                {
                    Int64 newId = 0;

                    try
                    {
                        if (associated != null)
                        {
                            newId = Zamba.Data.CoreData.GetNewID(IdTypes.DOCID);

                            try
                            {
                                List<IIndex> indexs = new List<IIndex>();
                                SResult sResult = new SResult();
                                InsertResult result = InsertResult.NoInsertado;
                                INewResult newresult = new SResult().GetNewNewResult(associated.DocTypeId);

                                StringBuilder IndexDataLog = new StringBuilder();
                                foreach (var InsertIndex in associated.indexs)
                                {
                                    Boolean IndexValidate = false;
                                    String ErrorMsg = "";
                                    foreach (var NewResultIndex in newresult.Indexs)
                                    {

                                        if (NewResultIndex.ID == InsertIndex.id)
                                        {
                                            NewResultIndex.Data = InsertIndex.value;
                                            NewResultIndex.DataTemp = InsertIndex.value;
                                            indexs.Add(NewResultIndex);
                                            IndexDataLog.Append(NewResultIndex.Name);
                                            IndexDataLog.Append(": ");
                                            IndexDataLog.Append(InsertIndex.value);
                                            IndexDataLog.Append(", ");
                                            IndexValidate = true;
                                            break;
                                        }
                                    }
                                    if (!IndexValidate)
                                        throw new Exception($"El atributo no existe en la Entidad seleccionada. ({InsertIndex.id} : {InsertIndex.value})");
                                }

                                ZTrace.WriteLineIf(ZTrace.IsInfo, IndexDataLog.ToString());

                                newresult.OffSet = parentResult.OffSet;
                                newresult.Disk_Group_Id = parentResult.Disk_Group_Id;
                                newresult.DISK_VOL_PATH = parentResult.DISK_VOL_PATH;
                                //                               newresult.Volume = parentResult.Volume;
                                newresult.Doc_File = parentResult.File;
                                result = sResult.Insert(ref newresult, false, false, false, false, true, false, false, true, false, User.ID, newId, true);

                                if (result == InsertResult.Insertado)
                                {
                                    try
                                    {
                                        if (insertRequest.OriginalFileName != null && !string.IsNullOrEmpty(insertRequest.OriginalFileName))
                                        {
                                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, $"update doc_t{associated.DocTypeId} set disk_group_id = -1, doc_file = '{parentResult.FullPath}', offset = {parentResult.OffSet}, vol_id = -1, Original_FileName = '{insertRequest.OriginalFileName}' where doc_id = {newresult.ID}");
                                        }
                                        else
                                        {
                                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, $"update doc_t{associated.DocTypeId} set disk_group_id = -1, doc_file = '{parentResult.FullPath}', offset = {parentResult.OffSet}, vol_id = -1 where doc_id = {newresult.ID}");
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        ZClass.raiseerror(ex);
                                    }

                                    insertResult IR = null;

                                    List<char> DigitisIds = newresult.ID.ToString().ToList();
                                    string NewID = string.Empty;
                                    for (int i = 0; i < DigitisIds.Count - 1; i++)
                                    {
                                        if (i % 2 == 0)
                                        {
                                            NewID += DigitisIds[i] + new Random().Next(9).ToString();
                                        }
                                        else
                                        {
                                            NewID += DigitisIds[i];

                                        }
                                    }
                                    Int64 internalId = Int64.Parse(NewID);
                                    IR = new insertResult
                                    {
                                        Id = internalId,
                                        ReturnId = insertRequest.ReturnId,
                                        msg = "OK"
                                    };

                                    if (insertRequest.ReturnId > 0)
                                    {
                                        IResult res = rb.GetResult(newresult.ID, newresult.DocTypeId, true);
                                        IIndex ReturnNewIndex = res.get_GetIndexById(insertRequest.ReturnId);

                                        if (ReturnNewIndex == null || ReturnNewIndex.Data == string.Empty)
                                        {
                                            throw new Exception($"El atributo de retorno no existe en la entidad seleccionada. ({insertRequest.ReturnId})");
                                        }
                                        else
                                        {
                                            IR.ReturnValue = ReturnNewIndex.Data;
                                        }
                                    }

                                    IRs.Add(IR);
                                }
                                else
                                {
                                    Exception ex = new Exception(result.ToString());
                                    ZClass.raiseerror(ex);
                                    throw new Exception(ex.ToString());
                                }
                            }

                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                throw new Exception(ex.ToString());

                            }
                        }
                        else
                            throw new Exception(StringHelper.BadInsertParameter);

                    }
                    catch (Exception ex)
                    {
                        Dictionary<object, object> datosInsert = new Dictionary<object, object>();

                        ZClass.raiseerror(ex);
                        ex.Data.Add("DocTypeId", associated.DocTypeId);
                        ex.Data.Add("UserId", insertRequest.Userid);
                        ex.Data.Add("Id", string.Empty);
                        ex.Data.Add("Value", string.Empty);

                        foreach (var item in associated.indexs)
                        {
                            ex.Data["Id"] += string.Format("{0}; ", item.id.ToString());
                            ex.Data["Value"] += string.Format("{0}; ", item.value.ToString());
                        }

                        ex.Data.Add("File", insertRequest.file.data);

                        throw;
                    }
                }
            }

            return IRs;

        }

        private void CopyFileBKP(ref string pathTemp, insert insert, Int64 newDocID)
        {
            if (insert.file != null)
            {
                var filename = newDocID + "." + insert.file.extension;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format(@"Nuevo Archivo: {0}", filename));

                if (!Directory.Exists(Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp")))
                {
                    Directory.CreateDirectory(Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp"));
                }

                pathTemp = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp", filename);

                string[] s = insert.file.data.Split(',');
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format(@"String de Archivo: {0}", insert.file.data));
                var data = Convert.FromBase64String(s[1].Replace(" ", "+"));
                File.WriteAllBytes(pathTemp, data);

                var pathBKP = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "BKP InsertFile", DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(pathBKP))
                {
                    Directory.CreateDirectory(pathBKP);
                }
                File.Copy(pathTemp, Path.Combine(pathBKP, filename), true);
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo inexistente.");
                throw new Exception("Archivo inexistente.");

            }

        }


    }
}


