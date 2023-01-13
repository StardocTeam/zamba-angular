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

namespace ZambaWeb.RestApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TasksController : ApiController
    {
        // GET api/tasks
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tasks/5
        public string Get(int id)
        {
            return "value";
        }

     
        // PUT api/tasks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tasks/5
        public void Delete(int id)
        {
        }
    }
}
