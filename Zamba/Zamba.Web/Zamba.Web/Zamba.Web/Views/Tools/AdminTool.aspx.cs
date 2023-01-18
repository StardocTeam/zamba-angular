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
using System.IO;
using Zamba.Core;

public partial class Views_Tools_AdminTool : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userTools"] == null)
            Response.Redirect("../../Views/Security/Login_WebTools.aspx");

        lblMsg.Text = "Ruta destino a la cual se copiaran los archivos";
        ActiveFileBrowser(@"~/", @"~/", @"~/");
        FileExplorer1.VisibleControls = Telerik.Web.UI.FileExplorer.FileExplorerControls.All;

        FileExplorer1.EnableOpenFile = true;
        FileExplorer1.DisplayUpFolderItem = true;
        
        FileExplorer1.AllowPaging = true;
        FileExplorer1.EnableCreateNewFolder = true;
        FileExplorer1.Configuration.MaxUploadFileSize = 104857600;

        FileExplorer1.DisplayUpFolderItem = true;
        FileExplorer1.EnableCopy = true;
        FileExplorer1.EnableCreateNewFolder = true;
        FileExplorer1.EnableFilterTextBox = true;
        FileExplorer1.EnableAsyncUpload = true;
        FileExplorer1.EnableFilterTextBox = true;
        FileExplorer1.ExplorerMode = Telerik.Web.UI.FileExplorer.FileExplorerMode.Default;
        

        if (!IsPostBack)
        {
            FileExplorer1.InitialPath = Page.ResolveUrl(@"~/");
            try
            {
                ZOptBusiness zopt = new ZOptBusiness();

                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
            }
            catch { }
        }
    }

    protected void ActiveFileBrowser(string VPath,string UPath,string DelPath)
    {        
        string[] viewPaths = new string[]
        {
            VPath
        };

        string[] uploadPaths = new string[]
        {
            UPath
        };

        string[] deletePaths = new string[] 
        {
           DelPath
        };

        FileExplorer1.Configuration.ViewPaths = viewPaths;
        FileExplorer1.Configuration.UploadPaths = uploadPaths;
        FileExplorer1.Configuration.DeletePaths = deletePaths;
    }

   protected void btnUp_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (string.Compare(txtfilepath.Text, string.Empty) != 0)
            {
                try
                {
                    CopyFiles(Server.MapPath(@"~/Views/Tools/toolsup"), txtfilepath.Text);                    
                    DeleteFiles(Server.MapPath(@"~/Views/Tools/toolsup"));
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    error = true;
                }
                if (!error)
                {
                    lblMsg.Text = "Carpeta copiada con éxito.";
                }
            }
            else
                lblMsg.Text = "Ingrese ruta destino.";
        }

   /// <summary>
   /// Copia todos los archivos de la carpeta content a donde esta el formulario
   /// </summary>
   /// <param name="originalPath"></param>
   /// <param name="copyPath"></param>
   protected void CopyFiles(String originalPath, String copyPath)
   {
       foreach (String f in Directory.GetFiles(originalPath))
           if (File.Exists(copyPath + f.Remove(0, f.LastIndexOf("\\"))) == false)
               File.Copy(f, copyPath + f.Remove(0, f.LastIndexOf("\\")));

       foreach (String d in Directory.GetDirectories(originalPath))
       {
           if (Directory.Exists(copyPath + d.Remove(0, d.LastIndexOf("\\"))) == false)
           {
               Directory.CreateDirectory(copyPath + d.Remove(0, d.LastIndexOf("\\")));
           }

           CopyFiles(d, copyPath + d.Remove(0, d.LastIndexOf("\\")));
       }

   }
       
        /// <summary>
        /// Borra todos los archivos de una carpeta
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="copyPath"></param>
        protected void DeleteFiles(String originalPath)
        {
            foreach (String f in Directory.GetFiles(originalPath))
            {
                File.Delete(f);
            }

            foreach (String d in Directory.GetDirectories(originalPath))
            {
                Directory.Delete(originalPath + d.Remove(0, d.LastIndexOf("\\")), true);                
            }            
        }

}