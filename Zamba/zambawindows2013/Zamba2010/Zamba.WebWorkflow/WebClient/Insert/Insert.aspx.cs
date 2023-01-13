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
using System.Collections.Generic;

public partial class WebClient_Insert_Insert : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        UCInsertFindDocument.DropDownLst.SelectedIndexChanged += SelectedIndexChanged;
        UCInsertNewOffice.DropDownLst.SelectedIndexChanged += SelectedIndexChanged;
        UCInsertNewOffice.PreviewDoc += viewoniframe;
        InsertVirtualForm.PvFmEvn += ShowVForm;
        UCInsertFindDocument.Visualizar.Click += new EventHandler(btVisulizar_Click);
        this.UCInsertFindDocument.DropDownLst.PreRender += new EventHandler(DropDownLst_PreRender);
    }

            /// <summary>
    /// Devuelve el Id de usuario logeado
    /// </summary>
    private Int64? UserId
    {
        get
        {
            Int64? Id = null;

            if (null != Session["UserId"])
            {
                Int64 TryValue;
                if (Int64.TryParse(Session["UserId"].ToString(), out TryValue))
                    Id = TryValue;
            }

            return Id;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack == true)
        {
            if (!UserId.HasValue)
                FormsAuthentication.RedirectToLoginPage();
        }
        if (this.IndexsControl.CurrentIndexs.Length != 0)
            Session["PostBackGetInfoAux"] = this.IndexsControl.GetIndexs();
        if (this.DocTypesIds.Count > 0)
            LoadIndexs(this.DocTypesIds[0]);
        if (Request.QueryString["DocId"] != null && Request.QueryString["DocTypeId"] != null && !Page.IsPostBack)
        {
            ViewState["DocTypeID"] = Request.QueryString["DocTypeId"];
            this.IndexsControl.MostrarIndices(Results_Business.GetResult(Convert.ToInt64(Request.QueryString["DocId"].ToString()), Convert.ToInt64(Request.QueryString["DocTypeId"])).ID);
        }
    }
   

    private void DropDownLst_PreRender(object sender, EventArgs e)
    {
        if (ViewState["DocTypeID"] != null)
        {
            this.UCInsertFindDocument.DropDownLst.SelectedIndex = this.UCInsertFindDocument.DropDownLst.Items.IndexOf(this.UCInsertFindDocument.DropDownLst.Items.FindByValue(ViewState["DocTypeID"].ToString()));
            this.DocTypesIds = new List<int> { Convert.ToInt32(ViewState["DocTypeID"].ToString()) };
            ViewState["DocTypeID"] = null;
            Session["SaveDocType"] = ((DropDownList)sender).SelectedValue;
        }
        else if (!Page.IsPostBack)
        {
            if (this.DocTypesIds.Count > 0)
                this.UCInsertFindDocument.DropDownLst.SelectedIndex = this.UCInsertFindDocument.DropDownLst.Items.IndexOf(this.UCInsertFindDocument.DropDownLst.Items.FindByValue(this.DocTypesIds[0].ToString()));
            this.DocTypesIds = new List<int> { Convert.ToInt32(((DropDownList)sender).SelectedValue) };
            Zamba.Core.Index[] lista = GetindexSchemaNew(DocTypesIds);
            Session["ShowIndexOfInsert"] = lista;
            LoadIndexs(Convert.ToInt32(((DropDownList)sender).SelectedValue));
            Session["ShowIndexOfInsert"] = null;
            Session["SaveDocType"] = ((DropDownList)sender).SelectedValue;
        }
    }

    private void viewoniframe(string path,byte Type)
    {
        string tmppath = null;
        switch (Type)
        {
            case 0:
                tmppath = "/temp/";
                break;
            case 1:
                tmppath = "/OfficeTemp/";
                break;
        }
        View.Attributes.Add("src", System.Web.HttpRuntime.AppDomainAppVirtualPath + tmppath + new System.IO.FileInfo(path).Name);
        ViewState["FileDocSave"] = path;
        UpdatePanel2.Update();
    }

    private void ShowVForm(ZwebForm Form)
    {
        View.Attributes.Add("src", Form.Path);
        Session["SaveDocType"] = Form;
        UpdatePanel2.Update();
        UpdatePanel1.Update();
    }
    public System.Collections.Generic.List<Int32> DocTypesIds
    {
        get
        {
            if (Session["DocTypesIdsInsert"] != null)
                return (System.Collections.Generic.List<Int32>)Session["DocTypesIdsInsert"];
            else
                return new System.Collections.Generic.List<Int32>();
        }
        set
        {
            Session["DocTypesIdsInsert"] = value;
        }
    }

    protected Zamba.Core.Index[] GetindexSchemaNew(System.Collections.Generic.List<int> DocTypesIds)
    {
        ArrayList ar = new ArrayList();
        ar.AddRange(DocTypesIds);
        return Zamba.Core.ZCore.FilterSearchIndex(ar);
    }

    public void SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadIndexs(Convert.ToInt32(((DropDownList)sender).SelectedValue));
        Session["SaveDocType"] = ((DropDownList)sender).SelectedValue;
        UpdatePanel1.Update();
        //this.DocTypesIds = new List<int> { Convert.ToInt32(((DropDownList)sender).SelectedValue) };
        //Zamba.Core.Index[] lista = GetindexSchemaNew(DocTypesIds);
        //object aux = Session["CurrentIndexs"];
        //Session["CurrentIndexs"] = lista;
        //Session["ShowIndexOfInsert"] = lista;
        //LoadIndexs(Convert.ToInt32(((DropDownList)sender).SelectedValue));
        //Session["ShowIndexOfInsert"] = null;
        //Session["CurrentIndexs"] = aux;
    }

    private void LoadIndexs(int Id)
    {
        try
        {
            this.DocTypesIds = new List<int> { Id };
            Zamba.Core.Index[] Indexs = GetindexSchemaNew(DocTypesIds);
            this.IndexsControl.CurrentIndexs = Indexs;
            Session["ShowIndexOfInsert"] = Indexs;
            List<Index> IndexList = Indexs.ToList();
            this.IndexsControl.ShowIndexsWithValues(IndexList.ToArray());
            Session["ShowIndexOfInsert"] = null;
        }
        catch (Exception ex)
        {

            ZClass.raiseerror(ex);
        }

    }

    protected void SaveInsert_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SaveDocType"] != null && ViewState["FileDocSave"] != null)
            {
                if (Session["SaveDocType"] is ZwebForm)
                {
                    IResult _insertedNewResult;
                    _insertedNewResult = FormBussines.CreateVirtualDocument(((ZwebForm)Session["SaveDocType"]).DocTypeId);
                }
                else
                {
                    DocType dt = null;
                    try
                    {
                        dt = DocTypesBusiness.GetDocType(Convert.ToInt64(Session["SaveDocType"]));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Int32 userId = Int32.MinValue;
                    if (Session["UserId"] != null)
                        userId = Int32.Parse(Session["UserId"].ToString());
                    else
                        throw new ArgumentException("El UserId debe ser distinto de nulo");

                    NewResult result = null;
                    //Agrega el archivo a zamba
                    try
                    {
                        result = Results_Business.GetNewNewResult(dt, userId, ViewState["FileDocSave"].ToString());
                        result.Indexs = ArrayList.Adapter(this.IndexsControl.GetIndexs());
                        if (Request.QueryString["DocId"] != null && Request.QueryString["DocTypeId"] != null)
                            result.FolderId = Results_Business.GetResult(Convert.ToInt64(Request.QueryString["DocId"].ToString()), Convert.ToInt64(Request.QueryString["DocTypeId"].ToString())).FolderId;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    result.OriginalName = Session["FileOriginalDocSave"].ToString();

                    //Agrega el archivo a zamba

                    //Luego eliminar el archivo
                    try
                    {
                        Zamba.Core.Results_Business.InsertDocument(ref result, false, false, false, false, false, false, false);
                        System.IO.File.Delete(ViewState["FileDocSave"].ToString());
                        View.Attributes.Add("src", "");
                    }
                    catch (System.IO.IOException ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void btVisulizar_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            try
            {
                if (UCInsertFindDocument.FlBrowse.HasFile || (System.IO.File.Exists(UCInsertFindDocument.FlBrowse.PostedFile.FileName) && UCInsertFindDocument.FlBrowse.FileBytes.Length == 0))
                {
                    string pathTemp = Server.MapPath("~/temp");
                    string ext = string.Empty;
                    string file = string.Empty;
                    string originalName = string.Empty;
                    //Genera el nombre del archivo
                    ext = System.IO.Path.GetExtension(UCInsertFindDocument.FlBrowse.FileName);
                    file = System.IO.Path.Combine(pathTemp, Guid.NewGuid().ToString() + ext);
                    originalName = System.IO.Path.GetFileName(UCInsertFindDocument.FlBrowse.FileName);
                    //Sube el archivo
                    try
                    {
                        UCInsertFindDocument.FlBrowse.SaveAs(file);
                        Session["FileOriginalDocSave"] = originalName;
                        viewoniframe(file, 0);
                    }
                    catch (System.IO.IOException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}
