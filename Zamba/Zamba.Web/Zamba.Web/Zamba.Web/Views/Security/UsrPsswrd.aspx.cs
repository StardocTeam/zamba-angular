using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

public partial class Views_Security_Default : System.Web.UI.Page
{
    protected void lnkSave_clic(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {  
            IUser User;        
            User =  Zamba.Membership.MembershipHelper.CurrentUser;
            if (String.Compare(CurrentPassword.Text.Trim(), User.Password) == 0)
            {
                if (String.Compare(NewPassword.Text.Trim(), NewPassword2.Text.Trim(), true) == 0)
                {
                    //valido CaseSensitive
                    if (String.Compare(NewPassword.Text.Trim(), NewPassword2.Text.Trim(), false) == 0)
                    {
                        if (IsValidPassword(User.ID, User.Name, NewPassword.Text, true))
                        {
                            User.Password = NewPassword.Text;
                            UserBusiness UB = new UserBusiness();
                            UB.UpdateUserPassword(User);
                            lblMsj.Visible = true;
                            lblMsj.Text = "La contraseña se cambió correctamente.";

                            string Script = "$(document).ready(function(){ parent.CloseChangePassDlg();});";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Logout", Script, true); 
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Las claves no coindicen. Recuerde que el Password es CaseSensitive");
                    }
                }
                else
                {
                     ShowErrorMessage("La confirmación de la nueva contraseña es incorrecta.");
                }
            }
            else
            {
                ShowErrorMessage("Contraseña de usuario incorrecta.");
            }
         }

        divError.Style.Remove("display");
        divError.Style.Add("display", "block");
    }

    void ShowErrorMessage(string msg)
    {
        lblMsj.Visible = true;
        divError.Attributes.Remove("display");
        divError.Attributes.Add("display", "block");
        lblMsj.Text = msg;
    }

    // -----------------------------------------------------------------------------
    // <summary>
    // Verifica si el password es válido
    // </summary>
    // <param name="userid"></param>
    // <param name="username"></param>
    // <param name="password"></param>
    // <returns></returns>
    // <remarks>SecurityOptions</remarks>
    // <history>
    // 	[Hernan]	16/06/2005	Created
    // </history>
    // -----------------------------------------------------------------------------
    private bool IsValidPassword(Int64 userid,String username,String password,bool PswHasChanged)
    {
        Opciones opcion = new Opciones();

        if (opcion.SameUser == true)
        {
            if (String.Compare(username, password) == 0)
            {
              lblMsj.Visible = true;
              lblMsj.Text = "La contraseña no puede ser igual al usuario, ingrese otro";
              return false;   
            }           
        }
        if (opcion.AcceptNull == false)
        {
            if (password.Length == 0)
            {
              lblMsj.Visible = true;
              lblMsj.Text = "La contraseña no puede estar en blanco";
              return false;   
            }           
        }
        if (opcion.SamePC == false)
        {
             if (String.Compare(password,  Environment.MachineName) == 0)
            {
              lblMsj.Visible = true;
              lblMsj.Text = "La contraseña no puede ser igual al nombre de la PC";
              return false;   
            }           
        }
        if (opcion.Alfanumerico == true)
        {
             if (SecurityOptions.IsAlfanumeric(password) == false)
            {
              lblMsj.Visible = true;
              lblMsj.Text = "La contraseña debe contener letras y números";
              return false;   
            }           
        }
        if (opcion.LongitudMinima == true)
        {
            if (password.Length < 6)
            {
              lblMsj.Visible = true;
              lblMsj.Text = "La contraseña debe tener como mínimo 6 caracteres";
              return false;   
            }           
        }
        
        if (PswHasChanged == true)
        {
            if (opcion.CanRepeat == false)
            {

                if (SecurityOptions.Repetida(Convert.ToInt32(userid), password) == true)
                {
                    lblMsj.Visible = true;
                    lblMsj.Text = "Esta contraseña ya fue utilizada en los últimos 12 meses.";
                    return false;
                }
            }
        }
        return true;
    }
    
}
