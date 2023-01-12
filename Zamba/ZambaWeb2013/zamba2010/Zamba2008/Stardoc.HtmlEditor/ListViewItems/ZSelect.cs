using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    public class ZSelect
         : ListViewItem, IZambaHtmlControls
    {

        #region Propiedades
        public ZSelectItem InnerControl
        {
            get { return (ZSelectItem)this.Tag; }
            set { this.Tag = (ZSelectItem)value; }
        }
        #endregion

          #region Constructores
        public ZSelect(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public ZSelect(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public ZSelect(Int32 imageIndex, String toolTip, ZSelectItem innerControl)
            : this(imageIndex, toolTip)
        {
            this.Tag = (Object)innerControl;
        }
        public ZSelect(Int32 imageIndex, String toolTip, ZSelectItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip , innerControl)
        {
            IndexId = indexId;
        }
        #endregion

        public long IndexId
        {
            get { return InnerControl.IndexId; }
            set { InnerControl.IndexId = value; }
        }

        public string ToHtml()
        {
            return HtmlParser.ParseSelect(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return HtmlControlType.Select; }
        }
    }
}
