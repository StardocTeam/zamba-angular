using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Collections;
using System.Web.Script.Services;
using System.Data;

namespace ScriptWebServices
{
    /// <summary>
    /// Summary description for Indexs
    /// </summary>
    [WebService(Namespace = "ScriptWebServices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class IndexsService : System.Web.Services.WebService
    {
        public IndexsService()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHierarchyOptions(long IndexId, long ParentIndexId, string ParentValue, int UserId)
        {
            try
            {

                SIndex indexServices = new SIndex();

                return IndexId.ToString() + '|' + indexServices.GetHierarchyOptions(IndexId, ParentIndexId, ParentValue);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHierarchyOptionsWidthID(long IndexId, long ParentIndexId, string ParentValue, int UserId, string SenderID)
        {
            try
            {

                SIndex indexServices = new SIndex();

                return SenderID + '|' + indexServices.GetHierarchyOptions(IndexId, ParentIndexId, ParentValue);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool ValidateHierarchyValue(string ValueToValidate, long IndexId, long ParentIndexId, string ParentValue, int UserId)
        {

            SIndex indexServices = new SIndex();

            return indexServices.ValidateHierarchyValue(ValueToValidate, IndexId, ParentIndexId, ParentValue);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTree(string token, int currentuserid)
        {
            try
            {
                //var lastselectednodeid = Zamba.Core.UserPreferences.getEspecificUserValue("WebSearchLastNodes", Zamba.Core.Sections.Viewer, string.Empty, currentuserid);
                //var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON select " + Zamba.Servers.Server.DBUser + ".qfn_XmlToJson((select tree from " + Zamba.Servers.Server.DBUser + ".zfn_search_100_gettree({0},'{1}')))", currentuserid, lastselectednodeid));

                var lastselectednodeid = Zamba.Core.UserPreferences.getEspecificUserValue("WebSearchLastNodes", Zamba.Core.Sections.Viewer, string.Empty, currentuserid);
                var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON select dbo.qfn_XmlToJson((select tree from dbo.zfn_search_100_gettree({0},'{1}')))", currentuserid, lastselectednodeid));
                return JsonTree.ToString();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool GetLastNodes(string LastNodes, int currentuserid)
        {
            try
               

            {
                Zamba.Core.UserPreferences.setEspecificUserValue("WebSearchLastNodes", LastNodes, Zamba.Core.Sections.Viewer, currentuserid);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
