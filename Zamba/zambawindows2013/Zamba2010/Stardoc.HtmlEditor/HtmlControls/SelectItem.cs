using System;
using System.Collections.Generic;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public class SelectItem
        : BaseHtmlElement
    {
        #region Atributos
        protected List<OptionItem> _options = null;
        #endregion

        #region Propiedades
        public List<OptionItem> Options
        {
            get { return _options; }
            set { _options = value; }
        }
        override public HtmlControlType Type()
        {
            return HtmlControlType.Select; 
        }
        #endregion

        #region Constructores
        public SelectItem()
            :base()
        { }
        public SelectItem(String name, List<OptionItem> options)
            :base(name )
        {
            _name = name;

            if (null == options)
                _options = new List<OptionItem>();
            else
                _options = options;
        }
        public SelectItem(String name, List<OptionItem> options, Boolean enabled)
            : this(name,options)
        {
            _enabled = enabled;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();
            DisplayBuilder.Append(base.ToString()); 

            foreach (OptionItem InnerControl in _options)
                DisplayBuilder.AppendLine(InnerControl.ToString());

            return DisplayBuilder.ToString();
        }
    }
}
