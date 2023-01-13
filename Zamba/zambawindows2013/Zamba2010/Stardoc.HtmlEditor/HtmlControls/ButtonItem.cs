using System;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ButtonItem
            : BaseHtmlElement
    {
        #region Atributos
        private String _inputType = "button";
        #endregion
        #region Propiedades

        public override HtmlControlType Type()
        {
            return HtmlControlType.Button;
        }
        public String InputType
        {
            get { return _inputType; }
            set { _inputType = value; }
        }
        #endregion

        #region Constructores
        public ButtonItem()
        { }
        public ButtonItem(String name)
            : base(name)
        { }
        public ButtonItem(String name, String id)
            : base(name, id)
        { }
        public ButtonItem(String name, String id, Boolean enabled)
            : base(name, id, enabled)
        { }
        public ButtonItem(String name, String id, Boolean enabled, String inputType)
            : this(name, id, enabled)
        {
            _inputType = inputType;
        }
        public ButtonItem(String name, String id, Boolean enabled, String inputType,Boolean isFormButton)
            : this(name, id, enabled,inputType)
        {
            _isFormButton = isFormButton;
        }
        #endregion
    }
}