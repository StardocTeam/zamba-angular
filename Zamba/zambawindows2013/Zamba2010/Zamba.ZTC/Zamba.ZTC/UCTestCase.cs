using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Zamba.Core;

namespace Zamba.ZTC
{
    public partial class UcTestCase : UserControl
    {
        #region Delegates

        public delegate void TestCaseDeletedEventHandler(object sender, EventArgs e);

        #endregion

        private readonly bool _rCreate;
        private readonly bool _rDelete;
        private readonly bool _rEdit = false;
        private readonly bool _rExecute;
        private ControlsFactory _controlsFactory;
        private bool _cutNode;
        public string TestCaseName { get; set; }

        /// <summary>
        /// Clase de caso de prueba
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="objectId"></param>
        /// <param name="nombre"></param>
        /// <param name="projectid"></param>
        /// <param name="rEdit"></param>
        /// <param name="rCreate"></param>
        /// <param name="rDelete"></param>
        /// <param name="rExecute"></param>
        public UcTestCase(Int64 objType, Int64 objectId, string nombre, Int64 projectid, bool rEdit, bool rCreate,
                          bool rDelete, bool rExecute)
        {
            try
            {
                _rEdit = rEdit;
                _rCreate = rCreate;
                _rDelete = rDelete;
                _rExecute = rExecute;
                InitializeComponent();

                TestCaseName = nombre + " Id " + objectId;

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se abre el caso de prueba bajo el nombre de : " + TestCaseName);
                _controlsFactory = new ControlsFactory(objType, objectId, rtvTestCase, ucTestCaseDescription1,
                                                      ucTestCaseNewExecution1, ucTestCaseExecutionHistory1, projectid);
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                ZClass.raiseerror(ex);
            }
        }
        
        private void UcTestCaseLoad(object sender, EventArgs e)
        {
            try
            {
                SetControlVisibility();

                if (_controlsFactory != null)
                {
                    _controlsFactory.LoadTree();
                    rtvTestCase.Focus();
                    ucTestCaseDescription1.Enabled = false;
                    ucTestCaseDescription1.TestCaseModified += UcTestCaseDescription1TestCaseModified;
                    ucTestCaseDescription1.TestCaseNewExecution += UcTestCaseDescription1TestCaseNewExecution;
                    ucTestCaseExecutionHistory1.TestCaseEXSelected += UcTestCaseExecutionHistory1TestCaseExSelected;
                    ucTestCaseNewExecution1.TestCaseNewExecutionCanceled += UcTestCaseNewExecution1TestCaseNewExecutionCanceled;

                    Editable();
                }
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
                mnuAddCategory.Enabled = false;
            }

            if (!_rEdit)
            {
                mnuCategoryName.Enabled = false;
            }

            if (!_rDelete)
            {
                mnuDeleteCategory.Enabled = false;
            }

            if (!_rCreate)
            {
                mnuAddTestCase.Enabled = false;
            }

            if (!_rEdit)
            {
                mnuCopyTestCase.Enabled = false;
            }

            if (!_rEdit)
            {
                mnuCutTestCase.Enabled = false;
            }

            if (!_rEdit)
            {
                mnuCutTestCase.Enabled = false;
            }

            if (!_rDelete)
            {
                mnuDeleteTestCase.Enabled = false;
            }

            if (!_rCreate)
            {
                btnnewcategory.Enabled = false;
            }

            if (!_rCreate)
            {
                btnNewTC.Enabled = false;
            }
        }

