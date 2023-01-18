using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Zamba.Core;

namespace Zamba.WebFormEditor
{
    public partial class FormEditorTest : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
      

            if (!IsPostBack)
            {

                int FormId;
                String FormType = String.Empty;
                if (Request.QueryString["FId"] != null) FormId = int.Parse(Request.QueryString["FId"]); else FormId = 0;

                if (FormId == -1)
                    Response.Redirect("~/FormEditorMenu.aspx");

                if (FormId > 0)
                {
                    FormBusiness FB = new FormBusiness();
                    try
                    {
                        var ZForm = FB.GetForm(FormId);

                        FormType = ZForm.Type.ToString();

                        if (string.IsNullOrEmpty(ZForm.Path) == false)
                        {

                            if (File.Exists(ZForm.Path))
                            {
                                StreamReader sr = null;
                                String FormContent;

                                try
                                {
                                    sr = new StreamReader(ZForm.Path);
                                    FormContent = sr.ReadToEnd();
                                }
                                finally
                                {
                                    if (sr != null)
                                    {
                                        sr.Close();
                                        sr.Dispose();
                                        sr = null;
                                    }
                                }


                                Response.Write(FormContent);

                            }
                            else
                            {
                                Response.Write("No se ha encontrado el formulario seleccionado en la ruta: " + ZForm.Path);
                            }
                        }
                        else
                        {
                            Response.Write("El formulario seleccionado no tiene una ruta fisica");
                        }
                    }
                    catch (Exception ex)
                    {

                        Response.Write("<h2>Error al cargar el contenido del formulario</h2> " + ex.ToString());
                        Zamba.Core.ZClass.raiseerror(ex);
                    }
                    finally { FB = null; }
                }
             


              

            }
        }
    }
}