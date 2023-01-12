using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Zamba.Core;

namespace Zamba.ZTC
{
    public partial class UCTestCaseExecutionHistory : UserControl
    {
        #region Delegates

        public delegate void TestCaseEXSelectedEventHandler(object sender, EventArgs e);

        #endregion

        public event TestCaseEXSelectedEventHandler TestCaseEXSelected;

        public UCTestCaseExecutionHistory()
        {
            InitializeComponent();
        }
        
        public void LoadExecutionHistoryTestCase(Int64 TestCaseId)
        {
            ((ISupportInitialize) (radExecutionHistoryGrid)).BeginInit();


            try
            {
                radExecutionHistoryGrid.DataSource = from p in ControlsFactory.dbContext.ZTC_EX
                                                     join c in ControlsFactory.dbContext.ZTC_RS
                                                         on p.ExecutionResultID equals c.ExecutionResultID
                                                     join f in ControlsFactory.dbContext.USRTABLE on p.UserId equals
                                                         f.ID
                                                     where p.TestCaseID == TestCaseId
                                                     select
                                                         new
                                                             {
                                                                 p.ExecutionId,
                                                                 p.TestCaseID,
                                                                 Usuario = (f.NOMBRES + " " + f.APELLIDO),
                                                                 Fecha = p.ExecutionDate,
                                                                 Resultado = c.ExecutionResultName,
                                                                 Observaciones = p.Comment
                                                             };


                radExecutionHistoryGrid.Columns["TestCaseId"].IsVisible = false;


                radExecutionHistoryGrid.Columns["ExecutionId"].HeaderText = "ID";

                radExecutionHistoryGrid.Refresh();
                radExecutionHistoryGrid.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                ((ISupportInitialize) (radExecutionHistoryGrid)).EndInit();
                ResumeLayout(false);
            }
        }

        public bool hasExecution()
        {
            return (radExecutionHistoryGrid.Rows.Count > 0);
        }


        private void radExecutionHistoryGrid_Click(object sender, EventArgs e)
        {
            try
            {
                object executionId = radExecutionHistoryGrid.SelectedRows[0].Cells["ExecutionId"].Value;

                TestCaseEXSelected(executionId, new EventArgs());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Refresca la grilla de historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Refresh();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void radExecutionHistoryGrid_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            var cell = sender as GridDataCellElement;
            if (cell != null)
            {
                var dataCell = sender as GridDataCellElement;

                e.ToolTipText = dataCell.Value.ToString();
            }
        }
    }
}