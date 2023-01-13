using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.Enumerators;
using System.Collections.Generic;
using Stardoc.HtmlEditor.HtmlControls;

public sealed class VirtualFormsBuilder
    : Form
{
    #region Atributos

    private Container components = null;
    private HtmlEditorControl htmlEditorControl;
    private HtmlControlsList UcControlList;
    private String workingDirectory = string.Empty;
    private MenuStrip mnsMainMenu;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem abrirHTMLToolStripMenuItem;
    private ToolStripMenuItem guardarHTMLToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem cerrarToolStripMenuItem;
    private ToolStripMenuItem asdToolStripMenuItem;
    private ToolStripMenuItem colorDeFondoToolStripMenuItem;
    private ToolStripMenuItem colorPrimerPlanoToolStripMenuItem;
    private ToolStripMenuItem verToolStripMenuItem;
    private ToolStripMenuItem verHTMLToolStripMenuItem;
    private ToolStripMenuItem barraDeHerramientasToolStripMenuItem;
    private ToolStripMenuItem insertarToolStripMenuItem;
    private ToolStripMenuItem textoToolStripMenuItem;
    private ToolStripMenuItem hTMLToolStripMenuItem;
    private ToolStripMenuItem tablaToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem5;
    private ToolStripMenuItem buscarYRemplazarToolStripMenuItem;
    private ToolStripMenuItem imprimirToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem3;
    private SplitContainer splitContainer1;
    private ToolStripMenuItem editarHTMLToolStripMenuItem;
    private ToolStripMenuItem nuevoToolStripMenuItem;
    private ToolStripMenuItem guardarComoToolStripMenuItem;
    //private ListViewItem _draggedItem = null;

    /// <summary>
    /// Los atributos de la entidad especificado pasados _frmRules controles HTML
    /// </summary>
    private List<BaseHtmlElement> _indexes = null;


    #endregion

    #region Propiedades
    /// <summary>
    /// El nombre de la entidad
    /// </summary>
    public String DocTypeName
    {
        set { UcControlList.DocTypeName = value; }
    }
    /// <summary>
    /// Los atributos de la entidad especificado pasados _frmRules controles HTML
    /// </summary>
    private List<BaseHtmlElement> Indexes
    {
        get { return _indexes; }
        set { _indexes = value; }
    }

    public String FilePath()
    {
        return htmlEditorControl.FilePath;
    }


    public Dictionary<Int64, String> Workflows
    {
        set { htmlEditorControl.Workflows = value; }
    }
    public Dictionary<Int64, String> Steps
    {
        set { htmlEditorControl.Steps = value; }
    }
    public Dictionary<Int64, String> Rules
    {
        set { htmlEditorControl.Rules = value; }
    } 

    #endregion

    #region Constructores

    public VirtualFormsBuilder()
    {
        InitializeComponent();
    }
    public VirtualFormsBuilder(String fileName)
        : this()
    {
        OpenFile(fileName);
    }


    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.mnsMainMenu = new System.Windows.Forms.MenuStrip();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.abrirHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.guardarHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
        this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
        this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.asdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.colorDeFondoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.colorPrimerPlanoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.verHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editarHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.barraDeHerramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
        this.buscarYRemplazarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.insertarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.textoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.tablaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.htmlEditorControl = new Stardoc.HtmlEditor.HtmlEditorControl();
        this.UcControlList = new Stardoc.HtmlEditor.HtmlControlsList();
        this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.mnsMainMenu.SuspendLayout();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.SuspendLayout();
        // 
        // mnsMainMenu
        // 
        this.mnsMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.asdToolStripMenuItem,
            this.verToolStripMenuItem,
            this.insertarToolStripMenuItem});
        this.mnsMainMenu.Location = new System.Drawing.Point(0, 0);
        this.mnsMainMenu.Name = "mnsMainMenu";
        this.mnsMainMenu.Size = new System.Drawing.Size(1022, 24);
        this.mnsMainMenu.TabIndex = 30;
        this.mnsMainMenu.Text = "menuStrip1";
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirHTMLToolStripMenuItem,
            this.guardarHTMLToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.imprimirToolStripMenuItem,
            this.toolStripMenuItem3,
            this.cerrarToolStripMenuItem});
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
        this.toolStripMenuItem1.Text = "Archivo";
        // 
        // nuevoToolStripMenuItem
        // 
        this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
        this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.nuevoToolStripMenuItem.Text = "Nuevo";
        this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
        // 
        // abrirHTMLToolStripMenuItem
        // 
        this.abrirHTMLToolStripMenuItem.Name = "abrirHTMLToolStripMenuItem";
        this.abrirHTMLToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.abrirHTMLToolStripMenuItem.Text = "Abrir";
        this.abrirHTMLToolStripMenuItem.Click += new System.EventHandler(this.abrirHTMLToolStripMenuItem_Click);
        // 
        // guardarHTMLToolStripMenuItem
        // 
        this.guardarHTMLToolStripMenuItem.Name = "guardarHTMLToolStripMenuItem";
        this.guardarHTMLToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.guardarHTMLToolStripMenuItem.Text = "Guardar";
        this.guardarHTMLToolStripMenuItem.Click += new System.EventHandler(this.guardarHTMLToolStripMenuItem_Click);
        // 
        // toolStripMenuItem2
        // 
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new System.Drawing.Size(151, 6);
        // 
        // imprimirToolStripMenuItem
        // 
        this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
        this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.imprimirToolStripMenuItem.Text = "Imprimir";
        this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
        // 
        // toolStripMenuItem3
        // 
        this.toolStripMenuItem3.Name = "toolStripMenuItem3";
        this.toolStripMenuItem3.Size = new System.Drawing.Size(151, 6);
        // 
        // cerrarToolStripMenuItem
        // 
        this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
        this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.cerrarToolStripMenuItem.Text = "Cerrar";
        this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
        // 
        // asdToolStripMenuItem
        // 
        this.asdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorDeFondoToolStripMenuItem,
            this.colorPrimerPlanoToolStripMenuItem});
        this.asdToolStripMenuItem.Name = "asdToolStripMenuItem";
        this.asdToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
        this.asdToolStripMenuItem.Text = "Estilo";
        // 
        // colorDeFondoToolStripMenuItem
        // 
        this.colorDeFondoToolStripMenuItem.Name = "colorDeFondoToolStripMenuItem";
        this.colorDeFondoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
        this.colorDeFondoToolStripMenuItem.Text = "Color de fondo";
        this.colorDeFondoToolStripMenuItem.Click += new System.EventHandler(this.colorDeFondoToolStripMenuItem_Click);
        // 
        // colorPrimerPlanoToolStripMenuItem
        // 
        this.colorPrimerPlanoToolStripMenuItem.Name = "colorPrimerPlanoToolStripMenuItem";
        this.colorPrimerPlanoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
        this.colorPrimerPlanoToolStripMenuItem.Text = "Color primer plano";
        this.colorPrimerPlanoToolStripMenuItem.Click += new System.EventHandler(this.colorPrimerPlanoToolStripMenuItem_Click);
        // 
        // verToolStripMenuItem
        // 
        this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verHTMLToolStripMenuItem,
            this.editarHTMLToolStripMenuItem,
            this.barraDeHerramientasToolStripMenuItem,
            this.toolStripMenuItem5,
            this.buscarYRemplazarToolStripMenuItem});
        this.verToolStripMenuItem.Name = "verToolStripMenuItem";
        this.verToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
        this.verToolStripMenuItem.Text = "Ver ";
        // 
        // verHTMLToolStripMenuItem
        // 
        this.verHTMLToolStripMenuItem.Name = "verHTMLToolStripMenuItem";
        this.verHTMLToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
        this.verHTMLToolStripMenuItem.Text = "Ver HTML";
        this.verHTMLToolStripMenuItem.Click += new System.EventHandler(this.verHTMLToolStripMenuItem_Click);
        // 
        // editarHTMLToolStripMenuItem
        // 
        this.editarHTMLToolStripMenuItem.Name = "editarHTMLToolStripMenuItem";
        this.editarHTMLToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
        this.editarHTMLToolStripMenuItem.Text = "Editar HTML";
        this.editarHTMLToolStripMenuItem.Click += new System.EventHandler(this.editarHTMLToolStripMenuItem_Click);
        // 
        // barraDeHerramientasToolStripMenuItem
        // 
        this.barraDeHerramientasToolStripMenuItem.Checked = true;
        this.barraDeHerramientasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
        this.barraDeHerramientasToolStripMenuItem.Name = "barraDeHerramientasToolStripMenuItem";
        this.barraDeHerramientasToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
        this.barraDeHerramientasToolStripMenuItem.Text = "Barra de herramientas";
        this.barraDeHerramientasToolStripMenuItem.Click += new System.EventHandler(this.barraDeHerramientasToolStripMenuItem_Click);
        // 
        // toolStripMenuItem5
        // 
        this.toolStripMenuItem5.Name = "toolStripMenuItem5";
        this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 6);
        // 
        // buscarYRemplazarToolStripMenuItem
        // 
        this.buscarYRemplazarToolStripMenuItem.Name = "buscarYRemplazarToolStripMenuItem";
        this.buscarYRemplazarToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
        this.buscarYRemplazarToolStripMenuItem.Text = "Buscar y remplazar";
        this.buscarYRemplazarToolStripMenuItem.Click += new System.EventHandler(this.buscarYRemplazarToolStripMenuItem_Click);
        // 
        // insertarToolStripMenuItem
        // 
        this.insertarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textoToolStripMenuItem,
            this.hTMLToolStripMenuItem,
            this.tablaToolStripMenuItem});
        this.insertarToolStripMenuItem.Name = "insertarToolStripMenuItem";
        this.insertarToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
        this.insertarToolStripMenuItem.Text = "Insertar";
        // 
        // textoToolStripMenuItem
        // 
        this.textoToolStripMenuItem.Name = "textoToolStripMenuItem";
        this.textoToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
        this.textoToolStripMenuItem.Text = "Texto";
        this.textoToolStripMenuItem.Click += new System.EventHandler(this.textoToolStripMenuItem_Click);
        // 
        // hTMLToolStripMenuItem
        // 
        this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
        this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
        this.hTMLToolStripMenuItem.Text = "HTML";
        this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
        // 
        // tablaToolStripMenuItem
        // 
        this.tablaToolStripMenuItem.Name = "tablaToolStripMenuItem";
        this.tablaToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
        this.tablaToolStripMenuItem.Text = "Tabla";
        this.tablaToolStripMenuItem.Click += new System.EventHandler(this.tablaToolStripMenuItem_Click);
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(0, 24);
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.htmlEditorControl);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.UcControlList);
        this.splitContainer1.Size = new System.Drawing.Size(1022, 542);
        this.splitContainer1.SplitterDistance = 740;
        this.splitContainer1.TabIndex = 31;
        // 
        // htmlEditorControl
        // 
        this.htmlEditorControl.AllowDrop = true;
        this.htmlEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.htmlEditorControl.InnerText = null;
        this.htmlEditorControl.Location = new System.Drawing.Point(0, 0);
        this.htmlEditorControl.Name = "htmlEditorControl";
        this.htmlEditorControl.Size = new System.Drawing.Size(740, 542);
        this.htmlEditorControl.TabIndex = 26;
        this.htmlEditorControl.HtmlNavigation += new Stardoc.HtmlEditor.HtmlNavigationEventHandler(this.htmlEditorControl_HtmlNavigation);
        this.htmlEditorControl.OnSavedDocument += new Stardoc.HtmlEditor.HtmlEditorControl.SavedDocument(this.htmlEditorControl_OnSavedDocument);
        this.htmlEditorControl.InsertedHtmlControl += new Stardoc.HtmlEditor.HtmlEditorControl.OnInsertedHtmlControl(this.htmlEditorControl_InsertedHtmlControl);
        this.htmlEditorControl.OnLoadWorkflows += new HtmlEditorControl.LoadWorkflows(htmlEditorControl_OnLoadWorkflows);
        this.htmlEditorControl.OnSelectedStep += new HtmlEditorControl.SelectedStep(htmlEditorControl_OnSelectedStep);
        this.htmlEditorControl.OnSelectedWorkflowChanged += new HtmlEditorControl.SelectedWorkflow(htmlEditorControl_OnSelectedWorkflowChanged);
        // 
        // UcControlList
        // 
        this.UcControlList.AllowDrop = true;
        this.UcControlList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.UcControlList.DocTypeName = null;
        this.UcControlList.Location = new System.Drawing.Point(0, 0);
        this.UcControlList.Name = "UcControlList";
        this.UcControlList.Size = new System.Drawing.Size(278, 542);
        this.UcControlList.TabIndex = 29;
        this.UcControlList.OnSelectedHtmlControl += new Stardoc.HtmlEditor.HtmlControlsList.SelectedHtmlControl(this.UcControlList_OnSelectedHtmlControl);
        this.UcControlList.Load += new System.EventHandler(this.UcControlList_Load);
        this.UcControlList.OnReloadIndexes += new HtmlControlsList.ReloadIndexes(UcControlList_OnReloadIndexes);
        // 
        // guardarComoToolStripMenuItem
        // 
        this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
        this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
        this.guardarComoToolStripMenuItem.Text = "Guardar Como";
        this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
        // 
        // VirtualFormsBuilder
        // 
        this.AllowDrop = true;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.ClientSize = new System.Drawing.Size(1022, 566);
        this.Controls.Add(this.splitContainer1);
        this.Controls.Add(this.mnsMainMenu);
        this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        this.MainMenuStrip = this.mnsMainMenu;
        this.Name = "VirtualFormsBuilder";
        this.Text = "Editor de documentos HMTL";
        this.Load += new System.EventHandler(this.EditorTestForm_Load);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VirtualFormsBuilder_FormClosing);
        this.mnsMainMenu.ResumeLayout(false);
        this.mnsMainMenu.PerformLayout();
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    public event LoadWorkflows OnLoadWorkflows;
    public event SelectedWorkflow OnSelectedWorkflowChanged;
    public event SelectedStep OnSelectedStep;
    public delegate void LoadWorkflows();
    public delegate void SelectedWorkflow(Int64 workflowId);
    public delegate void SelectedStep(Int64 stepId);

    private void htmlEditorControl_OnSelectedWorkflowChanged(long workflowId)
    {
        OnSelectedWorkflowChanged(workflowId);
    }

    private void htmlEditorControl_OnSelectedStep(long stepId)
    {
        OnSelectedStep(stepId);
    }

    private void htmlEditorControl_OnLoadWorkflows()
    {
        OnLoadWorkflows();
    }

    private void htmlEditorControl_OnSavedDocument(string filePath)
    {
        if (null != OnSavedDocument)
            OnSavedDocument(filePath);
    }

    private void UcControlList_OnSelectedHtmlControl(IHtmlWritable selectedItem)
    {
        htmlEditorControl.ItemToInsert = selectedItem;
    }
    #endregion
    #endregion

    #region Eventos

    private void EditorTestForm_Load(object sender, EventArgs e)
    {
        SetFlatStyleSystem(this);
    }

    private void htmlEditorControl_HtmlException(object sender, HtmlExceptionEventArgs args)
    {
        // obtain the message and operation
        // concatenate the message with any inner message
        string operation = args.Operation;
        Exception ex = args.ExceptionObject;
        string message = ex.Message;
        if (ex.InnerException != null)
        {
            if (ex.InnerException.Message != null)
            {
                message = string.Format("{0}\n{1}", message, ex.InnerException.Message);
            }
        }
        // define the title for the internal message box
        string title;
        if (operation == null || operation == string.Empty)
        {
            title = "Unknown Error";
        }
        else
        {
            title = operation + " Error";
        }
        // display the error message box
        MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void htmlEditorControl_HtmlNavigation(object sender, HtmlNavigationEventArgs e)
    {
        e.Cancel = false;
    }

    private void tbtable_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddTable();
        htmlEditorControl.Focus();
    }

    private void htmlEditorControl_InsertedHtmlControl()
    {
        UcControlList.DeleteSelectedControl();
    }
    private void barraDeHerramientasToolStripMenuItem_Click(object sender, EventArgs e)
    {
        barraDeHerramientasToolStripMenuItem.Checked = !barraDeHerramientasToolStripMenuItem.Checked;
        ToogleToolbar();
    }

    private void verHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ViewHtml();
    }

    private void colorDeFondoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SetBackgroundColor();
    }

    private void colorPrimerPlanoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SetForegroundColor();
    }

    private void abrirHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenHtml();
    }

    private void guardarHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.SaveFile();
        this.htmlEditorControl.Focus();
    }

    private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        Close();
    }

    private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        InsertHtml();
    }

    private void textoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        InsertText();
    }

    public delegate void SavedDocument(String filePath);
    public event SavedDocument OnSavedDocument;
    #endregion

    /// <summary>
    /// Carga los elementos en el listado de controles del editor HTML.  
    /// </summary>
    /// <param name="elements"></param>
    public void LoadHtmlElements(List<BaseHtmlElement> elements)
    {
        Indexes = elements;
        List<BaseHtmlElement> NotInsertedElements = new List<BaseHtmlElement>(elements.Count);

        String ParsedId = string.Empty;
        Boolean ControlExistsInEditor = false;
        foreach (BaseHtmlElement CurrentElement in elements)
        {
            switch (CurrentElement.Type())
            {
                case HtmlControlType.CheckBox:
                    if (CurrentElement is ZCheckBoxItem)
                        ParsedId = HtmlParser.GetParsedId(((ZCheckBoxItem)CurrentElement).IndexId.ToString().ToString());
                    else
                        ParsedId = HtmlParser.GetParsedId(CurrentElement.Id);
                    break;
                case HtmlControlType.Select:
                    if (CurrentElement is ZSelectItem)
                        ParsedId = HtmlParser.GetParsedId(((ZSelectItem)CurrentElement).IndexId.ToString());
                    else
                        ParsedId = HtmlParser.GetParsedId(CurrentElement.Id);
                    break;
                case HtmlControlType.TextArea:
                    if (CurrentElement is ZTextAreaItem)
                        ParsedId = HtmlParser.GetParsedId(((ZTextAreaItem)CurrentElement).IndexId.ToString()) + "_LBL";
                    else
                        ParsedId = HtmlParser.GetParsedId(CurrentElement.Id) + "_LBL";
                    break;
                case HtmlControlType.RadioButton:
                    if (CurrentElement is ZRadioButtonItem)
                        ParsedId = HtmlParser.GetParsedId(((ZRadioButtonItem)CurrentElement).IndexId.ToString());
                    else
                        ParsedId = HtmlParser.GetParsedId(CurrentElement.Id);
                    break;
                case HtmlControlType.TextBox:
                    if (CurrentElement is ZTextBoxItem)
                        ParsedId = HtmlParser.GetParsedId(((ZTextBoxItem)CurrentElement).IndexId.ToString());
                    else
                        ParsedId = HtmlParser.GetParsedId(CurrentElement.Id);
                    break;
                case HtmlControlType.Table:
                    //Las tablas se usan hoy por hoy para los documentos asociados solamente.
                    ParsedId = CurrentElement.Id;
                    break;
                case HtmlControlType.Button:
                    ParsedId = CurrentElement.Id;
                    break;
                default:
                    ParsedId = string.Empty;
                    break;
            }

            ControlExistsInEditor = htmlEditorControl.ExistsElementId(ParsedId);

            if (!ControlExistsInEditor)
                NotInsertedElements.Add(CurrentElement);
        }

        UcControlList.LoadHtmlElements(NotInsertedElements);
    }

    private void ToogleToolbar()
    {
        this.htmlEditorControl.ToolbarVisible = !this.htmlEditorControl.ToolbarVisible;
        this.htmlEditorControl.Focus();
    }

    private void SetBackgroundColor()
    {
        using (ColorDialog dialog = new ColorDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color = dialog.Color;
                this.htmlEditorControl.BodyBackColor = color;
            }
        }
        this.htmlEditorControl.Focus();
    }

    private void SetForegroundColor()
    {
        using (ColorDialog dialog = new ColorDialog())
        {

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color = dialog.Color;
                this.htmlEditorControl.BodyForeColor = color;
            }
        }
        this.htmlEditorControl.Focus();
    }

    private void EditHtml()
    {
        this.htmlEditorControl.HtmlContentsEdit();
        this.htmlEditorControl.Focus();
    }

    private void ViewHtml()
    {
        this.htmlEditorControl.HtmlContentsView();
        this.htmlEditorControl.Focus();
    }

    private void ToogleOverWrite()
    {
        this.htmlEditorControl.ToggleOverWrite();
        this.htmlEditorControl.Focus();
    }

    private void OpenHtml()
    {
        this.htmlEditorControl.OpenFilePrompt();
        this.htmlEditorControl.Focus();
    }

    public void NewHtml()
    {
        this.htmlEditorControl.CreateNewFile();
        this.htmlEditorControl.Focus();
    }

    private void InsertHtml()
    {
        this.htmlEditorControl.InsertHtmlPrompt();
        this.htmlEditorControl.Focus();
    }

    private void InsertText()
    {
        this.htmlEditorControl.InsertTextPrompt();
        this.htmlEditorControl.Focus();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }


    /// <summary>
    /// Obtains the text resource from an embedded file 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private string GetResourceText(string filename)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resource = string.Empty;

        // resources are named using _frmRules fully qualified name
        string streamName = this.GetType().Namespace + @"." + filename;
        using (Stream stream = assembly.GetManifestResourceStream(streamName))
        {
            // read the contents of the embedded file
            using (StreamReader reader = new StreamReader(stream))
            {
                resource = reader.ReadToEnd(); ;
            }
        }

        return resource;
    }
    /// <summary>
    /// set the flat style of the dialog based on the user setting
    /// </summary>
    /// <param name="parent"></param>
    private void SetFlatStyleSystem(Control parent)
    {
        // iterate through all controls setting the flat style
        foreach (Control control in parent.Controls)
        {
            // Only these controls have _frmRules FlatStyle property
            ButtonBase button = control as ButtonBase;
            GroupBox group = control as GroupBox;
            Label label = control as Label;
            TextBox textBox = control as TextBox;
            if (button != null) button.FlatStyle = FlatStyle.System;
            else if (group != null) group.FlatStyle = FlatStyle.System;
            else if (label != null) label.FlatStyle = FlatStyle.System;

            // Set contained controls FlatStyle, too
            SetFlatStyleSystem(control);
        }

    }

    private void tablaToolStripMenuItem_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddTable();
    }

    private void buscarYRemplazarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        htmlEditorControl.FindAndReplace();
    }

    private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
    {
        htmlEditorControl.DocumentPrint();
    }

    /// <summary>
    /// Abre el archivo en el editor
    /// </summary>
    /// <param name="filePath"></param>
    public void OpenFile(string filePath)
    {
        htmlEditorControl.OpenFile(filePath);
    }

    private void UcControlList_Load(object sender, EventArgs e)
    {

    }

    private void editarHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        EditHtml();
    }

    private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        NewHtml();
    }

    private void VirtualFormsBuilder_FormClosing(object sender, FormClosingEventArgs e)
    {
        try
        {
            htmlEditorControl.OnParentClose();
        }
        catch (Exception)
        { }
    }

    private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        htmlEditorControl.SaveFileAs();
        htmlEditorControl.Focus();
    }

    private void UcControlList_OnReloadIndexes()
    {
        LoadHtmlElements(Indexes);
    }
}