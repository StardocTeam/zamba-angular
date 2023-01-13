using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Zamba.Core;
using Zamba.CoreExt;
using Zamba.Data;
using Zamba.Membership;

namespace Zamba.ZTC
{
    public partial class UCTestCaseNewExecution : UserControl
    {
        #region Delegates

        public delegate void TestCaseNewExecutionCanceledEventHandler(object sender, EventArgs e);

        #endregion

        private readonly bool _rExecute;

        private Decimal _executionId;

        private bool _saveExecutionError;
        private Decimal _tcId;

        public bool ExecutionSaved { get; set; }

        public UCTestCaseNewExecution(bool rExecute)
        {
            _rExecute = rExecute;
            InitializeComponent();
        }
        
        public void AddExecutionTestCase(Int64 testCaseId, String testCaseName)
        {
            Enabled = true;
            radExecutionGrid.ReadOnly = false;
            radExecutionGrid.Enabled = true;
            btnsave.Enabled = true;
            txtDescription.Enabled = true;
            txtDescription.ReadOnly = false;
            lblTitle.Enabled = true;
            ExecutionSaved = false;

            lblTitle.Text = testCaseName;
            _tcId = testCaseId;

            radExecutionGrid.DataSource = null;
            radExecutionGrid.Refresh();

            Enabled = true;

            LoadExecutionTestCase(0);
        }

        public void LoadExecutionTestCase(Decimal executionId)
        {
            Enabled = true;
            _executionId = executionId;
            ZTC_EX ex = null;

            if (_executionId != 0)
            {
                IQueryable<ZTC_EX> query = from p in ControlsFactory.dbContext.ZTC_EX
                                           where p.ExecutionId == _executionId
                                           select p;

                IEnumerable<ZTC_EX> eXs = query.ToList();
                ex = eXs.Single();

                _tcId = ex.TestCaseID;
                lblAuthor.Text = UserGroupBusiness.GetUserorGroupNamebyId((Int64) ex.UserId);
                lblCreateDate.Text = ex.ExecutionDate.ToString();
                txtDescription.Text = ex.Comment;
                radExecutionGrid.ReadOnly = true;
                radExecutionGrid.Enabled = true;
                txtDescription.ReadOnly = true;
                btnsave.Enabled = false;
            }
            else
            {
                lblAuthor.Text = UserGroupBusiness.GetUserorGroupNamebyId(MembershipHelper.CurrentUser.ID);
                lblCreateDate.Text = DateTime.Now.ToString();
                txtDescription.Text = String.Empty;
            }

            IQueryable<ZTC_CT> query1 = from p in ControlsFactory.dbContext.ZTC_CT
                                        where p.TestCaseId == _tcId
                                        select p;

            IEnumerable<ZTC_CT> TCs = query1.ToList();


            if (TCs.Count() > 0)
            {
                ZTC_CT CT = null;
                CT = TCs.Single();

                if (CT != null)
                {
                    lblTitle.Text = CT.NodeName;
                    radExecutionGrid.DataSource = CT.ZTC_TS.OrderBy(step => step.StepId);
                    if (ex != null) LoadCheckboxs(ex);
                }
            }
        }

