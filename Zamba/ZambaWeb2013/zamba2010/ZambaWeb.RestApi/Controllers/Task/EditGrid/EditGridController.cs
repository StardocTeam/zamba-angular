using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using ZambaWeb.RestApi.Models;
using ZambaWeb.RestApi.Controllers.Common;
using Zamba.Core;
using System;
using Newtonsoft.Json;
using System.Linq;
using Zamba;
using System.Net.Http;
using System.Net;
using Zamba.Core.WF.WF;
using Zamba.Framework;
using Zamba.Services;
using Zamba.Membership;
using System.Web.Script.Serialization;

namespace ZambaWeb.RestApi.Controllers.Task.EditGrid
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EditGridController :ApiController
    {
        [RestAPIAuthorize]
        [globalControlRequestFilter]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/NewgetAssociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult NewgetAssociatedResults(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                CommonFuntions cf = new CommonFuntions();

                var user = cf.GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    STasks stasks = new STasks();
                    Int64 parentTaskId = 0;
                    Int64 parentResultId = 0;
                    Int64 parentEntityId = 0;
                    List<string> AsociatedIds = new List<string>();

                    parentResultId = Int64.Parse(paramRequest.Params["parentResultId"].ToString());
                    parentEntityId = Int64.Parse(paramRequest.Params["parentEntityId"].ToString());
                    parentTaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                    if (parentEntityId == 0)
                    {
                        parentEntityId = stasks.GetDocTypeId(parentTaskId);
                    }
                    AsociatedIds.AddRange(paramRequest.Params["AsociatedIds"].ToString().Split(char.Parse(",")));


                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(parentResultId, parentEntityId, true);

                    //DataTable dtAsoc = null;

                    //if (result != null)
                    //{
                    //    dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(result, 100, AsociatedIds, user.ID);
                    //}


                    SDocAsociated sda = new SDocAsociated();
                    DataTable AsociatedResults = new DataTable();

                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                    searchResult sr = new searchResult();
                    sr.columnsStringAssociated = UP.getValue("columnStringAssociated-" + parentEntityId + "-" + String.Join("-", AsociatedIds), UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);

                    var customUserAssociatedOrderBy = UP.getValue("customUserAssociatedOrderBy-" + parentEntityId + "-" + String.Join("-", AsociatedIds), UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);

                    if (customUserAssociatedOrderBy != string.Empty)
                    {
                        sr.OrderBy = customUserAssociatedOrderBy;
                    }

                    foreach (string DocTypeId in AsociatedIds)
                    {
                        if (AsociatedResults.Rows.Count == 0)
                            AsociatedResults = sda.getAsociatedResultsFromResultAsList(Int64.Parse(DocTypeId), result, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        else
                            AsociatedResults.Merge(sda.getAsociatedResultsFromResultAsList(Int64.Parse(DocTypeId), result, Zamba.Membership.MembershipHelper.CurrentUser.ID), true, MissingSchemaAction.Ignore);

                        sr.entities.Add(new EntityDto() { id = Int64.Parse(DocTypeId), name = new DocTypesBusiness().GetDocTypeName(Int64.Parse(DocTypeId)), enabled = true });
                    }

                    sda = null;

                    List<string> ColumnsToRemove = new List<string>();
                    foreach (DataColumn c in AsociatedResults.Columns)
                    {
                        if ((c.ColumnName.ToLower().StartsWith("i") && cf.IsNumeric(c.ColumnName.Remove(0, 1))) || (GridColumns.ColumnsVisibility.ContainsKey(c.ColumnName.ToLower()) && GridColumns.ColumnsVisibility[c.ColumnName.ToLower()] == false))
                        {
                            ColumnsToRemove.Add(c.ColumnName);
                        }
                    }
                    //Remuevo las columnas
                    if (ColumnsToRemove.Count > 0)
                    {
                        foreach (string colName in ColumnsToRemove)
                            if (colName != "ICON_ID")
                            {
                                AsociatedResults.Columns.Remove(colName);
                            }
                    }

                    // Cambia el nombre por el alias para mostrar en la grilla
                    foreach (var item in GridColumns.ZambaColumns)
                    {
                        if (AsociatedResults.Columns.Contains(item.Value))
                            AsociatedResults.Columns[item.Value].ColumnName = item.Key;
                    }

                    foreach (DataColumn c in AsociatedResults.Columns)
                    {
                        string id = IndexsBusiness.GetIndexIdByName(c.ColumnName).ToString();
                        if (id != "0")
                        {
                            sr.Index.Add((new IndexsBusiness()).GetIndex(Convert.ToInt64(id)));
                        }

                    }
                    var ValueColums = AsociatedResults.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

                    sr.columns = ValueColums;

                    String CustomOrderBy = string.Empty;
                    string tempOrderBy = string.Empty;
                    string sortOp = string.Empty;


                    if (!string.IsNullOrEmpty(customUserAssociatedOrderBy))
                    {
                        sr.OrderBy = customUserAssociatedOrderBy;

                        if (customUserAssociatedOrderBy.ToLower().Contains(" desc"))
                            sortOp = "desc";
                        else
                            sortOp = "asc";

                        var indexof = customUserAssociatedOrderBy.IndexOf(sortOp);
                        if (indexof == -1) indexof = customUserAssociatedOrderBy.Length;
                        tempOrderBy = customUserAssociatedOrderBy.Substring(0, indexof).ToString().Trim();
                        tempOrderBy = GridColumns.GetColumnNameByAliasName(tempOrderBy);

                        if (tempOrderBy.Contains("_"))
                        {
                            tempOrderBy = tempOrderBy.Replace("_", " ");

                            if (tempOrderBy.Split().Length > 1)
                                tempOrderBy = string.Format("{0}", tempOrderBy.Trim());
                        }

                        //Se pregunta si el result tiene filas y si esta filtrando por columna

                        if (Zamba.Servers.Server.isOracle)
                        {
                            CustomOrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                        }
                        else
                        {
                            CustomOrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                        }
                    }

                    if (AsociatedResults.Rows.Count > 0 && CustomOrderBy.Length > 0)
                    {
                        AsociatedResults.DefaultView.Sort = CustomOrderBy;
                        AsociatedResults.AcceptChanges();

                        sr.data = AsociatedResults.DefaultView.ToTable();

                    }
                    else
                    {
                        sr.data = AsociatedResults;
                    }
                    searchResultList srl = new searchResultList();
                    List<List<searchResultList>> Lista = new List<List<searchResultList>>();
                    sr.data.AsEnumerable().ToList().ForEach(n =>
                    {
                        Lista.Add(n.ItemArray
                        .AsEnumerable()
                        .Select((value, index) => new searchResultList
                        {
                            Column = sr.data.Columns[index].ColumnName,
                            value = value.ToString(),
                            Type = Convert.ToInt32(SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).Type),
                            Len = SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).Len,
                            DropDownList = SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).DropDownList,
                            DropDown = Convert.ToInt32(SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).DropDown),
                            ID = SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).ID,
                            Name = SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).Name,
                            Required = Convert.ToBoolean(SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).Required),
                            DefaultValue = SearchValueList(sr.Index, sr.data.Columns[index].ColumnName).DefaultValue
                        }
                        )
                        .ToList()
                        );
                    });
                    var newresults = JsonConvert.SerializeObject(Lista, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        public IIndex SearchValueList(List<IIndex> indices, string ColumnName)
        {
            return indices.Where(n => n.Name == ColumnName).Count() == 1
                ? indices.Where(n => n.Name == ColumnName).First()
                : new Zamba.Core.Index { };
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [OverrideAuthorization]
        [Route("api/search/NewgetAssociatedlist")]

        public IHttpActionResult NewgetAssociatedlist(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                try
                {
                    Int64 id = 0;
                    Int64 value = 0;
                    DataTable dim = new DataTable();
                    List<string> List = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        id = Int64.Parse(paramRequest.Params["id"].ToString());
                        value = Int64.Parse(paramRequest.Params["value"]);
                    }

                    if (value == 2)
                    {
                        AutoSubstitutionBusiness ASB = new AutoSubstitutionBusiness();
                        dim = ASB.GetIndexData(id, false);

                        var newresults = JsonConvert.SerializeObject(dim, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                        return Ok(newresults);

                    }

                    if (value == 1)
                    {
                        //var diccionario = new Dictionary<string, string>();
                        List = IndexsBusiness.GetDropDownList(id);
                        var a = List.Select((obj, index) => new { Codigo = index, Descripcion = obj });

                        var newresults = JsonConvert.SerializeObject(a, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                        return Ok(newresults);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [OverrideAuthorization]
        [Route("api/search/InsertResultGrid")]
        public IHttpActionResult InsertResultGrid(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                try
                {
                    CommonFuntions cf = new CommonFuntions();

                    var user = cf.GetUser(paramRequest.UserId);
                    Int32 EntityId = Int32.Parse(paramRequest.Params["EntityId"]);
                    Int32 parentEntityid = Int32.Parse(paramRequest.Params["parentEntityid"]);
                    Int32 ParantDocId = Int32.Parse(paramRequest.Params["ParantDocId"]);
                    string result = paramRequest.Params["result"];
                    Dictionary<long, string> dicFormVariables = new Dictionary<long, string>();
                    List<itemVarsList> listFormVariables = JsonConvert.DeserializeObject<List<itemVarsList>>(result);

                    for (int i = 0; i < listFormVariables.Count; i++)
                    {
                        if (listFormVariables[i].ID.ToString() != "0" && listFormVariables[i].newValue != null)
                        {
                            dicFormVariables.Add(long.Parse(listFormVariables[i].ID.ToString()), listFormVariables[i].newValue);
                        }

                    }
                    WFTaskBusiness WTB = new WFTaskBusiness();
                    ITaskResult ParentTask = WTB.GetTaskByDocId(ParantDocId, user.ID);
                    if (ParentTask != null)
                    {
                        SResult sResult = new SResult();
                        //Nuevo resultado                

                        INewResult newresult = new SResult().GetNewNewResult(EntityId);
                        newresult.DocTypeId = EntityId;
                        newresult.UserId = user.ID;


                        //loop por cada atributo(index) del padre y heredar los valores al hijo
                        foreach (IIndex PI in ParentTask.Indexs)
                        {
                            cf.SetIndexData(PI.Data, PI.ID, newresult.Indexs);
                        }

                        //loop por cada Indexs(columnas) del result que viene de la grilla

                        foreach (var item in dicFormVariables)
                        {
                            cf.SetIndexData(item.Value, item.Key, newresult.Indexs);
                        }

                        InsertResult insertResult = new InsertResult();
                        Results_Business RB = new Results_Business();
                        insertResult = RB.Insert(ref newresult, true, false, false, false, true, false, false, false, true, user.ID);


                        var newresults = JsonConvert.SerializeObject(insertResult, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                        return Ok(newresults);


                    }

                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [OverrideAuthorization]
        [Route("api/search/setTaskIndexsSaveTable")]
        public IHttpActionResult setTaskIndexsSaveTable(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                CommonFuntions cf = new CommonFuntions();

                var user = cf.GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 ResultId = 0;
                    Int64 EntityId = 0;
                    Int64 TaskId = 0;
                    IResult result = null;

                    if (paramRequest.Params != null)
                    {
                        ResultId = Int64.Parse(paramRequest.Params["resultId"]);
                        EntityId = Int64.Parse(paramRequest.Params["entityId"]);

                        if (paramRequest.Params.ContainsKey("taskId"))
                        {
                            TaskId = Int64.Parse(paramRequest.Params["taskId"]);
                        }

                        Results_Business RB = new Results_Business();

                        if (TaskId != 0)
                        {
                            WFTaskBusiness WTB = new WFTaskBusiness();
                            result = WTB.GetTask(TaskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        }
                        else
                        {
                            result = RB.GetResult(ResultId, EntityId, true);
                        }
                        string Indexs = paramRequest.Params["Indexs"];

                        List<IndexsObject> IndexsObject = new JavaScriptSerializer().Deserialize<List<IndexsObject>>(Indexs);

                        foreach (IndexsObject I in IndexsObject)
                        {
                            Int64 IndexId = I.ID;
                            string indexValue = I.Data;

                            result.get_GetIndexById(I.ID).Data = I.Data;
                            result.get_GetIndexById(I.ID).DataTemp = I.Data;

                            RB.SaveModifiedIndexData(ref result, true, false, null, null);
                        }
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return Ok(false);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/DeleteAsociatedResult")]
        [OverrideAuthorization]
        public void DeleteAsociatedResult(genericRequest paramRequest)
        {
            SResult sResult = new SResult();
            if (paramRequest != null)
            {
                try
                {
                    CommonFuntions cf = new CommonFuntions();
                    sResult.deleteBaremo(cf.GetResultFromParamRequest(paramRequest));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("El parametro es nulo.");
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/InsertResultGridOneResult")]
        [OverrideAuthorization]
        public IHttpActionResult InsertResultGridOneResult(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                try
                {
                    CommonFuntions cf = new CommonFuntions();

                    var user = cf.GetUser(paramRequest.UserId);
                    Int32 EntityId = Int32.Parse(paramRequest.Params["EntityId"]);
                    Int32 parentEntityid = Int32.Parse(paramRequest.Params["parentEntityid"]);
                    Int32 ParantDocId = Int32.Parse(paramRequest.Params["ParantDocId"]);
                    //string result = paramRequest.Params["result"];
                    //Dictionary<long, string> dicFormVariables = new Dictionary<long, string>();
                    //List<itemVarsList> listFormVariables = JsonConvert.DeserializeObject<List<itemVarsList>>(result);

                    //for (int i = 0; i < listFormVariables.Count; i++)
                    //{
                    //    if (listFormVariables[i].ID.ToString() != "0" && listFormVariables[i].newValue != null)
                    //    {
                    //        dicFormVariables.Add(long.Parse(listFormVariables[i].ID.ToString()), listFormVariables[i].newValue);
                    //    }

                    //}
                    WFTaskBusiness WTB = new WFTaskBusiness();
                    ITaskResult ParentTask = WTB.GetTaskByDocId(ParantDocId, user.ID);
                    if (ParentTask != null)
                    {
                        SResult sResult = new SResult();
                        //Nuevo resultado                

                        INewResult newresult = new SResult().GetNewNewResult(EntityId);
                        newresult.DocTypeId = EntityId;
                        newresult.UserId = user.ID;


                        //loop por cada atributo(index) del padre y heredar los valores al hijo
                        foreach (IIndex PI in ParentTask.Indexs)
                        {
                            cf.SetIndexData(PI.Data, PI.ID, newresult.Indexs);
                        }

                        //loop por cada Indexs(columnas) del result que viene de la grilla

                        //foreach (var item in dicFormVariables)
                        //{
                        //    cf.SetIndexData(item.Value, item.Key, newresult.Indexs);
                        //}

                        InsertResult insertResult = new InsertResult();
                        Results_Business RB = new Results_Business();
                        insertResult = RB.Insert(ref newresult, true, false, false, false, true, false, false, false, true, user.ID);


                        var newresults = JsonConvert.SerializeObject(insertResult, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                        return Ok(newresults);


                    }

                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        internal class IndexsObject
        {
            public long ID { get; set; }
            public string Data { get; set; }
        }

        public class itemVarsList
        {
            public Int32 ID { get; set; }
            public string newValue { get; set; }
        }

        private class searchResult
        {
            public DataTable data { get; set; }
            public List<string> columns { get; set; } = new List<string>();
            public string columnsStringAssociated { get; set; }
            public string columnsStringMyTasks { get; set; }
            public string columnsStringTeam { get; set; }
            public string columnsStringAll { get; set; }
            public List<EntityDto> entities { get; set; } = new List<EntityDto>();
            public List<Filters> filters { get; set; } = new List<Filters>();
            public long total { get; set; } = 0;

            public string OrderBy { get; set; }
            public string VirtualEntities { get; internal set; }

            public class Filters
            {
                public string Name { get; set; }
                public string Value { get; set; }


            }
            public List<IIndex> Index { get; set; } = new List<IIndex>();

        }

        private class searchResultList
        {
            public object value { get; set; }
            public long Type { get; set; }
            public long Len { get; set; }
            public List<String> DropDownList { get; set; }
            public int DropDown { get; set; }
            public string Column { get; set; }
            public int IconId { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
            public bool Required { get; set; }
            public string DefaultValue { get; set; }


        }

    }
}