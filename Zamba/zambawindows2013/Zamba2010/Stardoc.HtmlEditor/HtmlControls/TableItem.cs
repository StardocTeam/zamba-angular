using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class TableItem
        : BaseHtmlElement
    {
        #region Contructores
        public TableItem()
            : base()
        { }
        public TableItem(String name)
            : base(name)
        { }
        public TableItem(String name, String id)
            : base(name, id)
        { }
        #endregion

        override public HtmlControlType Type()
        {
            return HtmlControlType.Table;
        }
    }
}
