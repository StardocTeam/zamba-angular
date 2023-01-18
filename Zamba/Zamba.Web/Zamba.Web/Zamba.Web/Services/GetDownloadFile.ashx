<%@ WebHandler Language="C#" Class="GetDownloadFile" %>

using System;
using System.Web;
using System.IO;
using Zamba.Core;
using Zamba.Services;
using System.Linq;
public class GetDownloadFile : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            long DocTypeId = 0;
            long DocId = 0;
            Int64 UserID = 0;
            Boolean ConvertToPDf = true;

            if (context.Request.QueryString["DocTypeId"].ToString() != null && context.Request.QueryString["DocId"].ToString() != null && context.Request.QueryString["UserID"].ToString() != null && context.Request.QueryString["DocTypeId"].ToString() != "undefined" && context.Request.QueryString["DocId"].ToString() != "undefined" && context.Request.QueryString["UserID"].ToString() != "undefined")

            {
                DocTypeId = long.Parse(context.Request.QueryString["DocTypeId"].ToString());
                DocId = long.Parse(context.Request.QueryString["DocId"].ToString());
                try
                {
                    UserID = Int64.Parse(context.Request.QueryString["UserID"].ToString());
                    if (Zamba.Membership.MembershipHelper.CurrentUser == null)
                    {
                        UserBusiness UB = new UserBusiness();
                        UB.ValidateLogIn(UserID, ClientType.Web);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                if (context.Request.QueryString["ConvertToPDf"] != null)
                {
                    ConvertToPDf = Boolean.Parse(context.Request.QueryString["ConvertToPDf"].ToString());
                }

            }
            else
                throw new ArgumentException("No parameter specified");

            SResult sResult = new SResult();
            IResult res = (Result)sResult.GetResult(DocId, DocTypeId,false);
            bool fileNotFound = false;

            if (res != null && res.FullPath != null && res.FullPath.Contains("."))
            {



                byte[] file = null;
                Boolean IsBlob = false;

                string filename = res.FullPath;
                string newPDFFile = res.FullPath + ".pdf";


                if (ConvertToPDf && File.Exists(newPDFFile) && res.IsMsg == false && res.IsOffice2 == false)
                {
                    file = FileEncode.Encode(newPDFFile);
                    filename = newPDFFile;
                }

                if (file == null || file.Length == 0)
                {
                    if (ConvertToPDf == false)
                    { filename = res.FullPath; }

                    if (res.IsMsg)
                    {
                        if (File.Exists(filename))
                        {
                        
                        //filename = filename.Replace(".msg", ".html");
                        //res.Doc_File = Path.GetFileName(res.FullPath).ToLower().Replace(".msg", ".html");
                        filename = res.FullPath;
                        res.Doc_File = Path.GetFileName(res.FullPath).ToLower();

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
                        file = res.EncodedFile;
                    }
                    else
                    {
                        string sUseWebService = Zopt.GetValue("UseWebService");
                        //Verifica si debe utilizar el webservice para obtener el documento
                        if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                            file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, UserID);
                        else
                            file = sResult.GetFileFromResultForWeb(res, out IsBlob);
                    }


                    if (file != null && file.Length > 0)
                    {
                        if (res.IsWord)
                        {
                            if (ConvertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsHTML || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (ConvertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (ConvertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }

                            }

                        }

                        if ((res.IsImage) && res.IsTif == false)
                        {
                            if (ConvertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                {
                                    file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsTif))
                        {
                            String TempDir = ZOptBusiness.GetValueOrDefault("PdfsDirectory", Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Pdfs"));
                            TempDir = Path.Combine(TempDir, res.DocTypeId.ToString());

                            try
                            {
                                if (Directory.Exists(TempDir) == false)
                                {
                                    Directory.CreateDirectory(TempDir);
                                }
                            }
                            catch (Exception)
                            {
                                TempDir = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Pdfs", res.DocTypeId.ToString());
                            }

                            if (ConvertToPDf == false)
                            {
                                if (res.RealFullPath().Contains("__.__"))
                                {
                                    string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                    FileEncode.Decode(tempPDFFile, file);
                                    file = FileEncode.Encode(tempPDFFile);
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
                                        FileEncode.Decode(tempPDFFile, file);

                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();

                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, file);
                                            file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
                                        }

                                    }
                                    else
                                    {
                                        file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                }
                                else
                                {
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();

                                    if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                    {
                                        file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else { filename = res.FullPath; }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (ConvertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }
                    }
                }
                string msjFileNotFound = "../Views/CustomErrorPages/FileNotFound.html";
                string msjFileDownloaded = "../Views/CustomErrorPages/FileDownloaded.html";

                //Verifica la existencia del documento buscado 
                if (!(file != null && file.Length > 0))
                {
                    if (res.IconId == 60)
                        msjFileNotFound = "../Views/CustomErrorPages/FileNotScanned.html";
                    FileStream fs = File.Open(context.Server.MapPath(msjFileNotFound), FileMode.Open, FileAccess.Read);

                    file = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(file, 0, file.Length);
                    fileNotFound = true;
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }

                Zopt = null;
                sResult = null;

                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.BufferOutput = true;

                FileInfo FI = new FileInfo((fileNotFound) ? context.Server.MapPath(msjFileNotFound) : filename);
                String FileName = res.OriginalName.Split('\\').Last();
                context.Response.AppendHeader("content-disposition", "inline; filename=" + FileName);
                if (filename != null && filename.ToLower().EndsWith("pdf"))
                    res.MimeType = "application/pdf";
                context.Response.ContentType = (fileNotFound) ? "text/html" : res.MimeType;
                context.Response.OutputStream.Write(file, 0, file.Length);
                context.Response.OutputStream.Flush();
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            LoadFileNotFound(context);
        }
    }

    //Esta función es usada para mostrar un mensaje de error en el iframe sin posibilidad de error(es la última instancia).
    //Si es necesario cargar un mensaje más descriptivo.
    private void LoadFileNotFound(HttpContext context)
    {
        try
        {
            context.Response.Write("Ha ocurrido un error al intentar obtener el documento.");
        }
        catch (Exception ex)
        {
            //Zamba.AppBlock.ZException.Log(ex, false);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



}
