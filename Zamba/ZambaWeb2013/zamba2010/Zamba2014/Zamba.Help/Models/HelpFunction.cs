using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace Zamba.Help.Models
{
    // [Table("HelperFunction")]
    public class HelpFunction
    {
        [Key]
        public int Id { get; set; }
        public string Function { get; set; }
        public int HelpModuleId { get; set; }
        public virtual HelpModule HelpModule { get; set; }
        [NotMapped]
        public List<HelpItem> Items { get; internal set; }
        //public virtual HashSet<HelpItem> HelpItems { get; set; }
        //public HelpFunction()
        //{
        //    HelpItems = new HashSet<HelpItem>();
        //}

        public int OrderId { get; set; }
    }
}