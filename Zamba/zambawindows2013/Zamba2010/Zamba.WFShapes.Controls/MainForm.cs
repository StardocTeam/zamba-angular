using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Zamba.Core;
using Zamba.WFShapes.Core;
using Zamba.AppBlock;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming;

namespace Zamba.WFShapes.Controls
{
    //Declaro el delegado que va a manejar el agregar de los steps en el arbol
    public delegate void AddedStep(IWFStep NewStep);
    public delegate void RemovedStep(IWFStep DelStep);
    public delegate void NameStep(IWFStep NewStep);
    public delegate void AddedRule(IZambaCore ruleStep, Int64 IDRule);
    public delegate void RemovedRule(IWFRuleParent rule, IWFStep step);
    public delegate void NameRule(Int64 id, string name);
    public delegate void DesignStep(Int64 stepId, String name);
    public delegate void DesignRule(Int64 ruleId, String name);

    /// <summary>
    /// Main class or form of the demo.
    /// </summary>
    public partial class MainForm : ZControl
    {

        #region variables
        private PrintDocument p = null;
        private IWorkFlow oWorkFlow = null;

        //Declaro una variable del delegado
        private AddedStep dAddStep = null;
        private RemovedStep dRemoveStep = null;
        private NameStep dNameStep = null;
        private AddedRule dAddRule = null;
        private RemovedRule dRemoveRule = null;
        private NameRule dNameRule = null;
        private DesignStep dDesignStep = null;
        #endregion

        #region eventos 
        private void passStep(IWFStep NewStep)
        {
            if (this.dAddStep != null)
                dAddStep(NewStep);
        }
        private void passRule(IWFStep ruleStep, Int64 IDRule)
        {
            if (this.dAddRule != null)
                dAddRule(ruleStep, IDRule);
        }
        private void passDelStep(IWFStep DelStep)
        {
            if (this.dRemoveStep != null)
                dRemoveStep(DelStep);
        }

        private void nameStep(IWFStep NewStep)
        {
            if (this.dNameStep != null)
                dNameStep(NewStep);
        }

        private void passDelRule(IWFRuleParent DelRule, IWFStep step)
        {
            if (this.dRemoveRule != null)
                dRemoveRule(DelRule, step);
        }

        private void nameRule(Int64 id, string name)
        {
            if (this.dNameRule != null)
                dNameRule(id, name);
        }
        #endregion

        #region Constructor and initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// Alejandro Ruetalo
        /// 
        public MainForm(IWorkFlow wf, Boolean ShowEditControls)
        {
            this.oDiagramShape = new Zamba.WFShapes.Core.DiagramShape(!ShowEditControls);

            InitializeComponent();

            this.oWorkFlow = wf;

            cargarWorkFlow();

            //Para la impresion del diagrama
            p = new PrintDocument();
            p.PrintPage += new PrintPageEventHandler(this.imprimir);

            if (ShowEditControls == true)
            {
                this.oDiagramShape.OnAddShape += new AddedShape(this.passStep);
                this.oDiagramShape.OnAddRuleShape += new AddedRuleShape(this.passRule);
                this.oDiagramShape.OnRemoveShape += new RemovedShape(this.passDelStep);
                this.oDiagramShape.OnNameShape += new NameShape(this.nameStep);
                this.oDiagramShape.OnRemoveConnection += new RemovedConnection(this.passDelRule);
                this.oDiagramShape.OnNameConnection += new NameConnection(this.nameRule);
            }
            else
            {
                this.mainStrip.Visible = false;
                this.actionsStrip.Visible = false;
                this.eliminarShape.Visible = false;
                this.eliminarConnection.Visible = false;
                tlpDistribute.Visible = false;
                tlpStep.Visible = false;
                //this.drawingStrip.Visible = false;
                //  this.stripContainer.Visible = false;
            }

            mainStrip.Focus();

        }
        #endregion

        #region Undo/redo tools

        /// <summary>
        /// Handles the Click event of the undoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void undoButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.Undo();
        }

