using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Models
{
    public class Index
    {
        public string name;
        public long id;
        public long parent;
        public long index_type;
        public SearchFilterType type;     

        public Index(long id, string name, long parent, long index_type) 
        {
            this.name = name;
            this.id = id;
            this.parent = parent;
            this.type = SearchFilterType.Attribute;
            this.index_type = index_type;       
        }       
    }

}
