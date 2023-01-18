using System;
using System.Text;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZRadioButtonItem
           : RadioButtonItem
    {
        #region Constantes
        protected const String INDEX_ID_TAG = "ID del indice : ";
        protected const String SELECTION_VALUE_TAG = "Valor de seleccion: ";
        #endregion

        #region Atributos
        private Int64 _indexId;
        private Boolean _selectionValue = false;
        #endregion

        #region Propiedades
        public Int64 IndexId
        {
            get { return _indexId; }
            set { _indexId = value; }
        }
        public Boolean SelectionValue
        {
            get { return _selectionValue; }
            set { _selectionValue = value; }
        }
        #endregion

        #region Constructores
        public ZRadioButtonItem()
            : base()
        { }
        public ZRadioButtonItem(String name, String category)
            : base(name, category)
        { }
        public ZRadioButtonItem(String name, String category, Boolean isChecked)
            : base(name, category, isChecked)
        { }
        public ZRadioButtonItem(String name, String category, Boolean isChecked, Boolean enabled)
            : base(name, category, isChecked, enabled)
        { }
        public ZRadioButtonItem(String name, String category, Boolean isChecked, Boolean enabled, Int64 indexId)
            : this(name, category, isChecked, enabled)
        {
            _indexId = indexId;
        }
        public ZRadioButtonItem(String name, String category, Boolean isChecked, Boolean enabled, Int64 indexId, Boolean selectionValue)
            : this(name, category, isChecked, enabled, indexId)
        {
            _selectionValue = selectionValue;
        }
        public ZRadioButtonItem(RadioButtonItem radio, Int64 indexId)
            : this(radio.Name, radio.Category, radio.Checked, radio.Enabled, indexId)
        { }
        public ZRadioButtonItem(RadioButtonItem radio, Int64 indexId, Boolean selectionValue)
            : this(radio, indexId)
        {
            _selectionValue = selectionValue; 
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(NAME_TAG);
            DisplayBuilder.AppendLine(_name);
            DisplayBuilder.Append(VALUE_TAG);
            DisplayBuilder.AppendLine(_category);

            DisplayBuilder.Append(INDEX_ID_TAG);
            DisplayBuilder.AppendLine(_indexId.ToString());
            DisplayBuilder.Append(SELECTION_VALUE_TAG);
            if (_selectionValue)
                DisplayBuilder.AppendLine("S");
            else
                DisplayBuilder.AppendLine("N");

            if (_checked)
                DisplayBuilder.AppendLine(CHECKED_TAG);

            return DisplayBuilder.ToString();
        }
    }
}
