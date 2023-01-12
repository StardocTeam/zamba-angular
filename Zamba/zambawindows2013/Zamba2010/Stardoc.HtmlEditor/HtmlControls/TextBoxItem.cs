using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class TextBoxItem
        : BaseHtmlElement
    {
        #region Constantes
        private const String VALUE_TAG = "Valor: ";
        #endregion

        #region Atributos
        private String _value;
        private Boolean _readOnly = false;
        private Int32 _lenght;
        #endregion

        #region Propiedades
        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public Boolean ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }
        override public HtmlControlType Type()
        {
            return HtmlControlType.TextBox;
        }
        public Int32 Lenght
        {
            get { return _lenght; }
            set { _lenght = value; }
        }
        #endregion

        #region Constructores
        public TextBoxItem(String name, String value)
            :base(name)
        {
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
        public TextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Int32 lenght)
            : this(name, value, readOnly, enabled)
        {
            Lenght = lenght;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(base.ToString());
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.Append(_value);

            return DisplayBuilder.ToString();
        }

    }
}
    