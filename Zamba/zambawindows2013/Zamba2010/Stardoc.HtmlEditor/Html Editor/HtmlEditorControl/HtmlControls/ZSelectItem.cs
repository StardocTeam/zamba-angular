using System;
using System.Collections.Generic;
using System.Text;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class ZSelectItem
        : SelectItem
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
        #endregion

        #region Constructores
        public ZSelectItem()
            : base()
        { }
        public ZSelectItem(List<OptionItem> options)
            : base(options)
        { }
        public ZSelectItem(List<OptionItem> options, Boolean enabled)
            : base(options, enabled)
        { }

        public ZSelectItem(List<OptionItem> options, Boolean enabled, Boolean required)
            : base(options, enabled, required)
        { }
        public ZSelectItem(List<OptionItem> options, Boolean enabled, Boolean required, Int64 indexId)
            : this(options, enabled, required)
        {
            _indexId = indexId;
        }
        public ZSelectItem(SelectItem select, Int64 indexId)
            : this(select.Options, select.Enabled, select.Required, indexId)
        { }
        #endregion
    }
}
