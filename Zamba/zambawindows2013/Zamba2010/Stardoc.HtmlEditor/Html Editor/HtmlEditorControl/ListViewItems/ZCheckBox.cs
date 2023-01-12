using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal class ZCheckBox
       : ListViewItem, IZambaHtmlControls
    {

        #region Propiedades
        public ZCheckBoxItem InnerControl
        {
            get { return (ZCheckBoxItem)this.Tag; }
            set
            {
                this.Tag = (ZCheckBoxItem)value;
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
        
        public ZCheckBox(Int32 imageIndex)
            : base()
        {
            this.ImageIndex = imageIndex;
        }
        public ZCheckBox(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public ZCheckBox(Int32 imageIndex, String toolTip, ZCheckBoxItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
        }
        public ZCheckBox(Int32 imageIndex, String toolTip, ZCheckBoxItem innerControl, Int64 indexId)
            : this(imageIndex, toolTip, innerControl)
        {
            indexId = indexId;
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
