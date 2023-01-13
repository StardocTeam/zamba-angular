using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal sealed class CheckBox
        : ListViewItem, IHtmlWritable
    {

        #region Propiedades
        public CheckBoxItem InnerControl
        {
            get { return (CheckBoxItem)this.Tag; }
            set
            {
                this.Tag = (Object)value;
                this.ToolTipText = value.ToString();
            }
        }
        #endregion

        #region Constructores
        public CheckBox(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public CheckBox(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public CheckBox(Int32 imageIndex, String toolTip, CheckBoxItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
        }
        #endregion

        public string ToHtml()
        {
            return HtmlParser.ParseCheckBox(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return HtmlControlType.CheckBox; }
        }
    }
}
