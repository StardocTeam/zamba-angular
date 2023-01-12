using System;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZCheckBoxItem
        : CheckBoxItem
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
        public ZCheckBoxItem()
            : base()
        { }
        public ZCheckBoxItem(String text, Boolean isChecked)
            : base(text, isChecked)
        { }
        public ZCheckBoxItem(String text, Boolean isChecked, Boolean enabled)
            : base(text, isChecked, enabled)
        { }
        public ZCheckBoxItem(String text, Boolean isChecked, Boolean enabled, Boolean required)
            : base(text, isChecked, enabled, required)
        { }

        public ZCheckBoxItem(String text, Boolean isChecked, Boolean enabled, Boolean required, Int64 indexId)
            : this(text, isChecked, enabled, required)
        {
            _indexId = indexId;
        }
        public ZCheckBoxItem(CheckBoxItem checkBox, Int64 indexId)
            : this(checkBox.Text, checkBox.Checked, checkBox.Enabled, checkBox.Required, indexId)
        { }
        #endregion
    }
}
