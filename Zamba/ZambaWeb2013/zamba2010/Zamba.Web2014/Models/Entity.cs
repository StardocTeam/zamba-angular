using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;

namespace ZambaWeb.RestApi.Models
{
    public class Entity
    {
        public Entity(long id, string name, long parent)
        {
            this.id = id;
            this.type = SearchFilterType.Entity;
            this.parent = parent;
            this.name = name;
            this.indexes = new List<Index>();
        }
        public void addIndex(Index index)
        {
            this.indexes.Add(index);
        }
        public string name;
        public long id;
        public long parent;
        public List<Index> indexes;
        public SearchFilterType type;
    }
}