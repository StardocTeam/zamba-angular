using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Zamba.Services;

public partial class ucDocTypes: UserControl 
{    
    public delegate void DocTypesSelected(long docTypesId);
    public event DocTypesSelected _DocTypesSelected;

    public void LoadDocTypes()
    {
        try 
        {
            if (Session["User"] != null)
            {
                ArrayList dsdt = new sDocType().GetDocTypesIdsAndNamesbyRightView(((Zamba.Core.IUser)Session["User"]).ID);

                DocTypes.DataValueField = "ID";
                DocTypes.DataTextField = "NAME";

                DocTypes.DataSource = dsdt;
                DocTypes.DataBind();

                DocTypes.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                //05/07/11: Sumada la posibilidad que cuando venga un documento (que se usa para asociar) cargue automaticamente el combobox.
                Int32 tempDoctypeId;
                Int32.TryParse(Request.QueryString["doctypeid"], out tempDoctypeId);
                if (tempDoctypeId > 0)
                {
                    DocTypes.SelectedValue = tempDoctypeId.ToString();
                    DocTypes.Enabled = false;
                    DocTypes.BackColor = System.Drawing.Color.LightGray;

                    _DocTypesSelected(long.Parse(DocTypes.SelectedItem.Value));
                }
                else
                {
                    DocTypes.SelectedValue = Request.Form[DocTypes.UniqueID];
                    if (Request.Form[DocTypes.UniqueID] != null)
                        _DocTypesSelected(long.Parse(Request.Form[DocTypes.UniqueID]));
                }

                //DocTypes_SelectedIndexChanged(DocTypes, new EventArgs());
            }
        }
        catch (Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }
    }

    public void LoadDocTypes(Hashtable Params)
    {
        try
        {
            if (Session["User"] != null)
            {
                ArrayList dsdt = new sDocType().GetDocTypesIdsAndNamesbyRightView(((Zamba.Core.IUser)Session["User"]).ID);

                DocTypes.DataValueField = "ID";
                DocTypes.DataTextField = "NAME";

                DocTypes.DataSource = dsdt;
                DocTypes.DataBind();

                DocTypes.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                Int32 tempDoctypeId;
                Int32.TryParse(Params["DocTypeId"].ToString(), out tempDoctypeId);
                if (tempDoctypeId > 0)
                {
                    DocTypes.SelectedValue = tempDoctypeId.ToString();
                    DocTypes.Enabled = false;
                    DocTypes.BackColor = System.Drawing.Color.LightGray;
                }

                DocTypes_SelectedIndexChanged(DocTypes, new EventArgs());
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    protected void DocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //_DocTypesSelected(long.Parse(DocTypes.SelectedItem.Value));
        //_DocTypesSelected(long.Parse(Request.Form[DocTypes.UniqueID]));
    }
}