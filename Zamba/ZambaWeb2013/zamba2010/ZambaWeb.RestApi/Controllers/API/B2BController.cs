using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Framework;
using Zamba.Membership;
using Zamba.Services;
using ZambaWeb.RestApi.Models;

namespace ZambaWeb.RestApi.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/b2b")]
    public class B2BController : ApiController
    {

        [Route("InsertResults")]
        [HttpPost]
        public IHttpActionResult InsertResults(genericRequest paramRequest)
        {
            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }

            try
            {
                AccountController AC = new AccountController();
                AC.LoginById(paramRequest.UserId);

                //UserBusiness UBR = new UserBusiness();
                // IUser user = UBR.ValidateLogIn(paramRequest.UserId, ClientType.WebApi); 

                //if (user == null)
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                //        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("entityId") && !string.IsNullOrEmpty(paramRequest.Params["entityId"]) &&
                paramRequest.Params.ContainsKey("data") && !string.IsNullOrEmpty(paramRequest.Params["data"]))
                {

                    Int64 entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                    string data = paramRequest.Params["data"].ToString();
                    data = data.Trim().TrimStart(Char.Parse("{"));
                    data = data.Trim().Replace("\"Table\"", "");
                    data = data.Trim().TrimStart(Char.Parse(":"));
                    data = data.Trim().TrimEnd(Char.Parse("}"));
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(data.Trim());
                    Results_Business RB = new Results_Business();

                    foreach (DataRow r in dataTable.Rows)
                    {

                        try
                        {

                            INewResult nr = RB.GetNewResult(entityId, string.Empty);
                            nr.Indexs = ZCore.GetInstance().FilterIndex(entityId);
                            foreach (DataColumn c in dataTable.Columns)
                            {
                                if (c.ColumnName.StartsWith("I"))
                                {
                                    IIndex I = ((IResult)nr).get_GetIndexById(Int64.Parse(c.ColumnName.Replace("I","")));
                                    if (I != null)
                                    {
                                        I.DataTemp = r[c.ColumnName].ToString();
                                    }
                                }
                            }

                            InsertResult ir = RB.Insert(ref nr, false, false, false, false, true, false, false, false, false, paramRequest.UserId);
                            //if (ir = InsertResult.Insertado) { 

                            //}
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }

                    }

                    return Ok();

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }



        [Route("GetResultFile")]
        [HttpPost]
        public IHttpActionResult GetResultFile(genericRequest paramRequest)
        {
            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }

            try
            {
                AccountController AC = new AccountController();
                AC.LoginById(paramRequest.UserId);

               

                if (paramRequest.Params.ContainsKey("entityId") && !string.IsNullOrEmpty(paramRequest.Params["entityId"]) &&
                paramRequest.Params.ContainsKey("keyValue") && !string.IsNullOrEmpty(paramRequest.Params["keyValue"]))
                {


                    byte[] _file = null;
                    try
                    {
                        Int64 entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        string key = paramRequest.Params["key"].ToString();
                        string keyValue = paramRequest.Params["keyValue"].ToString();

                        bool convertToPDf = true;

                        Int64 docid = Int64.Parse(Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("select doc_id from doc_I{0} where I{1} = '{2}'", entityId, key,keyValue)).ToString());

                        SResult sResult = new SResult();
                        Result res = (Result)sResult.GetResult(docid, entityId, false);

                        try
                        {
                            return Ok(GetDocumentData(paramRequest.UserId.ToString(), entityId.ToString(), docid.ToString(), ref convertToPDf, res));
                            //return Ok(documentdata);
                        }
                        catch (Exception ex)
                        {
                            return Ok(System.Convert.FromBase64String(GetDocumentData(paramRequest.UserId.ToString(), entityId.ToString(), docid.ToString(), ref convertToPDf, res)));
                            throw ex;
                        }

                      
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
                    }
                    finally
                    {
                        if (_file != null)
                            _file = null;
                    }

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }


        private string GetDocumentData(string userId, string doctypeId, string docId, ref bool convertToPDf, IResult res)
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            SResult sResult = new SResult();

            long DocTypeId, DocId, userID;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(doctypeId) && !string.IsNullOrEmpty(docId))
            {
                DocTypeId = long.Parse(doctypeId);
                DocId = long.Parse(docId);
                userID = long.Parse(userId);

                try
                {
                    if (MembershipHelper.CurrentUser == null)
                    {
                        UserBusiness UB = new UserBusiness();
                        UB.ValidateLogIn(userID, ClientType.Web);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else
            {
                ZClass.raiseerror(new Exception("No se recibieron todos los parametros"));
                throw new Exception("No se recibieron todos los parametros");
            }



            byte[] _file = null;
            if (res != null && res.FullPath != null && res.FullPath.Contains("."))
            {
                bool IsBlob = false;

                string filename = res.FullPath;
                string newPDFFile = res.FullPath + ".pdf";

                if (convertToPDf && File.Exists(newPDFFile) && res.IsMsg == false && res.IsOffice2 == false)
                {
                    _file = FileEncode.Encode(newPDFFile);
                    filename = newPDFFile;
                }

                if (_file == null || _file.Length == 0)
                {
                    if (convertToPDf == false)
                        filename = res.FullPath;

                    if (res.IsMsg)
                    {
                        if (File.Exists(filename.Replace(".msg", ".html")))
                        {
                            filename = res.FullPath;
                            filename = filename.Replace(".msg", ".html");
                            res.Doc_File = Path.GetFileName(res.FullPath).ToLower().Replace(".msg", ".html");
                        }
                        else
                        {
                            Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();

                            var a = JsonConvert.SerializeObject(ST.ConvertMSGToJSON(res.FullPath, newPDFFile, true), Formatting.Indented,
                              new JsonSerializerSettings
                              {
                                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
                              });
                            return a;
                        }
                    }



                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 &&
                        (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase ||
                        (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))

                        sResult.LoadFileFromDB(ref res);

                    //Verifica si el result contiene el documento guardado
                    if (res.EncodedFile != null)
                    {
                        _file = res.EncodedFile;
                    }
                    else
                    {
                        string sUseWebService = Zopt.GetValue("UseWebService");
                        //Verifica si debe utilizar el webservice para obtener el documento
                        if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                            _file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, userID);
                        else
                            _file = sResult.GetFileFromResultForWeb(res, out IsBlob);
                    }

                    if (_file != null && _file.Length > 0)
                    {
                        if (res.IsWord)
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsHTML || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                }
                            }
                        }

                        if ((res.IsImage) && res.IsTif == false)
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                }
                            }
                        }

                        if ((res.IsTif))
                        {
                            String TempDir = ZOptBusiness.GetValueOrDefault("PdfsDirectory", Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Pdfs"));
                            TempDir = Path.Combine(TempDir, res.DocTypeId.ToString());

                            try
                            {
                                if (Directory.Exists(TempDir) == false)
                                    Directory.CreateDirectory(TempDir);
                            }
                            catch (Exception)
                            {
                                TempDir = Path.Combine(MembershipHelper.AppTempPath, "Pdfs", res.DocTypeId.ToString());
                            }

                            if (convertToPDf == false)
                            {
                                if (res.RealFullPath().Contains("__.__"))
                                {
                                    string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                    FileEncode.Decode(tempPDFFile, _file);
                                    _file = FileEncode.Encode(tempPDFFile);
                                    filename = res.FullPath;
                                }
                            }
                            else
                            {
                                if (res.RealFullPath().Contains("__.__"))
                                {
                                    newPDFFile = Path.Combine(TempDir, Path.GetFileName(res.RealFullPath())) + ".pdf";
                                    if (File.Exists(newPDFFile) == false)
                                    {
                                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                        FileEncode.Decode(tempPDFFile, _file);
                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, _file);
                                            _file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
                                        }

                                    }
                                    else
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                }
                                else
                                {
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                    if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else { filename = res.FullPath; }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }
                    }
                }

                if (_file == null || _file.Length <= 0)
                {
                    throw new Exception("No se pudo obtener el recurso");
                }

                Zopt = null;
                sResult = null;

                return System.Convert.ToBase64String(_file);
            }
            throw new Exception("No se pudo obtener el recurso");
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
    }
}
