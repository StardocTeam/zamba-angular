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
using Zamba.Core;


public partial class Controls_Insert_WCInsert : System.Web.UI.UserControl
{
    #region Fields
    public delegate void ViewIndexs(string DocTypeId);
    
    public delegate void InsertOfficeDocument_NewDocumentSelected(Int64 Docid, Int64 doctypeid);
    public delegate void InsertOfficeDocument_ErrorOcurred();
    public delegate void InsertFindDocument(Int64 Docid, Int64 doctypeid);

    //Declaro una variable del delegado
    private InsertOfficeDocument_NewDocumentSelected dInsertOfficeDocument_NewDocumentSelected = null;
    private InsertOfficeDocument_ErrorOcurred dInsertOfficeDocument_ErrorOcurred = null;
    private InsertFindDocument dInsertFindDocument = null;

    public event InsertOfficeDocument_NewDocumentSelected OnInsertOfficeDocument_NewDocumentSelected
    {
        add
        {
            this.dInsertOfficeDocument_NewDocumentSelected += value;
        }
        remove
        {
            this.dInsertOfficeDocument_NewDocumentSelected -= value;

        }
    }

    public event InsertOfficeDocument_ErrorOcurred OnInsertOfficeDocument_ErrorOcurred
    {
        add
        {
            this.dInsertOfficeDocument_ErrorOcurred += value;
        }
        remove
        {
            this.dInsertOfficeDocument_ErrorOcurred -= value;

        }
    }
    public event InsertFindDocument OnAddDoc
    {
        add
        {
            this.dInsertFindDocument += value;
        }
        remove
        {
            this.dInsertFindDocument -= value;
        }
    }

    public Button Visualizar
    {
        get { return this.btInsertar; }
        set { this.btInsertar = value; }
    }

    public FileUpload FlBrowse
    {
        get { return this.FileUpload1; }
        set { this.FileUpload1 = value; }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (IsPostBack)
        {
           
            //OfficeSelector.OnErrorOcurred += new Controls_Insert_NewOffice_NewOfficeSelector.ErrorOcurred(OfficeSelector_OnErrorOcurred);
            //OfficeSelector.OnAdd += new Controls_Insert_NewOffice_NewOfficeSelector.NewDocumentSelected(OfficeSelector_OnAdd);
        }

    }
    //protected void OfficeSelector_OnErrorOcurred()
    //{
    //    if (null != dInsertOfficeDocument_ErrorOcurred)
    //        dInsertOfficeDocument_ErrorOcurred();
    //}

    //protected void OfficeSelector_OnAdd(string Filename)
    //{
    //    DocType dt = DocTypesBusiness.GetDocType(Int64.Parse(DropDownList1.SelectedValue));
    //    Int32 userId = Int32.Parse(Session["UserId"].ToString());
    //    NewResult result = Results_Business.GetNewNewResult(dt, userId, Filename);
    //    Zamba.Core.Results_Business.InsertDocument(ref result, false, false, false, false, false, false, false);

    //    if (null != dInsertOfficeDocument_NewDocumentSelected)
    //        dInsertOfficeDocument_NewDocumentSelected(result.ID, dt.ID);

    //    //Borra el archivo
    //    System.IO.FileInfo tempfile = new System.IO.FileInfo(Filename);
    //    tempfile.Delete();


    //}

    public DropDownList DropDownLst
    {
        get { return this.DropDownList1; }
        set { this.DropDownList1 = value; }
    }

    public event ViewIndexs OnCurrentIndexs;
}

