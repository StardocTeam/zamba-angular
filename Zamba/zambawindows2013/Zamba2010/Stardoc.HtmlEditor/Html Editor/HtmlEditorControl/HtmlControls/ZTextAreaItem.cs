using System;
using Stardoc.HtmlEditor.Enumerators;
using System.Text;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZTextAreaItem
        : TextAreaItem
    {
        #region Constantes
        protected const String INDEX_ID_TAG = "ID del indice : ";
        #endregion

        #region Atributos
        protected Int64 _indexId;
        #endregion

        #region Propiedades
        public Int64 IndexId
        {
            get { return _indexId; }
            set { _indexId = value; }
        }
        public HtmlControlType Type()
        {
            return HtmlControlType.TextArea;
        }
        #endregion

        #region Constructores
        public ZTextAreaItem()
            : base()
        { }
        public ZTextAreaItem(Int64 rowCount, Int64 columnCount)
            : base(rowCount, columnCount)
        { }
        public ZTextAreaItem(Int64 rowCount, Int64 columnCount, String innerText)
            : base(rowCount, columnCount, innerText)
        { }

        public ZTextAreaItem(Int64 rowCount, Int64 columnCount, String innerText, Boolean enabled)
            : base(rowCount, columnCount, innerText, enabled)
        { }

        public ZTextAreaItem(Int64 rowCount, Int64 columnCount, String innerText, Boolean enabled, Int64 indexId)
            : this(rowCount, columnCount, innerText, enabled)
        { _indexId = indexId; }

        public ZTextAreaItem(TextAreaItem textArea, Int64 indexId)
            : this(textArea.RowCount, textArea.ColumnCount, textArea.InnerText, textArea.Enabled, indexId)
        { }
        #endregion
    }
}
