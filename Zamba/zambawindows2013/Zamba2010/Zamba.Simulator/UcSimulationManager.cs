using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Simulator.Model;

namespace Zamba.Simulator
{
    public partial class UcSimulationManager : UserControl
    {
        List<Simulation> lstSimulations = new List<Simulation>();
        UcSimulationABM ucSimAbm = null;
        UcSimulationExecution ucSimExec = null;

        public UcSimulationManager()
        {
            InitializeComponent();

            //GetLastSearchResults3(1);            
        }
        public void AddTestCaseToCurrentSim( long ruleId, string ruleName)
        {
            try
            {
                Process process = new Process(ruleId);
                TestCase testCase = new TestCase(process);
                if (lstSimulations.Count == 0)
                {
                    string description = "Prueba individual para la regla " + ruleName;
                    AddNewSimulation(ruleId, ruleName, description);

                }
                else
                {
                    if (SimRepeater.CurrentItemIndex <= lstSimulations.Count && SimRepeater.CurrentItemIndex != -1)
                        lstSimulations[SimRepeater.CurrentItemIndex].TestCases.Add(testCase);
                    if (ucSimAbm != null)
                        ucSimAbm.UpdateProcessList();
                }
            }
            catch (Exception )
            {

            }

            //if ( this.Controls.Contains(ucSimAbm))
            //{
            //    this.Controls.Find(ucSimAbm)
            //    ucSimAbm = new UcSimulationABM();
            //    ucSimAbm.Dock = DockStyle.Fill;
            //    ucSimAbm.Closed += ucSimAbm_Closed;
            //}
            

        }

        public void AddNewSimulation(long ruleId, string ruleName, string description)
        {
            try
            {
                Process process = new Process(ruleId);
                TestCase testCase = new TestCase(process);
                Simulation simulation = new Simulation(ruleId, ruleName, description, DateTime.Now.ToString(), null, "No ejecutado", false);
                simulation.TestCases.Add(testCase);
                lstSimulations.Add(simulation);
                SimRepeater.DataSource = null;
                SimRepeater.DataSource = lstSimulations;
            }
            catch (Exception )
            {

            }

        }


        /// <summary>
        /// Crea una nueva simulación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Tomas] 18/08/2013
        /// </history>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlServiceMgr.Visible = false;

