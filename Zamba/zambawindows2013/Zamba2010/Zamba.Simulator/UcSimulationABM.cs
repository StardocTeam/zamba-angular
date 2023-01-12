using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Simulator.Model;
using Zamba.Core;

namespace Zamba.Simulator
{
    
    public partial class UcSimulationABM : UserControl
    {
        List<TestCase> testCases = null;
        public delegate void CloseEventHandler(bool addNewSimulation);
        public event CloseEventHandler Closed;
        public Simulation Simulation { get; set; }
        public bool IsNew { get; set; }

        private long NextSimId = 1;

        public UcSimulationABM()
        {
            InitializeComponent();
            IsNew = true;
            lblTitle.Text = "Nueva Simulación";
        }
        public UcSimulationABM(Simulation sim)
        {
            InitializeComponent();

            IsNew = false;
            lblTitle.Text = "Editar Simulación";

            this.Simulation = sim;
            txtName.Text = sim.Name;
            txtDescription.Text = sim.Description;
            testCases = sim.TestCases.ToList();
        }
        public void UpdateProcessList()
        {
            testCases = this.Simulation.TestCases.ToList();
            TCRepeater.DataSource = testCases;
        }

        private void UcSimulationABM_Load(object sender, EventArgs e)
        {
            TCRepeater.DataSource = testCases;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(this.Simulation == null)
                this.Simulation = new Simulation(NextSimId++, txtName.Text, txtDescription.Text, DateTime.Now.ToString(), null, "No ejecutado", false);
            else
            {
                this.Simulation.Name = txtName.Text;
                this.Simulation.Description = txtDescription.Text;
                this.Simulation.IsAutomatic = false;
                this.Simulation.LastUpdate = DateTime.Now.ToString();
            }
            
            if (TCRepeater.DataSource != null)
            this.Simulation.TestCases = (List<TestCase>)TCRepeater.DataSource;
            Cerrar(IsNew);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Se cierra sin modificacion alguna de la simulacion.
            Cerrar(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Cerrar(bool addNewSimulation)
        {
            if (Closed != null)
                Closed(addNewSimulation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveTC_Click(object sender, EventArgs e)
        {
            testCases.RemoveAt(TCRepeater.CurrentItemIndex);
            TCRepeater.DataSource = null;
            TCRepeater.DataSource = testCases;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDictionary_Click(object sender, EventArgs e)
        {

            TestCase testCase = testCases[TCRepeater.CurrentItemIndex];

            if (testCase.Dictionary.Data.Columns.Count == 0)
            {
                IWFRuleParent rule = WFRulesBusiness.GetInstanceRuleById(testCase.Process.RuleId, true);
                List<String> Params = rule.DiscoverParams();
                rule = null;

                if (Params != null && Params.Count != 0)
                    foreach (var param in Params)
                    {
                        testCase.Dictionary.Data.Columns.Add(new DataColumn(param, typeof(string)));
                    }
                else
                {
                    System.Windows.Forms.MessageBox.Show("La regla no utiliza Diccionario de datos");
                    return;
                }
            }
            
            using (FrmDictionaryABM frm = new FrmDictionaryABM(testCase.Dictionary, testCase.Process.RuleId))
            {
                frm.ShowDialog();
            }

            
        }

        private void TCRepeater_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void TCRepeater_ItemValueNeeded(object sender, Microsoft.VisualBasic.PowerPacks.DataRepeaterItemValueEventArgs e)
        {
            if (e.ItemIndex < testCases.Count)
            {
                switch (e.Control.Name)
                {
                    case "lblProcessValue":
                        e.Value = testCases[e.ItemIndex].Process.Name;
                        break;
                    case "lblDictionaryValue":
                        e.Value = testCases[e.ItemIndex].Dictionary.Name;
                        break;
                }
            }
        }
    }
}
