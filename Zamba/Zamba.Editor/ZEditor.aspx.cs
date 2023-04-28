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

            byte[] strData;
            Zamba.Core.ConsumeServiceRestApi consumeServiceRestApi = new Zamba.Core.ConsumeServiceRestApi();
            bool convertToPDf = false;
            String newPDFFile = string.Empty;
            GetResult();
            Zamba.Core.DocumentData Document = consumeServiceRestApi.GetDocumentData(UserId, docTypeId.ToString(), docId.ToString(), convertToPDf, false, false, newPDFFile);
            strData = Document.data;
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicio de zamba.editor");
            String convertedContent = "";
            convertedContent = new DocxToHtmlConverter().ConvertDocxToHtml(strData);
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