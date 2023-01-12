using System;
using System.Collections.Generic;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

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
        public String IndexName
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        #endregion

        #region Constructores
        public ZSelectItem()
            : base()
        { }
        public ZSelectItem(String name, List<OptionItem> options)
            : base(name, options)
        { }
        public ZSelectItem(String name, List<OptionItem> options, Boolean enabled, Int64 indexId)
            : base(name,options, enabled)
        {
            _indexId = indexId;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

            DisplayBuilder.Append(base.ToString());

            DisplayBuilder.Append(INDEX_ID_TAG);
            DisplayBuilder.AppendLine(_indexId.ToString());

            foreach (OptionItem InnerControl in _options)
                DisplayBuilder.AppendLine(InnerControl.ToString());

            return DisplayBuilder.ToString();
        }
    }
}
