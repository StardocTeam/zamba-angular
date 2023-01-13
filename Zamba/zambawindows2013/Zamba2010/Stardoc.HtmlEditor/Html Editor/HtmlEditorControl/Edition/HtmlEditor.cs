using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Stardoc.HtmlEditor.Enumerators;
/// <summary>
/// Form usado para editar o mirar contenido en HTML.
/// El contenido HTML ingresado no es parseado , se ve tal cual se escribe.
/// </summary>
internal class HtmlMainEditor
    : Form
{
    private HtmlFormState _state;
    public HtmlFormState State
    {
        get { return _state; }
        set
        {
            _state = value;
        }
    }
    #region Atributos
    private TextBox tbHtml;
    private Button btAceptar;
    private Button btCancelar;
    private Container components = null;

    /// <summary>
    /// Establece si el contenido es solo lectura
    /// </summary>
    private bool _readOnly;
    #endregion

    #region Constantes
    /// <summary>
    /// Representa el texto del boton btCancelar si el Form esta en estado Editar
    /// </summary>
    private const string editCommand = "Cancelar";
    /// <summary>
    /// Representa el texto del boton btCancelar si el Form esta en estado Mirar
    /// </summary>
    private const string viewCommand = "Cerrar";
    private const String TITLE_STATE_VIEW = "Resumen elemento HTML";
    private const String TITLE_STATE_UPDATE = "Edición elemento HTML";
    private const String TITLE_STATE_INSERT = "Insertar elemento HTML";
    #endregion

    #region Constructores
    /// <summary>
    /// Constructor basico
    /// </summary>
    public HtmlMainEditor()
    {
        InitializeComponent();

        this.tbHtml.Text = string.Empty;
        this.IsReadOnly = true;

    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Establece el Titulo del Form
    /// </summary>
    /// <param name="caption"></param>
    public void SetCaption(string caption)
    {
        this.Text = caption;
    }

    /// <summary>
    /// Establece u obtiene el Html que contiene el Form
    /// </summary>
    public string HTML
    {
        get
        {
            return this.tbHtml.Text.Trim();
        }
        set
        {
            this.tbHtml.Text = (value != null) ? value.Trim() : string.Empty;
            this.tbHtml.SelectionStart = 0;
            this.tbHtml.SelectionLength = 0;
        }

    }

    /// <summary>
    /// Establece u obtiene si el contenido es solo lectura
    /// </summary>
    public bool IsReadOnly
    {
        get
        {
            return _readOnly;
        }
        set
        {
            _readOnly = value;
            this.btAceptar.Visible = !_readOnly;
            this.tbHtml.ReadOnly = _readOnly;
            this.btCancelar.Text = _readOnly ? viewCommand : editCommand;
        }
    }
    #endregion

    #region Eventos
    private void bOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
    #endregion

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

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(HtmlMainEditor));
        this.tbHtml = new TextBox();
        this.btAceptar = new Button();
        this.btCancelar = new Button();
        this.SuspendLayout();
        // 
        // htmlText
        // 
        this.tbHtml.AcceptsReturn = true;
        this.tbHtml.AcceptsTab = true;
        this.tbHtml.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right)));
        this.tbHtml.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        this.tbHtml.Location = new Point(8, 8);
        this.tbHtml.Multiline = true;
        this.tbHtml.Name = "htmlText";
        this.tbHtml.ScrollBars = ScrollBars.Vertical;
        this.tbHtml.Size = new Size(576, 320);
        this.tbHtml.TabIndex = 0;
        // 
        // bOK
        // 
        this.btAceptar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        this.btAceptar.Location = new Point(416, 336);
        this.btAceptar.Name = "bOK";
        this.btAceptar.Size = new Size(75, 23);
        this.btAceptar.TabIndex = 1;
        this.btAceptar.Text = "OK";
        this.btAceptar.Click += new System.EventHandler(this.bOK_Click);
        // 
        // bCancel
        // 
        this.btCancelar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        this.btCancelar.Location = new Point(504, 336);
        this.btCancelar.Name = "bCancel";
        this.btCancelar.Size = new Size(75, 23);
        this.btCancelar.TabIndex = 2;
        this.btCancelar.Text = "Cancel";
        this.btCancelar.Click += new System.EventHandler(this.bCancel_Click);
        // 
        // HtmlEditor
        // 
        this.AcceptButton = this.btAceptar;
        this.AutoScaleBaseSize = new Size(5, 13);
        this.CancelButton = this.btCancelar;
        this.ClientSize = new Size(592, 366);
        this.Controls.Add(this.btCancelar);
        this.Controls.Add(this.btAceptar);
        this.Controls.Add(this.tbHtml);
        this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "HtmlEditor";
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Html";
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion
}