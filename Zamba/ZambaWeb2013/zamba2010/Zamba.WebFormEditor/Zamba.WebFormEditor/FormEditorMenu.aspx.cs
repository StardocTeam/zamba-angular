using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Data;
using System.IO;

namespace Zamba.WebFormEditor
{
    public partial class FormEditorMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDocTypes();
                LoadFormTypes();
            }
        }

        protected void LoadDocTypes()
        {
            var DocTypes = Zamba.Core.DocTypesBusiness.GetDocTypesIdsAndNamesSorted();

            foreach (DataRow DT in DocTypes.Tables[0].Rows)
            {
                int DocTypeId = int.Parse(DT["DOC_TYPE_ID"].ToString());

                Telerik.Web.UI.RadTreeNode RT = new Telerik.Web.UI.RadTreeNode(DT["DOC_TYPE_NAME"].ToString(),DocTypeId.ToString() );
                RT.Category = "Entity";
              
                this.DropDownListEntity.Items.Add(new ListItem(DT["DOC_TYPE_NAME"].ToString(), DT["DOC_TYPE_ID"].ToString()));

                var Forms = Zamba.Core.FormBusiness.GetAllForms(DocTypeId);

                if (Forms != null)
                {
                    this.RadTreeView1.Nodes.Add(RT);
                    foreach (ZwebForm ZF in Forms)
                    {
                        Telerik.Web.UI.RadTreeNode RTF = new Telerik.Web.UI.RadTreeNode(ZF.Name + " " + ZF.Type, ZF.ID.ToString());
                        RTF.Category = "Form";
                        RT.Nodes.Add(RTF);
                    }

                  
                }
            }
        }

        protected void RadTreeView1_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            if (this.RadTreeView1.SelectedNode != null)
            {
                if (this.RadTreeView1.SelectedNode.Category == "Form")
                {
                    String DocTypeId = this.RadTreeView1.SelectedNode.ParentNode.Value;
                    String FormId = this.RadTreeView1.SelectedNode.Value;

                    Response.Redirect(String.Format("FormEditorDD.aspx?EId={0}&FId={1}", DocTypeId, FormId));

                }
            }
        }

        protected void BtnNewForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DropDownListEntity.SelectedIndex != -1)
                {
                    if (this.DropDownListFormTypes.SelectedIndex != -1)
                    {
                        if (String.IsNullOrEmpty(txtName.Text) == false)
                        {
                            if (String.IsNullOrEmpty(txtPath.Text) == false)
                            {
                                    if (!txtPath.Text.ToLower().Trim().EndsWith(".html"))
                                    {
                                        this.txtPath.Text = Path.Combine(this.txtPath.Text.Trim(), txtName.Text.Trim() + ".html"); 
                                    }

                                    if (File.Exists(txtPath.Text) == false)
                                    {

                                    String FormTypeId = this.DropDownListFormTypes.SelectedValue;
                                        String DocTypeId = this.DropDownListEntity.SelectedValue;
                                        this.lblerror.Text = "";

                                        //Se guarda en sesion asi no se ve en el query string
                                        Session.Add("Path: " + txtName.Text, txtPath.Text);

                                        Response.Redirect(String.Format("FormEditorDD.aspx?EId={0}&FId=0&FTId={1}&Name={2}", DocTypeId, FormTypeId, txtName.Text));
                                    
                                }
                                else
                                {
                                    this.lblerror.Text = "Ya existe un archivo con ese nombre en la direccion especificada";
                                }
                            }
                            else
                            {
                                this.lblerror.Text = "La ruta ingresada es invalida";
                            }
                        }
                        else
                        {
                            this.lblerror.Text = "Debe ingresar un nombre para el formulario";
                        }
                    }
                    else
                    {
                        this.lblerror.Text = "Debe seleccionar un tipo de formulario a crear";
                    }
                }
                else
                {
                    this.lblerror.Text = "Debe seleccionar una Entidad en el árbol de la izquierda";
                }
            }
            catch (Exception ex)
            {
                this.lblerror.Text = ex.ToString();
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        protected void LoadFormTypes()
        {
            try
            {
                foreach (int FormTypeId in Enum.GetValues(typeof(Zamba.Core.FormTypes)))
                {
                    if (Enum.GetName(typeof(Zamba.Core.FormTypes),FormTypeId).ToLower()!="all")
                        this.DropDownListFormTypes.Items.Add(new ListItem(Enum.GetName(typeof(Zamba.Core.FormTypes),FormTypeId),FormTypeId.ToString()));            
                }

                string path = ZOptBusiness.GetValue("FormPath");
                if (!string.IsNullOrEmpty(path) && string.IsNullOrEmpty(txtPath.Text))
                {
                    txtPath.Text = path;
                }
            }
            catch (Exception ex)
            {
                this.lblerror.Text = ex.ToString();
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }
    }
}
