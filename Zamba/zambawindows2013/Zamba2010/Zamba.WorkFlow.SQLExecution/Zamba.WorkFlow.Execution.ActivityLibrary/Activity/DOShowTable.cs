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
    /// This activity show a ArrayList of delays
    /// </summary>
    [RuleDescription("Mostrar tabla de datos"), RuleHelp("Muestra los datos en una tabla y permite la seleccion de una fila")]
    public partial class DOShowTable : ZRule, IResultActivity, IDOShowTable
        	{
        public DOShowTable()
		{
			InitializeComponent();
		}
         
            protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
            {
                Zamba.WFExecution.PlayDOSHOWTABLE play =
                    new Zamba.WFExecution.PlayDOSHOWTABLE();
                play.Play(Results, this);
                return ActivityExecutionStatus.Closed;
            }
        
            #region Miembros de IDOShowTable

         

            #endregion

        public override void SetParams(object[] Params)
        {
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            //Params.Add(this._Name);
            //Params.Add(this._SQL);
            //Params.Add(this._HashTable);
            //Params.Add(this._servertype);
            //Params.Add(this._dbuser);
            //Params.Add(this._dbpassword);
            //Params.Add(this._dbname);
            //Params.Add(this._servidor);
            //Params.Add(this._ExecuteType);
            return Params;
        }
      
        #region IDOShowTable Members

        public string GetSelectedCols
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int SQLSelectId
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool SelectMultiRow
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public string ShowColumns
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public string VarDestiny
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public string VarSource
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool ShowCheckColumn
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool ShowDataOnly
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region IResultActivity Members

        System.Collections.Generic.List<object> IResultActivity.GetParams()
        {
            throw new NotImplementedException();
        }

        string IResultActivity.MaskName
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

        event ItemListChanging IResultActivity.OnItemListChanging
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        System.Collections.Generic.List<ITaskResult> IResultActivity.Results
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

        void IResultActivity.SetParams(object[] Params)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRule Members

        bool? IRule.AlertExecution
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

        System.Collections.Generic.List<IRule> IRule.ChildRules
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

        bool? IRule.CloseTask
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

        bool? IRule.ContinueWithError
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

        bool? IRule.ExecuteWhenResult
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

        string IRule.Description
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

        string IRule.MaskName
        {
            get { throw new NotImplementedException(); }
        }

        IRule IRule.ParentRule
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

        bool? IRule.RefreshRule
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

        TypesofRules IRule.RuleType
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

        int IRule.Version
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

        long IRule.WFStepId
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

        #region ICore Members

        long ICore.ID
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

        string ICore.Name
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

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
            }
}