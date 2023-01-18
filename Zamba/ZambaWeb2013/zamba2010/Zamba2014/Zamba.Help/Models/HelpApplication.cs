using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Zamba.Help.Models
{
    //[Table("HelperApplication")]
        public class HelpApplication
    {
        [Key]
        public int Id { get; set; }
        public string Application { get; set; }
        //[NotMapped]
        //public  HelpItem AppItem { get; set; }

        [NotMapped]
        public List<HelpModule> Modules { get; internal set; }

        //public virtual HashSet<HelpModule> HelpModules { get; set; }

        //public HelpApplication() {
        //    HelpModules = new HashSet<HelpModule>();
        //}

        public int OrderId { get; set; }
    }
}