using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal sealed class RadioButton
     : ListViewItem, IHtmlWritable
    {
        #region Propiedades
        public RadioButtonItem InnerControl
        {
            get { return (RadioButtonItem)this.Tag; }
            set
            {
                this.Tag = (Object)value;
                base.Text = value.Name;
            }
        }
        #endregion

        #region Constructores
        public RadioButton(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public RadioButton(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public RadioButton(Int32 imageIndex, String toolTip, RadioButtonItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        } 
        #endregion

        public string ToHtml()
        {
            return HtmlParser.ParseRadioButton(InnerControl);
        }

        HtmlControlType IHtmlWritable.Type
        {
            get { return InnerControl.Type(); }
        }

    }
}