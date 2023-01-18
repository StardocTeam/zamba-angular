using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Zamba.Core;

namespace Zamba.Shapes.Views
{
    public partial class UCRuleDetail : UserControl
    {
        public UCRuleDetail(ref WFRuleParent rule)
        {
            InitializeComponent();
            LoadDescription(ref rule);
            LoadWorkflowAndStepNames(rule.ID);
            lblName.Text = "Nombre: " + rule.Name;
            lblId.Text = "ID: " + rule.ID.ToString();
        }

        /// <summary>
        /// Carga el nombre de la acción de la regla (el que aparece al crear una regla desde el administrador)
        /// </summary>
        /// <param name="rule"></param>
        private void LoadDescription(ref WFRuleParent rule)
        {
            Assembly ruleData;
            string description = null;

            try
            {
                ruleData = System.Reflection.Assembly.LoadFile(Application.StartupPath + "\\Zamba.WFActivity.Regular.dll");

                foreach (Type M in ruleData.GetTypes())
                {
                    if (string.Compare(M.Name, rule.RuleClass) == 0)
                    {
                        foreach (object o in M.GetCustomAttributes(true))
                        {
                            if (string.Compare(o.GetType().Name, "RuleDescription") == 0)
                            {
                                description = ((RuleDescription)o).Description;
                                lblAction.Text = "Acción: " + description;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                if (String.IsNullOrEmpty(description)) lblAction.Text = "Acción: " + rule.RuleClass;
                ruleData = null;
            }
        }

        /// <summary>
        /// Carga el nombre del proceso y la etapa
        /// </summary>
        /// <param name="ruleId"></param>
        private void LoadWorkflowAndStepNames(Int64 ruleId)
        {
            WFRulesBusinessExt wfRulesBusinessExt = new WFRulesBusinessExt();
            string[] wfAndStepNames = wfRulesBusinessExt.GetWfAndStepNameByRuleId(ruleId);
            wfRulesBusinessExt = null;

            lblWorkflow.Text = "Proceso: " + wfAndStepNames[0];
            lblStep.Text = "Etapa: " + wfAndStepNames[1];
            wfAndStepNames = null;
        }
    }
}
