using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChatJsMvcSample.Controllers;

namespace ChatJsMvcSample.Services
{
    /// <summary>
    /// Summary description for Crud1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Crud1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string Delete(int Id)
        {
            CrudController cc = new CrudController();
            string result=cc.Delete(Id);
           
            return result;
        }

        [WebMethod]
        public string Create(string Name)
        {
            CrudController cc = new CrudController();
            string result = cc.Create(Name);

            return result;
        }

        [WebMethod]
        public string BlockUser(int Id, int Block)
        {//                                 1=Block - 0=Unblock
            CrudController cc = new CrudController();
            string result = cc.BlockUser(Id, Block);

            return result;
        }

        [WebMethod]
        public string UpdateAvatar(int Id, int Type, string Avatar)
        {//                              0=Default 1: Base64. 2: Path
            CrudController cc = new CrudController();
            string result = cc.UpdateAvatar(Id, Type, Avatar);

            return result;
        }

        [WebMethod]                                                         //0=Active 1=Listener 2=Blocked
        public string UpdateUser(int Id, string Name, int Status, int Role)
        {//                                                 0=Offline 1=Online 2=Busy
            CrudController cc = new CrudController();
            string result = cc.UpdateUser(Id, Name, Status, Role);

            return result;
        }

    }
}
