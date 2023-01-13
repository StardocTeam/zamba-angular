using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;
using System;

namespace Stardoc.HtmlEditor.ListViewItems
{
    public class Button
            : ListViewItem, IHtmlWritable
    {
        #region Atributos
        private ButtonItem _button;
        #endregion

        #region Propiedades
        public string ToHtml()
        {
            return HtmlParser.ParseButton(InnerControl);
        }
        public HtmlControlType Type
        {
            get { return InnerControl.Type(); }
        }
        public ButtonItem InnerControl
        {
            get { return _button; }
            set
            {
                _button = value;
                base.Text = value.Name;
            }
        }
        #endregion

        #region Constructores
        public Button(Int32 imageIndex)
        {
            this.ImageIndex = imageIndex;
        }
        public Button(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public Button(Int32 imageIndex, String toolTip, ButtonItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        }
        #endregion
    }
}