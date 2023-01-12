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
    /// Regla que permite modificar la fecha de vencimiento de una tarea
    /// </summary>
    [RuleDescription("VENCIMIENTO DE TAREA"), RuleHelp("Modifica la fecha de vencimiento de una tarea.")]
    public partial class DoChangeExpireDate : ZRule, IResultActivity, IDoChangeExpireDate
	{

        private Int32 _Direccion1, _Direccion2, _Direccion3, _Direccion4, _Direccion5;
        private Int32 _Value1, _Value2, _Value3, _Value4, _Value5;


		public DoChangeExpireDate()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDoChangeExpireDate play =
                new Zamba.WFExecution.PlayDoChangeExpireDate();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            Direccion1 = Int32.Parse(Params.GetValue(0).ToString());
            Direccion2 = Int32.Parse(Params.GetValue(1).ToString());
            Direccion3 = Int32.Parse(Params.GetValue(2).ToString());
            Direccion4 = Int32.Parse(Params.GetValue(3).ToString());
            Direccion5 = Int32.Parse(Params.GetValue(4).ToString());
            Value1 = Int32.Parse(Params.GetValue(5).ToString());
            Value2 = Int32.Parse(Params.GetValue(6).ToString());
            Value3 = Int32.Parse(Params.GetValue(7).ToString());
            Value4 = Int32.Parse(Params.GetValue(8).ToString());
            Value5 = Int32.Parse(Params.GetValue(9).ToString());
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(Direccion1);
            Params.Add(Direccion2);
            Params.Add(Direccion3);
            Params.Add(Direccion4);
            Params.Add(Direccion5);
            Params.Add(Value1);
            Params.Add(Value2);
            Params.Add(Value3);
            Params.Add(Value4);
            Params.Add(Value5);
            return Params;
        }

        #region IDoChangeExpireDate Members

        public int Direccion1
        {
            get
            {
                return _Direccion1;
            }
            set
            {
                _Direccion1 = value;
            }
        }

        public int Direccion2
        {
            get
            {
                return _Direccion2;
            }
            set
            {
                _Direccion2 = value;
            }
        }

        public int Direccion3
        {
            get
            {
                return _Direccion3;
            }
            set
            {
                _Direccion3 = value;
            }
        }

        public int Direccion4
        {
            get
            {
                return _Direccion4;
            }
            set
            {
                _Direccion4 = value;
            }
        }

        public int Direccion5
        {
            get
            {
                return _Direccion5;
            }
            set
            {
                _Direccion5 = value;
            }
        }

        public int Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
            }
        }

        public int Value2
        {
            get
            {
                return _Value2;     
            }
            set
            {
                _Value2 = value;
            }
        }

        public int Value3
        {
            get
            {
                return _Value3;
            }
            set
            {
                _Value3 = value;
            }
        }

        public int Value4
        {
            get
            {
                return _Value4;
            }
            set
            {
                _Value4 = value;
            }
        }

        public int Value5
        {
            get
            {
                return _Value5;
            }
            set
            {
                _Value5 = value;
            }
        }

        #endregion
     }
}
