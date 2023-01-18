using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class TextBoxItem
        : IHtmlElement
    {
        #region Constantes
        protected const String NAME_TAG = "Nombre: ";
        protected const String VALUE_TAG = "Valor: ";
        protected const String REQUIRED_TAG = "Requerido";
        protected const String NOT_REQUIRED_TAG = "No requerido";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        protected const String READ_ONLY_TAG = "Solo lectura";
        #endregion

        #region Atributos
        private String _name;
        private String _value;
        private Boolean _readOnly = false;
        private Boolean _enabled = true;
        private Boolean _required = true;
        private DataType _dataType = DataType.Alfanumerico;
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
        public String Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public Boolean ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }
        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public HtmlControlType Type
        {
            get { return HtmlControlType.TextBox; }
        }
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        public DataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
        #endregion

        #region Constructores
        public TextBoxItem(String name, String value)
        {
            _name = name;
            _value = value;
        }
        public TextBoxItem(String name, String value, Boolean readOnly)
            : this(name, value)
        {
            _readOnly = readOnly;
        }
        public TextBoxItem(String name, String value, Boolean readOnly, Boolean enabled)
            : this(name, value, readOnly)
        {
            _enabled = enabled;
        }

        public TextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Boolean required)
            : this(name, value, readOnly, enabled)
        {
            _required = required;
        }
        public TextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Boolean required, DataType dataType)
            : this(name, value, readOnly, enabled,required )
        {
            _dataType = dataType;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(NAME_TAG);
            DisplayBuilder.AppendLine(_name);
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_value);

            if (_enabled)
                DisplayBuilder.AppendLine(ENABLED_TAG);
            else
                DisplayBuilder.AppendLine(DISABLED_TAG);

            if (_required)
                DisplayBuilder.AppendLine(REQUIRED_TAG);
            else
                DisplayBuilder.AppendLine(NOT_REQUIRED_TAG);

            if (_readOnly)
                DisplayBuilder.AppendLine(READ_ONLY_TAG);

            return DisplayBuilder.ToString();
        }

    }
}
    