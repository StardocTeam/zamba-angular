using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal sealed class ZTextArea
        : ListViewItem, IZambaHtmlControls
    {

        #region Propiedades
        public ZTextAreaItem InnerControl
        {
            get { return (ZTextAreaItem)this.Tag; }
            set
            {
                this.Tag = (ZTextAreaItem)value;
                base.Text = value.IndexName;
            }
        }
        public Int64 IndexId
        {
            get { return InnerControl.IndexId; }
            set { InnerControl.IndexId = value; }
        }
        #endregion

        #region Constructores
        public ZTextArea(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public ZTextArea(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public ZTextArea(Int32 imageIndex, String toolTip, ZTextAreaItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        }
        public ZTextArea(Int32 imageIndex, String toolTip, ZTextAreaItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip, innerControl)
        {
            IndexId = indexId;
        }
        #endregion

        String IHtmlWritable.ToHtml()
        {
            return HtmlParser.ParseTextArea(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return InnerControl.Type(); }
        }
    }
}
