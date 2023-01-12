using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;
using System.Windows.Forms;

namespace Stardoc.HtmlEditor.ListViewItems
{
    public class ZTextBox
        : ListViewItem, IZambaHtmlControls
    {
        #region Propiedades
        public ZTextBoxItem InnerControl
        {
            get { return (ZTextBoxItem)this.Tag; }
            set { this.Tag = (ZTextBoxItem)value; }
        }
        #endregion

        #region Constructores
        public ZTextBox(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public ZTextBox(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public ZTextBox(Int32 imageIndex, String toolTip, ZTextBoxItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
        }
        public ZTextBox(Int32 imageIndex, String toolTip, ZTextBoxItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip, innerControl)
        {
            IndexId = indexId;
        }

        #endregion

        public Int64 IndexId
        {
            get { return InnerControl.IndexId; }
            set { InnerControl.IndexId = value; }
        }

        String IHtmlWritable.ToHtml()
        {
            return HtmlParser.ParseTextBox(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return HtmlControlType.TextBox; }
        }
    }
}
