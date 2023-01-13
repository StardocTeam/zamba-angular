using System.Collections.Generic;
using System.Web.Http;
//using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Web.Http.Routing;
using AttributeRouting;
using AttributeRouting.Web;
using AttributeRouting.Web.Http;

using System.Net.Http;
using System.Net;

namespace ZambaWeb.RestApi.Controllers
{
  // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {

        public SearchController() {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [Route("api/search/Suggestions")]       
        public IEnumerable<Dictionary<string, object>> GetSuggestions(string text ="")
        {
            SearchSuggestions ss;
            DataSet ds = null;

            try
            {
                ss = new SearchSuggestions();
                ds = ss.getData(text);
                List<Dictionary<string, object>> lstData = ss.GetJson(ds.Tables[0]);
                return lstData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las sugerencias");
            }
            finally
            {
                ss = null;
                if(ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

       
        [Route("api/search/Results")]
        public DataTable Results(List<Filter> parameters)
        {
            SearchHelper sh = new SearchHelper();
            Zamba.Core.Search.ModDocuments md = new Zamba.Core.Search.ModDocuments();

            try
            {
                var search = sh.GetSearch(ref parameters);

                //TODO: agregar Login por usuario
                search.UserId = 2;

                //TODO: modificar este metodo con el que corresponda.
                DataTable dt = md.DoSearch(search, search.UserId, 1, 10, false, false, true, false, false);

                 List<IResult> results = new List<IResult>();
                
                //If IsNothing(dt) = False Then
                //    For Each row As DataRow In dt.Rows
                //        Dim doctypeid As Int64 = CInt(row("doc_type_id"))
                //        'Por ahora se implementa que vaya a la base a buscar el doc_type, hasta que se implemente la opcion de clonado
                //        Dim r As Result = New Result(CInt(row("doc_id")), DocTypesBusiness.GetDocType(doctypeid, True), row("Nombre del Documento").ToString(), 0)
                //        Results_Business.CompleteDocument(r, row)
                //        results.Add(r)
                //    Next
                //End If
                //Return results
                 return dt;
                //return "Salio bien";
            }
            catch (Exception ex)
            {
                return new DataTable("Error al obtener los resultados");
            }
            finally
            {
                sh = null;
                md = null;
            }
        }

        [Route("api/search/Entities")]
        public List<Entity> GetEntities()
        {
            RightsSchema rightsSchema = new RightsSchema();

            try
            {
                return rightsSchema.GetEntities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las entidades y sus atributos");
            }
            finally
            {
                rightsSchema = null;
            }
        }

        [Route("api/search/Tree")]
        public HttpResponseMessage GetTree(string token, int currentuserid)
        {
            try
            {
              var lastselectednodeid = Zamba.Core.UserPreferences.getEspecificUserValue("WebSearchLastNodes", Zamba.Core.Sections.Viewer, string.Empty,currentuserid);
              var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON select " + Zamba.Servers.Server.DBUser + ".qfn_XmlToJson((select tree from " + Zamba.Servers.Server.DBUser + ".zfn_search_100_gettree({0},'{1}')))", currentuserid, lastselectednodeid));
              return  Request.CreateResponse<string>(HttpStatusCode.OK, JsonTree.ToString(), Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
            }
        }

        [Route("api/search/LastNodes")]
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
