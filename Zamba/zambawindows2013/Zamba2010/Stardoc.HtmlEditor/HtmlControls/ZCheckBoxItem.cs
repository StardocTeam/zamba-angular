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
        public String IndexName
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        #endregion

        #region Constructores
        public ZCheckBoxItem()
            : base()
        { }
        public ZCheckBoxItem(String name, Boolean isChecked)
            : base(name, isChecked)
        { }
        public ZCheckBoxItem(String name, Boolean isChecked, Boolean enabled)
            : base(name, isChecked, enabled)
        { }

        public ZCheckBoxItem(String name, Boolean isChecked, Boolean enabled, Int64 indexId)
            : this(name, isChecked, enabled)
        {
            _indexId = indexId;
        }
        #endregion
    }
}
