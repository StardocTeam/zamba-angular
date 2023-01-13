using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zamba.Help.Models
{
  //  [Table("HelperType")]
    public class HelpType
    {

        public int Id { get; set; }
        public string Type { get; set; }
    }
}