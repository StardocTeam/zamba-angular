using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using Zamba.Shapes.Controllers;
using Zamba.WFUserControl;
using Diagram = Zamba.Shapes.Views.Diagram;

namespace Zamba.Shapes.Views
{
    public partial class FrmRuleProperties : Form, IDisposable
    {
        WFRuleParent rule;
        DOpenRuleDiagram openRuleDiagram;
        TabControl tabControl;
        Control designer;
        public bool InitializationError { get; set; }

        public FrmRuleProperties(ref WFRuleParent rule, DOpenRuleDiagram openRuleDiagram)
        {
            InitializeComponent();

            this.rule = rule;
            this.openRuleDiagram = openRuleDiagram;
            InitializeRuleControl();
        }

        private void InitializeRuleControl()
        {
            InitializationError = true;

            if (rule != null)
            {
                Assembly tt;

                try
                {
                    tt = Assembly.LoadFile(Application.StartupPath + "\\Zamba.WFUserControl.dll");
                    System.Type t = tt.GetType("Zamba.WFUserControl.UC" + rule.RuleClass, true, true);

                    List<Int64> ruleInstaceList = new List<Int64>();
                    Object[] args = { WFRulesBusiness.GetInstanceRuleById(rule.ID, rule.WFStepId, true), null };
                    ruleInstaceList.Clear();
                    ruleInstaceList = null;

                    designer = (Control)Activator.CreateInstance(t, args);

                    if (designer != null)
                    {
                        designer.Height = 650;
                        designer.Width = 750;
                        this.Controls.Add(designer);
                        this.Width = designer.Width;
                        this.Height = designer.Height;

                        tabControl = (TabControl)designer.Controls[0];
                        ZTabPage tabDescription = new ZTabPage("Detalle");
                        UCRuleDetail ucRuleDetail = new UCRuleDetail(ref rule);
                        tabDescription.Controls.Add(ucRuleDetail);
                        tabControl.TabPages.Add(tabDescription);
                        FixGoToRuleHandlers();

                        InitializationError = false;
                        this.Text = "Propiedades de la regla '" + rule.Name + "' (Id: " + rule.ID.ToString() + ")";
                    }
                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);
                }
                finally
                {
                    tt = null;
                }
            }

            if (InitializationError)
            {
                MessageBox.Show("Error al cargar la configuración de la regla");
            }
        }

        private void FixGoToRuleHandlers()
        {
            //Boton de ir a la regla de error, genérico para todas las reglas
            IZRuleControl zRuleControl = (IZRuleControl)designer;
            zRuleControl.GoToErrorRule += GoToErrorRuleButton;
            string ruleClass = rule.RuleClass.ToLower();

            //Botones específicos por tipo de regla
            if (string.Compare(ruleClass, "domail") == 0)
            {
                List<string[]> buttons = new List<string[]>();
                buttons.Add(new string[] { "btnDoMailRule", "CboDoMailRule" });
                buttons.Add(new string[] { "btnExecAdditionalRule", "CboExecAdditionalRule" });
                AttachGoToRuleEvent(buttons);
                buttons = null;
            }
            else if (string.Compare(ruleClass, "doexecuterule") == 0)
            {
                List<string[]> buttons = new List<string[]>();
                buttons.Add(new string[] { "btnGoRule", "CBORules" });
                AttachGoToRuleEvent(buttons);
                buttons = null;
            }
            else if (string.Compare(ruleClass, "doexecuteexplorer") == 0)
            {
                List<string[]> buttons = new List<string[]>();
                buttons.Add(new string[] { "btnRulesGoRule", "CBORules" });
                buttons.Add(new string[] { "btnElseGoRule", "CBOElse" });
                buttons.Add(new string[] { "btnEvaluateGoRule", "CBOEvaluateRule" });
                AttachGoToRuleEvent(buttons);
                buttons = null;
            }
            else if (string.Compare(ruleClass, "doforo") == 0)
            {
                List<string[]> buttons = new List<string[]>();
                buttons.Add(new string[] { "btnForoRules", "CboForumRules" });
                AttachGoToRuleEvent(buttons);
                buttons = null;
            }

            zRuleControl = null;
        }

        void GoToErrorRuleButton(object sender, EventArgs e)
        {
            ComboBox cmbRuleId = (ComboBox)((Button)sender).Tag;

            if (cmbRuleId.SelectedValue != null)
            {
                Int64 ruleId = Int64.Parse(cmbRuleId.SelectedValue.ToString());
                openRuleDiagram(ruleId);
            }

            cmbRuleId = null;
            this.Close();
        }

        void AttachGoToRuleEvent(List<string[]> buttonNames)
        {
            Control tempControl;
            for (int i = 0; i < buttonNames.Count; i++)
            {
                tempControl = FindControl(buttonNames[i][0]);
                tempControl.Click += GoToErrorRuleButton;
                tempControl.Tag = FindControl(buttonNames[i][1]);
            }
        }

        Control FindControl(string name)
        {
            Control[] buttons;
            for (int j = 0; j < designer.Controls.Count; j++)
            {
                buttons = designer.Controls.Find(name, true);

                if (buttons.Length == 1)
                {
                    return buttons[0];
                    break;
                }
            }
            return null;
        }
    }
}
