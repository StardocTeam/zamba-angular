using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.Enumerators;
using System.Collections.Generic;

public sealed class VirtualFormsBuilder
    : Form
{
    #region Atributos

    private CheckBox chkReadonly;
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
    private ListViewItem _draggedItem = null;
    #endregion

    #region Propiedades
    public Dictionary<Int64, String> DocTypes
    {
        set { UcControlList.DocTypes = value; }
    }
    #endregion

    #region Constructores

    public VirtualFormsBuilder()
    {
        InitializeComponent();
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.chkReadonly = new System.Windows.Forms.CheckBox();
        this.UcControlList = new Stardoc.HtmlEditor.HtmlControlsList();
        this.htmlEditorControl = new Stardoc.HtmlEditor.HtmlEditorControl();
        this.mnsMainMenu = new System.Windows.Forms.MenuStrip();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.abrirHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.guardarHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
        this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.asdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.colorDeFondoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.colorPrimerPlanoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.verHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.barraDeHerramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
        this.buscarYRemplazarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.insertarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.textoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.tablaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
        this.mnsMainMenu.SuspendLayout();
        this.SuspendLayout();
        // 
        // chkReadonly
        // 
        this.chkReadonly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.chkReadonly.Location = new System.Drawing.Point(12, 530);
        this.chkReadonly.Name = "chkReadonly";
        this.chkReadonly.Size = new System.Drawing.Size(104, 24);
        this.chkReadonly.TabIndex = 9;
        this.chkReadonly.Text = "Solo Lectura";
        this.chkReadonly.CheckedChanged += new System.EventHandler(this.chkReadonly_CheckedChanged);
        // 
        // UcControlList
        // 
        this.UcControlList.AllowDrop = true;
        this.UcControlList.Location = new System.Drawing.Point(682, 32);
        this.UcControlList.Name = "UcControlList";
        this.UcControlList.Size = new System.Drawing.Size(309, 435);
        this.UcControlList.TabIndex = 29;
        this.UcControlList.OnSelectedHtmlControl += new Stardoc.HtmlEditor.HtmlControlsList.SelectedHtmlControl(this.UcControlList_OnSelectedHtmlControl);
        this.UcControlList.OnSelectedDocTypeChanged += new Stardoc.HtmlEditor.HtmlControlsList.SelectedDocTypeChanged(this.UcControlList_OnSelectedDocTypeChanged);
        // 
        // htmlEditorControl
        // 
        this.htmlEditorControl.AllowDrop = true;
        this.htmlEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.htmlEditorControl.InnerText = null;
        this.htmlEditorControl.Location = new System.Drawing.Point(12, 32);
        this.htmlEditorControl.Name = "htmlEditorControl";
        this.htmlEditorControl.Size = new System.Drawing.Size(648, 496);
        this.htmlEditorControl.TabIndex = 26;
        this.htmlEditorControl.HtmlNavigation += new Stardoc.HtmlEditor.HtmlNavigationEventHandler(this.htmlEditorControl_HtmlNavigation);
        this.htmlEditorControl.InsertedHtmlControl += new Stardoc.HtmlEditor.HtmlEditorControl.OnInsertedHtmlControl(this.htmlEditorControl_InsertedHtmlControl);
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
        this.mnsMainMenu.Size = new System.Drawing.Size(1016, 24);
        this.mnsMainMenu.TabIndex = 30;
        this.mnsMainMenu.Text = "menuStrip1";
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirHTMLToolStripMenuItem,
            this.guardarHTMLToolStripMenuItem,
            this.toolStripMenuItem2,
            this.imprimirToolStripMenuItem,
            this.toolStripMenuItem3,
            this.cerrarToolStripMenuItem});
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
        this.toolStripMenuItem1.Text = "Archivo";
        // 
        // abrirHTMLToolStripMenuItem
        // 
        this.abrirHTMLToolStripMenuItem.Name = "abrirHTMLToolStripMenuItem";
        this.abrirHTMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.abrirHTMLToolStripMenuItem.Text = "Abrir HTML";
        this.abrirHTMLToolStripMenuItem.Click += new System.EventHandler(this.abrirHTMLToolStripMenuItem_Click);
        // 
        // guardarHTMLToolStripMenuItem
        // 
        this.guardarHTMLToolStripMenuItem.Name = "guardarHTMLToolStripMenuItem";
        this.guardarHTMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.guardarHTMLToolStripMenuItem.Text = "Guardar HTML";
        this.guardarHTMLToolStripMenuItem.Click += new System.EventHandler(this.guardarHTMLToolStripMenuItem_Click);
        // 
        // toolStripMenuItem2
        // 
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
        // 
        // cerrarToolStripMenuItem
        // 
        this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
        this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.cerrarToolStripMenuItem.Text = "Cerrar";
        this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
        // 
        // asdToolStripMenuItem
        // 
        this.asdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorDeFondoToolStripMenuItem,
            this.colorPrimerPlanoToolStripMenuItem});
        this.asdToolStripMenuItem.Name = "asdToolStripMenuItem";
        this.asdToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
        this.asdToolStripMenuItem.Text = "Estilo";
        // 
        // colorDeFondoToolStripMenuItem
        // 
        this.colorDeFondoToolStripMenuItem.Name = "colorDeFondoToolStripMenuItem";
        this.colorDeFondoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
        this.colorDeFondoToolStripMenuItem.Text = "Color de fondo";
        this.colorDeFondoToolStripMenuItem.Click += new System.EventHandler(this.colorDeFondoToolStripMenuItem_Click);
        // 
        // colorPrimerPlanoToolStripMenuItem
        // 
        this.colorPrimerPlanoToolStripMenuItem.Name = "colorPrimerPlanoToolStripMenuItem";
        this.colorPrimerPlanoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
        this.colorPrimerPlanoToolStripMenuItem.Text = "Color primer plano";
        this.colorPrimerPlanoToolStripMenuItem.Click += new System.EventHandler(this.colorPrimerPlanoToolStripMenuItem_Click);
        // 
        // verToolStripMenuItem
        // 
        this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verHTMLToolStripMenuItem,
            this.barraDeHerramientasToolStripMenuItem,
            this.toolStripMenuItem5,
            this.buscarYRemplazarToolStripMenuItem});
        this.verToolStripMenuItem.Name = "verToolStripMenuItem";
        this.verToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
        this.verToolStripMenuItem.Text = "Ver ";
        // 
        // verHTMLToolStripMenuItem
        // 
        this.verHTMLToolStripMenuItem.Name = "verHTMLToolStripMenuItem";
        this.verHTMLToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        this.verHTMLToolStripMenuItem.Text = "Ver HTML";
        this.verHTMLToolStripMenuItem.Click += new System.EventHandler(this.verHTMLToolStripMenuItem_Click);
        // 
        // barraDeHerramientasToolStripMenuItem
        // 
        this.barraDeHerramientasToolStripMenuItem.Checked = true;
        this.barraDeHerramientasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
        this.barraDeHerramientasToolStripMenuItem.Name = "barraDeHerramientasToolStripMenuItem";
        this.barraDeHerramientasToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        this.barraDeHerramientasToolStripMenuItem.Text = "Barra de herramientas";
        this.barraDeHerramientasToolStripMenuItem.Click += new System.EventHandler(this.barraDeHerramientasToolStripMenuItem_Click);
        // 
        // toolStripMenuItem5
        // 
        this.toolStripMenuItem5.Name = "toolStripMenuItem5";
        this.toolStripMenuItem5.Size = new System.Drawing.Size(184, 6);
        // 
        // buscarYRemplazarToolStripMenuItem
        // 
        this.buscarYRemplazarToolStripMenuItem.Name = "buscarYRemplazarToolStripMenuItem";
        this.buscarYRemplazarToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
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
        this.insertarToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
        this.insertarToolStripMenuItem.Text = "Insertar";
        // 
        // textoToolStripMenuItem
        // 
        this.textoToolStripMenuItem.Name = "textoToolStripMenuItem";
        this.textoToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
        this.textoToolStripMenuItem.Text = "Texto";
        this.textoToolStripMenuItem.Click += new System.EventHandler(this.textoToolStripMenuItem_Click);
        // 
        // hTMLToolStripMenuItem
        // 
        this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
        this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
        this.hTMLToolStripMenuItem.Text = "HTML";
        this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
        // 
        // tablaToolStripMenuItem
        // 
        this.tablaToolStripMenuItem.Name = "tablaToolStripMenuItem";
        this.tablaToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
        this.tablaToolStripMenuItem.Text = "Tabla";
        this.tablaToolStripMenuItem.Click += new System.EventHandler(this.tablaToolStripMenuItem_Click);
        // 
        // imprimirToolStripMenuItem
        // 
        this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
        this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.imprimirToolStripMenuItem.Text = "Imprimir";
        this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
        // 
        // toolStripMenuItem3
        // 
        this.toolStripMenuItem3.Name = "toolStripMenuItem3";
        this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
        // 
        // VirtualFormsBuilder
        // 
        this.AllowDrop = true;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.ClientSize = new System.Drawing.Size(1016, 566);
        this.Controls.Add(this.UcControlList);
        this.Controls.Add(this.htmlEditorControl);
        this.Controls.Add(this.chkReadonly);
        this.Controls.Add(this.mnsMainMenu);
        this.MainMenuStrip = this.mnsMainMenu;
        this.Name = "VirtualFormsBuilder";
        this.Text = "Editor de HMTL";
        this.Load += new System.EventHandler(this.EditorTestForm_Load);
        this.mnsMainMenu.ResumeLayout(false);
        this.mnsMainMenu.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    void UcControlList_OnSelectedDocTypeChanged(long? docTypeId)
    {
        OnSelectedDocTypeChanged(docTypeId);
    }

    private void UcControlList_OnSelectedHtmlControl(IHtmlWritable selectedItem)
    {
        htmlEditorControl.ItemToInsert = selectedItem;
    }
    #endregion
    #endregion

    public void LoadHtmlElements(List<IHtmlElement> elements)
    {
        UcControlList.LoadHtmlElements(elements);
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
        
    private void ToogleReadOnly()
    {
        this.htmlEditorControl.ReadOnly = this.chkReadonly.Checked;
        this.htmlEditorControl.Focus();
    }

    private void ToogleOverWrite()
    {
        this.htmlEditorControl.ToggleOverWrite();
        this.htmlEditorControl.Focus();
    }

    private void SaveHtml()
    {
        this.htmlEditorControl.SaveFilePrompt();
        this.htmlEditorControl.Focus();
    }

    private void OpenHtml()
    {
        this.htmlEditorControl.OpenFilePrompt();
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

    #region

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
        SaveHtml();
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

    private void chkReadonly_CheckedChanged(object sender, EventArgs e)
    {
        ToogleReadOnly();
    }

    public event SelectedDocTypeChanged OnSelectedDocTypeChanged;
    public delegate void SelectedDocTypeChanged(Int64? docTypeId);

    #endregion

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

    ///// <summary>
    ///// The main entry point for the application.
    ///// </summary>
    //[STAThread]
    //static void Main()
    //{
    //    Application.EnableVisualStyles();
    //    Application.DoEvents();
    //    Application.Run(new VirtualFormsBuilder());
    //}

    /// <summary>
    /// Obtains the text resource from an embedded file 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private string GetResourceText(string filename)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resource = string.Empty;

        // resources are named using a fully qualified name
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
            // Only these controls have a FlatStyle property
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

}