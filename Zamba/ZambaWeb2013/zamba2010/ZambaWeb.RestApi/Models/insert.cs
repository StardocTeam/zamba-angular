using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Models
{
    public partial class insert
    {
      
        public file file { get; set; }
        public long DocTypeId { get; set; }
        //public string name{ get; set; }
        public long Userid { get; set; }
        public List<Indexs> indexs { get; set; }

        public long ReturnId { get; set; }

        public string OriginalFileName{ get; set; }

    }


    public partial class insertResult
    {
        public long Id { get; set; }
        public string msg{ get; set; }
        public long ReturnId { get; set; }
        public string ReturnValue { get; set; }
        public string error { get; set; }
    }

    public class file 
    {
        public string data { get; set;}
        public string extension { get; set;}
    }


    public class Indexs
    {
        public long id { get; set; }

        public string value { get; set; }

    }


}