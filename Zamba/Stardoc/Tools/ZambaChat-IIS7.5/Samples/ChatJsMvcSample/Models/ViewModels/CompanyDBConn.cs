using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatJsMvcSample.Models.ViewModels
{
    public class CompanyDBConn
    {
       // public int Id { get; set; }
        public string Company { get; set; }
        public string InitialCatalog { get; set; }
        public string DataSource { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}