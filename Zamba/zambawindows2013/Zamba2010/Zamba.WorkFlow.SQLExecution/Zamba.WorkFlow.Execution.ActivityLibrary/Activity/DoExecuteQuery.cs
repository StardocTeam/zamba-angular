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
    /// Esta Regla ejecuta una consulta a una base de datos.
    /// </summary>
    [RuleDescription("EJECUTAR CONSULTA"), RuleHelp("Ejecuta una consulta a una base de datos.")]
    public partial class DOExecuteQuery : ZRule, IResultActivity, IDOExecuteQuery
	{	
		

        private string _sql; //Cadena SQL que debe ejecutar la regla
        private ReturnType _queryType;
        private String _folder;
        
        public DOExecuteQuery()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOExecuteQuery play =
                new Zamba.WFExecution.PlayDOExecuteQuery();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOExecuteQuery Members

        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }

        public ReturnType QueryType
        {
            get
            {
                return _queryType;
            }
            set
            {
                _queryType = value;
            }
        }

        public string Sql
        {
            get
            {
                return _sql;
            }
            set
            {
                _sql = value;
            }
        }

        #endregion

        #region Miembros de IResultActivity

        public override void SetParams(object[] Params)
        {
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();

            return Params;
        }

        #endregion
    }
}
