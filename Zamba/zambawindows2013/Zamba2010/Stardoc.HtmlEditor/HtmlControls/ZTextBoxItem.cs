using System;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZTextBoxItem
        : TextBoxItem
    {
        #region Atributos
        protected Int64 _indexId;
        private String _indexName;
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
        public ZTextBoxItem(String indexName, String value)
            : base(indexName, value)
        { }
        public ZTextBoxItem(String indexName, String value, Boolean readOnly)
            : base(indexName, value, readOnly)
        { }
        public ZTextBoxItem(String indexName, String value, Boolean readOnly, Boolean enabled)
            : base(indexName, value, readOnly, enabled)
        { }
        public ZTextBoxItem(String indexName, String value, Boolean readOnly, Boolean enabled, Int64 indexId)
            : this(indexName, value, readOnly, enabled)
        {
            _indexId = indexId;
        }
        public ZTextBoxItem(String indexName, String value, Boolean readOnly, Boolean enabled, Int64 indexId, Int32 lenght)
            : this(indexName, value, readOnly, enabled, indexId)
        {
            base.Lenght = lenght;
        }
        #endregion
    }
}
