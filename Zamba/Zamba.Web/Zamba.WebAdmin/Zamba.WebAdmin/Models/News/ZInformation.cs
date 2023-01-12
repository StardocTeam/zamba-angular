using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Zamba.WebAdmin.Models.News
{
    public class ZInformation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Repetir noticia")]
        public int Repeat { get; set; }
   
        [Display(Name = "Importancia")]
        public int Important { get; set; }

        [Display(Name = "Fecha de creacion")]
        public DateTime Created { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido breve")]
        public string ShortContent { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido completo")]
        public string FullContent { get; set; }

         [NotMapped]
        public bool Readed { get; set; }
        // [NotMapped]
        [ScriptIgnore]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual List<ZInformationUser> ZInformationUser { get;  set; }
    }
}
