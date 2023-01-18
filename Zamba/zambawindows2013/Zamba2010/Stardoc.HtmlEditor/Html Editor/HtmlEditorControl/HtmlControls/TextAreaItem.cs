using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class TextAreaItem
        : IHtmlElement
    {
        #region Constantes
        protected const String REQUIRED_TAG = "Requerido";
        protected const String NOT_REQUIRED_TAG = "No requerido";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        protected const Int64 DEFAULT_ROW_COUNT = 2;
        protected const Int64 DEFAULT_COLUMN_COUNT = 20;
        protected const String INNER_TEXT_TAG = "Texto: ";
        protected const String COLUMN_COUNT_TAG = "Cantidad de columnas: ";
        protected const String ROW_COUNT_TAG = "Cantidad de filas: ";
        #endregion

        #region Atributos
        protected Int64 _rowCount = DEFAULT_ROW_COUNT;
        protected Int64 _columnCount = DEFAULT_COLUMN_COUNT;
        protected String _innerText;
        protected Boolean _enabled = true;
        protected Boolean _required = true;
        #endregion

        #region Propiedades
        public String InnerText
        {
            get { return _innerText; }
            set { _innerText = value; }
        }
        public Int64 ColumnCount
        {
            get { return _columnCount; }
            set { _columnCount = value; }
        }
        public Int64 RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }
        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public HtmlControlType Type
        {
            get { return HtmlControlType.TextArea; }
        }
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        #endregion

        #region Constructores
        public TextAreaItem()
        { }
        public TextAreaItem(Int64 rowCount, Int64 columnCount)
            : this()
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
        }
        public TextAreaItem(Int64 rowCount, Int64 columnCount, String innerText)
            : this(rowCount, columnCount)
        {
            _innerText = innerText;
        }
        public TextAreaItem(Int64 rowCount, Int64 columnCount, String innerText, Boolean enabled)
            : this(rowCount, columnCount, innerText)
        {
            _enabled = enabled;
        }
        public TextAreaItem(Int64 rowCount, Int64 columnCount, String innerText, Boolean enabled , Boolean required)
            : this(rowCount, columnCount, innerText, enabled)
        {
            _required = required;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

            DisplayBuilder.Append(COLUMN_COUNT_TAG);
            DisplayBuilder.AppendLine(_columnCount.ToString());

            DisplayBuilder.Append(ROW_COUNT_TAG);
            DisplayBuilder.AppendLine(_rowCount.ToString());

            if (_enabled)
                DisplayBuilder.AppendLine(ENABLED_TAG);
            else
                DisplayBuilder.AppendLine(DISABLED_TAG);

            if (_required)
                DisplayBuilder.AppendLine(REQUIRED_TAG);
            else
                DisplayBuilder.AppendLine(NOT_REQUIRED_TAG);

            DisplayBuilder.Append(INNER_TEXT_TAG);
            DisplayBuilder.Append(_innerText);

            return DisplayBuilder.ToString();
        }
    }
}
