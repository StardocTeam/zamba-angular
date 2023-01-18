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

public partial class Controls_Insert_Forms_NewFormSelector : System.Web.UI.UserControl
{
    #region Fields
    public delegate void NewFormSelected(Int64 Docid, Int64 doctypeid);
    public delegate void ErrorOcurred();

    //Declaro una variable del delegado
    private NewFormSelected dNewFormSelected = null;
    private ErrorOcurred dErrorOcurred = null;

    public event NewFormSelected OnNewDocumentSelected
    {
        add
        {
            this.dNewFormSelected += value;
        }
        remove
        {
            this.dNewFormSelected -= value;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadData();
        }
    }
    private void LoadData()

    {

        cmbVirtualForm.DataSource = FormBussines.GetVirtualDocumentsByRightsOfView(FormTypes.Insert, Int64.Parse(Session["UserId"].ToString()));
        cmbVirtualForm.DataTextField = "Name";
        cmbVirtualForm.DataValueField = "ID";
        cmbVirtualForm.DataBind();
    }
    public delegate void PreviewForm(ZwebForm Form);
    public event PreviewForm PvFmEvn;
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        ZwebForm CurrentForm = FormBussines.GetForm(Int64.Parse(cmbVirtualForm.SelectedValue));
        PvFmEvn(CurrentForm);
        //_insertedNewResult = FormBussines.CreateVirtualDocument(CurrentForm.DocTypeId);
        //if (null != dNewFormSelected)
        //    dNewFormSelected(_insertedNewResult.ID, CurrentForm.DocTypeId);
    }
}