        private void UcTestCaseNewExecution1TestCaseNewExecutionCanceled(object sender, EventArgs e)
        {
            try
            {
                ucTestCaseDescription1.RightToEdit = _rEdit;
                ShowTc(Int64.Parse(sender.ToString()));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void Editable()
        {
            ucTestCaseDescription1.editable(ucTestCaseExecutionHistory1.hasExecution());
        }

        public void ShowTc(Int64 testCaseId)
        {
            try
            {
                //Falta Buscar en el arbol y seleccionar el nodo
                ctrlTestCase.Visible = true;
                _controlsFactory.LoadTestCase(testCaseId);
                _controlsFactory.SelectNodeByID(testCaseId);
                ctrlTestCase.SelectedPage = radPageViewPage1;
                ucTestCaseDescription1.Enabled = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void UcTestCaseDescription1TestCaseNewExecution(object sender, EventArgs e)
        {
            try
            {
                Int64 currentTestCaseId = ((UCTestCaseDescription) sender).CurrentTestCaseID;
                String currentTestCaseName = ((UCTestCaseDescription) sender).CurrentTestCaseName;

                ctrlTestCase.SelectedPage = radPageViewPage2;
                ucTestCaseNewExecution1.Visible = true;
                ucTestCaseNewExecution1.BringToFront();
                ucTestCaseNewExecution1.AddExecutionTestCase(currentTestCaseId, currentTestCaseName);
                _controlsFactory.ucTestCaseNewExecution.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void UcTestCaseExecutionHistory1TestCaseExSelected(object sender, EventArgs e)
        {
            try
            {
                ctrlTestCase.SelectedPage = radPageViewPage2;
                ucTestCaseNewExecution1.Visible = true;
                ucTestCaseNewExecution1.BringToFront();
                ucTestCaseNewExecution1.LoadExecutionTestCase(Int64.Parse(sender.ToString()));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Recarga el arbol y blanquea el panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcTestCaseDescription1TestCaseModified(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.LoadTree();
                _controlsFactory.SelectNodeByID((decimal) sender);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RtvCasesSelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    if (((DataRowView) e.Node.DataBoundItem).Row.ItemArray[4].ToString() != "1")
                    {
                        btnNewTC.Enabled = false;
                        _controlsFactory.LoadTestCase(Int64.Parse(e.Node.Value.ToString()));
                        ctrlTestCase.SelectedPage = radPageViewPage1;
                        ucTestCaseDescription1.Enabled = true;
                        ucTestCaseNewExecution1.Visible = true;
                        ctrlTestCase.Visible = true;
                        Editable();
                    }
                    else
                    {
                        btnNewTC.Enabled = true;
                        ctrlTestCase.Visible = false;
                    }
                }
                else
                {
                    btnNewTC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuAddCategoryClick(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.AddCategory();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuCategoryNameClick(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.ChangeCategoryName();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuDeleteCategoryClick(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.DeleteCategory();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuAddCaseClick(object sender, EventArgs e)
        {
            try
            {
                ctrlTestCase.Visible = true;

                _controlsFactory.AddTestCase();
                ucTestCaseDescription1.Enabled = true;
                ucTestCaseDescription1.Visible = true;
                ctrlTestCase.Pages[0].Show();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public event TestCaseDeletedEventHandler TestCaseDeleted;

        private void MnuDeleteCaseClick(object sender, EventArgs e)
        {
            try
            {
                if (_controlsFactory != null)
                {
                    _controlsFactory.SelectedTestCaseId();

                    if (_controlsFactory.DeleteTestCase())
                    {
                        radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
                        ctrlTestCase.Visible = false;
                        TestCaseDeleted(this, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuCopyTestCaseClick(object sender, EventArgs e)
        {
            try
            {
                _cutNode = false;
                FrmMain.CopiedNode = rtvTestCase.SelectedNode;
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
                //falta: debe seleccionar el nuevo caso
                ctrlTestCase.Visible = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuCutTestCaseClick(object sender, EventArgs e)
        {
            try
            {
                _cutNode = true;
                FrmMain.CopiedNode = rtvTestCase.SelectedNode;
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
                ctrlTestCase.Visible = false;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MnuPasteTestCaseClick(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.PasteTestCase(FrmMain.CopiedNode, _cutNode);
                FrmMain.CopiedNode = null;
                ctrlTestCase.Pages[0].Show();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
                ctrlTestCase.Visible = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RtvTestCaseContextMenuOpening(object sender, TreeViewContextMenuOpeningEventArgs e)
        {
            try
            {
                radContextMenu.Items.Clear();

                //Verifica si existe un nodo seleccionado
                if (e.Node != null)
                {
                    //Verifica si es una categoría
                    if (((DataRowView) e.Node.DataBoundItem).Row.ItemArray[4].ToString() == "1")
                    {
                        radContextMenu.Items.AddRange(new RadItem[]
                                                          {
                                                              mnuCategoryHeader,
                                                              mnuAddCategory,
                                                              mnuCategoryName,
                                                              mnuDeleteCategory,
                                                              mnuTestCaseHeader,
                                                              mnuAddTestCase
                                                          });

                        if (FrmMain.CopiedNode != null) radContextMenu.Items.Add(mnuPasteTestCase);
                    }
                    else
                    {
                        radContextMenu.Items.AddRange(new RadItem[]
                                                          {
                                                              mnuTestCaseHeader,
                                                              mnuCopyTestCase,
                                                              //this.mnuCutTestCase,//ACA descomentado CUANDO ESTE IMPLEMENTADA LA FUNCIONALIDAD DE CORTAR
                                                              mnuDeleteTestCase
                                                          });
                    }
                }
                else
                {
                    radContextMenu.Items.AddRange(new RadItem[]
                                                      {
                                                          mnuCategoryHeader,
                                                          mnuAddCategory
                                                      });
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void SelectedInitialNode(Int64 ucid)
        {
            _controlsFactory.SelectNodeByID(ucid);
        }

        private void RtvTestCaseMouseClick(object sender, MouseEventArgs e)
        {
            //            radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
        }

        private void RadPageView1SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                if (ctrlTestCase.SelectedPage == radPageViewPage1)
                {
                    if (rtvTestCase.SelectedNode != null)
                    {
                        if (ucTestCaseNewExecution1.ExecutionSaved)
                        {
                            ucTestCaseNewExecution1.Visible = true;
                            Editable();

                        }
                        ucTestCaseDescription1.Enabled = true;
                        ucTestCaseDescription1.Visible = true;


                        ucTestCaseNewExecution1.Visible = false;
                        _controlsFactory.ucTestCaseDescription.Refresh();
        
                     
                       
                    }
                }
                if (ctrlTestCase.SelectedPage == radPageViewPage2)
                {
                    if (rtvTestCase.SelectedNode != null)
                    {
                        ucTestCaseDescription1.Enabled = false;
                        ucTestCaseDescription1.Visible = false;


                        ucTestCaseNewExecution1.Visible = true;
                        //ML: Esta llamada creo que esta mal, porque le esta pasando el id de TC en lugar del ID del EX, que igualmente en este evento no tiene sentido.
//                   this.controlsFactory.ucTestCaseNewExecution.LoadExecutionTestCase(Int64.Parse(this.rtvTestCase.SelectedNode.Value.ToString()));
                        _controlsFactory.ucTestCaseNewExecution.Refresh();
                    }
                }
                if (ctrlTestCase.SelectedPage == radPageViewPage3)
                {
                    if (rtvTestCase.SelectedNode != null)
                    {
                        ucTestCaseDescription1.Enabled = false;
                        ucTestCaseDescription1.Visible = false;


                        ucTestCaseNewExecution1.Visible = false;

                        _controlsFactory.ucTestCaseExecutionHistory.LoadExecutionHistoryTestCase(
                        Int64.Parse(rtvTestCase.SelectedNode.Value.ToString()));
                      
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void BtnnewcategoryClick(object sender, EventArgs e)
        {
            try
            {
                _controlsFactory.AddCategory();
                radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void BtnNewTcClick(object sender, EventArgs e)
        {
            try
            {
                //Verifica si existe un nodo seleccionado
                if (rtvTestCase.SelectedNode != null)
                {
                    //Verifica si es una categoría
                    if (((DataRowView) rtvTestCase.SelectedNode.DataBoundItem).Row.ItemArray[4].ToString() == "1")
                    {
                        ctrlTestCase.Visible = true;
                        _controlsFactory.AddTestCase();                        
                        ucTestCaseDescription1.Enabled = true;
                        ctrlTestCase.SelectedPage = radPageViewPage1;
                        ctrlTestCase.Pages[0].Show();
                        radContextMenu.DropDown.ClosePopup(RadPopupCloseReason.AppFocusChange);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}