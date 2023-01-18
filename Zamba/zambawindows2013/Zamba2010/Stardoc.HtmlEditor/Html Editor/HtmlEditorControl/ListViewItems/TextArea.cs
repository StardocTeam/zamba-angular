using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal class TextArea
        : ListViewItem, IHtmlWritable
    {

        #region Propiedades
        public TextAreaItem InnerControl
        {
            get { return (TextAreaItem)this.Tag; }
            set
            {
                this.Tag = (TextAreaItem)value;
                this.ToolTipText = value.ToString();
            }
        }
        #endregion

        #region Constructores
        public TextArea(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public TextArea(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public TextArea(Int32 imageIndex, String toolTip, TextAreaItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
        }
        #endregion
        public string ToHtml()
        {
            return HtmlParser.ParseTextArea(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return HtmlControlType.TextArea; }
        }

    }
}
