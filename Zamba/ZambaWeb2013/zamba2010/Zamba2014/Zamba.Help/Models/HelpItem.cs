using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Zamba.Help.Models
{
    // [Table("HelperItem")]
    public class HelpItem
    {
        #region Constructors
        public HelpItem() { }
        public HelpItem(int id, string code, string title, string name, int orderId)
        {
            this.Id = id;
            this.Code = code;
            this.Title = title;
            this.Name = name;
            this.OrderId = orderId;
        }
        public HelpItem(int id, string code, string title, string name, string type, string app, string mod, string fn, bool forAllUsers, int orderId)
        {
            this.Id = id;
            this.Code = code;
            this.Title = title;
            this.Name = name;
            this.HelpType =new HelpType{ Id = 0, Type= type };
            this.ForAllUsers = forAllUsers;
            this.OrderId = orderId;

            this.HelpFunction = new HelpFunction
            {
                Id = 0,
                Function = fn,
                HelpModule = new HelpModule
                {
                    Id = 0,
                    Module = mod,
                    HelpApplication = new HelpApplication
                    {
                        Id = 0,
                        Application = app,                        
                    }
                }
            };
        }
        #endregion
        #region Fields
        [Key]
        public int Id { get; set; }
        public int HelpFunctionId { get; set; }
        [ScriptIgnore]
        public virtual HelpFunction HelpFunction { get; set; }
        public int HelpTypeId { get; set; }
        public virtual HelpType HelpType { get; set; }

        [Display(Name = "Codigo")]
        public string Code { get; set; }
        public string Title { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido breve")]
        public string ShortContent { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido completo")]
        public string FullContent { get; set; }

        [AllowHtml]
        [Display(Name = "Publico")]
        public bool ForAllUsers { get; set; }

        public int OrderId { get; set; }

        #endregion
    }

}