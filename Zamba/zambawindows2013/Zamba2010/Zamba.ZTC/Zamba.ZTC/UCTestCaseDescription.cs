using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Zamba.Core;
using Zamba.CoreExt;
using Zamba.Data;

namespace Zamba.ZTC
{
    public partial class UCTestCaseDescription : UserControl
    {
        //Se define el menu contextual

        #region Delegates

        public delegate void TestCaseModifiedEventHandler(object sender, EventArgs e);

        public delegate void TestCaseNewExecutionEventHandler(object sender, EventArgs e);

        #endregion

        #region TCModes enum

        public enum TCModes
        {
            ReadOnlyMode,
            EditMode,
            NewMode,
            BlankMode
        };

        #endregion

        private readonly bool _rCreate;
        private bool _rEdit;
        private readonly bool rExecute;


        private StringBuilder sbMod = new StringBuilder();
        private ZTC_CT CT;
        private Int64 CategoryId;
        private Decimal DefaultStepTypeValue;
        private Boolean Editing;
        private Int64 ObjectId;
        private Int64 ObjectTypeId;
        private Decimal TCId;
        private String TCName;
        private IUser User;
        private bool _rDelete;
        private RadDropDownMenu contextMenu;
        private Boolean flagMod;

        private Int64 projectId { get; set; }
        private TCModes CurrentMode { get; set; }

        public Int64 CurrentTestCaseID
        {
            get { return (Int64)TCId; }
        }

        public String CurrentTestCaseName
        {
            get { return TCName; }
        }

        public bool RightToEdit
        {
            get { return _rEdit; }
            set { _rEdit = value; }
        }

        public event TestCaseModifiedEventHandler TestCaseModified;
        public event TestCaseNewExecutionEventHandler TestCaseNewExecution;

        public UCTestCaseDescription(bool rEdit, bool rCreate, bool rDelete, bool rExecute)
        {
            _rEdit = rEdit;
            _rCreate = rCreate;
            _rDelete = rDelete;
            this.rExecute = rExecute;
            InitializeComponent();
            InitializeContextMenu();
            
            var list = new Dictionary<int, string>();
            foreach (object v in Enum.GetValues(typeof (ExportTypes)))
            {
                list.Add((int) v, Enum.GetName(typeof (ExportTypes), (int) v));
            }
            cmbExportTypes.ComboBox.DisplayMember = "Value";
            cmbExportTypes.ComboBox.ValueMember = "Key";
            cmbExportTypes.ComboBox.DataSource = new BindingSource(list, null);

            cmbExportTypes.ComboBox.SelectedIndex = 0;

            list.Clear();
            list = null;
        }

