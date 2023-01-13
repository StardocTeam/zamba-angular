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
        public String IndexName
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        #endregion

        #region Constructores
        public ZTextAreaItem()
            : base()
        { }
        public ZTextAreaItem(String indexName, Int64 rowCount, Int64 columnCount)
            : base(indexName , rowCount, columnCount)
        { }
        public ZTextAreaItem(String indexName, Int64 rowCount, Int64 columnCount, String innerText)
            : base(indexName, rowCount, columnCount, innerText)
        { }
        public ZTextAreaItem(String indexName, Int64 indexId, Int64 rowCount, Int64 columnCount, String innerText)
            : this(indexName, rowCount, columnCount, innerText)
        { _indexId = indexId; }
        #endregion


        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

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
