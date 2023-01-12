using System;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public sealed class RuleButtonItem
        : ButtonItem
    {
        #region Atributos
        private Int64 _ruleId; 
        #endregion

        #region Propiedades
        public Int64 RuleId
        {
            get { return _ruleId; }
            set { _ruleId = value; }
        } 
        #endregion

        #region Constructores
        public RuleButtonItem()
            : base()
        { }
        public RuleButtonItem(String name)
            : base(name)
        { }
        public RuleButtonItem(String name, String id)
            : base(name, id)
        { }
        public RuleButtonItem(String name, String id, Boolean enabled)
            : base(name, id, enabled)
        { }
        #endregion
    }
}
