using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Zamba.Core;
using Zamba.FAD;

namespace ZambaWeb.RestApi.Controllers
{


    public class ActiveDirectoryController : ApiController
    {
        public ActiveDirectoryController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.WebRS");
            }
        }
        //string user =@"pseguros.com\stardoc";
        //string password = "noviembre2017";
        //string url = "LDAP://main.pseguros.com/"

        //[Authorize]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[Route("api/ActiveDirectory/UserLogin")]
        //public bool UserLogin([FromBody]string userName)
        //{
        //    //try
        //    //{
        //    //    ADResources ad = new ADResources();
        //    //    //var Isvalid = ad.ADValidation(userName);
        //    //    if (Isvalid)
        //    //        return true;
        //    //    else
        //    //        return false;

        //    //}
        //    //catch (Exception e)
        //    //{

        //    //    throw e;
        //    //}
        //}
        //        ADResources ad = new ADResources();
        //        //var Isvalid = ad.ADValidation(user,password,url,userName);
        //        //var Isvalid = ad.GetRolesForUser(userName);
        //        //if (Isvalid == true)
        //        //    return true;
        //        //else
        //        //    return false;

        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}
        //[Authorize]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[Route("api/ActiveDirectory/GetUsers")]
        //public string GetUsers()
        //{´+
        //ZTrace.WriteLineIf(ZTrace.IsVerbose, "Ingreso a GetUsers");
        // ADResources ad = new ADResources();

        //[Authorize]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/ActiveDirectory/GetUsers")]
        public string GetUsers()
        {
           ZTrace.WriteLineIf(ZTrace.IsVerbose, "Ingreso a GetUsers");
            ADResources ad = new ADResources();
            try
            {
                List<string> listUsers = new List<string>();


                //if (listUsers != null)
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "listUsers Count" + listUsers.Count.ToString());
                //listUsers = ad.GetUsers(user,password,url);
                if (listUsers != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "listUsers Count" + listUsers.Count.ToString());

                    //    var newresults = JsonConvert.SerializeObject(listUsers, Formatting.Indented,
                    //    new JsonSerializerSettings
                    //    {
                    //        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    //    });
                    //    return newresults;

                }

                else
                    return null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }

            return null;
        }    

        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/ActiveDirectory/GetAllGroup")]
        public List<string> GetAllGroup(string username)
        {
            ADResources ad = new ADResources();
            try
            {   
                List<string> Groups = ad.GetRolesForUser(username);
                if (Groups != null)
                    return Groups;
                else
                    return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        //[Authorize]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[Route("api/ActiveDirectory/GetUserGroup")]
        //public string GetUserGroup([FromBody]string Username)
        //{
        //    ADResources ad = new ADResources();
        //    try
        //    {

        //        //var UserGroup = ad.GetUserGroup(Username);
        //        //if (UserGroup != null)
        //        //    return UserGroup;
        //        //else
        //        //    return null;

        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }

        //}
        //[Authorize]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[Route("api/ActiveDirectory/GetUsersFromGroup")]
        //public List<string> GetUsersFromGroup([FromBody]string GroupName)
        //{
        //    ADResources ad = new ADResources();
        //    List<string> userlist = new List<string>();
        //    try
        //    {
        //        userlist = ad.GetUsersFromGroup(GroupName);

        //        if (userlist !=  null)
        //            return userlist;
        //        else
        //            return null;

        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }


        //}

    }
}