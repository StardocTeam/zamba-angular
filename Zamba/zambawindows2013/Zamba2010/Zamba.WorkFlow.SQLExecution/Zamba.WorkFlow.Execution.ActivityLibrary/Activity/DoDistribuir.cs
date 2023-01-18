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
    /// Esta Regla sirve para distribuir documentos entre etapas.
    /// </summary>
    [RuleDescription("DISTRIBUIR"), RuleHelp("Distribuye los documentos entre etapas")]
    public partial class DoDistribuir : ZRule, IResultActivity, IDoDistribuir
    {

        //private Boolean _SelecCarp;
        private Int64 _NewWFStepId;
        private Zamba.WFExecution.PlayDODistribuir play;
        public DoDistribuir()
        {
            InitializeComponent();
            play = new Zamba.WFExecution.PlayDODistribuir(this);
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }


        #region IDoDistribuir Members

        public Int64 NewWFStepId
        {
            get
            {
                return _NewWFStepId;
            }
            set
            {
                _NewWFStepId = value;
            }
        }

        public String NewWFStepName
        {
            get
            {
                return WFStepBussines.GetStepNameById(NewWFStepId);
            }
        }

        //public bool SelecCarp
        //{
        //    get
        //    {
        //        return _SelecCarp;
        //    }
        //    set
        //    {
        //        _SelecCarp = value;
        //    }
        //}

        #endregion

        public override void SetParams(object[] Params)
        {
            NewWFStepId = Int32.Parse(Params.GetValue(0).ToString());
            //SelecCarp = Convert.ToBoolean(Int32.Parse(Params.GetValue(1).ToString()));
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(NewWFStepId);
            //Params.Add(SelecCarp);
            return Params;
        }
    }
}