        private void LoadCheckboxs(ZTC_EX EX)
        {
            radExecutionGrid.ValueChanged -= RadGridView1ValueChanged;
            if (EX.ExecutionResultID == 1)
            {
                //Carga los checkboxs
                for (int i = 0; i <= EX.LastStepExecutionID - 1; i++)
                {
                    radExecutionGrid.Rows[i].Cells[4].Value = true;
                    radExecutionGrid.Rows[i].Cells[5].Value = false;
                }
            }
            else
            {
                //Carga los checkboxs
                for (int i = 0; i <= EX.LastStepExecutionID - 1; i++)
                    if (i != EX.LastStepExecutionID - 1)
                    {
                        radExecutionGrid.Rows[i].Cells[4].Value = true;
                        radExecutionGrid.Rows[i].Cells[5].Value = false;
                    }
                    else
                    {
                        radExecutionGrid.Rows[i].Cells[4].Value = false;
                        radExecutionGrid.Rows[i].Cells[5].Value = true;
                    }
            }
            radExecutionGrid.ValueChanged += RadGridView1ValueChanged;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                
                ZTC_EX ex = null;

                if (_executionId != 0)
                {
                    IQueryable<ZTC_EX> query = from p in ControlsFactory.dbContext.ZTC_EX
                                               where p.ExecutionId == _executionId
                                               select p;

                    ex = query.ToList().Single();
                    ex.Comment = txtDescription.Text;

                    ControlsFactory.dbContext.SaveChanges();
                    ExecutionSaved = true;
                }
                else
                {
                    ex = new ZTC_EX
                             {
                                 UserId = Membership.MembershipHelper.CurrentUser.ID,
                                 ExecutionDate = DateTime.Now,
                                 Comment = txtDescription.Text,
                                 ExecutionId = CoreData.GetNewID(IdTypes.TestCaseExecution),
                                 TestCaseID = _tcId
                             };

                    CheckLastStepAndResult(ex);

                    if (_saveExecutionError)
                    {
                        MessageBox.Show("Por favor complete correctamente los resultados antes de continuar", "Atención",
                                        MessageBoxButtons.OK);
                    }
                    else
                    {
                        ControlsFactory.dbContext.AddToZTC_EX(ex);
                        ControlsFactory.dbContext.SaveChanges();
                        LoadExecutionTestCase(ex.ExecutionId);
                        ExecutionSaved = true;
                        
                        
                    }
                }
                radExecutionGrid.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Error al guardar la ejecución del caso de prueba", "Zamba Software",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckLastStepAndResult(ZTC_EX EX)
        {
            foreach (GridViewRowInfo r in radExecutionGrid.Rows)
            {
                _saveExecutionError = false;
                EX.ExecutionResultID = 2;
                EX.LastStepExecutionID = 0;

                bool chkOK = Convert.ToBoolean(r.Cells[4].Value);
                bool chkError = Convert.ToBoolean(r.Cells[5].Value);

                if (chkError)
                {
                    EX.ExecutionResultID = 2;
                    EX.LastStepExecutionID = r.Index + 1;
                    //El break es para dejar que solo haya un error.
                    break;
                }
                else if (chkOK)
                {
                    EX.ExecutionResultID = 1;
                    EX.LastStepExecutionID = r.Index + 1;
                }
                else
                {
                    _saveExecutionError = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Carga la descripcion de los casos de prueba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcTestCaseDescriptionLoad(object sender, EventArgs e)
        {
            try
            {
                this.radExecutionGrid.ContextMenu = null;
                if (ControlsFactory.EntityConnectionString != null)
                {
                    IQueryable<ZTC_ST> query = from p in ControlsFactory.dbContext.ZTC_ST
                                               select p;

                    List<ZTC_ST> list = query.ToList();

                    var GridComboBoxColumn =
                        (GridViewComboBoxColumn) radExecutionGrid.MasterTemplate.Columns[3];
                    GridComboBoxColumn.ValueMember = "StepTypeID";
                    GridComboBoxColumn.DisplayMember = "StepTypeName";
                    GridComboBoxColumn.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RadGridView1ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int current =
                (((GridCellElement)
                  ((((BaseInputEditor)(sender)).EditorElement).Parent)).ColumnInfo).Index;
                GridViewRowInfo row =
                    ((GridViewEditManager)
                     (((BaseInputEditor)(sender)).EditorManager)).GridViewElement.CurrentRow;
                var val =
                    (ToggleState)
                    ((RadCheckBoxEditor)(sender)).Value;

                radExecutionGrid.ValueChanged -= RadGridView1ValueChanged;

                if (current == 5)
                {
                    row.Cells[4].Value = val == ToggleState.On ? ToggleState.Off : ToggleState.On;
                }
                else if (current == 4)
                {
                    row.Cells[5].Value = val == ToggleState.On ? ToggleState.Off : ToggleState.On;
                }

                radExecutionGrid.ValueChanged += RadGridView1ValueChanged;
                row = null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RadExecutionGridCellValueNeeded(object sender, GridViewCellValueEventArgs e)
        {
            //Verifica que solo se encuentre cargando las columnas con los checkboxs
            if (e.ColumnIndex >= 4)
            {
                //Ver que es el sender
                //obtener el dato de la ultima fila
                //si fila actual supera a la ultima entonces no proceso
                //      si fila actual no tiene error
            }
        }

        private void RadExecutionGridToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            try
            {
                var cell = sender as GridDataCellElement;
                if (cell != null)
                {
                    var dataCell = sender as GridDataCellElement;
                    if (dataCell.Value != null)
                    {
                        e.ToolTipText = dataCell.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public event TestCaseNewExecutionCanceledEventHandler TestCaseNewExecutionCanceled;

        private void BtncancelClick(object sender, EventArgs e)
        {
            Visible = false;
            TestCaseNewExecutionCanceled(_tcId, new EventArgs());
        }
    }
}