                //abre el formulario de creacion de simulacion
                ucSimAbm = new UcSimulationABM();
                ucSimAbm.Dock = DockStyle.Fill;
                ucSimAbm.Closed += ucSimAbm_Closed;
                this.Controls.Add(ucSimAbm); 
            }
            catch (Exception)
            {

            }
           
        }

        void ucSimAbm_Closed(bool addNewSimulation)
        {
            try
            {
                if (addNewSimulation)
                {
                    lstSimulations.Add(ucSimAbm.Simulation);
                    SimRepeater.DataSource = null;
                    SimRepeater.DataSource = lstSimulations;
                }

                pnlServiceMgr.Visible = true;
                this.Controls.Remove(ucSimAbm);

                SavelastSearchResults3(1, lstSimulations);
            }
            catch (Exception)
            {
         
            }

        }

        /// <summary>
        /// Edita la configuración de una simulación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Tomas] 18/08/2013
        /// </history>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifica que exista una simulacion seleccionada
                if (SimRepeater.CurrentItemIndex > -1)
                {
                    Simulation sim = lstSimulations[SimRepeater.CurrentItemIndex];
                    pnlServiceMgr.Visible = false;

                    //abre el mismo fromulario de creacion pero en modo edicion
                    ucSimAbm = new UcSimulationABM(sim);
                    ucSimAbm.Dock = DockStyle.Fill;
                    ucSimAbm.Closed += ucSimAbm_Closed;
                    this.Controls.Add(ucSimAbm);
                }
                SavelastSearchResults3(1, lstSimulations);
            }
            catch (Exception)
            {
              
            }

        }

        public void SavelastSearchResults3(Int64 NewId, List<Simulation> Sims)
        {
            if (Sims == null || NewId == 0)
            {
                return;
            }

            try
            {
                String Path = Application.StartupPath + "\\Sim" + NewId + ".osl";
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            try
            {
                String Path = Application.StartupPath + "\\Sim" + NewId + ".osl";

                System.IO.Stream objStream = System.IO.File.Open(Path, System.IO.FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                objFormatter.Serialize(objStream, Sims);
                objStream.Flush();
                objStream.Close();
                objStream.Dispose();
                objStream = null;
                objFormatter = null;

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        //[Judn] 24-09-2014: Se comenta ya que tira error al pararse en la solapa de simulación.  
        //public List<Simulation> GetLastSearchResults3(Int64 Id)
        //{
        //    List<Simulation> Sims = new List<Simulation>();
        //    try
        //    {

        //        String Path = Application.StartupPath + "\\Sim" + Id + ".osl";
        //        System.IO.Stream objStream = System.IO.File.Open(Path, System.IO.FileMode.Open);
        //        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //        Sims = (List<Simulation>)(objFormatter.Deserialize(objStream));

        //        objStream.Close();
        //        objStream.Dispose();
        //        objStream = null;
        //        objFormatter = null;
        //        return Sims;
        //    }
        //    catch (Exception ex)
        //    {
        //        Zamba.Core.ZClass.raiseerror(ex);

        //        return Sims;
        //    }
        //}

        /// <summary>
        /// Elimina la simulación seleccionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Tomas] 18/08/2013
        /// </history>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifica que exista una simulacion seleccionada
                if (SimRepeater.CurrentItemIndex > -1)
                {
                    if (MessageBox.Show("Presione Aceptar para eliminar la simulación seleccionada",
                        "Confirmar acción",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //elimina la simulacion
                        Simulation sim = lstSimulations[SimRepeater.CurrentItemIndex];
                        lstSimulations.Remove(sim);
                        SimRepeater.DataSource = null;
                        SimRepeater.DataSource = lstSimulations;
                    }
                }
            }
            catch (Exception)
            {
                
            }

        }

        /// <summary>
        /// Crea una copia de una simulación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Tomas] 20/08/2013
        /// </history>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            //copia y crea toda una simulacion seleccionada
            //solo su configuracion (procesos y diccionarios)
            //no incluye resultados ni estadisticas
        }

       

        /// <summary>
        /// Carga los datos de las simulaciones en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Tomas] 18/08/2013
        /// </history>
        private void SimRepeater_ItemValueNeeded(object sender, Microsoft.VisualBasic.PowerPacks.DataRepeaterItemValueEventArgs e)
        {
            if (e.ItemIndex < lstSimulations.Count && e.ItemIndex != -1)
            {
                switch (e.Control.Name)
                {
                    case "lblSimName":
                        e.Value = lstSimulations[e.ItemIndex].Name;
                        break;
                    case "lblSimDescription":
                        e.Value = lstSimulations[e.ItemIndex].Description;
                        break;
                    case "lblSimLastEdition":
                        e.Value = lstSimulations[e.ItemIndex].LastUpdate;
                        break;
                    case "lblSimLastExecution":
                        e.Value = lstSimulations[e.ItemIndex].LastExecution;
                        break;
                    case "lblSimResult":
                        e.Value = lstSimulations[e.ItemIndex].LastResult;
                        if(e.Value != null)
                        switch (e.Value.ToString())
	                    {
                            case "CORRECTO":
                                e.Control.Parent.BackColor = Color.FromArgb(242,255,242);
                                break;
                            case "INCORRECTO":
                                e.Control.Parent.BackColor = Color.FromArgb(255, 242, 242);
                                break;
	                    }
                        break;
                    case "imgService":
                        imgService.Visible = lstSimulations[e.ItemIndex].IsAutomatic;
                        break;
                }
            }
        }

        private void btnPlayTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (SimRepeater.CurrentItemIndex > -1)
                {
                    pnlServiceMgr.Visible = false;

                    selectedSimulation = SimRepeater.CurrentItemIndex;
                    ucSimExec = new UcSimulationExecution(lstSimulations[selectedSimulation]);
                    ucSimExec.Dock = DockStyle.Fill;
                    ucSimExec.Closed += ucSimExec_Closed;
                    this.Controls.Add(ucSimExec);
                }
            }
            catch (Exception)
            {
                
            }

        }

        int selectedSimulation;
        private void ucSimExec_Closed()
        {
            try
            {
                SimRepeater.DataSource = null;
                //lstSimulations[selectedSimulation] = ucSimExec.Sim;
                SimRepeater.DataSource = lstSimulations;

                pnlServiceMgr.Visible = true;
                this.Controls.Remove(ucSimExec);
                SavelastSearchResults3(1, lstSimulations);
            }
            catch (Exception)
            {
                
            }


        }
    }
}
