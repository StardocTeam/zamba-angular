using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Zamba.WebActiveX;
using Zamba.Core;


public partial class DocViewer : System.Web.UI.Page
{
    private string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.IO.FileInfo fi;
        System.IO.FileInfo fa;

        try
        {
            url = Request.QueryString["fullpath"];//gvDocuments.SelectedRow.Cells[gvDocuments.SelectedRow.Cells.Count - 1].Text;

            fi = new System.IO.FileInfo(url);
            fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + fi.Name);
            try
            {
                if (fa.Exists == true)
                {
                    if (fa.IsReadOnly == true)
                        fa.IsReadOnly = false;

                    fa.Delete();
                }
                if (fa.Directory.Exists == false)
                    fa.Directory.Create();
                System.IO.File.Copy(fi.FullName, fa.FullName);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            //if (fi.Name.ToLower().EndsWith(".doc") || fi.Name.ToLower().EndsWith(".docx") || fi.Name.ToLower().EndsWith(".rtf"))
            //{
            //    ActiveXObject Viewer = new ActiveXObject();
            //    Viewer.OnClosedActivex += new ClosedActivex(closeActivex);
            //    Viewer.File = fa.FullName;
            //    Viewer.Open();
            //}
            //else
            WebBrowser.Attributes.Add("src", System.Web.HttpRuntime.AppDomainAppVirtualPath + "/temp/" + fi.Name);
            //Configura el control de indices
            string docid = string.Empty;
            Int64 res = Int64.MinValue;
            if (Request.QueryString["docid"] != null)
            {
                docid = Request.QueryString["docid"] as string;
                if (Int64.TryParse(docid, out res))
                    WCIndexs1.docId = res;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            fi = null;
            fa = null;
        }
    }

    private void closeActivex()
    {
            try
            {
                System.IO.File.Copy(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + url.Remove(0, url.LastIndexOf("\\")), url,true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
    }
}