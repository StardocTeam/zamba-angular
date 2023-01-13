using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class RadioButtonItem
        : BaseHtmlElement
    {
        #region Constantes
        protected const String VALUE_TAG = "Categoria: ";
        protected const String CHECKED_TAG = "Seleccionado";
        #endregion

        #region Atributos
        protected String _category;
        protected Boolean _checked;
        #endregion

        #region Propiedades
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
        override public HtmlControlType Type()
        {
            return HtmlControlType.RadioButton; 
        }

        #endregion

        #region Constructores
        public RadioButtonItem()
        {
        }
        public RadioButtonItem(String name, String category)
            : base(name)
        {
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
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(base.ToString());
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_category);

            if (_checked)
                DisplayBuilder.Append(CHECKED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}
