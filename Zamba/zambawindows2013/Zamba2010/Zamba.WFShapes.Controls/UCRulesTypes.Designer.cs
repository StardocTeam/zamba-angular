using Zamba.WFShapes;
using Zamba.WFShapes.Controls;
using Zamba.WFShapes.Controls.Properties;
using Zamba.WFShapes.Tools;
using Zamba.WFShapes.Core;
using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.WFShapes.Controls
{
    partial class UCRulesTypes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.stripContainer = new ToolStripContainer();
            this.contextMenuToolbars = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagramControl1 = new Zamba.WFShapes.Win.DiagramControl();
            this.tstToolbar = new ZToolBar();
            this.tlpStep = new System.Windows.Forms.ToolStripButton();
            this.tlpDistribute = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlpPrint = new System.Windows.Forms.ToolStripButton();
            this.tlpRefresh = new System.Windows.Forms.ToolStripButton();
            this.shapesImageList = new System.Windows.Forms.ImageList(this.components);
            this.statusLabel1 = new ToolStripStatusLabel();
            this.statusProgressBar = new ToolStripProgressBar();
            this.actionsStrip = new ZToolBar();
            this.sendToBackButton = new System.Windows.Forms.ToolStripButton();
            this.sendBackwardsButton = new System.Windows.Forms.ToolStripButton();
            this.sendForwardButton = new System.Windows.Forms.ToolStripButton();
            this.sendToFrontButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.groupButton = new System.Windows.Forms.ToolStripButton();
            this.ungroupButton = new System.Windows.Forms.ToolStripButton();
            this.drawingStrip = new ZToolBar();
            this.drawRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.drawEllipseButton = new System.Windows.Forms.ToolStripButton();
            this.textToolButton = new System.Windows.Forms.ToolStripButton();
            this.mainStrip = new ZToolBar();
            this.propertiesButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.pointerButton = new System.Windows.Forms.ToolStripButton();
            this.drawingButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Actualizar = new System.Windows.Forms.ToolStripButton();
            this.Imprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.classDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cambiarColorShape = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.cambiarIconoShape = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.cambiarNombreShape = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.editarShape = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.eliminarShape = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cambiarNombreConnection = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.eliminarConnection = new Zamba.WFShapes.Tools.ShapeToolStripMenuItem();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.stripContainer.ContentPanel.SuspendLayout();
            this.stripContainer.TopToolStripPanel.SuspendLayout();
            this.stripContainer.SuspendLayout();
            this.contextMenuToolbars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).BeginInit();
            this.tstToolbar.SuspendLayout();
            this.actionsStrip.SuspendLayout();
            this.drawingStrip.SuspendLayout();
            this.mainStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripContainer
            // 
            // 
            // stripContainer.BottomZToolBarPanel
            // 
            this.stripContainer.BottomToolStripPanel.ContextMenuStrip = this.contextMenuToolbars;
            // 
            // stripContainer.ContentPanel
            // 
            this.stripContainer.ContentPanel.AutoScroll = true;
            this.stripContainer.ContentPanel.Controls.Add(this.diagramControl1);
            this.stripContainer.ContentPanel.Size = new System.Drawing.Size(785, 588);
            this.stripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // stripContainer.LeftZToolBarPanel
            // 
            this.stripContainer.LeftToolStripPanel.ContextMenuStrip = this.contextMenuToolbars;
            this.stripContainer.Location = new System.Drawing.Point(0, 0);
            this.stripContainer.Name = "stripContainer";
            // 
            // stripContainer.RightZToolBarPanel
            // 
            this.stripContainer.RightToolStripPanel.ContextMenuStrip = this.contextMenuToolbars;
            this.stripContainer.Size = new System.Drawing.Size(785, 613);
            this.stripContainer.TabIndex = 1;
            this.stripContainer.Text = "ToolStripContainer1";
            // 
            // stripContainer.TopToolStripPanel
            // 
            this.stripContainer.TopToolStripPanel.ContextMenuStrip = this.contextMenuToolbars;
            this.stripContainer.TopToolStripPanel.Controls.Add(this.tstToolbar);
            // 
            // contextMenuToolbars
            // 
            this.contextMenuToolbars.Items.AddRange(new ToolStripItem[] {
            this.standardToolStripMenuItem,
            this.drawingToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.contextMenuToolbars.Name = "contextMenuToolbars";
            this.contextMenuToolbars.Size = new System.Drawing.Size(130, 70);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Checked = true;
            this.standardToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.standardToolStripMenuItem.Text = "Standard";
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // drawingToolStripMenuItem
            // 
            this.drawingToolStripMenuItem.Checked = true;
            this.drawingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawingToolStripMenuItem.Name = "drawingToolStripMenuItem";
            this.drawingToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.drawingToolStripMenuItem.Text = "Drawing";
            this.drawingToolStripMenuItem.Click += new System.EventHandler(this.drawingToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Checked = true;
            this.actionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.actionsToolStripMenuItem.Text = "Actions";
            this.actionsToolStripMenuItem.Click += new System.EventHandler(this.actionsToolStripMenuItem_Click);
            // 
            // diagramControl1
            // 
            this.diagramControl1.AllowDrop = true;
            this.diagramControl1.BackColor = System.Drawing.Color.White;
            this.diagramControl1.BackgroundType = Zamba.WFShapes.CanvasBackgroundTypes.FlatColor;
            this.diagramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl1.EnableAddConnection = true;
            this.diagramControl1.Location = new System.Drawing.Point(0, 0);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.Size = new System.Drawing.Size(785, 588);
            this.diagramControl1.TabIndex = 1;
            this.diagramControl1.DoubleClick += new System.EventHandler(this.Diagram_controlDoubleClick);
            this.diagramControl1.OnEntityAdded += new System.EventHandler<Zamba.WFShapes.EntityEventArgs>(this.diagramControl1_OnEntityAdded);
            this.diagramControl1.OnShowSelectionProperties += new System.EventHandler<Zamba.WFShapes.SelectionEventArgs>(this.diagramControl1_OnShowSelectionProperties);
            this.diagramControl1.OnHistoryChange += new System.EventHandler<Zamba.WFShapes.HistoryChangeEventArgs>(this.diagramControl1_OnHistoryChange);
            // 
            // tstToolbar
            // 
            this.tstToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.tstToolbar.Items.AddRange(new ToolStripItem[] {
            this.tlpStep,
            this.tlpDistribute,
            this.ToolStripSeparator1,
            this.tlpPrint,
            this.tlpRefresh});
            this.tstToolbar.Location = new System.Drawing.Point(3, 0);
            this.tstToolbar.Name = "tstToolbar";
            this.tstToolbar.Size = new System.Drawing.Size(281, 25);
            this.tstToolbar.TabIndex = 0;
            // 
            // tlpStep
            // 
            this.tlpStep.Image = ((System.Drawing.Image)(resources.GetObject("tlpStep.Image")));
            this.tlpStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpStep.Name = "tlpStep";
            this.tlpStep.Size = new System.Drawing.Size(55, 22);
            this.tlpStep.Text = "Etapa";
            this.tlpStep.ToolTipText = "Agregar una etapa al Workflow";
            this.tlpStep.Click += new System.EventHandler(this.tlpStep_Click);
            // 
            // tlpDistribute
            // 
            this.tlpDistribute.Image = global::Zamba.WFShapes.Controls.Properties.Resources.Connection;
            this.tlpDistribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpDistribute.Name = "tlpDistribute";
            this.tlpDistribute.Size = new System.Drawing.Size(69, 22);
            this.tlpDistribute.Text = "Distribuir";
            this.tlpDistribute.Click += new System.EventHandler(this.tlpDistribute_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlpPrint
            // 
            this.tlpPrint.Image = ((System.Drawing.Image)(resources.GetObject("tlpPrint.Image")));
            this.tlpPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpPrint.Name = "tlpPrint";
            this.tlpPrint.Size = new System.Drawing.Size(65, 22);
            this.tlpPrint.Text = "Imprimir";
            this.tlpPrint.Click += new System.EventHandler(this.tlpPrint_Click);
            // 
            // tlpRefresh
            // 
            this.tlpRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tlpRefresh.Image")));
            this.tlpRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpRefresh.Name = "tlpRefresh";
            this.tlpRefresh.Size = new System.Drawing.Size(74, 22);
            this.tlpRefresh.Text = "Refrescar";
            this.tlpRefresh.Click += new System.EventHandler(this.tlpRefresh_Click);
            // 
            // shapesImageList
            // 
            this.shapesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("shapesImageList.ImageStream")));
            this.shapesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.shapesImageList.Images.SetKeyName(0, "ClassShape.png");
            this.shapesImageList.Images.SetKeyName(1, "DecisionShape.png");
            this.shapesImageList.Images.SetKeyName(2, "ImageShape.png");
            this.shapesImageList.Images.SetKeyName(3, "");
            // 
            // statusLabel1
            // 
            this.statusLabel1.Image = global::Zamba.WFShapes.Controls.Properties.Resources.info;
            this.statusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(16, 17);
            this.statusLabel1.Visible = false;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Alignment = ToolStripItemAlignment.Right;
            this.statusProgressBar.AutoToolTip = true;
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(150, 16);
            this.statusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.statusProgressBar.Visible = false;
            // 
            // actionsStrip
            // 
            this.actionsStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.actionsStrip.Items.AddRange(new ToolStripItem[] {
            this.sendToBackButton,
            this.sendBackwardsButton,
            this.sendForwardButton,
            this.sendToFrontButton,
            this.ToolStripSeparator6,
            this.groupButton,
            this.ungroupButton});
            this.actionsStrip.Location = new System.Drawing.Point(82, 25);
            this.actionsStrip.Name = "actionsStrip";
            this.actionsStrip.Size = new System.Drawing.Size(154, 25);
            this.actionsStrip.TabIndex = 7;
            // 
            // sendToBackButton
            // 
            this.sendToBackButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.sendToBackButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.SendToBackHS;
            this.sendToBackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendToBackButton.Name = "sendToBackButton";
            this.sendToBackButton.Size = new System.Drawing.Size(23, 22);
            this.sendToBackButton.Text = "Send to back";
            this.sendToBackButton.Click += new System.EventHandler(this.sendToBackButton_Click);
            // 
            // sendBackwardsButton
            // 
            this.sendBackwardsButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.sendBackwardsButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.SendBackwardHS;
            this.sendBackwardsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendBackwardsButton.Name = "sendBackwardsButton";
            this.sendBackwardsButton.Size = new System.Drawing.Size(23, 22);
            this.sendBackwardsButton.Text = "Send backwards";
            this.sendBackwardsButton.Click += new System.EventHandler(this.sendBackwardsButton_Click);
            // 
            // sendForwardButton
            // 
            this.sendForwardButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.sendForwardButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.BringForwardHS;
            this.sendForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendForwardButton.Name = "sendForwardButton";
            this.sendForwardButton.Size = new System.Drawing.Size(23, 22);
            this.sendForwardButton.Text = "Send forward";
            this.sendForwardButton.Click += new System.EventHandler(this.sendForwardButton_Click);
            // 
            // sendToFrontButton
            // 
            this.sendToFrontButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.sendToFrontButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.BringToFrontHS;
            this.sendToFrontButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendToFrontButton.Name = "sendToFrontButton";
            this.sendToFrontButton.Size = new System.Drawing.Size(23, 22);
            this.sendToFrontButton.Text = "Send to front";
            this.sendToFrontButton.Click += new System.EventHandler(this.sendToFrontButton_Click);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // groupButton
            // 
            this.groupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.groupButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.Group;
            this.groupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.groupButton.Name = "groupButton";
            this.groupButton.Size = new System.Drawing.Size(23, 22);
            this.groupButton.Text = "Group";
            this.groupButton.Click += new System.EventHandler(this.groupButton_Click);
            // 
            // ungroupButton
            // 
            this.ungroupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ungroupButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.Ungroup;
            this.ungroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ungroupButton.Name = "ungroupButton";
            this.ungroupButton.Size = new System.Drawing.Size(23, 22);
            this.ungroupButton.Text = "Ungroup";
            this.ungroupButton.Click += new System.EventHandler(this.ungroupButton_Click);
            // 
            // drawingStrip
            // 
            this.drawingStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.drawingStrip.Items.AddRange(new ToolStripItem[] {
            this.drawRectangleButton,
            this.drawEllipseButton,
            this.textToolButton});
            this.drawingStrip.Location = new System.Drawing.Point(3, 25);
            this.drawingStrip.Name = "drawingStrip";
            this.drawingStrip.Size = new System.Drawing.Size(79, 25);
            this.drawingStrip.TabIndex = 6;
            // 
            // drawRectangleButton
            // 
            this.drawRectangleButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.drawRectangleButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.DrawRectangle;
            this.drawRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawRectangleButton.Name = "drawRectangleButton";
            this.drawRectangleButton.Size = new System.Drawing.Size(23, 22);
            this.drawRectangleButton.Text = "Draw rectangle";
            this.drawRectangleButton.Click += new System.EventHandler(this.drawRectangleButton_Click);
            // 
            // drawEllipseButton
            // 
            this.drawEllipseButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.drawEllipseButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.DrawEllipse;
            this.drawEllipseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawEllipseButton.Name = "drawEllipseButton";
            this.drawEllipseButton.Size = new System.Drawing.Size(23, 22);
            this.drawEllipseButton.Text = "Draw ellipse";
            this.drawEllipseButton.Click += new System.EventHandler(this.drawEllipseButton_Click);
            // 
            // textToolButton
            // 
            this.textToolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.textToolButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.FontDialogHS;
            this.textToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.textToolButton.Name = "textToolButton";
            this.textToolButton.Size = new System.Drawing.Size(23, 22);
            this.textToolButton.Text = "Text tool";
            this.textToolButton.ToolTipText = "Text tool";
            this.textToolButton.Click += new System.EventHandler(this.textToolButton_Click);
            // 
            // mainStrip
            // 
            this.mainStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainStrip.Items.AddRange(new ToolStripItem[] {
            this.propertiesButton,
            this.ToolStripSeparator,
            this.pointerButton,
            this.drawingButton,
            this.ToolStripSeparator2,
            this.Actualizar,
            this.Imprimir});
            this.mainStrip.Location = new System.Drawing.Point(20, 20);
            this.mainStrip.Name = "mainStrip";
            this.mainStrip.Size = new System.Drawing.Size(200, 200);
            this.mainStrip.TabIndex = 5;
            // 
            // propertiesButton
            // 
            this.propertiesButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.propertiesButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.PropertiesHS;
            this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(23, 197);
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(6, 200);
            // 
            // pointerButton
            // 
            this.pointerButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.pointerButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.StandardArrow1;
            this.pointerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointerButton.Name = "pointerButton";
            this.pointerButton.Size = new System.Drawing.Size(23, 197);
            this.pointerButton.Text = "Pointer tool";
            // 
            // drawingButton
            // 
            this.drawingButton.Checked = true;
            this.drawingButton.CheckOnClick = true;
            this.drawingButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawingButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.drawingButton.Image = global::Zamba.WFShapes.Controls.Properties.Resources.Drawing;
            this.drawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawingButton.Name = "drawingButton";
            this.drawingButton.Size = new System.Drawing.Size(23, 197);
            this.drawingButton.Text = "Drawing tools";
            this.drawingButton.Click += new System.EventHandler(this.drawingButton_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 200);
            // 
            // Actualizar
            // 
            this.Actualizar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("Actualizar.Image")));
            this.Actualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Actualizar.Name = "Actualizar";
            this.Actualizar.Size = new System.Drawing.Size(23, 197);
            this.Actualizar.Text = "Actualizar";
            this.Actualizar.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // Imprimir
            // 
            this.Imprimir.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("Imprimir.Image")));
            this.Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.Size = new System.Drawing.Size(23, 197);
            this.Imprimir.Text = "Imprimir";
            this.Imprimir.Click += new System.EventHandler(this.Imprimir_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(181, 6);
            // 
            // classDocumentationToolStripMenuItem
            // 
            this.classDocumentationToolStripMenuItem.Enabled = false;
            this.classDocumentationToolStripMenuItem.Name = "classDocumentationToolStripMenuItem";
            this.classDocumentationToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.classDocumentationToolStripMenuItem.Text = "Class documentation";
            this.classDocumentationToolStripMenuItem.Click += new System.EventHandler(this.classDocumentationToolStripMenuItem_Click);
            // 
            // statusImageList
            // 
            this.statusImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("statusImageList.ImageStream")));
            this.statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.statusImageList.Images.SetKeyName(0, "INFO.ICO");
            this.statusImageList.Images.SetKeyName(1, "");
            this.statusImageList.Images.SetKeyName(2, "");
            this.statusImageList.Images.SetKeyName(3, "");
            this.statusImageList.Images.SetKeyName(4, "");
            this.statusImageList.Images.SetKeyName(5, "");
            this.statusImageList.Images.SetKeyName(6, "");
            this.statusImageList.Images.SetKeyName(7, "");
            this.statusImageList.Images.SetKeyName(8, "");
            this.statusImageList.Images.SetKeyName(9, "");
            this.statusImageList.Images.SetKeyName(10, "");
            this.statusImageList.Images.SetKeyName(11, "");
            this.statusImageList.Images.SetKeyName(12, "");
            this.statusImageList.Images.SetKeyName(13, "");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            this.cambiarColorShape,
            this.cambiarIconoShape,
            this.cambiarNombreShape,
            this.editarShape,
            this.eliminarShape});
            this.contextMenuStrip1.Name = "contextMenuStripShapes";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 136);
            // 
            // cambiarColorShape
            // 
            this.cambiarColorShape.Data = this.diagramControl1;
            this.cambiarColorShape.Name = "cambiarColorShape";
            this.cambiarColorShape.Size = new System.Drawing.Size(164, 22);
            this.cambiarColorShape.Text = "Cambiar Color";
            this.cambiarColorShape.Click += new System.EventHandler(this.cambiarColorShape_Click);
            // 
            // cambiarIconoShape
            // 
            this.cambiarIconoShape.Data = this.diagramControl1;
            this.cambiarIconoShape.Name = "cambiarIconoShape";
            this.cambiarIconoShape.Size = new System.Drawing.Size(164, 22);
            this.cambiarIconoShape.Text = "Cambiar Icono";
            this.cambiarIconoShape.Click += new System.EventHandler(this.cambiarIconoShape_Click);
            // 
            // cambiarNombreShape
            // 
            this.cambiarNombreShape.Data = this.diagramControl1;
            this.cambiarNombreShape.Name = "cambiarNombreShape";
            this.cambiarNombreShape.Size = new System.Drawing.Size(164, 22);
            this.cambiarNombreShape.Text = "Cambiar Nombre";
            this.cambiarNombreShape.Click += new System.EventHandler(this.cambiarNombreShape_Click);
            // 
            // editarShape
            // 
            this.editarShape.Data = this.diagramControl1;
            this.editarShape.Name = "editarShape";
            this.editarShape.Size = new System.Drawing.Size(164, 22);
            this.editarShape.Text = "Editar";
            this.editarShape.Click += new System.EventHandler(this.editarShape_Click);
            // 
            // eliminarShape
            // 
            this.eliminarShape.Data = this.diagramControl1;
            this.eliminarShape.Name = "eliminarShape";
            this.eliminarShape.Size = new System.Drawing.Size(164, 22);
            this.eliminarShape.Text = "Eliminar";
            this.eliminarShape.Click += new System.EventHandler(this.eliminarShape_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] {
            this.cambiarNombreConnection,
            this.eliminarConnection});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(165, 48);
            // 
            // cambiarNombreConnection
            // 
            this.cambiarNombreConnection.Data = this.diagramControl1;
            this.cambiarNombreConnection.Name = "cambiarNombreConnection";
            this.cambiarNombreConnection.Size = new System.Drawing.Size(164, 22);
            this.cambiarNombreConnection.Text = "Cambiar Nombre";
            this.cambiarNombreConnection.Click += new System.EventHandler(this.cambiarNombreConnection_Click);
            // 
            // eliminarConnection
            // 
            this.eliminarConnection.Data = this.diagramControl1;
            this.eliminarConnection.Name = "eliminarConnection";
            this.eliminarConnection.Size = new System.Drawing.Size(164, 22);
            this.eliminarConnection.Text = "Eliminar";
            this.eliminarConnection.Click += new System.EventHandler(this.eliminarConnection_Click);
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton1.Text = "ToolStripButton1";
            this.ToolStripButton1.ToolTipText = "Buscar en Workflow";
            // 
            // ToolStripButton2
            // 
            this.ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton2.Image")));
            this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton2.Name = "ToolStripButton2";
            this.ToolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton2.Text = "ToolStripButton2";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.stripContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Size = new System.Drawing.Size(785, 613);
            this.stripContainer.ContentPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.PerformLayout();
            this.stripContainer.ResumeLayout(false);
            this.stripContainer.PerformLayout();
            this.contextMenuToolbars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).EndInit();
            this.tstToolbar.ResumeLayout(false);
            this.tstToolbar.PerformLayout();
            this.actionsStrip.ResumeLayout(false);
            this.actionsStrip.PerformLayout();
            this.drawingStrip.ResumeLayout(false);
            this.drawingStrip.PerformLayout();
            this.mainStrip.ResumeLayout(false);
            this.mainStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripContainer stripContainer;
       // private System.Windows.Forms.StatusStrip statusStrip;
        private ZToolBar mainStrip;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private ZToolBar drawingStrip;
        private System.Windows.Forms.ToolStripButton drawRectangleButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuToolbars;
        private System.Windows.Forms.ToolStripMenuItem standardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton drawEllipseButton;
        private System.Windows.Forms.ImageList shapesImageList;
        private System.Windows.Forms.ToolStripButton drawingButton;
        private System.Windows.Forms.ToolStripButton pointerButton;
        private ZToolBar actionsStrip;
        private System.Windows.Forms.ToolStripButton sendBackwardsButton;
        private System.Windows.Forms.ToolStripButton sendToBackButton;
        private System.Windows.Forms.ToolStripButton sendToFrontButton;
        private System.Windows.Forms.ToolStripButton sendForwardButton;
        private System.Windows.Forms.ToolStripButton groupButton;
        private System.Windows.Forms.ToolStripButton ungroupButton;
        private System.Windows.Forms.ToolStripButton propertiesButton;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem classDocumentationToolStripMenuItem;
        private ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.ImageList statusImageList;
        //private System.Windows.Forms.Timer statusTimer;
        private ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripButton textToolButton;
        private System.Windows.Forms.ToolStripButton Actualizar;
        private System.Windows.Forms.ToolStripButton Imprimir;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private ShapeToolStripMenuItem eliminarShape;
        private ShapeToolStripMenuItem cambiarNombreShape;
        private ShapeToolStripMenuItem editarShape;
        private ShapeToolStripMenuItem cambiarColorShape;
        private ShapeToolStripMenuItem cambiarIconoShape;
        private ShapeToolStripMenuItem eliminarConnection;
        private ShapeToolStripMenuItem cambiarNombreConnection;
        private DiagramShape oDiagramShape = null;
        private Zamba.WFShapes.Win.DiagramControl diagramControl1;
        private ZToolBar tstToolbar;
        private System.Windows.Forms.ToolStripButton tlpStep;
        private System.Windows.Forms.ToolStripButton tlpDistribute;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlpRefresh;
        private System.Windows.Forms.ToolStripButton tlpPrint;
        private System.Windows.Forms.ToolStripButton ToolStripButton1;
        private System.Windows.Forms.ToolStripButton ToolStripButton2;
    }
}