using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Zamba.Services;
using System.Collections.Generic;
using Zamba.Core;

public partial class ucDocTypes : UserControl
{
    public delegate void DocTypesSelected(long docTypesId);
    public event DocTypesSelected _DocTypesSelected;

    public void LoadDocTypes()
    {
        try
        {
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                List<DocType> dsdt = new sDocType().GetDocTypesIdsAndNamesbyRightView(( Zamba.Membership.MembershipHelper.CurrentUser).ID);

                DocTypes.DataValueField = "ID";
                DocTypes.DataTextField = "NAME";

                DocTypes.DataSource = dsdt;
                DocTypes.DataBind();

                DocTypes.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                //05/07/11: Sumada la posibilidad que cuando venga un documento (que se usa para asociar) cargue automaticamente el combobox.
                Int32 tempDoctypeId;
                Int32.TryParse(Request.QueryString["doctypeid"], out tempDoctypeId);
                //Permite seleccionar entidad y herencia desde icono adjuntar en tarea
                var entityChange = (Request.QueryString["entitychange"] == "true") ? true : false;
                if (tempDoctypeId > 0 && !entityChange)
                {
                    DocTypes.SelectedValue = tempDoctypeId.ToString();
                    DocTypes.Enabled = false;
                    DocTypes.BackColor = System.Drawing.Color.LightGray;
                    _DocTypesSelected(long.Parse(DocTypes.SelectedItem.Value));
                }
                else
                {
                    var uId = Request.Form[DocTypes.UniqueID];
                    if (entityChange)
                    {
                        DocTypes.SelectedValue = (uId == null) ? tempDoctypeId.ToString() : uId;
                        _DocTypesSelected((uId == null) ? long.Parse(DocTypes.SelectedItem.Value) : long.Parse(uId));
                    }
                    else
                    {
                        DocTypes.SelectedValue = uId;
                        if (uId != null)
                            _DocTypesSelected(long.Parse(uId));
                    }
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
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                List<DocType> dsdt = new sDocType().GetDocTypesIdsAndNamesbyRightView(( Zamba.Membership.MembershipHelper.CurrentUser).ID);

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