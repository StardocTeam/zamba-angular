using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor
{
    public interface IHtmlElement
    {
        Boolean Enabled { get; set; }
        HtmlControlType Type { get;  }
        Boolean Required { get; set; }
    }
}
