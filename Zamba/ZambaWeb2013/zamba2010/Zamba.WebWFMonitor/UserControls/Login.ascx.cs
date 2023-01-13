using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Servers;
using Zamba.Users.Factory;
using Zamba.Core;

public partial class Login : System.Web.UI.UserControl
{
    public delegate void SuccesfullLogIn();
    public event SuccesfullLogIn LoggedIn;
    private Int32 _loginAttempts = 0;
    private Boolean _remeberUser = false;
    private User _user = null;

    private void Login_Fail(String Usuario)
    {
        _loginAttempts += 1;
        if (_loginAttempts == 3)
        {
            String IsUserLocked;
            IConnection Con = Zamba.Servers.Server.get_Con(false, false, false);
            Con.Open();
            IsUserLocked = (String)Con.ExecuteScalar(CommandType.Text, "Select lockuser from ZSecOption");
            Con.Close();
            Con.dispose();
            if (Boolean.Parse(IsUserLocked))
                SecurityOptions.LockUserPassword(Int32.Parse(this.ID), Usuario);

            RightFactory.SaveAction(GetUserID(Usuario), IUser.ObjectTypes.Users, IUser.RightsType.InicioFallidoDeSesion, "PC:" + Environment.MachineName + ",Usuario_Windows:" + Environment.UserName, GetUserID(Usuario));
            Response.Redirect("about:blank");
        }
    }

    private static Int32 GetUserID(String name)
    {
        String Query = "SELECT id FROM usrtable WHERE name='" + name + "'";
        IConnection Con = Zamba.Servers.Server.get_Con(false, false, false);
        Con.Open();
        Int32 UserId = Int32.Parse((Con.ExecuteScalar(CommandType.Text, Query).ToString()));
        Con.Close();
        Con.dispose();

        return UserId;
    }

    private void Validar(String nombreUsuario, String password)
    {
        try
        {
           _user = RightFactory.ValidateLogIn(nombreUsuario, password);
            if (_user == null)
                Login_Fail(nombreUsuario);

            if (_user.ID  > 0)
            {
                if (!SecurityOptions.ClaveVencida((Int32)_user.ID))
                    Login_Ok(nombreUsuario);
                else
                {
                    pnlLog.Visible = false;
                    pnlCambioPassword.Visible = true;
                }
            }
            else
            {
                if (_user.ID == -1)
                    FailureText.Text = "El Usuario se encuentra bloqueado";
                else
                    FailureText.Text = "El Usuario o la clave ingresadas son incorréctos";

                Login_Fail(nombreUsuario);
            }
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            ZClass.RaiseError(ex);
        }
        catch (Exception ex)
        {
            ZClass.RaiseError(ex);
            FailureText.Text = ex.Message;
            Login_Fail(nombreUsuario);
        }
    }

    private void Login_Ok(String usuario)
    {
        try
        {
            String WinUser = Environment.UserName;
            String WinPc = Environment.MachineName;
            _remeberUser = cbRememberMe.Checked;
            if (_remeberUser)
            {
                if (RightFactory.GetUserRights(IUser.ObjectTypes.ModuleWorkFlow, IUser.RightsType.Use, -1) == false)
                    _remeberUser = false;
            }

            RightFactory.CurrentUser.WFLic = _remeberUser;
            RightFactory.CurrentUser.ConnectionId = Ucm.NewConnection((Int32)RightFactory.CurrentUser.ID, WinUser, WinPc, UserPreferences.TimeOut, _remeberUser);
            Ucm.ConectionTime = DateTime.Now;

            _remeberUser = cbRememberMe.Checked;
            if (_remeberUser)
            {
                if (RightFactory.GetUserRights(IUser.ObjectTypes.ModuleWorkFlow, IUser.RightsType.Use, -1) == false)
                    _remeberUser = false;
            }
            RightFactory.CurrentUser.WFLic = _remeberUser;
            RightFactory.CurrentUser.ConnectionId = Ucm.NewConnection((Int32)RightFactory.CurrentUser.ID, _user.Name, WinPc, UserPreferences.TimeOut, _remeberUser);
            Ucm.ConectionTime = DateTime.Now;

            LoggedIn();
        }
        catch (Exception ex)
        {
            ZClass.RaiseError(ex);
            this.FailureText.Text = "Maximo de Licencias conectadas, contáctese con su proveedor para adquirir nuevas licencias.";
            RightFactory.CurrentUser.ConnectionId = 0;
        }
    }
        
    protected void LoginButton_Click(object sender, System.EventArgs e)
    {
        Validar(this.tbNombreUsuario.Text, this.tbContraseña.Text);
    }
    
    protected void btCambiarPassword_Click(object sender, EventArgs e)
    {
        if (SecurityOptions.IsValidPassword((Int32)_user.ID, tbNombreUsuario.Text, tbContraseña2.Text.Trim(), true))
        {
            _user.Password = tbContraseña2.Text.Trim();
            try
            {
                UserFactory.Update(this._user);
                this.pnlCambioPassword.Visible = false;
                this.pnlLog.Visible = true;
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
                Login_Fail(tbNombreUsuario.Text);
            }
        }
    }
}