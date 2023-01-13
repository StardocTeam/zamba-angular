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

/// -----------------------------------------------------------------------------
/// <summary>
/// Clase que permite administrar las plantillas o templates
/// </summary>
/// <history>
///     [Gaston]	22/01/2009	Created
/// </history>
/// -----------------------------------------------------------------------------
public partial class WebClient_ABMTemplates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet dsTemplates = new DataSet();
            dsTemplates = TemplatesBusiness.GetTemplates();

            foreach (DataRow row in dsTemplates.Tables[0].Rows)
                this.lstTemplates.Items.Add(row["Name"].ToString());

            ViewState["templates"] = dsTemplates;
        }
        else
        {
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
    }

    protected void btnAddTemplate_click(object sender, EventArgs e)
    {
            // Si se presiono el botón "Agregar" para mostrar el popup
            if(btnAdd.Text ==  "Aceptar")
            {
                // Se crea una fila con el esquema de la tabla contenida en el DsTemplates
                DataRow row = ((DataSet)ViewState["templates"]).Tables[0].NewRow();
                // Se genera un nuevo id para el Template y se lo coloca en la celda Id de la fila
                row["Id"] = TemplatesBusiness.GetNewTemplateId();
                // Se colocan los datos ingresados en la fila en base a lo ingresado en el popup
                row["Name"] = this.ppTxtName.Text;
                row["Path"] = this.ppfuUploadFile.PostedFile.FileName;
                row["Description"] = this.pptxtDescription.Text;
                row["Type"] = 1;
                // Se agrega la fila al DsTemplates
                ((DataSet)ViewState["templates"]).Tables[0].Rows.Add(row);
                // Se agrega el elemento al listBox lstTemplates
                this.lstTemplates.Items.Add(row["Name"].ToString());
                // El elemento actualmente seleccionado en lstTemplates pasa a ser el nuevo elemento que se agrego en el Dataset
                this.lstTemplates.SelectedIndex = this.lstTemplates.Items.Count - 1;
            }
            // Sino, si se presiono el botón "Actualizar" para mostrar el popup
            else
            {
                // Se modifica la fila que se corresponde con el template seleccionado en la lista
                ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["name"] = this.ppTxtName.Text;
                ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["path"] = this.ppfuUploadFile.PostedFile.FileName;
                ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["description"] = this.pptxtDescription.Text;

                this.lstTemplates.SelectedItem.Value = this.ppTxtName.Text;

                ppTxtName.Text = "";
                pptxtDescription.Text = "";

                btnAdd.Text = "Aceptar";
            }

            DataSet ds = (DataSet)ViewState["templates"];
            // Se guarda el template en la base de datos
            TemplatesBusiness.SaveTemplates(ref ds);
            updateTextBoxs();
            ds.Dispose();
            ds = null;
        }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.btnAdd.Text = "Actualizar";
        //string name = this.txtName.Text;
        ppTxtName.Text = this.txtName.Text;
        //this.ppfuUploadFile.PostedFile.FileName = this.txtPath.Text;
        //string description = this.txtDescription.Text;
        this.pptxtDescription.Text = this.txtDescription.Text;
        PopUp.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
    }

    protected void lstTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(lstTemplates.SelectedItem != null)
            updateTextBoxs();
    }

    private void updateTextBoxs()
    {
        this.txtPath.Text = ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["path"].ToString();
        this.txtName.Text = ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["name"].ToString();
        this.txtDescription.Text = ((DataSet)ViewState["templates"]).Tables[0].Rows[this.lstTemplates.SelectedIndex]["description"].ToString();
    }
}
