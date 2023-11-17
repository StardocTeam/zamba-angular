using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Zamba;
using Zamba.Core;

namespace ScriptWebServices
{
    /// <summary>
    /// Descripción breve de ViewsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ViewsService : System.Web.Services.WebService
    {



        public ViewsService()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTaskUrl(string token, string currentuserid, string docid, string entityid)
        {
            try
            {
                UserBusiness UB = new UserBusiness();
                Zamba.Core.IUser user = UB.ValidateLogIn(int.Parse(currentuserid), ClientType.Web);
                UserPreferences UP = new UserPreferences();
                Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, user.ID));
                user.TraceLevel = TraceLevel;
                Zamba.Membership.MembershipHelper.SetCurrentUser(user);
                Results_Business RB = new Results_Business();
                var path = RB.GetTaskURL(int.Parse(currentuserid), int.Parse(docid), int.Parse(entityid), true);
                RB = null;

                string relativePath = MapPathReverse(path).Replace(@"\", "/");
                return relativePath;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }

        }

        [WebMethod]
        public static string MapPathReverse(string fullServerPath)
        {
            return fullServerPath.ToLower().Replace(HttpContext.Current.Request.PhysicalApplicationPath.ToLower(), String.Empty);
        }

        [WebMethod(EnableSession = true)]
        public Int64 LoginOkta(int userid, string token)
        {
            Zamba.Core.ZssFactory zssFactory = new Zamba.Core.ZssFactory();
            Zamba.Services.SUsers suser = new Zamba.Services.SUsers();
            var user = suser.GetUser(userid);
            if (!zssFactory.CheckTokenInDatabase(userid, token, false))
                return 0;
            Zamba.Membership.MembershipHelper.SetCurrentUser(user);
            bool isLicenceOk = DoConsumeLicense(false, Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0);
            if (isLicenceOk)
            {
                return Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId; 
            }
            else
            {
                return 0;
            }
            
        }

        private bool DoConsumeLicense(Boolean blnWindowsLogin, string userName, Int64 userId, int connectionId)
        {
            string computerNameOrIp = GetComputerNameOrIp(blnWindowsLogin);
            UserPreferences UP = new UserPreferences();
            Zamba.Services.SRights sRights = null;

            try
            {
                // Se agrega una nueva pc a la tabla UCM
                // Parámetros: id de usuario actual | usuario de windows | nombre o IP de la computadora del usuario | timeOut | WFAvailable = valor false 
                // para licencia documental
                sRights = new Zamba.Services.SRights();
                Int32 timeout = 180;
                Int32.TryParse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "180"), out timeout);
                if (timeout == 0) timeout = 180;
                Ucm ucm = new Ucm();
                var ConnectionID = ucm.NewConnection(userId, userName, computerNameOrIp + "/" + HttpContext.Current.Session.SessionID, timeout, 0, false).ToString();

                //if (!Page.IsPostBack)
                //{
                //    ConnectionID = connectionId.ToString();
                //}

                Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId = Convert.ToInt32(ConnectionID);

                if (Int64.Parse(ConnectionID) > 0)
                {

                    // Se guarda la computadora del usuario. Es necesario por si el usuario presiona el botón "Cerrar Sesión" 
                    // para que éste pueda eliminarse de la tabla UCM
                    if (ConnectionID != null)
                        Session["ComputerNameOrIP"] = computerNameOrIp + "/" + Session.SessionID;

                    return true;
                }
                else
                {
                    ZClass.raiseerror(new Exception("No se ha podido establecer la conexion a Zamba: " + computerNameOrIp));
                    Session.Abandon();
                    Session.Clear();
                    sRights = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                Session.Abandon();
                Session.Clear();
                sRights = null;
                return false;
            }
        }

        private string GetComputerNameOrIp(bool blnWindowsLogin)
        {
            string computerNameOrIp = string.Empty;
            computerNameOrIp = GetUserIP();
            return computerNameOrIp;
        }
        private string GetUserIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }




        [WebMethod(EnableSession = true)]
        public string getValueFromWebConfig(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return string.Empty;
                }
                return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

    }
}