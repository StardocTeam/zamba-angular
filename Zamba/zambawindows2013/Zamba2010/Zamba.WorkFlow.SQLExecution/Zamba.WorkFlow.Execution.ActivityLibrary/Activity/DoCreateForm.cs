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
    /// Regla que crea un formulario.
    /// </summary>
   [RuleDescription("CREAR FORMULARIO"), RuleHelp("Crea un formulario.")]
    public partial class DOCreateForm : ZRule, IResultActivity, IDOCreateForm
    {
        private Boolean m_bAddToWf;
        private WFStep m_WFstep = null;
        private Int32 m_iDocTypeIdVirtual;
        private String m_hashtable;

        public DOCreateForm()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOCreateForm play =
                new Zamba.WFExecution.PlayDOCreateForm();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOCreateForm Members

        public bool AddToWf
        {
            get
            {
                return this.m_bAddToWf;
            }
            set
            {
                this.m_bAddToWf = value;
            }
        }

        public int DocTypeIdVirtual
        {
            get
            {
                return m_iDocTypeIdVirtual;
            }
            set
            {
                m_iDocTypeIdVirtual = value;
            }
        }

        // <summary>
        // Propiedad para contener la variable interregla
        // </summary>
        // <value></value>
        // <returns></returns>
        // <remarks></remarks>
        // <history>
        //      [Gaston]	18/07/2008	Created
        // </history>
        public string HashTable
        {
            get { return m_hashtable; }
            set { m_hashtable = value; }
        }

       public event IDOCreateForm.FormCreatedEventHandler FormCreated;

        public object RuleWFStep
        {
            get
            {
                return m_WFstep;
            }
        }

        #endregion

        public override void SetParams(object[] Params)
        {
            DocTypeIdVirtual = Int32.Parse(Params.GetValue(0).ToString());
            AddToWf = Convert.ToBoolean(Int16.Parse(Params.GetValue(1).ToString()));
            HashTable = Params.GetValue(2).ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(DocTypeIdVirtual);
            Params.Add(AddToWf);
            return Params;
        }

    }
}
