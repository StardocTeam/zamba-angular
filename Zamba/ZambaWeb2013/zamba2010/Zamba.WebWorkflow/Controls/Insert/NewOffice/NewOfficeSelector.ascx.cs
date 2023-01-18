using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Zamba.Core;
public partial class Controls_Insert_NewOffice_NewOfficeSelector : System.Web.UI.UserControl
{
    #region Fields - Propertys
    private string TEMP_DIRECTORY;
    private string NEWFILE_DIRECTORY;
    public delegate void NewDocumentSelected(Int64 docid,Int64 doctypeid);
    public delegate void ErrorOcurred();

    //Declaro una variable del delegado
    private NewDocumentSelected dAdd = null;
    private ErrorOcurred dErrorOcurred = null;
    public delegate void PrevDoc(string path,byte Type);
    public event PrevDoc PreviewDoc;

    public DropDownList DropDownLst
    {
        get { return this.DropDownList1; }
        set { this.DropDownList1 = value; }
    }

    public event NewDocumentSelected OnAdd
    {
        add
        {
            this.dAdd += value;
        }
        remove
        {
            this.dAdd -= value;

        }
    }

    public event ErrorOcurred OnErrorOcurred
    {
        add
        {
            this.dErrorOcurred += value;
        }
        remove
        {
            this.dErrorOcurred -= value;

        }
    }

    #endregion
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        TEMP_DIRECTORY = Server.MapPath("~") + "\\OfficeTemp\\";
        NEWFILE_DIRECTORY = Server.MapPath("~") + "\\Controls\\Insert\\NewOffice\\";
    }
    protected void lnkNewWord_Click(object sender, EventArgs e)
    {
        CopyNewFile("WORD");
    }
    protected void LnkNewExcel_Click(object sender, EventArgs e)
    {
        CopyNewFile("EXCEL");
    }
    protected void LnkNewPowerPoint_Click(object sender, EventArgs e)
    {
        CopyNewFile("POWER");
    }
    #endregion
    #region Methods
    private void CopyNewFile(string Selection)
    {
        FileInfo file;
        string filename;
        try
        {
            filename = GetAvaliableTempName(Selection);
            switch (Selection)
            {
                case "WORD":
                    file = new FileInfo(NEWFILE_DIRECTORY + "New1.doc");
                    file.CopyTo(filename);
                    break;
                case "EXCEL":
                    file = new FileInfo(NEWFILE_DIRECTORY + "New1.xls");
                    file.CopyTo(filename);

                    break;
                case "POWER":
                    file = new FileInfo(NEWFILE_DIRECTORY + "New1.ppt");
                    file.CopyTo(filename);
                    break;
            }
            Session["FileOriginalDocSave"] = new System.IO.FileInfo(filename).Name;
            PreviewDoc(filename,1);
            //DocType dt = DocTypesBusiness.GetDocType(Int64.Parse(DropDownList1.SelectedValue));

            //Int32 userId = Int32.Parse(Session["UserId"].ToString());
            //NewResult result = Results_Business.GetNewNewResult(dt, userId, filename);
            //Zamba.Core.Results_Business.InsertDocument(ref result, false, false, false, false, false, false, false);
            //if (this.dAdd != null)
            //    dAdd(result.ID, dt.ID);
            
            ////Borra el archivo
            //System.IO.FileInfo tempfile = new System.IO.FileInfo(filename);
            //tempfile.Delete();

         }
        catch
        {
            if (this.dErrorOcurred != null)
                dErrorOcurred();
        }
        finally
        {
            file = null;
            GC.Collect();
        }

    }
    private string GetAvaliableTempName(string Selection)
    {
        Int64 i = 0;
        string filename;
        string extension = string.Empty;

        switch (Selection)
        {
            case "WORD":
                extension = ".doc";
                break;
            case "EXCEL":
                extension = ".xls";
                break;
            case "POWER":
                extension = ".ppt";
                break;
        }

        do
        {
            filename = TEMP_DIRECTORY + Selection + i.ToString() + extension;
            if (!File.Exists(filename))
            {
                return filename;
            }
            i += 1;
        } while (i < 99999);
        return string.Empty;

    }
    #endregion



}