using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZambaWeb.RestApiTests.ConfValues;

namespace ZambaWeb.RestApiTests.Helpers
{
   public class CredentialsHelper
    {
        public static Dictionary<string, string> ValidUserDictionary()
        {
            ValidUser user = new ValidUser();
            var d = new Dictionary<string, string>();
            d.Add("userName", user.userName);
            d.Add("password", user.password);
            d.Add("computerNameOrIp", user.computerNameOrIp);
            return d;
        }
        public static Dictionary<string, string> InValidUserDictionary()
        {
            ValidUser user = new ValidUser();
            var d = new Dictionary<string, string>();
            d.Add("userName", "invalidUser");
            d.Add("password", "invalidpassword");
            d.Add("computerNameOrIp", "invalidCPU");
            return d;
        }
        public static Dictionary<string, string> EmptyUserDictionary()
        {
            ValidUser user = new ValidUser();
            var d = new Dictionary<string, string>();
            d.Add("userName", string.Empty);
            d.Add("password", string.Empty);
            d.Add("computerNameOrIp", string.Empty);
            return d;
        }
    }
}
