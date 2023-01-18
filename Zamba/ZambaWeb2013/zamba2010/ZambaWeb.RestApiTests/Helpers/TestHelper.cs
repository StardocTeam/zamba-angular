using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZambaWeb.RestApiTests.Helpers
{
    public class TestHelper
    {
        public static string GetExceptionString(WebException ex)
        {
            var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            dynamic obj = JsonConvert.DeserializeObject(resp);
            string messageFromServer = obj.GetType().Name == "String" ? obj.ToString() : obj.Message.ToString();
            return messageFromServer;
        }
    }
}
