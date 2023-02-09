using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
//using Zamba.Core.Enumerators;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Zamba.Core;

namespace Zamba.Web.Services
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>

    public class FileUpload : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        /// <summary>
        /// Esta funcion inserta todos los archivos pasados al DropZone.
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var tempPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio de Trace Dropzone");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "temPath " + tempPath);
            //context.Response.ContentType = "text/plain";
            #region Para remover, llamado desde AJAX
            if (context.Request["remove"] == "true")
            {
                var cFile = context.Request["file"];
                if (cFile != null && cFile.ToString() != string.Empty)
                {
                    var current = (List<string>)context.Session["Insert_UploadedFile"];
                    string DeleteDate = string.Empty;
                    foreach (string fi in current)
                    {
                        var NameFi = fi.Replace(tempPath, "");
                        if (NameFi == cFile.ToString())
                        {
                            DeleteDate = fi;
                            if (File.Exists(tempPath + cFile.ToString()))
                            {
                                File.Delete(tempPath + cFile.ToString());
                            }
                        }
                    }
                    if (DeleteDate != "")
                    {
                        current.Remove(DeleteDate);
                        context.Session["Insert_UploadedFile"] = current;
                        DeleteDate = string.Empty;
                    }
                }
            }
            #endregion
            else
            {
                string fileName = "";
                if (context.Request.Files.Count > 0)
                {
                    try
                    {
                        List<string> FileNames = new List<string>();
                        List<string> FileNamesOnly = new List<string>();

                        foreach (string s in context.Request.Files)//Itera de a uno asincronico
                        {
                            HttpPostedFile file = context.Request.Files[s];
                            string fileExtension = file.ContentType;
                                fileName = file.FileName;
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo que se insertara en dropzone " + fileName);
                            if (context.Session["Insert_UploadedFile"] == null)
                                context.Session["Insert_UploadedFile"] = new List<string>();

                            var current = (List<string>)context.Session["Insert_UploadedFile"];
                            if (current.FirstOrDefault(x => x == fileName.ToString()) == null)
                            {
                                current.Add(tempPath + fileName);
                                context.Session["Insert_UploadedFile"] = current;

                                //if (FileNames.FirstOrDefault(x => x == fileName.ToString()) == null)
                                //{
                                //    FileNames.Add(tempPath + fileName);

                                if (Directory.Exists(tempPath) == false)
                                    Directory.CreateDirectory(tempPath);

                                file.SaveAs(tempPath + fileName);
                                FileNames.Add(tempPath + fileName);
                                FileNamesOnly.Add(fileName);
                            }
                        }

                        var qs = context.Request.Form;
                        if (qs["isNGform"] == "true")
                        {
                            var UploadParentEntityId = Int64.Parse(qs["UploadParentEntityId"]);
                            var UploadParentDocId = Int64.Parse(qs["UploadParentDocId"]);
                            var UploadNewEntityId = Int64.Parse(qs["UploadNewEntityId"]);

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "UploadParentEntityId " + UploadParentEntityId);
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "UploadParentDocId " + UploadParentDocId);
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "UploadNewEntityId " + UploadNewEntityId);

                            var DFWS = new DropFiles();
                            DropFiles.insertResultDto IR = DFWS.InsertDoc(UploadParentEntityId, UploadParentDocId, UploadNewEntityId, FileNames);
                            // var resultstr = JsonConvert.SerializeObject(IR);
                            string resultstr;
                            if (IR.newDocIds.Count == 0)
                            {
                                resultstr = "[{\"insertResults\":\"" + IR.insertResult + "\",\"DocIds\":\"[" + IR.newDocIds + "]\"}]";
                            }
                            else
                            {
                                resultstr = "[{\"insertResults\":\"" + IR.insertResult + "\",\"DocIds\":\"" + IR.newDocIds[0] + "\"}]";
                            }
                            context.Response.Write(
                            resultstr
                            );
                        }
                        else
                        {
                            string resultstr;
                            resultstr = "[{\"tempFiles\":\"" + string.Join(",", FileNamesOnly) + "\"}]";
                            context.Response.Write(resultstr);
                        }
                    }
                    catch (Exception ex)
                    {
                        Zamba.AppBlock.ZException.Log(ex);
                    }
                }
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
}