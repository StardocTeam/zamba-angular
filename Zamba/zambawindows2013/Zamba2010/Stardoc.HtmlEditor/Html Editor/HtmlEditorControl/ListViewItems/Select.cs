using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal sealed class Select
        : ListViewItem, IHtmlWritable
    {
        #region Propiedades

        public SelectItem InnerControl
        {
            get { return (SelectItem)this.Tag; }
            set
            {
                this.Tag = (SelectItem)value;
                this.ToolTipText = value.ToString();
            }
        }
        #endregion

        #region Constructores
        public Select(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public Select(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public Select(Int32 imageIndex, String toolTip, SelectItem innerControl)
            : this(imageIndex, toolTip)
        {
            this.Tag = (Object)innerControl;
        }
        #endregion

        public string ToHtml()
        {
            return HtmlParser.ParseSelect(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get
            {
                return HtmlControlType.Select;
            }
        }
    }
}
