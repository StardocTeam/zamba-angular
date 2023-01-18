using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.Core;
using Zamba.Core.Users;

namespace Zamba.Help.Models
{
    public class LoginViewModel //: Controller
    {

        [Required]
        [Display(Name = "Usuario")]
        public string user { get; set;}

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}