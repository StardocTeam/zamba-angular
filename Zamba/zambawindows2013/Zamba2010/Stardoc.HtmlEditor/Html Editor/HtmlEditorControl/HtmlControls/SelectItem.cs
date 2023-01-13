using System;
using System.Collections.Generic;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class SelectItem
        : IHtmlElement
    {
        #region Constantes
        protected const String REQUIRED_TAG = "Requerido";
        protected const String NOT_REQUIRED_TAG = "No requerido";
        protected const String ENABLED_TAG = "Habilitado";
        protected const String DISABLED_TAG = "Deshabilitado";
        #endregion

        #region Atributos
        protected List<OptionItem> _options = null;
        protected Boolean _enabled = true;
        protected Boolean _required = true;
        #endregion

        #region Propiedades
        public List<OptionItem> Options
        {
            get { return _options; }
            set { _options = value; }
        }
        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public HtmlControlType Type
        {
            get { return HtmlControlType.Select; }
        }
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        #endregion

        #region Constructores
        public SelectItem()
        {
            _options = new List<OptionItem>();
        }
        public SelectItem(List<OptionItem> options)
        {
            _options = options;
        }
        public SelectItem(List<OptionItem> options, Boolean enabled)
            : this(options)
        {
            _enabled = enabled;
        }
        public SelectItem(List<OptionItem> options, Boolean enabled, Boolean required)
            : this(options, enabled)
        {
            _required = required;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

            if (_enabled)
                DisplayBuilder.AppendLine(ENABLED_TAG);
            else
                DisplayBuilder.AppendLine(DISABLED_TAG);

            if (_required)
                DisplayBuilder.AppendLine(REQUIRED_TAG);
            else
                DisplayBuilder.AppendLine(NOT_REQUIRED_TAG);


            foreach (OptionItem InnerControl in _options)
                DisplayBuilder.AppendLine(InnerControl.ToString());

            return DisplayBuilder.ToString();
        }
    }
}
