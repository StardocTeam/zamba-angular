using System;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor
{
    public interface IHtmlElement
    {
        String Id { get; set; }
        String Name { get; set; }
        Boolean Enabled { get; set; }
       // HtmlControlType Type { get; }
    }
}