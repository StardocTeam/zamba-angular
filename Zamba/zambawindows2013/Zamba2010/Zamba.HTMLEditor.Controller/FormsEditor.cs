using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Zamba.Core;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.HtmlControls;
using System.Collections;
using System.Data;

namespace Zamba.HTMLEditor.Controller
{
    public partial class FormsEditor
    {
        #region Atributos
        private VirtualFormsBuilder _mainForm = null;
        private IZwebForm _form;
        private IDocType _docType;
        #endregion

        #region Constructores
        public FormsEditor()
        {
        }

        public FormsEditor(IDocType docType)
        {
            _docType = docType;
        }
        public FormsEditor(IZwebForm form, IDocType docType)
            : this(docType)
        {
            _form = form;
        }
        #endregion

        #region Eventos
        private static void _mainForm_OnSavedDocument(string filePath)
        {
        }

        private void _mainForm_OnLoadWorkflows()
        {
            List<IWorkFlow> WfsList = WFBusiness.GetWorkflows();

            Dictionary<Int64, String> ParsedWfs = new Dictionary<long, string>(WfsList.Count);
            foreach (IWorkFlow CurrentWorkflow in WfsList)
                ParsedWfs.Add(CurrentWorkflow.ID, CurrentWorkflow.Name);

            WfsList.Clear();
            WfsList = null;
            _mainForm.Workflows = ParsedWfs;
        }
        private void _mainForm_OnSelectedWorkflowChanged(long workflowId)
        {
            List<IWFStep> StepList = WFStepBusiness.GetStepsByWorkflow(workflowId);

            Dictionary<Int64, String> ParsedSteps = new Dictionary<long, string>(StepList.Count);
            foreach (IWFStep CurrentWorkflow in StepList)
                ParsedSteps.Add(CurrentWorkflow.ID, CurrentWorkflow.Name);

            StepList.Clear();
            StepList = null;
            _mainForm.Steps = ParsedSteps;
        }

        private void _mainForm_OnSelectedStep(long stepId)
        {
            _mainForm.Rules = WFRulesBusiness.GetRulesIdsAndNames(stepId);
        }

        #endregion

        /// <summary>
        /// Muestra el formulario
        /// </summary>
        public void Show()
        {
            LoadFormEditor();
            _mainForm.ShowDialog();
        }

        /// <summary>
        /// Carga los valores en el formulario
        /// </summary>
        private void LoadFormEditor()
        {
            try
            {
                if (null != _form && !String.IsNullOrEmpty(_form.Path))
                    _mainForm = new VirtualFormsBuilder(_form.Path);
                else
                    _mainForm = new VirtualFormsBuilder();

                _mainForm.OnSavedDocument += new VirtualFormsBuilder.SavedDocument(_mainForm_OnSavedDocument);
                _mainForm.OnLoadWorkflows += new VirtualFormsBuilder.LoadWorkflows(_mainForm_OnLoadWorkflows);  
                _mainForm.OnSelectedStep += new VirtualFormsBuilder.SelectedStep(_mainForm_OnSelectedStep);
                _mainForm.OnSelectedWorkflowChanged += new VirtualFormsBuilder.SelectedWorkflow(_mainForm_OnSelectedWorkflowChanged);

                _mainForm.DocTypeName = _docType.Name;

                List<BaseHtmlElement> Elements = new List<BaseHtmlElement>(_docType.Indexs.Count);
                Elements.Add(new TableItem("Documentos Asociados", "zamba_associated_documents"));
                //Elements.Add(new ButtonItem("Guardar Atributos", "zamba_save"));

                Elements.Add(new ButtonItem("Cancelar", "btncancel", true, "submit",true));
                Elements.Add(new ButtonItem("Guardar", "btnsave", true, "submit",true));

                foreach (IIndex CurrentIndex in _docType.Indexs)
                {
                    switch (CurrentIndex.Type)
                    {
                        case IndexDataType.Alfanumerico:
                        case IndexDataType.Alfanumerico_Largo:

                            switch (CurrentIndex.DropDown)
                            {
                                case IndexAdditionalType.AutoSustitución:
                                case IndexAdditionalType.DropDown:
                                    Elements.Add(new ZSelectItem(CurrentIndex.Name, null, true, CurrentIndex.ID));
                                    break;
                                case IndexAdditionalType.LineText:
                                    Elements.Add(new ZTextBoxItem(CurrentIndex.Name, string.Empty, false, true, CurrentIndex.ID, CurrentIndex.Len));
                                    break;
                                default:
                                    break;
                            }

                            break;

                        case IndexDataType.Fecha:
                        case IndexDataType.Fecha_Hora:
                            switch (CurrentIndex.DropDown)
                            {
                                case IndexAdditionalType.AutoSustitución:
                                case IndexAdditionalType.DropDown:
                                    Elements.Add(new ZSelectItem(CurrentIndex.Name, null, true, CurrentIndex.ID));
                                    break;
                                case IndexAdditionalType.LineText:
                                    Elements.Add(new ZTextBoxItem(CurrentIndex.Name, string.Empty, false, true, CurrentIndex.ID, CurrentIndex.Len));
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case IndexDataType.Moneda:
                        case IndexDataType.Numerico_Decimales:
                        case IndexDataType.Numerico:
                        case IndexDataType.Numerico_Largo:
                            switch (CurrentIndex.DropDown)
                            {
                                case IndexAdditionalType.AutoSustitución:
                                case IndexAdditionalType.DropDown:
                                    Elements.Add(new ZSelectItem(CurrentIndex.Name, null, true, CurrentIndex.ID));
                                    break;
                                case IndexAdditionalType.LineText:
                                    Elements.Add(new ZTextBoxItem(CurrentIndex.Name, string.Empty, false, true, CurrentIndex.ID, CurrentIndex.Len));
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case IndexDataType.Si_No:
                            Elements.Add(new ZCheckBoxItem(CurrentIndex.Name, false, true, CurrentIndex.ID));
                            break;
                    }
                }

                _mainForm.LoadHtmlElements(Elements);
            }

            catch (Exception ex)
            {
                ZCore.raiseerror(ex);
                MessageBox.Show(ex.Message);
            }
        }

      

        public String FilePath()
        {
            return _mainForm.FilePath();
        }
    }
}
