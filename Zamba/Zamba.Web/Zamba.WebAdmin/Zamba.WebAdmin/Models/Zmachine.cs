using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zamba.WebAdmin.Models
{
    public class Zmachine
    {


        public string DefaultValue { get; set; }

        public int section { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

    }


    public class ZuserConfig
    {


        public string DefaultValue { get; set; }

        public string User { get; set; }

        public int section { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

    }


    public class Zopt
    {

        public string Name { get; set; }

        public string Value { get; set; }

    }
}