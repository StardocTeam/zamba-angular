using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zamba.Help.Models
{
    public class ListOptions
    {
        public List<HelpApplication> Applications { get; set; }
        public List<HelpType> Types { get; set; }
    }

    public class HelpTreeItem
    {
        public HelpTreeItem()
        {
            ChildItems = new List<HelpTreeItem>();
            HelpItems = new List<HelpItem>();
        }
        public string Name { get; set; }
        public List<HelpTreeItem> ChildItems { get; set; }
        public List<HelpItem> HelpItems { get; set; }
    }
    public class HelpItemVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        public string Module { get; set; }
        public string Function { get; set; }
        public bool ForAllUsers { get; set; }
        public int OrderId { get; set; }
    }

    public class HelpItemWithPermitions: UserPermitions
    {
        public List<HelpItem> HelpItems { get; set; }     
    }
    public class UserPermitions
    {
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }
    }
    //public class AppVM
    //{
    //    public AppVM()
    //    {
    //        ModVM = new List<ModVM>();
    //    }
    //    public string ApplicationName { get; set; }
    //    public List<ModVM> ModVM { get; set; }
    // //   public HelpItem AppItem { get; set; }
    //}

    //public class ModVM
    //{
    //    public ModVM()
    //    {
    //        FunVM = new List<FunVM>();
    //    }
    //    public string ModuleName { get; set; }
    //    public List<FunVM> FunVM { get; set; }
    //  //  public HelpItem ModItem { get; set; }
    //}
    //public class FunVM
    //{
    //    public FunVM()
    //    {
    //        HelpItem = new List<HelpItem>();
    //    }
    //    public string FunctionName { get; set; }
    //    public List<HelpItem> HelpItem { get; set; }
    //}
}