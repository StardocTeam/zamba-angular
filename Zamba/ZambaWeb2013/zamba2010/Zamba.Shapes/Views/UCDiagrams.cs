using System;
using System.Windows.Forms;
using MindFusion.Diagramming;
using Zamba.Core;
using Zamba.Shapes.Controllers;
using System.Collections.Generic;
using Zamba.Diagrams.Shapes;
using Telerik.WinControls.UI;
using System.Drawing;
using MindFusion.Diagramming.Layout;
using Zamba.Shapes.Forms;
using MindFusion.Diagramming.WinForms;

namespace Zamba.Shapes.Views
{
    public delegate void DOpenRuleDiagram(Int64 ruleId);
    
    public partial class UcDiagrams : UserControl
    {

      
        public bool StateOverview;
        /// <summary>
        /// Genera un UserControl que contiene todos los tabs con los diagramas
        /// </summary>
        public UcDiagrams()
        {
            InitializeComponent();
            GenericController.NeedOpenDiagram +=new GenericController.OpenDiagram(GenericController_NeedOpenDiagram);
            if (_overview != null)
            {
                _overview.Close();
                _overview.Dispose();
            }
            // Create the overview window
            _overview = new OverviewForm();
           _overview.Owner = (Form)this.Parent;
           _overview.TopLevel = true;
           _overview.TopMost = true;
            _overview.Show();

        }

        private OverviewForm _overview;
       
