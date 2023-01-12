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

public delegate void DocTypesSelected(System.Collections.Generic.List<int> DocTypesIds);
public partial class Controls_Core_WCDocTypesl : System.Web.UI.UserControl
{
    
    public System.Collections.Generic.List<int> SelectedsDocTypesIds
    {
        get
        {
            if (Session["SelectedsDocTypesIds"] != null)
                return (System.Collections.Generic.List<int>)Session["SelectedsDocTypesIds"];
            else

            {
                System.Collections.Generic.List<int> SDTL = new System.Collections.Generic.List<int>();
                Session["SelectedsDocTypesIds"] = SDTL;
                return (System.Collections.Generic.List<int>)Session["SelectedsDocTypesIds"];
            }
        }
        set
        {
            Session["SelectedsDocTypesIds"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        {
            if (!Page.IsPostBack ==true)
            {
            Int32 UserId = Int32.Parse(Page.Session["UserId"].ToString());
            ArrayList DSDT = Zamba.Core.DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserId,Zamba.Core.RightsType.View);
            
                this.DataList1.DataSource = DSDT;
            this.DataList1.DataBind();
            }        }
        catch (Exception ex)
        { }
        
        try {
            if (!Page.IsPostBack)
                if (SelectedsDocTypesIds.Count > 0)
                    CheckDocsSelected();
            LoadIndexs();
        }
        catch (Exception ex) { }
    }

      
            protected void Dt_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            e.Item.Attributes["onclick"] = "CheckClick";
                    }
        catch (Exception ex)
        {
        }
    }

            protected void Check(object sender, EventArgs e)
            {
                try
                {
                    if (((System.Web.UI.WebControls.CheckBox)sender).Checked == true)
                    {
                        if (SelectedsDocTypesIds.Contains(Int16.Parse(((WebControl)sender).Attributes["dtid"])) != true)
                        {
                           
                            SelectedsDocTypesIds.Add(Int16.Parse(((WebControl)sender).Attributes["dtid"]));
                        }
                                            }
                    else
                    {
                        SelectedsDocTypesIds.Remove(int.Parse(((WebControl)sender).Attributes["dtid"]));
                    }
                    }
                catch (Exception ex)
                {
                }

                LoadIndexs();
            }

    
     private DocTypesSelected dDocTypesSelected = null;

     public event DocTypesSelected OnDocTypesSelected
     {
         add
         {
             this.dDocTypesSelected += value;
         }
         remove
         {
             this.dDocTypesSelected -= value;
         }
     }
    protected void LoadIndexs()
    {
        try
        {
            Int32 ListBox1SelectedIndicesCount = this.SelectedsDocTypesIds.Count();            
            if (ListBox1SelectedIndicesCount > 0)
            {
                //Declaro una variable del delegado
                if (this.dDocTypesSelected != null)
                    dDocTypesSelected(SelectedsDocTypesIds);
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Método que sirve para marcar los documentos seleccionados al volver a la pagina de busqueda.
    /// </summary>
    /// <history>
    ///     [Ezequiel]  19/01/2009  Created
    /// </history>
    private void CheckDocsSelected()
    {
        try
        {
            foreach (Control DocTypes in this.DataList1.Controls)
            {
                if (SelectedsDocTypesIds.Contains(Convert.ToInt32(((TextBox)DocTypes.Controls[1]).Text)))
                    ((CheckBox)DocTypes.Controls[3]).Checked = true;
            }
        }
        catch
        {
        }
    }

    /// <summary>
    /// Metodo que notifica si hay indices seleccionados.
    /// </summary>
    /// <returns>Devuelve true si hay, de lo contrario false.</returns>
    /// <history>
    ///     [Ezequiel]  21/01/2009  Created
    /// </history>
    public bool GotSelectedIndexs()
    {
        bool ret = false;
        foreach (Control DocTypes in this.DataList1.Controls)
        {
            if (((CheckBox)DocTypes.Controls[3]).Checked == true)
            {
                ret = true;
                break;
            }
        }
        return ret;
    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
