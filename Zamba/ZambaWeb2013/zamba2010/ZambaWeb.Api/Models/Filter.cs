using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core.Searchs;
using Zamba.Core;

namespace ZambaWeb.RestApi.Models
{
    public class Filter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public SearchFilterType Type { get; set; }

        //public bool EditMode { get; set; }
        //public object Placeholder { get; set; }
    }
}