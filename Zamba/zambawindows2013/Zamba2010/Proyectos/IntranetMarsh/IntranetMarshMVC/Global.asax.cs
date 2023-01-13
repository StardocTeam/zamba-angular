using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System;
using Zamba.Services;
using Zamba.Core;

namespace IntranetMarshMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutesIIS7(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Inicio", action = "Index", id = "" }  // Parameter defaults
            ); 
        }

        public static void RegisterRoutesIIS5(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}.aspx/{action}/{id}",
                new { action = "Index", id = "" }
              );

            routes.MapRoute(
              "Root",
              "",
              new { controller = "Inicio", action = "Index", id = "" }
            );
        }

        protected void Application_Start()
        {
            if (double.Parse(ConfigurationSettings.AppSettings["iis"].ToString()) < 7)
                RegisterRoutesIIS5(RouteTable.Routes);
            else
                RegisterRoutesIIS7(RouteTable.Routes);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            int zamba_user_Id = int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString());
            int conn_id, TimeOut;

            string username = UserBusiness.GetUserNamebyId(zamba_user_Id);

            // timeout de la sesion y la licencia
            if(!int.TryParse(ConfigurationSettings.AppSettings["timeout"], out TimeOut))
                TimeOut = 1;

            Session.Timeout = TimeOut;            

            try
            {
                Rights.ValidateLogIn(zamba_user_Id);                
                
                // intentar conectar sin forzar conexion
                conn_id = doLogin(zamba_user_Id, username, TimeOut, false);

                if (conn_id <= 0)
                {
                    // forzar conexion
                    doLogin(zamba_user_Id, username, TimeOut, true);
                }                
            }
            catch (Exception ex)
            {
                // si falla por maximo de licencias usadas dejarlo que pase igual
                if (ex.Message.Contains("Máximo de licencias") || ex.Message.Contains("Número de licencias negativo"))
                {
                    // forzar conexion
                    doLogin(zamba_user_Id, username, TimeOut, true);
                }
                else
                {
                    Session.Timeout = 0;
                    ZClass.raiseerror(ex);
                }
            }            
        }

        private int doLogin(int user_id, string username, int timeout, bool forzar)
        {
            int conn_id = 0;

            conn_id = Ucm.NewConnection(user_id, username, Environment.MachineName, (short)timeout, 0, forzar);
            
            UserBusiness.Rights.CurrentUser().ConnectionId = conn_id;
            Ucm.ConectionTime = DateTime.Now;
            UserBusiness.Rights.CurrentUser().WFLic = false;

            if(forzar)
                UserBusiness.Rights.SaveAction(conn_id, ObjectTypes.LogIn, RightsType.InicioForzadoDeSesion, "Usuario: Intranet", user_id);

            return conn_id;
        }
    }
}