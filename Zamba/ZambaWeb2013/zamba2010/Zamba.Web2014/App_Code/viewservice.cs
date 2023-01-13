using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using Zamba.Core;
using Tasks;

namespace ScriptWebServices
{
/// <summary>
/// Summary description for viewservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class viewservice : System.Web.Services.WebService {

    public viewservice () {

        Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
        ZC.InitializeSystem("Zamba.Web");
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetTaskUrl(string token, string currentuserid, string docid, string entityid)
    {
        try
        {
            Zamba.Core.IUser user = UserBusiness.Rights.ValidateLogIn(int.Parse(currentuserid), ClientType.Web);
            Zamba.Membership.MembershipHelper.SetCurrentUser(user);

            ViewerComponents VC = new ViewerComponents();
            var path = VC.GetTaskURL(int.Parse(currentuserid), int.Parse(docid), int.Parse(entityid), true);

            string relativePath = MapPathReverse(path).Replace(@"\", "/");
            return relativePath;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
        }

    }

    public static string MapPathReverse(string fullServerPath)
    {
        return fullServerPath.ToLower().Replace(HttpContext.Current.Request.PhysicalApplicationPath.ToLower(), String.Empty);
    }


}
}