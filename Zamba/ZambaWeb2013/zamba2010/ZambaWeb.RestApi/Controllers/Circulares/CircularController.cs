using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zamba.Core;
using ZambaWeb.RestApi.Models;

namespace ZambaWeb.RestApi.Controllers
{
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class CircularController : ApiController
    {
         public CircularController()
		{
			if (Zamba.Servers.Server.ConInitialized == false)
			{
				Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
				ZC.InitializeSystem("Zamba.Web");
                
                
                // IUser currentUser = UserBusiness.ValidateLogIn(22357, ClientType.WebApi);
            }
		}
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Circular/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Circular
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Circular/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Circular/5
        public void Delete(int id)
        {
        }
  
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Circular/Base64FromCircular")]
        [OverrideAuthorization]
        public string Base64FromCircular(CircularImage circularimage)
        {
            try
            {
                string[] s = circularimage.Base64Url.Split(',');
                byte[] data = Convert.FromBase64String(s[1]);
  
                var localFile = System.AppDomain.CurrentDomain.BaseDirectory +
                             "/Content/images/" + Guid.NewGuid() + Path.GetExtension(".png");

                File.WriteAllBytes(localFile, data);
      
                return localFile;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Circular/Base64")]
        [OverrideAuthorization]
        public string Base64(string str)
        {
            try
            {
                string[] s = str.Split(',');
                byte[] data = Convert.FromBase64String(s[1]);

                var localFile = System.AppDomain.CurrentDomain.BaseDirectory +
                             "/Content/images/" + Guid.NewGuid() + Path.GetExtension(".png");

                File.WriteAllBytes(localFile, data);

                return localFile;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