        /// <summary>
        /// Agrega un tab con un diagrama específico
        /// </summary>
        /// <param name="diagramType">Tipo de diagrama a graficar</param>
        /// <param name="param">Parámetros opcionales que definen espécificamente lo que se irá a dibujar</param>
        public void AddDiagram(DiagramType diagramType, Object[] param = null)
        {
            try
            {
             new Label() { Name = "lblDiagram", Text = "El diagrama seleccionado no tiene nodos" };
                DiagramTab oldTabToShow = null;
                string newTabName = GetNewTabName(diagramType, param);

                for (int i = 0; i < rpvDiagrams.Pages.Count; i++)
                {
                    if (string.Compare(rpvDiagrams.Pages[i].Name, newTabName) == 0)
                    {
                        oldTabToShow = (DiagramTab)rpvDiagrams.Pages[i];
                        break;
                    }
                }

                if (oldTabToShow != null)
                {
                    this.rpvDiagrams.SelectedPage = oldTabToShow;
                }
                else
                {
                    //Se obtiene el diagrama
                    Diagram diagram = (Diagram)GenericController.GetDiagram(diagramType, param);

                    if (diagram != null)
                    {
                        //Se guarda el tab padre
                        diagram.PreviousDiagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                        //Se crea el tab con el diagrama
                        var diagramTab = new DiagramTab(diagram, param, false);
                        var objectname = string.Empty;

                        if (param != null)
                        {
                            if (param[0].GetType().Name == "TableNode")
                            {
                                objectname = ((TableNode)(param[0])).Caption;
                            }
                            else
                            {
                                objectname = ((MindFusion.Diagramming.ShapeNode)(param[0])).Text;
                            }
                        }

                        diagramTab.Text = DiagramTypeName.GetTypeName(diagramType) + objectname;
                        diagramTab.DiagramView.Diagram.NodeDoubleClicked += DiagramNodeDoubleClicked;
                        diagramTab.DiagramView.Diagram.NodeClicked += DiagramNodeClicked;
                        diagramTab.DiagramView.MouseWheel += new MouseEventHandler(diagramView1_MouseWheel);
                        
                        //Se agrega el tab al TabManager
                        this.rpvDiagrams.Pages.Add(diagramTab);
                        //Se comenta la linea, para que quedé seleccionada la home al inciar.
                        //De otro modo, quedará seleccionado el último diagrama agregado.
                        this.rpvDiagrams.SelectedPage = diagramTab;

                        _overview.SetView(diagramTab.DiagramView);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private string GetNewTabName(DiagramType diagramType, Object[] param = null)
        {
            if (param != null)
            {
                switch (param[0].GetType().Name)
                {
                    case "TableNode":
                        return diagramType.ToString() + ";" + ((TableNode)param[0]).Id.ToString();
                        break;
                    default:
                        if (param[0] is ICore)
                            return diagramType.ToString() + ";" + ((ICore)param[0]).ID.ToString();
                        else
                            return diagramType.ToString() + ";" + ((GenericShape)param[0]).Id.ToString();
                        break;
                }
            }
            else
            {
                return diagramType.ToString() + ";";
            }
        }

        public void SelectFirstPage()
        {
            this.rpvDiagrams.SelectedPage = this.rpvDiagrams.Pages[0];
        }
        public void Refresh()
        {
            try
            {
                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                object[] param = null;

                param = diagramTab.Parameters;
                var diagram = diagramTab.DiagramView.Diagram;
                DiagramType diagramType = ((Zamba.Shapes.Views.Diagram)(diagram)).DiagramType;

                Diagram diagram2 = (Zamba.Shapes.Views.Diagram)GenericController.RefreshDiagram(diagramType, param);
                diagramTab.RefreshDiagramView(diagram2);
                //diagramTab.DiagramView = new DiagramView((MindFusion.Diagramming.Diagram)diagram2);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
 
        }
        public void Refreshpage()
        {
            try
            {
              

                //var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                //object[] param = null;

                //param = diagramTab.Parameters;
                //var diagram = diagramTab.DiagramView.Diagram;
                //DiagramType diagramType = ((Zamba.Shapes.Views.Diagram)(diagram)).DiagramType;

                //Diagram diagram2 = (Diagram)GenericController.GetDiagram(diagramType, param);
                //diagramTab.DiagramView = new DiagramView((MindFusion.Diagramming.Diagram)diagram2);
            }
                catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
     

        /// <summary>
        /// genera un bitmap a partir de un diagrama
        /// </summary>
        /// <param name="type">Tipo de diagrama a graficar</param>
        /// <param name="param">Parámetros opcionales que definen espécificamente lo que se irá a dibujar</param>
        public System.Drawing.Bitmap GenerateDiagramImage(DiagramType type, Object[] param = null)
        {
            //Se obtiene el diagrama
            Diagram diagram = (Diagram)GenericController.GetDiagram(type, param);
            Dictionary<string, System.Drawing.Bitmap> diagram_element = new Dictionary<string, System.Drawing.Bitmap>();
            System.Drawing.Bitmap image = null;

            if (diagram != null)
            {
                //Se guarda el tab padre
                diagram.PreviousDiagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;

                //Se crea el tab con el diagrama
                var diagramTab = new DiagramTab(diagram, param,true);
                var objectname = string.Empty;
                if (param != null)
                {
                    if (param[0].GetType().Name == "TableNode")
                    {
                        objectname = ((TableNode)(param[0])).Caption;
                    }
                    else
                    {
                        objectname = ((MindFusion.Diagramming.ShapeNode)(param[0])).Text;
                    }
                }

                image = diagramTab.DiagramView.Diagram.CreateImage();
            }
            return image;
        }

        /// <summary>
        /// Evento disparado en el diagrama al hacer doble click sobre un Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contiene la propiedad Node que representa el nodo que disparó el evento</param>
        public void DiagramNodeDoubleClicked(object sender, NodeEventArgs e)
        {
            //Verifica si el nodo no es de tipo TableNode y si es raiz del diagrama
            if (!(e.Node is TableNode) && ((GenericShape)e.Node).IsRoot)
            {
                //Se obtiene el tab de diagrama padre
                DiagramTab tabToShow = ((Diagram)e.Node.Parent).PreviousDiagramTab;
                if (tabToShow != null)
                {
                    if (!rpvDiagrams.Pages.Contains(tabToShow))
                        rpvDiagrams.Pages.Add(tabToShow);

                    rpvDiagrams.SelectedPage = tabToShow;
                }
            }
            else
            {
                object[] shape = { e.Node };

                if (shape[0].GetType().Name == "TableNode")
                {
                    AddDiagram(DiagramType.DER, shape);
                }
                else
                {
                    ShapeType SelectedObjectType = ((GenericShape)(shape[0])).ShapeType;

                    switch (SelectedObjectType)
                    {
                        case ShapeType.Workflow:
                            AddDiagram(DiagramType.WorkflowSteps, shape);
                            break;
                        case ShapeType.Step:
                            AddDiagram(DiagramType.StepActions, shape);
                            break;
                        case ShapeType.Rule:
                            if (((DiagramTab)rpvDiagrams.SelectedPage).TbbCategoryFilter == null)
                                AddDiagram(DiagramType.WorkFlowRules, new object[] { e.Node, 1 });
                            else
                                AddDiagram(DiagramType.WorkFlowRules, new object[] { e.Node, GetSelectedCategory() });
                            break;
                        case ShapeType.ZForms:
                            AddDiagram(DiagramType.Forms, new object[] { e.Node, ((FormShape)shape[0]).ZForm });
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Int32 GetSelectedCategory()
        {
            Int32 nextCategory = Int32.Parse(((DiagramTab)rpvDiagrams.SelectedPage).TbbCategoryFilter.Text);

            if (nextCategory == 10)
                return nextCategory;
            else
                return nextCategory + 1;
        }

        /// <summary>
        /// Evento utilizado para mostrar el menú contextual disponible para el Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contiene la propiedad Node que representa el nodo que disparó el evento</param>
        public void DiagramNodeClicked(object sender, NodeEventArgs e)
        {
            if (e.MouseButton == MouseButton.Right)
                GenericController.OpenShapeContextMenu(e.Node, new DOpenRuleDiagram(OpenRuleDiagram), (DiagramTab)rpvDiagrams.SelectedPage);
        }

        public void OpenRuleDiagram(Int64 ruleId)
        {
            Int64 stepId;
            IWFStep wfStep;
            IRule rule;
            StepShape shpStep;
            RuleShape shpRule;
            MindFusion.Diagramming.Diagram tempDiagram;

            try
            {
                stepId = WFStepBusiness.GetStepIdByRuleId(ruleId, true);
                wfStep = WFStepBusiness.GetStepById(stepId);
                rule = WFRulesBusiness.GetInstanceRuleById(ruleId, stepId, true);
                tempDiagram = new MindFusion.Diagramming.Diagram();
                shpStep = new StepShape(tempDiagram, wfStep);
                shpRule = new RuleShape(tempDiagram, rule, shpStep);

                AddDiagram(DiagramType.WorkFlowRules, new object[] { shpRule });
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al mostrar la regla");
            }
            finally
            {
                rule = null;
                shpRule = null;
                wfStep = null;
                shpStep = null;
                tempDiagram = null;
            }
        }

        public System.Drawing.Bitmap GenerateWorkflowDiagram(Int64 workId)
        {
            IWF workflow;
            IWFStep wfStep;
            WFShape workflowShape;
            MindFusion.Diagramming.Diagram tempDiagram;

            try
            {
                workflow = WFlowDiagramBusiness.GetAllWFsByID(workId);
                tempDiagram = new MindFusion.Diagramming.Diagram();
                workflowShape = new WFShape(tempDiagram, workflow);

                return GenerateDiagramImage(DiagramType.WorkflowSteps, new object[] { workflowShape });
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                wfStep = null;
                workflowShape = null;
                tempDiagram = null;
            }
            return null;
        }

        public System.Drawing.Bitmap GenerateStepDiagram(Int64 stepId)
        {
            IWFStep wfStep;
            StepShape stepShape;
            MindFusion.Diagramming.Diagram tempDiagram;

            try
            {
                wfStep = WFStepBusiness.GetStepById(stepId);
                tempDiagram = new MindFusion.Diagramming.Diagram();
                stepShape = new StepShape(tempDiagram, wfStep);

                return GenerateDiagramImage(DiagramType.StepActions, new object[] { stepShape });
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                wfStep = null;
                tempDiagram = null;
            }
            return null;
        }

        public System.Drawing.Bitmap GenerateRuleDiagram(Int64 ruleId)
        {
            Int64 stepId;
            IWFStep wfStep;
            IRule rule;
            StepShape shpStep;
            RuleShape shpRule;
            MindFusion.Diagramming.Diagram tempDiagram;

            try
            {
                stepId = WFStepBusiness.GetStepIdByRuleId(ruleId, true);
                wfStep = WFStepBusiness.GetStepById(stepId);
                rule = WFRulesBusiness.GetInstanceRuleById(ruleId, stepId, true);
                tempDiagram = new MindFusion.Diagramming.Diagram();
                shpStep = new StepShape(tempDiagram, wfStep);
                shpRule = new RuleShape(tempDiagram, rule, shpStep);

                return GenerateDiagramImage(DiagramType.WorkFlowRules, new object[] { shpRule, int.Parse(rule.Category.Value.ToString())});
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                rule = null;
                shpRule = null;
                wfStep = null;
                shpStep = null;
                tempDiagram = null;
            }
            return null;
        }

        private void radPageView1_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            if (e.Page.Text == DiagramTypeName.GetTypeName(DiagramType.Home))
            {
                e.Cancel = true;
            }
        }

        void GenericController_NeedOpenDiagram(IDiagram diagramToOpen, object[] parameters)
        {
            if (diagramToOpen != null)
            {
                DiagramTab oldTabToShow = null;
                string newTabName = GetNewTabName(diagramToOpen.DiagramType, parameters);

                for (int i = 0; i < rpvDiagrams.Pages.Count; i++)
                {
                    if (string.Compare(rpvDiagrams.Pages[i].Name, newTabName) == 0)
                    {
                        oldTabToShow = (DiagramTab)rpvDiagrams.Pages[i];
                        break;
                    }
                }

                if (oldTabToShow != null)
                {
                    this.rpvDiagrams.SelectedPage = oldTabToShow;
                }
                else
                {
                    //Se crea el tab con el diagrama
                    var diagramTab = new DiagramTab(diagramToOpen, parameters, false);

                    string objectname = string.Empty;

                    if (parameters != null)
                    {
                        if (parameters[0] is ShapeNode)
                            objectname = ((ShapeNode)parameters[0]).Text;
                        else
                            objectname = ((ICore)parameters[0]).Name;
                    }

                    diagramTab.Text = DiagramTypeName.GetTypeName(diagramToOpen.DiagramType) + objectname;

                    diagramTab.DiagramView.Diagram.NodeDoubleClicked += DiagramNodeDoubleClicked;
                    diagramTab.DiagramView.Diagram.NodeClicked += DiagramNodeClicked;

                    //Se agrega el tab al TabManager
                    this.rpvDiagrams.Pages.Add(diagramTab);
                    //Se comenta la linea, para que quedé seleccionada la home al inciar.
                    //De otro modo, quedará seleccionado el último diagrama agregado.
                    this.rpvDiagrams.SelectedPage = diagramTab;

                }
            }
        }

        private void ddviews_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
            Layout layout;

            switch (this.ddviews.SelectedItem.Tag.ToString())
            {
                case "TreeLayout":
                    TreeLayout layout1 = new TreeLayout();
                    layout1.NodeDistance = 20;
                    layout1.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;
                case "FlowchartLayout":
                    FlowchartLayout layout2 = new FlowchartLayout();
                    layout2.NodeDistance = 20;
                    layout2.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;

                case "HierarchicalLayout":
                    HierarchicalLayout layout3 = new HierarchicalLayout();
                    layout3.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;

                case "LayeredLayout":
                    LayeredLayout layout4 = new LayeredLayout();
                    layout4.NodeDistance = 20;
                    layout4.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;

                case "CircularLayout":
                    CircularLayout layout5 = new CircularLayout();
                    layout5.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;

                case "GridLayout":
                    GridLayout layout6 = new GridLayout();
                    layout6.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);
                    break;

                case "SpringLayour":

                    // apply SwimlaneLayout
                    SpringLayout layout7 = new SpringLayout();
                    layout7.NodeDistance = 20;
                    layout7.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);


                    break;

                case "SwinLane":


                    // apply SwimlaneLayout
                    SwimlaneLayout layout8 = new SwimlaneLayout();
                    layout8.NodeDistance = 20;
                    layout8.Arrange(diagramTab.DiagramView.Diagram);

                    diagramTab.DiagramView.Diagram.ResizeToFitItems(30);


                    break;


            }

            // zoom to show the whole diagram
            //RectangleF r = diagramTab.DiagramView.Diagram.Bounds;
            //r.Inflate(5, 5);
            //diagramTab.DiagramView.Diagram.Bounds = r;
            //diagramTab.DiagramView.ZoomToRect(r);

        }

        public void diagramView1_MouseWheel(object sender, MouseEventArgs e)
        {
            var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
        
            diagramTab.DiagramView.ZoomFactor += e.Delta / 40;
        }

        private void commandBarButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF files|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MindFusion.Diagramming.Export.PdfExporter pdfExp =
                    new MindFusion.Diagramming.Export.PdfExporter();
                pdfExp.Export(diagramTab.DiagramView.Diagram, saveFileDialog.FileName);
            }

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "svg";
                saveFileDialog.Filter = "SVG files|*.svg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MindFusion.Diagramming.Export.SvgExporter svgExp =
                        new MindFusion.Diagramming.Export.SvgExporter();
                    svgExp.ExternalImages = false;
                    svgExp.Export(diagramTab.DiagramView.Diagram, saveFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void commandBarButton3_Click(object sender, EventArgs e)
        {
            try
            {
                                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "png";
            saveFileDialog.Filter = "PNG files|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = diagramTab.DiagramView.Diagram.CreateImage();
                image.Save(saveFileDialog.FileName);
                image.Dispose();
            }

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            try
            {
                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                diagramTab.DiagramView.ZoomFactor += 10;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void commandBarButton5_Click(object sender, EventArgs e)
        {
            try
            {
                var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
                diagramTab.DiagramView.ZoomFactor += -10;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void rpvDiagrams_SelectedPageChanged(object sender, EventArgs e)
        {
            var diagramTab = (DiagramTab)this.rpvDiagrams.SelectedPage;
            _overview.SetView(diagramTab.DiagramView);
        }

    }
}
