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
    // [Table("HelperModule")]
    public class HelpModule
    {
        [Key]
        public int Id { get; set; }
        public string Module { get; set; }
        public int HelpApplicationId { get; set; }
        //[NotMapped]
        //public HelpItem ModItem { get; set; }
        public virtual HelpApplication HelpApplication { get; set; }
        [NotMapped]
        public List<HelpFunction> Functions { get; internal set; }
        //public virtual HashSet<HelpFunction> HelpFunctions { get; set; }
        //public HelpModule()
        //{
        //    HelpFunctions = new HashSet<HelpFunction>();
        //}
        public int OrderId { get; set; }

    }
}