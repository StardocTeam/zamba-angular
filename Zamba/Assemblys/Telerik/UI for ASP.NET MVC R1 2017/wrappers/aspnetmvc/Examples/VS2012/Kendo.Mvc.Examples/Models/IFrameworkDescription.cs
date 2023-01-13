﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kendo.Mvc.Examples.Models
{
    public interface IFrameworkDescription
    {
        string Name { get; }

        IEnumerable<ExampleFile> GetFiles(HttpServerUtilityBase server, string example, string section);
    }
}
