using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stardoc.HtmlEditor
{
    interface IZambaHtmlControls
        : IHtmlWritable
    {
        Int64 IndexId { get; set; }
    }
}
