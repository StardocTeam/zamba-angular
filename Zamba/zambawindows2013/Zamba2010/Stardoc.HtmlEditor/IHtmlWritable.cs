using Stardoc.HtmlEditor.Enumerators;
using System;

namespace Stardoc.HtmlEditor
{
    /// <summary>
    /// Interfaz que deben implementar los controles que pueden ser insertados en el formulario
    /// </summary>
    public interface IHtmlWritable
    {
        String ToHtml();
        HtmlControlType Type{get;}
    }
}
