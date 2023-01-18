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
    [RuleDescription("QUERY SELECT"), RuleHelp("Ejecuta una consulta")]
    public partial class DOSELECT : ZRule, IResultActivity, IDOSELECT
	{
        private Zamba.WFExecution.PlayDOSELECT play;
        public DOSELECT()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDOSELECT(this);
		}
            private String _servidor ;
            private String _dbuser;
            private String _dbpassword;
            private String _dbname;
            private Int32 _servertype;
            private String _SQLName;
            private String _SQL;
            private String _HashTable;
            private String _ExecuteType;

            protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
            {
                play.Play(Results);
                return ActivityExecutionStatus.Closed;
            }
            public Hashtable RuleVariablesInterReglas 
            {
                get{return WFRuleParent.VariablesInterReglas; }
            }

        #region Miembros de IDOSELECT

            string IDOSELECT.Dbname
            {
                get
                {
                    return _dbname;
                }
                set
                {
                    _dbname = value;
                }
            }

            string IDOSELECT.Dbpassword
            {
                get
                {
                    return _dbpassword;
                }
                set
                {
                    _dbpassword = value;
                }
            }

            string IDOSELECT.Dbuser
            {
                get
                {
                    return _dbuser;
                }
                set
                {
                    _dbuser = value;
                }
            }

            string IDOSELECT.ExecuteType
            {
                get
                {
                    return _ExecuteType;
                }
                set
                {
                    _ExecuteType = value;
                }
            }

            string IDOSELECT.HashTable
            {
                get
                {
                    return _HashTable;
                }
                set
                {
                    _HashTable = value;
                }
            }

            string IDOSELECT.SQLName
            {
                get
                {
                    return _SQLName;
                }
                set
                {
                    _SQLName = value;
                }
            }

          

            string IDOSELECT.SQL
            {
                get
                {
                    return _SQL;
                }
                set
                {
                    _SQL = value;
                }
            }

            int IDOSELECT.Servertype
            {
                get
                {
                    return _servertype;
                }
                set
                {
                    _servertype = value;
                }
            }

            string IDOSELECT.Servidor
            {
                get
                {
                    return _servidor;
                }
                set
                {
                    _servidor = value;
                }
            }

            #endregion

        public override void SetParams(object[] Params)
        {
            this._SQLName = Params[0].ToString();
            this._SQL = Params[1].ToString();
            this._HashTable = Params[2].ToString();
            //this._servertype = Int32.Parse(Params[3].ToString());
            this._dbuser = Params[4].ToString();
            this._dbpassword = Params[5].ToString();
            this._dbname = Params[6].ToString();
            this._servidor = Params[7].ToString();
            this._ExecuteType = Params[8].ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._SQLName);
            Params.Add(this._SQL);
            Params.Add(this._HashTable);
            Params.Add(this._servertype);
            Params.Add(this._dbuser);
            Params.Add(this._dbpassword);
            Params.Add(this._dbname);
            Params.Add(this._servidor);
            Params.Add(this._ExecuteType);
            return Params;
        }
    }
}