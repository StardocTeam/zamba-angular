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
    /// Esta regla compara un listado por un atributo de cada elemento y guarda en una variable los elementos que cumplan con ese valor
    /// </summary>
    [RuleDescription("Comparar Listado de Datos"), RuleHelp("Permite filtrar listado de Datos por valor dado por el usuario, guardando el listado filtrado en una variable")]
    public partial class DoCompare : ZRule, IResultActivity, IDoCompare
	{
        public DoCompare()
		{
			InitializeComponent();
		}

            protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
            {
                Zamba.WFExecution.PlayDoCompare play =
                    new Zamba.WFExecution.PlayDoCompare();
                play.Play(Results, this);
                return ActivityExecutionStatus.Closed;
            }
            public Hashtable RuleVariablesInterReglas 
            {
                get{return WFRuleParent.VariablesInterReglas; }
            }

        public override void SetParams(object[] Params)
        {
           //this._servertype = Int32.Parse(Params[3].ToString());
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
           // Params.Add(this._SQLName);
            return Params;
        }

        #region IDoCompare Members

        public string Comp
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string IdAsoc
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseAsocDoc
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long idDocTypeAsoc
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string valueComp
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string valueFilter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string valueList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDoCompare Members


        public string variableName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}