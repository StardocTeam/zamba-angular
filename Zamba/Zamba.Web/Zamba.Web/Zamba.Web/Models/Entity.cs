using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Framework;

namespace Zamba.Web.Models
{
    public class Entity
    {
        public Entity(long id, string name, long parent)
        {
            this.id = id;
            this.type = SearchFilterType.Entity;
            this.parent = parent;
            this.name = name;
            this.indexes = new List<IIndex>();
        }
        public void addIndex(IIndex index)
        {
            this.indexes.Add(index);
        }
        public string name;
        public long id;
        public long parent;
        public List<IIndex> indexes;
        public SearchFilterType type;
    }
}