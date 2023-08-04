using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;
using Zamba.Services;

namespace ZambaWeb.RestApi.Controllers.Web
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Toolbar")]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class ToolbarController : ApiController
    {


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetTaskHistory")]
        [OverrideAuthorization]
        public DataSet GetTaskHistory(Int64 DocId)
        {
            DataSet ListHistory; 
            STasks Tasks = new STasks();
            var task = Tasks.GetTaskIdAndNameByDocId(DocId);
            ListHistory = Tasks.GetTaskHistory(task.ID);
            return ListHistory;

        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetAssociates")]
        [OverrideAuthorization]
        public DataTable GetAssociates(long DocId,long DocTypeId,long Userid)
        {
            STasks Tasks = new STasks();
            DataTable ListAssociates;
            SResult SResult = new SResult();
            var result =  SResult.GetResult(DocId, DocTypeId, true);

            ListAssociates = Tasks.getAsociatedDTResultsFromResult(result, 0, false, Userid, true);

            return ListAssociates;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetEmailHistory")]
        [OverrideAuthorization]
        public DataSet GetEmailHistory(long docId)
        {

            DataSet dt = EmailBusiness.getHistory(docId);
            return dt;
        }

    }
}
