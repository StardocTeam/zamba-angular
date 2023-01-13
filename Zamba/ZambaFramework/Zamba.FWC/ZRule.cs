using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
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
using Zamba.Core.Enumerators;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Clase base de las actividades
    /// <history>
    /// Marcelo Created 23/06/2008
    /// </history>
    /// </summary>
    public partial class ZRule : Activity, IResultActivity
	{
        public ZRule()
		{
			InitializeComponent();
		}

        //[Marcelo] 18/01/10 Created
        private Int64 _ruleId;

        /// <summary>
        /// Regla de WF de Zamba
        /// </summary>
        public Int64 ruleId
        {
            get
            {
                return _ruleId;
            }
            set
            {
                _ruleId = value;
            }
        }

        private string _ruleClass;
        public string RuleClass
        {
            get
            {
                return _ruleClass;
            }
            set
            {
                _ruleClass = value;
            }
        }

        /// <summary>
        /// Lista de results
        /// </summary>
        System.Collections.Generic.List<Zamba.Core.ITaskResult> results;
        /// <summary>
        /// Lista de results
        /// </summary>
        public System.Collections.Generic.List<Zamba.Core.ITaskResult> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
            }
        }

        /// <summary>
        /// Id de la etapa
        /// </summary>
        Int64 wfstepId;
        /// <summary>
        /// Id de la etapa
        /// </summary>
        public Int64 WFStepId
        {
            get
            {
                return wfstepId;
            }
            set
            {
                wfstepId = value;
            }
        }

        /// <summary>
        /// Tipo de regla
        /// </summary>
        TypesofRules ruletype;
        /// <summary>
        /// Tipo de regla
        /// </summary>
        public TypesofRules RuleType
        {
            get
            {
               return ruletype;
            }
            set
            {
                ruletype = value;
            }
        }

        /// <summary>
        /// Evento que se dispara al agrega activities
        /// </summary>
        public event ItemListChanging OnItemListChanging;

        #region ILazyLoad Members

        public void FullLoad()
        {
            throw new NotImplementedException();
        }

        private bool _IsFull= false  ;
        public bool IsFull
        {
            get { return _IsFull; }
        }

        private bool _IsLoaded=false ;
        public bool IsLoaded
        {
            get { return _IsLoaded; }
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}