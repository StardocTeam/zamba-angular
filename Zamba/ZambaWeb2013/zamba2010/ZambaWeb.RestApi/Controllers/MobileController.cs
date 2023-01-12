using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZambaWeb.RestApi.Controllers
{
    public class MobileController : ApiController
    {
        // GET: api/Mobile
        public List<Menu> Get()
        {
            List<Menu> Menus = new List<Menu>();
            Menus.Add(new Menu() { title= "Buscar", component= "SearchPage", icon= "swap" });
            Menus.Add(new Menu() { title= "Nuevo", component= "SearchPage", icon= "swap" });
            return Menus;
        }

        public class Menu
        {
           public string title { get; set; }
           public string component { get; set; }
           public string icon { get; set; }
        }
        // GET: api/Mobile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mobile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mobile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mobile/5
        public void Delete(int id)
        {
        }
    }
}
