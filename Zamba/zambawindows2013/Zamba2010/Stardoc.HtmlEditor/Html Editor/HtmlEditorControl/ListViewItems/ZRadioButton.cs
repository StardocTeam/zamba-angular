using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal class ZRadioButton
        : ListViewItem, IZambaHtmlControls
    {
        #region Propiedades
        public ZRadioButtonItem InnerControl
        {
            get { return (ZRadioButtonItem)this.Tag; }
            set
            {
                this.Tag = (ZRadioButtonItem)value;
                this.ToolTipText = value.ToString();
            }
        }
        public Int64 IndexId
        {
            get { return InnerControl.IndexId; }
            set { InnerControl.IndexId = value; }
        }
        #endregion

        #region Constructores
        public ZRadioButton(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public ZRadioButton(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public ZRadioButton(Int32 imageIndex, String toolTip, ZRadioButtonItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
        }
        public ZRadioButton(Int32 imageIndex, String toolTip, ZRadioButtonItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip, innerControl)
        {
            IndexId = indexId;
        }
        #endregion

        String IHtmlWritable.ToHtml()
        {
            return HtmlParser.ParseRadioButton(InnerControl);
        }
        HtmlControlType IHtmlWritable.Type
        {
            get { return HtmlControlType.RadioButton; }
        }
    }
}