        public Boolean AddTestCase(Int64 _CategoryId, IUser user, Int64 _ObjectTypeId, Int64 _ObjectId,
                                   String TestCaseName, Int64 ProjectId)
        {
            try
            {
                if (Editing == false ||
                    MessageBox.Show("Desea cancelar la edicion del caso de prueba actual?", "Caso de Prueba",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SetMode(TCModes.NewMode);
                    projectId = ProjectId;
                    CategoryId = _CategoryId;
                    User = user;
                    ObjectTypeId = _ObjectTypeId;
                    ObjectId = _ObjectId;
                    txtTitle.Text = TestCaseName;
                    lblversion.Text = "1";
                    lblAuthor.Text = user.Name;
                    lblCreateDate.Text = DateTime.Now.ToShortDateString();
                    lblmodifieddate.Text = DateTime.Now.ToShortDateString();
                    txtDescription.Text = String.Empty;
                    radGridView1.DataSource = null;
                    radGridView1.Refresh();


                    TCId = 0;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        public void SetMode(TCModes Mode)
        {
            try
            {
                switch (Mode)
                {
                    case TCModes.BlankMode:
                        {
                            CurrentMode = TCModes.BlankMode;

                            Enabled = false;
                            txtTitle.Enabled = true;
                            txtDescription.Enabled = false;
                            radGridView1.Enabled = false;
                            radGridView1.ReadOnly = false;
                            btncancel.Visible = true;
                            btnEdit.Visible = false;
                            btnExecute.Visible = false;
                            btnsave.Visible = true;
                            break;
                        }
                    case TCModes.NewMode:
                        {
                            CurrentMode = TCModes.NewMode;
                            Enabled = true;
                            Visible = true;
                            txtTitle.Enabled = true;
                            txtDescription.Enabled = true;
                            radGridView1.Enabled = true;
                            radGridView1.Visible = true;
                            radGridView1.ReadOnly = false;
                            btncancel.Visible = true;
                            btnEdit.Visible = false;
                            btnExecute.Visible = false;
                            btnsave.Visible = true;
                            break;
                        }
                    case TCModes.EditMode:
                        {
                            CurrentMode = TCModes.EditMode;
                            Enabled = true;
                            txtTitle.Enabled = true;
                            txtDescription.Enabled = true;
                            radGridView1.Enabled = true;
                            radGridView1.ReadOnly = false;
                            btncancel.Visible = true;
                            btnEdit.Visible = false;
                            btnExecute.Visible = false;
                            btnsave.Visible = true;
                            break;
                        }
                    case TCModes.ReadOnlyMode:
                        {
                            CurrentMode = TCModes.ReadOnlyMode;
                            Enabled = true;
                            txtTitle.Enabled = false;
                            txtDescription.Enabled = false;
                            radGridView1.Enabled = true;
                            radGridView1.ReadOnly = true;
                            btncancel.Visible = false;
                            if (_rEdit)
                            {
                                btnEdit.Visible = true;
                            }
                            else
                            {
                                btnEdit.Visible = false;
                            }
                            if (rExecute)
                            {
                                btnExecute.Visible = true;
                            }
                            else
                            {
                                btnExecute.Visible = false;
                            }

                            btnsave.Visible = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Carga el caso de prueba
        /// </summary>
        /// <param name="_TCId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Boolean LoadTestCase(Decimal _TCId, IUser user)
        {
            try
            {
                User = user;

                if (Editing == false ||
                    MessageBox.Show("Desea cancelar la edicion del caso de prueba actual?", "Caso de Prueba",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SetMode(TCModes.ReadOnlyMode);

                    TCId = _TCId;
                    var TCB = new TCBusiness();
                    DataTable DsTC = TCB.GetTestCase(TCId);

                    if (DsTC != null && DsTC.Rows.Count > 0)
                    {
                        txtTitle.Text = DsTC.Rows[0]["NodeName"].ToString();
                        lblversion.Text = DsTC.Rows[0]["VN"].ToString();
                        lblAuthor.Text = UserGroupBusiness.GetUserorGroupNamebyId(Int64.Parse(DsTC.Rows[0]["Author"].ToString()));

                        if (int.Parse(DsTC.Rows[0]["Nodetype"].ToString()) == 2)
                        {
                            if (!_rCreate)
                            {
                                btnnewversion.Visible = false;
                            }
                            else
                            {
                                btnnewversion.Visible = true;
                            }
                        }
                        else
                        {
                            btnnewversion.Visible = false;
                        }
                        ;
                        lblCreateDate.Text = DsTC.Rows[0]["CreateDate"].ToString();
                        lblmodifieddate.Text = DsTC.Rows[0]["UpdateDate"].ToString();
                        txtDescription.Text = DsTC.Rows[0]["NodeDescription"].ToString();
                    }

                    IQueryable<ZTC_CT> query = from p in ControlsFactory.dbContext.ZTC_CT
                                               where p.TestCaseId == TCId
                                               select p;
                    IEnumerable<ZTC_CT> TCs = query.ToList();

                    if (TCs.Count() > 0)
                    {
                        CT = TCs.Single();
                    }
                    if (CT != null)
                    {
                        radGridView1.DataSource = CT.ZTC_TS.OrderBy(step => step.StepId);

                        TCId = CT.TestCaseId;
                        TCName = CT.NodeName;

                        projectId = (Int64) TCBusiness.GetProjectId(CT.TestCaseId, CT.ObjectTypeID);

                        txtTitle.Text = DsTC.Rows[0]["NodeName"].ToString();
                        lblversion.Text = DsTC.Rows[0]["VN"].ToString();
                        lblAuthor.Text = UserGroupBusiness.GetUserorGroupNamebyId(Int64.Parse(DsTC.Rows[0]["Author"].ToString()));

                        if (int.Parse(DsTC.Rows[0]["Nodetype"].ToString()) == 2)
                        {
                            btnnewversion.Visible = true;
                        }
                        else
                        {
                            btnnewversion.Visible = false;
                        }
                        ;
                        lblCreateDate.Text = DsTC.Rows[0]["CreateDate"].ToString();
                        lblmodifieddate.Text = DsTC.Rows[0]["UpdateDate"].ToString();
                        txtDescription.Text = DsTC.Rows[0]["NodeDescription"].ToString();

                        GetHistoryInfo(_TCId);
                    }


                    //DataTable DtTCD = TCB.GetTCD(TCId);
                    //LoadTCD(DtTCD);
                    Editing = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private void GetHistoryInfo(decimal tcId)
        {
            try
            {
                //Obtiene la última modificación realizada al Caso de Prueba.
                var ztcData = new ZTCData();
                DataTable dtMod = ztcData.GetTestCaseLastModification(Int64.Parse(tcId.ToString()), 106);

                if (dtMod != null && dtMod.Rows.Count > 0)
                {
                    DataRow r = dtMod.Rows[0];
                    lblModifiedBy.Text = UserGroupBusiness.GetUserorGroupNamebyId(Int64.Parse(r["user_id"].ToString()));
                    toolTipMod.ShowAlways = true;
                    toolTipMod.SetToolTip(lblmodifieddate, r["s_object_id"].ToString());
                }
                else
                {
                    toolTipMod.SetToolTip(lblmodifieddate, "Sin Definir");
                    lblModifiedBy.Text = "Sin Definir";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        private void LoadTCD(DataTable DtTCD)
        {
            try
            {
                radGridView1.MasterTemplate.AutoGenerateColumns = true;

                radGridView1.DataSource = DtTCD;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Guarda en la BD el nuevo caso de prueba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Debe ingresar un titulo valido", "Caso de Prueba");
                }
                else
                {
                    ZTC_CT tcCt = null;

                    if (TCId != 0)
                    {
                        IQueryable<ZTC_CT> query = from p in ControlsFactory.dbContext.ZTC_CT
                                                   where p.TestCaseId == TCId
                                                   select p;

                        tcCt = query.ToList().Single();

                        string auxName = tcCt.NodeName;
                        string auxDesc = tcCt.NodeDescription;

                        tcCt.NodeName = txtTitle.Text;
                        tcCt.UpdateDate = DateTime.Now;
                        tcCt.NodeDescription = txtDescription.Text;

                        if (SaveDescriptions(tcCt) == false)
                        {
                            return;
                        }

                        ControlsFactory.dbContext.SaveChanges();
                        TCId = tcCt.TestCaseId;
                        TCName = tcCt.NodeName;

                        Editing = false;
                        SetMode(TCModes.ReadOnlyMode);

                        var sb = new StringBuilder();

                        if (tcCt.NodeName != auxName)
                            sb.Append("Nombre de caso de prueba de '" + auxName
                                      + "' a '" + tcCt.NodeName);
                        if (tcCt.NodeName != auxName)
                            sb.Append(", ");
                        if (tcCt.NodeDescription != auxDesc)
                            sb.Append("Descripción de caso de prueba de '" + auxDesc
                                      + "' a '" + tcCt.NodeDescription + " ");

                        if (tcCt.NodeName != auxName || tcCt.NodeDescription != auxDesc || flagMod)
                        {
                            //Se genera una entrada en USER_HST Con la modificación de un Caso de prueba 
                            string[] mivar = (from c in ControlsFactory.dbContext.ZTC_CT
                                              where c.TestCaseId == tcCt.ParentNode
                                              select c.NodeName).ToArray();

                            UserBusiness.Rights.SaveAction(Int64.Parse(TCId.ToString()), ObjectTypes.TestCase,
                                                           RightsType.Edit,
                                                           "Modificación del Caso de prueba: '" + auxDesc
                                                           + "' en la categoría: '" + mivar[0] + "'. " + sb + "'" +
                                                           sbMod);
                            flagMod = false;
                        }
                    }
                    else
                    {
                        tcCt = new ZTC_CT();

                        tcCt.NodeName = txtTitle.Text;
                        tcCt.Author = User.ID;
                        tcCt.CreateDate = DateTime.Now;
                        tcCt.UpdateDate = DateTime.Now;
                        tcCt.NodeDescription = txtDescription.Text;
                        tcCt.NodeType = 1;
                        tcCt.ObjectID = ObjectId;
                        tcCt.ObjectTypeID = ObjectTypeId;
                        tcCt.ParentNode = CategoryId;

                        tcCt.VN = 1;
                        tcCt.TestCaseId = CoreData.GetNewID(IdTypes.TestCase);

                        tcCt.NodeType = (int) TestCaseNodeTypes.TestCase;


                        if (SaveDescriptions(tcCt) == false)
                        {
                            return;
                        }

                        ControlsFactory.dbContext.AddToZTC_CT(tcCt);
                        ControlsFactory.dbContext.SaveChanges();
                        TCId = tcCt.TestCaseId;
                        TCName = tcCt.NodeName;
                        ObjectTypeId = (Int64) tcCt.ObjectTypeID;

                       

                        UserBusiness.Rights.SaveAction(Int64.Parse(tcCt.TestCaseId.ToString()), ObjectTypes.TestCase,
                                                       RightsType.Create,
                                                       "Creación del Caso de prueba: " + tcCt.NodeName );
                        sbMod.Clear();
                        try
                        {
                            TCBusiness.AssingTCToProject(TCId, projectId, ObjectTypeId);
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                        Editing = false;
                        LoadTestCase(tcCt.TestCaseId, User);
                        SetMode(TCModes.ReadOnlyMode);
                    }
                    TestCaseModified(TCId, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Editing = true;
                ZClass.raiseerror(ex);
            }
        }

        private void AssingTCToProject(decimal TCId, decimal projectId, decimal objecttypeid)
        {
            try
            {
                var PRO = new PRJ_R_O();

                PRO.OBJID = TCId;
                PRO.OBJTYP = objecttypeid;
                PRO.PRJID = projectId;

                ControlsFactory.dbContext.AddToPRJ_R_O(PRO);
                ControlsFactory.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Guarda las descripciones
        /// </summary>
        /// <param name="CT"></param>
        /// <returns></returns>
        private Boolean SaveDescriptions(ZTC_CT _CT)
        {
            try
            {
                IQueryable<ZTC_TS> currentCTquery = from ct in ControlsFactory.dbContext.ZTC_TS
                                                    where ct.TestCaseID == _CT.TestCaseId
                                                    select ct;

                List<ZTC_TS> list = currentCTquery.ToList();
                int i = 0;

                var newsteps = new EntityCollection<ZTC_TS>();

                Int64 LastStepId = 0;
                if (radGridView1.Rows.Count > 0)
                {
                    foreach (GridViewRowInfo R in radGridView1.Rows)
                    {
                        var Step = new ZTC_TS();
                        Step.TestCaseID = _CT.TestCaseId;
                        Step.StepId = LastStepId + 1;
                        Step.StepDescription = R.Cells[1].Value == null ? String.Empty : R.Cells[1].Value.ToString();
                        Step.StepObservation = R.Cells[2].Value == null ? String.Empty : R.Cells[2].Value.ToString();

                        if (list.Count == 0)
                        {
                            //Concatena en un sbMod los pasos agregados al nuevo TestCase.
                            sbMod.Append(" ID Step: '");
                            sbMod.Append(Step.StepId.ToString());
                            sbMod.Append("' ");
                            sbMod.Append("Desc step: '");
                            sbMod.Append(Step.StepDescription);
                            sbMod.Append("'");
                            sbMod.Append(" Obs step: '");
                            sbMod.Append(Step.StepObservation);
                            sbMod.Append(" '");

                            if (Step.StepDescription == string.Empty)
                            {
                                MessageBox.Show("Hay pasos en los que no se ha completado la descripcion", "ATENCION");
                                return false;
                            }

                            if (R.Cells[3].Value != null)
                            {
                                Step.StepTypeID = Decimal.Parse(R.Cells[3].Value.ToString());

                                sbMod.Append(" Tipo Step: '");
                                sbMod.Append(Step.StepTypeID.ToString());
                                sbMod.Append("'. |");
                            }

                            else
                            {
                                MessageBox.Show("Hay pasos en los que no se ha completado el tipo", "ATENCION");
                                return false;
                            }

                            newsteps.Add(Step);
                            LastStepId = LastStepId + 1;
                        }
                        else
                        {
                            if (list.Count > i)
                            {
                                if (R.Cells[2].Value == null)
                                {
                                    R.Cells[2].Value = string.Empty;
                                }
                                //Concatena en un sbMod las modificaciones realizadas a los pasos de un TestCase.
                                if (list[i].StepDescription != R.Cells[1].Value.ToString() ||
                                    list[i].StepObservation != R.Cells[2].Value.ToString() ||
                                    list[i].StepTypeID.ToString() != R.Cells[3].Value.ToString())
                                {
                                    sbMod.Append("ID Step modificado: '");
                                    sbMod.Append(Step.StepId.ToString());
                                    sbMod.Append("' ");
                                }

                                if (list[i].StepDescription != R.Cells[1].Value.ToString())
                                {
                                    sbMod.Append("Desc step: de '");
                                    sbMod.Append(list[i].StepDescription);
                                    sbMod.Append("' a '");
                                    sbMod.Append(R.Cells[1].Value.ToString());
                                    sbMod.Append("' ");
                                }
                                if (R.Cells[2].Value != null)
                                    if (list[i].StepObservation != R.Cells[2].Value.ToString())
                                    {
                                        sbMod.Append(" Obs step: de '");
                                        sbMod.Append(list[i].StepObservation);
                                        sbMod.Append("' a '");
                                        sbMod.Append(R.Cells[2].Value.ToString());
                                        sbMod.Append("' | ");
                                    }

                                if (Step.StepDescription == string.Empty)
                                {
                                    MessageBox.Show("Hay pasos en los que no se ha completado la descripcion",
                                                    "ATENCION");
                                    return false;
                                }

                                if (R.Cells[3].Value != null)
                                {
                                    Step.StepTypeID = Decimal.Parse(R.Cells[3].Value.ToString());

                                    if (list[i].StepTypeID.ToString() != R.Cells[3].Value.ToString())
                                    {
                                        sbMod.Append(" Tipo Step: de '");
                                        sbMod.Append(list[i].StepObservation);
                                        sbMod.Append("' a '");
                                        sbMod.Append(R.Cells[3].Value.ToString());
                                        sbMod.Append("'");
                                    }
                                }

                                else
                                {
                                    MessageBox.Show("Hay pasos en los que no se ha completado el tipo", "ATENCION");
                                    return false;
                                }

                                newsteps.Add(Step);
                                LastStepId = LastStepId + 1;
                                i++;
                                flagMod = true;
                            }
                            else
                            {
                                //Concatena en un sbMod los nuevos pasos de un TestCase.

                                sbMod.Append("ID Step Nuevo: '");
                                sbMod.Append(Step.StepId.ToString());
                                sbMod.Append("' ");

                                sbMod.Append("Desc step: '");
                                sbMod.Append(R.Cells[1].Value.ToString());
                                sbMod.Append("' ");
                                if (R.Cells[2].Value != null)
                                {
                                    sbMod.Append(" Obs step: de '");
                                    sbMod.Append(R.Cells[2].Value.ToString());
                                    sbMod.Append("' | ");
                                }
                                if (Step.StepDescription == string.Empty)
                                {
                                    MessageBox.Show("Hay pasos en los que no se ha completado la descripcion",
                                                    "ATENCION");
                                    return false;
                                }

                                if (R.Cells[3].Value != null)
                                {
                                    Step.StepTypeID = Decimal.Parse(R.Cells[3].Value.ToString());

                                    sbMod.Append(" Tipo Step: '");
                                    sbMod.Append(R.Cells[3].Value.ToString());
                                    sbMod.Append("'");
                                }

                                else
                                {
                                    MessageBox.Show("Hay pasos en los que no se ha completado el tipo", "ATENCION");
                                    return false;
                                }

                                newsteps.Add(Step);
                                LastStepId = LastStepId + 1;
                                flagMod = true;
                            }
                        }
                    }

                    _CT.ZTC_TS.Clear();
                    foreach (ZTC_TS currentts in newsteps)
                    {
                        _CT.ZTC_TS.Add(currentts);
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar al menos un paso", "ATENCION");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private void UCTestCaseDescription_Load(object sender, EventArgs e)
        {
            try
            {
                SetControlVisibility();


                IQueryable<ZTC_ST> query = from p in ControlsFactory.dbContext.ZTC_ST
                                           select p;

                List<ZTC_ST> List = query.ToList();

                var GridComboBoxColumn = (GridViewComboBoxColumn) radGridView1.MasterTemplate.Columns[3];
                GridComboBoxColumn.DisplayMember = "StepTypeName";
                GridComboBoxColumn.ValueMember = "StepTypeID";
                GridComboBoxColumn.DataSource = List;

                DefaultStepTypeValue = List[0].StepTypeID;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void SetControlVisibility()
        {
            if (!_rCreate)
            {
                btnnewversion.Visible = false;
            }
            if (!_rEdit)
            {
                btnEdit.Visible = false;
            }
            if (!rExecute)
            {
                btnExecute.Visible = false;
            }
            if (!_rEdit)
            {
                btnExecute.Visible = false;
            }
            if (!_rEdit)
            {
                btnsave.Visible = false;
            }
        }
       

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (radGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Ingrese un paso en los casos de prueba antes de ejecutar", "Atención",
                               MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    if (Editing == false ||
                        MessageBox.Show("Desea cancelar la edicion del caso de prueba actual?", "Caso de Prueba",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        TestCaseNewExecution(this, new EventArgs());
                        Editing = false;
                        _rEdit = false;
                       
                        SetMode(TCModes.ReadOnlyMode);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }

        /// <summary>
        /// Cancela la creacion del caso de prueba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                Editing = false;

                LoadTestCase(TCId, User);

                TestCaseModified(TCId, new EventArgs());
           
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void editable(bool executed)
        {
            if (!_rEdit)
            {
                btnEdit.Visible = false;
            }
            else
            {
                btnEdit.Visible = !executed;
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Si el textbox esta habilitado quiere decir que no es un documento nuevo
                //por lo tanto ponemos el Editing en True
                if (txtTitle.Enabled)
                    Editing = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Si el textbox esta habilitado quiere decir que no es un documento nuevo
                //por lo tanto ponemos el Editing en True
                if (txtDescription.Enabled)
                    Editing = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SetMode(TCModes.EditMode);
                var CTAux = new ZTC_CT();
                var newts = new ZTC_TS();
                foreach (ZTC_TS TS in CT.ZTC_TS.OrderBy(step => step.StepId))
                {
                    var Auxts = new ZTC_TS();
                    Auxts.StepId = TS.StepId;
                    Auxts.StepDescription = TS.StepDescription;
                    Auxts.StepObservation = TS.StepObservation;
                    Auxts.StepTypeID = TS.StepTypeID;
                    Auxts.TestCaseID = TS.TestCaseID;
                    CTAux.ZTC_TS.Add(Auxts);
                    
                }
                CT = CTAux;
                radGridView1.DataSource = CT.ZTC_TS;
                radGridView1.Refresh();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String indexrow = "1";
                ControlsFactory.InputBox("Insertar paso entre filas", "Ingrese el numero de fila donde insertar",
                                         ref indexrow);
                var dataRowInfo = new GridViewDataRowInfo(radGridView1.MasterView);
                dataRowInfo.Cells[0].Value = radGridView1.Rows.Count + 1;
                Int32 indexrownumber = 1;
                if (Int32.TryParse(indexrow, out indexrownumber))
                {
                    if (indexrownumber > 0 && radGridView1.Rows.Count >= indexrownumber)
                    {
                        radGridView1.Rows.Insert(indexrownumber - 1, dataRowInfo);

                        try
                        {
                            Int64 LastStepId = 0;
                            foreach (GridViewRowInfo R in radGridView1.Rows)
                            {
                                R.Cells[0].Value = LastStepId + 1;
                                LastStepId = LastStepId + 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }
                    else
                        MessageBox.Show("Ingrese un valor valido", "ATENCION");
                }
                else
                    MessageBox.Show("Ingrese un valor valido", "ATENCION");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void radGridView1_DefaultValuesNeeded_1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Editing = true;

                try
                {
                    Int64 LastStepId = 0;
                    foreach (GridViewRowInfo R in radGridView1.Rows)
                    {
                        R.Cells[0].Value = LastStepId + 1;
                        LastStepId = LastStepId + 1;
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                e.Row.Cells[0].Value = radGridView1.Rows.Count + 1;

                e.Row.Cells[1].Value = String.Empty;
                e.Row.Cells[1].Value = String.Empty;
                e.Row.Cells[3].Value = DefaultStepTypeValue;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        private void GenerateNewVersion(ZTC_CT CT)
        {
            try
            {
                IQueryable<ZTC_CT> currentCTquery = from ct in ControlsFactory.dbContext.ZTC_CT
                                                    where ct.TestCaseId == CT.TestCaseId
                                                    select ct;

                CT = currentCTquery.Single();

                var CTv = new ZTC_CT();

                CTv.VN = CT.VN + 1;
                CTv.TestCaseId = CoreData.GetNewID(IdTypes.TestCase);

                CTv.NodeName = CT.NodeName;
                CTv.Author = User.ID;
                CTv.CreateDate = DateTime.Now;
                CTv.UpdateDate = DateTime.Now;
                CTv.NodeDescription = CT.NodeDescription;
                CTv.NodeType = CT.NodeType;
                CTv.ObjectID = CT.ObjectID;
                CTv.ObjectTypeID = CT.ObjectTypeID;
                CTv.ParentNode = CT.ParentNode;


                CT.ParentNode = CTv.TestCaseId;
                CT.NodeType = 3;

                if (SaveDescriptions(CTv) == false)
                {
                    return;
                }

                ControlsFactory.dbContext.AddToZTC_CT(CTv);
                ControlsFactory.dbContext.SaveChanges();

                TCId = CTv.TestCaseId;
                TCName = CTv.NodeName;
                ObjectTypeId = (Int64) CT.ObjectTypeID;

                try
                {
                    TCBusiness.AssingTCToProject(TCId, projectId, ObjectTypeId);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                Editing = false;
                _rEdit = true;
                LoadTestCase(TCId, User);
                SetMode(TCModes.ReadOnlyMode);

                TestCaseModified(TCId, new EventArgs());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Exporta el contenido de la grilla a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            try
            {
                sfd.Title = "Ingrese la ruta y el nombre del archivo de Excel";
                switch ((ExportTypes) Enum.Parse(typeof (ExportTypes), cmbExportTypes.ComboBox.SelectedValue.ToString())
                    )
                {
                    case ExportTypes.CSV:
                        sfd.Filter = "CSV files (*.csv)|*.csv";
                        break;
                    case ExportTypes.Excel:
                        sfd.Filter = "excel files (*.xls)|*.xls";
                        break;
                    case ExportTypes.PDF:
                        sfd.Filter = "PDF files (*.pdf)|*.pdf";
                        break;
                    case ExportTypes.Word:
                        sfd.Filter = "Word files (*.doc)|*.doc";
                        break;
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var TCB = new TCBusiness();
                    if (TCB.Export_Excel(sfd.FileName, radGridView1,
                                         (ExportTypes)
                                         Enum.Parse(typeof (ExportTypes),
                                                    cmbExportTypes.ComboBox.SelectedValue.ToString())))
                        MessageBox.Show("Exportacion realizada con exito", "Zamba Software", MessageBoxButtons.OK);
                    else
                        MessageBox.Show("Ha ocurrido un error en la exportacion", "Zamba Software", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                sfd.Dispose();
                sfd = null;
            }
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                if (!radGridView1.ReadOnly)
                {
                    e.ContextMenu = contextMenu;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void rmi1_Click(object sender, EventArgs e)
        {
            var CTAux = new ZTC_CT();
            bool errorFound = false;

            try
            {
                var newts = new ZTC_TS();
                newts.StepId = (decimal) radGridView1.CurrentRow.Cells[0].Value;
                newts.StepTypeID = DefaultStepTypeValue;
                Decimal count = 1;
                foreach (ZTC_TS TS in CT.ZTC_TS)
                {
                    if (count == newts.StepId)
                    {
                        CTAux.ZTC_TS.Add(newts);
                        count++;
                    }
                    var Auxts = new ZTC_TS();
                    Auxts.StepId = count;
                    if (TS.StepDescription == null)
                    {
                        MessageBox.Show("Complete las descripciones antes de continuar.", "Atención");
                        errorFound = true;
                        break;
                    }

                    Auxts.StepDescription = TS.StepDescription;
                    Auxts.StepObservation = TS.StepObservation;
                    Auxts.StepTypeID = TS.StepTypeID;
                    Auxts.TestCaseID = TS.TestCaseID;
                    CTAux.ZTC_TS.Add(Auxts);
                    count++;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                errorFound = true;
            }

            //Validamos si se producio un error
            //Si se producio un error no se guarda lo realizado , volviendo al estado anterior
            //Si no se producio guardamos lo realizado para que se puedan agregar mas pasos sucesivamente.
            if (!errorFound)
            {
                CT = CTAux;
                radGridView1.DataSource = CT.ZTC_TS;
                radGridView1.Refresh();
            }
        }

        private void rmi2_Click(object sender, EventArgs e)
        {
            DeleteStep();
        }

        private void DeleteStep()
        {
            var CTAux = new ZTC_CT();
            bool errorFound = false;

            try
            {
                Decimal count = 1;
                foreach (ZTC_TS TS in CT.ZTC_TS.OrderBy(ln => ln.StepId))
                {
                    if (TS.StepId != (decimal) radGridView1.CurrentRow.Cells[0].Value)
                    {
                        var Auxts = new ZTC_TS();
                        Auxts.StepId = count;
                        Auxts.StepDescription = TS.StepDescription;
                        Auxts.StepObservation = TS.StepObservation;
                        Auxts.StepTypeID = TS.StepTypeID;
                        Auxts.TestCaseID = TS.TestCaseID;
                        CTAux.ZTC_TS.Add(Auxts);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                errorFound = true;
            }

            //Validamos si se producio un error
            //Si se producio un error no se guarda lo realizado , volviendo al estado anterior
            //Si no se producio guardamos lo realizado para que se puedan agregar mas pasos sucesivamente.
            if (!errorFound)
            {
                CT = CTAux;
                radGridView1.DataSource = CT.ZTC_TS;
                radGridView1.Refresh();
            }
        }

        private void radGridView1_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
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

        private void InitializeContextMenu()
        {
            if (_rEdit)
            {
                contextMenu = new RadDropDownMenu();
                var rmi1 = new RadMenuItem("Agregar paso");
                rmi1.Click += rmi1_Click;
                contextMenu.Items.Add(rmi1);
                var rmi2 = new RadMenuItem("Quitar paso");
                rmi2.Click += rmi2_Click;
                contextMenu.Items.Add(rmi2);
            }
            radGridView1.ContextMenuOpening += radGridView1_ContextMenuOpening;
        }


        private void btnnewversion_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateNewVersion(CT);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}