        /// <summary>
        /// Handles the Click event of the redoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void redoButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.Redo();
        }

        /// <summary>
        /// Updates the undo/redo buttons to reflect the history changes
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Zamba.WFShapes.HistoryChangeEventArgs"/> instance containing the event data.</param>
        private void diagramControl1_OnHistoryChange(object sender, HistoryChangeEventArgs e)
        {
            //this.tabDiagram.AutoScrollMinSize = new Size(this.diagramControl1.Width, this.diagramControl1.Height);
        }
        #endregion

        #region Toolbars actions
        /// <summary>
        /// Inserta una nueva etapa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpStep_Click(object sender, EventArgs e)
        {
            if (this.diagramControl1.Controller.Tools[7].IsActive == true)
                this.diagramControl1.Controller.Tools[7].DeactivateTool();
            oDiagramShape.newShape(this.oWorkFlow, this.diagramControl1);
        }

        /// <summary>
        /// Recarga el diagrama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpRefresh_Click(object sender, EventArgs e)
        {
            cargarWorkFlow();
        }

        /// <summary>
        /// Imprime el diagrama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpPrint_Click(object sender, EventArgs e)
        {
            Imprimir_Click(sender, e);
        }

        /// <summary>
        /// Activo la herramienta de conexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpDistribute_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Connection Tool");
        }
        /// <summary>
        /// View or hide the actions strip.
        /// </summary>
        private void ViewHideActionsStrip()
        {
            actionsToolStripMenuItem.Checked = !actionsToolStripMenuItem.Checked;
            actionsStrip.Visible = actionsToolStripMenuItem.Checked;
        }

        /// <summary>
        /// View or hide the drawing strip.
        /// </summary>
        private void ViewHideDrawingStrip()
        {
            drawingToolStripMenuItem.Checked = !drawingToolStripMenuItem.Checked;
            //drawingStrip.Visible = drawingToolStripMenuItem.Checked;
        }

        #endregion

        #region Grouping tools
        /// <summary>
        /// Handles the Click event of the groupButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void groupButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Group Tool");
        }

        /// <summary>
        /// Handles the Click event of the ungroupButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ungroupButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Ungroup Tool");
        }
        #endregion

        #region Z-ordering tools
        /// <summary>
        /// Handles the Click event of the sendToBackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void sendToBackButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("SendToBack Tool");
        }

        /// <summary>
        /// Handles the Click event of the sendBackwardsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void sendBackwardsButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("SendBackwards Tool");
        }

        /// <summary>
        /// Handles the Click event of the sendForwardButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void sendForwardButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("SendForwards Tool");
        }

        /// <summary>
        /// Handles the Click event of the sendToFrontButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void sendToFrontButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("SendToFront Tool");
        }

        #endregion

        #region Standard tools


       


        #endregion

        #region Context menu of the toolbars
        /// <summary>
        /// Handles the Click event of the standardToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            standardToolStripMenuItem.Checked = !standardToolStripMenuItem.Checked;
            mainStrip.Visible = standardToolStripMenuItem.Checked;
        }
        /// <summary>
        /// Handles the Click event of the drawingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void drawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHideDrawingStrip();
        }
        /// <summary>
        /// Handles the Click event of the actionsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHideActionsStrip();
        }
        #endregion

        #region Diagram control handler links
        private void diagramControl1_OnShowSelectionProperties(object sender, SelectionEventArgs e)
        {

            // this.propertyGrid.SelectedObjects = e.SelectedObjects;
        }
        #endregion

        #region Drawing tools
        /// <summary>
        /// Handles the Click event of the drawRectangleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void drawRectangleButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Rectangle Tool");
        }
        /// <summary>
        /// Handles the Click event of the drawEllipseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void drawEllipseButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Ellipse Tool");
        }

        /// <summary>
        /// Handles the Click event of the drawingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void drawingButton_Click(object sender, EventArgs e)
        {
            ViewHideDrawingStrip();
        }

        /// <summary>
        /// Activa la herramienta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shapesListView_DoubleClick(object sender, EventArgs e)
        {
            ListView item = (ListView)sender;
            if (item.SelectedItems.Count == 1)
            {
                if (item.SelectedItems[0].Text == "Regla")
                {
                    this.diagramControl1.ActivateTool("Connection Tool");
                }
                else
                {
                    if (this.diagramControl1.Controller.Tools[7].IsActive == true)
                        this.diagramControl1.Controller.Tools[7].DeactivateTool();
                    oDiagramShape.newShape(this.oWorkFlow, this.diagramControl1);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the connectionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void connectionButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Connection Tool");
        }

        /// <summary>
        /// Handles the Click event of the connectorMoverButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void connectorMoverButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Connector Mover Tool");
        }

        private void textToolButton_Click(object sender, EventArgs e)
        {
            this.diagramControl1.ActivateTool("Text Tool");
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        /// <summary>
        /// Shows the about-info.
        /// </summary>
        private void ShowAbout()
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Zamba.WFShapes.Controls.Resources.AboutTemplate.htm");
                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);
                    string template = reader.ReadToEnd();
                    template = template.Replace("$title$", AssemblyInfo.AssemblyTitle);
                    template = template.Replace("$version$", AssemblyInfo.AssemblyVersion);
                    template = template.Replace("$company$", AssemblyInfo.AssemblyCompany);
                    template = template.Replace("$description$", AssemblyInfo.AssemblyDescription);

                    reader.Close();
                    stream.Close();

                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Title: {0}", AssemblyInfo.AssemblyTitle);
                    sb.Append("<br/>");
                    sb.AppendFormat("Version: {0}", AssemblyInfo.AssemblyVersion);
                    sb.Append("<br/>");
                    sb.AppendFormat("Company: {0}", AssemblyInfo.AssemblyCompany);
                    sb.Append("<br/>");
                    sb.AppendFormat("Description: {0}", AssemblyInfo.AssemblyDescription);

                }
            }
            catch
            {
            }

        }

        private void classDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\html\\index.html";
            //this.webBrowser.Url = new Uri(path);
        }

        private void diagramControl1_OnEntityAdded(object sender, EntityEventArgs e)
        {
            string msg = string.Empty;

            //le asigno a los shapes y las connections su contextmenu            
            if (e.Entity is IShape)
            {
                msg = "New shape '" + e.Entity.EntityName + "' added.";
                e.Entity.contextMenu = this.contextMenuStrip1;
            }
            else if (e.Entity is IConnection)
            {
                msg = "New connection added.";
                e.Entity.contextMenu = this.contextMenuStrip2;
                IConnection conexion = (IConnection)e.Entity;
                if (conexion.Name == "DoDistribuir")
                {
                    if (this.dAddRule != null)
                        foreach (IDiagramEntity shape in diagramControl1.Controller.Model.Pages[0].DefaultLayer.Entities)
                            if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                                if (shape.ZambaObject.ID == conexion.Id1)
                                {
                                    dAddRule(shape.ZambaObject, conexion.Id);
                                    break;
                                }
                }
            }

            if (msg.Length > 0)
                ShowStatusText(msg);
            if (e.Entity is ComplexRectangle)
            {
                ComplexRectangle shape = e.Entity as ComplexRectangle;

                try
                {
                    shape.Services[typeof(IMouseListener)] = new MyPlugin(shape);
                }
                catch (ArgumentException exc)
                {
                    ShowStatusText(exc.Message);

                }


            }
            else if (e.Entity is ImageShape)
            {
                Bitmap bmp = GetSampleImage();
                if (bmp != null)
                    (e.Entity as ImageShape).Image = bmp;
            }
        }

        private Bitmap GetSampleImage()
        {

            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Zamba.WFShapes.Controls.Resources.Talking.gif");
            Bitmap bmp = null;
            if (stream != null)
            {
                bmp = Bitmap.FromStream(stream) as Bitmap;
                stream.Close();
            }
            return bmp;
        }

        private void ShowStatusText(string text)
        {
            //this.statusLabel1.Text = text;
            //this.statusLabel1.Visible = true;
            ////statusTimer.Start();
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            //statusTimer.Stop();
            //  this.statusLabel1.Visible = false;
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //statusProgressBar.Visible = true;
            //statusProgressBar.Value = 0;
        }

        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // statusProgressBar.Value = Convert.ToInt32(Math.Round(e.CurrentProgress * (double)100 / e.MaximumProgress, 0));
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //statusProgressBar.Visible = false;
        }

        private void shapesListView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            Cursor.Current = Cursors.HSplit;
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            //this.leftTabControl.SelectedTab = tabProperties;
            //this.propertyGrid.SelectedObjects = diagramControl1.SelectedItems.ToArray();
        }

        ///<summary>
        /// Traigo un workflow y lleno el grafico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton1_Click(object sender, EventArgs e)//Boton Actualizar
        {
            cargarWorkFlow();
        }

        /// <summary>
        /// Llena el diagrama con los valores del WorkFlow del componente
        /// </summary>
        public void cargarWorkFlow()
        {
            try
            {
                if (this.oWorkFlow != null)
                {
                    diagramControl1.ClearDiagram();
                    oDiagramShape.loadWF(diagramControl1, this.oWorkFlow);
                    oDiagramShape.DibujarFlechaWF(diagramControl1, this.oWorkFlow);
                    diagramControl1.SelectedItems.Clear();
                    oDiagramShape.ActualizarTamaño(diagramControl1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Llena el diagrama con los valores de un WorkFlow 
        /// </summary>
        /// <param name="WF"></param>
        public void cargarWorkFlow(IWorkFlow WF, Boolean ShowEditControls)
        {
            try
            {
                if (WF != null)
                {
                    diagramControl1.ClearDiagram();
                    oDiagramShape.loadWF(diagramControl1, WF);
                    oDiagramShape.DibujarFlechaWF(diagramControl1, WF);
                    diagramControl1.SelectedItems.Clear();
                    oDiagramShape.ActualizarTamaño(diagramControl1);
                    if (ShowEditControls == true)
                    {
                    }
                    else
                    {
                        this.mainStrip.Visible = false;
                        this.actionsStrip.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene las propiedades de impresion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog PD = new PrintDialog();
                PD.UseEXDialog = true;
                PD.Document = p;
                DialogResult resultado = PD.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    p.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo imprimir, excepción: " + ex.ToString().Substring(20), "Zamba - Imprimir Imagen®");
                Exception exn = new Exception("Error al imprimir imagen® ,en mainForm, excepción: " + ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Imprime el dibujo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imprimir(object sender, PrintPageEventArgs e)
        {
            try
            {
                diagramControl1.ObtenerDibujo(e.Graphics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void Diagram_controlDoubleClick(object sender, EventArgs e)
        {
            EditDiagram();
        }

        private void EditDiagram()
        {
            if (this.diagramControl1.SelectedItems.Count > 0)
            {
                if (this.diagramControl1.SelectedItems[0] is Zamba.WFShapes.IShape)
                {
                    IShape shape = (IShape)this.diagramControl1.SelectedItems[0];
                    if (this.dDesignStep != null)
                    {

                        this.dDesignStep(shape.ZambaObject.ID, shape.ZambaObject.Name);
                    }
                }
            }
        }






        #region events
        /// <summary>
        /// Manejo del addStep
        /// </summary>
        public event AddedStep OnAddStep
        {
            add
            {
                this.dAddStep += value;
            }
            remove
            {
                this.dAddStep -= value;

            }
        }

        /// <summary>
        /// Manejo del RemoveStep
        /// </summary>
        public event RemovedStep OnRemoveStep
        {
            add
            {
                this.dRemoveStep += value;
            }
            remove
            {
                this.dRemoveStep -= value;
            }
        }

        /// <summary>
        /// Nombre
        /// </summary>
        public event NameStep OnNameStep
        {
            add
            {
                this.dNameStep += value;
            }
            remove
            {
                this.dNameStep -= value;
            }
        }

        /// <summary>
        /// Manejo del RemoveRule
        /// </summary>
        public event RemovedRule OnRemoveRule
        {
            add
            {
                this.dRemoveRule += value;
            }
            remove
            {
                this.dRemoveRule -= value;
            }
        }
        /// <summary>
        /// Manejo del AddRule
        /// </summary>
        public event AddedRule OnAddRule
        {
            add
            {
                this.dAddRule += value;
            }
            remove
            {
                this.dAddRule -= value;
            }
        }

        public event NameRule OnNameRule
        {
            add
            {
                this.dNameRule += value;
            }
            remove
            {
                this.dNameRule -= value;
            }
        }

        public event DesignStep OnDesignStep
        {
            add
            {
                this.dDesignStep += value;
            }
            remove
            {
                this.dDesignStep -= value;
            }
        }
        #endregion

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void editarShape_Click(object sender, EventArgs e)
        {
            EditDiagram();
        }

        private void editarShape_Click_1(object sender, EventArgs e)
        {
            EditDiagram();
        }

        private void BtRefreshClick(object sender, EventArgs e)
        {
            cargarWorkFlow();
        }

        private void btImprimir_Click(object sender, EventArgs e)
        {
            Imprimir_Click(sender, e);
        }

        /// <summary>
        /// Cambia el color de la etapa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarColorShape_Click(object sender, EventArgs e)
        {
            oDiagramShape.CambiarColorClick(sender, e);
        }

        /// <summary>
        /// Cambiar el icono
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarIconoShape_Click(object sender, EventArgs e)
        {
            oDiagramShape.CambiarIconoClick(sender, e);
        }

        /// <summary>
        /// Borra la etapa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eliminarShape_Click(object sender, EventArgs e)
        {
            oDiagramShape.eliminar_Click(sender, e);
        }

        /// <summary>
        /// Cambia el nombre de la conexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarNombreConnection_Click(object sender, EventArgs e)
        {
            oDiagramShape.cambiarNombre_Click(sender, e);
        }

        /// <summary>
        /// Eliminar conexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eliminarConnection_Click(object sender, EventArgs e)
        {
            oDiagramShape.eliminar_Click(sender, e);
        }

        /// <summary>
        /// Cambiar el nombre de la etapa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarNombreShape_Click(object sender, EventArgs e)
        {
            oDiagramShape.cambiarNombre_Click(sender, e);
        }
    }

    public class MyPlugin : IMouseListener
    {
        ComplexRectangle shape;
        public MyPlugin(ComplexRectangle shape)
        {
            this.shape = shape;
        }

        #region IClickListener Members

        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.shape.ShapeColor = ArtPallet.RandomColor;
            (this.shape.Children[0] as LabelMaterial).Text = this.shape.ShapeColor.ToString();
            //foreach (IPaintable material in shape.Children)
            //{
            //    //material.;
            //}
        }

        #endregion

        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {

        }

        public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }
    }

}
