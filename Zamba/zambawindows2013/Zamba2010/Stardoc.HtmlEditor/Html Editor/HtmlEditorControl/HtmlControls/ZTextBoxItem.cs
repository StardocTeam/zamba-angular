using System;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZTextBoxItem
        : TextBoxItem
    {
        #region Atributos
        protected Int64 _indexId;
        #endregion

        #region Propiedades
        public Int64 IndexId
        {
            get { return _indexId; }
            set { _indexId = value; }
        }
        #endregion

        #region Constructores
        public ZTextBoxItem(String name, String value)
            : base(name, value)
        { }
        public ZTextBoxItem(String name, String value, Boolean readOnly)
            : base(name, value, readOnly)
        { }
        public ZTextBoxItem(String name, String value, Boolean readOnly, Boolean enabled)
            : base(name, value, readOnly, enabled)
        { }
        public ZTextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Boolean required)
            : base(name, value, readOnly, enabled, required)
        {}
        public ZTextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Boolean required, Int64 indexId)
            : this(name, value, readOnly, enabled, required)
        {
            _indexId = indexId;
        }
        public ZTextBoxItem(String name, String value, Boolean readOnly, Boolean enabled, Boolean required, Int64 indexId, DataType dataType)
            : base(name, value, readOnly, enabled, required, dataType)
        {
            _indexId = indexId;
        }
        public ZTextBoxItem(TextBoxItem textBox, Int64 indexId)
            : this(textBox.Name, textBox.Value, textBox.ReadOnly, textBox.Enabled, textBox.Required, indexId, textBox.DataType)
        { }

        #endregion
    }
}
