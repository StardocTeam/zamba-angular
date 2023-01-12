using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Servers;
using System.Data;
using System.Data.SqlClient;

namespace WebLoginFactory
{
    public class LoginFactory
    {
        public static Int32 LogIn(string userName, string password, string ip)
        {
            try
            {
                IConnection con = Zamba.Servers.Server.get_Con(false, true, false);
                con.Open();

                if (userName.Contains("'")) userName = userName.Replace("'", String.Empty);
                if (password.Contains("'")) password = password.Replace("'", String.Empty);

                object[] param = {userName, password, ip};
                Object result = con.ExecuteScalar("zsp_login_100", param);       
          
                con.Close();
                param = null;

                return (Int32)result;
                //if ((Int32)result == 1)
                //    return true;
                //else
                //    return false;
            }
            catch
            {
                throw new Exception("Ha ocurrido un error inesperado al conectarse");
            }
        }
        public static Int32 LogOut(string userName, string ip)
        {
            try
            {
                IConnection con = Zamba.Servers.Server.get_Con(false, true, false);
                con.Open();

                if (userName.Contains("'")) userName = userName.Replace("'", String.Empty);

                object[] param = { userName, ip };
                Object result = (Int32)con.ExecuteScalar("zsp_logout_100", param);

                con.Close();
                param = null;

                return (Int32)result;
                //if ((Int32)result == 1)
                //    return true;
                //else
                //    return false;
            }
            catch
            {
                throw new Exception("Ha ocurrido un error inesperado al desconectarse");
            }
        }
    }
}
