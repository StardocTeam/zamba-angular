using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Zamba.WebAdmin.Models.News
{
  public  class ZInformationUser
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Id Usuario")]
        public Int64 UserId { get; set; }

        public int ZInformationId { get; set; }
        public virtual ZInformation ZInformation { get; set; }
        

        public int Readed { get; set; }
        public DateTime LastRead { get; set; }
    }
}
