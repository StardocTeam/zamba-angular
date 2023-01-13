using System;
using System.Collections.Generic;
using System.Text;
using WebLoginFactory;

namespace WebLoginBusiness
{
    public class LoginBusiness
    {
        public static string LogIn(string userName, string password, string IP)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new Exception("El nombre del usuario se encuentra vacío");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("La contraseña se encuentra vacía");
            }
                 
            switch(LoginFactory.LogIn(userName, password, IP))
            {
                case 0:
                    {
                        return userName;
                    }
                case 1:
                    {
                        return userName;
                    }
                case 2:
                    {
                        throw new Exception("El usuario o clave son incorrectos");
                    }
                default:
                    {
                        throw new Exception("Ha ocurrido un error inesperado");
                    }
            }
        }

        public static bool LogOut(string userName, string IP)
        {
            switch (LoginFactory.LogOut(userName, IP))
            {
                case 0:
                    {
                        return true;
                    }
                case 1:
                    {
                        throw new Exception("El usuario se encuentra desconectado actualmente");
                    }
                case 2:
                    {
                        throw new Exception("El usuario es inexistente");
                    }
                default:
                    {
                        throw new Exception("Ha ocurrido un error inesperado");
                    }
            }

        }
    }
}
