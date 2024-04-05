using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Zamba;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Filters;
using Zamba.Framework;
using Zamba.Services;
using Zamba.Web;
using static Zamba.Core.UserBusiness;

namespace ScriptWebServices
{
    /// <summary>
    /// Descripción breve de AccountService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class AccountService : System.Web.Services.WebService
    {

        //23/08/11:Este método es usado para finalizar las tareas abiertas en la aplicacion Web
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Login(Int64 userId, string token)
        {
            try
            {
                if (userId == null || token == null)
                {
                    throw new Exception("fallo la autenticacion de dashboard RRHH");
                }

                Results_Business rb = new Results_Business();
                if (!rb.getValidateActiveSession(userId, token))
                {
                    throw new Exception("fallo la autenticacion de dashboard RRHH");
                }
                ZssFactory zssFactory = new ZssFactory();
                Zss zss = new Zss();
                User user = new User();
                user.ID = userId;
                zss = zssFactory.GetZss(user);
                //hdnAuthorizationData.Value = JsonConvert.SerializeObject(zss);
                // localStorage.setItem("authorizationData" ,JsonConvert.SerializeObject(zss));
                // localStorage.setItem("UserId", zss.UserId);
                //window.parent.postMessage('login-rrhh-ok', '*');
                //                HttpContext.Current.Session["SessionRefreshToken"] = DateTime.Now;
                return "login-rrhh-ok";
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                throw;
            }
        }        
        
    }
}