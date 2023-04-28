using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebGrease;
using Zamba;
using Zamba.Core;
using Zamba.Core.Access;
using Zamba.Framework;
using Zamba.Membership;
using Zamba.Services;
using ZambaWeb.RestApi.Controllers.Class;

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
                                    IIndex I = ((IResult)nr).get_GetIndexById(Int64.Parse(c.ColumnName.Replace("I", "")));
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


        [Route("GetResultFileByDocId")]
        [HttpPost]
        public IHttpActionResult GetResultFileByDocId(genericRequest paramRequest)
        {
            try
            {
                AccountController AC = new AccountController();
                AC.LoginById(paramRequest.UserId);
                byte[] _file = null;
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada al metodo api/b2b/GetResultFileByDocId");

                try
                {
                    Int64 docId = Int64.Parse(paramRequest.Params["DocId"].ToString());
                    Int64 docTypeId = Int64.Parse(paramRequest.Params["DocTypeId"].ToString());
                    bool includeAttachs = Boolean.Parse(paramRequest.Params["includeAttachs"].ToString());
                    bool MsgPreview = Boolean.Parse(paramRequest.Params["MsgPreview"].ToString());
                    bool convertToPDf = Boolean.Parse(paramRequest.Params["convertToPDf"].ToString());

                    SResult sResult = new SResult();
                    Result res = (Result)sResult.GetResult(docId, docTypeId, false);

                    try
                    {
                        DocumentData DD = new DocumentData();

                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Creando DocuentData");
                        string newPDFFile = string.Empty;

                        string userId = MembershipHelper.CurrentUser.ID.ToString();

                        string data = GetDocumentData(userId, docTypeId.ToString(), docId.ToString(), ref convertToPDf, res, MsgPreview, includeAttachs, ref newPDFFile);

                        DD.fileName = convertToPDf ? newPDFFile : res.Doc_File;

                        DD.ContentType = convertToPDf ? "application/pdf" : res.MimeType;

                        try
                        {
                            var thumbPath = newPDFFile.Replace(".pdf", ".bmp");
                            if (File.Exists(thumbPath))
                            {
                                DD.thumbImage = FileEncode.Encode(thumbPath);
                            }
                            else
                            {
                                var thumbimage = Zamba.Thumbs.Base64.ConvertFromPath(newPDFFile, thumbPath);
                                if (thumbimage != string.Empty)
                                    DD.thumbImage = System.Convert.FromBase64String(thumbimage);
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }


                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                            DD.dataObject = JsonConvert.DeserializeObject<MsgData>(data);
                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                            DD.data = System.Convert.FromBase64String(data);
                        }

                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo");
                            var jsonDD = JsonConvert.SerializeObject(DD);
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json ");
                            return Ok(DD);
                        }
                        catch (Exception ex)
                        {
                            //return Ok(System.Convert.FromBase64String(DD));
                            //throw ex;
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo With EX");
                            var jsonDD = JsonConvert.SerializeObject(DD);
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json with EX");
                            return Ok(DD);
                        }

                    }
                    catch (Exception ex)
                    {
                        //return Ok(System.Convert.FromBase64String(GetDocumentData(paramRequest.UserId.ToString(), docTypeId.ToString(), docId.ToString(), ref convertToPDf, res)));
                        ZClass.raiseerror(ex);
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

                        Int64 docid = Int64.Parse(Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("select doc_id from doc_I{0} where I{1} = '{2}'", entityId, key, keyValue)).ToString());

                        SResult sResult = new SResult();
                        Result res = (Result)sResult.GetResult(docid, entityId, false);

                        try
                        {
                            //return Ok(GetDocumentData(paramRequest.UserId.ToString(), entityId.ToString(), docid.ToString(), ref convertToPDf, res));
                            return Ok();

                            //return Ok(documentdata);
                        }
                        catch (Exception ex)
                        {
                            //return Ok(System.Convert.FromBase64String(GetDocumentData(paramRequest.UserId.ToString(), entityId.ToString(), docid.ToString(), ref convertToPDf, res)));
                            return Ok();
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


        /// <summary>
        /// Obtiene el archivo asociado a la tarea.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doctypeId"></param>
        /// <param name="docId"></param>
        /// <param name="convertToPDf"></param>
        /// <returns>Devuelve un JSON o Bytes[] como String</returns>
        public string GetDocumentData(string userId, string doctypeId, string docId, ref bool convertToPDf, IResult res, bool MsgPreview, bool includeAttachs, ref string newPDFFile)
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            SResult sResult = new SResult();

            long DocTypeId, DocId, userID;
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada a GetDocumentData en b2bController");
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
                if (newPDFFile == string.Empty)
                {
                    if (!res.FullPath.EndsWith(".pdf"))
                        newPDFFile = res.FullPath + ".pdf";
                    else
                        newPDFFile = res.FullPath;

                }

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
                        filename = res.FullPath;

                        if (MsgPreview)
                        {
                            Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo MSG");
                            string a = "";

                            try
                            {
                                a = JsonConvert.SerializeObject(ST.ConvertMSGToJSON(res.FullPath, newPDFFile, includeAttachs), Formatting.Indented,
                                  new JsonSerializerSettings
                                  {
                                      PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                  });
                            }
                            catch (FileNotFoundException ex)
                            {
                                //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                                _file = GetFilesBytesFromDB(res, Zopt, sResult, userID, ref IsBlob);

                                MemoryStream stream = new MemoryStream();
                                stream.Write(_file, 0, _file.Length);

                                a = JsonConvert.SerializeObject(ST.ConvertMSGToJSON(stream, newPDFFile, includeAttachs), Formatting.Indented,
                                  new JsonSerializerSettings
                                  {
                                      PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                  });
                            }

                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Retornando Archivo");
                            return a;
                        }
                    }

                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 && (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase || (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))
                    {
                        sResult.LoadFileFromDB(ref res);
                    }
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
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (!File.Exists(res.FullPath) && _file.Length > 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Copiando Archivo");
                                    File.WriteAllBytes(res.FullPath, _file);
                                }

                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }
                        if (res.IsXPS)
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (!File.Exists(res.FullPath) && _file.Length > 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Copiando Archivo");
                                    File.WriteAllBytes(res.FullPath, _file);
                                }

                                if (ST.ConvertXPSToPFD(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }


                        if ((res.IsHTML || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                if (ST.ConvertHTMLToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }


                        if (res.IsCSV)
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (ST.ConvertCSVToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }

                        if ((res.IsImage) && res.IsTif == false || res.FullPath.Contains(".jpeg"))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (File.Exists(res.FullPath))
                                {
                                    if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else
                                    {
                                        filename = res.FullPath;
                                        convertToPDf = false;
                                    }

                                }
                                else
                                {

                                    //Check if directory exist

                                    var NewPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";

                                    if (!System.IO.Directory.Exists(NewPath))
                                    {
                                        System.IO.Directory.CreateDirectory(NewPath); //Create directory if it doesn't exist
                                    }

                                    string imageName = res.Doc_File;

                                    //set the image path
                                    string imgPath = Path.Combine(NewPath, imageName);
                                    File.WriteAllBytes(imgPath, _file);


                                    if (ST.ConvertImageToPDF(NewPath + imageName, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else
                                    {
                                        filename = res.FullPath;
                                        convertToPDf = false;
                                    }


                                }

                            }
                        }

                        if ((res.IsTif))
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    newPDFFile = Path.Combine(TempDir, Path.GetFileName(res.RealFullPath())) + ".pdf";
                                    if (File.Exists(newPDFFile) == false)
                                    {
                                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                        FileEncode.Decode(tempPDFFile, _file);
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, _file);
                                            _file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
                                            convertToPDf = false;
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);


                                    if (File.Exists(res.FullPath))
                                    {
                                        if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else { filename = res.FullPath; convertToPDf = false; }

                                    }
                                    else
                                    {

                                        //Check if directory exist

                                        var NewPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";

                                        if (!System.IO.Directory.Exists(NewPath))
                                        {
                                            System.IO.Directory.CreateDirectory(NewPath); //Create directory if it doesn't exist
                                        }

                                        string imageName = res.Doc_File;

                                        //set the image path
                                        string imgPath = Path.Combine(NewPath, imageName);
                                        File.WriteAllBytes(imgPath, _file);


                                        if (ST.ConvertTIFFToPDF(NewPath + imageName, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else { filename = res.FullPath; convertToPDf = false; }


                                    }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }
                    }
                }

                if (_file == null || _file.Length <= 0)
                {
                    throw new Exception("No se pudo obtener el recurso File nulo");
                }

                Zopt = null;
                sResult = null;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convert To Base64");
                return System.Convert.ToBase64String(_file);
            }
            throw new Exception("No se pudo obtener el recurso");
        }

        /// <summary>
        /// Obtiene los bytes del documento.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doctypeId"></param>
        /// <param name="docId"></param>
        /// <param name="convertToPDf"></param>
        /// <returns></returns>
        public byte[] GetBytesFromDocument(string userId, string doctypeId, string docId, ref bool convertToPDf, IResult res, bool MsgPreview, ref string newPDFFile)
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            SResult sResult = new SResult();

            long DocTypeId, DocId, userID;
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada a GetBytesFromDocument en b2bController");
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
                if (newPDFFile == string.Empty)
                {
                    if (!res.FullPath.EndsWith(".pdf"))
                        newPDFFile = res.FullPath + ".pdf";
                    else
                        newPDFFile = res.FullPath;

                }

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
                        filename = res.FullPath;

                        if (MsgPreview)
                        {
                            Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo MSG");

                            //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                            _file = GetFilesBytesFromDB(res, Zopt, sResult, userID, ref IsBlob);

                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Retornando Archivo");
                            return _file;
                        }
                    }

                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 && (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase || (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))
                    {
                        sResult.LoadFileFromDB(ref res);
                    }
                    //Verifica si el result contiene el documento guardado
                    if (res.EncodedFile != null)
                    {
                        _file = res.EncodedFile;

                        String TempDir = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "TEMP");
                        TempDir = Path.Combine(TempDir, res.DocTypeId.ToString());
                        try
                        {
                            if (Directory.Exists(TempDir) == false) Directory.CreateDirectory(TempDir);
                        }
                        catch (Exception)
                        {
                            TempDir = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Pdfs", res.DocTypeId.ToString());
                        }
                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                        FileEncode.Decode(tempPDFFile, _file);
                        filename = tempPDFFile;
                        res.File = tempPDFFile;
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
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (!File.Exists(res.FullPath) && _file.Length > 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Copiando Archivo");
                                    File.WriteAllBytes(res.FullPath, _file);
                                }

                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }
                        if (res.IsXPS)
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (!File.Exists(res.FullPath) && _file.Length > 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Copiando Archivo");
                                    File.WriteAllBytes(res.FullPath, _file);
                                }

                                if (ST.ConvertXPSToPFD(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }


                        if ((res.IsHTML || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                if (ST.ConvertHTMLToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }


                        if (res.IsCSV)
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (ST.ConvertCSVToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }

                        if ((res.IsImage) && res.IsTif == false || res.FullPath.Contains(".jpeg"))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);

                                if (File.Exists(res.FullPath))
                                {
                                    if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else
                                    {
                                        filename = res.FullPath;
                                        convertToPDf = false;
                                    }

                                }
                                else
                                {

                                    //Check if directory exist

                                    var NewPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";

                                    if (!System.IO.Directory.Exists(NewPath))
                                    {
                                        System.IO.Directory.CreateDirectory(NewPath); //Create directory if it doesn't exist
                                    }

                                    string imageName = res.Doc_File;

                                    //set the image path
                                    string imgPath = Path.Combine(NewPath, imageName);
                                    File.WriteAllBytes(imgPath, _file);


                                    if (ST.ConvertImageToPDF(NewPath + imageName, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else
                                    {
                                        filename = res.FullPath;
                                        convertToPDf = false;
                                    }


                                }

                            }
                        }

                        if ((res.IsTif))
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    newPDFFile = Path.Combine(TempDir, Path.GetFileName(res.RealFullPath())) + ".pdf";
                                    if (File.Exists(newPDFFile) == false)
                                    {
                                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                        FileEncode.Decode(tempPDFFile, _file);
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, _file);
                                            _file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
                                            convertToPDf = false;
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
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);


                                    if (File.Exists(res.FullPath))
                                    {
                                        if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else { filename = res.FullPath; convertToPDf = false; }

                                    }
                                    else
                                    {

                                        //Check if directory exist

                                        var NewPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";

                                        if (!System.IO.Directory.Exists(NewPath))
                                        {
                                            System.IO.Directory.CreateDirectory(NewPath); //Create directory if it doesn't exist
                                        }

                                        string imageName = res.Doc_File;

                                        //set the image path
                                        string imgPath = Path.Combine(NewPath, imageName);
                                        File.WriteAllBytes(imgPath, _file);


                                        if (ST.ConvertTIFFToPDF(NewPath + imageName, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else { filename = res.FullPath; convertToPDf = false; }


                                    }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools(_file);
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }
                    }
                }

                if (_file == null || _file.Length <= 0)
                {
                    throw new Exception("No se pudo obtener el recurso File nulo");
                }

                Zopt = null;
                sResult = null;

                return _file;
            }
            throw new Exception("No se pudo obtener el recurso");
        }

        /// <summary>
        /// Obtiene un archivo en forma de array de bytes.
        /// </summary>
        /// <returns>Devuelve un HttpResponseMessage</returns>
        [System.Web.Http.AcceptVerbs("POST")]
        [Route("GetDocFilePOSTMethod")]
        [AllowAnonymous]
        public HttpResponseMessage GetDocFilePOSTMethod(genericRequest paramRequest)
        {
            try
            {
                string docid = "";
                string docTypeId = "";
                bool convertToPDf = false;
                long userId = 0;
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada al metodo api/b2b/GetDocFilePOSTMethod");
                docid = paramRequest.Params["DocId"].ToString();
                docTypeId = paramRequest.Params["DocTypeId"].ToString();
                convertToPDf = Boolean.Parse(paramRequest.Params["ConvertToPDf"]);
                userId = paramRequest.UserId;

                AccountController AC = new AccountController();
                AC.LoginById(userId);

                IResult res = new SResult().GetResult(long.Parse(docid), long.Parse(docTypeId), false);
                string newPDFFile = res.FullPath.Replace(".pdf", "") + ".pdf";

                byte[] File = GetBytesFromDocument(userId.ToString(), docTypeId, docid, ref convertToPDf, res, false, ref newPDFFile);

                if (File == null || File.Length == 0)
                {
                    return returnFileNotFound();
                }

                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);
                responseMessage.Content = new ByteArrayContent(File);
                responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

                if ((res.FullPath != null && res.FullPath.ToLower().EndsWith("pdf")) || convertToPDf == true)
                {
                    responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                    if (!convertToPDf)
                        responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last() + ".pdf";
                    else
                        responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last();
                }
                else
                {
                    responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(res.MimeType);
                    responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last();
                }
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada al metodo api/b2b/GetDocFilePOSTMethod");
                return responseMessage;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un archivo en forma de array de bytes.
        /// </summary>
        /// <returns>Devuelve un HttpResponseMessage</returns>
        [System.Web.Http.AcceptVerbs("GET")]
        [Route("GetDocFileGETMethod")]
        [AllowAnonymous]
        public HttpResponseMessage GetDocFileGETMethod()
        {
            try
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada al metodo api/b2b/GetDocFileGETMethod");
                string docid = "";
                string docTypeId = "";
                bool convertToPDf = false;
                long userId = 0;

                Uri myUri = new Uri(Request.RequestUri.AbsoluteUri);
                docid = HttpUtility.ParseQueryString(myUri.Query).Get("DocId");
                docTypeId = HttpUtility.ParseQueryString(myUri.Query).Get("DocTypeId");
                convertToPDf = Boolean.Parse(HttpUtility.ParseQueryString(myUri.Query).Get("ConvertToPDF"));
                userId = long.Parse(HttpUtility.ParseQueryString(myUri.Query).Get("UserId"));

                AccountController AC = new AccountController();
                AC.LoginById(userId);

                IResult res = new SResult().GetResult(long.Parse(docid), long.Parse(docTypeId), false);
                string newPDFFile = res.FullPath.Replace(".pdf", "") + ".pdf";

                byte[] File = GetBytesFromDocument(userId.ToString(), docTypeId, docid, ref convertToPDf, res, false, ref newPDFFile);

                if (File == null || File.Length == 0)
                {
                    return returnFileNotFound();
                }

                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);
                responseMessage.Content = new ByteArrayContent(File);
                responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

                if ((res.FullPath != null && res.FullPath.ToLower().EndsWith("pdf")) || convertToPDf == true)
                {
                    responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                    if (!convertToPDf)
                        responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last();
                    else
                        responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last() + ".pdf";
                }
                else
                {
                    responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(res.MimeType);
                    responseMessage.Content.Headers.ContentDisposition.FileName = res.Name + "." + res.Doc_File.Split('.').Last();
                }
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "fin de llamada al metodo api/b2b/GetDocFileGETMethod");
                return responseMessage;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                throw ex;
            }
        }
        private static byte[] GetFilesBytesFromDB(IResult res, SZOptBusiness Zopt, SResult sResult, long userID, ref bool IsBlob)
        {
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "llamada al metodo GetFilesBytesFromDB en b2bController");
            byte[] _file;
            if (res.Disk_Group_Id > 0 && (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase || (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))
            {
                sResult.LoadFileFromDB(ref res);
            }

            //Verifica si el result contiene el documento guardado
            if (res.EncodedFile != null)
            {
                _file = res.EncodedFile;
            }
            else
            {
                //Verifica si debe utilizar el webservice para obtener el documento
                string sUseWebService = Zopt.GetValue("UseWebService");
                if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                    _file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, userID);
                else
                    _file = sResult.GetFileFromResultForWeb(res, out IsBlob);
            }
            return _file;
        }

        private HttpResponseMessage returnFileNotFound()
        {
            byte[] file;

            //TODO: Validar ruta fisica del servidor y la ruta del archivo HTML
            string msjFileNotFound = "Views/CustomErrorPages/FileNotFound.html";
            FileStream fs = File.Open(AppContext.BaseDirectory + msjFileNotFound, FileMode.Open, FileAccess.Read);

            file = new byte[int.Parse(fs.Length.ToString())];
            fs.Read(file, 0, file.Length);
            fs.Close();
            fs.Dispose();
            fs = null;

            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.NotFound);
            MediaTypeHeaderValue contentTye = new MediaTypeHeaderValue("text/html");

            responseMessage.Content = new ByteArrayContent(file);
            responseMessage.Content.Headers.ContentType = contentTye;
            responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            responseMessage.Content.Headers.ContentDisposition.FileName = "FileNotFound.html";

            return responseMessage;
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