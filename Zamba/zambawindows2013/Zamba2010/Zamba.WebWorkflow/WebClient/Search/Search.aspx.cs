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

public partial class WebClient_Search_Search : System.Web.UI.Page
{
    public System.Collections.Generic.List<Int32> DocTypesIds
    {
        get
        {
            if (ViewState["DocTypesIds"] != null)
                return (System.Collections.Generic.List<Int32>)ViewState["DocTypesIds"];
            else
                return new System.Collections.Generic.List<Int32>();
        }
        set
        {
            ViewState["DocTypesIds"] = value;
        }
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
        this.Form.DefaultButton = btnSearch.UniqueID;
        this.Form.DefaultFocus = btnSearch.UniqueID;
    

        if (!Page.IsPostBack ==true)
            {
                if (!UserId.HasValue)
                    FormsAuthentication.RedirectToLoginPage();
        }
        this.DocTypesControl.OnDocTypesSelected += new DocTypesSelected(this.DocTypesSelected1);
    
    }
    protected void DocTypesSelected1(System.Collections.Generic.List<int> DocTypesIds)
    {
        if (Page.IsCallback)
        {
        }
        ShowIndexs(DocTypesIds);
    }

    protected Zamba.Core.Index[] GetindexSchemaNew(System.Collections.Generic.List<int> DocTypesIds)
    {
        ArrayList ar = new ArrayList();
        ar.AddRange(DocTypesIds);
        return Zamba.Core.ZCore.FilterSearchIndex(ar);
    }

    /// <summary>
    /// </summary>    
    /// <param name="Indexs">Lista de indices</param>    
    /// <history>
    ///     [Ezequiel]  21/01/2009  Modified - Se modifico para que borre los indices al no tener ningun documento seleccionado.
    /// </history>
    protected void ShowIndexs(System.Collections.Generic.List<int> DocTypesIds)
    {
        try
        {
            this.DocTypesIds = DocTypesIds;
            Zamba.Core.Index[] Indexs = GetindexSchemaNew(DocTypesIds);
            List<Index> IndexList = Indexs.ToList();
            //IndexList.Sort(new IndexComparer());
            if (!this.DocTypesControl.GotSelectedIndexs())
            {
                IndexList = new List<Index>();
                Session["SelectedsDocTypesIds"] = new System.Collections.Generic.List<int>();
            }
            this.IndexsControl.ShowIndexs(IndexList.ToArray());
        }
        catch (Exception ex)
        {
            
        ZClass.raiseerror(ex);
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        NewIndexSearch();
    }

    /// <summary>
    /// Evento que se ejecuta tras presionar el botón "Limpiar índices"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  26/03/2009  Created      
    /// </history>
    protected void btnClearIndexs_Click(object sender, EventArgs e)
    {
        if (DocTypesIds.Count > 0)
        {
            // Se crean de vuelta los índices
            ShowIndexs(DocTypesIds);
        }

        // Se vacían las tres cajas de texto principales de búsqueda
        this.txtTextSearch.Text = string.Empty;
        this.txtSearchNotesAndForum.Text = string.Empty;
        this.txtSearchDocumentary.Text = string.Empty;

        // Si se encuentra visible el mensaje "No se encontraron resultados"
        if (this.NoResults.Visible == true)
        {
            // Entonces se oculta
            this.NoResults.Visible = false;
        }
    }

    public Zamba.Core.IUser Currentuser
    {
        get { return (Zamba.Core.IUser)Session["CurrentUser"]; }
    }

    public ArrayList Results
    {
        get
        {
            if (ViewState["Results"] != null)
                return (ArrayList)ViewState["Results"];
            else
            {
                ArrayList mresults = new ArrayList();
                ViewState["Results"] = mresults;
                return mresults;
            }
        }
        set
        {
            ViewState["Results"] = value;
        }
    }

    /// <summary>
    /// Método que sirve para buscar documentos en base a un tipo de documento
    /// </summary>
    /// <history>
    ///     [Gaston]  06/01/2009  Modified      Se agrego la búsqueda por contenido (los tres textboxs)
    /// </history>
    protected void NewIndexSearch()
    {        

        lblMessage.Visible = false;

        try
        {
            //22-08-08 Comentado por osanchez. Verificar que IndexsControl contenga el metodo DevolverValores.
            //System.Collections.Generic.List<Zamba.Core.Index> Indexs = new System.Collections.Generic.List<Zamba.Core.Index>();
            //Indexs = this.IndexsControl.DevolverValores();

            System.Collections.Generic.List<Zamba.Core.DocType> DocTypes = new System.Collections.Generic.List<Zamba.Core.DocType>();

            foreach (Int32 DocTypeId in this.DocTypesIds)
            {
                DocTypes.Add(Zamba.Core.DocTypesBusiness.GetDocType(DocTypeId));
            }

            Zamba.Core.DocType[] arDocTypes = DocTypes.ToArray<Zamba.Core.DocType>();
            //    Zamba.Core.Index[] arIndexs = Indexs.ToArray<Zamba.Core.Index>();


            
            Zamba.Core.Searchs.Search Search = new Zamba.Core.Searchs.Search(this.IndexsControl.MakeSearchIndexsList(), txtTextSearch.Text, true, txtSearchNotesAndForum.Text, "", 0, arDocTypes, true, "");
            this.Results = Zamba.Core.ModDocuments.DoSearch(Search, Currentuser, true);
            if (Results.Count > 0)
            {
                System.Collections.Generic.List<Zamba.Core.IResult> ResultList = new System.Collections.Generic.List<Zamba.Core.IResult>();
                foreach (Zamba.Core.Result R in this.Results)
                {
                    ResultList.Add(R);
                }
                Session["CurrentResults"] = ResultList;
                Response.Redirect("~/WebClient/Results/Results.aspx");
                //Server.Transfer("~/WebClient/Results/Results.aspx", true);                               
            }
            else
                NoResults.Visible = true;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
       
    }
   
    private class IndexComparer
     : IComparer<Index>
    {
        public int Compare(Index x, Index y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (NoResults.Visible == true)
            NoResults.Visible = false;
    }
}
