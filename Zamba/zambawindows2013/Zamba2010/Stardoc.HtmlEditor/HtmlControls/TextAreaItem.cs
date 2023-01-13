using System;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class TextAreaItem
        : BaseHtmlElement
    {
        #region Constantes
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
        private Int32 _lenght;
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
        override public HtmlControlType Type()
        {
            return HtmlControlType.TextArea;
        }
        public Int32 Lenght
        {
            get { return _lenght; }
            set { _lenght = value; }
        }

        #endregion

        #region Constructores
        public TextAreaItem()
            :base()
        { }
        public TextAreaItem(String name, Int64 rowCount, Int64 columnCount)
            : base(name)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
        }
        public TextAreaItem(String name, Int64 rowCount, Int64 columnCount, String innerText)
            : this(name, rowCount, columnCount)
        {
            _innerText = innerText;
        }
        public TextAreaItem(String name, Int64 rowCount, Int64 columnCount, String innerText, Boolean enabled)
            : this(name, rowCount, columnCount, innerText)
        {
            _enabled = enabled;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

            DisplayBuilder.Append(base.ToString());

            DisplayBuilder.Append(COLUMN_COUNT_TAG);
            DisplayBuilder.AppendLine(_columnCount.ToString());

            DisplayBuilder.Append(ROW_COUNT_TAG);
            DisplayBuilder.AppendLine(_rowCount.ToString());

            DisplayBuilder.Append(INNER_TEXT_TAG);
            DisplayBuilder.Append(_innerText);

            return DisplayBuilder.ToString();
        }
    }
}
