using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal class ZSelect
         : ListViewItem, IZambaHtmlControls
    {

        #region Propiedades
        public ZSelectItem InnerControl
        {
            get { return (ZSelectItem)this.Tag; }
            set
            {
                this.Tag = (ZSelectItem)value;
                base.Text = value.IndexName;
            }
        }
        public Int64 IndexId
        {
            get { return InnerControl.IndexId; }
            set { InnerControl.IndexId = value; }
        }
        HtmlControlType IHtmlWritable.Type
        {
            get { return InnerControl.Type(); }
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
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        }
        public ZSelect(Int32 imageIndex, String toolTip, ZSelectItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip, innerControl)
        {
            IndexId = indexId;
        }
        #endregion

        public string ToHtml()
        {
            return HtmlParser.ParseSelect(InnerControl);
        }
    }
}
