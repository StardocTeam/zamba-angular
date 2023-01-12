using Stardoc.HtmlEditor.HtmlControls;
using System;

namespace Stardoc.HtmlEditor.ListViewItems
{
    public sealed class RuleButton
        : Button
    {
        #region Atributos
        private RuleButtonItem _button = null; 
        #endregion

        #region Propiedades
        public RuleButtonItem InnerControl
        {
            get { return _button; }
            set
            {
                _button = value;
                base.Text = value.Name;
            }
        }
        public string ToHtml()
        {
            return HtmlParser.ParseButton(InnerControl);
        } 
        #endregion

        #region Constructores
        public RuleButton(Int32 imageIndex)
            : base(imageIndex)
        { }
        public RuleButton(Int32 imageIndex, String toolTip)
            : base(imageIndex, toolTip)
        { }
        public RuleButton(Int32 imageIndex, String toolTip, RuleButtonItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        } 
        #endregion
    }
}
