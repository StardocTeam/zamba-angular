using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class OptionItem
        : BaseHtmlElement
    {
        #region Constantes
        private const String VALUE_TAG = "Valor: ";
        private const String DEFAULT_TAG = "Default";
        #endregion

        #region Atributos
        private Boolean _default = false;
        private String _value;
        #endregion

        #region Propiedades
        public Boolean Default
        {
            get { return _default; }
            set { _default = value; }
        }
        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }
        override public HtmlControlType Type()
        {
            return HtmlControlType.Option;
        }
        #endregion

        #region Constructores
        public OptionItem(String name, String value, Boolean isDefault)
            : base(name)
        {
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
            DisplayBuilder.Append(base.ToString());
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_value);

            if (_default)
                DisplayBuilder.AppendLine(DEFAULT_TAG);

            DisplayBuilder.Remove(DisplayBuilder.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return DisplayBuilder.ToString();
        }
    }
}