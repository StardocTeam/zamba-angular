using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers.Forum
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class ForumController : ApiController
    {
        public ForumController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.WebRS");
            }
        }
        private Zamba.Core.IUser GetUser()
        {
            var user = TokenHelper.GetUser(User.Identity);
            return user;
        }

        public class MessageDto
        {
            public string avatar { get; set; }
            public string messagedate { get; set; }
            public string name { get; set; }
            public long id { get; set; }
        }
        private class ForumSearchResult
        {
            public int id { get; set; }
            public string name { get; set; }
            public List<MensajeForo> messages { get; set; } = new List<MensajeForo>();

        }



        public class ForumSearchDto
        {
            public Int64 UserId { get; set; }
            public Int64 ResultId { get; set; }
            public long LastPage { get; set; } = 0;
            public int PageSize { get; set; } = 100;

            public string OrderBy { get; set; }

            public List<Object> Filters { get; set; }

        }
        [OverrideAuthorization]
        [Route("api/Forum/Forum")]
        [HttpPost]
        [HttpGet]
        public string Forum(ForumSearchDto forumSearch)
        {
            ZForoBusiness FB = new ZForoBusiness();
            UserBusiness UBR = new UserBusiness();

            try
            {
                IUser User = UBR.ValidateLogIn(forumSearch.UserId, ClientType.WebApi);
                if (User != null)
                {
                    ForumSearchResult sr = new ForumSearchResult();

                    sr.messages = FB.GetAllMessages(forumSearch.ResultId);


                    var newresults = JsonConvert.SerializeObject(sr, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return newresults;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener la mensajes: " + ex.ToString());
            }
        }



        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}