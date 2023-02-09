using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Zamba.Core;
using Zamba.Web.GlobalSearch.search;

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
                //throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
                return "";
            }

        }

        [WebMethod]
        public static string MapPathReverse(string fullServerPath)
        {
            return fullServerPath.ToLower().Replace(HttpContext.Current.Request.PhysicalApplicationPath.ToLower(), String.Empty);
        }

        [WebMethod(EnableSession = true)]
        public bool LoginOkta(int userid, string token)
        {
            Zamba.Core.ZssFactory zssFactory = new Zamba.Core.ZssFactory();
            Zamba.Services.SUsers suser = new Zamba.Services.SUsers();
            var user = suser.GetUser(userid);
            //zssFactory.CheckTokenInDatabase(System.Convert.ToInt64(userid), token, false);
            if (!zssFactory.CheckTokenInDatabase(userid, token, false))
                return false;
            Zamba.Membership.MembershipHelper.SetCurrentUser(user);
            return true;

        }

        [WebMethod(EnableSession = true)]
        public string getValueFromWebConfig(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) {
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