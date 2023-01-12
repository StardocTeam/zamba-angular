using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.AppBlock;
using Zamba.Controls;
using System.Collections;

namespace Zamba.Debugger
{
    public partial class DebuggerForm : Form
    {        
        public DebuggerForm()
        {
            InitializeComponent();
            ZIconsList IL = new ZIconsList();
            WFPanelDebugger = new WFPanelDebugger(IL);
        }


        List<System.Diagnostics.TraceListener> ListenersCollection;

        public DebuggerForm(System.Collections.Generic.List<System.Diagnostics.TraceListener> pListenersCollection)
        {
            InitializeComponent();
            this.ListenersCollection = pListenersCollection;
            //LoadInfo();
        }

        WFPanelDebugger WFPanelDebugger;
        private long currentRuleId;

        public ITaskResult CurrentResult { get; private set; }

        private Boolean AreTasksTheSame(List<ITaskResult> oldTasks, List<ITaskResult> Tasks)
        {
            Boolean finded = false;
            foreach (ITaskResult TR in Tasks)
            {
                foreach (ITaskResult oTR in oldTasks)
                {
                    if (TR.ID == oTR.ID)
                    {
                        finded = true;
                        break;
                    }
                }

                if (!finded)
                    return false;
                else
                    finded = false;
            }

            return true;
        }

        public delegate void RuleExecutedDelegate(WFRuleParent Rule, List<ITaskResult> Tasks);

        public void RuleExecuted(WFRuleParent Rule, List<ITaskResult> Tasks)
        {
            try
            {
                RuleExecutedDelegate d = new RuleExecutedDelegate(IntRuleExecuted);
                this.Invoke(d, new object[] { Rule, Tasks });
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void IntRuleExecuted(WFRuleParent Rule, List<ITaskResult> Tasks)
        {
            if (WFPanelDebugger != null)
                WFPanelDebugger.SetResult(Rule, true);

            //if (oldTasks == null || AreTasksTheSame(oldTasks, Tasks) == false)
            //{
            //    oldTasks = Tasks;
            //    WFPanelDebugger.ClearTrace(null);
            //}

            //WFPanelDebugger.Trace(Rule);

        }

        public delegate void RuleExecutedErrorDelegate(WFRuleParent Rule, List<ITaskResult> Tasks, Exception ex, ref Boolean erorBreakPoint);

        public void RuleExecutedError(WFRuleParent Rule, List<ITaskResult> Tasks, Exception ex, ref Boolean erorBreakPoint)
        {
            try
            {
                RuleExecutedErrorDelegate d = new RuleExecutedErrorDelegate(IntRuleExecutedError);
                this.Invoke(d, new object[] { Rule, Tasks, ex, erorBreakPoint });
            }
            catch (Exception ex1)
            {
                ZClass.raiseerror(ex1);
            }
        }

        private void IntRuleExecutedError(WFRuleParent Rule, List<ITaskResult> Tasks, Exception ex, ref Boolean erorBreakPoint)
        {
            if (WFPanelDebugger != null)
                WFPanelDebugger.SetResult(Rule, false);

            //if (oldTasks == null || AreTasksTheSame(oldTasks, Tasks) == false)
            //{
            //    oldTasks = Tasks;
            //    WFPanelDebugger.ClearTrace(null);
            //}

            //WFPanelDebugger.Trace(Rule);
        }

        //Al cargar el formulario se agrega el controlador
        private void DebuggerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.TabGeneral.Controls.Add(WFPanelDebugger);
                WFPanelDebugger.Dock = DockStyle.Fill;

                string MachineName = Environment.MachineName;
                UserPreferences.setValueForMachine("SaveTraceInDB", "True", UPSections.MonitorPreferences, MachineName);

                DBBusiness.AddDBListener(Membership.MembershipHelper.Module, new DBWriter());
                this.WindowState = FormWindowState.Normal;
                this.Show();
                LoadBreakPoints();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void LoadBreakPoints()
        {
            try
            {
                BreakPointsUtil.BreakPoints = WFRulesBusiness.GetBreakPointsByUserID(Membership.MembershipHelper.CurrentUser.ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public delegate void HandleModuleWithDoctypeDelegate(ResultActions resultActionType, ref ZambaCore currentResult, ZambaCore docType, Hashtable Params);

        public void HandleModuleWithDoctype(ResultActions resultActionType, ref ZambaCore currentResult, ZambaCore docType, Hashtable Params)
        {
            try
            {
                HandleModuleWithDoctypeDelegate d = new HandleModuleWithDoctypeDelegate(IntHandleModuleWithDoctype);
                this.Invoke(d, new object[] { resultActionType, currentResult, docType, Params });
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void IntHandleModuleWithDoctype(ResultActions resultActionType, ref ZambaCore currentResult, ZambaCore docType, Hashtable Params)
        {
        }

        public delegate void RuleToExecuteDelegate(WFRuleParent Rule, List<ITaskResult> Tasks);

        public void RuleToExecute(WFRuleParent Rule, List<ITaskResult> Tasks)
        {
            try
            {
                RuleToExecuteDelegate d = new RuleToExecuteDelegate(IntRuleToExecute);
                this.Invoke(d, new object[] { Rule, Tasks });
                currentRuleId = Rule.ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void IntRuleToExecute(WFRuleParent Rule, List<ITaskResult> Tasks)

        {
            try
            {
                if (WFPanelDebugger != null)
                {
                    WFPanelDebugger.AddNewRule(Rule, true, Tasks[0]);
          
                }
                //if (oldTasks == null || AreTasksTheSame(oldTasks, Tasks) == false)
                //{
                //    oldTasks = Tasks;
                //    WFPanelDebugger.ClearTrace(null);
                //}
                //WFPanelDebugger.Trace(Rule);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void WriteTrace()
        {
            try
            {
                Int32 lastID = 0;

                DataTable dt = ServiceBusiness.GetServiceTrace(Membership.MembershipHelper.Module, lastID, 100, "desc");
                dtGridView.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    lastID = Int32.Parse(dt.Select("ID = MAX(ID)")[0]["ID"].ToString());
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void DebuggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string MachineName = Environment.MachineName;
                UserPreferences.setValueForMachine("SaveTraceInDB", "False", UPSections.MonitorPreferences, MachineName);
                DBBusiness.AddDBListener(Membership.MembershipHelper.Module, new DBWriter());
                if (BreakPointsUtil.BreakPoints != null)
                    BreakPointsUtil.BreakPoints.Clear();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void TabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                //switch (tcDebugger.SelectedIndex)
                //{
                //    case 0:
                //        break;
                //    case 1:
                //        break;
                //    case 2:
                //        break;
                //    case 3:
                //        WriteTrace();
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void TsBtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (WFPanelDebugger != null)
                {
                    WFPanelDebugger.ClearDebugPanel();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void tsBtnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (tsBtnDetails.Checked)
                {
                    WFPanelDebugger.SplitContainer1.Panel2Collapsed = false;
                }
                else
                {
                    WFPanelDebugger.SplitContainer1.Panel2Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void tsBtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                BreakPointsUtil.SetContinueBreakPointState(currentRuleId, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
