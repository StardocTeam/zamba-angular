using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal class TextBox
                : ListViewItem, IHtmlWritable
    {
        #region Propiedades
        public TextBoxItem InnerControl
        {
            get { return (TextBoxItem)this.Tag; }
            set
            {
                this.Tag = (TextBoxItem)value;
                base.Text = value.Name;
            }
        }
        #endregion

        #region Constructores
        public TextBox(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public TextBox(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public TextBox(Int32 imageIndex, String toolTip, TextBoxItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        }
        #endregion

        string IHtmlWritable.ToHtml()
        {
            return HtmlParser.ParseTextBox(InnerControl);
        }
        HtmlControlType IHtmlWritable.Type
        {
            get { return InnerControl.Type(); }
        }

    }
}
