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
using Zamba.Core;

public partial class PrintDocument : System.Web.UI.Page
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

     
            WebBrowser.Attributes.Add("src", System.Web.HttpRuntime.AppDomainAppVirtualPath + "/temp/" + fi.Name);
      
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
}