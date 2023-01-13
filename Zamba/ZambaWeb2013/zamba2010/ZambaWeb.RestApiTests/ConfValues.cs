using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZambaWeb.RestApiTests
{
    public class ConfValues
    {
        public class ValidUser
        {
            public int ID { get; set; } = 2;
            public string userName { get; set; } = "administrador";
            public string password { get; set; } = "";
            public string computerNameOrIp { get; set; }  =  "::1/fczddgt4z5ikwqdpwlaabme1";           
          
        }

        public class RestApi
        {
            public string URL { get; set; } = "http://localhost/zambaweb.restapi/";
            public string API => this.URL + "api/";
        }
    }
}
