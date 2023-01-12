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
   [RuleDescription("ACTIVAR O DESACTIVAR REGLA"), RuleHelp("Activa o desactiva una regla.")]
    public partial class DoEnableRule : ZRule, IResultActivity, IDoEnableRule
    {

        private String m_sSelectedRuleIDs;
        private Boolean m_bEstado;
        private Boolean m_bOnlyForTask;
        private Boolean m_bJoinExecution;
        private String m_sNombreDeLaRegla;
        private Boolean m_bViewAllSteps;
        private Zamba.WFExecution.PlayDoEnableRule play;
       
        public DoEnableRule()
        {
            InitializeComponent();
            play = new Zamba.WFExecution.PlayDoEnableRule(this);
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        #region IDoEnableRule Members

        public bool OnlyForTask
        {
            get
            {
                return m_bOnlyForTask;
            }
            set
            {
                m_bOnlyForTask = value;
            }
        }

        public bool RuleEjecucion
        {
            get
            {
                return m_bJoinExecution;
            }
            set
            {
                m_bJoinExecution = value;
            }
        }

        public bool RuleEstado
        {
            get
            {
                return m_bEstado;
            }
            set
            {
                m_bEstado = value;
            }
        }

        public string SelectedRulesIDs
        {
            get
            {
                return m_sSelectedRuleIDs;
            }
            set
            {
                m_sSelectedRuleIDs = value;
            }
        }

        public string RuleName
        {
            get
            {
                return m_sNombreDeLaRegla;
            }
            set
            {
                m_sNombreDeLaRegla = value;
            }
        }

        public bool ViewAllSteps
        {
            get
            {
                return m_bViewAllSteps;
            }
            set
            {
                m_bViewAllSteps = value;
            }
        }


        #endregion
        
        public override void SetParams(object[] Params)
        {
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();

            return Params;
        }
    }
}