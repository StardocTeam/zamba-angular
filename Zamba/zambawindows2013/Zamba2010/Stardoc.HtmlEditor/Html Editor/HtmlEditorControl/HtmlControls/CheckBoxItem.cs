using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class CheckBoxItem
        : IHtmlElement
    {
        #region Constantes
        protected const String TEXT_TAG = "Texto: ";
        protected const String SELECTED_TAG = "Seleccionado";
        protected const String REQUIRED_TAG = "Requerido";
        protected const String NOT_REQUIRED_TAG = "No requerido";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        #endregion

        #region Atributos
        private String _text = null;
        private Boolean _checked;
        private Boolean _enabled = true;
        protected Boolean _required = true;
        #endregion

        #region Propiedades
        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public Boolean Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public HtmlControlType Type
        {
            get { return HtmlControlType.CheckBox; }
        }
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        #endregion

        #region Constructores
        public CheckBoxItem()
        { }
        public CheckBoxItem(String text, Boolean isChecked)
            : this()
        {
            _text = text;
            _checked = isChecked;
        }
        public CheckBoxItem(String text, Boolean isChecked, Boolean enabled)
            : this(text, isChecked)
        {
            _enabled = enabled;
        }

        public CheckBoxItem(String text, Boolean isChecked, Boolean enabled, Boolean required)
            : this(text, isChecked, enabled)
        {
            _required = required;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(TEXT_TAG);
            DisplayBuilder.AppendLine(_text);

            if (_enabled)
                DisplayBuilder.AppendLine(ENABLED_TAG);
            else
                DisplayBuilder.AppendLine(DISABLED_TAG);

            if (_checked)
                DisplayBuilder.AppendLine(SELECTED_TAG);

            if (_required)
                DisplayBuilder.AppendLine(REQUIRED_TAG);
            else
                DisplayBuilder.AppendLine(NOT_REQUIRED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}