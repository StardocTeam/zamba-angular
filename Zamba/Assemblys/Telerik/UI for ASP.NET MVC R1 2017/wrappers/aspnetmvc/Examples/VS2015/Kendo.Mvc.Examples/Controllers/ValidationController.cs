using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using Kendo.Mvc.Examples.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mvc3RemoteVal.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        public ValidationController()
        {
        }

        public JsonResult IsProductName_Available(string ProductName)
        {
            var northwind = new SampleEntities();
            Product existingProduct = northwind.Products.FirstOrDefault(product => product.ProductName == ProductName);
            if (existingProduct == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            string suggestedName = String.Format(CultureInfo.InvariantCulture,
                "{0} is not available.", ProductName);

            for (int i = 1; i < 100; i++)
            {
                string altName = ProductName + i.ToString();
                if (northwind.Products.FirstOrDefault(product => product.ProductName == altName) == null)
                {
                    suggestedName = String.Format(CultureInfo.InvariantCulture,
                   "{0} is not available. Try {1}.", ProductName, altName);
                    break;
                }
            }
            return Json(suggestedName, JsonRequestBehavior.AllowGet);
        }
    }
}