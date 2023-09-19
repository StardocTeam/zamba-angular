using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Zamba.Core;
using Zamba.Membership;

/// <summary>
/// Summary description for Security
/// </summary>
/// 
namespace ZambaWeb.Api.Controllers
{
    
    public class SecuritySQL
    {
        public static void ValidateRequestSQLInjection(string value, HttpResponse response)
        {
            List<char> invalidChars = new List<char>();
            char[] charList = { '<', '>', ';', '\'', '\"', '\\', '+' };
            invalidChars.AddRange(charList);
            Boolean resp = value.ToCharArray().Any(c => invalidChars.Contains(c));
            if (!resp)
            {
                response.StatusCode = 400;
                response.End();
            }
        }
    }
    
    public class Security: Controller
    {
        public string ChangePassword(string newPass)
        {
            //
            // TODO: Add constructor logic here
            //
            IUser User;
            User = Zamba.Membership.MembershipHelper.CurrentUser;
            var isValid = IsValidPassword(User.ID, User.Name, newPass, true);
            if (isValid != string.Empty)
                return isValid;

            User.Password = newPass;
            try
            {
                UserBusiness UB = new UserBusiness();
                UB.UpdateUserPassword(User);
            }
            catch (Exception ex)
            {
                return "Error al cambiar contraseña: " + ex.ToString();
            }

            return string.Empty;
        }
        private string IsValidPassword(Int64 userid, String username, String password, bool PswHasChanged)
        {
            Opciones opcion = new Opciones();

            if (opcion.SameUser == true)
            {
                if (String.Compare(username, password) == 0)
                {
                    //lblMsj.Visible = true;
                    //lblMsj.Text =;
                    return "La contraseña no puede ser igual al usuario, ingrese otro";
                }
            }
            if (opcion.AcceptNull == false)
            {
                if (password.Length == 0)
                {
                    //lblMsj.Visible = true;
                    //lblMsj.Text = 
                    return "La contraseña no puede estar en blanco";
                }
            }
            if (opcion.SamePC == false)
            {
                if (String.Compare(password, Environment.MachineName) == 0)
                {
                    //lblMsj.Visible = true;
                    //lblMsj.Text =
                    return "La contraseña no puede ser igual al nombre de la PC";
                }
            }
            if (opcion.Alfanumerico == true)
            {
                if (SecurityOptions.IsAlfanumeric(password) == false)
                {
                    //lblMsj.Visible = true;
                    //lblMsj.Text = 
                    return "La contraseña debe contener letras y números";
                }
            }
            if (opcion.LongitudMinima == true)
            {
                if (password.Length < 6)
                {
                    //lblMsj.Visible = true;
                    //lblMsj.Text = 
                    return "La contraseña debe tener como mínimo 6 caracteres";
                }
            }

            if (PswHasChanged == true)
            {
                if (opcion.CanRepeat == false)
                {

                    if (SecurityOptions.Repetida(Convert.ToInt32(userid), password) == true)
                    {
                        //lblMsj.Visible = true;
                        //lblMsj.Text = 
                        return "Esta contraseña ya fue utilizada en los últimos 12 meses.";
                    }
                }
            }
            return string.Empty;
        }
    }
}