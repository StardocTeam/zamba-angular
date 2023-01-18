using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;
using Zamba.Services;
using Zamba.Membership;
using System.Text;
using Zamba.Data;
using ZambaWeb.RestApi.Models;
using Zamba.Servers;
using Newtonsoft.Json;


namespace Zamba.Editor
{
    public partial class Default : System.Web.UI.Page
    {
        Int64 docId;
        Int64 docTypeId;
        Int64 taskid;
        String token;
        Int64 UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                SetInitialContent();
            }
            else
            {
                if (Request.Params["__EVENTTARGET"] == "ctl00$ContentPlaceHolder1$BtnExportToDOCXLink")
                    BtnExportToDocx_Click(sender, e);
            }

        }

        private void SetInitialContent()
        {
            Result res = GetResult();
            bool convertToPDf = false;
            String newPDFFile = string.Empty;
            var StrBase64 = GetDocumentData(UserId.ToString(), docTypeId.ToString(), docId.ToString(), ref convertToPDf, res, false, false, ref newPDFFile);
            byte[] strData = System.Convert.FromBase64String(StrBase64);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicio de zamba.editor");
            String convertedContent = "";
            if (res.IsWord)
            {
                convertedContent = new DocxToHtmlConverter().ConvertDocxToHtml(strData);
            }
            if (!String.IsNullOrEmpty(convertedContent))
                RadEditor1.Content = convertedContent;
        }

        protected void BtnExportToDocx_Click(object sender, EventArgs e)
        {
            try
            {
                IResult res = GetResult();
                Results_Business rb = new Results_Business();
                Zamba.Data.Transaction tr = new Transaction();
                Byte[] contentFile;
                TelerikDocxExportTemplate telerikDocxExportTemplate = new TelerikDocxExportTemplate(RadEditor1);
                contentFile = telerikDocxExportTemplate.DocxToArrayBytes();
                String TempPath = CopyFileBKP(contentFile, res.Doc_File);
                var ZEditorUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ZEditorOfficeTemp"];
                rb.ReplaceDocumentForZeditor(ref res, TempPath, false, tr, ZEditorUrl);
                tr.Commit();

                string script = " document.addEventListener('DOMContentLoaded', function(event) { SaveOK(); });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveOK", script, true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                string script = " document.addEventListener('DOMContentLoaded', function(event) { SaveError(); });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveError", script, true);
            }
        }
        public string GetDocumentData(string userId, string doctypeId, string docId, ref bool convertToPDf, IResult res, bool MsgPreview, bool includeAttachs, ref string newPDFFile)
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
                        sResult.LoadFileFromDB( ref res);
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




        private static byte[] GetFilesBytesFromDB(IResult res, SZOptBusiness Zopt, SResult sResult, long userID, ref bool IsBlob)
        {
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


        Result GetResult()
        {
            docId = Convert.ToInt64(Request.Params["docid"].ToString());
            token = Request.Params["t"].ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["DocType"]))
            {
                docTypeId = Convert.ToInt64(Request.QueryString["DocType"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["doctype"]))
            {
                docTypeId = Convert.ToInt64(Request.QueryString["doctype"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["taskid"]))
            {
                taskid = Convert.ToInt64(Request.QueryString["taskid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                UserId = Convert.ToInt64(Request.QueryString["user"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                UserId = Convert.ToInt64(Request.QueryString["userid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
            {
                UserId = Convert.ToInt64(Request.QueryString["userId"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["u"]))
            {
                UserId = Convert.ToInt64(Request.QueryString["u"]);
            }
            UserBusiness ub = new UserBusiness();
            ZCore ZC = new ZCore();

            if (Zamba.Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.ZEditor");

            ZC.VerifyFileServer();
            ub.ValidateLogIn(UserId, ClientType.Web);
            Results_Business rb = new Results_Business();
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Result Data");
            Result res = (Result)rb.GetResult(docId, docTypeId, true);
            return res;
        }


        private String CopyFileBKP(Byte[] ArrayBytes, String filename)
        {
            String pathTemp = String.Empty;
            ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format(@"Nuevo Archivo: {0}", filename));
            if (!Directory.Exists(Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp")))
            {
                Directory.CreateDirectory(Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp"));
            }
            pathTemp = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Temp", filename);
            File.WriteAllBytes(pathTemp, ArrayBytes);
            return pathTemp;
        }




    }


}