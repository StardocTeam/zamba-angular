using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Models
{
    
    public class Entity
    {
       public string name;
        public long id;
        public long parent;
        public List<Index> indexes;
        public long entity_type;
        public SearchFilterType type;

        public Entity()
        {
           
        }
        public Entity(long id, string name, long parent, long entity_type)
        {
            this.id = id;
            this.type = SearchFilterType.Entity;
            this.parent = parent;
            this.name = name;
            this.indexes = new List<Index>();
            this.entity_type = entity_type;
        }
        public void addIndex(Index index)
        {
            this.indexes.Add(index);
        }   
 
    }
}