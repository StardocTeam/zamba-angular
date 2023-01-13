using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor
{
    /// <summary>
    /// Formularios para mostrar el listado de reglas RuleButton insertar en el editor
    /// </summary>
    public partial class RulesSelector
        : Form
    {
        #region Propiedades
        public Dictionary<Int64, String> Workflows
        {
            set
            {
                cmbWorkflows.Items.Clear();

                foreach (KeyValuePair<Int64, String> CurrentWorkflow in value)
                    cmbWorkflows.Items.Add(new ComboBoxItem<Int64>(CurrentWorkflow.Key, CurrentWorkflow.Value));

                if (cmbWorkflows.Items.Count > 0)
                    cmbWorkflows.SelectedIndex = 0;
                else
                    cmbWorkflows.SelectedIndex = -1;

            }
        }
        public Dictionary<Int64, String> Steps
        {
            set
            {
                cmbSteps.Items.Clear();

                foreach (KeyValuePair<Int64, String> CurrentStep in value)
                    cmbSteps.Items.Add(new ComboBoxItem<Int64>(CurrentStep.Key, CurrentStep.Value));

                if (cmbSteps.Items.Count > 0)
                    cmbSteps.SelectedIndex = 0;
                else
                    cmbSteps.SelectedIndex = -1;
            }
        }
        public Dictionary<Int64, String> Rules
        {
            set
            {
                lvRules.Items.Clear();

                RuleButtonItem RuleButton = null;
                foreach (KeyValuePair<Int64, String> CurrentRule in value)
                {
                    RuleButton = new RuleButtonItem(CurrentRule.Value, CurrentRule.Key.ToString());
                    RuleButton.RuleId = CurrentRule.Key;
                    lvRules.Items.Add(new ListViewItems.RuleButton(0, RuleButton.ToString(), RuleButton));
                }
            }
        } 
        #endregion

        #region Contructores
        public RulesSelector()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal event LoadWorkflows OnLoadWorkflows;
        internal event SelectedWorkflow OnSelectedWorkflowChanged;
        internal event SelectedStep OnSelectedStep;
        internal event SelectedHtmlControl OnSelectedHtmlControl;
        internal delegate void LoadWorkflows();
        internal delegate void SelectedWorkflow(Int64 workflowId);
        internal delegate void SelectedStep(Int64 stepId);
        internal delegate void SelectedHtmlControl(String html);

        private void cmbWorkflows_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<Int64> SelectedWorkflow = (ComboBoxItem<Int64>)cmbWorkflows.SelectedItem;
            OnSelectedWorkflowChanged(SelectedWorkflow.Value); 
        }

        private void cmbSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<Int64> SelectedStep = (ComboBoxItem<Int64>)cmbSteps.SelectedItem;
            OnSelectedStep(SelectedStep.Value); 
        }

        private void RulesSelector_Load(object sender, EventArgs e)
        {
            try
            {
                OnLoadWorkflows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            ComboBoxItem<Int64> SelectedStep = (ComboBoxItem<Int64>)cmbSteps.SelectedItem;
            OnSelectedStep(SelectedStep.Value); 
        }

        private void lvRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRules.SelectedItems.Count > 0)
            {
                ListViewItems.RuleButton button = (ListViewItems.RuleButton)lvRules.SelectedItems[0];
                OnSelectedHtmlControl(button.ToHtml());
            }
        }
        #endregion

        /// <summary>
        /// Clase que representa un item de un combobox con valor generico
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ComboBoxItem<T>
        {
            #region Atributos
            private T _value;
            private String _text; 
            #endregion

            #region Propiedades
            public T Value
            {
                get { return _value; }
                set { _value = value; }
            }
            public String Text
            {
                get { return _text; }
                set { _text = value; }
            }
            #endregion

            #region Constructor
            public ComboBoxItem(T value, String text)
            {
                _text = text;
                _value = value;
            } 
            #endregion

            public override string ToString()
            {
                return _text;
            }
        }
    }
}