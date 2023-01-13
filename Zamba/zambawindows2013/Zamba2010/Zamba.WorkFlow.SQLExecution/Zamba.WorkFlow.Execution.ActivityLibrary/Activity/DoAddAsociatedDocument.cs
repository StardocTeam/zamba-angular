using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Reflection;
using System.Diagnostics;
using Zamba.Core;
//using Zamba.WFUserControl;


namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Regla que permite insertar documentos y que esten asociados al documento inicial.
    /// </summary>
    /// 
    
    [RuleDescription("ASOCIAR NUEVOS DOCUMENTOS"), RuleHelp("Permite insertar documentos y que esten asociados al documento inicial.")]
    public partial class DoAddAsociatedDocument : ZRule, IResultActivity, IDoAddAsociatedDocument
    {

        private DocType _asociatedDocType;
        private int _TemplateId;
        private OfficeTypes _Typeid;
 private IDoAddAsociatedDocument.Selection _Selectionid;

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOAddAsociatedDocument play =
                new Zamba.WFExecution.PlayDOAddAsociatedDocument();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDoAddAsociatedDocument Members

        public IDocType AsociatedDocType
        {
            get
            {
                if (_asociatedDocType == null)
                    _asociatedDocType = new DocType();

                return _asociatedDocType;
            }
            set
            {
                _asociatedDocType = (Zamba.Core.DocType)value;
            }
        }
        public int TemplateId
        {
            get
            {
                return _TemplateId;
            }
            set
            {
                _TemplateId = value;
            }
        }

        public IDoAddAsociatedDocument.Selection SelectionId
        {
            get
            {
                return _Selectionid;
            }
            set
            {
                _Selectionid = value;
            }
        }

        public OfficeTypes Typeid
        {
            get
            {
                return _Typeid;
            }
            set
            {
                _Typeid = value;
            }
        }
        #endregion

        #region Miembros de IResultActivity

        public override void SetParams(object[] Params)
        {
            AsociatedDocType = Zamba.Data.DocTypesFactory.GetDocType(Int64.Parse(Params.GetValue(0).ToString()));
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(AsociatedDocType);
            return Params;
        }

        #endregion
    }
}
