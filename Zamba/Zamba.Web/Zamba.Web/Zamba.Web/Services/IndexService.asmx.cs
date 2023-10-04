using System;
using System.Web.Services;
using Zamba.Core;
using Zamba.Services;
using System.Web.Script.Services;
using ZambaWeb.Api.Controllers;
using System.Data;
using System.Text;
using Zamba;
using Zamba.Core.Cache;
using Newtonsoft.Json;

namespace ScriptWebServices
{
    /// <summary>
    /// Descripción breve de IndexService
    /// </summary>
    [WebService(Namespace = "ScriptWebServices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [ScriptService]
    public class IndexService : System.Web.Services.WebService
    {

        public IndexService()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHierarchyOptions(long IndexId, long ParentIndexId, string ParentValue, int UserId)
        {
            GetUser(UserId);
            SIndex indexServices = new SIndex();
            IndexsBusiness IB = new IndexsBusiness();
            IIndex parentIndex = IB.GetIndex(ParentIndexId);
            parentIndex.Data = ParentValue;
            return IndexId.ToString() + '|' + indexServices.GetHierarchyOptions(IndexId, parentIndex);

        }

        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                IUser user = null;

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }


        public string GetUserName(int userId)
        {
            Zamba.Core.UserBusiness UB;
            try
            {
                UB = new Zamba.Core.UserBusiness();
                var userName = UB.GetUserNamebyId(userId);
                return userName;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
            finally
            {
                UB = null;
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHierarchyOptionsWidthID(long IndexId, long ParentIndexId, string ParentValue, int UserId, string SenderID)
        {
            SecuritySQL.ValidateRequestSQLInjection(ParentValue, Context.Response);
            GetUser(UserId);
            SIndex indexServices = new SIndex();
            IndexsBusiness IB = new IndexsBusiness();

            IIndex parentIndex = IB.GetIndex(ParentIndexId);
            parentIndex.Data = ParentValue;
            return SenderID + '|' + indexServices.GetHierarchyOptions(IndexId, parentIndex);
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
        public string GetTree(int currentuserid, string token = "")
        {
            string EntitiesString = string.Empty;

            if (DocTypesAndIndexs.hsSearchEntities.ContainsKey(currentuserid) == false)
            {
                try
                {

                    string LastSelectedNodesIds = GetLastSelectedNodesIds(currentuserid);
                    DataTable entities = GetEntitiesByUser(currentuserid);
                    EntitiesString = GetTreeNodeJsonString(entities, LastSelectedNodesIds);                   

                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
                }

                lock (DocTypesAndIndexs.hsSearchEntities)
                {
                    if (!DocTypesAndIndexs.hsSearchEntities.ContainsKey(currentuserid))
                        DocTypesAndIndexs.hsSearchEntities.Add(currentuserid, EntitiesString);
                }

                return EntitiesString;

            }
            else
            {
                return DocTypesAndIndexs.hsSearchEntities[currentuserid].ToString();
            }

        }

        private string GetLastSelectedNodesIds(int currentuserid)
        {
            UserPreferences UP = new UserPreferences();
            string lastSelectedNodeIds = UP.getEspecificUserValue("WebSearchLastNodes", UPSections.Viewer, string.Empty, currentuserid);
            lastSelectedNodeIds = FormatNodesIds(lastSelectedNodeIds);
            
            return lastSelectedNodeIds;
        }

        private string FormatNodesIds(string lastSelectedNodeIds)
        {
            lastSelectedNodeIds = lastSelectedNodeIds.Replace("=", "").Replace("Section", "").Replace("Entity", "").Replace("-", "");
            if (lastSelectedNodeIds.Length == 0) lastSelectedNodeIds = "0";
            return lastSelectedNodeIds;
        }

        private DataTable GetEntitiesByUser(long userId)
        {
            StringBuilder entitiesQuery = new StringBuilder();

            entitiesQuery.Append("SELECT distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME) text, DocTypeDocGroup.orden orden ");
            entitiesQuery.Append("FROM doc_type e inner join USR_RIGHTS re on re.RTYPE = 7 and re.ADITIONAL = e.DOC_TYPE_ID AND ");
            entitiesQuery.Append($"(groupid = {userId} or groupid in (select inheritedusergroup from group_r_group where usergroup = {userId}) OR ");
            entitiesQuery.Append($"groupid in ( Select groupid from usr_r_group where usrid = {userId}) or groupid IN ");
            entitiesQuery.Append($"(select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid = {userId} ))) ");
            entitiesQuery.Append("INNER JOIN (select * from(select doc_type_id, min(doc_order) orden from doc_type_r_doc_type_group A group by doc_type_id) q ) ");
            entitiesQuery.Append("DocTypeDocGroup ON DocTypeDocGroup.doc_type_id = e.DOC_TYPE_ID order by text");

            var Entities = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, entitiesQuery.ToString());

            Entities.Tables[0].Columns.RemoveAt(2);

            return Entities.Tables[0];
        }

        private string GetTreeNodeJsonString(DataTable entities, string lastselectednodeid)
        {
            StringBuilder EntitiesTree = new StringBuilder();
            EntitiesTree.Append("[{\"id\":-1,\"text\":\"Entidades\",\"PARENT_ID\":0,\"spriteCssClass\":\"glyphicon glyphicon-folder-open\",\"NodeType\":\"Section\",\"checked\":\"false\",\"items\":[");

            foreach (DataRow r in entities.Rows)
            {
                EntitiesTree.Append("{");
                EntitiesTree.Append($"\"id\":{r["id"].ToString()},\"text\":\"{r["text"].ToString()}\",\"EntityOrder\":1,\"ParentId\":1,\"spriteCssClass\":\"glyphicon glyphicon-file\",\"NodeType\":\"Entity\",\"checked\":\"");
                EntitiesTree.Append(lastselectednodeid.IndexOf(r["id"].ToString()) != -1 ? "true" : "false");
                EntitiesTree.Append("\"},");
            }
            if (entities.Rows.Count > 0)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no tiene permisos para buscar ninguna entidad");
                EntitiesTree.Remove(EntitiesTree.Length - 1, 1);
            }
            EntitiesTree.Append("]}]");

            return EntitiesTree.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SetLastNodes(string LastNodes, int currentUserId)
        {
            UserPreferences UP;
            try
            {
                UP = new UserPreferences();
                UP.setEspecificUserValue("WebSearchLastNodes", LastNodes, UPSections.Viewer, currentUserId);

                // vuelvo a armar el arbol para sobreescribir el cache
                string lastNodes = FormatNodesIds(LastNodes);
                DataTable entities = GetEntitiesByUser(currentUserId);
                string EntitiesString = GetTreeNodeJsonString(entities, lastNodes);

                lock (DocTypesAndIndexs.hsSearchEntities)
                {
                    if (!DocTypesAndIndexs.hsSearchEntities.ContainsKey(currentUserId))
                    {
                        DocTypesAndIndexs.hsSearchEntities.Add(currentUserId, EntitiesString);
                    }
                    else
                    {
                        DocTypesAndIndexs.hsSearchEntities[currentUserId] = EntitiesString;
                    }
                }

                var result = JsonConvert.SerializeObject("OK", Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return result;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al guardar nodos: " + ex.ToString());
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool GetLastNodes(string LastNodes, int currentuserid)
        {
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

            try
            {

                UP.setEspecificUserValue("WebSearchLastNodes", LastNodes, Zamba.UPSections.Viewer, currentuserid);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                UP = null;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string KeepAlive()
        {
            return "Hi";
        }



    }

}
