using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Examples.Models;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class Mobile_ListViewController : Controller
    {       
        public ActionResult Local_Virtualization()
        {
            return View(productService.Read());
        }        
    }
}
