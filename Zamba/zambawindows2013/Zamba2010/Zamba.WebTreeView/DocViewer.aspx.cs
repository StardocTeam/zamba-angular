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

public partial class DocViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.IO.FileInfo fi;
        System.IO.FileInfo fa;

        try
        {
            string Url = Request.QueryString["fullpath"];//gvDocuments.SelectedRow.Cells[gvDocuments.SelectedRow.Cells.Count - 1].Text;

            fi = new System.IO.FileInfo(Url);
            fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + fi.Name);
            try
            {
                if (fa.Exists == true)
                {
                    fa.Delete();
                }
                if (fa.Directory.Exists == false)
                    fa.Directory.Create();
                System.IO.File.Copy(fi.FullName, fa.FullName);
            }
            catch (Exception ex)
            {
                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            }
            Response.Redirect(".\\temp\\" + fi.Name);
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
        finally
        {
            fi = null;
            fa = null;
        }
    }
    /// <summary>
    /// Escribe un log con el error ocurrido en el servidor
    /// </summary>
    /// <param name="message"></param>
    private void writeLog(String message)
    {
        try
        {
            string path = System.Web.HttpRuntime.AppDomainAppPath + "Exceptions\\";

            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path += "Exception ";
            path += System.DateTime.Now.ToString().Replace(".", "").Replace(":", "");
            path = path.Replace("/", "-");
            path += ".txt";

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(message);
            writer.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
