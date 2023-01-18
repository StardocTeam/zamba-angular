using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.Framework
{
    public class kendoFilter: ikendoFilter
    {
      public  string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public string DataBaseColumn { get; set; }

        public bool Enabled { get; set; }

        public long FilterID { get; set; }
    }
}
