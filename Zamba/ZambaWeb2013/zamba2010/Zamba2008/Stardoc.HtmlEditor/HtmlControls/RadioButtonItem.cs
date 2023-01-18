using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class RadioButtonItem
        : IHtmlElement
    {
        #region Constantes
        private const String NAME_TAG = "Texto: ";
        private const String VALUE_TAG = "Categoria: ";
        private const String CHECKED_TAG = "Seleccionado";
        #endregion

        #region Atributos
        private String _name;
        private String _category;
        private Boolean _checked;
        private Boolean _enabled = true;
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

        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(NAME_TAG);
            DisplayBuilder.AppendLine(_name);
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_category);

            if (_checked)
                DisplayBuilder.AppendLine(CHECKED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}
