using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using Zamba.Simulator.Model;
using Zamba.Core;
using System.Diagnostics;
using System.IO;
using ConsoleRedirection;

namespace Zamba.Simulator
{
    public partial class UcSimulationExecution : UserControl
    {
        public delegate void CloseEventHandler();
        public event CloseEventHandler Closed;
        public Simulation Sim { get; set; }
        public static bool pauseWorker;
        bool firstLoad = true;
        string executionResult;

        public UcSimulationExecution()
        {
            InitializeComponent();
        }

        public UcSimulationExecution(Simulation sim)
        {
            InitializeComponent();
            Sim = sim;
            lblSimulation.Text = "Simulación: " + sim.Name;
        }

        private void FrmSimulationExecution_Load(object sender, EventArgs e)
        {
            if (Sim != null && Sim.TestCases != null && Sim.TestCases.Count > 0)
            { 
                simulationsRepeater.DataSource = Sim.TestCases;
                firstLoad = false;
            }
        }

        private void simulationsRepeater_ItemValueNeeded(object sender, DataRepeaterItemValueEventArgs e)
        {
            if (e.ItemIndex < Sim.TestCases.Count)
            {
                switch (e.Control.Name)
                {
                    case "lblProcess":
                        e.Value = Sim.TestCases[e.ItemIndex].Process.Name;
                        break;
                    case "lblDictionary":
                        e.Value = Sim.TestCases[e.ItemIndex].Dictionary.Name;
                        break;
                    case "imgState":
                        if (firstLoad)
                            e.Control.BackgroundImage = global::Zamba.Simulator.Properties.Resources.pause_64;
                        else
                            switch (Sim.TestCases[e.ItemIndex].State)
                            {
                                case "CORRECTO":
                                    e.Control.Parent.BackColor = Color.FromArgb(242, 255, 242);
                                    e.Control.BackgroundImage = global::Zamba.Simulator.Properties.Resources.checkmark_64;
                                    break;
                                case "INCORRECTO":
                                    e.Control.Parent.BackColor = Color.FromArgb(255, 242, 242);
                                    e.Control.BackgroundImage = global::Zamba.Simulator.Properties.Resources.delete_sign_64;
                                    break;
                                case "EJECUCION":
                                    e.Control.Parent.BackColor = Color.FromArgb(255, 255, 242);
                                    e.Control.BackgroundImage = global::Zamba.Simulator.Properties.Resources.play_64;
                                    break;
                                case "PENDIENTE":
                                    e.Control.BackgroundImage = global::Zamba.Simulator.Properties.Resources.pause_64;
                                    break;
                            }
                        break;
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if           (  UcSimulationExecution.pauseWorker == false)
            {
        
            btnClose.Visible = false;
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            Sim.LastResult = "CORRECTO";

            bgWorker.RunWorkerAsync(Sim);
            }
            else
            {
                UcSimulationExecution.pauseWorker = false;
}
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            //if (frmSimDebug == null || frmSimDebug.IsDisposed)
            //    frmSimDebug = new FrmSimulationDebug();

            LoadTrace();

            //frmSimDebug.Show();

            btnClose.Visible = false;
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            Sim.LastResult = "CORRECTO";

            bgWorker.RunWorkerAsync(Sim);
        }

private         TextWriter _writer = null;

            private  void LoadTrace()
            {
                try
                {
                    _writer = new TextBoxStreamWriter(this.txtHistory);
                    Console.SetOut(_writer);
                    Trace.Listeners.Add(new TextWriterTraceListener(_writer));
                    Trace.AutoFlush = true;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        private void btnStop_Click(object sender, EventArgs e)
        {
            btnClose.Visible = true;
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;

            if (bgWorker.IsBusy)
                bgWorker.CancelAsync();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            pauseWorker = !pauseWorker;
            if (pauseWorker)
                btnPause.Enabled = false;
        }

        #region BackgroundWorker
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WFRulesBusiness wfrb = new WFRulesBusiness();
            Simulation sim = (Simulation)e.Argument;
            TestCase tc;
            bool result;

            for (int i = 0; i < sim.TestCases.Count;i++ )
            {
                if (pauseWorker)
                {
                    btnPause.Enabled = true;
                    while (pauseWorker)
                        System.Threading.Thread.Sleep(1000);
                }

                tc = sim.TestCases[i];
                IWFRuleParent rule = WFRulesBusiness.GetInstanceRuleById(tc.Process.RuleId, true);
                result = wfrb.TestRule(ref rule, ref tc.Dictionary.Data);
                tc = null;
                rule = null;

                bgWorker.ReportProgress(i, result);
            }

            wfrb = null;
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (pauseWorker)
            {
                btnPause.Enabled = true;
                while (pauseWorker)
                    System.Threading.Thread.Sleep(1000);
            }

            executionResult = ((bool)e.UserState) ? "CORRECTO" : "INCORRECTO";
            Sim.TestCases[e.ProgressPercentage].State = executionResult;
            if(executionResult == "INCORRECTO")
                Sim.LastResult = executionResult;

            if (e.ProgressPercentage < Sim.TestCases.Count - 1)
                Sim.TestCases[e.ProgressPercentage + 1].State = "EJECUCION";

            simulationsRepeater.DataSource = null;
            simulationsRepeater.DataSource = Sim.TestCases;


        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnClose.Visible = true;
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (Closed != null)
                Closed();
        }
    }
}
