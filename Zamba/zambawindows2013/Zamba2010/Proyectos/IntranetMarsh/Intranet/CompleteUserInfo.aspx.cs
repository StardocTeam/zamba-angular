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

namespace IntranetMarsh
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 UserId =  Int64.Parse(Request.QueryString["UserId"]);
            Hashtable UserInfo;
            
            UserInfo = UserBusiness.GetUserInfo(UserId);

            
            txtApellido.Text = UserInfo["APELLIDO"].ToString();
            txtEmpresa .Text = UserInfo["EMPRESA"].ToString();
            //txtEstado.Text = UserInfo["STATE"].ToString();
            txtIntEmpresa.Text = UserInfo["INTEMPRESA"].ToString();
            txtInterno.Text = UserInfo["INTERNO"].ToString();
            //txtNick.Text = UserInfo["NAME"].ToString();
            txtNombres.Text = UserInfo["NOMBRES"].ToString();
            //txtPassword.Text = UserInfo["PASSWORD"].ToString();
            txtEmail.Text = UserInfo["CORREO"].ToString();
            txtPuesto.Text = UserInfo["PUESTO"].ToString();
            txtTelefono.Text = UserInfo["TELEFONO"].ToString();
            txtTipo.Text = UserInfo["TIPO"].ToString();
            txtSector.Text = UserInfo["SECTOR"].ToString();
            lblTitle.Text = lblTitle.Text + txtApellido.Text + " " + txtNombres.Text ;

            
            
               
                Image1.ImageUrl = UserInfo["FOTO"].ToString();
            
            
            
            
            
        }

       
    }
}
