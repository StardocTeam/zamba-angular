using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kendo.Mvc.Examples.Models.SpreadStreamProcessing
{
    public class Client
    {
        public Client(string name, string company)
        {
            this.Name = name;
            this.Company = company;
        }

        public string Name { get; private set; }

        public string Company { get; private set; }
    }
}