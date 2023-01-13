using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class OptionItem
        : IHtmlElement
    {
        #region Constantes
        protected const String NAME_TAG = "Nombre: ";
        protected const String VALUE_TAG = "Valor: ";
        protected const String DEFAULT_TAG = "Default";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        #endregion

        #region Atributos
        protected Boolean _default = false;
        protected String _name;
        protected String _value;
        protected Boolean _enabled = true;
        #endregion

        #region Propiedades
        public Boolean Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }
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

        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public HtmlControlType Type
        {
            get { return HtmlControlType.Option; }
        }
        public bool Required
        {
            //Solo se implementa porque lo pide la Interfaz , pero no tiene sentido que una opcion de 1 select sea requerida
            get { return false; }
            set { }
        }
        #endregion

        #region Constructores
        public OptionItem(String name, String value, Boolean isDefault)
        {
            _name = name;
            _value = value;
            _default = isDefault;
        }
        public OptionItem(String name, String value, Boolean isDefault, Boolean enabled)
            : this(name, value, isDefault)
        {
            _enabled = enabled;
        }       
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(NAME_TAG);
            DisplayBuilder.Append(_name);
            DisplayBuilder.Append(" "); 

            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.Append(_value);
            DisplayBuilder.Append(" ");

            if (_default)
            {
                DisplayBuilder.Append(DEFAULT_TAG);
                DisplayBuilder.Append(" "); 
            }

            if (_enabled)
                DisplayBuilder.Append(ENABLED_TAG);
            else
                DisplayBuilder.Append(DISABLED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}