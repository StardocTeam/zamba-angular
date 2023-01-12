using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;

namespace ZambaWeb.RestApi.Models
{
    public class Index
    {
        public string name;
        public long id;
        public long parent;
        public SearchFilterType type;

        public Index(long id, string name , long parent) 
        {
            this.name = name;
            this.id = id;
            this.parent = parent;
            this.type = SearchFilterType.Attribute;
        }
    }
}
