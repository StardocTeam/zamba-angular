using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Controllers.Task.WFTree
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/WF")]
    public class WFController : ApiController
    {

        private class WFTreeDTO
        {
            public List<EntityView> workflows { get; set; }
            public Int64 lastWFId { get; set; } = 0;
            public Int64 lastStepId { get; set; } = 0;
            public Int64 lastStateId { get; set; } = 0;
        }


        [AcceptVerbs("GET", "POST")]
        [Route("GetWF")]
        public IHttpActionResult GetWF(Int64 UserId)
        {
            try
            {

                var user = GetUser(UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".

                WFTreeDTO WT = new WFTreeDTO();
                SWorkflow SWorkflow = new SWorkflow();

                // Obtiene los workflows
                WT.workflows = SWorkflow.GetUserWFIdsAndNamesWithSteps(UserId);

                WFTaskBusiness WTB = new WFTaskBusiness();
                RightsBusiness RB = new RightsBusiness();


                // Recorre los workflows y los agrega
                foreach (EntityView wf in WT.workflows)
                {
                    wf.ObjecttypeId = (int)ObjectTypes.ModuleWorkFlow;
                    wf.TasksCount = 0;
                    // Agrega las etapas
                    foreach (EntityView wfStepNode in wf.ChildsEntities)
                    {
                        wfStepNode.ObjecttypeId = (int)ObjectTypes.WFSteps;

                        //Esto es solo si quisieramos traer el conteo, pero tarda mucho.
                        //wfStepNode.ChildCount = WTB.GetTaskCount(wfStepNode.ID, true, UserId);
                        //wf.TasksCount += wfStepNode.TasksCount;
                        wfStepNode.TasksCount = 0;

                        // Agrega los estados
                        if (RB.GetUserRights(UserId, ObjectTypes.WFSteps, RightsType.ShowStates, wfStepNode.ID))
                        {

                            List<IWFStepState> states = WFStepStatesComponent.GetStepStatesByStepId(wfStepNode.ID);

                            foreach (IWFStepState st in states)
                            {
                                EntityView StateNode = new EntityView(st.ID, st.Name, 0);
                                StateNode.ParentId = wfStepNode.ID;
                                StateNode.ObjecttypeId = (int)ObjectTypes.WFStates;
                                StateNode.TasksCount = 0;
                                wfStepNode.ChildsEntities.Add(StateNode);
                            }
                        }
                    }
                }

                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                // se para en ultimo WFUtilizado
                string lastWfUsed = UP.getValue("UltimoWFUtilizado", UPSections.WorkFlow, string.Empty);

                if (!string.IsNullOrEmpty(lastWfUsed) && Information.IsNumeric(lastWfUsed))
                {
                    WT.lastWFId = Int64.Parse(lastWfUsed);
                }

                string lastWfStepUsed = UP.getValue("UltimoWFStepUtilizado", UPSections.WorkFlow, string.Empty);
                if (Information.IsNumeric(lastWfStepUsed))
                    WT.lastStepId = System.Convert.ToInt64(lastWfStepUsed);


                return Ok(WT);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.ToString())));

            }
        }


        private Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("User"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
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


        private class CountDTO
        {
            public Int64 TotalCount { get; set; } = 0;
            public Int64 ID { get; set; }
            public string Name { get; set; }
            public Int64 ObjecttypeId { get; set; }
        }
        [AcceptVerbs("GET", "POST")]
        [Route("GetWFAndStepIdsAndNamesAndTaskCount")]
        public IHttpActionResult GetWFAndStepIdsAndNamesAndTaskCount(Int64 UserId, Boolean useCache)
        {
            try
            {
                var user = GetUser(UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                List<CountDTO> rv = new List<CountDTO>();
                if (useCache && !Zamba.Core.Cache.Workflows.hsWFAndStepIdsAndNamesAndTaskCount.Contains(UserId))
                {
                    lock (Zamba.Core.Cache.Workflows.hsWFAndStepIdsAndNamesAndTaskCount)
                    {
                        //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".


                        SWorkflow SWorkflow = new SWorkflow();

                        // Obtiene los workflows
                        List<EntityView> workflows = SWorkflow.GetUserWFIdsAndNamesWithSteps(UserId);

                        WFTaskBusiness WTB = new WFTaskBusiness();
                        RightsBusiness RB = new RightsBusiness();


                        // Recorre los workflows y los agrega
                        foreach (EntityView wf in workflows)
                        {
                            CountDTO WFC = new CountDTO();
                            WFC.ID = wf.ID;
                            WFC.ObjecttypeId = (int)ObjectTypes.ModuleWorkFlow;
                            WFC.TotalCount = 0;

                            foreach (EntityView wfStepNode in wf.ChildsEntities)
                            {
                                CountDTO WTC = new CountDTO();
                                WTC.ID = wfStepNode.ID;
                                WTC.ObjecttypeId = (int)ObjectTypes.WFSteps;
                                WTC.TotalCount = WTB.GetTaskCount(wfStepNode.ID, true, UserId);
                                WFC.TotalCount += WTC.TotalCount;
                                rv.Add(WTC);
                            }
                            rv.Add(WFC);
                        }

                        if (Zamba.Core.Cache.Workflows.hsWFAndStepIdsAndNamesAndTaskCount.Contains(UserId) == false)
                        {
                            Zamba.Core.Cache.Workflows.hsWFAndStepIdsAndNamesAndTaskCount.Add(UserId, rv);
                        }
                        return Ok(rv);
                    }

                }

                return Ok(Zamba.Core.Cache.Workflows.hsWFAndStepIdsAndNamesAndTaskCount[UserId]);

            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.ToString())));

            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("SetLastWFSelected")]
        public IHttpActionResult SetLastWFSelected(Int64 UserId, string IDselected, string selectedView)
        {
            try
            {

                var user = GetUser(UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".

                UserPreferencesFactory.setValueDB("SelectedNodeeTreView-" + selectedView, Convert.ToString(IDselected), UPSections.UserPreferences, user.ID);

                return Ok();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.ToString())));

            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("GetLastWFSelected")]
        public IHttpActionResult GetLastWFSelected(Int64 UserId, string selectedView)
        {
            try
            {

                var user = GetUser(UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".

                var selectedNode = UserPreferencesFactory.getValueDB("SelectedNodeeTreView-" + selectedView, UPSections.UserPreferences, user.ID);
                return Ok(selectedNode);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.ToString())));

            }
        }

    }
}