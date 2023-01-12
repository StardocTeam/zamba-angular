using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Zamba.Core;
using Zamba.Services;


using System.Web.Script.Services;
//using Zamba.Core.Enumerators;
using System.Web.Script.Serialization;

namespace ScriptWebServices
{
    /// <summary>
    /// Summary description for IUser
    /// </summary>
    /// 
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

    [System.Web.Script.Services.ScriptService]
    public class UsersInfo : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public string GetUserIdByCredentials(string userName, string password)
        {
            try { 
            IUser user = UserBusiness.Rights.ValidateLogIn(userName, password,ClientType.Web);
            return new JavaScriptSerializer().Serialize(user.ID);           
        }
            catch (Exception ex) { 
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetUserInfoByName(string userName)
        {

           // var windowsUserDomain = System.Web.HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
               // var windowsUser = windowsUserDomain.Split('\\')[1];
                IUser user = Zamba.Core.UserBusiness.GetUserByname(userName);
                return Newtonsoft.Json.JsonConvert.SerializeObject(user);
            }
            else
                return null;
        }

        [WebMethod]        
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =true,XmlSerializeString =false)]       
        public string GetUserInfoById(int id)
        {
            if (id>=1)
            {
                InitializeConection();             
                IUser user = Zamba.Core.UserBusiness.GetUserById(id);
                return Newtonsoft.Json.JsonConvert.SerializeObject(user);
            }
            else
                return null;
        }

        private void InitializeConection()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                var zc = new ZCore();               
                zc.InitializeSystem("Zamba.Web");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetUserId()
        {
            var windowsUserDomain = System.Web.HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrEmpty(windowsUserDomain))
            {
                var windowsUser = windowsUserDomain.Split('\\')[1];
                IUser user = Zamba.Core.UserBusiness.GetUserByname(windowsUser);
                return Newtonsoft.Json.JsonConvert.SerializeObject(user.ID);
            }
            else
                return null;
        }
        //IIS 6 ver.
        //System.Security.Principal.WindowsIdentity.GetCurrent().Name
        //System.Environment.UserName
    }  
}
