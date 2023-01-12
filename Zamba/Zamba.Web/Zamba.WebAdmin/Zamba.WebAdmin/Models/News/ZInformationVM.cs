using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zamba.WebAdmin.Models.News
{
    [NotMapped]
    public class ZInformationVM
    {
        public int Id { get; set; }
   
        public string Title { get; set; }
   
        public string Content { get; set; }
        public bool Readed { get; set; }
        public DateTime Created { get; set; }
    }
}