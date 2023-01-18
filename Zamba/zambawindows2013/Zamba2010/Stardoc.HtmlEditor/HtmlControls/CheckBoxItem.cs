using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class CheckBoxItem
        : BaseHtmlElement
    {
        #region Constantes
        private const String SELECTED_TAG = "Seleccionado";
        #endregion

        #region Atributos
        private Boolean _checked;
        #endregion

        #region Propiedades
        public Boolean Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        override public HtmlControlType Type()
        {
            return HtmlControlType.CheckBox;
        }
        #endregion

        #region Constructores
        public CheckBoxItem()
            : base()
        { }
        public CheckBoxItem(String name, Boolean isChecked)
            : base(name)
        {
            _checked = isChecked;
        }
        public CheckBoxItem(String name, Boolean isChecked, Boolean enabled)
            : this(name, isChecked)
        {
            _enabled = enabled;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(base.ToString());

            if (_checked)
                DisplayBuilder.AppendLine(SELECTED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}