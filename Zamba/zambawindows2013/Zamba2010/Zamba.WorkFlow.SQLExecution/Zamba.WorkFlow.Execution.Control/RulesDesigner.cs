using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using Zamba.WFActivity.Xoml;
using Zamba.Core;

namespace Zamba.WorkFlow.Execution.Control
{
    /// <summary>
    /// Formulario donde se muestran los diseñadores de las reglas
    /// </summary>
    public partial class RulesDesigner : Form
    {
        public RulesDesigner(Core.IResultActivity activity)
        {
            InitializeComponent();

            System.Reflection.Assembly tt  = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath + "\\Zamba.WFUserControl.dll");
            System.Type t = tt.GetType("Zamba.WFUserControl.UC" + activity.RuleClass, true, true);

            List<Int64> ruleInstancesList = new List<Int64>();
            Object[] args = {WFRulesBusiness.GetInstanceRuleById(activity.ruleId,activity.WFStepId,true), null};
            ruleInstancesList.Clear();
            ruleInstancesList = null;

            Zamba.WFUserControl.ZRuleControl designer = (Zamba.WFUserControl.ZRuleControl)Activator.CreateInstance(t, args);

            //Zamba.WFUserControl.ZRuleControl designer = activity.GetDesigner();
            if (designer != null)
            {
                this.Name = designer.Name;
                designer.Height = 650;
                designer.Width = 750;
                designer.Disposed += new EventHandler(designer_Disposed);
                this.Controls.Add(designer);
                this.Width = designer.Width + 10;
                this.Height = designer.Height + 10;
                this.Text = "Diseñador de " + ((Activity)activity).Name;
                isDesigned = true;
            }
            else
                this.Close();
        }

        void designer_Disposed(object sender, EventArgs e)
        {
            this.Close();
        }

        public Boolean isDesigned = false;

        //void designer_closeDesignered()
        //{
        //    this.Close();
        //}
    }
}