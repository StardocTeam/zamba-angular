using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.ViewModels
{
    public class AccountVM
    {
    }
    public class LoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComputerNameOrIp { get; set; }

        public string name { get; set; }
        public string lastName { get; set; }
        public string eMail { get; set; }
        public string type { get; set; }


    }


    public class UserChangePassword
    {

        public int Userid { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPassword2 { get; set; }
    }

}