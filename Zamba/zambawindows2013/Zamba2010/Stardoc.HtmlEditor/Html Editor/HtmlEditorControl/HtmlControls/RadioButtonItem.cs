using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class RadioButtonItem
        : IHtmlElement
    {
        #region Constantes
        protected const String NAME_TAG = "Texto: ";
        protected const String VALUE_TAG = "Categoria: ";
        protected const String CHECKED_TAG = "Seleccionado";
        protected const String REQUIRED_TAG = "Requerido";
        protected const String NOT_REQUIRED_TAG = "No requerido";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        #endregion

        #region Atributos
        protected String _name;
        protected String _category;
        protected Boolean _checked;
        protected Boolean _enabled = true;
        protected Boolean _required = true;
        #endregion

        #region Propiedades
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public String Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
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
            get { return HtmlControlType.RadioButton; }
        }
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        #endregion

        #region Constructores
        public RadioButtonItem()
        {
        }
        public RadioButtonItem(String name, String category)
            : this()
        {
            _name = name;
            _category = category;
        }
        public RadioButtonItem(String name, String category, Boolean isChecked)
            : this(name, category)
        {
            _checked = isChecked;
        }
        public RadioButtonItem(String name, String category, Boolean isChecked, Boolean enabled)
            : this(name, category, isChecked)
        {
            _enabled = enabled;
        }
        public RadioButtonItem(String name, String category, Boolean isChecked, Boolean enabled, Boolean required)
            : this(name, category, isChecked, enabled)
        {
            _required = required;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(NAME_TAG);
            DisplayBuilder.AppendLine(_name);
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_category);

            if (_enabled)
                DisplayBuilder.AppendLine(ENABLED_TAG);
            else
                DisplayBuilder.AppendLine(DISABLED_TAG);

            if (_checked)
                DisplayBuilder.AppendLine(CHECKED_TAG);

            if (_required)
                DisplayBuilder.AppendLine(REQUIRED_TAG);
            else
                DisplayBuilder.AppendLine(NOT_REQUIRED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}
